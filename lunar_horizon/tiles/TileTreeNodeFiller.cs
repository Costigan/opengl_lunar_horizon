using lunar_horizon.pds;
using lunar_horizon.terrain;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace lunar_horizon.tiles
{
    abstract public class TileTreeNodeFiller
    {
        public const int Height = TileTreeNode.Height;
        public const int Width = TileTreeNode.Width;

        public string CacheSubdirectory = "default";
        abstract public Bitmap Fill(TileTreeNode n);
        
    }

    public class HillshadeFiller : TileTreeNodeFiller
    {
        public override Bitmap Fill(TileTreeNode n)
        {
            var stride = n.Stride;
            const double elevation = 30d * Math.PI / 180d;
            const double azimuth = -Math.PI / 2;
            var Zenith_rad = Math.PI / 2d - elevation;
            var Azimuth_rad = Math.PI - azimuth;  // Convert from map azimuth to hillshade azimuth

            var demFirstY = n.Y * Height * n.Stride;
            var demFirstX = n.X * Width * n.Stride;

            Console.WriteLine($"HillshadeFiller isNorth={InMemoryTerrainManager.Singleton.IsNorth} X={n.X} Y={n.Y} Stride={n.Stride} demFirstY={demFirstY} demFirstX={demFirstX}");
            var dem = new short[Height, Width];
            var terrain = InMemoryTerrainManager.Singleton;
            for (var iy = 0; iy < Height; iy++)
            {
                for (var ix = 0; ix < TileTreeNode.Width; ix++)
                    dem[iy, ix] = terrain.Fetch(demFirstY + iy * stride, demFirstX + ix * stride);
            }

            var bitmap = n._bitmap != null ? n._bitmap : new Bitmap(TileTreeNode.Width, TileTreeNode.Height, PixelFormat.Format8bppIndexed);
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

        internal void SlopesForHillshade(int x, int y, short[,] dem, int width, int height, float cellsize, out float dzdx, out float dzdy)
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
    }

    public class SlopeFiller : TileTreeNodeFiller
    {
        public override Bitmap Fill(TileTreeNode n)
        {
            var stride = n.Stride;
            var demFirstY = n.Y * Height * n.Stride;
            var demFirstX = n.X * Width * n.Stride;

            Console.WriteLine($"SlopeFiller isNorth={InMemoryTerrainManager.Singleton.IsNorth} X={n.X} Y={n.Y} Stride={n.Stride} demFirstY={demFirstY} demFirstX={demFirstX}");
            var dem = new short[Height, Width];
            var terrain = InMemoryTerrainManager.Singleton;
            for (var iy = 0; iy < Height; iy++)
            {
                for (var ix = 0; ix < TileTreeNode.Width; ix++)
                    dem[iy, ix] = terrain.Fetch(demFirstY + iy * stride, demFirstX + ix * stride);
            }

            var bitmap = n._bitmap ?? new Bitmap(TileTreeNode.Width, TileTreeNode.Height, PixelFormat.Format32bppArgb);

            var green = Color.FromArgb(255, Color.PaleGreen).ToArgb();
            var yellow = Color.FromArgb(255, Color.LightGoldenrodYellow).ToArgb();
            var red = Color.FromArgb(255, Color.IndianRed).ToArgb();

            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                for (var y = 0; y < Height; y++)
                {
                    var rowptr = (Int32*)(bitmapData.Scan0 + y * bitmapData.Stride);
                    for (var x = 0; x < Width; x++)
                    {
                        var slope = SlopesForHillshade(x, y, dem, Width, Height, 20f * stride);
                        var angle_deg = Math.Atan(slope) * 180d / Math.PI;

                        if (angle_deg <= 11d)    // matches the round-down in the traverse planner.
                            *(rowptr++) = green;
                        else if (angle_deg <= 16d)  
                            *(rowptr++) = yellow;
                        else
                            *(rowptr++) = red;
                    }
                }
            }
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }

        internal unsafe float SlopesForHillshade(int x, int y, short[,] dem, int width, int height, float cellsize)
        {
            var xp = Math.Min(x + 1, width - 1);
            var xm = Math.Max(x - 1, 0);
            var yp = Math.Min(y + 1, height - 1);
            var ym = Math.Max(y - 1, 0);

            var max_slope = 0f;
            var ary = stackalloc int[4];
            var center = dem[y, x];

            ary[0] = dem[ym, x];
            ary[1] = dem[y, xm];
            ary[2] = dem[y, xp];
            ary[3] = dem[yp, x];
            var scale = 0.5f / cellsize;  //2f / cellsize;
            for (var i = 0; i < 4; i++)
            {
                var slope = Math.Abs(ary[i] - center) * scale;
                if (slope > max_slope) max_slope = slope;
            }

            ary[0] = dem[ym, xm];
            ary[1] = dem[ym, xp];
            ary[2] = dem[yp, xm];
            ary[3] = dem[yp, xp];
            scale /= 1.414214f;
            for (var i = 0; i < 4; i++)
            {
                var slope = Math.Abs(ary[i] - center) * scale;
                if (slope > max_slope) max_slope = slope;
            }

            return max_slope;
        }
    }

    public class StabilityDepthFiller : TileTreeNodeFiller
    {
        private IceStabilityTileGenerator StabilityTileGenerator;
        private object StabilityTileGeneratorLock = new object();

        public override Bitmap Fill(TileTreeNode n)
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
    }

    public class PSRFiller : TileTreeNodeFiller
    {
        public MemoryMappedImgFile ImgFile;

        public override Bitmap Fill(TileTreeNode n)
        {
            var stride = n.Stride;
            var demFirstY = n.Y * Height * n.Stride;
            var demFirstX = n.X * Width * n.Stride;

            Console.WriteLine($"PSRFiller X={n.X} Y={n.Y} Stride={n.Stride} demFirstY={demFirstY} demFirstX={demFirstX}");

            var bitmap = n._bitmap ?? new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                for (var iy = 0; iy < Height; iy++)
                {
                    var rowptr = (byte*)(bitmapData.Scan0 + iy * bitmapData.Stride);
                    for (var ix = 0; ix < Width; ix++)
                    {
                        var val = ImgFile.ReadByte(demFirstY + iy * stride, demFirstX + ix * stride);
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
    }

    public class ByteGrayscaleImageFiller : TileTreeNodeFiller
    {
        public MemoryMappedImgFile ImgFile;

        public override Bitmap Fill(TileTreeNode n)
        {
            var stride = n.Stride;
            var demFirstY = n.Y * Height * n.Stride;
            var demFirstX = n.X * Width * n.Stride;

            Console.WriteLine($"PSRFiller X={n.X} Y={n.Y} Stride={n.Stride} demFirstY={demFirstY} demFirstX={demFirstX}");

            var bitmap = n._bitmap ?? new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                for (var iy = 0; iy < Height; iy++)
                {
                    var rowptr = (byte*)(bitmapData.Scan0 + iy * bitmapData.Stride);
                    for (var ix = 0; ix < Width; ix++)
                    {
                        var val = ImgFile.ReadByte(demFirstY + iy * stride, demFirstX + ix * stride);
                        *(rowptr++) = val;    // B
                        *(rowptr++) = val;    // G
                        *(rowptr++) = val;  // R
                        *(rowptr++) = 255;  // A
                    }
                }
            }
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }
    }
}
