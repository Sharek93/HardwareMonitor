using HardwareMonitor.Enums;
using HardwareMonitor.Helpers;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace HardwareMonitor.Components
{
    public class InterfaceManager
    {
        private readonly MainWindow _window;
        private readonly DataManager _dataManager;
        public bool IsRunning { get; private set; } = false;

        public InterfaceManager(MainWindow window, DataManager dataManager)
        {
            _window = window;
            _dataManager = dataManager;
            IsRunning = false;
        }

        public void Start()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                Run();
            }
        }

        public void Stop()
        {
            IsRunning = false;
        }

        private void Run()
        {
            Task.Run(() => {
                while(IsRunning)
                {
                    UpdateCpuInterface();
                    Thread.Sleep(1000);
                }
            });
        }

        private void UpdateCpuInterface()
        {
            var data = _dataManager.CpuData;
            try
            {
                _window.Dispatcher.Invoke(new Action(() => {
                    try
                    {
                        _window.CpuName.Content = data.Name;

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
                Debug.WriteLine("Error occured while invoking. " + e.Message);
            }

        }

        private void ChangeColorAndUpdateText(Label control, float value, DataType type)
        {
            switch (type)
            {
                case DataType.CpuTemp:
                    if (value < Config.AppConfig.Cpu.CpuTempYellowThreshold)
                    {
                        control.Foreground = Brushes.Lime;
                    }
                    else if (value < Config.AppConfig.Cpu.CpuTempRedThreshold)
                    {
                        control.Foreground = Brushes.Yellow;
                    }
                    else
                    {
                        control.Foreground = Brushes.Red;
                    }
                    control.Content = value.ToString();
                    break;
                case DataType.CpuUsage:
                    if (value < Config.AppConfig.Cpu.CpuUsageYellowThreshold)
                    {
                        control.Foreground = Brushes.Lime;
                    }
                    else if (value < Config.AppConfig.Cpu.CpuUsageRedThreshold)
                    {
                        control.Foreground = Brushes.Yellow;
                    }
                    else
                    {
                        control.Foreground = Brushes.Red;
                    }
                    control.Content = value.ToString("0.00");
                    break;
                default:
                    break;
            }
        }
    }
}
