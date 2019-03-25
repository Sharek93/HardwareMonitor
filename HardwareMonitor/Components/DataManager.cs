using HardwareMonitor.Components.Monitors;
using HardwareMonitor.Models;
using HardwareMonitor.Models.Monitors;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace HardwareMonitor.Components
{
    public class DataManager
    {
        public bool IsRunning { get; private set; } = false;
        public CpuData CpuData => _cpuMonitor.Data ?? throw new Exception("CPU monitor not initialize");
        public MemoryData MemoryData => _memoryMonitor.Data ?? throw new Exception("RAM monitor not initialize");
        public GpuData GpuData => _gpuMonitor.Data ?? throw new Exception("GPU monitor not initialize");
        private readonly CpuMonitor _cpuMonitor;
        private readonly MemoryMonitor _memoryMonitor;
        private readonly GpuMonitor _gpuMonitor;
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
                _gpuMonitor = new GpuMonitor();
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
                        _gpuMonitor.UpdateInfo();
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
