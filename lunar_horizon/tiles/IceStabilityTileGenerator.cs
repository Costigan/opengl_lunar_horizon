using lunar_horizon.terrain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace lunar_horizon.tiles
{
    public class IceStabilityTileGenerator
    {
        const int ChunkSize = 1024;

        const string NP_Siegler_240m = @"C:\RP\Tasks\Ice_Stability_Depth_for_horizons\NP_Siegler_etal_2016_Ice.txt";
        const string SP_Siegler_240m = @"C:\RP\Tasks\Ice_Stability_Depth_for_horizons\SP_Siegler_etal_2016_Ice.txt";

        const string NP_Siegler_HermiteA_20m = @"C:\RP\maps\inputs_20m\HermiteA\Hermite\Other\HermiteA6000_Siegler.txt";

        List<StabilityChunk> Chunks;
        Brush[] brushes = { Brushes.Red, Brushes.Green, Brushes.Yellow, Brushes.DarkGray };
        Brush[] _depthBrushes = Enumerable.Range(0, 256).Select(i => new SolidBrush(Color.FromArgb(i, i, i))).ToArray();

        #region Load data

        public void Load()
        {
            Console.WriteLine(@"IceStabilityTileGenerator loading ...");
            var path = LunarHorizon.Singleton.IsNorth ? NP_Siegler_240m : SP_Siegler_240m;
            var triangles_240 = GetTriangles240m(path);
            //var triangles_20m = GetTriangles240m(NP_Siegler_HermiteA_20m);
            //triangles_240.AddRange(triangles_20m);
            Chunks = BuildChunks(triangles_240);
            Console.WriteLine(@"IceStabilityTileGenerator finished loading.");
        }

        private List<StabilityChunk> BuildChunks(List<IceStabilityTriangle> triangles)
        {
            const int DEMSize = TileTreeNode.MapWidth;

            // Build 2d array of chunks
            var chunkwidth = (int)Math.Ceiling(DEMSize / (float)ChunkSize);
            var chunkArray = new StabilityChunk[chunkwidth, chunkwidth];
            for (var y = 0; y < chunkwidth; y++)
                for (var x = 0; x < chunkwidth; x++)
                    chunkArray[x, y] = new StabilityChunk { BoundingBox = new Rectangle(x * ChunkSize, y*ChunkSize, ChunkSize, ChunkSize), Triangles = new List<IceStabilityTriangle>() };

            // loop through the triangles, assigning them to chunks
            foreach (var t in triangles)
            {
                var box = t.BoundingBox();
                InsertIntoChunks(chunkArray, box.X / ChunkSize, box.Y / ChunkSize, t, box);
            }

            var chunks = new List<StabilityChunk>();
            for (var y = 0; y < chunkwidth; y++)
                for (var x = 0; x < chunkwidth; x++)
                    if (chunkArray[x, y].Triangles.Count > 0)
                        chunks.Add(chunkArray[x, y]);
            return chunks;
        }

        void InsertIntoChunks(StabilityChunk[,] ary, int x, int y, IceStabilityTriangle tri, Rectangle box)
        {
            InsertIntoChunkMaybe(ary, x, y, tri, box);
            InsertIntoChunkMaybe(ary, x - 1, y, tri, box);
            InsertIntoChunkMaybe(ary, x + 1, y, tri, box);

            InsertIntoChunkMaybe(ary, x, y-1, tri, box);
            InsertIntoChunkMaybe(ary, x - 1, y - 1, tri, box);
            InsertIntoChunkMaybe(ary, x + 1, y - 1, tri, box);

            InsertIntoChunkMaybe(ary, x, y + 1, tri, box);
            InsertIntoChunkMaybe(ary, x - 1, y + 1, tri, box);
            InsertIntoChunkMaybe(ary, x + 1, y + 1, tri, box);
        }

        void InsertIntoChunkMaybe(StabilityChunk[,] ary, int x, int y, IceStabilityTriangle tri, Rectangle box)
        {
            if (x>=0 && y>=0 && x<ary.GetLength(0) && y<ary.GetLength(1))
            {
                var chunk = ary[x, y];
                if (box.IntersectsWith(chunk.BoundingBox))
                    chunk.Triangles.Add(tri);
            }            
        }

        List<IceStabilityTriangle> GetTriangles240m(string path)
        {
            var triangles = new List<IceStabilityTriangle>();
            string line;
            using (var file = new System.IO.StreamReader(path))
                while ((line = file.ReadLine()) != null)
                {
                    var tokens = line.Split('\t');
                    var column = 0;
                    var x1 = double.Parse(tokens[column++]);
                    var y1 = double.Parse(tokens[column++]);
                    var z1 = double.Parse(tokens[column++]);
                    var x2 = double.Parse(tokens[column++]);
                    var y2 = double.Parse(tokens[column++]);
                    var z2 = double.Parse(tokens[column++]);
                    var x3 = double.Parse(tokens[column++]);
                    var y3 = double.Parse(tokens[column++]);
                    var z3 = double.Parse(tokens[column++]);
                    var lon = double.Parse(tokens[column++]);
                    var lat = double.Parse(tokens[column++]);
                    var elevation = double.Parse(tokens[column++]);
                    var num = (int)Math.Round(float.Parse(tokens[column++]));
                    var modern = float.Parse(tokens[column++]);
                    var paleo = float.Parse(tokens[column++]);

                    var p1 = new PointF();
                    var p2 = new PointF();
                    var p3 = new PointF();

                    ProjectPoint(new double[] { x1, y1, z1 }, ref p1);
                    ProjectPoint(new double[] { x2, y2, z2 }, ref p2);
                    ProjectPoint(new double[] { x3, y3, z3 }, ref p3);

                    var polygon = new PointF[] { p1, p2, p3 };

                    var m = modern;
                    var zone = m < 0 ? 0
                             : m < 0.5 ? 1
                             : m < 1 ? 2
                             : 3;
                    triangles.Add(new IceStabilityTriangle { Depth = modern, Zone = zone, Polygon = polygon });
                }
            return triangles;
        }

        private void ProjectPoint(double[] rectan, ref PointF p)
        {
            double r = 0, lat_rad = 0, lon_rad = 0;
            spice.CSpice.reclat_c(rectan, ref r, ref lon_rad, ref lat_rad);
            var lon_deg = lon_rad * 180 / Math.PI;
            var lat_deg = lat_rad * 180 / Math.PI;
            InMemoryTerrainManager.GetLineSample(lat_rad, lon_rad, out int line, out int sample);
            p.X = sample;
            p.Y = line;
        }

        #endregion

        #region structs

        public struct IceStabilityTriangle
        {
            public PointF[] Polygon;
            public float Depth;
            public int Zone;

            public Rectangle BoundingBox()
            {
                int minx = int.MaxValue, maxx = int.MinValue, miny = int.MaxValue, maxy = int.MinValue;
                if (Polygon!=null)
                for (var i=0;i< Polygon.Length;i++)
                    {
                        var p = Polygon[i];
                        minx = Math.Min(minx, (int)Math.Floor(p.X));
                        maxx = Math.Max(maxx, (int)Math.Ceiling(p.X));
                        miny = Math.Min(miny, (int)Math.Floor(p.Y));
                        maxy = Math.Max(maxy, (int)Math.Ceiling(p.Y));
                    }
                return new Rectangle(minx-1, miny-1, maxx - minx + 2, maxy - miny + 2);  // Some margin
            }
        }

        public struct StabilityChunk
        {
            public Rectangle BoundingBox;
            public List<IceStabilityTriangle> Triangles;
        }

        #endregion

        #region drawing

        // Note no graphics scaling here.  That is in the caller.
        public void Draw(Graphics g, Rectangle box)
        {
            //g.FillRectangle(Brushes.Green, 0, 0, 32000, 32000);
            int count = 0;
            foreach (var chunk in Chunks)
                if (box.IntersectsWith(chunk.BoundingBox))
                {
                    var tris = chunk.Triangles;
                    for (var i = 0; i < tris.Count; i++) // ***** Bug Hunt i++
                    {
                        var t = tris[i];
                        var brush = brushes[t.Zone];
                        //var d = t.Depth;
                        //var index = d < 0f ? 0 : (int)(255f * d / 2.5f);
                        //var brush = _depthBrushes[index];
                        g.FillPolygon(brush, t.Polygon);
                        count++;
                    }
                }
            Console.WriteLine($"Drawing {box} count={count}");
        }

        #endregion
    }
}
