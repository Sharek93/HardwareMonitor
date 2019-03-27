using HardwareMonitor.Resources;
using HardwareMonitor.Windows;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace HardwareMonitor.Helpers
{
    public static class WindowPosition
    {
        private static Window _window;
        private static MotherboardData _motherboardDataWindow;
        private static bool _follow;

        public static void Initialize(Window window, MotherboardData motherboardDataWindow)
        {
            _follow = false;
            _window = window;
            _motherboardDataWindow = motherboardDataWindow;
            UpdateMotherboardWindowPosition();
        }

        public static void StartFollow()
        {
            _follow = true;
            _motherboardDataWindow.Visibility = Visibility.Hidden;
        }

        public static void StopFollow()
        {
            _follow = false;
            if (AppInfo.MotherboardWindowVisible)
            {
                _motherboardDataWindow.Visibility = Visibility.Visible;
            }            
            UpdateMotherboardWindowPosition();
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

            UpdateMotherboardWindowPosition();

            Debug.WriteLine($"Window position {_window.Left}x{_window.Top}");
            Debug.WriteLine($"Cursor position {position.X}x{position.Y}");
        }

        public static void UpdateMotherboardWindowPosition()
        {
            UpdateMotherboardWindowPosition(new System.Drawing.PointF((float)_window.Left, (float)_window.Top), (float)_window.Height);
        }

        public static void UpdateMotherboardWindowPosition(System.Drawing.PointF masterWindowLocalication, float MasterWindowHeight)
        {
            if (_motherboardDataWindow.Visibility == Visibility.Visible)
            {
                _motherboardDataWindow.Left = masterWindowLocalication.X;
                _motherboardDataWindow.Top = masterWindowLocalication.Y + MasterWindowHeight;
            }
        }

        private static void OffsetPosition(ref System.Drawing.Point startPoint)
        {
            var button = (Button)_window.FindName("FollowButton");
            var buttonPos = button.TransformToAncestor(_window).Transform(new Point(0, 0));
            var buttonSize = new System.Drawing.Size((int)button.Width, (int)button.Height);
            startPoint = new System.Drawing.Point(startPoint.X - (int)buttonPos.X - buttonSize.Width / 2, startPoint.Y - (int)buttonPos.Y - buttonSize.Height / 2);
        }
    }
}
