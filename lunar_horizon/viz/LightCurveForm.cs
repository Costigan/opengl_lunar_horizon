using lunar_horizon.calc;
using System;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace lunar_horizon.viz
{
    public partial class LightCurveForm : Form
    {
        private Point _point;
        private readonly ZedGraphControl plotControl;
        private LightCurveApprox _generator;

        public LightCurveForm()
        {
            InitializeComponent();
            plotControl = new ZedGraphControl { Dock = DockStyle.Fill };
            Controls.Add(plotControl);
            _generator = new LightCurveApprox();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdatePlot();
        }

        public Point Point
        {
            get { return _point; }
            set {
                _point = value;
                UpdatePlot();
            }
        }

        private void UpdatePlot()
        {
            var points = _generator.GetLightCurve(_point.Y, _point.X, dtStart.Value, dtStop.Value, new TimeSpan((int)udStep.Value, 0, 0));
            Console.WriteLine($"Adding {points.Count} points to the form");
            var pane = plotControl.GraphPane;
            pane.CurveList.Clear();
            pane.AddCurve(null, points, Color.Blue, SymbolType.None);
            pane.XAxis.Title.Text = "Time";
            pane.XAxis.Type = AxisType.DateAsOrdinal;
            pane.YAxis.Title.Text = "Sun Fraction";
            pane.YAxis.Type = AxisType.Linear;
            pane.Title.Text = "Sun fraction over time";
            pane.AxisChange();
            plotControl.Invalidate();
        }
    }
}
