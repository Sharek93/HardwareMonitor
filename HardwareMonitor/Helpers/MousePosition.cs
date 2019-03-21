using System.Runtime.InteropServices;
using System.Drawing;

namespace HardwareMonitor.Helpers
{
    static class MousePosition
    {
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point lpPoint);

        public static Point GetMousePosition()
        {
            GetCursorPos(out var point);
            return point;
        }
    }
}
