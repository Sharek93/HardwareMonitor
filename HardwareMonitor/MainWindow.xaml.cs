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
        public MainWindow()
        {
            InitializeComponent();
            ComputerManager.Initialize();
            CpuMonitor.Initialize();
            WindowPosition.Initialize(this);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataManager.IsRunning)
            {
                DataManager.Stop();
                return;
            }
            DataManager.Start();
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
