using lunar_horizon.patches;
using System;
using System.Drawing;
using System.Windows.Forms;
using lunar_horizon.math;
using lunar_horizon.terrain;
using lunar_horizon.tiles;
using System.Drawing.Imaging;
using System.Linq;

namespace lunar_horizon.view
{
    public partial class ShadowCasterWindow : Form
    {
        ShadowCasterPanel pnlRender;

        public ShadowCasterWindow()
        {
            InitializeComponent();
            pnlRender = new ShadowCasterPanel { Dock = DockStyle.Fill };
            Controls.Add(pnlRender);
        }

        public void SetPatch(TerrainPatch p)
        {
            pnlRender.Patch = p;
            pnlRender.Invalidate();
        }

        public void SetPoint(Point p)
        {
            pnlRender.PointInPatch = p;
            pnlRender.Invalidate();
        }

        public void UpdateShadowCaster(Vector3d c)
        {
            pnlRender.ShadowCaster = c;
            pnlRender.Invalidate();
        }
    }

    public class ShadowCasterPanel : Panel
    {
        public InMemoryTerrainManager Terrain => TileTreeNode.Terrain;
        internal TerrainPatch Patch;
        internal Point PointInPatch;
        internal Vector3d ShadowCaster;
        Brush _grayBrush = new SolidBrush(Color.FromArgb(100, 100, 100, 100));
        Bitmap _bitmap;

        public ShadowCasterPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
        }

        bool ReadyToRender() => Patch != null && PointInPatch.Y >= 0 && PointInPatch.Y < TerrainPatch.DefaultSize && PointInPatch.X >= 0 && PointInPatch.X < TerrainPatch.DefaultSize;


        protected override void OnPaint(PaintEventArgs e)
        {
            if (!ReadyToRender())
                return;
            if (_bitmap == null || _bitmap.Width != Width || _bitmap.Height != Height)
            {
                _bitmap?.Dispose();
                _bitmap =  new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            }

            Patch.FillPointsAndMatrices(Terrain);
            var m = Patch.Matrices[PointInPatch.Y][PointInPatch.X];
            var temp = new Vector3d();
            TerrainPatch.Transform(ref ShadowCaster, ref m, ref temp);

            var x = temp[0];
            var y = temp[1];
            var z = temp[2];
            var azimuth_rad = Math.Atan2(y, x) + Math.PI;  // [0,2PI]
            var alen = Math.Sqrt(x * x + y * y);
            var slope = z / alen;

            var elevation_deg = ((float)Math.Atan(slope)) * 180f / 3.141592653589f;

            var normalized_azimuth = azimuth_rad / (2d * Math.PI);  // range [0,1)
            var horizon_index_f = (float)(Horizon.HorizonSamplesD * normalized_azimuth);

            var buf = Patch.Horizons[PointInPatch.Y][PointInPatch.X].Buffer;

            var width = _bitmap.Width;
            var height = _bitmap.Height;

            var sunCenter = new Point(width / 2, height / 2);
            var sunRadius = 8; //  was 20

            // The width of a single slat in pixels
            const float sun_width_deg = .25f;
            var slat_width = sunRadius;  // .25 deg
            var slat_width2 = slat_width / 2;

            // The index of the slat that 'covers' the sun's center
            var index_center = (int)horizon_index_f;
            var center_slat_left = (int)((sunCenter.X - (horizon_index_f - index_center) * sunRadius) + 0.5f);

            //Console.WriteLine($"caster render idx={index_center} horizon_elev={LookupHorizon(buf, index_center)} sun_elev={elevation_deg}");

            var span = (width / 2) / sunRadius + 1;
            var span2 = 2 * span;

            using (var g = Graphics.FromImage(_bitmap))
            {
                g.FillRectangle(Brushes.White, 0, 0, _bitmap.Width, _bitmap.Height);
                g.FillEllipse(Brushes.Yellow, sunCenter.X - sunRadius, sunCenter.Y - sunRadius, sunRadius * 2, sunRadius * 2);

                for (var slat_offset = -span; slat_offset <= span2; slat_offset++)
                {
                    var slat_left = center_slat_left + slat_offset * slat_width;
                    var slat_index = index_center + slat_offset;
                    var slat_angle1 = LookupHorizon(buf, slat_index);
                    var slat_angle2 = LookupHorizon(buf, slat_index - 1);
                    var slat_top1 = sunCenter.Y + ((elevation_deg - slat_angle1) / sun_width_deg) * sunRadius;
                    var slat_top2 = sunCenter.Y + ((elevation_deg - slat_angle2) / sun_width_deg) * sunRadius;

                    //Console.WriteLine($"  slat_top={slat_top1} slat_angle={slat_angle1}");
                    var top = Math.Max(0, slat_top1);

                    g.FillRectangle(_grayBrush, slat_left, top, slat_width, height);
                    g.DrawRectangle(Pens.DarkGray, slat_left, top, slat_width, height);

                    g.DrawLine(Pens.Red, slat_left - slat_width2, slat_top2, slat_left + slat_width2, slat_top1);
                }

                var azimuth_deg = azimuth_rad * 180d / Math.PI;
                var frac = SunFraction2(g, sunCenter, sunRadius, buf, (float)azimuth_deg, elevation_deg);
                Console.WriteLine($"sun frac={frac}");

            }
            e.Graphics.DrawImage(_bitmap, 0, 0);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Invalidate();
        }

        float LookupHorizon(float[] buf, int index)
        {
            var rem = index - buf.Length * (index / buf.Length);
            if (rem < 0) rem += buf.Length;
            return buf[rem];
        }

        #region Model of what Horizon does

        public const int HorizonSamples = 360 * 4;
        public const float HorizonSamplesF = 360 * 4f;

        const float SunHalfAngle_deg = 0.54f / 2f;
        const int half_circle_ticks = 8;
        static float[] half_circle = PremultiplyHalfCircle(MakeHalfCircle());
        static float max_photons = 2f * PremultiplyHalfCircle(MakeHalfCircle()).Sum();

        static float[] MakeHalfCircle() => Enumerable.Range(0, half_circle_ticks * 2).Select(i => (float)(Math.Sqrt(64d - (half_circle_ticks - 0.5d - i) * (half_circle_ticks - 0.5d - i)) / (half_circle_ticks))).ToArray();
        static float[] PremultiplyHalfCircle(float[] h) => h.Select(v => v * SunHalfAngle_deg).ToArray();

        public float SunFraction2(Graphics g, Point sunCenter, int sunRadius, float[] _buffer, float az_deg, float el_deg)
        {

            const float BucketWidth_deg = 360f / HorizonSamplesF;
            const float BucketHalfWidth_deg = BucketWidth_deg / 2f;  // Used because we're interpolating between two buckets.

            const float frac_step_per_half_circle_index = SunHalfAngle_deg / BucketWidth_deg / half_circle_ticks;

            var sun_left_deg = az_deg - SunHalfAngle_deg;   // 
            var sun_left_bucket_float = sun_left_deg * (HorizonSamplesF / 360f);
            var sun_left_bucket = (int)sun_left_bucket_float; // [0,1440)
            var frac = sun_left_bucket_float - sun_left_bucket;
            if (sun_left_bucket < 0) sun_left_bucket += HorizonSamples;  // [0,1440)

            // We're going to interpolate the horizon between two buckets.  sun_left_bucket is the index
            // of the left most of the two buckets for the first interpolation.  frac is the fraction of the
            // way that the sun's left edge between these two buckets

            var left_bucket_index = sun_left_bucket;
            var right_bucket_index = left_bucket_index + 1;
            if (right_bucket_index >= HorizonSamples) right_bucket_index -= HorizonSamples;

            var left_bucket_elevation_deg = _buffer[left_bucket_index];
            var right_bucket_elevation_deg = _buffer[right_bucket_index];
            var bucket_delta_deg = right_bucket_elevation_deg - left_bucket_elevation_deg;

            var photons = 0f;

            for (var i = 0; i < half_circle.Length; i++)
            {
                var horizon_elevation_deg = frac * bucket_delta_deg + left_bucket_elevation_deg;
                var sun_column_deg = half_circle[i];    // This is now pre-multiplied * SunHalfAngle_deg;
                var sun_top_deg = el_deg + sun_column_deg;

                if (horizon_elevation_deg >= sun_top_deg)
                    goto continue_loop;

                var angle_delta = sun_top_deg - horizon_elevation_deg;
                var sun_column_deg2 = sun_column_deg + sun_column_deg;  // Can't have more than this much light
                if (angle_delta > sun_column_deg2)
                    angle_delta = sun_column_deg2;

                photons += angle_delta;

                {
                    var sun_left_side_pixel = sunCenter.X - sunRadius;
                    var line_top_y = sunCenter.Y - ((sun_column_deg / SunHalfAngle_deg) * sunRadius);
                    var line_height = (angle_delta / SunHalfAngle_deg) * sunRadius;
                    var line_azimuth = (left_bucket_index + frac) * 0.25f;
                    var line_pixel_x = (((line_azimuth - az_deg) / SunHalfAngle_deg) * sunRadius) + sunCenter.X;
                    g.DrawLine(Pens.Orange, line_pixel_x, line_top_y, line_pixel_x, line_top_y + line_height);
                }

                continue_loop:

                frac += frac_step_per_half_circle_index;
                if (frac < 1f)
                    continue;

                // Move the bucket
                left_bucket_index = right_bucket_index;
                right_bucket_index = left_bucket_index + 1;
                if (right_bucket_index >= HorizonSamples) right_bucket_index -= HorizonSamples;

                left_bucket_elevation_deg = right_bucket_elevation_deg;
                right_bucket_elevation_deg = _buffer[right_bucket_index];
                bucket_delta_deg = right_bucket_elevation_deg - left_bucket_elevation_deg;

                frac -= 1f;
            }

            var sun_fraction = photons / max_photons;

            return sun_fraction;

        }
        #endregion
    }
}
