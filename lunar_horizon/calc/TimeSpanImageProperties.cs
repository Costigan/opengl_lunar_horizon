using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using lunar_horizon.view;
using ZedGraph;

namespace lunar_horizon.calc
{
    public interface IMapLayerPropertySheet
    {
        IMapLayer Layer { get; set; }
    }

    public partial class TimeSpanImageProperties : UserControl, IMapLayerPropertySheet
    {
        public float LowHours = 10f;
        public float HighHours = 500f;

        protected IMapLayer _layer;

        public TimeSpanImageProperties()
        {
            InitializeComponent();
        }

        public IMapLayer Layer {
            get { return _layer; }
            set
            {
                _layer = value;
                lblName.Text = _layer.Name;
            }
        }

        private void tbMinHours_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(tbMinHours.Text, out LowHours))
                tbMinHours.ForeColor = Color.Black;
            else
                tbMinHours.ForeColor = Color.Red;
        }

        private void tbMaxHours_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(tbMaxHours.Text, out HighHours))
                tbMaxHours.ForeColor = Color.Black;
            else
                tbMaxHours.ForeColor = Color.Red;
        }

        private void btnHistogram_Click(object sender, EventArgs e)
        {
            if (Layer == null)
                return;
            if (!(Layer is TimeSpanImage image))
                return;
            var dataset = image.Dataset;
            var data = dataset.Data;

            var rect = LunarHorizon.Singleton.MapView.SelectionRectangle;
            var do_filter = LunarHorizon.Singleton.MapView.SelectionRectangleIsVisible;

            const int bucket_count = 40;
            var buckets = new int[bucket_count];
            var low_ticks = TimeSpan.FromHours(LowHours).Ticks;
            var high_ticks = TimeSpan.FromHours(HighHours).Ticks;
            var factor = (high_ticks - low_ticks) / ((double)bucket_count);
            if (do_filter)
            {
                var rect_left = (int)Math.Floor(rect.X);
                var rect_top = (int)Math.Floor(rect.Y);
                var filter_rect_in_dataset_coords = new Rectangle(rect_left - dataset.Bounds.X, rect_top - dataset.Bounds.Y, (int)Math.Floor(rect.Right) - rect_left, (int)Math.Floor(rect.Bottom) - rect_top);
                var bounds_in_dataset_coords = new Rectangle(0, 0, dataset.Bounds.Width, dataset.Bounds.Height);
                var intersection = Rectangle.Intersect(bounds_in_dataset_coords, filter_rect_in_dataset_coords);
                var left = intersection.Left;
                var top = intersection.Top;
                var right = intersection.Right;
                var bottom = intersection.Bottom;
                for (var y = top; y < bottom; y++)
                    for (var x = left; x < right; x++)
                    {
                        var ticks = data[y, x];
                        if (low_ticks <= ticks && ticks <= high_ticks)
                        {
                            var bucket = ticks == high_ticks ? bucket_count - 1 : (int)((ticks - low_ticks) / factor);
                            buckets[bucket]++;
                        }
                    }                        
            }
            else
            {
                foreach (var ticks in dataset.Enumerate())
                    if (low_ticks <= ticks && ticks <= high_ticks)
                    {
                        var bucket = ticks == high_ticks ? bucket_count - 1 : (int)((ticks - low_ticks) / factor);
                        buckets[bucket]++;
                    }
            }

            var points = new PointPairList();
            for (var i = 0; i < bucket_count; i++)
                points.Add(LowHours+(HighHours-LowHours)*(i/(double)bucket_count), buckets[i]);
            var curve = new BarItem("pixel count", points, Color.Black);
            LunarHorizon.Singleton.RenderPlot(curve, "hours", "pixels");

            Console.WriteLine($"There were {buckets.Sum()} pixels found");
        }

        private void btnGenerateImage_Click(object sender, EventArgs e)
        {
            if (Layer == null)
                return;
            if (!(Layer is TimeSpanImage image))
                return;
            image.Load(image.Dataset, LowHours, HighHours);
            LunarHorizon.Singleton.MapView.Invalidate();
        }
    }
}
