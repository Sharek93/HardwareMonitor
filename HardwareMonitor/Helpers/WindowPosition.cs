using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace HardwareMonitor.Helpers
{
    public static class WindowPosition
    {
        private static Window _window;
        private static bool _follow;

        public static void Initialize(Window window)
        {
            _follow = false;
            _window = window;
        }

        public static void StartFollow()
        {
            _follow = true;
        }

        public static void StopFollow()
        {
            _follow = false;
        }

        public static void Follow()
        {
            if (!_follow)
            {
                return;
            }
            var position = MousePosition.GetMousePosition();
            if (position == null)
            {
                return;
            }
            OffsetPosition(ref position);
            _window.Left = position.X;
            _window.Top = position.Y;

            Debug.WriteLine($"Window position {_window.Left}x{_window.Top}");
            Debug.WriteLine($"Cursor position {position.X}x{position.Y}");
        }



        private static void OffsetPosition(ref System.Drawing.Point startPoint)
        {
            var button = (Button)_window.FindName("FollowButton");
            var buttonPos = button.TransformToAncestor(_window).Transform(new Point(0, 0));
            var butSize = new System.Drawing.Size((int)button.Width, (int)button.Height);
            startPoint = new System.Drawing.Point(startPoint.X - (int)buttonPos.X - butSize.Width / 2, startPoint.Y - (int)buttonPos.Y - butSize.Height / 2);
        }
    }
}
