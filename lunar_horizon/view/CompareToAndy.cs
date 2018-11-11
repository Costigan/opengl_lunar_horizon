using lunar_horizon.patches;
using lunar_horizon.terrain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace lunar_horizon.view
{
    public partial class CompareToAndy : Form
    {
        public const string HermiteASunDirectory = @"C:\RP\maps\processed_20m\hermitea\Sun";
        public const string HermiteATileDirectory = @"C:\RP\tiles\np\horizons";
        public const string HermiateACanonicalImage = @"C:\RP\maps\processed_20m\hermitea\Other\canonical.tif";

        public InMemoryTerrainManager Terrain;

        Dictionary<Point, TerrainPatch> _idToPatch = new Dictionary<Point, TerrainPatch>();

        public double[] AffineTransform;
        OSGeo.OSR.CoordinateTransformation pixelToLatLon;
        OSGeo.OSR.CoordinateTransformation latLonToPixel;

        string _sunDirectory;
        string _tileDirectory;
        string[] _sunFilenames = new string[0];

        Bitmap _bitmap;

        public CompareToAndy()
        {
            InitializeComponent();
        }

        private void hermiteASunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _idToPatch = new Dictionary<Point, TerrainPatch>();
            _sunDirectory = HermiteASunDirectory;
            _tileDirectory = HermiteATileDirectory;
            _sunFilenames = Directory.GetFiles(_sunDirectory);
            tbImageIndex.Maximum = _sunFilenames.Length;
            tbImageIndex.Minimum = 0;

            LoadAffineTransform(HermiateACanonicalImage);
        }

        public static DateTime DateFromFilename(string name)
        {
            string n = Path.GetFileNameWithoutExtension(name);
            n = Path.GetFileNameWithoutExtension(n);
            n = Path.GetFileNameWithoutExtension(n);
            string ext = Path.GetExtension(n);
            return DateTime.ParseExact(ext, ".yyyy-MM-ddTHH-mm-ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeUniversal);
        }

        private void tbImageIndex_ValueChanged(object sender, EventArgs e)
        {
            GenerateComparison(tbImageIndex.Value);
        }

        #region Generate image comparison

        void GenerateComparison(int index)
        {
            if (index < 0 || index >= _sunFilenames.Length)
                return;

            Bitmap bitmap = null;

            if (andyOnlyToolStripMenuItem.Checked)
            {
                bitmap = GetAndyBitmap(index);
            }
            else if (markOnlyToolStripMenuItem.Checked)
            {
                bitmap = GetMarkBitmap(index, GetAndyBitmap(index));
            }
            else if (comparisonToolStripMenuItem.Checked)
            {

            }

            pbImage.BackgroundImage = bitmap;
            pbImage.Size = bitmap == null ? new Size(100, 100) : bitmap.Size;
        }

        Bitmap GetAndyBitmap(int index)
        {
            var sunFilename = _sunFilenames[index];
            var time = DateFromFilename(sunFilename);
            var sunBitmap = Image.FromFile(Path.Combine(_sunDirectory, sunFilename));
            return sunBitmap as Bitmap;
        }

        DateTime TimeAtIndex(int index) => index < 0 || index >= _sunFilenames.Length ? new DateTime() : DateFromFilename(_sunFilenames[index]);

        unsafe Bitmap GetMarkBitmap(int index, Bitmap andyBitmap)
        {
            var time = TimeAtIndex(index);
            _bitmap = UpdateBitmapSize(_bitmap, andyBitmap.Size);
            var width = _bitmap.Width;
            var height = _bitmap.Height;
            var bmpData = _bitmap.LockBits(new Rectangle(0, 0, width, height), System.Drawing.Imaging.ImageLockMode.WriteOnly, _bitmap.PixelFormat);
            for (var row = 0;row<height;row++)
            {
                var rowptr = (byte*)(bmpData.Scan0 + row * bmpData.Stride);
                for (var col=0;col<width;col++)
                {
                    PixelToLatLon(col, row, out double lat, out double lon);
                    InMemoryTerrainManager.GetLineSampleD(lat,lon, out double line, out double sample);

                    var line1 = (int)line;
                    var sample1 = (int)sample;

                    // WORK HERE
                }

            }

            _bitmap.UnlockBits(bmpData);
            return null;
        }

        Bitmap UpdateBitmapSize(Bitmap a, Size size)
        {
            if (a != null && a.Size.Equals(size))
                return a;
            var b = a == null ? new Bitmap(size.Width, size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb) : new Bitmap(a, size);
            a.Dispose();
            return b;
        }

        #endregion

        #region gdal

        public void LoadAffineTransform(string filename)
        {
            using (OSGeo.GDAL.Dataset dataset = OSGeo.GDAL.Gdal.Open(filename, OSGeo.GDAL.Access.GA_ReadOnly))
            {
                AffineTransform = new double[6];
                dataset.GetGeoTransform(AffineTransform);

                string targetProjection = "GEOGCS[\"Moon 2000\", DATUM[\"D_Moon_2000\", SPHEROID[\"Moon_2000_IAU_IAG\",1737400.0,0.0]], PRIMEM[\"Greenwich\",0], UNIT[\"Decimal_Degree\",0.0174532925199433]]";

                var src = new OSGeo.OSR.SpatialReference(dataset.GetProjectionRef());
                var dst = new OSGeo.OSR.SpatialReference(targetProjection);

                pixelToLatLon = new OSGeo.OSR.CoordinateTransformation(src, dst);
                latLonToPixel = new OSGeo.OSR.CoordinateTransformation(dst, src);
            }
        }

        public void PixelToLatLon(double pixelX, double pixelY, out double lat, out double lon)
        {
            if (pixelToLatLon == null || AffineTransform == null)
                throw new Exception(@"No coordinate transform has been provided to go from pixels to lat/lon");

            double x = pixelX * AffineTransform[1] + AffineTransform[0];
            double y = pixelY * AffineTransform[5] + AffineTransform[3];

            var o = new double[3];
            pixelToLatLon.TransformPoint(o, x, y, 0d);
            lat = o[1];
            lon = o[0];
        }

        public void LatLonToPixel(double lat, double lon, out float col, out float row)
        {
            var o = new double[3];
            latLonToPixel.TransformPoint(o, lon, lat, 0d);
            col = (float)((o[0] - AffineTransform[0]) / AffineTransform[1]);
            row = (float)((o[1] - AffineTransform[3]) / AffineTransform[5]);
        }

        #endregion

        private void andyOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            andyOnlyToolStripMenuItem.Checked = true;
            markOnlyToolStripMenuItem.Checked = false;
            comparisonToolStripMenuItem.Checked = false;
            GenerateComparison(tbImageIndex.Value);
        }

        private void markOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            andyOnlyToolStripMenuItem.Checked = false;
            markOnlyToolStripMenuItem.Checked = true;
            comparisonToolStripMenuItem.Checked = false;
            GenerateComparison(tbImageIndex.Value);
        }

        private void comparisonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            andyOnlyToolStripMenuItem.Checked = false;
            markOnlyToolStripMenuItem.Checked = false;
            comparisonToolStripMenuItem.Checked = true;
            GenerateComparison(tbImageIndex.Value);
        }
    }
}
