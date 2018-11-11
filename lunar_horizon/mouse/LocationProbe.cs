using lunar_horizon.view;
using lunar_horizon.views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lunar_horizon.mouse
{
    public class LocationProbe : Pickable
    {
        public string Text = null;
        public PointF Offset = new PointF(-6, 12);

        #region Drawing

        SolidBrush _brush;
        Pen _pen;
        [Browsable(false)]
        [JsonIgnore]
        public SolidBrush Brush => _brush != null ? _brush : _brush = new SolidBrush(Color);
        [Browsable(false)]
        [JsonIgnore]
        public Pen Pen => _pen != null ? _pen : _pen = new Pen(Color) { Width = 1 };

        public Color Color = Color.Red;

        public override void Paint(PaintEventArgs e, PickablePanel p)
        {
            var m = p as MapView;
            Debug.Assert(p != null);
            MapView.DisplayTransform t = m.Transform;
            Graphics g = e.Graphics;
            float x = t.X(Location.X);
            float y = t.Y(Location.Y);
            g.FillRectangle(Brush, x - 1, y - 1, 3, 3);
            g.DrawLine(Pen, x, y, x + Offset.X, y + Offset.Y);
            if (Text != null)
                g.DrawString(Text, DrawingHelper.SecondsFont, Brush, x, y + 12);
        }

        #endregion

        public override PointF Location
        {
            get { return _location; }
            set
            {
                _location = value;
                Bounds = new RectangleF(_location.X - _spread, _location.Y - _spread, _spread + _spread + 1, +_spread + _spread + 1);
            }
        }

        public override void Dragging(PickablePanel panel)
        {
            LocationProbeMapLayer.DefaultPropertySheet.Dragging(this);
        }

        public override void StopDragging(PickablePanel panel)
        {
            LocationProbeMapLayer.DefaultPropertySheet.StopDragging(this);
        }
    }
}
