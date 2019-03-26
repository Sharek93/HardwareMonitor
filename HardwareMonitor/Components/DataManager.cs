using HardwareMonitor.Components.Monitors;
using HardwareMonitor.Models;
using HardwareMonitor.Models.Monitors;
using HardwareMonitor.Resources;
using System.Collections.Generic;
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
        public GpuData GpuData => _gpuMonitor.Data ?? null;
        public List<DiskData> DiskData => _diskMonitor.Data ?? null;
        private readonly CpuMonitor _cpuMonitor;
        private readonly MemoryMonitor _memoryMonitor;
        private readonly GpuMonitor _gpuMonitor;
        private readonly DiskMonitor _diskMonitor;
        public DataManagerConfig Config { get; }

        public DataManager(DataManagerConfig config)
        {
            Debug.WriteLine(config.ToString());
            Config = config;
            ComputerManager.Initialize(config);
            if (config.MonitorCpu)
            {
                _cpuMonitor = new CpuMonitor();
            }
            if (config.MonitorGpu)
            {
                _gpuMonitor = new GpuMonitor();
            }
            if (config.MonitorMemory)
            {
                _memoryMonitor = new MemoryMonitor();
            }
            if (config.MonitorDisks)
            {
                _diskMonitor = new DiskMonitor();
            }
        }

        public void Start()
        {
            IsRunning = true;
            AppInfo.IsRunning = true;
            Work();
        }

        public void Stop()
        {
            AppInfo.IsRunning = false;
            IsRunning = false;
        }

        private void Work()
        {
            Task.Run(() => {
                do
                {
                    if (Config.MonitorCpu)
                    {
                        _cpuMonitor.UpdateInfo();
                    }
                    if (Config.MonitorGpu)
                    {
                        _gpuMonitor.UpdateInfo();
                    }
                    if (Config.MonitorMemory)
                    {
                        _memoryMonitor.UpdateInfo();
                    }
                    if (Config.MonitorDisks)
                    {
                        _diskMonitor.UpdateInfo();
                    }
                    Thread.Sleep(1000);
                } while (IsRunning);
            });
        }
    }
}
