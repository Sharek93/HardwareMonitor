using HardwareMonitor.Enums;
using HardwareMonitor.Helpers;
using HardwareMonitor.Models;
using HardwareMonitor.Resources;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HardwareMonitor.Components
{
    public class InterfaceManager
    {
        private readonly MainWindow _window;
        private readonly DataManager _dataManager;
        public bool IsRunning { get; private set; } = false;
        private readonly DataManagerConfig _config;

        public InterfaceManager(MainWindow window, DataManager dataManager)
        {
            _window = window;
            _dataManager = dataManager;
            IsRunning = false;
            _config = dataManager.Config;
        }

        public void UpdateStatus()
        {
            _window.Dispatcher.Invoke(() => {
                if (AppInfo.IsRunning)
                {
                    _window.StatusLabel.Foreground = Brushes.Lime;
                    _window.StatusLabel.Content = "Running";
                    return;
                }
                _window.StatusLabel.Foreground = Brushes.Red;
                _window.StatusLabel.Content = "Stopped";
            });
        }

        public void UpdateTopMostStatus()
        {
            if (AppInfo.TopMost)
            {
                _window.TopMostFrame.Background = Brushes.Lime;
                return;
            }
            _window.TopMostFrame.Background = Brushes.Red;
        }

        public void Start()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                Run();
            }
            UpdateStatus();
        }

        public void Stop()
        {
            ResetInterface(_window);
            UpdateStatus();
            IsRunning = false;
        }

        private void Run()
        {
            Task.Run(() => {
                do
                {
                    if (_config.MonitorCpu)
                    {
                        UpdateCpuInterface();
                    }
                    if (_config.MonitorMemory)
                    {
                        UpdateMemoryInterface();
                    }
                    if (_config.MonitorGpu)
                    {
                        UpdateGpuInterFace();
                    }
                    if (_config.MonitorDisks)
                    {
                        UpdateDiskInterface();
                    }
                    Thread.Sleep(1000);
                } while (IsRunning);
            });
        }

        private void UpdateCpuInterface()
        {
            var data = _dataManager.CpuData;

            try
            {
                _window.Dispatcher.Invoke(new Action(() =>
                {
                    try
                    {
                        _window.CpuName.Content = data.Name ?? "No data";

                        ChangeColorAndUpdateText(_window.Core1Usage, data.Load.Core1, DataType.CpuUsage);
                        ChangeColorAndUpdateText(_window.Core2Usage, data.Load.Core2, DataType.CpuUsage);
                        ChangeColorAndUpdateText(_window.Core3Usage, data.Load.Core3, DataType.CpuUsage);
                        ChangeColorAndUpdateText(_window.Core4Usage, data.Load.Core4, DataType.CpuUsage);
                        ChangeColorAndUpdateText(_window.TotalUsage, data.Load.Total, DataType.CpuUsage);

                        ChangeColorAndUpdateText(_window.Core1Temp, data.Temperature.Core1, DataType.CpuTemp);
                        ChangeColorAndUpdateText(_window.Core2Temp, data.Temperature.Core2, DataType.CpuTemp);
                        ChangeColorAndUpdateText(_window.Core3Temp, data.Temperature.Core3, DataType.CpuTemp);
                        ChangeColorAndUpdateText(_window.Core4Temp, data.Temperature.Core4, DataType.CpuTemp);
                        ChangeColorAndUpdateText(_window.TotalTemp, data.Temperature.Total, DataType.CpuTemp);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Error occured while updating CpuData interface: " + e.Message);
                    }
                }));
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error occured while invoking CPU interface. " + e.Message);
            }
        }

        private void UpdateMemoryInterface()
        {
            var data = _dataManager.MemoryData;

            try
            {
                _window.Dispatcher.Invoke(new Action(() =>
                {
                    try
                    {
                        _window.MemoryName.Content = data.Name ?? "No data";

                        ChangeColorAndUpdateText(_window.MemoryUsed, data.MemoryUsePercent, DataType.Memory);
                        _window.MemoryUsed.Content = data.MemoryUsed != null ? $"{data.MemoryUsed:0.00}GB ({data.MemoryUsePercent:0}%)" : "-";
                        _window.MemoryFree.Content = data.MemoryAvailable != null ? $"{data.MemoryAvailable:0.00}GB" : "-";
                        _window.MemoryTotal.Content = data.MemoryTotal != null ? $"{data.MemoryTotal:0.00}GB" : "-";
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Error occured while updating MemoryData interface: " + e.Message);
                    }
                }));
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error occured while invoking memory interface. " + e.Message);
            }
        }

        private void UpdateGpuInterFace()
        {
            var data = _dataManager.GpuData;

            try
            {
                _window.Dispatcher.Invoke(new Action(() =>
                {
                    try
                    {
                        _window.GpuName.Content = data.Name ?? "No data";
                        ChangeColorAndUpdateText(_window.GpuTemp, data.Temperature, DataType.GpuTemp);
                        _window.GpuFanSpeed.Content = data.FanSpeed != null ? $"{data.FanSpeed}RPM" : "-";
                        _window.GpuMemory.Content = data.MemoryUsed != null ? $"{data.MemoryUsed/1024:0.00}GB/{data.MemoryTotal/1024:0}GB ({data.MemoryLoad:0}%)" : "-";
                        _window.GpuCoreClock.Content = data.CoreLoad != null ? $"{data.CoreLoad}% ({data.CoreClock:0}MHz)" : "-";
                        _window.GpuMemoryClock.Content = data.MemoryControlLoad != null ? $"{data.MemoryControlLoad}% ({data.MemoryClock:0}MHz)" : "-";
                        _window.GpuShaderClock.Content = data.VideoEngineLoad != null ? $"{data.VideoEngineLoad}% ({data.ShaderClock:0}MHz)" : "-";
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Error occured while updating GpuData interface: " + e.Message);
                    }
                }));
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error occured while invoking GPU interface. " + e.Message);
            }
        }

        private void UpdateDiskInterface()
        {
            var data = _dataManager.DiskData;

            try
            {
                _window.Dispatcher.Invoke(new Action(() =>
                {
                    try
                    {
                        for (int i = 0; i < data.Count; i++)
                        {
                            var d = data[i];

                            var diskNameLabel = (Label)_window.FindName($"Disk{i+1}Name");
                            diskNameLabel.Content = d.Name ?? "No drive detected";
                            var diskLoadLabel = (Label)_window.FindName($"Disk{i+1}Load");
                            ChangeColorAndUpdateText(diskLoadLabel, d.Used, DataType.HddUsage);
                            var diskTempLabel = (Label)_window.FindName($"Disk{i+1}Temp");
                            ChangeColorAndUpdateText(diskTempLabel, d.Temperature, DataType.HddTemp);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Error occured while updating DiskData interface: " + e.Message);
                    }
                }));
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error occured while invoking disk interface. " + e.Message);
            }
        }

        private void ResetInterface(DependencyObject dependencyObject)
        {
            try
            {
                _window.Dispatcher.Invoke(() =>
                    {
                        try
                        {
                            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
                            {
                                var child = VisualTreeHelper.GetChild(dependencyObject, i);

                                if (child is Label)
                                {
                                    if (((Label)child).Name.Contains("Name"))
                                    {
                                        ((Label)child).Content = "No data";
                                    }
                                    else if (((Label)child).Name == null || ((Label)child).Name == "")
                                    {

                                    }
                                    else
                                    {
                                        ((Label)child).Content = "-";
                                    }
                                }
                                else
                                {
                                    ResetInterface(child);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine("Error occured while cleaning interface. " + e.Message);
                        }
                    });
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error occured while invoking interface. " + e.Message);
            }
        }

        private void ChangeColorAndUpdateText(Label control, float? value, DataType type)
        {
            switch (type)
            {
                case DataType.CpuTemp:
                    if (value < Config.AppConfig.Cpu.TempYellowThreshold || value == null)
                    {
                        control.Foreground = Brushes.Lime;
                    }
                    else if (value < Config.AppConfig.Cpu.TempRedThreshold)
                    {
                        control.Foreground = Brushes.Yellow;
                    }
                    else
                    {
                        control.Foreground = Brushes.Red;
                    }
                    control.Content = value != null ? value.ToString() : "-";
                    break;
                case DataType.CpuUsage:
                    if (value < Config.AppConfig.Cpu.UsageYellowThreshold || value == null)
                    {
                        control.Foreground = Brushes.Lime;
                    }
                    else if (value < Config.AppConfig.Cpu.UsageRedThreshold)
                    {
                        control.Foreground = Brushes.Yellow;
                    }
                    else
                    {
                        control.Foreground = Brushes.Red;
                    }
                    control.Content = value != null ? $"{value:0.00}%" : "-";
                    break;
                case DataType.Memory:
                    if (value < Config.AppConfig.Memory.UsageYellowThreshold || value == null)
                    {
                        control.Foreground = Brushes.Lime;
                    }
                    else if (value < Config.AppConfig.Memory.UsageRedThreshold)
                    {
                        control.Foreground = Brushes.Yellow;
                    }
                    else
                    {
                        control.Foreground = Brushes.Red;
                    }
                    break;
                case DataType.GpuTemp:
                    if (value < Config.AppConfig.Gpu.TempYellowThreshold || value == null)
                    {
                        control.Foreground = Brushes.Lime;
                    }
                    else if (value < Config.AppConfig.Gpu.TempRedThreshold)
                    {
                        control.Foreground = Brushes.Yellow;
                    }
                    else
                    {
                        control.Foreground = Brushes.Red;
                    }
                    control.Content = value != null ? $"{value}°C" : "-";
                    break;
                case DataType.HddTemp:
                    if (value < Config.AppConfig.Hdd.TempYellowThreshold || value == null)
                    {
                        control.Foreground = Brushes.Lime;
                    }
                    else if (value < Config.AppConfig.Hdd.TempRedThreshold)
                    {
                        control.Foreground = Brushes.Yellow;
                    }
                    else
                    {
                        control.Foreground = Brushes.Red;
                    }
                    control.Content = value != null ? $"{value}°C" : "-";
                    break;
                case DataType.HddUsage:
                    if (value < Config.AppConfig.Hdd.UsageYellowThreshold || value == null)
                    {
                        control.Foreground = Brushes.Lime;
                    }
                    else if (value < Config.AppConfig.Hdd.UsageRedThreshold)
                    {
                        control.Foreground = Brushes.Yellow;
                    }
                    else
                    {
                        control.Foreground = Brushes.Red;
                    }
                    control.Content = value != null ? $"{value:0.00}%" : "-";
                    break;
                default:
                    break;
            }
        }
    }
}
