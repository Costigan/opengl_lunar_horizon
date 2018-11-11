using System.Windows.Forms;

namespace lunar_horizon.view
{
    public static class StatusManager
    {
        public static ToolStripStatusLabel StatusLabel;

        internal static void ShowStatus(string msg)
        {
            if (StatusLabel != null)
                StatusLabel.Text = msg;
        }
    }
}
