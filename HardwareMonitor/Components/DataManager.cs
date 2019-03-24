using HardwareMonitor.Components.Monitors;
using HardwareMonitor.Models;
using System.Threading;
using System.Threading.Tasks;

namespace HardwareMonitor.Components
{
    public class DataManager
    {
        public bool IsRunning { get; private set; } = false;
        public CpuData CpuData => _cpuMonitor.Data;
        private readonly CpuMonitor _cpuMonitor;

        public DataManager(DataManagerConfig config)
        {
            ComputerManager.Initialize();
            _cpuMonitor = new CpuMonitor();
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
                    _cpuMonitor.UpdateInfo();
                    Thread.Sleep(1000);
                } while (IsRunning);
            });
        }
    }

    public class DataManagerConfig
    {
        public bool MonitorCpu { get; set; }
        public bool MonitorGpu { get; set; }
        public bool MonitorMemory { get; set; }
        public bool MonitorDisks { get; set; }
    }

}
