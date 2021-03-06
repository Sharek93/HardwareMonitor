﻿using System.Diagnostics;
using System.Linq;
using HardwareMonitor.Models.Monitors;
using OpenHardwareMonitor.Hardware;

namespace HardwareMonitor.Components.Monitors
{
    public class CpuMonitor
    {
        public CpuData Data { get; set; }

        public CpuMonitor()
        {
            Data = new CpuData
            {
                Temperature = new CpuData.CpuTemperature(),
                Load = new CpuData.CpuLoad()
            };
        }

        public void UpdateInfo()
        {
            var hardware = ComputerManager.Computer.Hardware.FirstOrDefault(e => e.HardwareType == HardwareType.CPU);

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
                if (sensor.SensorType == SensorType.Load)
                {
                    if (sensor.Name.Contains("#1"))
                    {
                        Data.Load.Core1 = sensor.Value;
                    }
                    if (sensor.Name.Contains("#2"))
                    {
                        Data.Load.Core2 = sensor.Value;
                    }
                    if (sensor.Name.Contains("#3"))
                    {
                        Data.Load.Core3 = sensor.Value;
                    }
                    if (sensor.Name.Contains("#4"))
                    {
                        Data.Load.Core4 = sensor.Value;
                    }
                    if (sensor.Name.Contains("Total"))
                    {
                        Data.Load.Total = sensor.Value;
                    }

                    //Debug.WriteLine($"{hardware.Name} {sensor.Name} Clock: {sensor.Value}");
                }
                if (sensor.SensorType == SensorType.Temperature)
                {
                    if (sensor.Name.Contains("#1"))
                    {
                        Data.Temperature.Core1 = (int)sensor.Value;
                    }
                    if (sensor.Name.Contains("#2"))
                    {
                        Data.Temperature.Core2 = (int)sensor.Value;
                    }
                    if (sensor.Name.Contains("#3"))
                    {
                        Data.Temperature.Core3 = (int)sensor.Value;
                    }
                    if (sensor.Name.Contains("#4"))
                    {
                        Data.Temperature.Core4 = (int)sensor.Value;
                    }
                    if (sensor.Name.Contains("Package"))
                    {
                        Data.Temperature.Total = (int)sensor.Value;
                    }

                    //Debug.WriteLine($"{hardware.Name} {sensor.Name} Temperature: {sensor.Value}");
                }
            }

            Debug.WriteLine(Data.ToString());
        }
    }
}
