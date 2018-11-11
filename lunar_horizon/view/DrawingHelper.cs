using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace lunar_horizon.view
{
    public static class DrawingHelper
    {
        public static string[] DaysOfWeek = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
        public static string[] StringNums = null;

        public static SolidBrush DrawBrush = new SolidBrush(Color.Black);
        public static Font DrawFont = new Font("Arial", 10, FontStyle.Regular);
        public static SolidBrush GrayBrush = new SolidBrush(Color.Gray);
        public static Font HourFont = new Font("Arial", 10, FontStyle.Regular);
        public static Font SecondsFont = new Font("Arial", 8, FontStyle.Regular);

        public static Font LargeFont = new Font("Arial", 18, FontStyle.Regular);

        public static void Initialize()
        {
            if (StringNums != null) return;
            const int cnt = 365;
            StringNums = new string[cnt];
            for (int i = 0; i < cnt; i++)
                StringNums[i] = "" + i;
        }

        public static string GetStringNum(int num)
        {
            if (num < 365) // 0 <= num && 
                return StringNums[num];
            return num.ToString();
        }
    }
}
