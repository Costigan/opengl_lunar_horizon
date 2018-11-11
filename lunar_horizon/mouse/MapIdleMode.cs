using System;
using System.Drawing;
using System.Windows.Forms;
using lunar_horizon.views;
using lunar_horizon.terrain;
using lunar_horizon.view;
using lunar_horizon.calc;

namespace lunar_horizon.mouse
{
    public class MapIdleMode : MouseMode
    {
        public const int StartDragThreshold = 2;
        protected Point mouseDownPoint;
        protected PointF mapDownPoint;
        protected bool MouseDown;

        public override void OnMouseDown(Object sender, MouseEventArgs e)
        {
            var map = sender as MapView;
            mouseDownPoint = e.Location;
            mapDownPoint = map.TransformMouse(e.Location);
            Console.WriteLine($"mapDownPoint=[{mapDownPoint.X},{mapDownPoint.Y}]");
            map.Select();
            if (e.Button == MouseButtons.Right)
                map.ShowContextMenu(e.Location);
            else
                MouseDown = true;
        }

        public override void OnMouseMove(object sender, MouseEventArgs e)
        {
            var map = sender as MapView;
            //map.HighlightPatch(e.X, e.Y, true);
            var maploc = map.Transform.MouseToMap(e.Location);
            var defaultSize = patches.TerrainPatch.DefaultSize;
            var id = new Point((int)(maploc.X / defaultSize), (int)(maploc.Y / defaultSize));
            InMemoryTerrainManager.GetLatLon(maploc.Y, maploc.X, out double lat, out double lon);
            ShowStatus($"{maploc} id={id} lat={lat * 180f / 3.141592653f:F4} lon={lon * 180f / 3.141592653f:F4}");

            // Debugging
            //int line1, sample1;
            //TerrainManager.GetLineSample(lat, lon, out line1, out sample1);
            //ShowStatus($"{maploc} lat={lat * 180f / 3.141592653f:F4} lon={lon * 180f / 3.141592653f:F4}  x1={sample1} y1={line1}");

            if (!MouseDown) return;
            if (PointDistance(mouseDownPoint, e.Location) > StartDragThreshold)  //  / map.Transform.Scale
            {
                Pickable p = map.Pick(mapDownPoint);
                if (p == null)
                    map.ReplaceMouseMode(new MapPanMode(map, mouseDownPoint));
                else
                    map.ReplaceMouseMode(new DragPickableInMap { Pickable = p, DownPoint = mapDownPoint, Offset = Subtract(p.Location, mapDownPoint) });
            }
        }

        public override void OnMouseLeave(object sender, EventArgs e)
        {
            var map = sender as MapView;
            //map.HighlightPatch(false);
        }

        public override void OnMouseUp(object sender, MouseEventArgs e)
        {
            //Console.WriteLine(@"EditIdleMode>>OnMouseUp");
            MouseDown = false;
            var map = sender as MapView;
            //if (e.Button == MouseButtons.Right)
            //    t.ShowContextMenu(e);

            switch (Control.ModifierKeys)
            {
                case Keys.Control:
                    {
                    }
                    return;
                case Keys.None:
                    map.Select(map.Pick(map.TransformMouse(e.Location)));
                    break;
            }
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

        public override void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!(LunarHorizon.Singleton.GetSelectedLayer() is LocationProbeMapLayer layer)) return;
            if (!(sender is MapView map)) return;
            if (!(layer.GetPropertySheet() is LocationProbePropertySheet sheet)) return;
            var probe = sheet.NewLocationProbe();  // adds and calls invalidate
            probe.Location = map.TransformMouse(e.Location);
        }
    }
}
