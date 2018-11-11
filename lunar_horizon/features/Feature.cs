using System.Drawing;

namespace lunar_horizon.features
{
    public class Feature
    {
        public string Flag { get; set; }
        public string Name { get; set; }

        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float Diameter { get; set; }

        public string FT { get; set; }
        public string NameOrigin { get; set; }
        public int IAU_ID { get; set; }

        /// <summary>
        /// Location in DEM map coordinates
        /// </summary>
        public PointF Location;
    }
}
