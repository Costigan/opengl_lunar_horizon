using lunar_horizon.patches;
using lunar_horizon.terrain;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace lunar_horizon.view
{
    public partial class PlotWindow : Form
    {
        ZedGraphControl plotControl;
        public PlotWindow()
        {
            plotControl = new ZedGraphControl { Dock = DockStyle.Fill };
            Controls.Add(plotControl);
        }

        public void Plot(Horizon horizon) => Plot(horizon, Color.Blue);
        public void Plot(Horizon horizon, Color color)
        {
            if (horizon == null || horizon.Buffer == null)
                return;
            var pane = plotControl.GraphPane;
            //pane.CurveList.Clear();
            PointPairList points = null;
            foreach (var pt in horizon.PartialPoints())
            {
                if (pt.HasValue)
                {
                    (points ?? (points = new PointPairList())).Add(pt.Value.X, pt.Value.Y);
                }
                else if (points != null)
                {
                    pane.AddCurve(null, points, color, SymbolType.None);
                    points = null;
                }
            }
            if (points != null)
                pane.AddCurve(null, points, color, SymbolType.None);
            pane.XAxis.Title.Text = "Azimuth";
            pane.YAxis.Title.Text = "Elevation";
            pane.Title.Text = "horizon";
            pane.AxisChange();
            plotControl.Invalidate();
        }

        public void DrawHorizon(TerrainPatch a, int line = 0, int sample = 0)
        {
            Plot(a.Horizons[line][sample]);
        }
    }
}
