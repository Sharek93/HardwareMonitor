using HardwareMonitor.Models.Monitors;
using OpenHardwareMonitor.Hardware;
using System.Diagnostics;
using System.Linq;

namespace HardwareMonitor.Components.Monitors
{
    public class GpuMonitor
    {
        public GpuData Data { get; set; }

        public GpuMonitor()
        {
            Data = new GpuData();
        }

        public void UpdateInfo()
        {
            var hardware = ComputerManager.Computer.Hardware.FirstOrDefault(e => e.HardwareType == HardwareType.GpuNvidia);

            hardware.Update();
            foreach (IHardware subHardware in hardware.SubHardware)
                subHardware.Update();

            Data.Name = hardware.Name;

            foreach (var sensor in hardware.Sensors)
            {
                if (sensor.SensorType == SensorType.Temperature)
                {
                    Data.Temperature = sensor.Value ?? 0;
                }
                if (sensor.SensorType == SensorType.Fan)
                {
                    Data.FanSpeed = sensor.Value ?? 0;
                }
                if (sensor.SensorType == SensorType.Clock)
                {
                    switch (sensor.Name)
                    {
                        case "GPU Core":
                            Data.CoreClock = sensor.Value ?? 0;
                            break;
                        case "GPU Memory":
                            Data.MemoryClock = sensor.Value ?? 0;
                            break;
                        case "GPU Shader":
                            Data.ShaderClock = sensor.Value ?? 0;
                            break;
                        default:
                            break;
                    }
                }
                if (sensor.SensorType == SensorType.Load)
                {
                    switch (sensor.Name)
                    {
                        case "GPU Core":
                            Data.CoreLoad = sensor.Value ?? 0;
                            break;
                        case "GPU Memory Controller":
                            Data.MemoryControlLoad = sensor.Value ?? 0;
                            break;
                        case "GPU Video Engine":
                            Data.VideoEngineLoad = sensor.Value ?? 0;
                            break;
                        case "GPU Memory":
                            Data.MemoryLoad = sensor.Value ?? 0;
                            break;
                        default:
                            break;
                    }
                }
                if (sensor.SensorType == SensorType.Control)
                {
                    Data.FanControll = sensor.Value ?? 0;
                }
                if (sensor.SensorType == SensorType.SmallData)
                {
                    switch (sensor.Name)
                    {
                        case "GPU Memory Total":
                            Data.MemoryTotal = sensor.Value ?? 0;
                            break;
                        case "GPU Memory Used":
                            Data.MemoryUsed = sensor.Value ?? 0;
                            break;
                        case "GPU Memory Free":
                            Data.MemoryFree = sensor.Value ?? 0;
                            break;
                        default:
                            break;
                    }
                }
            }

            Debug.WriteLine(Data.ToString());
        }
    }
}
