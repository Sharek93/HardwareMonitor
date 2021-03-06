﻿using HardwareMonitor.Components;
using HardwareMonitor.Helpers;
using HardwareMonitor.Models;
using HardwareMonitor.Resources;
using HardwareMonitor.Windows;
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
        private MotherboardData _motherboardDataWindow;

        public MainWindow()
        {
            InitializeComponent();
            Config.LoadConfig();
            _motherboardDataWindow = new MotherboardData();            
            if (Config.AppConfig.StartTopMost)
            {
                AppInfo.TopMost = true;
            }
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
            _motherboardDataWindow.Show();
            _motherboardDataWindow.Visibility = Visibility.Hidden;
            WindowPosition.Initialize(this, _motherboardDataWindow);
            _interfaceManager = new InterfaceManager(this, _motherboardDataWindow, _dataManager);
            _interfaceManager.UpdateStatus();
            _interfaceManager.UpdateTopMostStatus();
            _interfaceManager.SetOpacitySettings(Config.AppConfig.Opacity);            
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
            Config.SaveConfig();
            _motherboardDataWindow.Close();
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

        private void TopMostButton_Click(object sender, RoutedEventArgs e)
        {
            AppInfo.TopMost = !AppInfo.TopMost;
            _interfaceManager.UpdateTopMostStatus();
        }

        private void Window_Deactivated(object sender, System.EventArgs e)
        {
            if (AppInfo.TopMost)
            {
                ((Window)sender).Topmost = true;
                return;
            }
            ((Window)sender).Topmost = false;
        }

        private void OpacitySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _interfaceManager.UpdateOpacity((int)e.NewValue);
        }

        private void ShowMotherboardDataButton_Click(object sender, RoutedEventArgs e)
        {
            _interfaceManager.ShowMotherboardWindow();
        }
    }
}
