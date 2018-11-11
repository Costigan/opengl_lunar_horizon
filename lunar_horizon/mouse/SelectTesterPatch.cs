using lunar_horizon.patches;
using lunar_horizon.view;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace lunar_horizon.mouse
{
    public class SelectTesterPatch : MouseMode
    {
        internal int Selection;

        public override void OnMouseDown(Object sender, MouseEventArgs e)
        {
            var id = new Point(e.Location.X / TerrainPatch.DefaultSize, e.Location.Y / TerrainPatch.DefaultSize);
            var pnl = sender as Panel;
            var tester = pnl?.Parent?.Parent as TileTester;
            if (tester == null) return;
            switch (Selection)
            {
                case 1:
                    tester.Selection1 = id;
                    pnl?.Invalidate();
                    break;
                case 2:
                    {
                        if (tester.Selection2.Contains(id))
                            tester.Selection2.RemoveAll(p => p.Equals(id));
                        else
                            tester.Selection2.Add(id);
                        pnl?.Invalidate();
                        break;
                    }
            }
        }
    }

}
