using lunar_horizon.patches;
using lunar_horizon.utilities;
using lunar_horizon.view;
using lunar_horizon.views;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lunar_horizon.calc
{
    public class LightCurveImage : IMapLayer
    {
        protected static int Count = 0;

        public Bitmap Image;

        public DateTime Start;
        public DateTime Stop;
        public TimeSpan Step = new TimeSpan(1, 0, 0);

        public Rectangle Bounds;  // Bounds in DEM pixel coordinates
        public LightCurveApprox Generator;

        public LightCurveImage() => Name = $"light-curve-image-{++Count}";

        protected float _Transparency = 1f;
        protected readonly ImageAttributes _imageAttributes = new ImageAttributes();  // Holds the image attributes so it can be reused
        protected ImageAttributes _drawAttributes = null;  // Either points at _imageAttributes or null
        protected readonly ColorMatrix _colorMatrix = new ColorMatrix();
        public float Transparency
        {
            get { return _Transparency; }
            set
            {
                _Transparency = value;
                if (_Transparency < 0f) _Transparency = 0f;
                if (_Transparency > 1f) _Transparency = 1f;
                if (_Transparency == 1f)
                    _drawAttributes = null;
                else
                {
                    _colorMatrix.Matrix33 = _Transparency;
                    _imageAttributes.SetColorMatrix(_colorMatrix);
                    _drawAttributes = _imageAttributes;
                }
            }
        }

        public string Name { get; set; }

        public virtual void Load() { }

        #region Utility

        int RoundDown(int i, int mod) => mod * (i / mod);
        Point GetID(int x, int y) => new Point(x / TerrainPatch.DefaultSize, y / TerrainPatch.DefaultSize);
        Point OffsetInPatch(int x, int y) => new Point(x - RoundDown(x, TerrainPatch.DefaultSize), y - RoundDown(y, TerrainPatch.DefaultSize));

        protected Rectangle RoundRectangle(Rectangle a)
        {
            var m = TerrainPatch.DefaultSize;
            return new Rectangle(
                m * (int)Math.Round(a.X / (float)m),
                m * (int)Math.Round(a.Y / (float)m),
                m * (int)Math.Round(a.Width / (float)m),
                m * (int)Math.Round(a.Height / (float)m));
        }

        public virtual void Draw(Graphics g, MapView.DisplayTransform t, ImageAttributes attributes = null)
        {
            var r = t[Bounds];
            g.DrawRectangle(Pens.Blue, r.Left, r.Top, r.Width, r.Height);
        }

        public virtual UserControl GetPropertySheet() => null;

        public void Selected(MapView view, bool isSelected) { }
        public void Checked(MapView view, bool isChecked) { }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Image?.Dispose();
                    Image = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~LightCurveImage() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

        #endregion
    }

    public class AverageSunImage : LightCurveImage
    {
        public override void Load() => GenerateAverageSunImage();

        private unsafe void GenerateAverageSunImage()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Debug.Assert(Generator != null);
            var width = Bounds.Width;
            var height = Bounds.Height;

            if (Image != null && Image.Size != Bounds.Size)
            {
                Image.Dispose();
                Image = null;
            }
            if (Image == null)
                Image = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            var bmpData = Image.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, Image.PixelFormat);
            var options = new ParallelOptions { MaxDegreeOfParallelism = 6 };
            Parallel.For(0, height, options, y =>
            {
                float sum = 0f;
                Action<DateTime, float> action = (time, sun) => sum += sun;
                var rowptr = (int*)(bmpData.Scan0 + y * bmpData.Stride);
                for (var x = 0; x < width; x++)
                {
                    sum = 0f;
                    Generator.MapOverLightCurve(y + Bounds.Top, x + Bounds.Left, Start, Stop, Step, action);
                    var totalSunHours = sum * Step.TotalHours;
                    var maxSunHours = (Stop - Start).TotalHours;
                    var frac = (int)(totalSunHours / maxSunHours * 255d);
                    rowptr[x] = Color.FromArgb(255, frac, frac, frac).ToArgb();
                }
            });
            Image.UnlockBits(bmpData);
            stopwatch.Stop();
            Console.WriteLine($"Generated light curve image in {stopwatch.Elapsed}");
        }

        public override void Draw(Graphics g, MapView.DisplayTransform t, ImageAttributes attributes = null)
        {
            var r = t[Bounds];
            if (Image!= null)
            {
                if (attributes == null)
                    g.DrawImage(Image, r);
                else
                {
                    var r1 = new Rectangle((int)r.X, (int)r.Y, (int)r.Width, (int)r.Height);
                    g.DrawImage(Image, r1, 0, 0, Image.Width, Image.Height, GraphicsUnit.Pixel, attributes);
                }
            }
            else
            g.DrawRectangle(Pens.Blue, r.Left, r.Top, r.Width, r.Height);
        }
    }
}
