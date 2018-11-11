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
    public class TimeSpanImage : IMapLayer
    {
        protected static int Count = 0;
        public Bitmap Image;
        public Rectangle Bounds;  // Bounds in DEM pixel coordinates

        public string Name { get; set; }
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

        public TimeImageDataset Dataset;

        public TimeSpanImage() => Name = $"timespan-image-{++Count}";
        public TimeSpanImage(string filename)
        {
            Name = filename;
            Load(new TimeImageDataset(filename));
        }

        public virtual void Draw(Graphics g, MapView.DisplayTransform t, ImageAttributes attributes = null)
        {
            var r = t[Bounds];
            var r2 = new Rectangle((int)r.Left, (int)r.Top, (int)r.Width, (int)r.Height);  // kludge
            g.DrawRectangle(Pens.Blue, r2);
            //Image.Save("mark.png", ImageFormat.Png);
            //attributes = null;
            if (_drawAttributes == null)
                g.DrawImage(Image, r2);
            else
                g.DrawImage(Image, r2, 0, 0, Image.Width, Image.Height, GraphicsUnit.Pixel, _drawAttributes);
        }

        public virtual UserControl GetPropertySheet() => new TimeSpanImageProperties();

        public unsafe void Load(TimeImageDataset dataset, double min_hours = 100d, double max_hours = 1000d)
        {
            Bounds = dataset.Bounds;
            Dataset = dataset;
            var data = dataset.Data;
            if (Image == null || Image.Width != Bounds.Width || Image.Height != Bounds.Height)
            {
                Image?.Dispose();
                Image = new Bitmap(Bounds.Width, Bounds.Height, PixelFormat.Format32bppArgb);
            }
            var width = Bounds.Width;
            var height = Bounds.Height;

            var min_ticks = TimeSpan.FromHours(min_hours).Ticks;
            var max_ticks = TimeSpan.FromHours(max_hours).Ticks;
            var delta_hours = max_hours - min_hours;

            var bmpData = Image.LockBits(new Rectangle(0, 0, Image.Width, Image.Height), ImageLockMode.WriteOnly, Image.PixelFormat);
            for (var y = 0; y < height; y++)
            {
                var rowptr = (int*)(bmpData.Scan0 + y * bmpData.Stride);
                for (var x = 0; x < width; x++)
                {
                    var d = data[y, x];
                    if (d < min_ticks || d > max_ticks)  // Outside interesting range
                    {
                        rowptr[x] = 0;
                        continue;
                    }

                    var hours = new TimeSpan(d).TotalHours;
                    var frac = (hours - min_hours) / delta_hours;
                    var hue = frac * 240;

                    //if (frac < 0.2)
                    //    Console.WriteLine($"[{x},{y}]");

                    var base_color = Color.FromArgb(rowptr[x]);
                    var hsl = ColorUtilities.RGBtoHSB(base_color.R, base_color.G, base_color.B);
                    //hsl.Saturation = sat;
                    //hsl.Hue = sat;

                    var rgb = ColorUtilities.HSBtoRGB(hue, 1, 1);  // hsl.Brightness);
                    rowptr[x] = Color.FromArgb(rgb.Red, rgb.Green, rgb.Blue).ToArgb();
                }
            }
            Image.UnlockBits(bmpData);
            //Image.Save("lora.png", ImageFormat.Png);
        }

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
        // ~TimeSpanImage() {
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
    }
}
