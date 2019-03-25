using HardwareMonitor.Components.Monitors;
using HardwareMonitor.Models;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace HardwareMonitor.Components
{
    public class DataManager
    {
        public bool IsRunning { get; private set; } = false;
        public CpuData CpuData => _cpuMonitor.Data;
        private readonly CpuMonitor _cpuMonitor;
        private readonly DataManagerConfig _config;

        public DataManager(DataManagerConfig config)
        {
            Debug.WriteLine(config.ToString());
            _config = config;
            ComputerManager.Initialize();
            if (config.MonitorCpu)
            {
                _cpuMonitor = new CpuMonitor();
            }
            if (config.MonitorGpu)
            {

            }
            if (config.MonitorMemory)
            {

            }
            if (config.MonitorDisks)
            {

            }
        }


        public void Start()
        {
            IsRunning = true;
            Work();
        }

        public void Stop()
        {
            IsRunning = false;
        }

        private void Work()
        {
            Task.Run(() => {
                do
                {
                    if (_config.MonitorCpu)
                    {
                        _cpuMonitor.UpdateInfo();
                    }
                    Thread.Sleep(1000);
                } while (IsRunning);
            });
        }
    }
}
