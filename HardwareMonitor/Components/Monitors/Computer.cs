using OpenHardwareMonitor.Hardware;

namespace HardwareMonitor.Components.Monitors
{
    public class ComputerManager
    {
        public static Computer Computer { get; set; }

        public static void Initialize()
        {
            Computer = new Computer() { CPUEnabled = true, GPUEnabled = true, RAMEnabled = true };
            Computer.Open();
        }
    }
}
