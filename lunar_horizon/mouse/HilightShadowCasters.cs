using System;
using System.Drawing;
using System.Windows.Forms;
using lunar_horizon.views;
using lunar_horizon.patches;
using System.Linq;

namespace lunar_horizon.mouse
{
    public class HilightShadowCasters : MouseMode
    {
        public override void OnMouseDown(Object sender, MouseEventArgs e)
        {
            var map = sender as MapView;
            if (map == null) return;
            var maploc = map.Transform.MouseToMap(e.Location);
            var x = (int)(map.Transform.MouseToMapX(e.X) / TerrainPatch.DefaultSize);
            var y = (int)(map.Transform.MouseToMapY(e.Y) / TerrainPatch.DefaultSize);
            var id = new Point(x, y);
            var patch = map.FinishedPatches.FirstOrDefault(p => p.Id.Equals(id));
            if (patch == null) return;
            map.SelectedPatches = patch.ShadowCasters.Select(s => new TerrainPatch { Line = s.Y * TerrainPatch.DefaultSize, Sample = s.X * TerrainPatch.DefaultSize, Width = TerrainPatch.DefaultSize, Height = TerrainPatch.DefaultSize, Step = 1 }).ToList();
            map.Invalidate();
        }
    }
}
