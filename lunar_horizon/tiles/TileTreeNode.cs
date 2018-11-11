using lunar_horizon.terrain;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace lunar_horizon.tiles
{
    public class TileTreeNode
    {
        public static InMemoryTerrainManager Terrain;

        public static views.MapView MapView;
        public static IceStabilityTileGenerator StabilityTileGenerator;
        public static object StabilityTileGeneratorLock = new object();

        //public static Func<TileTreeNode, Bitmap> Filler = HillshadeFiller;

        public const int MapWidth = 30400;
        public const int Width = 950;
        public const int Height = 950;
        public const int TopLevel = 5;
        public static int[] PowersOf2 = { 1, 2, 4, 8, 16, 32 };

        public int Stride => PowersOf2[Level];

        public int X;  // Position within the cell grid at that level
        public int Y;
        public int Level;

        // address = [level - 3 bits | Y - 5 bits | X - 5 bits]
        public static int CalculateAddress(int level, int x, int y) => x | (y << 5) | (level << 10);

        public int Address => CalculateAddress(Level, X, Y);

        internal Bitmap _bitmap;
        internal bool _isFilling;

        public TileTreeNode(int address, string directory)
        {
            X = 31 & address;
            Y = 31 & (address >> 5);
            Level = 7 & (address >> 10);
            Path = System.IO.Path.Combine(directory, Address.ToString() + ".png");
        }

        public Bitmap Bitmap
        {
            get
            {
                return _bitmap;
            }
            set
            {
                SetBitmap(value);
            }
        }

        public Bitmap GenerateBitmap(TileTreeNodeFiller filler)
        {
            if (_bitmap != null)
                return _bitmap;
            if (_isFilling)
                return null;  // This is a small window where a race could occur
            _isFilling = true;
            Task.Run(() =>
            {
                Console.WriteLine($"Filling {Address}");
                if (File.Exists(Path))
                {
                    SetBitmap((Bitmap)Image.FromFile(Path));
                    InvalidateMapView();
                }
                else if (Terrain != null)
                {
                    var bitmap = filler.Fill(this);
                    bitmap.Save(Path, ImageFormat.Png);
                    SetBitmap(bitmap);
                    _isFilling = false;
                    InvalidateMapView();
                }
            });
            return null;
        }

        void SetBitmap(Bitmap b)
        {
            if (_bitmap == b) return;
            if (_bitmap != null) _bitmap.Dispose();
            _bitmap = b;
        }

        protected void InvalidateMapView() => MapView?.Invalidate();

        public string Path;

        /* static fillers

        #region hillshade

        public static Bitmap HillshadeFiller(TileTreeNode n)
        {
            var stride = n.Stride;
            const double elevation = 30d * Math.PI / 180d;
            const double azimuth = -Math.PI / 2;
            var Zenith_rad = Math.PI / 2d - elevation;
            var Azimuth_rad = Math.PI - azimuth;  // Convert from map azimuth to hillshade azimuth

            var demFirstY = n.Y * Height * n.Stride;
            var demFirstX = n.X * Width * n.Stride;

            Console.WriteLine($"HillshadeFiller isNorth={Terrain.IsNorth} X={n.X} Y={n.Y} Stride={n.Stride} demFirstY={demFirstY} demFirstX={demFirstX}");
            var dem = new short[Height, Width];
            for (var iy = 0; iy < Height; iy++)
            {
                for (var ix = 0; ix < Width; ix++)
                    dem[iy, ix] = Terrain.Fetch(demFirstY + iy * stride, demFirstX + ix * stride);
            }

            var bitmap = n._bitmap != null ? n._bitmap : new Bitmap(Width, Height, PixelFormat.Format8bppIndexed);
            var palette = bitmap.Palette;
            for (var i = 0; i < 256; i++)
                palette.Entries[i] = Color.FromArgb(i, i, i);
            bitmap.Palette = palette;

            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            unsafe
            {
                for (var y = 0; y < Height; y++)
                {
                    var rowptr = (byte*)(bitmapData.Scan0 + y * bitmapData.Stride);
                    for (var x = 0; x < Width; x++)
                    {
                        SlopesForHillshade(x, y, dem, Width, Height, 20f * stride, out float dzdx, out float dzdy);

                        double Aspect_rad;
                        if (dzdx != 0f)
                        {
                            Aspect_rad = Math.Atan2(dzdy, -dzdx);
                            if (Aspect_rad < 0f)
                                Aspect_rad = Math.PI * 2d + Aspect_rad;
                        }
                        else
                        {
                            if (dzdy > 0f)
                                Aspect_rad = Math.PI / 2d;
                            else if (dzdy < 0)
                                Aspect_rad = 1.5d * Math.PI;
                            else
                                Aspect_rad = 0d;
                        }

                        // Should be this.  There's some inversion in the slopes (I think)
                        //const double z_factor = 1d;
                        //var Slope_rad = Math.Atan(z_factor * Math.Sqrt(dzdx * dzdx + dzdy * dzdy));

                        var Slope_rad = -Math.Atan(Math.Sqrt(dzdx * dzdx + dzdy * dzdy));
                        var Hillshade = (Math.Cos(Zenith_rad) * Math.Cos(Slope_rad)) + (Math.Sin(Zenith_rad) * Math.Sin(Slope_rad) * Math.Cos(Azimuth_rad - Aspect_rad));

                        Hillshade = Math.Max(0d, Hillshade);
                        *(rowptr++) = (byte)(255 * Hillshade);
                    }
                }
            }
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }

        internal static void SlopesForHillshade(int x, int y, short[,] dem, int width, int height, float cellsize, out float dzdx, out float dzdy)
        {
            var xp = Math.Min(x + 1, width - 1);
            var xm = Math.Max(x - 1, 0);
            var yp = Math.Min(y + 1, height - 1);
            var ym = Math.Max(y - 1, 0);

            var a = dem[ym, xm];
            var b = dem[ym, x];
            var c = dem[ym, xp];
            var d = dem[y, xm];
            var e = dem[y, x];
            var f = dem[y, xp];
            var g = dem[yp, xm];
            var h = dem[yp, x];
            var i = dem[yp, xp];

            dzdx = ((c + f + f + i) - (a + d + d + g)) / (8f * cellsize);
            dzdy = ((g + h + h + i) - (a + b + b + c)) / (8f * cellsize);
        }

        #endregion

        #region ice stability depth

        public static Bitmap StabilityDepthFiller(TileTreeNode n)
        {
            if (StabilityTileGenerator == null)
            {
                lock (StabilityTileGeneratorLock)
                {
                    if (StabilityTileGenerator == null)
                    {
                        var generator = new IceStabilityTileGenerator();
                        generator.Load();
                        StabilityTileGenerator = generator;
                    }
                }
            }
            var demFirstY = n.Y * Height * n.Stride;
            var demFirstX = n.X * Width * n.Stride;
            Console.WriteLine($"IceStabilityFiller X={n.X} Y={n.Y} Stride={n.Stride} demFirstY={demFirstY} demFirstX={demFirstX}");
            lock (StabilityTileGeneratorLock)
            {
                var bitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
                using (var g = Graphics.FromImage(bitmap))
                {
                    g.ScaleTransform(1f / n.Stride, 1f / n.Stride);
                    g.TranslateTransform(-demFirstX, -demFirstY);
                    StabilityTileGenerator.Draw(g, new Rectangle(demFirstX, demFirstY, Width * n.Stride, Height * n.Stride));
                }
                return bitmap;
            }
        }

        public static Bitmap PSRFiller(TileTreeNode n)
        {
            var stride = n.Stride;
            var demFirstY = n.Y * Height * n.Stride;
            var demFirstX = n.X * Width * n.Stride;

            Console.WriteLine($"PSRFiller isNorth={Terrain.IsNorth} X={n.X} Y={n.Y} Stride={n.Stride} demFirstY={demFirstY} demFirstX={demFirstX}");

            var bitmap = n._bitmap ?? new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                for (var iy = 0; iy < Height; iy++)
                {
                    var rowptr = (byte*)(bitmapData.Scan0 + iy * bitmapData.Stride);
                    for (var ix = 0; ix < Width; ix++)
                    {
                        var val = PSRImgFile.ReadByte(demFirstY + iy * stride, demFirstX + ix * stride);
                        *(rowptr++) = 0;    // B
                        *(rowptr++) = 0;    // G
                        *(rowptr++) = val;  // R
                        *(rowptr++) = val;  // A
                    }
                }
            }
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }

        #endregion

    */
    }
}
