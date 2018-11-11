using lunar_horizon.math;
using lunar_horizon.mouse;
using lunar_horizon.patches;
using lunar_horizon.tiles;
using lunar_horizon.view;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace lunar_horizon.views
{
    public delegate void ChangedTimeHandler(DateTime time);

    public class MapView : PickablePanel
    {
        const float MetersPerPixel = 20f;

        public LunarHorizon MainWindow;

        public float OverlayTransparency = 0.5f;
        public float ControlKeyTransparency = 0.5f;
        public float ShiftKeyTransparency = 0.1f;
        public float AltKeyTransparency = 0.1f;

        public bool DrawTimestamp;
        public static Font TimestampFont = new Font("Arial", 32, FontStyle.Regular);
        public static Font RulerMeasureFont = new Font("Arial", 16, FontStyle.Regular);

        protected List<IMapLayer> _Layers;
        public List<IMapLayer> Layers
        {
            get { return _Layers; }
            set
            {
                _Layers = value;
                Invalidate();
            }
        }

        public bool UseRoutePlanner;

        protected readonly ImageAttributes _imageAttributes = new ImageAttributes();
        protected readonly ColorMatrix _colorMatrix = new ColorMatrix();

        public List<LocationProbe> Probes = new List<LocationProbe>();
        public override IEnumerable<Pickable> Pickables() => Probes == null ? Enumerable.Empty<Pickable>() : Probes.AsEnumerable<Pickable>();

        public Vector3d SunVector;
        public Vector3d EarthVector;

        protected const int PenWidth = 2;
        protected readonly Pen _greenPen = new Pen(Brushes.Green, PenWidth);

        protected readonly Pen _redPen = new Pen(Brushes.Red, PenWidth);
        protected readonly Pen _bluePen = new Pen(Brushes.Blue, PenWidth);

        public bool DrawPatchShadows = false;
        public List<TerrainPatch> FinishedPatches = new List<TerrainPatch>();
        protected readonly Brush _finishedBrush = new SolidBrush(Color.FromArgb(50, 100, 100, 255));  // blue
        protected readonly Pen _finishedPen = new Pen(Color.FromArgb(130, 130, 255), 1);

        protected readonly Pen _selectionPen = new Pen(Color.LightCyan, 2);
        protected readonly Brush _selectionBrush = new SolidBrush(Color.LightCyan);

        public List<TerrainPatch> SelectedPatches = new List<TerrainPatch>();
        public Action SelectedPatchesChanged;
        protected readonly Brush _tileSelectionBrush = new SolidBrush(Color.FromArgb(200, 100, 255, 100)); // green
        protected readonly Pen _tileSelectionPen = new Pen(Color.FromArgb(130, 255, 130), 1);

        protected TerrainPatch _highlightPatch = new TerrainPatch { Width = TerrainPatch.DefaultSize, Height = TerrainPatch.DefaultSize };
        protected bool _isHighlightPatchVisible;
        protected readonly Brush _highlightBrush = new SolidBrush(Color.FromArgb(200, 110, 255, 110)); // green, mouse highlight

        // Patch currently being processed (horizon generated)
        public List<TerrainPatch> ProcessingPatches = new List<TerrainPatch>();
        protected readonly Brush _processingBrush = new SolidBrush(Color.FromArgb(200, 255, 100, 100));  // red
        protected readonly Pen _processingPen = new Pen(Color.FromArgb(255, 130, 130), 1);

        // Patch used to generate horizon
        public List<TerrainPatch> FarPatches = new List<TerrainPatch>();
        protected readonly Brush _farBrush = new SolidBrush(Color.FromArgb(200, 200, 80, 80));  // darker red
        protected readonly Pen _farPen = new Pen(Color.FromArgb(200, 100, 100), 1);

        public List<Tuple<Point, Brush>> ColoredPatches = new List<Tuple<Point, Brush>>();
        public TraversePlanner.planning.LatLonTraverseSummary TraverseSummary;

        public MouseMessage MouseMessage;
        public float SunAzimuth;  // Radians
        public float SunElevation;
        public float EarthAzimuth;
        public float EarthElevation;

        protected bool _SelectionRectangleIsVisible = false;
        public bool SelectionRectangleIsVisible
        {
            get { return _SelectionRectangleIsVisible; }
            set { _SelectionRectangleIsVisible = value; Invalidate(); }
        }

        protected RectangleF _SelectionRectangle = new RectangleF(0, 0, 0, 0);
        public RectangleF SelectionRectangle
        {
            get { return _SelectionRectangle; }
            set { _SelectionRectangle = value; if (SelectionRectangleIsVisible) Invalidate(); }
        }

        protected bool _RulerMeasureIsVisible = false;
        public bool RulerMeasureIsVisible
        {
            get { return _RulerMeasureIsVisible; }
            set { _RulerMeasureIsVisible = value; Invalidate(); }
        }

        protected RectangleF _RulerMeasure = new RectangleF(0, 0, 0, 0);
        public RectangleF RulerMeasure
        {
            get { return _RulerMeasure; }
            set { _RulerMeasure = value; if (_RulerMeasureIsVisible) Invalidate(); }
        }

        protected ContextMenu _contextMenu;

        public TileTree _OverlayTree;
        public TileTree OverlayTree
        {
            get { return _OverlayTree; }
            set {
                _OverlayTree = value;
                _OverlayTree?.EnsureCacheDirectoryExists();
                Invalidate();
            }
        }

        private DisplayTransform _Transform;
        public DisplayTransform Transform {
            get { return _Transform; }
            set {
                _Transform = value;
                ClampTransform();
                Invalidate();
            }
        }

        private void ClampTransform()
        {
            _Transform.OffsetX = Math.Min(0, _Transform.OffsetX);
            _Transform.OffsetY = Math.Min(0, _Transform.OffsetY);
        }

        public MapView()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();

            Transform = new DisplayTransform { OffsetX = 0, OffsetY = 0, Scale = 32f };

            MouseModeStack = new Stack<MouseMode>(new[] { GetIdleMode() });

            InitializeContextMenu();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float MapScale
        {
            get { return _Transform.Scale; }
            set {
                _Transform.Scale = value;
                ScaleChanged?.Invoke(this, null);
                Invalidate();
            }
        }

        public event EventHandler ScaleChanged;

        public PointF TransformMouse(Point l) => Transform.MouseToMap(l);
        public float TransformMouseX(int x) => Transform.MouseToMapX(x);
        public float TransformMouseY(int y) => Transform.MouseToMapY(y);

        internal void MapView_PlanChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        #region Paint

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            if (DesignMode) return;
            PaintMap(e, e.Graphics);
        }

        public void PaintMap(PaintEventArgs e, Graphics g)
        {
            try
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;

                foreach (var layer in Layers)
                    layer.Draw(g, Transform);

                /*
                BaseTree.Draw(g, Transform);

                if (DrawPatchShadows)
                {
                    if (false)
                    {
                        foreach (var p in FinishedPatches)
                            p.Draw(g, Transform, p.GetShadows(ShadowCaster));
                    }
                    else
                    {
                        var window = Bounds;

                        // Matrices are needed for GetShadowsCarefully
                        if (FinishedPatches.Count > 0 && FinishedPatches[0].Matrices == null)
                            foreach (var p in FinishedPatches)
                                p.FillMatrices(TileTreeNode.Terrain);

                        foreach (var p in FinishedPatches)
                            if (p.IsVisible(Transform, window))
                                p.Draw(g, Transform, p.GetShadowsCarefully(ShadowCaster));
                    }
                }
                else
                {
                    foreach (var p in FinishedPatches)
                    {
                        p.Draw(g, Transform, _finishedBrush);
                        //p.Draw(g, Transform, _finishedPen);
                    }
                }

                if (OverlayTree != null)
                {
                    g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                    _colorMatrix.Matrix33 = OverlayTransparency;
                    _imageAttributes.SetColorMatrix(_colorMatrix);

                    OverlayTree.Draw(g, Transform, _imageAttributes);
                    //var r = new Rectangle((int)destrect.Left, (int)destrect.Top, (int)destrect.Width, (int)destrect.Height);
                    //g.DrawImage(bmp, r, 0, 0, Dataset.Description.Width, Dataset.Description.Height, GraphicsUnit.Pixel, _imageAttributes);

                    g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;  // Make sure to return this.  DrawString will fail later if this is left as .SourceCopy

                }


                foreach (var p in SelectedPatches)
                {
                    p.Draw(g, Transform, _selectionBrush);
                    p.Draw(g, Transform, _selectionPen);
                }
                */

                for (var i = 0; i < ColoredPatches.Count; i++)
                    DrawColoredPatch(g, Transform, ColoredPatches[i]);

                for (var i = 0; i < FarPatches.Count; i++)
                    FarPatches[i].Draw(g, Transform, _redPen);

                for (var i = 0; i < ProcessingPatches.Count; i++)
                    ProcessingPatches[i].Draw(g, Transform, _tileSelectionBrush);

                if (_isHighlightPatchVisible)
                    _highlightPatch.Draw(g, Transform, _highlightBrush);

                if (TraverseSummary != null)
                    DrawTraverseSummary(g, Transform, TraverseSummary);

                // Draw other pickables

                foreach (Pickable p in Pickables())
                    p.Paint(e, this);

                if (MouseMessage != null && MouseMessage.Message != null)
                    g.DrawString(MouseMessage.Message, MouseMessage.Font, MouseMessage.Brush, MouseMessage.X, MouseMessage.Y);

                if (SelectionRectangleIsVisible)
                    DrawSelectionRectangle(g, Transform, SelectionRectangle);

                if (RulerMeasureIsVisible)
                    DrawRulerMeasure(g, Transform, RulerMeasure);

                // Changes clipping region, so do this last, for now
                //DrawLegend(g);
            }
            catch (Exception e1)
            {
                Console.WriteLine(e1);
            }
        }

        private void DrawTraverseSummary(Graphics g, DisplayTransform t, TraversePlanner.planning.LatLonTraverseSummary traverseSummary)
        {
            var lst = traverseSummary.LineSampleList;
            var last = new PointF(0f, 0f);
            for (var i = 0; i < lst.Count; i++)
            {
                var p = t[lst[i]];
                if (i > 0)
                    g.DrawLine(Pens.IndianRed, last, p);
                last = p;
            }
        }

        private void DrawSelectionRectangle(Graphics g, DisplayTransform t, RectangleF r)
        {
            var loc = t[r.Location];
            var siz = t[r.Size];
            g.DrawRectangle(_selectionPen, loc.X, loc.Y, siz.Width, siz.Height);
        }

        private void DrawRulerMeasure(Graphics g, DisplayTransform t, RectangleF r)
        {
            var m1 = r.Location;
            var m2 = new PointF(m1.X + r.Width, m1.Y + r.Height);
            var p1 = t[m1];
            var p2 = t[m2];
            g.DrawLine(_selectionPen, p1.X, p1.Y, p2.X, p2.Y);
            var d = MetersPerPixel * m1.Distance(m2);
            string str = d > 1000f ? $"{(d/1000f).ToString("F2")} km" : $"{d.ToString("F0")} m";
            g.DrawString(str, RulerMeasureFont, _selectionBrush, p2.X + 4f, p2.Y + 4f, StringFormat.GenericDefault);
        }

        private void DrawColoredPatch(Graphics g, DisplayTransform t, Tuple<Point, Brush> tuple)
        {
            var sample = tuple.Item1.X * TerrainPatch.DefaultSize;
            var line = tuple.Item1.Y * TerrainPatch.DefaultSize;
            var p1 = t[new PointF(sample - 0.5f, line - 0.5f)];
            var p2 = t[new PointF(sample + TerrainPatch.DefaultSize + 1f, line + TerrainPatch.DefaultSize + 1f)];
            g.FillRectangle(tuple.Item2, p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y);

        }

        private readonly Brush _LegendBrush = new SolidBrush(Color.WhiteSmoke);
        private readonly Point[] _transformedPoints = new Point[1];
        private void DrawLegend(Graphics g)
        {
            const float LegendSize = 5000f;   // 5 km
            const float LegendY = 10;
            const float LegendX = 5;
            const float LegendTickHeight = 5f;

            const int rTop = 2;
            const int rLeft = 2;
            const int rWidth = 600;
            const int rHeight = 30;

            float _mapScale = Transform.Scale;

            const float targetLength = LegendSize / MetersPerPixel;
            float targetMeters = targetLength * MetersPerPixel / _mapScale;
            double legendMeters = Math.Pow(10d, Math.Ceiling(Math.Log10(targetMeters)));
            double legendStep = legendMeters / 10d;

            float factor = _mapScale / MetersPerPixel;

            g.Clip = new Region(new Rectangle(rTop, rLeft, rWidth, rHeight));
            g.FillRegion(Brushes.DarkGray, g.Clip);

            g.DrawLine(Pens.WhiteSmoke, LegendX, LegendY, LegendX + (float)(legendMeters * factor), LegendY);
            for (int i = 0; i < 11; i++)
            {
                float x = LegendX + (float)(legendStep * i * factor);
                g.DrawLine(Pens.WhiteSmoke, x, LegendY, x, LegendY - LegendTickHeight);
                //g.DrawString((i * legendStep).ToString() + " m", DrawingHelper.DrawFont, _LegendBrush, x, LegendY);
            }
        }

        /// <summary>
        /// Draw a compass face with vectors to the sun and earth
        /// Note that azimuth has 0 due north and increases clockwise
        /// </summary>
        /// <param name="g"></param>
        private void DrawSunEarthAngles(Graphics g)
        {
            const int size = 100;
            const int size2 = size / 2;
            const int border = 5;
            const int tick = 5;

            Rectangle c = ClientRectangle;

            float cx = c.Width - border - size2;
            const float cy = border + size2;
            g.FillEllipse(Brushes.LightSlateGray, cx - size2, cy - size2, size, size);
            const float vector_length = .8f * size2;

            g.DrawLine(Pens.Black, cx + size2 - tick, cy, cx + size2, cy);
            g.DrawLine(Pens.Black, cx - size2, cy, cx - size2 + tick, cy);
            g.DrawLine(Pens.Black, cx, cy - size2, cx, cy - size2 + tick);
            g.DrawLine(Pens.Black, cx, cy + size2 - tick, cx, cy + size2);

            g.DrawLine(Pens.Blue, cx, cy, cx + vector_length * (float)Math.Sin(EarthAzimuth), cy - vector_length * (float)Math.Cos(EarthAzimuth));
            g.DrawLine(Pens.Orange, cx, cy, cx + vector_length * (float)Math.Sin(SunAzimuth), cy - vector_length * (float)Math.Cos(SunAzimuth));

            /*
            if (false)
            {
                cy += size + border;
                g.FillEllipse(Brushes.LightSlateGray, cx - size2, cy - size2, size, size);
                g.DrawLine(Pens.Black, cx + size2 - tick, cy, cx + size2, cy);
                g.DrawLine(Pens.Black, cx - size2, cy, cx - size2 + tick, cy);
                g.DrawLine(Pens.Black, cx, cy - size2, cx, cy - size2 + tick);
                g.DrawLine(Pens.Black, cx, cy + size2 - tick, cx, cy + size2);
                g.DrawLine(Pens.Blue, cx, cy, cx + vector_length * (float)Math.Cos(EarthElevation), cy - vector_length * (float)Math.Sin(EarthElevation));
                g.DrawLine(Pens.Orange, cx, cy, cx + vector_length * (float)Math.Cos(SunElevation), cy - vector_length * (float)Math.Sin(SunElevation));
            }*/
        }

        public void DrawLine(Graphics g, Pen pen, System.Windows.Vector p1, System.Windows.Vector p2) => g.DrawLine(pen, _Transform[p1], _Transform[p2]);
        public void DrawLineToward(Graphics g, Pen pen, System.Windows.Vector p1, System.Windows.Vector p2, double d) => g.DrawLine(pen, _Transform[p1], _Transform[p1 + d * p2]);
        public void DrawPoint(Graphics g, Brush b, System.Windows.Vector v)
        {
            var m = _Transform[v];
            g.FillEllipse(b, m.X - 1, m.Y - 1, 3, 3);
        }

        public void WriteMapImage(string filename)
        {
            var bmp = new Bitmap(Width, Height, PixelFormat.Format32bppRgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                var e = new PaintEventArgs(g, Bounds);
                PaintMap(e, g);
            }
            bmp.Save(filename);
        }

        public void WriteMapImageToBitmap(Bitmap bmp)
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                var e = new PaintEventArgs(g, Bounds);
                PaintMap(e, g);
            }
        }

        #endregion Paint

        #region Picking

        /// <summary>
        /// PickablePanel does picking in map coordinate space.  I really want to move back to doing it in screen coordinates.
        /// This is the simplest hack to do that.  Otherwise, various state maintained by mouse modes would need to change.
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public override Pickable Pick(PointF pt)
        {
            Pickable r = null;
            const float dist = 5f;
            float distScaled = dist / Transform.Scale;
            float threshold = distScaled * distScaled;
            float mind2 = float.MaxValue;
            foreach (Pickable p in Pickables())
            {
                float d2 = (p.X - pt.X) * (p.X - pt.X) + (p.Y - pt.Y) * (p.Y - pt.Y);
                if (d2 <= mind2)
                {
                    r = p;
                    mind2 = d2;
                }
            }
            return mind2 <= threshold ? r : null;
        }

        #endregion Picking

        public struct DisplayTransform
        {
            public int OffsetX;
            public int OffsetY;
            public float Scale;

            public override string ToString() => $"<DisplayTransform Scale={Scale} OffsetX={OffsetX} OffsetY={OffsetY}";

            public PointF MouseToMap(Point m) => new PointF((m.X - OffsetX) * Scale, (m.Y - OffsetY) * Scale);
            public float MouseToMapX(int mx) => (mx - OffsetX) * Scale;
            public float MouseToMapY(int my) => (my - OffsetY) * Scale;

            /*
            public PointF MapToMouse(Point p)
            {
                float cellSize;
                int level, stride;
                TileTree.DrawCellSize(this, out cellSize, out stride, out level);
                var 
            }*/

            public float X(float mx) => mx / Scale + OffsetX;
            public float Y(float my) => my / Scale + OffsetY;
            public PointF MapToMouse(PointF p) => new PointF(X(p.X), Y(p.Y));
            
            public PointF this[PointF p] => new PointF(p.X/Scale+OffsetX,p.Y/Scale+OffsetY);
            public SizeF this[SizeF s] => new SizeF(s.Width/Scale,s.Height/Scale);

            public RectangleF this[Rectangle r] => new RectangleF(this[r.Location], this[r.Size]);
            public RectangleF this[RectangleF r] => new RectangleF(this[r.Location], this[r.Size]);

            //TODO: Change these?
            public Rectangle Transform(Rectangle r) => new Rectangle((int)(r.Left * Scale + OffsetX), (int)(r.Top * Scale + OffsetY), (int)(r.Width * Scale), (int)(r.Height * Scale));
            public Rectangle InvTransform(int Stride, Rectangle r) => new Rectangle(Stride * (int)((r.Left - OffsetX) / Scale), Stride * (int)((r.Y - OffsetY) / Scale), Stride * (int)(r.Width / Scale), Stride * (int)(r.Height / Scale));
            public PointF this[System.Windows.Vector p] => new PointF((float)(p.X * Scale + OffsetX), (float)(p.Y * Scale + OffsetY));
        }

        public event EventHandler PanZoomEnded;

        internal void EndPanZoom()
        {
            PanZoomEnded?.Invoke(this, new EventArgs());  // This isn't right yet, but it's sufficient for my needs
        }

        #region Highlighting and Context Menu

        public void HighlightPatch(bool isVisible)
        {
            _isHighlightPatchVisible = isVisible;
            Invalidate(_highlightPatch);
        }

        public void HighlightPatch(int mousex, int mousey, bool isVisible)
        {
            if (_isHighlightPatchVisible)
                Invalidate(_highlightPatch);
            _isHighlightPatchVisible = isVisible;
            var x = (int)(Transform.MouseToMapX(mousex) / TerrainPatch.DefaultSize);
            _highlightPatch.Sample = x * TerrainPatch.DefaultSize;
            var y = (int)(Transform.MouseToMapY(mousey) / TerrainPatch.DefaultSize);
            _highlightPatch.Line = y * TerrainPatch.DefaultSize;
            if (_isHighlightPatchVisible)
                Invalidate(_highlightPatch);
        }

        protected void Invalidate(TerrainPatch p)
        {
            var r = p.PixelBox(Transform);
            r.Inflate(2, 2);
            Invalidate(r);
        }

        private void InitializeContextMenu()
        {
            var cm = _contextMenu = new ContextMenu();
            var item1 = new MenuItem { Text = "Toggle Selection" };
            item1.Click += (sender, e) => ToggleSelection();
            cm.MenuItems.Add(item1);

            var item2 = new MenuItem { Text = "Describe Selection" };
            item2.Click += (sender, e) => DescribeSelection();
            cm.MenuItems.Add(item2);

            var item3 = new MenuItem { Text = "Set Shadowcaster Patch" };
            item3.Click += (sender, e) => SetShadowCasterPatch();
            cm.MenuItems.Add(item3);
        }

        public void ShowContextMenu(Point p)
        {
            _contextMenu.Show(this, p);
        }

        private void ToggleSelection()
        {
            var p = FindPatch(SelectedPatches, _highlightPatch.Line, _highlightPatch.Sample);
            if (p==null)
            {
                var p1 = _highlightPatch;
                var p2 = new TerrainPatch { Line = p1.Line, Sample = p1.Sample, Width = p1.Width, Height = p1.Height, Step = p1.Step };
                SelectedPatches.Add(p2);
            }
            else
            {
                SelectedPatches.Remove(p);
            }
            SelectedPatchesChanged?.Invoke();
        }

        private void SetShadowCasterPatch()
        {
            var p = FindPatch(FinishedPatches, _highlightPatch.Line, _highlightPatch.Sample);
            MainWindow.SetShadowCasterRenderPatch(p != null ? p : _highlightPatch.RelativeTo(0, 0));
        }

        public void SetShadowCasterPatch(Point id)
        {
            var patch = FinishedPatches.FirstOrDefault(p => p.Id.Equals(id));
            MainWindow.SetShadowCasterRenderPatch(patch);
        }

        public void SetShadowCasterPatch(Point id, Point pt)
        {
            var patch = FinishedPatches.FirstOrDefault(p => p.Id.Equals(id));
            MainWindow.SetShadowCasterRenderPatch(patch);
            MainWindow.SetShadowCasterRenderPoint(pt);
        }

        void DescribeSelection()
        {
            if (_highlightPatch== null)
            {
                Console.WriteLine(@"There is no highlighted patch");
                return;
            }
            _highlightPatch.Describe();
        }

        private TerrainPatch FindPatch(List<TerrainPatch> patchlist, int line, int sample) => patchlist.Where(p => p.Line == line && p.Sample == sample).FirstOrDefault();

        #endregion
    }

    public class MouseMessage
    {
        public int X;
        public int Y;
        public Brush Brush = DefaultBrush;
        public Font Font = DefaultFont;
        public string Message;

        public static Brush DefaultBrush = Brushes.Red;
        public static Font DefaultFont = new Font("Arial", 18, FontStyle.Regular);
    }
}
