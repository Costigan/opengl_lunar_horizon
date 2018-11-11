using lunar_horizon.terrain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using static lunar_horizon.views.MapView;

namespace lunar_horizon.features
{
    public class FeatureManager
    {
        public static string DatabaseFilename = @"Named_Lunar_Features_for_map_labeling.csv";
        public static SolidBrush FeatureBrush = new SolidBrush(Color.DarkRed);
        public static Font FeatureFont = new Font("Arial", 10, FontStyle.Regular);

        public List<Feature> NorthFeatures;
        public List<Feature> SouthFeatures;

        public void Load()
        {
            var mapBounds = new RectangleF(0f, 0f, 30400f, 30400f);
            NorthFeatures = new List<Feature>();
            SouthFeatures = new List<Feature>();
            using (var tr = new StreamReader(DatabaseFilename))
            using (var csv = new CsvHelper.CsvReader(tr))
            {
                csv.Configuration.HeaderValidated = null;
                foreach (var record in csv.GetRecords<Feature>())
                {
                    if (record.Name == null || record.Name.Length < 1 || record.Name[0] == '[')
                        continue;
                    if (!(record.Latitude >= 70f || record.Latitude <= -70f))
                        continue;
                    record.Flag = null;
                    record.FT = null;
                    record.NameOrigin = null;
                    if (record.Longitude < 0f) record.Longitude += 360f;
                    record.Longitude *= 3.141592653589f / 180f;
                    record.Latitude *= 3.141592653589f / 180f;

                    if (record.Latitude>0f)
                    {
                        InMemoryTerrainManager.GetLineSample(record.Latitude, record.Longitude, out int line, out int sample);
                        var loc = new PointF(sample, line);
                        if (!mapBounds.Contains(loc))
                            continue;
                        record.Location = loc;
                        NorthFeatures.Add(record);
                    }
                    else
                    {
                        InMemoryTerrainManager.GetLineSample(record.Latitude, record.Longitude, out int line, out int sample);
                        var loc = new PointF(sample, line);
                        if (!mapBounds.Contains(loc))
                            continue;
                        record.Location = loc;
                        SouthFeatures.Add(record);
                    }
                }
            }
        }

        public void Draw(Graphics g, DisplayTransform t)
        {
            var graphicsRect = g.ClipBounds;
            var topLeft = t.MouseToMap(new Point(0,0));
            var bottomRight = t.MouseToMap(new Point((int)graphicsRect.Width, (int)graphicsRect.Height));
            var clipRect = new RectangleF(topLeft.X, topLeft.Y, bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);

            InMemoryTerrainManager.GetLineSample((float)(Math.PI / 2), 0f, out int line, out int sample);
            var p = t[new PointF(sample, line)];
            g.FillEllipse(Brushes.Red, p.X - 1, p.Y - 1, 3, 3);

            foreach (var f in InMemoryTerrainManager.Singleton.IsNorth ? NorthFeatures : SouthFeatures)
            {
                if (clipRect.Contains(f.Location))
                {
                    var pt = t.MapToMouse(f.Location);
                    g.DrawString(f.Name, FeatureFont, FeatureBrush, pt);
                }
            }
        }
    }
}
