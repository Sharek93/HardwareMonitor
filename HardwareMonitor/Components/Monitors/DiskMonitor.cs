using HardwareMonitor.Models.Monitors;
using OpenHardwareMonitor.Hardware;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace HardwareMonitor.Components.Monitors
{
    public class DiskMonitor
    {
        public List<DiskData> Data { get; set; }

        public DiskMonitor()
        {
            Data = new List<DiskData>();
        }

        public void UpdateInfo()
        {
            var hardwares = ComputerManager.Computer.Hardware.Where(e => e.HardwareType == HardwareType.HDD).ToList();

            if (hardwares == null)
            {
                return;
            }

            Data.Clear();

            foreach (var hardware in hardwares)
            {
                hardware.Update();
                foreach (IHardware subHardware in hardware.SubHardware)
                    subHardware.Update();

                var diskData = new DiskData
                {
                    Name = hardware.Name
                };

                foreach (var sensor in hardware.Sensors)
                {                    
                    if (sensor.SensorType == SensorType.Load)
                    {
                        diskData.Used = sensor.Value;
                    }
                    if (sensor.SensorType == SensorType.Temperature)
                    {
                        diskData.Temperature = sensor.Value;
                    }
                }
                Debug.WriteLine(diskData.ToString());
                Data.Add(diskData);
            }            
        }
    }
}
