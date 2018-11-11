using lunar_horizon.patches;
using lunar_horizon.terrain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using lunar_horizon.gpu;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace lunar_horizon.view
{
    public partial class MeshGeneratorWindow : Form
    {
        const string boxCachePath = "box_cache.json";

        public LunarHorizon MainWindow;
        public InMemoryTerrainManager Terrain;

        Dictionary<Color, Brush> _brushes = new Dictionary<Color, Brush>();
        Dictionary<Point, Rectangle> boxCache;

        string _distances;

        public MeshGeneratorWindow()
        {
            InitializeComponent();
            cbZoom.SelectedIndex = 0;
            LoadBoxCache();
        }

        List<Point> GetHighResolutionPoints() => MainWindow.MapView.SelectedPatches.Select(p => p.Id).ToList();

        public void GetHorizonBoxes(List<Point> ids, Action<List<Rectangle>> callback) =>
             Task.Run(() =>
             {
                 var gpuProcessor = MainWindow.Processor as GPUProcessor;
                 var rectangles = new List<Rectangle>();
                 foreach (var id in ids)
                 {
                     gpuProcessor.RunQueue(new List<TerrainPatch> { TerrainPatch.FromId(id) }, null, writeHorizons: false);
                     if (gpuProcessor.BoundingBox.HasValue)
                         rectangles.Add(gpuProcessor.BoundingBox.Value);
                 }
                 callback(rectangles);
             });

        private void btnShowCoverage_Click(object sender, EventArgs e)
        {
            var highres = GetHighResolutionPoints();
            GetBoundingBox(highres, boundingBox =>
            {
                if (!boundingBox.HasValue)
                {
                    MessageBox.Show("No bounding box found");
                    return;
                }
                var distanceToZoom = GetDistanceToZoom();

                var g = new MeshGenerator { Terrain = Terrain, DistanceToZoom = distanceToZoom };
                g.GenerateMesh(highres, boundingBox.Value);
                var vertex_count = g.GenerateVertices();
                g.GenerateFaces();
                var faces = g.EnumerateFaces().Count();

                UpdateStats(vertex_count, faces);

                MainWindow.MapView.ColoredPatches = GetColoredPatches(g);
                MainWindow.MapView.Invalidate();
            });
        }

        void UpdateStats(int vertices, int faces) => UpdateStats($"v={vertices} f={faces}");

        delegate void StringArgReturningVoidDelegate(string text);
        void UpdateStats(string msg)
        {
            if (tbStats.InvokeRequired)
                tbStats.Invoke(new StringArgReturningVoidDelegate(UpdateStats), msg);
            else
                tbStats.Text = msg;
        }

        Dictionary<float, int> GetDistanceToZoom()
        {
            var distances = _distances.Split(',').Select(s => float.Parse(s.Trim())).ToArray();
            var zooms = Enumerable.Range(0, distances.Length).Select(i => (int)Math.Pow(2, i)).ToArray();
            //distances[distances.Length - 1] = 1000f;
            var dict = new Dictionary<float, int>();
            for (var i = 0; i < distances.Length; i++)
                dict.Add(distances[i], zooms[i]);
            return dict;
        }

        List<Tuple<Point, Brush>> GetColoredPatches(MeshGenerator g)
        {
            var coloredPatches = new List<Tuple<Point, Brush>>();
            var zoomToColor = g.ZoomToColor;
            foreach (var kv in g.Allocated)
            {
                var color = Color.White;
                if (!zoomToColor.TryGetValue(kv.Value.Zoom, out color))
                    Console.WriteLine("Shouldn't get here");
                if (!_brushes.TryGetValue(color, out Brush brush))
                {
                    brush = new SolidBrush(Color.FromArgb(200, color));
                    _brushes.Add(color, brush);
                }
                coloredPatches.Add(Tuple.Create(kv.Key, brush));
            }
            return coloredPatches;
        }

        private void btnWriteMesh_Click(object sender, EventArgs e)
        {
            var highres = GetHighResolutionPoints();
            GetBoundingBox(highres, boundingBox =>
            {
                if (!boundingBox.HasValue)
                {
                    MessageBox.Show("No bounding box found");
                    return;
                }
                var distanceToZoom = GetDistanceToZoom();

                var g = new MeshGenerator { Terrain = Terrain, DistanceToZoom = distanceToZoom };
                g.GenerateMesh(highres, boundingBox.Value);
                var vertex_count = g.GenerateVertices();
                g.GenerateFaces();
                var faces = g.EnumerateFaces().Count();

                UpdateStats(vertex_count, faces);

                var filename = $"mesh_{DateTime.Now.ToString("yyyyMMddhhmmss")}.ply";
                g.WritePlyFile(Terrain, filename, writeASCII: cbAscii.Checked, writeColorVertices: cbIncludeColors.Checked);

                MainWindow.MapView.ColoredPatches = GetColoredPatches(g);
                MainWindow.MapView.Invalidate();
            });
        }

        #region Bounding Box

        void GetBoundingBox(List<Point> patch_ids, Action<Rectangle?> box_callback)
        {
            if (boxCache == null)
                LoadBoxCache();
            var ids_not_in_cache = patch_ids.Where(id => !boxCache.ContainsKey(id)).ToList();
            MainWindow.RunQueue(ids_not_in_cache, newBoxes =>
            {
                Debug.Assert(ids_not_in_cache.Count == newBoxes.Count);
                for (var i = 0; i < ids_not_in_cache.Count; i++)
                    boxCache.Add(ids_not_in_cache[i], newBoxes[i]);
                WriteBoxCache();
                var boundingBox = BoundingBox(patch_ids);
                box_callback(boundingBox);
            });
        }

        void LoadBoxCache()
        {
            boxCache = new Dictionary<Point, Rectangle>();
            if (File.Exists(boxCachePath))
                using (StreamReader sr = File.OpenText(boxCachePath))
                using (var jw = new JsonTextReader(sr))
                {
                    var serializer = new JsonSerializer { TypeNameHandling = TypeNameHandling.None };
                    boxCache = serializer.Deserialize<Dictionary<Point, Rectangle>>(jw);
                }
        }

        void WriteBoxCache()
        {
            using (StreamWriter sw = File.CreateText(boxCachePath))
            using (var jw = new JsonTextWriter(sw) { Formatting = Formatting.Indented })
            {
                var serializer = new JsonSerializer { TypeNameHandling = TypeNameHandling.None };
                serializer.Serialize(jw, boxCache);
            }
        }

        Rectangle? BoundingBox(List<Point> ids)
        {
            if (ids.Count < 1)
                return null;
            Rectangle r = boxCache[ids[0]];
            foreach (var id in ids.Skip(1))
                r = Rectangle.Union(r, boxCache[id]);
            return r;
        }

        #endregion

        private void cbZoom_TextChanged(object sender, EventArgs e)
        {
            _distances = cbZoom.Text;
        }
    }
}
