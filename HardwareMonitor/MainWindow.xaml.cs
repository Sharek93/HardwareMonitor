using HardwareMonitor.Components;
using HardwareMonitor.Components.Monitors;
using HardwareMonitor.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            var config = new DataManagerConfig
            {
                MonitorCpu = true,
                MonitorGpu = true,
                MonitorMemory = true,
                MonitorDisks = true
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
