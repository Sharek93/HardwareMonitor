using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            _window.Dispatcher.Invoke(new Action(() => {
                try
                {
                    _window.Core1Usage.Content = data.Load.Core1.ToString("0.00");
                    _window.Core2Usage.Content = data.Load.Core2.ToString("0.00");
                    _window.Core3Usage.Content = data.Load.Core3.ToString("0.00");
                    _window.Core4Usage.Content = data.Load.Core4.ToString("0.00");
                    _window.TotalUsage.Content = data.Load.Total.ToString("0.00");

                    _window.Core1Temp.Content = data.Temperature.Core1.ToString();
                    _window.Core2Temp.Content = data.Temperature.Core2.ToString();
                    _window.Core3Temp.Content = data.Temperature.Core3.ToString();
                    _window.Core4Temp.Content = data.Temperature.Core4.ToString();
                    _window.TotalTemp.Content = data.Temperature.Total.ToString();
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error occured while updating CpuData interface: " + e.Message);                   
                }
            }));
        }
    }
}
