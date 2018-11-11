using lunar_horizon.patches;
using lunar_horizon.views;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace lunar_horizon.mouse
{
    public class DragRenderPoint : MouseMode
    {
        protected bool MouseDown;

        public override void OnMouseDown(Object sender, MouseEventArgs e)
        {
            var map = sender as MapView;
            MouseDown = true;
            map.Select();
        }

        public override void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!MouseDown)
                return;
            var map = sender as MapView;
            var maploc = map.Transform.MouseToMap(e.Location);
            var locPoint = new Point((int)maploc.X, (int)maploc.Y);
            var id = new Point(locPoint.X / TerrainPatch.DefaultSize, locPoint.Y / TerrainPatch.DefaultSize);
            var pointInPatch = new Point(locPoint.X - id.X * TerrainPatch.DefaultSize, locPoint.Y - id.Y * TerrainPatch.DefaultSize);
            map.SetShadowCasterPatch(id, pointInPatch);
        }

        public override void OnMouseUp(object sender, MouseEventArgs e)
        {
            MouseDown = false;
        }

        public override void OnMouseWheel(object sender, MouseEventArgs e)
        {
            const float zoom = 1.1f;
            var map = sender as MapView;
            float factor = e.Delta < 0 ? zoom : 1f / zoom;

            MapView.DisplayTransform t = map.Transform;

            PointF mapLocation = t.MouseToMap(e.Location);
            var mouse2 = t[mapLocation];
            t.Scale *= factor;
            PointF newLocation = t[mapLocation];

            t.OffsetX += (int)(e.Location.X - newLocation.X);
            t.OffsetY += (int)(e.Location.Y - newLocation.Y);

            map.Transform = t;
        }
    }
}
