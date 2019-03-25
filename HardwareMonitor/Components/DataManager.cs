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
        public CpuData CpuData => _cpuMonitor.Data ?? null;
        public MemoryData MemoryData => _memoryMonitor.Data ?? null;
        private readonly CpuMonitor _cpuMonitor;
        private readonly MemoryMonitor _memoryMonitor;        
        private readonly DataManagerConfig _config;

        public DataManager(DataManagerConfig config)
        {
            Debug.WriteLine(config.ToString());
            _config = config;
            ComputerManager.Initialize(config);
            if (config.MonitorCpu)
            {
                _cpuMonitor = new CpuMonitor();
            }
            if (config.MonitorGpu)
            {

            }
            if (config.MonitorMemory)
            {
                _memoryMonitor = new MemoryMonitor();
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
                    if (_config.MonitorGpu)
                    {

                    }
                    if (_config.MonitorMemory)
                    {
                        _memoryMonitor.UpdateInfo();
                    }
                    if (_config.MonitorDisks)
                    {

                    }
                    Thread.Sleep(1000);
                } while (IsRunning);
            });
        }
    }
}
