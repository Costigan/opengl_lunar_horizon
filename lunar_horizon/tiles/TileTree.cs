using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using static lunar_horizon.views.MapView;

namespace lunar_horizon.tiles
{
    /// <summary>
    /// Terrain Tiles know about 2D images and functions of the DEM
    /// </summary>
    public class TileTree : IDisposable
    {
        public const float Log2 = 0.6931472f;

        protected TileTreeNodeFiller _Filler = null;
        protected string _CacheRoot = "default";
        protected string _CacheDirectory = null;

        private readonly Dictionary<int, TileTreeNode> _tileCache = new Dictionary<int, TileTreeNode>();

        public string CacheRoot { get { return _CacheRoot; } set { _CacheRoot = value; UpdateCacheDirectory(); } }
        public TileTreeNodeFiller Filler { get { return _Filler; } set { _Filler = value; UpdateCacheDirectory(); } }

        public static TileTree MakeTree(TileTreeNodeFiller filler, string cache_root)
        {
            var t = new TileTree { Filler = filler, CacheRoot = cache_root };
            t.EnsureCacheDirectoryExists();
            return t;
        }

        private void UpdateCacheDirectory()
        {
            EmptyCache();
            if (_Filler != null)
                _CacheDirectory = System.IO.Path.Combine(_CacheRoot, _Filler.CacheSubdirectory);
        }

        public void EnsureCacheDirectoryExists()
        {
            if (_CacheDirectory == null)
                return;
            try
            {
                System.IO.Directory.CreateDirectory(_CacheDirectory);
            } catch (System.IO.IOException e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// Fetch a node.  Create one if it's not already there.
        /// </summary>
        /// <param name="a">An address</param>
        /// <returns></returns>
        public TileTreeNode Fetch(int a)
        {
            if (_tileCache.TryGetValue(a, out TileTreeNode r))
                return r;
            r = new TileTreeNode(a, _CacheDirectory);
            _tileCache.Add(a, r);
            return r;
        }

        public void EmptyCache()
        {
            foreach (var tile in _tileCache.Values.ToList())
            {
                _tileCache.Remove(tile.Address);
                tile.Bitmap?.Dispose();
            }
        }

        public void Draw(Graphics g, DisplayTransform t, ImageAttributes attributes = null)
        {
            var r = g.ClipBounds;
            //Console.WriteLine($"-- clipbounds={r}");
            // Scale indicates the number of map pixels per screen pixel.
            // OffsetX and OffsetY indicate the offset of the window origin relative to the upper left
            // corner of the tile array at the current level.

            var level = (int)(Math.Log(t.Scale) / Log2);  // Log2(Scale);
            level = Math.Min(TileTreeNode.TopLevel, Math.Max(0, level));  // Clip level
            var stride = TileTreeNode.PowersOf2[level];

            var drawScale = t.Scale / stride;                // range = [1,2)
            var cellSize = (int)(TileTreeNode.Width / drawScale);  // range = (475, 950], width==height
            var cellMax = TileTreeNode.MapWidth / (TileTreeNode.Width * stride);

            var x1 = Math.Max(0, -t.OffsetX / cellSize - 1);
            var y1 = Math.Max(0, -t.OffsetY / cellSize - 1);

            var x2 = cellMax - 1;
            var y2 = cellMax - 1;

            for (var y = y1; y <= y2; y++)
            {
                var pixelY = t.OffsetY + y * cellSize;
                if (pixelY > r.Bottom)
                {
                    //Console.WriteLine($"  break pixelY {pixelY} > r.Bottom {r.Bottom}");
                    break;
                }
                for (var x = x1; x <= x2; x++)
                {
                    var pixelX = t.OffsetX + x * cellSize;
                    if (pixelX > r.Right)
                    {
                        //Console.WriteLine($"  break pixelX {pixelX} > r.Right {r.Right}");
                        break;
                    }
                    //Console.WriteLine($"  drawing x={pixelX} y={pixelY} x={x} y={y} width={r.Width} height={r.Height}");
                    var address = TileTreeNode.CalculateAddress(level, x, y);
                    var cell = Fetch(address);
                    var bitmap = Filler != null ? cell.GenerateBitmap(Filler) : null;
                    if (bitmap != null)
                    {
                        var r1 = new Rectangle(pixelX, pixelY, cellSize, cellSize);
                        if (attributes == null)
                            g.DrawImage(bitmap, r1);
                        else
                            g.DrawImage(bitmap, r1, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, attributes);
                    }
                    else
                    {
                        if (attributes == null)
                            g.FillRectangle(Brushes.DarkGray, pixelX, pixelY, cellSize, cellSize);
                    }
                }
            }
        }

        /// <summary>
        /// Duplicates the calculation above to help the idle mouse mode calculate map positions
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static void DrawCellSize(DisplayTransform t, out float cellSize, out int stride, out int level)
        {
            level = (int)(Math.Log(t.Scale) / Log2);  // Log2(Scale);
            level = Math.Min(TileTreeNode.TopLevel, Math.Max(0, level));  // Clip level
            stride = TileTreeNode.PowersOf2[level];               
            cellSize = (int)(TileTreeNode.Width * stride / t.Scale);    // range = (475, 950], width==height
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                EmptyCache();

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~TileTree() {
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
