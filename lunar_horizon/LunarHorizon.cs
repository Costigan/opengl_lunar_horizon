using lunar_horizon.batch;
using lunar_horizon.calc;
using lunar_horizon.gpu;
using lunar_horizon.math;
using lunar_horizon.patches;
using lunar_horizon.pds;
using lunar_horizon.spice;
using lunar_horizon.terrain;
using lunar_horizon.tiles;
using lunar_horizon.utilities;
using lunar_horizon.view;
using lunar_horizon.views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace lunar_horizon
{
    public partial class LunarHorizon : Form
    {
        public enum ViewType { Hillshade, Slope, CurrentIceStability, PSR, AverageSun, AverageEarth };
        public static int MaxDegreeOfParallelism = 6;

        public const string TileRoot = @"c:/RP/tiles";
        public static string MapRoot = @"c:/RP/tiles/np";
        public static string HorizonsRoot = @"c:/RP/tiles/np/horizons";
        public const string SouthPsrImgPath = @"c:/RP/LOLA/LPSR_85S_060M_201608_DEM_byte.img";
        public const string NorthPsrImgPath = @"c:/RP/LOLA/LPSR_85N_060M_201608_DEM_byte.img";  // not right yet
        public const string SouthAvgSunImgPath = @"c:/RP/LOLA/AVGVISIB_85S_060M_201608_DEM.img";
        public const string NorthAvgSunImgPath = @"c:/RP/LOLA/AVGVISIB_85N_060M_201608_DEM.img";  // not right yet
        public const string SouthAvgEarthImgPath = @"c:/RP/LOLA/AVGVISIB_85S_060M_201608_DEM_EARTH.img";
        public const string NorthAvgEarthImgPath = @"c:/RP/LOLA/AVGVISIB_85N_060M_201608_DEM_EARTH.img";  // not right yet

        public const string HorizonDirectoryName = @"horizons";

        protected ViewType CurrentViewType = ViewType.Hillshade;

        private readonly ZedGraphControl plotControl;
        private readonly ZedGraphControl renderPlot;

        private bool _mouseDown;
        private TerrainPatch _patch;

        public static InMemoryTerrainManager Terrain;
        private MemoryMappedImgFile PsrImgFile, AverageSunImgFile, AverageEarthImgFile;
        public readonly MapView MapView;

        public List<IMapLayer> Layers = new List<IMapLayer>();

        public Dictionary<Point, TerrainPatch> PatchCache = new Dictionary<Point, TerrainPatch>();

        public List<TerrainPatch> ShadowCalculationQueue;

        protected TerrainPatch _renderTestPatch;
        protected float _renderTestAzimuth;
        protected float _renderTestElevation;

        protected DateTime _currentTime = DateTime.UtcNow;
        protected TimeSpan _timeSpan = new TimeSpan(3 * 28, 0, 0);

        public SpiceManager Spice;
        public QueueProcessor Processor;

        public ShadowCasterWindow CasterWindow;
        public CompareToAndy CompareToAndy;

        MeshGeneratorWindow _meshGeneratorWindow = null;

        public static LunarHorizon Singleton;

        private viz.LightCurveForm _lightCurveForm;
        public viz.LightCurveForm LightCurveForm => _lightCurveForm != null ? _lightCurveForm : _lightCurveForm = new viz.LightCurveForm();

        protected bool _isUpdating;
        protected bool _trackbarIsUpdating;

        Dictionary<IMapLayer, UserControl> _layerPropertySheets = new Dictionary<IMapLayer, UserControl>();

        #region Initialization

        public LunarHorizon()
        {
            InitializeComponent();

            Spice = SpiceManager.GetSingleton();

            var zed = plotControl = new ZedGraphControl { Dock = DockStyle.Fill };
            pnlZedgraphContainer.Controls.Add(zed);

            renderPlot = new ZedGraphControl { Dock = DockStyle.Fill };
            pnlPlot.Controls.Add(renderPlot);

            if (false)
                LoadNorth();
            else
                LoadSouth();

            UpdateLayerTransparencyText();

            MapView = new MapView { Dock = DockStyle.Fill, MainWindow = this };
            tabMap.Controls.Add(MapView);
            StatusManager.StatusLabel = lblLatLon;
            UpdateCurrentTime();
            cbTimeSpan.SelectedIndex = 2;
            cbProcessor.SelectedIndex = 1;
            cbSurroundings.SelectedIndex = 11;
            TileTreeNode.MapView = MapView;
            Singleton = this;
            UpdateFromLayers(Layers);
            GetItem("Hillshade").Checked = true;

            pnlPlot.Visible = false;

            TimeChanged += time => LocationProbeMapLayer.DefaultPropertySheet.TimeChanged(time);
        }

        #endregion

        public bool IsNorth => InMemoryTerrainManager.Singleton.IsNorth;

        public void SetStatus(bool active)
        {
            if (active)
            {
                Invoke((MethodInvoker)(() =>
                {
                    lbStatus.Text = "Running";
                    lbStatus.BackColor = Color.PaleVioletRed;
                }));
            }
            else
            {
                Invoke((MethodInvoker)(() =>
                {
                    lbStatus.Text = "Idle";
                    lbStatus.BackColor = Color.LightGray;
                }));
            }
        }

        private void DrawHorizon(TerrainPatch a, int line = 0, int sample = 0)
        {
            var pane = plotControl.GraphPane;
            pane.CurveList.Clear();
            var horizon = a.GetHorizon(line, sample);
            PointPairList points = null;
            foreach (var pt in horizon.PartialPoints())
            {
                if (pt.HasValue)
                {
                    (points ?? (points = new PointPairList())).Add(pt.Value.X, pt.Value.Y);
                }
                else if (points != null)
                {
                    pane.AddCurve(null, points, Color.Blue, SymbolType.None);
                    points = null;
                }
            }
            if (points != null)
                pane.AddCurve(null, points, Color.Blue, SymbolType.None);
            pane.XAxis.Title.Text = "Azimuth";
            pane.YAxis.Title.Text = "Elevation";
            pane.Title.Text = "horizon";
            pane.AxisChange();
            plotControl.Invalidate();
        }

        internal void RenderElevation(PointPairList points)
            => RenderPlot(new LineItem("elevation", points, Color.Black, SymbolType.None), x_label: "along path", y_label: "meters");

        protected Point _requestedPoint = new Point();
        protected Task<TerrainPatch> renderTask = null;
        public void RenderHorizon(Point p)
        {
            _requestedPoint = p;
            if (renderTask != null)
                return;
            Task.Run(() => Tuple.Create(LunarHorizon.Singleton.GetPatch(TerrainPatch.LineSampleToId(p.Y, p.X)), p))
                .ContinueWith(prev =>
                {
                    var patch = prev.Result.Item1;
                    if (patch == null) return;
                    var horizon = patch.GetHorizon(_requestedPoint.Y - patch.Id.Y * TerrainPatch.DefaultSize, _requestedPoint.X - patch.Id.X * TerrainPatch.DefaultSize);
                    PointPairList points = null;
                    foreach (var pt in horizon.PartialPoints().Where(pt => pt.HasValue))
                        (points ?? (points = new PointPairList())).Add(pt.Value.X, pt.Value.Y);
                    var curve = new LineItem("horizon", points, Color.Black, SymbolType.None);
                    LunarHorizon.Singleton.RenderPlot(curve, x_label: "azimuth", y_label: "elevation");
                });
        }

        public void RenderPlot(CurveItem curve, string x_label = null, string y_label = null, string title = null, bool x_axis_dates = false)
        {
            if (pnlPlot.InvokeRequired)
                pnlPlot.Invoke((MethodInvoker)(() => RenderPlotDelegate(new List<CurveItem> { curve }, x_label, y_label, title, x_axis_dates)));
            else
                RenderPlotDelegate(new List<CurveItem> { curve }, x_label, y_label, title, x_axis_dates);
        }

        public void RenderPlot(List<CurveItem> curves, string x_label = null, string y_label = null, string title = null, bool x_axis_dates = false)
        {
            if (pnlPlot.InvokeRequired)
                pnlPlot.Invoke((MethodInvoker)(() => RenderPlotDelegate(curves, x_label, y_label, title, x_axis_dates)));
            else
                RenderPlotDelegate(curves, x_label, y_label, title, x_axis_dates);
        }

        protected void RenderPlotDelegate(List<CurveItem> curves, string x_label, string y_label, string title, bool x_axis_dates)
        {
            pnlPlot.Visible = renderPlot.Visible = true;
            var pane = renderPlot.GraphPane;
            pane.CurveList.Clear();
            pane.CurveList.AddRange(curves);
            pane.Title.Text = title;
            pane.Title.IsVisible = title != null;
            pane.XAxis.Type = x_axis_dates ? AxisType.Date : AxisType.Linear;
            pane.XAxis.Title.Text = x_label;
            pane.XAxis.Title.IsVisible = x_label != null;
            pane.YAxis.Type = AxisType.Linear;
            pane.YAxis.Title.IsVisible = y_label != null;
            pane.YAxis.Title.Text = y_label;
            pane.AxisChange();
            renderPlot.Invalidate();
        }

        void MakeOnePlotVisible(Control child) => MakeOneChildVisible(pnlPlot, child);

        void MakeOneChildVisible(Control parent, Control child)
        {
            parent.Visible = true;
            foreach (var child2 in pnlPlot.Controls)
                if (child2 is Control c)
                    c.Visible = c == child;
        }

        private void pnlSelectPixel_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseDown = true;
        }

        private void pnlSelectPixel_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_mouseDown) return;
            SelectPixel(e.Y / 2, e.X / 2);
        }

        private void pnlSelectPixel_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseDown = false;
        }

        private void SelectPixel(int line, int sample)
        {
            if (_patch == null) return;
            if (line < 0 || line >= _patch.Height || sample < 0 || sample >= _patch.Width) return;
            DrawHorizon(_patch, line, sample);
            lblPixel.Text = $"line={line} sample={sample}";
        }

        #region Event Handlers

        private void northPoleToolStripMenuItem_Click(object sender, EventArgs e) => LoadNorth();
        private void southPoleToolStripMenuItem_Click(object sender, EventArgs e) => LoadSouth();

        public void LoadNorth()
        {
            Terrain?.Dispose();
            Terrain = new InMemoryTerrainManager();
            Terrain.LoadNorth();
            TileTreeNode.Terrain = Terrain;

            PsrImgFile?.Dispose(true);
            PsrImgFile = new MemoryMappedImgFile().SetNorth().Open(NorthPsrImgPath);

            if (true)
            {
                AverageEarthImgFile?.Dispose(true);
                AverageEarthImgFile = new MemoryMappedImgFile().SetNorth().Open(NorthAvgEarthImgPath);
                AverageSunImgFile?.Dispose(true);
                AverageSunImgFile = new MemoryMappedImgFile().SetNorth().Open(NorthAvgSunImgPath);
            }

            UpdateRoots();
            DeleteAllPatches();

            ReloadLayers();

            if (MapView != null)
            {
                UpdateFromLayers(Layers);
                GetItem("Hillshade").Checked = true;
                MapView.Invalidate();
            }

            northPoleToolStripMenuItem.Checked = true;
            southPoleToolStripMenuItem.Checked = false;
        }

        public void LoadSouth()
        {
            Terrain?.Dispose();
            Terrain = new InMemoryTerrainManager();
            Terrain.LoadSouth();
            TileTreeNode.Terrain = Terrain;

            PsrImgFile?.Dispose(true);
            PsrImgFile = new MemoryMappedImgFile().SetSouth().Open(SouthPsrImgPath);
            AverageSunImgFile?.Dispose(true);
            AverageSunImgFile = new MemoryMappedImgFile().SetSouth().Open(SouthAvgSunImgPath);
            AverageEarthImgFile?.Dispose(true);
            AverageEarthImgFile = new MemoryMappedImgFile().SetSouth().Open(SouthAvgEarthImgPath);

            UpdateRoots();
            DeleteAllPatches();

            ReloadLayers();

            if (MapView != null)
            {
                UpdateFromLayers(Layers);
                GetItem("Hillshade").Checked = true;
                MapView.Invalidate();
            }

            northPoleToolStripMenuItem.Checked = false;
            southPoleToolStripMenuItem.Checked = true;
        }

        private void ReloadLayers()
        {
            var pole_directory = InMemoryTerrainManager.Singleton.IsNorth ? "np" : "sp";
            var cache_root = Path.Combine(TileRoot, pole_directory, "tile_cache");

            var HillshadeFiller = new HillshadeFiller { CacheSubdirectory = "hillshade" };
            var SlopeFiller = new SlopeFiller { CacheSubdirectory = "slope" };
            var IceStabilityFiller = new StabilityDepthFiller { CacheSubdirectory = "ice_depth" };
            var PSRFiller = new PSRFiller { ImgFile = PsrImgFile, CacheSubdirectory = "psr" };
            var AverageSunFiller = new ByteGrayscaleImageFiller { ImgFile = AverageSunImgFile, CacheSubdirectory = "avg_sun" };
            var AverageEarthFiller = new ByteGrayscaleImageFiller { ImgFile = AverageEarthImgFile, CacheSubdirectory = "avg_earth" };

            RemoveAllLayers();
            Layers.Add(new LocationProbeMapLayer { Name = "Location Probes", MainWindow = this });
            Layers.Add(new SelectedPatchesMapLayer { Name = "Selected Patches", MainWindow = this });
            Layers.Add(new FeatureNameMapLayer { Name = "Geographic Features", MainWindow = this });
            Layers.Add(new TileTreeMapLayer { Name = "Permanent Shadow", Tree = TileTree.MakeTree(PSRFiller, cache_root), Transparency = 0.3f });
            Layers.Add(new SunShadowMapLayer { Name = "Sun Shadows", MainWindow = this });
            Layers.Add(new EarthShadowMapLayer { Name = "Earth Shadows", MainWindow = this });
            Layers.Add(new TileTreeMapLayer { Name = "Average Earth", Tree = TileTree.MakeTree(AverageEarthFiller, cache_root) });
            Layers.Add(new TileTreeMapLayer { Name = "Average Sun", Tree = TileTree.MakeTree(AverageSunFiller, cache_root) });
            Layers.Add(new TileTreeMapLayer { Name = "Ice Stability", Tree = TileTree.MakeTree(IceStabilityFiller, cache_root) });
            Layers.Add(new TileTreeMapLayer { Name = "Slope", Tree = TileTree.MakeTree(SlopeFiller, cache_root) });
            Layers.Add(new TileTreeMapLayer { Name = "Hillshade", Tree = TileTree.MakeTree(HillshadeFiller, cache_root) });

            UpdateLayerTransparencyText();
        }

        private void RemoveAllLayers()
        {
            foreach (var layer in Layers)
                layer.Dispose();
            Layers.Clear();
        }

        private void DeleteAllPatches()
        {
            if (MapView != null && MapView.FinishedPatches != null)
            {
                foreach (var patch in MapView.FinishedPatches)
                    patch.Dispose();
                MapView.FinishedPatches = null;
            }
            foreach (var patch in PatchCache.Values)
                patch.Dispose();
            PatchCache.Clear();
        }

        void UpdateRoots()
        {
            var pole_directory = InMemoryTerrainManager.Singleton.IsNorth ? "np" : "sp";
            MapRoot = Path.Combine(TileRoot, pole_directory);
            HorizonsRoot = Path.Combine(MapRoot, HorizonDirectoryName);
        }

        void UncheckViewOverlays()
        {
            horizonsAlreadyCalculatedToolStripMenuItem.Checked = false;
            showShadowCalculationQueueToolStripMenuItem.Checked = false;
        }

        private void horizonsAlreadyCalculatedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (horizonsAlreadyCalculatedToolStripMenuItem.Checked)
            {
                horizonsAlreadyCalculatedToolStripMenuItem.Checked = false;
                MapView.FarPatches = new List<TerrainPatch>();
                MapView.Invalidate();
                return;
            }
            UncheckViewOverlays();
            horizonsAlreadyCalculatedToolStripMenuItem.Checked = true;
            MapView.FarPatches = LoadPatchShells(LoadPatchFilenames()).ToList();
            MapView.Invalidate();
        }

        private void showShadowCalculationQueueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showShadowCalculationQueueToolStripMenuItem.Checked)
            {
                showShadowCalculationQueueToolStripMenuItem.Checked = false;
                MapView.FarPatches = new List<TerrainPatch>();
                MapView.Invalidate();
                return;
            }
            UncheckViewOverlays();
            showShadowCalculationQueueToolStripMenuItem.Checked = true;
            MapView.FarPatches = Processor.ReadShadowCalculationQueue();
            MapView.Invalidate();
            //MapView.SelectedPatchesChanged = () => Processor.WriteShadowCalculationQueue(MapView.SelectedPatches);
        }

        public IEnumerable<string> LoadPatchFilenames()
        {
            var filenames = (new DirectoryInfo(HorizonsRoot)).EnumerateFiles("*.bin").Select(fi => fi.Name).OrderBy(f => f).ToList();
            filenames = filenames.Where(f => !f.Contains("center")).ToList();  // Remove centers
            filenames = filenames.GroupBy(f => f.Substring(0, 11)).Select(group => group.First()).ToList();  // Take one of com, cpu or gpu in that order, for each tile
            filenames = filenames.Select(f => Path.Combine(HorizonsRoot, f)).ToList();
            return filenames;
        }
        public IEnumerable<TerrainPatch> LoadPatchShells(IEnumerable<string> filenames) => filenames.Select(TerrainPatch.ReadShell).Where(p => p != null);

        public IEnumerable<TerrainPatch> LoadPatches(IEnumerable<TerrainPatch> patches)
        {
            var result = new List<TerrainPatch>();
            foreach (var p in patches)
            {
                var path = p.DefaultPath;
                if (!File.Exists(path))
                    continue;
                p.Load(path);
                result.Add(p);
            }
            return result;
        }

        public IEnumerable<TerrainPatch> LoadPatches(IEnumerable<Point> ids) => ids.Select(id => GetPatch(id)).Where(p => p != null);

        public TerrainPatch GetPatch(Point id)
        {
            if (PatchCache.TryGetValue(id, out TerrainPatch patch))
                return patch;
            var patch2 = TerrainPatch.FromId(id);
            if (File.Exists(patch2.DefaultPath))
            {
                Console.WriteLine($"Reading patch {patch2.DefaultPath}");
                var patch3 = TerrainPatch.ReadFrom(patch2.DefaultPath);
                PatchCache[id] = patch3;
                return patch3;
            }
            return null;
        }

        public TerrainPatch GetPatch(TerrainPatch patch)
        {
            if (PatchCache.TryGetValue(patch.Id, out TerrainPatch patch2))
                return patch2;
            if (patch.Horizons != null && patch.Horizons[0][0].PartialPoints().Count()>0)
            {
                PatchCache[patch.Id] = patch;
                return patch;
            }
            if (File.Exists(patch.DefaultPath))
            {
                Console.WriteLine($"Reading patch {patch.DefaultPath}");
                var patch3 = TerrainPatch.ReadFrom(patch.DefaultPath);
                PatchCache[patch3.Id] = patch3;
                return patch3;
            }
            return null;
        }

        private void LoadHorizonsAlreadyChecked(bool loadHorizons = false)
        {
            var patches = LoadPatchShells(LoadPatchFilenames());
            if (loadHorizons)
                MapView.FinishedPatches = LoadPatches(patches).ToList();
            MapView.Invalidate();
        }

        #endregion

        #region Process Shadow Queue

        private void btnProcessShadowQueue_Click(object sender, EventArgs e)
        {
            var spread_array = new int[] { 0, 1, 2, 3, 4, 5, 10, 20, 30, 40, 50, -1 };
            if (cbSurroundings.SelectedIndex < 0) cbSurroundings.SelectedIndex = 0;
            var spread = spread_array[cbSurroundings.SelectedIndex];
           // QueueProcessor.ObserverHeight = GetObserverHeight();
            Task.Run(() => Processor.RunQueue(null, q => Processor.WriteShadowCalculationQueue(q), spread, filterTentpolesToolStripMenuItem.Checked, false, cbCenter.Checked));
        }

        public void RunQueue(Point id, Action callback, bool writeHorizons = false) =>
             Task.Run(
                    () =>
                    {
                        Processor.RunQueue(new List<TerrainPatch> { TerrainPatch.FromId(id) }, null, writeHorizons: writeHorizons);
                        callback();
                    });

        public void RunQueue(List<Point> ids, Action<List<Rectangle>> callback, bool writeHorizons = false) =>
            Task.Run(() =>
                {
                    var processor = Processor as GPUProcessor;
                    var results = new List<Rectangle>();
                    foreach (var id in ids)
                    {
                        processor.RunQueue(new List<TerrainPatch> { TerrainPatch.FromId(id) }, null, writeHorizons: writeHorizons);
                        Debug.Assert(processor.BoundingBox.HasValue);
                        results.Add(processor.BoundingBox.Value);
                    }
                    callback(results);
                });

        #endregion

        #region Time Panel

        private void dtStartTime_ValueChanged(object sender, EventArgs e)
        {
            UpdateCurrentTime();
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

        private void tbSelectTime_Scroll(object sender, EventArgs e)
        {
            if (cbUpdateContinuously.Checked)
                UpdateCurrentTime();
        }

        private void tbSelectTime_MouseUp(object sender, MouseEventArgs e)
        {
            if (!cbUpdateContinuously.Checked)
                UpdateCurrentTime();
        }

        public delegate void ChangedTimeHandler(DateTime time);
        public event ChangedTimeHandler TimeChanged;

        protected void UpdateCurrentTime()
        {
            _currentTime = new DateTime(dtStartTime.Value.Ticks + (long)(_timeSpan.Ticks * (tbSelectTime.Value / (float)tbSelectTime.Maximum)));
            lbCurrentTime.Text = _currentTime.ToString();
            UpdateShadowCasters();
            TimeChanged?.Invoke(_currentTime);
        }

        #endregion

        #region GPU Testing

        private void btnGPU1_Click(object sender, EventArgs e)
        {
            var g = new gpu.GPUTest();
            g.Test1();
        }

        private void btnGPU2_Click(object sender, EventArgs e)
        {
            var a = new TerrainPatch { Line = 800, Sample = 1300, Width = 128, Height = 128 };
            //var a = new TerrainPatch { Line = 17700, Sample = 13220, Width = 128, Height = 128 };
            a.FillPointsAndMatrices(Terrain);
            var o = a.RelativeTo(0, 1);
            var relativeHeights = Terrain.GenerateHeightField(o);
            var g = new gpu.GPUTest();
            float[] horizon = g.Test2(a, o, relativeHeights);
            a.LoadHorizons(horizon);
            _patch = a;
            DrawHorizon(a);
        }

        private void btnGPU3_Click(object sender, EventArgs e)
        {
            var a = new TerrainPatch { Line = 17700, Sample = 13220, Width = 128, Height = 128 };
            a.FillPointsAndMatrices(Terrain);
            var o = a.RelativeTo(0, 1);
            var relativeHeights = Terrain.GenerateHeightField(o);
            var g = new gpu.GPUTest();
            float[] horizon = g.Test3(a, o, relativeHeights);
            var h = new Horizon();
            h.Load(horizon);
        }

        private void btnGPU4_Click(object sender, EventArgs e)
        {
            var processor = new GPUProcessor { Terrain = Terrain };
            var a = new TerrainPatch { Line = 17700, Sample = 13220, Width = 128, Height = 128 };
            a.FillPointsAndMatrices(Terrain);
            var o = new List<TerrainPatch>() { a.RelativeTo(0, 1) };
            processor.UpdateHorizons(a, o);
        }

        #endregion

        private void renderIceStabilityDepthTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new graphics.RenderIceStabilityDepthTest()).Test1();
        }

        #region spice

        private void UpdateShadowCasters()
        {
            MapView.SunVector = SunPosition(_currentTime);
            MapView.EarthVector = EarthPosition(_currentTime);
            CasterWindow?.UpdateShadowCaster(MapView.SunVector);
            // Console.WriteLine($"ShadowCaster={MapView.ShadowCaster.Normalized()}");
            MapView.Invalidate();
        }

        public static DateTime Epoch = new DateTime(2000, 1, 1, 11, 58, 55, 816);
        public const int EarthId = 399;
        public const int MoonId = 301;
        public const int SunId = 10;

        public static Vector3d SunPosition(DateTime time)
        {
            var et = DateTimeToET(time);
            var state = new double[6];
            double lt = 0d;
            CSpice.spkgeo_c(SunId, et, "MOON_ME", MoonId, state, ref lt);
            return new Vector3d(state[0], state[1], state[2]);
        }

        public unsafe static Vector3d EarthPosition(DateTime time)
        {
            var et = DateTimeToET(time);
            var state = new double[6];
            double lt = 0d;
            CSpice.spkgeo_c(EarthId, et, "MOON_ME", MoonId, state, ref lt);
            return new Vector3d(state[0], state[1], state[2]);
        }

        // The 3D accounts for leap seconds since 2000.  This is valid only for dates after Jul 1 2012.
        public static double DateTimeToET(DateTime time) => (time - Epoch).TotalSeconds + 3d;

        #endregion

        private void runVerboseComparisonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunVerboseComparisonToolStripMenuItem_Click(Terrain);
        }
        public static void RunVerboseComparisonToolStripMenuItem_Click(InMemoryTerrainManager Terrain)
        {
            var a = new TerrainPatch { Line = 800, Sample = 1300, Width = 128, Height = 128 };
            //var b = a.RelativeTo(0, 1);

            a.FillPointsAndMatrices(Terrain);
            //b.FillPoints(Terrain);

            Console.WriteLine(@"Some points:");
            for (var y = 0; y < 1; y++)
                for (var x = 0; x < 5; x++)
                {
                    Console.WriteLine(a.Points[y][x]);
                }
            Console.WriteLine(@"--------------------");
            Console.WriteLine(@"Some matrices:");
            for (var y = 0; y < 1; y++)
                for (var x = 0; x < 5; x++)
                {
                    Console.WriteLine(@"---");
                    Console.WriteLine(a.Matrices[y][x]);
                }


            /*
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            a.UpdateHorizon(b);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
            _patch = a;
            DrawHorizon(a);
            */
        }

        private void fillMatricesOfCalculatedPatchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var p in MapView.FinishedPatches)
                p.FillMatrices(Terrain);
        }

        private void dWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<TerrainPatch> patches;
            if (false)
            {
                var patch = MapView.SelectedPatches.FirstOrDefault();
                if (patch == null) return;
                var patch2 = new TerrainPatch { Line = patch.Line, Sample = patch.Sample, Height = 5, Width = 5 };
                patches = new List<TerrainPatch> { patch2 };

                /*
                var filenames = new string[] {
                "patch_17664_13056.patch",
                "patch_17664_13184.patch",
                "patch_17664_13312.patch",
                "patch_17664_13440.patch",
                "patch_17792_13184.patch", };

                patches = filenames.Select(fn => TerrainPatch.ReadFrom(Path.Combine(@"C:\RP\tiles\np\horizons", fn))).ToList();
                */
            }
            else
            {
                patches = MapView.SelectedPatches;
                foreach (var p in patches)
                    if (p.Horizons == null && File.Exists(p.DefaultPath))
                        p.Load(p.DefaultPath);
            }

            var w = new viz.TerrainVizWindow { Patches = patches, Terrain = Terrain };
            w.Show();
        }

        #region Mouse Modes

        private void btnIdleMouseMode_Click(object sender, EventArgs e)
        {
            MapView.ReplaceMouseMode(new mouse.MapIdleMode());
        }

        private void btnPaintMouseMode_Click(object sender, EventArgs e)
        {
            MapView.ReplaceMouseMode(new mouse.PaintSelectionMode());
        }

        private void btnEraseMouseMode_Click(object sender, EventArgs e)
        {
            MapView.ReplaceMouseMode(new mouse.RemoveSelectionMode());
        }

        #endregion

        #region Testing

        private void tbRenderTestDateDelta_ValueChanged(object sender, EventArgs e)
        {
        }

        private void tbRenderTestAzimuth_ValueChanged(object sender, EventArgs e)
        {
            RenderTestUpdate(tbRenderTestAzimuth.Value / 10f, tbRenderTestElevation.Value / 10f);
        }

        private void tbRenderTestElevation_ValueChanged(object sender, EventArgs e)
        {
            RenderTestUpdate(tbRenderTestAzimuth.Value / 10f, tbRenderTestElevation.Value / 10f);
        }

        private void tbRenderTestDateDelta_Scroll(object sender, EventArgs e)
        {

        }

        private void tbRenderTestAzimuth_Scroll(object sender, EventArgs e)
        {

        }

        private void tbRenderTestElevation_Scroll(object sender, EventArgs e)
        {

        }

        private void RenderTestUpdate(float azimuth_deg, float elevation_deg)
        {
            _renderTestAzimuth = azimuth_deg;
            _renderTestElevation = elevation_deg;
            if (_renderTestPatch == null) return;
            _renderTestPatch.RenderShadows((Bitmap)pbTest1.Image, azimuth_deg, elevation_deg);
            pbTest1.Invalidate();
            Console.WriteLine(@"update");
        }

        private void loadRenderTestPatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _renderTestPatch = TerrainPatch.ReadFrom(@"C:\RP\tiles\np\horizons\patch_17664_13056.patch");
            _patch = _renderTestPatch;
            if (pbTest1.Image == null)
            {
                var bmp = new Bitmap(128, 128, PixelFormat.Format8bppIndexed);
                var p = bmp.Palette;
                for (var i = 0; i < 256; i++)
                    p.Entries[i] = Color.FromArgb(i, i, i);
                bmp.Palette = p;
                pbTest1.Image = bmp;
            }
        }

        private void testRenderToTextureToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void btnCasterRenderPoint_Click(object sender, EventArgs e)
        {
            MapView.ReplaceMouseMode(new mouse.DragRenderPoint());
        }

        private void btnShowShadowcasters_Click(object sender, EventArgs e)
        {
            MapView.ReplaceMouseMode(new mouse.HilightShadowCasters());
        }

        private void generateTestShadowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // This generates shadows for two tiles
            var p1 = new QueueProcessor { MaxSpread = 75, Terrain = Terrain, MapView = MapView };
            var q1 = new List<TerrainPatch> { new TerrainPatch { Line = 17664, Sample = 13184, Width = 128, Height = 128, Step = 1 } };
            var q2 = new List<TerrainPatch> { new TerrainPatch { Line = 17792, Sample = 13184, Width = 128, Height = 128, Step = 1 } };
            p1.RunQueue(q1, spread: 4, overHorizonCheck: false);
            p1.RunQueue(q2, spread: 3, overHorizonCheck: false);
            Console.WriteLine("Finished generating two patches");
        }

        private void tileTesterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var w = new view.TileTester(MapView.SelectedPatches);
            w.Show();
        }

        private void shadowcasterRenderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CasterWindow == null)
            {
                CasterWindow = new ShadowCasterWindow();
            }
            CasterWindow.Visible = true;
        }

        private void andyComparisonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CompareToAndy == null)
                CompareToAndy = new CompareToAndy { Terrain = Terrain };
            CompareToAndy.Visible = true;
        }

        public void SetShadowCasterRenderPatch(TerrainPatch p)
        {
            if (CasterWindow == null)
            {
                CasterWindow = new ShadowCasterWindow();
                CasterWindow.Show();
            }
            CasterWindow.SetPatch(p);
        }

        public void SetShadowCasterRenderPoint(Point p)
        {
            if (CasterWindow == null)
                CasterWindow = new ShadowCasterWindow();
            CasterWindow.SetPoint(p);
        }

        private void firstTestTileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var filenames = (new DirectoryInfo(HorizonsRoot)).EnumerateFiles().Select(fi => fi.FullName).ToList();
            if (filenames.Count < 1) return;
            MapView.FinishedPatches = new List<TerrainPatch> { TerrainPatch.ReadShell(filenames[0]) };
            foreach (var p in MapView.FinishedPatches)
                p.Load();
            MapView.Invalidate();
            MapView.DrawPatchShadows = true;
        }

        private void sundayTest1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // I'm currently seeing incompatible shadows in two ajacent patches.  One theory is that it's the IsOverHorizon test that's
            // wrong.  I'm trying to test this be making sure these patches are accounting for the same shadowcasters.
            // This didn't work.  Still looking
            var updateQueue = new List<Tuple<TerrainPatch, TerrainPatch>>();
            var cache = new Dictionary<Point, TerrainPatch>();
            var patchez = MapView.FinishedPatches.Select(a => a).ToList();
            var shadowCasterCounts = patchez.Select(p => p.ShadowCasters.Count).ToList();
            foreach (var a in patchez)
                foreach (var b in patchez)
                    if (a != b)
                    {
                        // make sure i has all of j's shadowcasters
                        foreach (var pt in b.ShadowCasters.Where(bpt => !a.ShadowCasters.Contains(bpt)))
                        {
                            if (!cache.TryGetValue(pt, out TerrainPatch other))
                            {
                                other = new TerrainPatch { Line = pt.Y * TerrainPatch.DefaultSize, Sample = pt.X * TerrainPatch.DefaultSize, Width = TerrainPatch.DefaultSize, Height = TerrainPatch.DefaultSize, Step = 1 };
                                cache[pt] = other;
                                other.FillPoints(Terrain);
                            }
                            updateQueue.Add(new Tuple<TerrainPatch, TerrainPatch>(a, other));
                        }
                    }
            Console.WriteLine($"There are {updateQueue.Count} updates to perform.");
            var options = new ParallelOptions { MaxDegreeOfParallelism = MaxDegreeOfParallelism };
            Parallel.ForEach(updateQueue, options, pair => pair.Item1.UpdateHorizon(pair.Item2));
            for (var i = 0; i < patchez.Count; i++)
                if (patchez[i].ShadowCasters.Count != shadowCasterCounts[i])
                    patchez[i].Write();
        }

        private void sundayTest2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // I'm currently seeing incompatible shadows in two ajacent patches.  One theory is that it's the IsOverHorizon test that's
            // wrong.  I'm trying to test this be making sure these patches are accounting for the same shadowcasters.
            var updateQueue = new List<Tuple<TerrainPatch, TerrainPatch>>();
            var cache = new Dictionary<Point, TerrainPatch>();
            var patchez = MapView.FinishedPatches.Select(a => a).ToList();
            var shadowCasterCounts = patchez.Select(p => p.ShadowCasters.Count).ToList();
            var span = 15;
            foreach (var a in patchez)
                for (var i = 1; i <= span; i++)
                    foreach (var pt in a.SurroundingPatches(i).Select(p => p.Id).Where(p => !a.ShadowCasters.Contains(p)))
                    {
                        if (!cache.TryGetValue(pt, out TerrainPatch other))
                        {
                            other = new TerrainPatch { Line = pt.Y * TerrainPatch.DefaultSize, Sample = pt.X * TerrainPatch.DefaultSize, Width = TerrainPatch.DefaultSize, Height = TerrainPatch.DefaultSize, Step = 1 };
                            cache[pt] = other;
                            other.FillPoints(Terrain);
                        }
                        updateQueue.Add(new Tuple<TerrainPatch, TerrainPatch>(a, other));
                    }
            Console.WriteLine($"There are {updateQueue.Count} updates to perform.");
            var options = new ParallelOptions { MaxDegreeOfParallelism = MaxDegreeOfParallelism };
            Parallel.ForEach(updateQueue, options, pair => pair.Item1.UpdateHorizon(pair.Item2));
            for (var i = 0; i < patchez.Count; i++)
                if (patchez[i].ShadowCasters.Count != shadowCasterCounts[i])
                    patchez[i].Write();
        }

        private void findMinmaxInDEMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"min={Terrain.Min}");
            Console.WriteLine($"min={Terrain.Max}");
        }

        private void extendExistingPatchesTo177ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task.Run(() => Processor.ExtendExistingPatches());
        }

        void UncheckMouseModeMenuItems()
        {
            var lst = mouseToolStripMenuItem.DropDown.Items;
            for (var i = 0; i < lst.Count; i++)
            {
                var v = lst[i] as ToolStripMenuItem;
                if (v != null)
                    v.Checked = false;
            }
        }

        private void idleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckMouseModeMenuItems();
            idleToolStripMenuItem.Checked = true;
            MapView.ReplaceMouseMode(new mouse.MapIdleMode());
        }

        private void paintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckMouseModeMenuItems();
            paintToolStripMenuItem.Checked = true;
            MapView.ReplaceMouseMode(new mouse.PaintSelectionMode());
            CheckSelectedPatchesLayer();
        }

        private void CheckSelectedPatchesLayer()
        {
            var layer = FindLayer("Selected Patches");
            if (layer != null) layer.Checked = true;
            MapView.Invalidate();
        }

        private void eraseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckMouseModeMenuItems();
            eraseToolStripMenuItem.Checked = true;
            MapView.ReplaceMouseMode(new mouse.RemoveSelectionMode());
            CheckSelectedPatchesLayer();
        }

        private void renderPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckMouseModeMenuItems();
            renderPointToolStripMenuItem.Checked = true;
            MapView.ReplaceMouseMode(new mouse.DragRenderPoint());
        }

        private void highlightShadowcastersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckMouseModeMenuItems();
            highlightShadowcastersToolStripMenuItem.Checked = true;
            MapView.ReplaceMouseMode(new mouse.HilightShadowCasters());
        }

        private void highlightSurroundingtestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckMouseModeMenuItems();
            highlightSurroundingtestToolStripMenuItem.Checked = true;
            MapView.ReplaceMouseMode(new mouse.HilightSurroundingPatches());
        }

        private void cPUVsGPUTest1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var target = TerrainPatch.FromId(102, 138);
            var cpu_caster = TerrainPatch.FromId(103, 138);
            // not finished
            // This was going to calculate a specific offset using the cpu and gpu and compare them
            // I'm not doing that because I found the problem with the earlier comparison approach, and the comparisons look reasonable
        }

        private void processQueueNearHorizonOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task.Run(() => Processor.RunQueue(null, q => Processor.WriteShadowCalculationQueue(q), overHorizonCheck: filterTentpolesToolStripMenuItem.Checked, centerOnly: true));
        }

        private void gPUTestSundayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var id = new Point(100, 129);
            var index = new Point(29, 93);
            var patch = TerrainPatch.FromId(100, 129);
            var processor = new GPUProcessor { Terrain = Terrain };

            //var spreads = new int[] { 0, 2 };
            var spreads = new int[] { 0 };
            var colors = new Color[] { Color.Black, Color.Blue, Color.Red, Color.Green };

            var horizons = spreads.Select(spread =>
            {
                patch.Horizons = null;
                processor.RunQueue(new List<TerrainPatch> { patch }, null, spread, true, false, true, false, false);
                return patch.Horizons;
            }).ToList();

            var plotWindow = new PlotWindow();
            plotWindow.Show();
            for (var i = 0; i < spreads.Length; i++)
                plotWindow.Plot(horizons[i][index.Y][index.X], colors[Math.Min(colors.Length - 1, i)]);

            if (true)
            {
                TerrainPatch.MaximumLocalDistance = 2000;
                patch.Horizons = null;
                processor.RunQueue(new List<TerrainPatch> { patch }, null, 0, true, false, true, false, false);
                plotWindow.Plot(patch.Horizons[index.Y][index.X], Color.Orange);
            }
        }

        private void useGPUProcessorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Processor = new GPUProcessor { Terrain = Terrain, MapView = MapView };
            useGPUProcessorToolStripMenuItem.Checked = true;
            useCPUProcessorToolStripMenuItem.Checked = false;
        }

        private void useCPUProcessorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Processor = new QueueProcessor { Terrain = Terrain, MapView = MapView };
            useGPUProcessorToolStripMenuItem.Checked = false;
            useCPUProcessorToolStripMenuItem.Checked = true;
        }

        private void gPUTestMondayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WriteTestCrossSections();
        }

        void WriteTestCrossSections()
        {
            var id = new Point(100, 129);
            var index = new Point(29, 93);
            var target = TerrainPatch.FromId(100, 129);

            using (var sw2 = new StreamWriter("diff.txt"))
            {

                var baseline_list = new List<float>();
                var gpu_list = new List<float>();

                //var angle_index = 1;
                //var angle_index = 2160;
                for (var angle_index = 0; angle_index < 2880; angle_index++)
                {
                    baseline_list = new List<float>();
                    gpu_list = new List<float>();

                    // Generate baseline
                    {
                        target.Points = null;
                        target.Matrices = null;  // Be sure!!
                        target.Matrices = null;  // Be sure!!
                        target.FillPointsAndMatrices(Terrain);

                        var mat = target.Matrices[index.Y][index.X];
                        var center_sample = target.Sample + index.X;
                        var center_line = target.Line + index.Y;

                        var ray_angle = 3.141592653589f * 2f * angle_index / (TerrainPatch.NearHorizonOversample * Horizon.HorizonSamples);
                        var ray_cos = (float)Math.Cos(ray_angle);
                        var ray_sin = (float)Math.Sin(ray_angle);

                        const float d_min = 1f;
                        var d_max = (float)TerrainPatch.MaximumLocalDistance;
                        const float d_step = TerrainPatch.LocalStep;

                        for (var d = d_min; d <= d_max; d += d_step)
                        {
                            var caster_line = center_line + ray_sin * d;
                            var caster_sample = center_sample + ray_cos * d;
                            var a = Terrain.GetPointInME(caster_line, caster_sample);
                            var b = Vector3d.Transform(a, mat);
                            var alen = Math.Sqrt(b.X * b.X + b.Y * b.Y);
                            var angle = (float)Math.Atan2(b.Z, alen);
                            baseline_list.Add(angle);
                        }
                    }

                    // Generate comparizon
                    {
                        var processor = new GPUProcessor { Terrain = Terrain, MapView = MapView };
                        target.Points = null;
                        target.Matrices = null;  // Be sure!!
                        target.FillPoints(Terrain);
                        target.FillMatricesRelativeToPoint(Terrain, target.Points[0][0]);
                        var cpu_matrices_size = target.Height * target.Width * 12;
                        var basePoint = target.Points[0][0];
                        var cpu_matrices = GPUProcessor.MakeCPUMatrices(target);

                        // Caster array
                        const int dem_size = TerrainPatch.DEM_size;
                        const int patch_size = TerrainPatch.DefaultSize;
                        var maxDistance = (float)TerrainPatch.MaximumLocalDistance;
                        var border = 2 + (int)Math.Ceiling(maxDistance); // the 2 is margin for the bilinear interpolation

                        var line_min = Math.Max(0, target.Line - border);
                        var line_max = Math.Min(dem_size - 1, target.Line + patch_size + border);  // 1 more than the highest index
                        var line_size = line_max - line_min;
                        var line_offset = target.Line - line_min;

                        var sample_min = Math.Max(0, target.Sample - border);
                        var sample_max = Math.Min(dem_size - 1, target.Sample + patch_size + border);  // 1 more than the highest index
                        var sample_size = sample_max - sample_min;
                        var sample_offset = target.Sample - sample_min;

                        var cpu_caster_points_size = line_size * sample_size * 3;
                        var cpu_caster_points = new float[cpu_caster_points_size];
                        processor.FillNearCasterArray(cpu_caster_points, target.Points[0][0], line_min, line_max, sample_min, sample_max);

                        var ray_angle = 3.141592653589f * 2f * angle_index / (TerrainPatch.NearHorizonOversample * Horizon.HorizonSamples);
                        var ray_cos = (float)Math.Cos(ray_angle);
                        var ray_sin = (float)Math.Sin(ray_angle);

                        var target_line = index.Y;
                        var target_sample = index.X;
                        var center_sample = sample_offset + index.X;
                        var center_line = line_offset + index.Y;

                        var matrices = cpu_matrices;
                        var pos = (target_line * TerrainPatch.DefaultSize + target_sample) * 12;

                        var points_columns = sample_size;
                        var points_rows = line_size;

                        var row0x = matrices[pos++];
                        var row1x = matrices[pos++];
                        var row2x = matrices[pos++];
                        var row3x = matrices[pos++];

                        var row0y = matrices[pos++];
                        var row1y = matrices[pos++];
                        var row2y = matrices[pos++];
                        var row3y = matrices[pos++];

                        var row0z = matrices[pos++];
                        var row1z = matrices[pos++];
                        var row2z = matrices[pos++];
                        var row3z = matrices[pos];

                        const float d_min = 1f;
                        var d_max = (float)TerrainPatch.MaximumLocalDistance;
                        const float d_step = TerrainPatch.LocalStep;

                        var points = cpu_caster_points;

                        for (var d = d_min; d <= d_max; d += d_step)
                        {
                            // Generate the location of the caster point in the points array
                            var caster_line = center_line + ray_sin * d;
                            var caster_sample = center_sample + ray_cos * d;

                            var x1 = (int)caster_sample;
                            var y1 = (int)caster_line;
                            int x2 = x1 + 1;
                            int y2 = y1 + 1;

                            var q11_offset = 3 * (y1 * points_columns + x1);  // (y1, x1);
                            var q11 = new Vector3(points[q11_offset], points[q11_offset + 1], points[q11_offset + 2]);

                            var q12_offset = 3 * (y2 * points_columns + x1);  // (y2, x1);
                            var q12 = new Vector3(points[q12_offset], points[q12_offset + 1], points[q12_offset + 2]);

                            // First interpolation across rows (line)
                            var q1_line = q11 + (caster_line - y1) * (q12 - q11);

                            var q21_offset = 3 * (y1 * points_columns + x2);  // (y1, x2);
                            var q21 = new Vector3(points[q21_offset], points[q21_offset + 1], points[q21_offset + 2]);

                            var q22_offset = 3 * (y2 * points_columns + x2);  // (y2, x2);
                            var q22 = new Vector3(points[q22_offset], points[q22_offset + 1], points[q22_offset + 2]);

                            // Second interpolation across rows
                            var q2_line = q21 + (caster_line - y1) * (q22 - q21);

                            // Interpolate across samples
                            var caster = q1_line + (caster_sample - x1) * (q2_line - q1_line);

                            // Break out the coordinates
                            var x_patch = caster.X;
                            var y_patch = caster.Y;
                            var z_patch = caster.Z;

                            // Transform the point to the local frame
                            var x = x_patch * row0x + y_patch * row1x + z_patch * row2x + row3x;
                            var y = x_patch * row0y + y_patch * row1y + z_patch * row2y + row3y;
                            var z = x_patch * row0z + y_patch * row1z + z_patch * row2z + row3z;

                            var alen = Math.Sqrt(x * x + y * y);
                            var angle = (float)Math.Atan2(z, alen);
                            gpu_list.Add(angle);
                        }
                    }

                    var diff = baseline_list.Zip(gpu_list, (a, b) => a - b).ToList();
                    var maxdiff = diff.Max();
                    Console.WriteLine($"angle_index={angle_index} maxdiff={maxdiff}");
                    sw2.Write($",{maxdiff}");
                }

                if (false)
                    using (var sw = new StreamWriter("comparison.txt"))
                    {
                        sw.Write("baseline={");
                        for (var i = 0; i < baseline_list.Count; i++)
                            sw.Write(i == 0 ? baseline_list[i].ToString() : $",{baseline_list[i]}");
                        sw.WriteLine("}");
                        sw.Write("gpu={");
                        for (var i = 0; i < gpu_list.Count; i++)
                            sw.Write(i == 0 ? gpu_list[i].ToString() : $",{gpu_list[i]}");
                        sw.WriteLine("}");
                    }

            }
        }

        private void meshTesterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new MeshTester { Terrain = Terrain }).Show();
        }

        private void meshCreationWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_meshGeneratorWindow == null)
                _meshGeneratorWindow = new MeshGeneratorWindow { Terrain = Terrain, MainWindow = this };
            _meshGeneratorWindow.Visible = true;
        }

        private void loadTraverseLatLonSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = new OpenFileDialog { FilterIndex = 0, Filter = "Traverse summary files (*.latlon)|*.latlon|All files (*.*)|*.*" };
            if (d.ShowDialog() != DialogResult.OK)
                return;
            var summary = TraversePlanner.planning.LatLonTraverseSummary.Load(d.FileName);
            summary.LineSampleList = summary.LatLonList.Select(ll =>
            {
                InMemoryTerrainManager.GetLineSampleD(ll.Latitude * Math.PI / 180d, ll.Longitude * Math.PI / 180d, out double line, out double sample);
                return new PointF((float)sample, (float)line);
            }).ToList();
            MapView.TraverseSummary = summary;
            MapView.Invalidate();
        }

        private void writeQueuefromSelectedPatchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Processor.WriteShadowCalculationQueue(MapView.SelectedPatches);
        }

        private void loadQueuetoSelectedPatchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapView.SelectedPatches = Processor.ReadShadowCalculationQueue();
            MapView.Invalidate();
        }

        private void cbProcessor_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbProcessor.SelectedIndex)
            {
                case 0:
                    Processor = new QueueProcessor { Terrain = Terrain, MapView = MapView };
                    TerrainPatch.GeneratorPostfix = "cpu";
                    break;
                case 1:
                    Processor = new GPUProcessor { Terrain = Terrain, MapView = MapView };
                    TerrainPatch.GeneratorPostfix = "gpu";
                    break;
                case 2:
                    Processor = new CombinedProcessor { Terrain = Terrain, MapView = MapView };
                    TerrainPatch.GeneratorPostfix = "com";
                    break;
            }

        }

        private void aToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new PatchCompare()).Show();
        }

        private void comparePatchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cpu = TerrainPatch.ReadFrom(@"C:\RP\tiles\np\horizons\16640_13440_cpu.bin");
            var gpu = TerrainPatch.ReadFrom(@"C:\RP\tiles\np\horizons\16640_13440_gpu.bin");

            Console.WriteLine($"equal={cpu.Line == gpu.Line} cpu.Line={cpu.Line} gpu.Line={gpu.Line}");
            Console.WriteLine($"equal={cpu.Sample == gpu.Sample} cpu.Sample={cpu.Sample} gpu.Sample={gpu.Sample}");
            Console.WriteLine($"equal={cpu.Width == gpu.Width} cpu.Width={cpu.Width} gpu.Width={gpu.Width}");
            Console.WriteLine($"equal={cpu.Height == gpu.Height} cpu.Height={cpu.Height} gpu.Height={gpu.Height}");
            Console.WriteLine($"equal={cpu.Step == gpu.Step} cpu.Step={cpu.Step} gpu.Step={gpu.Step}");

            for (var line = 0; line < 128; line++)
                for (var sample = 0; sample < 128; sample++)
                {
                    var ha = cpu.Horizons[line][sample];
                    var hb = gpu.Horizons[line][sample];
                    if (ha == null || hb == null)
                    {
                        Console.WriteLine($"[{line},{sample}] at least one of horizons is null");
                        continue;
                    }
                    if (ha.IsDegrees != hb.IsDegrees)
                    {
                        Console.WriteLine($"[{line},{sample}] IsDegrees mismatch");
                        continue;
                    }
                    var fa = ha.Buffer;
                    var fb = hb.Buffer;
                    if (fa.Length != fb.Length)
                    {
                        Console.WriteLine($"[{line},{sample}] buffer length mismatch");
                        continue;
                    }
                    for (var i = 0; i < fa.Length; i++)
                        if (Math.Abs(fa[i] - fb[i]) > 0.01f)
                        {
                            Console.WriteLine($"[{line},{sample}] Different value i={i} cpu={fa[i]} gpu={fb[i]} delta={Math.Abs(fa[i] - fb[i])}");
                            //goto escape_loop;
                        }
                    //escape_loop:
                    //continue;
                }
        }

        private void lightCurveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LightCurveForm.Show();
            MapView.ReplaceMouseMode(new mouse.LightCurveMode { LightCurveForm = LightCurveForm });
        }

        private void loadShadowsForPaintedTilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapView.FinishedPatches = MapView.SelectedPatches.Select(p => GetPatch(p)).Where(p => p != null).ToList();
            MapView.Invalidate();
        }

        private void tbTransparency_ValueChanged(object sender, EventArgs e)
        {
            if (_trackbarIsUpdating)
                return;
            if (lvLayers.SelectedIndices.Count != 1)
                return;
            var lt = GetSelectedLayer();
            if (lt != null)
            {
                lt.Transparency = tbTransparency.Value / 100f;
                var index = lvLayers.SelectedIndices[0];
                lvLayers.Items[index].SubItems[1].Text = tbTransparency.Value.ToString("00");
                MapView.Invalidate();
            }
        }

        private void UpdateLayerTransparencyText()
        {
            for (var i = 0; i < lvLayers.Items.Count; i++)
            {
                var item = lvLayers.Items[i];
                if (!(item.Tag is MapLayer layer)) continue;
                item.SubItems[1].Text = (layer.Transparency * 100).ToString("00");
            }
        }

        #endregion

        #region Handle Layers

        protected ListViewItem GetItem(int index) => index < 0 || index >= lvLayers.Items.Count ? null : lvLayers.Items[index];
        protected ListViewItem GetItem(string name)
        {
            foreach (var item in lvLayers.Items)
                if (name.Equals(((item as ListViewItem).Tag as MapLayer)?.Name))
                    return (item as ListViewItem);
            return null;
        }

        /// <summary>
        /// Return the ListViewItems bottom to top (draw order)
        /// </summary>
        /// <returns></returns>
        public List<ListViewItem> GetItems()
        {
            var result = new List<ListViewItem>();
            var items = lvLayers.Items;
            for (var i = items.Count - 1; i >= 0; i--)
                result.Add(items[i]);
            return result;
        }

        public List<ListViewItem> GetCheckedItems()
        {
            var result = new List<ListViewItem>();
            var items = lvLayers.Items;
            for (var i = items.Count - 1; i >= 0; i--)
                if (items[i].Checked)
                    result.Add(items[i]);
            return result;
        }

        public List<IMapLayer> GetAllLayers() => GetItems().Select(LayerFromItem).Where(l => l != null).ToList();

        public IMapLayer LayerFromItem(ListViewItem item) => item.Tag as IMapLayer;

        public void UpdateFromLayers(List<IMapLayer> layers)
        {
            _isUpdating = true;
            var currentItems = GetItems();
            var currentLayers = GetAllLayers();

            var layersToAdd = layers.Where(l => !currentLayers.Contains(l)).ToList();
            var layersToRemove = currentLayers.Where(l => !layers.Contains(l)).ToList();

            var items = lvLayers.Items;
            for (var i = items.Count - 1; i >= 0; i--)
                if (layersToRemove.Contains(LayerFromItem(items[i])))
                    items.RemoveAt(i);

            foreach (var toAdd in layersToAdd)
            {
                var subitems = new string[] { toAdd.Name, (toAdd.Transparency * 100f).ToString("00") };
                items.Add(new ListViewItem(subitems) { Name = toAdd.Name, Tag = toAdd });
            }

            if (!GetItems().Exists(item => item.Checked))
            {
                var first = GetItems().FirstOrDefault();
                if (first != null)
                    first.Checked = true;
            }

            _isUpdating = false;
            UpdateMapView();
        }

        public void InsertLayer(IMapLayer toAdd)
        {
            var subitems = new string[] { toAdd.Name, (toAdd.Transparency * 100f).ToString("00") };
            lvLayers.Items.Insert(0, new ListViewItem(subitems) { Name = toAdd.Name, Tag = toAdd });
            UpdateMapView();
        }

        private void lvLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idx = lvLayers.SelectedIndices.Count > 0 ? lvLayers.SelectedIndices[0] : -1;
            if (_isUpdating)
                return;
            if (lvLayers.SelectedIndices.Count < 1)
                return;
            _trackbarIsUpdating = true;
            var layer = LayerFromItem(lvLayers.SelectedItems[0]);
            var transparency = layer.Transparency;
            tbTransparency.Value = (int)(100 * transparency);
            UpdatePropertySheet(layer);
            _trackbarIsUpdating = false;
        }

        private void lvLayers_KeyDown(object sender, KeyEventArgs e)
        {
            if (lvLayers.SelectedIndices.Count < 1)
                return;
            var index = lvLayers.SelectedIndices[0];
            if (e.KeyCode == Keys.Up && index > 0)
            {
                var item1 = lvLayers.Items[index];
                var item2 = lvLayers.Items[index - 1];
                var text = item1.Text;
                var isChecked = item1.Checked;
                var tag = item1.Tag;
                item1.Text = item2.Text;
                item1.Checked = item2.Checked;
                item1.Tag = item2.Tag;
                item2.Text = text;
                item2.Checked = isChecked;
                item2.Tag = tag;
                lvLayers.SelectedIndices.Clear();
                lvLayers.SelectedIndices.Add(index - 1);
                lvLayers.Invalidate();
            }
            else if (e.KeyCode == Keys.Down && index < lvLayers.Items.Count - 1)
            {
                var tmp = lvLayers.Items[index];
                lvLayers.Items.RemoveAt(index);
                lvLayers.Items.Insert(index + 1, tmp);
                lvLayers.Items[index + 1].Selected = true;
                lvLayers.Invalidate();
            }
            UpdateMapView();
            var temp = lvLayers.SelectedIndices;
        }

        private ListViewItem FindLayer(string name)
        {
            for (var i = 0; i < lvLayers.Items.Count; i++)
            {
                var layer = lvLayers.Items[i].Tag as MapLayer;
                if (layer == null) continue;
                if (name.Equals(layer.Name, StringComparison.OrdinalIgnoreCase))
                    return lvLayers.Items[i];
            }
            return null;
        }

        private void lvLayers_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Tag is MapLayer layer)
                layer.Checked(MapView, e.Item.Checked);
            if (_isUpdating)
                return;
            UpdateMapView();
        }

        protected void UpdateMapView()
        {
            if (MapView == null) return;
            var layers = GetLayersToDraw();
            Console.WriteLine($"UpdateMapView layers={CommaSeparated(layers.Select(l => l.Name))}");
            MapView.Layers = layers;
        }

        public List<IMapLayer> GetLayersToDraw() => GetCheckedItems().Select(LayerFromItem).Where(l => l != null).ToList();
        public IMapLayer GetSelectedLayer() => lvLayers.SelectedItems.Count < 1 ? null : lvLayers.Items[lvLayers.SelectedIndices[0]].Tag as IMapLayer;

        private void lvLayers_SizeChanged(object sender, EventArgs e)
        {
            lvLayers.Columns[0].Width = lvLayers.Width - lvLayers.Columns[1].Width - 1;
        }

        protected string CommaSeparated(IEnumerable<string> items)
        {
            string r = null;
            foreach (var item in items)
            {
                if (r == null)
                    r = item;
                else
                    r = r + ", " + item;
            }
            return r;
        }

        private void UpdatePropertySheet(IMapLayer layer)
        {
            if (!_layerPropertySheets.TryGetValue(layer, out UserControl sheet))
            {
                sheet = layer.GetPropertySheet();
                if (sheet == null)
                {
                    foreach (var child in pnlPropertiesHolder.Controls)
                        if (child is UserControl c)
                            c.Visible = false;
                    return;
                }
                if (!(sheet is IMapLayerPropertySheet sheet1))
                {
                    MessageBox.Show($"layer returned an object that isn't an IMapLayerPropertySheet");
                    return;
                }
                _layerPropertySheets.Add(layer, sheet);
                sheet1.Layer = layer;
            }
            if (pnlPropertiesHolder != sheet.Parent)
                pnlPropertiesHolder.Controls.Add(sheet);
            foreach (var child in pnlPropertiesHolder.Controls)
                if (child is UserControl c)
                    c.Visible = c == sheet;                
        }

        #endregion

        private void controlsToolStripMenuItem_Click(object sender, EventArgs e) =>
            tabLeft.Visible = controlsToolStripMenuItem.Checked = !controlsToolStripMenuItem.Checked;

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e) =>
            tabRight.Visible = propertiesToolStripMenuItem.Checked = !propertiesToolStripMenuItem.Checked;

        private void timeControlToolStripMenuItem_Click(object sender, EventArgs e) =>
            pnlTime.Visible = timeControlToolStripMenuItem.Checked = !timeControlToolStripMenuItem.Checked;

        private void plotPanelToolStripMenuItem_Click(object sender, EventArgs e) =>
            pnlPlot.Visible = plotPanelToolStripMenuItem.Checked = !plotPanelToolStripMenuItem.Checked;

        private void btnToggleControlsPane_Click(object sender, EventArgs e) => controlsToolStripMenuItem_Click(sender, e);
        private void btnTogglePropertiesPane_Click(object sender, EventArgs e) => propertiesToolStripMenuItem_Click(sender, e);
        private void btnMouseIdle_Click(object sender, EventArgs e) => idleToolStripMenuItem_Click(sender, e);
        private void btnMousePaint_Click(object sender, EventArgs e) => paintToolStripMenuItem_Click(sender, e);
        private void btnMouseErase_Click(object sender, EventArgs e) => eraseToolStripMenuItem_Click(sender, e);
        private void btnDeletePaint_Click(object sender, EventArgs e)
        {
            MapView.SelectedPatches?.Clear();
            MapView.Invalidate();
        }

        private void btnDrawRectangle1_Click(object sender, EventArgs e)
        {
            UncheckMouseModeMenuItems();
            MapView.ReplaceMouseMode(new mouse.DragRectangleMode { Modulus = 1 });
        }

        private void btnDrawRectangle128_Click(object sender, EventArgs e)
        {
            UncheckMouseModeMenuItems();
            MapView.ReplaceMouseMode(new mouse.DragRectangleMode { Modulus = TerrainPatch.DefaultSize });
        }

        private void btnDrawRectangle1024_Click(object sender, EventArgs e)
        {
            UncheckMouseModeMenuItems();
            MapView.ReplaceMouseMode(new mouse.DragRectangleMode { Modulus = TerrainStretch.DefaultSize });
        }

        private void generateAverageSunSeriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var generator = new calc.LightCurveApprox();
            var image_start = new DateTime(2028, 1, 1);
            var image_stop = new DateTime(2030, 1, 1);
            var image_step = new TimeSpan(29, 12, 0, 0);

            for (var start = image_start; start < image_stop; start += image_step)
            {
                var light_curve_image = new calc.AverageSunImage
                {
                    Start = start,
                    Stop = start + image_step,
                    Step = new TimeSpan(2, 0, 0),
                    Generator = generator,
                    Bounds = ToRectangle(MapView.SelectionRectangle)
                };
                var filename = $"avg-sun-{start.ToString("yyyy-MM-dd")}.png";
                Console.WriteLine($"Generating {filename}");
                light_curve_image.Load();
                light_curve_image.Image.Save(filename, ImageFormat.Png);
            }
        }

        Rectangle ToRectangle(RectangleF r) => new Rectangle((int)r.X, (int)r.Y, (int)r.Width, (int)r.Height);

        private void btnMouseRulerMeasure_Click(object sender, EventArgs e)
        {
            UncheckMouseModeMenuItems();
            MapView.ReplaceMouseMode(new mouse.RulerMeasureMode());
        }

        private void btnMouseCrossSection_Click(object sender, EventArgs e)
        {
            UncheckMouseModeMenuItems();
            MapView.ReplaceMouseMode(new mouse.CrossSectionMode());
        }

        private void gPUTestSaturdayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Write 16640_13440_gpu.bin, center only
            var terrain = new TerrainPatch { Line = 16640, Sample = 13440, };
            var gpu = new GPUProcessor { Terrain = Terrain, MapView = MapView };
            gpu.AddNearHorizon(terrain);
            var hb = terrain.Horizons[0][0];
            var angles = hb.GetSlopeDegreeBuffer();
            var slope0 = hb.Buffer[0];
            var angle0 = angles[0];
            Console.WriteLine("here");
        }

        private void writeLongestNightDatasetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var generator = new calc.LightCurveApprox();
            var rect = ToRectangle(MapView.SelectionRectangle);
            var start = new DateTime(2028, 1, 1);
            var stop = new DateTime(2029, 1, 1);
            var step = new TimeSpan(2, 0, 0);
            var threshold = 0.8f;
            var filename = "shackleton-longest-night-2028-4m.tds";
            var data = generator.GenerateLongestNightDataset(rect, start, stop, step, threshold);
            var dataset = new TimeImageDataset { Data = data, Bounds = rect };
            dataset.Write(filename);
        }

        private void writeLongestNightHistogramCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var filename = "longest-night-2028.bin";
            var filename = "shackelton-longest-night-2028.tds";
            var dataset = new calc.TimeImageDataset(filename);
            var outfilename = Path.ChangeExtension(filename, "csv");
            using (var writer = new StreamWriter(outfilename))
                foreach (var ticks in dataset.Enumerate())
                    writer.WriteLine((new TimeSpan(ticks).TotalHours));
        }

        private unsafe void writeLongestNightImageFromDatasetToolStripMenuItem_Click(object sender, EventArgs e)
        {   // {X = 18176 Y = 7936 Width = 2048 Height = 1792}
            var rect = new Rectangle(18176, 7936, 2048, 1792);
            var filename = "longest-night-2028.tds";
            var dataset = new calc.TimeImageDataset(filename);
            var data = dataset.Data;
            using (var image = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb))
            {
                var width = image.Width;
                var height = image.Height;
                Debug.Assert(width == data.GetLength(1) && height == data.GetLength(0));
                // Draw hillshade
                var step = TerrainPatch.DefaultSize;
                using (var g = Graphics.FromImage(image))
                    for (var line = rect.Top; line < rect.Bottom; line += step)
                        for (var sample = rect.Left; sample < rect.Right; sample += step)
                        {
                            var patch = new TerrainPatch { Line = line, Sample = sample };
                            using (var hillshade = patch.GetHillshade())
                            {
                                //hillshade.Save($"test-{line}-{sample}.png", ImageFormat.Png);
                                g.DrawImage(hillshade, sample - rect.Left, line - rect.Top);
                            }
                        }

                var ticks_1000_hours = new TimeSpan(340, 0, 0).Ticks;
                var min_ticks = data.Enumerate().Where(t => t > 0 && t <= ticks_1000_hours).Min();
                var min_hours = new TimeSpan(min_ticks).TotalHours;
                var max_ticks = data.Enumerate().Where(t => t > 0 && t <= ticks_1000_hours).Max();
                var max_hours = new TimeSpan(max_ticks).TotalHours;

                var delta_hours = max_hours - min_hours;

                var bmpData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.WriteOnly, image.PixelFormat);
                for (var y = 0; y < height; y++)
                {
                    var rowptr = (int*)(bmpData.Scan0 + y * bmpData.Stride);
                    for (var x = 0; x < width; x++)
                    {
                        var d = data[y, x];
                        if (d < min_ticks || d > max_ticks)  // Outside interestingrange
                            continue;

                        var hours = new TimeSpan(d).TotalHours;
                        var frac = (hours-min_hours) / delta_hours;
                        var hue = frac * 240;

                        if (frac < 0.2)
                            Console.WriteLine($"[{x},{y}]");

                        var base_color = Color.FromArgb(rowptr[x]);
                        var hsl = ColorUtilities.RGBtoHSB(base_color.R, base_color.G, base_color.B);
                        //hsl.Saturation = sat;
                        //hsl.Hue = sat;

                        var rgb = ColorUtilities.HSBtoRGB(0, 1, 1);  // hsl.Brightness);
                        rowptr[x] = Color.FromArgb(rgb.Red, rgb.Green, rgb.Blue).ToArgb();
                    }
                }
                image.UnlockBits(bmpData);

                image.Save("test-image.png", ImageFormat.Png);
            }
        }

        private void sundayTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var img = new TimeSpanImage("longest-night-2028.tds");
            InsertLayer(img);
        }

        private void mondayTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var img = new TimeSpanImage("shackelton-longest-night-2028.tds");
            InsertLayer(img);
        }

        private void shadowBugCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapView.FinishedPatches = new List<TerrainPatch> { GetPatch(new Point(114, 123)), GetPatch(new Point(114, 124)) };
           // GetLayerByName("");
            MapView.Invalidate();
        }

        private void loadHorizonsForPaintedTilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapView.FinishedPatches = MapView.SelectedPatches.Select(p => GetPatch(p)).Where(p => p != null).ToList();
            MapView.Invalidate();
        }

        private void stopDrawingPaintedTilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var to_keep = MapView.FinishedPatches.Where(p => !MapView.SelectedPatches.Exists(s => s.Id == p.Id)).ToList();
            MapView.FinishedPatches = to_keep;
            MapView.Invalidate();
        }

        private void unloadHorizonsForPaintedTilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var to_delete = MapView.FinishedPatches.Where(p => MapView.SelectedPatches.Exists(s => s.Id == p.Id)).ToList();
            var to_keep = MapView.FinishedPatches.Where(p => !MapView.SelectedPatches.Exists(s => s.Id == p.Id)).ToList();
            MapView.FinishedPatches = to_keep;
            MapView.Invalidate();
            foreach (var key in to_delete.Select(p => p.Id))
                if (PatchCache.TryGetValue(key, out TerrainPatch patch))
                {
                    patch.Dispose();
                    PatchCache.Remove(key);
                }
        }

        private void deleteHorizonsForPaintedTilesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (var patch in MapView.SelectedPatches)
            {
                var filename = patch.DefaultPath;
                if (File.Exists(filename))
                    File.Delete(filename);
            }
        }

        private void fridayTestnobileLongestnightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var img = new TimeSpanImage("nobile-2m-longest-night-2028.tds");
            InsertLayer(img);
        }

        private void loadShackletonLongestNight20284mToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var img = new TimeSpanImage("shackleton-longest-night-2028-4m.tds");
            InsertLayer(img);
        }

        private void tbObserverHeight_TextChanged(object sender, EventArgs e) =>
            tbObserverHeight.ForeColor = float.TryParse(tbObserverHeight.Text, out float val) ? Color.Black : Color.Red;

        private float GetObserverHeight() => float.TryParse(tbObserverHeight.Text, out float val) ? val / 1000f : 0f;

        private void paintSelectedRectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapView.SelectedPatches.AddRange(GetPatchesUnderRectangle(MapView.SelectionRectangle));
            CheckSelectedPatchesLayer();
        }

        private List<TerrainPatch> GetPatchesUnderRectangle(RectangleF r) =>
            GetPatchesUnderRectangle(new Rectangle((int)Math.Round(r.Left), (int)Math.Round(r.Top), (int)Math.Round(r.Width), (int)Math.Round(r.Height)));

        private List<TerrainPatch> GetPatchesUnderRectangle(Rectangle r)
        {
            var d = TerrainPatch.DefaultSize;
            var left = (r.Left / d) * d;
            if (left < r.Left) left += d;
            var top = (r.Top / d) * d;
            if (top < r.Top) top += d;
            var right = (r.Right / d) * d - 1;
            if (right > r.Right) right -= d;
            var bottom = (r.Bottom / d) * d - 1;
            if (bottom > r.Bottom) bottom -= d;
            var result = new List<TerrainPatch>();
            if (right < left || bottom < top)
                return result;
            for (var y = top; y <= bottom; y += d)
                for (var x = left; x <= right; x += d)
                    result.Add(TerrainPatch.FromLineSample(y, x));
            return result;
        }

        private void filterTentpolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterTentpolesToolStripMenuItem.Checked = !filterTentpolesToolStripMenuItem.Checked;
        }

        internal void ShowRenderPlot(bool v)
        {
            pnlPlot.Visible = true;
            renderPlot.Visible = true;
        }
    }
}