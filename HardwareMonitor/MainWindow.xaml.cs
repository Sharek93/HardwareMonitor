using HardwareMonitor.Components;
using HardwareMonitor.Helpers;
using HardwareMonitor.Models;
using System.Windows;
using System.Windows.Input;

namespace HardwareMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataManager _dataManager;
        private InterfaceManager _interfaceManager;

        public MainWindow()
        {
            InitializeComponent();
            Config.LoadConfig();
            var config = new DataManagerConfig
            {
                MonitorCpu = true,
                MonitorGpu = true,
                MonitorMemory = true,
                MonitorDisks = true,
                MonitorFans = true,
                MonitorMotherboard = true,
            };
            _dataManager = new DataManager(config);
            WindowPosition.Initialize(this);
            _interfaceManager = new InterfaceManager(this, _dataManager);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (_dataManager.IsRunning)
            {
                _dataManager.Stop();
            }
            else
            {
                _dataManager.Start();
            }
            if (_interfaceManager.IsRunning)
            {
                _interfaceManager.Stop();
            }
            else
            {
                _interfaceManager.Start();
            }
        }

        private void CloseButton_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void FollowButton_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            WindowPosition.Follow();
        }

        private void FollowButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowPosition.StartFollow();
        }

        private void FollowButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            WindowPosition.StopFollow();
        }
    }
}
