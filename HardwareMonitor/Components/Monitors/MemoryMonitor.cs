using HardwareMonitor.Models.Monitors;
using OpenHardwareMonitor.Hardware;
using System.Diagnostics;
using System.Linq;

namespace HardwareMonitor.Components.Monitors
{
    public class MemoryMonitor
    {
        public MemoryData Data { get; set; }

        public MemoryMonitor()
        {
            Data = new MemoryData();
        }

        public void UpdateInfo()
        {
            var hardware = ComputerManager.Computer.Hardware.FirstOrDefault(e => e.HardwareType == HardwareType.RAM);

            if (hardware == null)
            {
                return;
            }

            hardware.Update();
            foreach (IHardware subHardware in hardware.SubHardware)
                subHardware.Update();

            Data.Name = hardware.Name;

            foreach (var sensor in hardware.Sensors)
            {
                if (sensor.Name == "Available Memory")
                {
                    Data.MemoryAvailable = sensor.Value;
                }
                if (sensor.Name == "Used Memory")
                {
                    Data.MemoryUsed = sensor.Value;
                }
                if (sensor.Name == "Memory")
                {
                    Data.MemoryUsePercent = sensor.Value;
                }
            }

            Data.MemoryTotal = Data.MemoryAvailable + Data.MemoryUsed;
            Debug.WriteLine(Data.ToString());
        }
    }
}
