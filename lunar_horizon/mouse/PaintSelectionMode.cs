using System;
using System.Drawing;
using System.Windows.Forms;
using lunar_horizon.views;
using lunar_horizon.terrain;
using lunar_horizon.patches;

namespace lunar_horizon.mouse
{
    public class PaintSelectionMode : MouseMode
    {
        public const int StartDragThreshold = 2;
        protected Point mouseDownPoint;
        protected PointF mapDownPoint;
        protected bool MouseDown;
        protected bool isPainting;

        public override void OnMouseDown(Object sender, MouseEventArgs e)
        {
            var map = sender as MapView;
            mouseDownPoint = e.Location;
            mapDownPoint = map.TransformMouse(e.Location);
            map.Select();
            MouseDown = true;
        }

        public override void OnMouseMove(object sender, MouseEventArgs e)
        {
            var map = sender as MapView;

            var maploc = map.Transform.MouseToMap(e.Location);
            InMemoryTerrainManager.GetLatLon(maploc.Y, maploc.X, out double lat, out double lon);
            ShowStatus($"{maploc} lat={lat * 180f / 3.141592653f:F4} lon={lon * 180f / 3.141592653f:F4}");

            if (!MouseDown)
                return;
            if (!isPainting && PointDistance(mouseDownPoint, e.Location) > StartDragThreshold / map.Transform.Scale)
                isPainting = true;

            // Paint
            var x = (int)(map.Transform.MouseToMapX(e.X) / TerrainPatch.DefaultSize);
            var sample = x * TerrainPatch.DefaultSize;
            var y = (int)(map.Transform.MouseToMapY(e.Y) / TerrainPatch.DefaultSize);
            var line = y * TerrainPatch.DefaultSize;

            if (!(map.SelectedPatches.Exists(p => p.Line == line && p.Sample == sample)))
            {
                map.SelectedPatches.Add(new TerrainPatch { Line = line, Sample = sample, Width = TerrainPatch.DefaultSize, Height = TerrainPatch.DefaultSize, Step = 1 });
                map.Invalidate();
            }
        }

        public override void OnMouseUp(object sender, MouseEventArgs e)
        {
            MouseDown = false;
            isPainting = false;
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

    public class RemoveSelectionMode : MouseMode
    {
        public const int StartDragThreshold = 2;
        protected Point mouseDownPoint;
        protected PointF mapDownPoint;
        protected bool MouseDown;
        protected bool isPainting;

        public override void OnMouseDown(Object sender, MouseEventArgs e)
        {
            var map = sender as MapView;
            mouseDownPoint = e.Location;
            mapDownPoint = map.TransformMouse(e.Location);
            map.Select();
            MouseDown = true;
        }

        public override void OnMouseMove(object sender, MouseEventArgs e)
        {
            var map = sender as MapView;

            var maploc = map.Transform.MouseToMap(e.Location);
            InMemoryTerrainManager.GetLatLon(maploc.Y, maploc.X, out double lat, out double lon);
            ShowStatus($"{maploc} lat={lat * 180f / 3.141592653f:F4} lon={lon * 180f / 3.141592653f:F4}");

            if (!MouseDown)
                return;
            if (!isPainting && PointDistance(mouseDownPoint, e.Location) > StartDragThreshold / map.Transform.Scale)
                isPainting = true;

            // Paint
            var x = (int)(map.Transform.MouseToMapX(e.X) / TerrainPatch.DefaultSize);
            var sample = x * TerrainPatch.DefaultSize;
            var y = (int)(map.Transform.MouseToMapY(e.Y) / TerrainPatch.DefaultSize);
            var line = y * TerrainPatch.DefaultSize;

            var selection = map.SelectedPatches.FindIndex(p => p.Line == line && p.Sample == sample);
            if (selection >= 0)
            {
                map.SelectedPatches.RemoveAt(selection);
                map.Invalidate();
            }
        }

        public override void OnMouseUp(object sender, MouseEventArgs e)
        {
            MouseDown = false;
            isPainting = false;
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
