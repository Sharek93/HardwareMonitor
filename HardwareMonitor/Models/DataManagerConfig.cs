namespace HardwareMonitor.Models
{
    public class DataManagerConfig
    {
        public bool MonitorCpu { get; set; } = false;
        public bool MonitorGpu { get; set; } = false;
        public bool MonitorMemory { get; set; } = false;
        public bool MonitorDisks { get; set; } = false;

        public override string ToString()
        {
            return $"Loaded Data Manager Config:\nMonitorCpu: {MonitorCpu}\nMonitorGpu: {MonitorGpu}\nMonitorMemory: {MonitorMemory}\nMonitorDisks: {MonitorDisks}";
        }
    }
}
