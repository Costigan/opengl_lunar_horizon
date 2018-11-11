using lunar_horizon.terrain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace lunar_horizon.graphics
{
    public class RenderIceStabilityDepthTest
    {
        const string NP_Siegler = @"C:\RP\Tasks\Ice_Stability_Depth_for_horizons\NP_Siegler_etal_2016_Ice.txt";

        internal void Test1()
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            var triangles = GetTriangles(NP_Siegler);
            stopwatch.Stop();
            Console.WriteLine($"Loading triangles took {stopwatch.Elapsed}");

            var brushes = new Brush[] { Brushes.Red, Brushes.Green, Brushes.Yellow, Brushes.DarkGray };

            var bmp = new Bitmap(1024, 1024, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(bmp))
            {
                g.ScaleTransform(bmp.Width / 30400f, bmp.Height / 30400f);
                stopwatch.Restart();
                foreach (var t in triangles)
                    g.FillPolygon(brushes[t.Zone], t.Polygon);
                stopwatch.Stop();
                Console.WriteLine($"Drawing triangles took {stopwatch.Elapsed}");
            }

            bmp.Save("test.png", ImageFormat.Png);
        }

        List<IceStabilityTriangle> GetTriangles(string path)
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
    }

    public struct IceStabilityTriangle
    {
        public PointF[] Polygon;
        public float Depth;
        public int Zone;
    }

}
