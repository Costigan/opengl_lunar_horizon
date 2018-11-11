using lunar_horizon.math;
using OSGeo.GDAL;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;

namespace lunar_horizon.pds
{
    /// <summary>
    /// This treats a large IMG file like the terrain and loads it 
    /// </summary>
    public class MemoryMappedImgFile
    {
        public static string LatLonProjection = "GEOGCS[\"Moon 2000\", DATUM[\"D_Moon_2000\", SPHEROID[\"Moon_2000_IAU_IAG\",1737400.0,0.0]], PRIMEM[\"Greenwich\",0], UNIT[\"Decimal_Degree\",0.0174532925199433]]";

        public string Path { get; set; }
        public int Width { get; set; }
        public int WidthWithStride { get; set; }
        public int Height { get; set; }

        public string Projection { get; set; }
        public double[] AffineTransform = new double[6];

        public OSGeo.OSR.CoordinateTransformation PixelToLatLon;
        public OSGeo.OSR.CoordinateTransformation LatLonToPixel;

        // Conversion to lat/lon
        public const double S0 = 15199.5d;             // PDS SAMPLE_PROJECTION_OFFSET
        public const double L0 = 15199.5d;             // PDS LINE_PROJECTION_OFFSET
        public static double LonFactor = 1d;  // appropriate for south
        public const double Scale = 20d / 1000d;
        public const double LonP = 0d;
        public static double LatP;
        public const double MoonRadius = 1737.4d;
        public const double RadiusInMeters = 1737400.0d;
        public const int Samples = 30400;

        private MemoryMappedViewAccessor _accessor;
        private MemoryMappedFile _mmf;

        public MemoryMappedImgFile Open(string path)
        {
            Console.WriteLine($"Creating a memory mapped file: {path}");
            Path = path;
            _mmf = MemoryMappedFile.CreateFromFile(path, FileMode.Open, path, 0L, MemoryMappedFileAccess.Read);

            // This maps the whole file.  There is an accessor creation method
            // that maps only a section of the file, which could be used for larger
            // files, but it would need to know what the lat/lon are first.
            //_accessor = _mmf.CreateViewAccessor();
            _accessor = _mmf.CreateViewAccessor(0, 0L, MemoryMappedFileAccess.Read);
            return this;
        }

        public void Close()
        {
            Dispose(true);
        }

        public void Dispose(bool ignore)
        {
            if (_mmf == null) return;
            _mmf.Dispose();
            _mmf = null;
            _accessor = null;
        }

        public byte ReadByte(int line, int sample)
        {
            const int height = 30400;
            const int width = 30400;
            const int stride = width;

            Debug.Assert(line >= 0 && line < height && sample >= 0 && sample < width);

            int index = line * stride + sample;
            return _accessor.ReadByte(index);
        }

        public void ReadBytes(int line, int sample, byte[] buf, int count)
        {
            const int height = 30400;
            const int width = 30400;
            const int stride = width;

            Debug.Assert(line >= 0 && line < height && sample >= 0 && sample < width);

            int index = line * stride + sample;
            _accessor.ReadArray(index, buf, 0, count);
        }

        public MemoryMappedImgFile SetNorth()
        {
            LonFactor = -1d;
            LatP = Math.PI / 2;
            return this;
        }

        public MemoryMappedImgFile SetSouth()
        {
            LonFactor = 1d;
            LatP = -Math.PI / 2;
            return this;
        }

        /// <summary>
        /// Return latitude and longitude in radians
        /// </summary>
        /// <param name="line"></param>
        /// <param name="sample"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        public static void GetLatLon(double line, double sample, out double latitude, out double longitude)
        {
            const double R = MoonRadius;
            var x = (sample - S0) * Scale;
            var y = (L0 - line) * Scale;
            var P = Math.Sqrt(x * x + y * y);
            var C = 2d * Math.Atan2(P, 2 * R);
            latitude = Math.Asin(Math.Cos(C) * Math.Sin(LatP) + y * Math.Sin(C) * Math.Cos(LatP) / P);
            longitude = LonP + Math.Atan2(x, y * LonFactor);
            //Console.WriteLine($"Line={line} Sample={sample} x={x} y={y} lat={latitude} lon={longitude}");
        }

        public static void GetLatLonDegrees(double line, double sample, out double latitude, out double longitude)
        {
            GetLatLon(line, sample, out double lat, out double lon);
            latitude = lat * 180d / Math.PI;
            longitude = lon * 180d / Math.PI;
        }

        public Vector3d GetVector3d(int line, int sample, float height)
        {
            var radius = MoonRadius + height / 1000d;
            GetLatLon(line, sample, out double lat, out double lon);
            var z = radius * Math.Sin(lat);
            var c = radius * Math.Cos(lat);
            var x = c * Math.Cos(lon);  // TODO: Not sure about these
            var y = c * Math.Sin(lon);
            return new Vector3d(x, y, z);
        }

        public static void GetLineSampleDegrees(double lat_deg, double lon_deg, out int line, out int sample)
        {
            GetLineSample(lat_deg * Math.PI / 180d, lon_deg * Math.PI / 180d, out line, out sample);
        }

        public static void GetLineSample(double lat_rad, double lon_rad, out int line, out int sample)
        {
            var x = 2d * MoonRadius * Math.Tan(Math.PI / 4d - lat_rad / 2d) * Math.Sin(lon_rad - LonP);
            var y = -2d * MoonRadius * Math.Tan(Math.PI / 4d - lat_rad / 2d) * Math.Cos(lon_rad - LonP);

            //x = (Sample - S0 - 1) * Scale
            //y = (1 - L0 - Line) * Scale => Line = 1 - L0 - y/Scale
            sample = (int)(x / Scale + S0 + 1d + 0.5d);

            // line = (int)(1d - L0 - y / Scale);   // Should be this per the notes
            line = -(int)(1d - L0 - y / Scale);  //NOTE: I'm not sure why there's a - sign here

            // I don't understand this correction
            const int mid = 30400 / 2;
            line = mid - (line - mid);
        }

        public static void GetLineSampleD(double lat_rad, double lon_rad, out double line, out double sample)
        {
            var x = 2d * MoonRadius * Math.Tan(Math.PI / 4d - lat_rad / 2d) * Math.Sin(lon_rad - LonP);
            var y = -2d * MoonRadius * Math.Tan(Math.PI / 4d - lat_rad / 2d) * Math.Cos(lon_rad - LonP);

            //x = (Sample - S0 - 1) * Scale
            //y = (1 - L0 - Line) * Scale => Line = 1 - L0 - y/Scale
            sample = x / Scale + S0 + 1d + 0.5d;

            // line = (int)(1d - L0 - y / Scale);   // Should be this per the notes
            line = -(1d - L0 - y / Scale);  //NOTE: I'm not sure why there's a - sign here

            // I don't understand this correction
            const int mid = 30400 / 2;
            line = mid - (line - mid);
        }

        public void SavedCode()
        {
            using (Dataset dataset = Gdal.Open(Path, Access.GA_ReadOnly))
            {
                if (dataset == null)
                    throw new Exception($"GDAL couldn't open {Path}");
                Width = dataset.RasterXSize;
                Height = dataset.RasterYSize;
                Debug.Assert(dataset.RasterCount == 1);

                dataset.GetGeoTransform(AffineTransform);
                Projection = dataset.GetProjectionRef();

                var latLonSpatialReference = new OSGeo.OSR.SpatialReference(LatLonProjection);
                var imgSpatialReference = new OSGeo.OSR.SpatialReference(LatLonProjection);

                PixelToLatLon = new OSGeo.OSR.CoordinateTransformation(imgSpatialReference, latLonSpatialReference);
                LatLonToPixel = new OSGeo.OSR.CoordinateTransformation(latLonSpatialReference, imgSpatialReference);



            }
        }
    }
}
