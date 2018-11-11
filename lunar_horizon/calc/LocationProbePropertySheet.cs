using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using lunar_horizon.view;
using lunar_horizon.mouse;
using lunar_horizon.patches;
using ZedGraph;
using lunar_horizon.terrain;
using lunar_horizon.utilities;

namespace lunar_horizon.calc
{
    public partial class LocationProbePropertySheet : UserControl, IMapLayerPropertySheet
    {
        public static Color[] ProbeColors = new[] { Color.Blue, Color.BlueViolet, Color.Brown, Color.Chartreuse, Color.Chartreuse, Color.Crimson, Color.DarkOrchid, Color.Aqua, Color.Firebrick, Color.Goldenrod, Color.LightSeaGreen };

        public IMapLayer Layer { get; set; }

        protected Dictionary<LocationProbe, CurveItem> _horizonCache = new Dictionary<LocationProbe, CurveItem>();
        protected Dictionary<LocationProbe, CurveItem> _sunCache = new Dictionary<LocationProbe, CurveItem>();
        protected Dictionary<LocationProbe, CurveItem> _lightCurveCache = new Dictionary<LocationProbe, CurveItem>();
        protected List<LocationProbe> _requestProbe = new List<LocationProbe>();
        protected List<Point> _requestPoint = new List<Point>();

        public LocationProbePropertySheet()
        {
            InitializeComponent();
        }

        public Color AllocateColor()
        {
            var copy = ProbeColors.ToList();
            foreach (var item in lvProbes.Items)
                copy.Remove(((item as ListViewItem).Tag as LocationProbe).Color);
            return copy.Count > 0 ? copy[0] : Color.Red;
        }

        public LocationProbe NewLocationProbe()
        {
            var probe = new LocationProbe { Color = AllocateColor(), Text = "<Click on probe location in map>" };
            Add(probe);
            return probe;
        }

        public void Add(LocationProbe probe)
        {
            var item = new ListViewItem { Text = probe.Text, Tag = probe, BackColor = probe.Color };
            lvProbes.Items.Add(item);
            if (!(Layer is LocationProbeMapLayer layer)) return;
            layer.Probes.Add(probe);
            if (layer == LunarHorizon.Singleton.GetSelectedLayer())
                LunarHorizon.Singleton.MapView.Invalidate();
        }

        public void Dragging(LocationProbe probe)
        {
            if (rbHorizon.Checked)
                UpdateHorizon(probe);
        }

        public void StopDragging(LocationProbe probe)
        {
            if (rbLightcurve.Checked)
                UpdateLightCurve(probe);
        }

        LightCurveApprox _lightCurveGenerator = null;

        private void UpdateLightCurve(LocationProbe probe)
        {
            lock (this)
            {
                if (_lightCurveGenerator == null)
                    _lightCurveGenerator = new LightCurveApprox();
                var pt = probe.Location.ToPoint();
                var points = _lightCurveGenerator.GetLightCurve(pt.Y, pt.X, new DateTime(2028, 1, 1), new DateTime(2029, 1, 1), new TimeSpan(2, 0, 0));
                var curve = new LineItem(null, points, probe.Color, SymbolType.None);
                _lightCurveCache[probe] = curve;
            }
            UpdatePlots();
        }

        Task _updateHorizonTask = null;
        private void UpdateHorizon(LocationProbe probe)
        {
            AddRequest(probe);
            if (_updateHorizonTask != null) return;
            _updateHorizonTask = Task.Run(() =>
            {
                restart:
                Point? id;
                while ((id = GetRequestedId(probe)).HasValue && !LunarHorizon.Singleton.PatchCache.ContainsKey(id.Value))
                    LunarHorizon.Singleton.GetPatch(id.Value);
                if (!id.HasValue) return;
                var patch = LunarHorizon.Singleton.GetPatch(id.Value);
                if (patch == null) return;
                var loc = GetRequest(probe);
                if (!loc.HasValue) return;
                var pt = new Point(loc.Value.X - patch.Id.X * TerrainPatch.DefaultSize, loc.Value.Y - patch.Id.Y * TerrainPatch.DefaultSize);
                if (pt.X < 0 || pt.X >= TerrainPatch.DefaultSize || pt.Y < 0 || pt.Y >= TerrainPatch.DefaultSize)
                    goto restart;
                var horizon = patch.GetHorizon(pt.Y, pt.X);
                lock (_horizonCache)
                {
                    _horizonCache[probe] = GetHorizonCurve(horizon, probe.Color);
                    _sunCache[probe] = GetSunCurve(patch, pt, LunarHorizon.Singleton.MapView.SunVector, probe.Color);
                }
                var loc2 = GetRequest(probe);
                if (loc2.HasValue && loc2.Value != loc.Value)
                    goto restart;
                _updateHorizonTask = null;
                UpdatePlots();
            });
        }

        private Point? GetRequestedId(LocationProbe probe)
        {
            var pt = GetRequest(probe);
            return pt.HasValue ? (Point?)TerrainPatch.LineSampleToId(pt.Value) : null;
        }

        private Point? GetRequest(LocationProbe probe)
        {
            lock (_requestProbe)
            {
                var index = _requestProbe.IndexOf(probe);
                return index < 0 ? null : (Point?)_requestPoint[index];
            }
        }

        private void AddRequest(LocationProbe probe)
        {
            lock (_requestProbe)
            {
                var pt = new Point((int)probe.Location.X, (int)probe.Location.Y);
                var index = _requestProbe.IndexOf(probe);
                if (index < 0)
                {
                    _requestProbe.Add(probe);
                    _requestPoint.Add(pt);
                }
                else
                    _requestPoint[index] = pt;
            }
        }

        private void DeleteRequeest(LocationProbe probe)
        {
            lock (_requestProbe)
            {
                var index = _requestProbe.IndexOf(probe);
                if (index >= 0)
                {
                    _requestProbe.RemoveAt(index);
                    _requestPoint.RemoveAt(index);
                }
            }
        }

        private CurveItem GetHorizonCurve(Horizon h, Color color)
        {
            PointPairList points = null;
            foreach (var pt in h.PartialPoints().Where(pt => pt.HasValue))
                (points ?? (points = new PointPairList())).Add(pt.Value.X, pt.Value.Y);
            return new LineItem(null, points, color, SymbolType.None);
        }

        private CurveItem GetSunCurve(TerrainPatch patch, Point pt, math.Vector3d sun, Color color)
        {
            patch.FillPointsAndMatrices(LunarHorizon.Terrain);
            patch.GetAzEl(sun, pt.X, pt.Y, out float az_rad, out float el_rad);

            {
                var horizon = patch.GetHorizon(pt.Y, pt.X);
                var frac = (byte)(255f * horizon.SunFraction2(az_rad * 180f / 3.141592653589f, el_rad * 180f / 3.141592653589f));
                Console.WriteLine($"patch.id={patch.Id} pt=[{pt.X},{pt.Y}] Sun frac={frac}");
            }

            var az_deg = az_rad * 180f / 3.141592565589f;
            var el_deg = el_rad * 180f / 3.141592565589f;
            const int count = 12;
            const double radius = 32d / 60d;
            var points = new PointPairList();
            for (var i = 0; i <= count; i++)
            {
                var ang = i * 2d * Math.PI / count;
                points.Add(az_deg + radius * Math.Cos(ang), el_deg + radius * Math.Sin(ang));
            }
            return new LineItem(null, points, color, SymbolType.None);
        }

        private void UpdatePlots()
        {
            lock (this)
            {
                if (rbHorizon.Checked)
                    LunarHorizon.Singleton.RenderPlot(_horizonCache.Values.Concat(_sunCache.Values).ToList(), "azimuth", "elevation", "horizon");
                else if (rbLightcurve.Checked)
                    LunarHorizon.Singleton.RenderPlot(_lightCurveCache.Values.ToList(), "Date", "Sunlight", "Light Curve", x_axis_dates: true);
            }
        }

        public void TimeChanged(DateTime time)
        {
            if (!Visible) return;
            if (rbHorizon.Checked)
                UpdateSunCache();
        }

        private void UpdateSunCache()
        {
            lock (_horizonCache)
            {
                var keys = _sunCache.Keys.ToList();
                foreach (var probe in keys)
                {
                    var pt = new Point((int)probe.Location.X, (int)probe.Location.Y);
                    var id = TerrainPatch.LineSampleToId(pt);
                    if (!LunarHorizon.Singleton.PatchCache.TryGetValue(id, out TerrainPatch patch)) continue;
                    var ptInPatch = patch.PointInPatch(pt);
                    _sunCache[probe] = GetSunCurve(patch, ptInPatch, LunarHorizon.Singleton.MapView.SunVector, probe.Color);
                }
            }
            UpdatePlots();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var probe = NewLocationProbe();
            lvProbes.Items.Add(new ListViewItem { Text = probe.Text, Tag = probe, BackColor = probe.Color });
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selected = lvProbes.SelectedIndices;
            var lst = new List<int>();
            for (var i = 0; i < selected.Count; i++)
                lst.Add(selected[i]);
            lst.Sort();
            var probes = lst.Select(i => lvProbes.Items[i].Tag as LocationProbe).Where(p => p!=null).ToList();
            for (var i = lst.Count - 1; i >= 0; i--)
                lvProbes.Items.RemoveAt(i);
            foreach (var probe in probes)
                if (_lightCurveCache.ContainsKey(probe))
                    _lightCurveCache.Remove(probe);
            UpdatePlots();
        }

        private void lvProbes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public class ProbeRequest
    {
        public LocationProbe Probe;
        public Point Location;
    }
}
