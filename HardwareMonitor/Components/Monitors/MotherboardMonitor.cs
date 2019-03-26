using HardwareMonitor.Models.Monitors;
using OpenHardwareMonitor.Hardware;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace HardwareMonitor.Components.Monitors
{
    public class MotherboardMonitor
    {
        public MotherboardData Data { get; set; }

        public MotherboardMonitor()
        {
            Data = new MotherboardData
            {
                Data = new List<MotherboardData.InnerData>()
            };
        }

        public void UpdateInfo()
        {
            var hardware = ComputerManager.Computer.Hardware.FirstOrDefault(e => e.HardwareType == HardwareType.Mainboard);

            if (hardware == null)
            {
                return;
            }

            hardware.Update();

            Data.Name = hardware.Name;
            Data.Data.Clear();

            foreach (IHardware subHardware in hardware.SubHardware)
            {
                subHardware.Update();
                var i = 0;
                foreach (ISensor sensor in subHardware.Sensors.OrderBy(e => e.Name))
                {
                    Data.Data.Add(new MotherboardData.InnerData
                    {
                        Id = ++i,
                        Key = $"{subHardware.Name} {sensor.Name}",
                        Value = sensor.Value.ToString()
                    });
                }                               
            }

            Debug.WriteLine(Data.ToString());
        }
    }
}
