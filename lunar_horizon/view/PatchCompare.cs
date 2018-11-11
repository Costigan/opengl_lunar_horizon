using lunar_horizon.math;
using lunar_horizon.patches;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using lunar_horizon.terrain;

namespace lunar_horizon.view
{
    public partial class PatchCompare : Form
    {
        static string patch_dir = @"c:\rp\tiles\np\horizons";

        internal DateTime _currentTime = DateTime.UtcNow;
        internal TimeSpan _timeSpan = new TimeSpan(3 * 28, 0, 0);

        internal Panel pnlComparison;

        internal Vector3d _sun;
        internal TerrainPatch patch1;
        internal TerrainPatch patch2;

        Bitmap imageDelta;
        Bitmap horizonDelta;

        public PatchCompare()
        {
            InitializeComponent();

            pnlComparison = new ComparisonPanel { form = this };
            pnlComparison.Size = new Size(512, 512);

            var wrapper = new ComparisonWrapper();
            wrapper.Dock = DockStyle.Fill;
            wrapper.AutoScroll = true;
            wrapper.Controls.Add(pnlComparison);

            pnlComparison.Location = new Point(0, 0);

            Controls.Add(wrapper);
            wrapper.BringToFront();

            cbTimeSpan.SelectedIndex = 0;
        }

        private void PatchCompare_Load(object sender, EventArgs e)
        {
            foreach (var filename in Directory.EnumerateFiles(patch_dir, "*_*.bin").OrderBy(s => s))
                lbPatches.Items.Add(Path.GetFileNameWithoutExtension(filename));
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (lbPatches.SelectedItems.Count != 2)
            {
                MessageBox.Show("Please select exactly two patches");
                return;
            }
            var selected = lbPatches.SelectedItems;
            patch1 = new TerrainPatch();
            patch1.Load(Path.Combine(patch_dir, (selected[0] as string)+".bin"));
            patch2 = new TerrainPatch();
            patch2.Load(Path.Combine(patch_dir, (selected[1] as string) + ".bin"));
        }

        private void cbTimeSpan_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbTimeSpan.SelectedIndex)
            {
                case 0:
                    _timeSpan = new TimeSpan(3 * 28, 0, 0, 0, 0);
                    break;
                case 1:
                    _timeSpan = new TimeSpan(2 * 28, 0, 0, 0, 0);
                    break;
                case 2:
                    _timeSpan = new TimeSpan(1 * 28, 0, 0, 0, 0);
                    break;
                case 3:
                    _timeSpan = new TimeSpan(2 * 7, 0, 0, 0, 0);
                    break;
                case 4:
                    _timeSpan = new TimeSpan(7, 0, 0, 0, 0);
                    break;
                case 5:
                    _timeSpan = new TimeSpan(1, 0, 0, 0, 0);
                    break;
                case 6:
                default:
                    _timeSpan = new TimeSpan(0, 1, 0, 0, 0);
                    break;
            }
            UpdateCurrentTime();
        }

        private void tbSelectTime_Scroll(object sender, EventArgs e)
        {
            UpdateCurrentTime();
        }

        protected void UpdateCurrentTime()
        {
            _currentTime = new DateTime(dtStartTime.Value.Ticks + (long)(_timeSpan.Ticks * (tbSelectTime.Value / (float)tbSelectTime.Maximum)));
            lbCurrentTime.Text = _currentTime.ToString();
            UpdateShadowCasters();
        }

        void UpdateShadowCasters()
        {
            _sun = LunarHorizon.SunPosition(_currentTime);
            pnlComparison.Invalidate();
        }

        private void rbFirst_Click(object sender, EventArgs e)
        {
            pnlComparison.Invalidate();
        }

        private void rbSecond_Click(object sender, EventArgs e)
        {
            pnlComparison.Invalidate();
        }

        private void rbDelta_Click(object sender, EventArgs e)
        {
            pnlComparison.Invalidate();
        }

        private void rbHorizon_Click(object sender, EventArgs e)
        {
            pnlComparison.Invalidate();
        }

        internal void pnlComparison_Paint(PatchCompare form, Graphics g)
        {
            if (rbFirst.Checked)
            {
                if (patch1 == null)
                    g.FillRectangle(Brushes.Red, 0, 0, 32000, 32000);
                else
                {
                    var bmp = patch1.GetShadows(_sun);
                    g.DrawImage(bmp, new Rectangle(new Point(0, 0), pnlComparison.Size));
                }
            }
            else if (rbSecond.Checked)
            {
                if (patch2 == null)
                    g.FillRectangle(Brushes.Red, 0, 0, 32000, 32000);
                else
                {
                    var bmp = patch2.GetShadows(_sun);
                    g.DrawImage(bmp, new Rectangle(new Point(0, 0), pnlComparison.Size));
                }
            }
            else if (rbDelta.Checked)
            {
                if (patch2 == null || patch1 == null)
                    g.FillRectangle(Brushes.Green, 0, 0, 32000, 32000);
                else
                {
                    var bmp = GetImageDelta();
                    g.DrawImage(bmp, new Rectangle(new Point(0, 0), form.pnlComparison.Size));
                }
            } else if (rbHorizon.Checked)
            {
                if (patch2 == null || patch1 == null)
                    g.FillRectangle(Brushes.Gray, 0, 0, 32000, 32000);
                else
                {
                    var bmp = GetHorizonDelta();
                    g.DrawImage(bmp, new Rectangle(new Point(0, 0), form.pnlComparison.Size));
                }
            }
        }

        Bitmap GetImageDelta()
        {
            if (patch2 == null || patch1 == null)
                return null;
            var bmp1 = patch1.GetShadows(_sun);
            var bmp2 = patch2.GetShadows(_sun);
            var width = bmp1.Width;
            var height = bmp1.Height;
            System.Diagnostics.Debug.Assert(bmp1.Size.Equals(bmp2.Size));
            System.Diagnostics.Debug.Assert(bmp1.PixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            if (imageDelta != null && !imageDelta.Size.Equals(bmp1.Size))
            {
                imageDelta.Dispose();
                imageDelta = null;
            }
            if (imageDelta == null)
            {
                imageDelta = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                var p = imageDelta.Palette;
                for (var i = 0; i < 255; i++)
                    p.Entries[i] = Color.FromArgb(i, i, i);
                imageDelta.Palette = p;
            }
            var bmp3 = imageDelta;

            var rect = new Rectangle(0, 0, width, height);
            var bmp1data = bmp1.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp1.PixelFormat);
            var bmp2data = bmp2.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp2.PixelFormat);
            var bmp3data = bmp3.LockBits(rect, System.Drawing.Imaging.ImageLockMode.WriteOnly, bmp3.PixelFormat);
            float max = -1f;
            unsafe
            {
                for (var row = 0; row < height; row++)
                {
                    var bmp1ptr = (byte*)bmp1data.Scan0 + bmp1data.Stride * row;
                    var bmp2ptr = (byte*)bmp2data.Scan0 + bmp2data.Stride * row;
                    for (var col = 0; col < width; col++)
                        max = Math.Max(max, Math.Abs(bmp1ptr[col] - bmp2ptr[col]));
                }

                for (var row = 0; row < height; row++)
                {
                    var bmp1ptr = (byte*)bmp1data.Scan0 + bmp1data.Stride * row;
                    var bmp2ptr = (byte*)bmp2data.Scan0 + bmp2data.Stride * row;
                    var bmp3ptr = (byte*)bmp3data.Scan0 + bmp3data.Stride * row;

                    for (var col = 0; col < width; col++)
                        bmp3ptr[col] = (byte)(255f*Math.Abs(bmp1ptr[col] - bmp2ptr[col])/max);
                }
            }
            bmp3.UnlockBits(bmp3data);
            bmp2.UnlockBits(bmp2data);
            bmp1.UnlockBits(bmp1data);

            lbMaxDelta.Text = Math.Round(max).ToString();

            return bmp3;
        }

        Bitmap GetHorizonDelta()
        {
            if (patch2 == null || patch1 == null)
                return null;
            var width = TerrainPatch.DefaultSize;
            var height = TerrainPatch.DefaultSize;
            if (imageDelta != null && (imageDelta.Width != width || imageDelta.Height != height))
            {
                imageDelta.Dispose();
                imageDelta = null;
            }
            if (imageDelta == null)
            {
                imageDelta = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                var p = imageDelta.Palette;
                for (var i = 0; i < 255; i++)
                    p.Entries[i] = Color.FromArgb(i, i, i);
                imageDelta.Palette = p;
            }
            var bmp3 = imageDelta;

            var diffs = new List<float>();
            for (var row = 0; row < height; row++)
                for (var col = 0; col < width; col++)
                    diffs.Add(HorizonDifference(patch1.GetHorizon(row, col), patch2.GetHorizon(row, col)));
            var max = diffs.Max();
            //max = 0.01f;
            max = max * (tbSelectTime.Value / (float)(tbSelectTime.Maximum-tbSelectTime.Minimum));

            Console.WriteLine($"diffs[0]={diffs[0]} max={max}");

            var rect = new Rectangle(0, 0, width, height);
            var bmp3data = bmp3.LockBits(rect, System.Drawing.Imaging.ImageLockMode.WriteOnly, bmp3.PixelFormat);

            unsafe
            {
                var ptr = 0;
                for (var row = 0; row < height; row++)
                {
                    var bmp3ptr = (byte*)bmp3data.Scan0 + bmp3data.Stride * row;
                    for (var col = 0; col < width; col++)
                        bmp3ptr[col] = (byte)(Math.Min(255f,255f * diffs[ptr++] / max));
                }
            }
            bmp3.UnlockBits(bmp3data);

            //lbMaxDelta.Text = Math.Round(max).ToString();
            lbMaxDelta.Text = max.ToString();

            return bmp3;
        }

        private float HorizonDifference(Horizon horizon1, Horizon horizon2)
        {
            var b1 = horizon1.Buffer;
            var b2 = horizon2.Buffer;
            System.Diagnostics.Debug.Assert(b1.Length == b2.Length);
            var sum = 0d;
            for (var i = 0; i < b1.Length; i++)
            {
                var d = b1[i] - b2[i];
                //sum += d * d;
                sum = Math.Max(sum, Math.Abs(d));
            }
            //return (float)Math.Sqrt(sum);
            return (float)sum;
        }

        private void btnUpdateStart_Click(object sender, EventArgs e)
        {
            dtStartTime.Value = _currentTime;
            tbSelectTime.Value = 0;
        }
    }

    public class ComparisonWrapper : Panel
    {
        public ComparisonWrapper()
        {
            this.DoubleBuffered = true;

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
        }
    }

    public class ComparisonPanel : Panel
    {
        internal PatchCompare form;
        public ComparisonPanel()
        {
            this.DoubleBuffered = true;

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            if (DesignMode) return;
            form.pnlComparison_Paint(form, e.Graphics);
        }


    }


}
