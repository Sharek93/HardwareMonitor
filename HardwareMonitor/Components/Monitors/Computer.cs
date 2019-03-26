using HardwareMonitor.Models;
using OpenHardwareMonitor.Hardware;

namespace HardwareMonitor.Components.Monitors
{
    public class ComputerManager
    {
        public static Computer Computer { get; set; }

        public static void Initialize(DataManagerConfig dataManagerConfig)
        {
            Computer = new Computer()
            {
                CPUEnabled = dataManagerConfig.MonitorCpu,
                GPUEnabled = dataManagerConfig.MonitorGpu,
                RAMEnabled = dataManagerConfig.MonitorMemory,
                HDDEnabled = dataManagerConfig.MonitorDisks,
                MainboardEnabled = dataManagerConfig.MonitorMotherboard,
                FanControllerEnabled = dataManagerConfig.MonitorFans
            };
            Computer.Open();
        }
    }
}
