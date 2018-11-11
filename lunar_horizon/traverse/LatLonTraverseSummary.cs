using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace TraversePlanner.planning
{
    public class LatLonTraverseSummary
    {
        public List<LatLon> LatLonList { get; set; }
        public List<PointF> LineSampleList = new List<PointF>();

        public static LatLonTraverseSummary Load(string path)
        {
            using (StreamReader sr = File.OpenText(path))
            using (var jw = new JsonTextReader(sr))
            {
                var serializer = new JsonSerializer { TypeNameHandling = TypeNameHandling.All };
                return serializer.Deserialize<LatLonTraverseSummary>(jw);
            }
        }
        public void Save(string path)
        {
            using (StreamWriter sw = File.CreateText(path))
            using (var jw = new JsonTextWriter(sw) { Formatting = Formatting.Indented })
            {
                var serializer = new JsonSerializer { TypeNameHandling = TypeNameHandling.None };
                serializer.Serialize(jw, this);
            }
        }
    }

    public class LatLon
    {
        public static LatLon Empty = new LatLon { Latitude = double.NaN, Longitude = double.NaN };
        public bool IsEmpty => double.IsNaN(Latitude);
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public override string ToString() => $"<{Latitude},{Longitude}>";
    }
}
