using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using lunar_horizon.mouse;

namespace lunar_horizon.utilities
{
    public static class Extensions
    {
        public static float DistanceTo(this Point a, Point b)
        {
            var dx = a.X - b.X;
            var dy = a.Y - b.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        public static float DistanceTo(this PointF a, PointF b)
        {
            var dx = a.X - b.X;
            var dy = a.Y - b.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        public static void Add(this Stack<MouseMode> stack, MouseMode mode) { stack.Push(mode); }

        public static Form GetParentForm(this Control c)
        {
            while (c != null && !(c is Form))
                c = c.Parent;
            return c as Form;
        }

        public static PointF ToPointF(this Point a) => new PointF(a.X.ToCoord(),a.Y.ToCoord());
        public static Point ToPoint(this PointF p) => new Point(p.X.ToFixed(), p.Y.ToFixed());

        public static float ToCoord(this int i) => i;
        public static int ToFixed(this float f) => (int)(f + 0.5f);

        public static Regex LatitudeRegex = new Regex("^\\s*(\\d+)d\\s*(\\d+)'\\s*(\\d*\\.\\d*)\"([NS])$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

        public static Regex LongitudeRegex = new Regex("^\\s*(\\d+)d\\s*(\\d+)'\\s*(\\d*\\.\\d*)\"([EW])$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

        public static bool TryToLatitude(this string s, out double r)
        {
            r = 0d;
            if (double.TryParse(s, out r)) return true;
            var match = LatitudeRegex.Match(s);
            if (!match.Success) return false;
            System.Diagnostics.Debug.Assert(match.Groups.Count == 5);
            var deg = match.Groups[1].Value;
            var min = match.Groups[2].Value;
            var sec = match.Groups[3].Value;
            var dir = match.Groups[4].Value;
            if (!int.TryParse(deg, out int degi) || !int.TryParse(min, out int mini) || !double.TryParse(sec, out double secd)) return false;
            r = degi + mini / 60d + secd / 3600d;
            if (dir.ToUpperInvariant().Equals("S"))
                r = -r;
            if (-90d <= r && r <= 90d) return true;
            r = 0d;
            return false;
        }

        public static bool TryToLongitude(this string s, out double r)
        {
            r = 0d;
            if (double.TryParse(s, out r)) return true;
            var match = LongitudeRegex.Match(s);
            if (!match.Success) return false;
            System.Diagnostics.Debug.Assert(match.Groups.Count == 5);
            var deg = match.Groups[1].Value;
            var min = match.Groups[2].Value;
            var sec = match.Groups[3].Value;
            var dir = match.Groups[4].Value;
            if (!int.TryParse(deg, out int degi) || !int.TryParse(min, out int mini) || !double.TryParse(sec, out double secd)) return false;
            r = degi + mini / 60d + secd / 3600d;
            if (dir.ToUpperInvariant().Equals("W"))
                r = -r;
            if (-180d <= r && r <= 180d) return true;
            r = 0d;
            return false;
        }

        public static System.Windows.Vector Rotate(this System.Windows.Vector v, double degrees)
        {
            return v.RotateRadians(degrees * Math.PI / 180d);
        }

        public static System.Windows.Vector RotateRadians(this System.Windows.Vector v, double radians)
        {
            var ca = Math.Cos(radians);
            var sa = Math.Sin(radians);
            return new System.Windows.Vector(ca * v.X - sa * v.Y, sa * v.X + ca * v.Y);
        }

        public static IEnumerable<T> Enumerate<T>(this T[,] data) where T : struct
        {
            var height = data.GetLength(0);
            var width = data.GetLength(1);
            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                    yield return data[y, x];
        }
    }
}
