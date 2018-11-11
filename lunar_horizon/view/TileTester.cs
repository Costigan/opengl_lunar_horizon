using lunar_horizon.gpu;
using lunar_horizon.patches;
using lunar_horizon.terrain;
using lunar_horizon.tiles;
using lunar_horizon.views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static lunar_horizon.views.MapView;

namespace lunar_horizon.view
{
    public enum PatchRenderMode { Hillshade, AzEl, ShadowCaster }
    public partial class TileTester : Form
    {
        public List<TerrainPatch> Patches;
        public Rectangle PatchBounds;
        TerrainPatch[,] _patchArray;
        DisplayTransform _transform;
        PickablePanel pnlTiles;
        public Point? Selection1;
        public List<Point> Selection2 = new List<Point>();

        Pen _selection1Pen = new Pen(Brushes.Green, 2);
        Pen _selection2Pen = new Pen(Brushes.Blue, 2);

        public math.Vector3d ShadowCaster;

        float azimuth_deg, elevation_deg;

        public PatchRenderMode RenderMode = PatchRenderMode.Hillshade;

        Dictionary<FromToLabel, ShadowFromTo> _fromToDictionary = new Dictionary<FromToLabel, ShadowFromTo>();
        Dictionary<Point, ShadowFromTo> _selfShadowDictionary = new Dictionary<Point, ShadowFromTo>();

        protected DateTime _currentTime = DateTime.UtcNow;
        protected TimeSpan _timeSpan = new TimeSpan(3 * 28, 0, 0);

        PlotWindow _plotWindow;

        TerrainPatch _gpu;
        TerrainPatch _delta;
        TerrainPatch _cpu;

        public TileTester(List<TerrainPatch> patches = null)
        {
            InitializeComponent();
            pnlTiles = new PickablePanel { Location = new Point(0, 0), Dock = DockStyle.Fill };
            pnlTiles.Paint += pnlTiles_Paint;
            pnlScroll.Controls.Add(pnlTiles);

            _transform = new DisplayTransform { OffsetX = 0, OffsetY = 0, Scale = 1f };
            Patches = patches;
            if (Patches != null)
                Load(Patches);
            cbWhichHorizon.SelectedIndex = 0;
        }

        public TerrainPatch[,] PatchArray => _patchArray;
        public InMemoryTerrainManager Terrain => TileTreeNode.Terrain;

        public bool Contains(Point p) => _patchArray != null && _patchArray.GetLength(0) > p.X && _patchArray.GetLength(1) > p.Y;

        private void Load(List<TerrainPatch> patches)
        {
            if (patches.Count < 1) return;
            var patchSize = TerrainPatch.DefaultSize;
            var ids = patches.Select(p => p.Id).ToList();
            var minline = ids.Min(p => p.Y);
            var maxline = ids.Max(p => p.Y);
            var minsample = ids.Min(p => p.X);
            var maxsample = ids.Max(p => p.X);
            PatchBounds = new Rectangle(minsample, minline, maxsample - minsample + 1, maxline - minline + 1);
            _patchArray = new TerrainPatch[PatchBounds.Height, PatchBounds.Width];
            for (var line = 0; line < PatchBounds.Height; line++)
                for (var sample = 0; sample < PatchBounds.Width; sample++)
                    _patchArray[line, sample] = new TerrainPatch { Line = (line + PatchBounds.Top) * patchSize, Sample = (sample + PatchBounds.Left) * patchSize, Height = patchSize, Width = patchSize, Step = 1 };
            pnlTiles.Size = new Size(PatchBounds.Width * patchSize, PatchBounds.Height * patchSize);
            _transform = new DisplayTransform { OffsetX = -PatchBounds.Left * patchSize, OffsetY = -PatchBounds.Top * patchSize, Scale = 1f };
            pnlScroll.Invalidate();
            pnlTiles.Invalidate();
        }

        private void pnlTiles_Paint(object sender, PaintEventArgs e)
        {
            if (_patchArray == null) return;
            for (var line = 0; line < _patchArray.GetLength(0); line++)
                for (var sample = 0; sample < _patchArray.GetLength(1); sample++)
                {
                    var t = _patchArray[line, sample];
                    t.Draw(e.Graphics, _transform, GetBitmap(t));
                }
            for (var line = 0; line < _patchArray.GetLength(0); line++)
                for (var sample = 0; sample < _patchArray.GetLength(1); sample++)
                {
                    var t = _patchArray[line, sample];
                    t.Draw(e.Graphics, _transform, Pens.DarkRed);
                }
            if (Selection1.HasValue)
                _patchArray[Selection1.Value.Y, Selection1.Value.X].Draw(e.Graphics, _transform, _selection1Pen);
            foreach (var pt in Selection2)
                if (pt.Y < _patchArray.GetLength(0) && pt.X < _patchArray.GetLength(1))
                    _patchArray[pt.Y, pt.X].Draw(e.Graphics, _transform, _selection2Pen);
        }

        Bitmap GetBitmap(TerrainPatch p)
        {
            switch (RenderMode)
            {
                case PatchRenderMode.AzEl:
                    return p.Horizons != null ? p.GetShadows(azimuth_deg, elevation_deg) : p.GetHillshade();
                case PatchRenderMode.ShadowCaster:
                    return p.Horizons != null ? p.GetShadows(ShadowCaster) : p.GetHillshade();
                case PatchRenderMode.Hillshade:
                default:
                    return p.GetHillshade();
            }
        }

        public TerrainPatch GetPatch(Point id)
        {
            if (_patchArray == null || id.Y >= _patchArray.GetLength(0) || id.X >= _patchArray.GetLength(1)) return null;
            return _patchArray[id.Y, id.X];
        }

        private void btnSelect1_Click(object sender, EventArgs e)
        {
            pnlTiles.ReplaceMouseMode(new mouse.SelectTesterPatch { Selection = 1 });
        }

        private void btnSelect2_Click(object sender, EventArgs e)
        {
            pnlTiles.ReplaceMouseMode(new mouse.SelectTesterPatch { Selection = 2 });
        }

        private void btnPlotMode_Click(object sender, EventArgs e)
        {
            if (_plotWindow==null)
                _plotWindow = new PlotWindow();
            _plotWindow.Show();
            pnlTiles.ReplaceMouseMode(new mouse.DragRenderPoint2 { Owner = this });
        }

        internal void DrawHorizon(Point p)
        {
            var target = GetPatchForHorizon();
            if (target == null) return;
            _plotWindow.DrawHorizon(target, p.Y, p.X);
        }

        TerrainPatch GetPatchForHorizon()
        {
            switch (cbWhichHorizon.SelectedIndex)
            {
                case 0:
                default:
                    return GetPatch(Selection1.Value);
                case 1:
                    return _cpu;
                case 2:
                    return _gpu;
                case 3:
                    return _delta;
            }
        }

        private void tbAzimuth_ValueChanged(object sender, EventArgs e)
        {
            azimuth_deg = tbAzimuth.Value;
            RenderMode = PatchRenderMode.AzEl;
            pnlTiles.Invalidate();
        }

        private void tbElevation_ValueChanged(object sender, EventArgs e)
        {
            elevation_deg = tbElevation.Value / 10f;
            RenderMode = PatchRenderMode.AzEl;
            pnlTiles.Invalidate();
        }

        private void btnClearCache_Click(object sender, EventArgs e)
        {
            _fromToDictionary = new Dictionary<FromToLabel, ShadowFromTo>();
        }

        private void btnWriteCache_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"Writing {_fromToDictionary.Count} from-to horizons");
            foreach (var ft in _fromToDictionary.Values)
                ft.Write();
            foreach (var ft in _selfShadowDictionary.Values)
                ft.Write();
            Console.WriteLine("Done");
        }

        private void tbSelectTime_Scroll(object sender, EventArgs e)
        {
            UpdateCurrentTime();
        }

        protected void UpdateCurrentTime()
        {
            _currentTime = new DateTime(dtStartTime.Value.Ticks + (long)(_timeSpan.Ticks * (tbSelectTime.Value / (float)tbSelectTime.Maximum)));
            lbCurrentTime.Text = _currentTime.ToString();
            //UpdateShadowCasters();
            ShadowCaster = LunarHorizon.SunPosition(_currentTime);
            RenderMode = PatchRenderMode.ShadowCaster;
            pnlTiles.Invalidate();
        }

        private void cbTimeSpan_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbTimeSpan.SelectedIndex)
            {
                case 0:
                    _timeSpan = new TimeSpan(3 * 28, 0, 0, 0, 0);
                    break;
                case 1:
                    _timeSpan = new TimeSpan(2 * 28, 0, 0, 0, 0);
                    break;
                case 2:
                    _timeSpan = new TimeSpan(1 * 28, 0, 0, 0, 0);
                    break;
                case 3:
                    _timeSpan = new TimeSpan(2 * 7, 0, 0, 0, 0);
                    break;
                case 4:
                    _timeSpan = new TimeSpan(7, 0, 0, 0, 0);
                    break;
                case 5:
                    _timeSpan = new TimeSpan(1, 0, 0, 0, 0);
                    break;
                case 6:
                default:
                    _timeSpan = new TimeSpan(0, 1, 0, 0, 0);
                    break;
            }
            UpdateCurrentTime();
        }

        private void dtStartTime_ValueChanged(object sender, EventArgs e)
        {
            UpdateCurrentTime();
        }

        private void btnShadow2To1_Click(object sender, EventArgs e)
        {
            if (!(Selection1.HasValue && ( Selection2.Count > 0 || cbSelfShadow.Checked)))
                return;

            var useCache = cbUseCache.Checked;

            var to_patch = GetPatch(Selection1.Value);
            var to_key = to_patch.Id;

            foreach (var local_from_key in Selection2)
            {
                var from_key = ToGlobalKey(local_from_key);
                var label = new FromToLabel(from_key.X, from_key.Y, to_key.X, to_key.Y);
                if (!useCache || !_fromToDictionary.ContainsKey(label))
                {
                    ShadowFromTo fromto = null;
                    if (useCache)
                    {
                        string path = ShadowFromTo.GenerateDefaultPath(from_key.X, from_key.Y, to_key.X, to_key.Y);
                        if (File.Exists(path))
                        {
                            fromto = ShadowFromTo.ReadFrom(path);
                            _fromToDictionary[label] = fromto;
                        }
                    }
                    if (fromto != null)
                        continue;
                    var s2 = GetPatch(local_from_key);
                    to_patch.FillPointsAndMatrices(Terrain);
                    s2.FillPoints(Terrain);
                    to_patch.InitializeHorizons();
                    to_patch.UpdateHorizon(s2);
                    fromto = new ShadowFromTo(from_key, to_key);
                    fromto.Horizons = to_patch.Horizons;
                    _fromToDictionary[label] = fromto;
                    to_patch.InitializeHorizons();
                }
            }

            ShadowFromTo selfShadow = null;
            if (cbSelfShadow.Checked)
            {
                var path = ShadowFromTo.GenerateSelfShadowPath(to_key);
                if (File.Exists(path))
                    selfShadow = ShadowFromTo.ReadFrom(path);
                else
                {
                    to_patch.FillPointsAndMatrices(Terrain);
                    to_patch.InitializeHorizons();
                    to_patch.ParallelAddNearHorizon(Terrain);
                    var self_shadow = new ShadowFromTo(to_key, to_key);
                    self_shadow.CopyFrom(to_patch.Horizons);
                    _selfShadowDictionary[to_key] = self_shadow;
                }
            }

            to_patch.InitializeHorizons();
            if (selfShadow != null)
                for (var line = 0; line < TerrainPatch.DefaultSize; line++)
                    for (var sample = 0; sample < TerrainPatch.DefaultSize; sample++)
                        to_patch.Horizons[line][sample].Merge(selfShadow.Horizons[line][sample]);

            foreach (var local_from_key in Selection2)
            {
                var from_key = ToGlobalKey(local_from_key);
                var label = new FromToLabel(from_key.X, from_key.Y, to_key.X, to_key.Y);
                var other = _fromToDictionary[label];
                for (var line = 0; line < TerrainPatch.DefaultSize; line++)
                    for (var sample = 0; sample < TerrainPatch.DefaultSize; sample++)
                        to_patch.Horizons[line][sample].Merge(other.Horizons[line][sample]);
            }

            for (var line = 0; line < TerrainPatch.DefaultSize; line++)
                for (var sample = 0; sample < TerrainPatch.DefaultSize; sample++)
                    to_patch.Horizons[line][sample].ConvertSlopeToDegrees();

            RenderMode = PatchRenderMode.AzEl;
            Invalidate();
        }

        private void fillTest1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ids = new List<Point>();
            for (var x = 100; x <= 106; x++)
                for (var y = 136; y <= 142; y++)
                    ids.Add(new Point(x, y));
            Patches = ids.Select(TerrainPatch.FromId).ToList();
            Load(Patches);
            Invalidate();
        }

        private void searchForBug1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var bug = new BugHunt();
            bug.Test1();
        }

        private void btnGpuCalculate_Click(object sender, EventArgs e)
        {
            if (!(Selection1.HasValue && (Selection2.Count > 0 || cbGpuSelfShadow.Checked)))
                return;

            var processor = new GPUProcessor { Terrain = Terrain };

            var useCache = cbUseCache.Checked;

            var target = GetPatch(Selection1.Value);

            ShadowFromTo selfShadow = null;
            if (cbGpuSelfShadow.Checked)
            {
                var to_key = target.Id;
                var path = ShadowFromTo.GenerateSelfShadowPath(to_key);
                if (useCache && File.Exists(path))
                    selfShadow = ShadowFromTo.ReadFrom(path);
                else
                {
                    target.InitializeHorizons();
                    if (false)
                    {
                        target.FillPointsAndMatrices(Terrain);
                        target.ParallelAddNearHorizon(Terrain);
                    }
                    else
                    {
                        target.FillPoints(Terrain);
                        target.FillMatricesRelativeToPoint(Terrain, target.Points[0][0]);

                        // GPU
                        processor.AddNearHorizon(target);
                    }

                    var self_shadow = new ShadowFromTo(to_key, to_key);
                    self_shadow.CopyFrom(target.Horizons);
                    _selfShadowDictionary[to_key] = self_shadow;
                }
            }
            else
            {
                target.InitializeHorizons();
            }

            if (selfShadow != null)
                for (var line = 0; line < TerrainPatch.DefaultSize; line++)
                    for (var sample = 0; sample < TerrainPatch.DefaultSize; sample++)
                        target.Horizons[line][sample].Merge(selfShadow.Horizons[line][sample]);

            // Now, for the main show
            var casters = Selection2.Select(ToGlobalKey).Select(TerrainPatch.FromId).ToList();

            processor.UpdateHorizons(target, casters);

            for (var line = 0; line < TerrainPatch.DefaultSize; line++)
                for (var sample = 0; sample < TerrainPatch.DefaultSize; sample++)
                    target.Horizons[line][sample].ConvertSlopeToDegrees();

            RenderMode = PatchRenderMode.AzEl;
            Invalidate();
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            if (!(Selection1.HasValue && (Selection2.Count > 0 || cbSelfShadow.Checked)))
                return;
            Console.Write(@"Comparing ... ");

            var target = GetPatch(Selection1.Value);
            target.InitializeHorizons();
            target.Matrices = null;  // Ensure these are generated fresh
            btnGpuCalculate_Click(sender, e);
            _gpu = new TerrainPatch();
            _gpu.Horizons = target.Horizons;

            target.InitializeHorizons();
            target.Matrices = null;  // Ensure these are generated fresh (different from matrices for gpu)
            btnShadow2To1_Click(sender, e);
            _cpu = new TerrainPatch();
            _cpu.Horizons = target.Horizons;

            _delta = new TerrainPatch();
            _delta.InitializeHorizons();

            // Subtract
            for (var i = 0; i < _cpu.Horizons.Length; i++)
                for (var j = 0; j < _cpu.Horizons.Length; j++)
                {
                    var g = _gpu.Horizons[i][j].Buffer;
                    var d = _delta.Horizons[i][j].Buffer;
                    var c = _cpu.Horizons[i][j].Buffer;
                    for (var k = 0; k < d.Length; k++)
                        d[k] = c[k] - g[k];
                }
            Console.WriteLine(@"Done");

            Console.WriteLine($"delta[0,0]={_delta.Horizons[0][0].Buffer[0]}");
        }

        Point ToGlobalKey(Point local)
        {
            var p = _patchArray[local.Y, local.X];
            return p.Id;
        }
    }

    public class FromToLabel
    {
        public readonly Point From;
        public readonly Point To;

        public FromToLabel(Point from, Point to)
        {
            From = from;
            To = to;
        }

        public override string ToString() => $"Label[{From.X},{From.Y}]->[{To.X},{To.Y}]>";

        public FromToLabel(int fromx, int fromy, int tox, int toy)
        {
            From = new Point(fromx, fromy);
            To = new Point(tox, toy);
        }

        public override bool Equals(object obj)
        {
            var o = obj as FromToLabel;
            if (o == null) return false;
            return From.Equals(o.From) && To.Equals(o.To);
        }

        public override int GetHashCode()
        {
            return 3 * From.X + 5 * From.Y + 7 * To.X + 13 * To.Y;
        }
    }

    public class ShadowFromTo
    {
        public static string FromToRoot = @"c:/RP/tiles/np/from_to";
        public string Path;
        public readonly Point From;
        public readonly Point To;
        public Horizon[][] Horizons;

        public ShadowFromTo(Point from, Point to)
        {
            From = from;
            To = to;
        }

        public override string ToString() => $"Shadow[{From.X},{From.Y}]->[{To.X},{To.Y}]>";


        #region Persist to Disk

        public static string GenerateDefaultFilename(int fx, int fy, int tx, int ty) => string.Format(@"from_{0:D4}_{1:D4}_to_{2:D4}_{3:D4}.patch", fx,fy,tx,ty);
        public static string GenerateDefaultPath(int fx, int fy, int tx, int ty) => System.IO.Path.Combine(FromToRoot, GenerateDefaultFilename(fx, fy, tx, ty));
        public string DefaultPath => GenerateDefaultPath(From.X, From.Y, To.X, To.Y);

        public static string GenerateSelfShadowPath(Point p) => GenerateDefaultPath(p.X, p.Y, p.X, p.Y);

        public static ShadowFromTo ReadFrom(string path)
        {
            using (var fs = File.OpenRead(path))
            using (var br = new BinaryReader(fs))
            {
                var from = new Point(br.ReadInt32(), br.ReadInt32());
                var to = new Point(br.ReadInt32(), br.ReadInt32());
                var p = new ShadowFromTo(from, to) { Path = path };

                p.Horizons = new Horizon[TerrainPatch.DefaultSize][];
                for (var h = 0; h < TerrainPatch.DefaultSize; h++)
                {
                    p.Horizons[h] = new Horizon[TerrainPatch.DefaultSize];
                    for (var w = 0; w < TerrainPatch.DefaultSize; w++)
                    {
                        var horizon = new Horizon().Init();
                        var buf = horizon.Buffer;
                        var ptr = 0;
                        while (ptr < Horizon.HorizonSamples)
                        {
                            ptr += br.ReadInt16();          // read and skip toSkip
                            var toRead = br.ReadInt16();    // read m
                            for (var j = 0; j < toRead; j++)
                                buf[ptr++] = br.ReadSingle();
                        }
                        p.Horizons[h][w] = horizon;
                    }
                }
                return p;
            }
        }

        public void Write(string path = null)
        {
            path = path ?? DefaultPath;
            using (var fs = File.Create(path))
            using (var bw = new BinaryWriter(fs))
            {
                bw.Write(From.X);
                bw.Write(From.Y);
                bw.Write(To.X);
                bw.Write(To.Y);

                for (var y = 0; y < TerrainPatch.DefaultSize; y++)
                {
                    for (var x = 0; x < TerrainPatch.DefaultSize; x++)
                    {
                        var horizons = Horizons[y][x];
                        var buf = horizons.Buffer;
                        System.Diagnostics.Debug.Assert(buf.Length == Horizon.HorizonSamples);

                        var ptr = 0;
                        while (ptr < Horizon.HorizonSamples)
                        {
                            int toSkip = CalculateToSkip(ptr, buf);
                            bw.Write((Int16)toSkip);
                            ptr += toSkip;
                            int toWrite = CalculateToWrite(ptr, buf);
                            bw.Write((Int16)toWrite);
                            for (var j = 0; j < toWrite; j++)
                                bw.Write(buf[ptr++]);
                        }
                    }
                }
            }
        }

        private int CalculateToSkip(int start, float[] buf)
        {
            var ptr = start;
            while (ptr < buf.Length)
            {
                if (buf[ptr] != float.MinValue)
                    return ptr - start;
                ptr++;
            }
            return ptr - start;
        }

        private int CalculateToWrite(int start, float[] buf)
        {
            var ptr = start;
            while (ptr < buf.Length)
            {
                if (buf[ptr] == float.MinValue)
                    return ptr - start;
                ptr++;
            }
            return ptr - start;
        }

        public void CopyFrom(Horizon[][] horizons)
        {
            if (Horizons == null)
            {
                Horizons = new Horizon[TerrainPatch.DefaultSize][];
                for (var line = 0; line < TerrainPatch.DefaultSize; line++)
                {
                    var ary = new Horizon[TerrainPatch.DefaultSize];
                    for (var sample = 0; sample < TerrainPatch.DefaultSize; sample++)
                        ary[sample] = new Horizon();
                    Horizons[line] = ary;
                }
            }
            for (var line = 0; line < TerrainPatch.DefaultSize; line++)
                for (var sample = 0; sample < TerrainPatch.DefaultSize; sample++)
                {
                    var to = Horizons[line][sample].Buffer;
                    var from = horizons[line][sample].Buffer;
                    Array.Copy(from, to, to.Length);
                }
        }

        public static void TestShadowFromTo()
        {
            var ft1 = new ShadowFromTo(new Point(0,0),new Point(1,1));

            var ary = new Horizon[TerrainPatch.DefaultSize][];
            for (var i = 0; i < TerrainPatch.DefaultSize; i++)
                ary[i] = new Horizon[TerrainPatch.DefaultSize];
            for (var i = 0; i < TerrainPatch.DefaultSize; i++)
                for (var j = 0; j < TerrainPatch.DefaultSize; j++)
                    ary[i][j] = new Horizon().Init();
            ft1.Horizons = ary;

            var r = new Random();
            var totalSamples = TerrainPatch.DefaultSize * TerrainPatch.DefaultSize *  Horizon.HorizonSamples;

            for (var i=0;i<1000;i++)
            {
                var len = (int)(100 * r.NextDouble());
                var start = (int)(totalSamples * r.NextDouble());
                var stop = start + len;
                for (var ptr = start; ptr<stop;ptr++)
                {
                    var index = ptr % totalSamples;
                    var z = index % Horizon.HorizonSamples;
                    index /= Horizon.HorizonSamples;
                    var y = index % TerrainPatch.DefaultSize;
                    index /= TerrainPatch.DefaultSize;
                    var x = index;
                    ary[x][y].Buffer[z] = ptr;
                }
            }

            const string filename = "test_ft.bin";
            ft1.Write(filename);
            var ft2 = ShadowFromTo.ReadFrom(filename);

            for (var x = 0; x < TerrainPatch.DefaultSize; x++)
                for (var y = 0; y < TerrainPatch.DefaultSize; y++)
                    for (var z = 0; z < Horizon.HorizonSamples; z++)
                        if (ft1.Horizons[x][y].Buffer[z] != ft2.Horizons[x][y].Buffer[z])
                            Console.WriteLine($"Error in cell {x} {y} {z}");

        }

        #endregion
    }
}
