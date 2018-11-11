using lunar_horizon.patches;
using lunar_horizon.views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace lunar_horizon.mouse
{
    public class HilightSurroundingPatches : MouseMode
    {
        public override void OnMouseDown(Object sender, MouseEventArgs e)
        {
            var map = sender as MapView;
            if (map == null) return;
            var maploc = map.Transform.MouseToMap(e.Location);
            var x = (int)(map.Transform.MouseToMapX(e.X) / TerrainPatch.DefaultSize);
            var y = (int)(map.Transform.MouseToMapY(e.Y) / TerrainPatch.DefaultSize);
            var id = new Point(x, y);
            var patch = TerrainPatch.FromId(id);
            map.ProcessingPatches = new List<TerrainPatch> { patch };
            var surrounding = patch.SurroundingPatches(1).Concat(patch.SurroundingPatches(3)).ToList();
            map.SelectedPatches = surrounding;
            map.Invalidate();
        }
    }
}
