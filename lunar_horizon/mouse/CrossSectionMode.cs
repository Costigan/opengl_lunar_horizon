using System.Drawing;
using System.Windows.Forms;
using lunar_horizon.math;
using lunar_horizon.views;
using lunar_horizon.terrain;
using ZedGraph;

namespace lunar_horizon.mouse
{
    public class CrossSectionMode : RulerMeasureMode
    {
        protected override void StartDragging(MapView map, PointF maploc)
        {
            base.StartDragging(map, maploc);
            LunarHorizon.Singleton.ShowRenderPlot(true);
        }

        public override void OnMouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(sender, e);
            var map = sender as MapView;
            if (MouseDragging && map.RulerMeasureIsVisible)
                FillCrossSection();
        }

        private void FillCrossSection()
        {
            const float steps = 100f;
            var map = LunarHorizon.Singleton.MapView;
            var ruler = map.RulerMeasure;
            var p1 = ruler.Location;
            var p2 = new PointF(p1.X + ruler.Width, p1.Y + ruler.Height);
            var terrain = LunarHorizon.Terrain;
            var result = new PointPairList();
            var delta = p2.Minus(p1);
            var low = 0f;
            var high = ((float)InMemoryTerrainManager.Samples) - 1f;
            for (var i = 0f; i <= steps; i += 1f)
            {
                var p = p1.Plus(delta.Times(i / steps));
                var x = p.X.Clamp(low, high);
                var y = p.Y.Clamp(low, high);
                var elevation = terrain.InterpolatedElevation(y, x);
                result.Add(new PointPair(i, elevation));
            }
            LunarHorizon.Singleton.RenderElevation(result);
        }
    }
}
