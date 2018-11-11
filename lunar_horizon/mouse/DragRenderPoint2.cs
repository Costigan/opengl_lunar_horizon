using lunar_horizon.patches;
using lunar_horizon.view;
using lunar_horizon.views;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace lunar_horizon.mouse
{
    // Similar to DragRenderPoint but for the TileTester window rather than the main window
    public class DragRenderPoint2 : MouseMode
    {
        protected bool MouseDown;
        public TileTester Owner;

        public override void OnMouseDown(Object sender, MouseEventArgs e)
        {
            var map = sender as PickablePanel;
            MouseDown = true;
            map.Select();
        }

        public override void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!MouseDown)
                return;
            var locPoint = e.Location;
            var id = new Point(locPoint.X / TerrainPatch.DefaultSize, locPoint.Y / TerrainPatch.DefaultSize);
            var pointInPatch = new Point(locPoint.X - id.X * TerrainPatch.DefaultSize, locPoint.Y - id.Y * TerrainPatch.DefaultSize);
            Owner.DrawHorizon(pointInPatch);
        }

        public override void OnMouseUp(object sender, MouseEventArgs e)
        {
            MouseDown = false;
        }
    }
}
