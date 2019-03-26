namespace HardwareMonitor.Models
{
    public class DataManagerConfig
    {
        public bool MonitorCpu { get; set; } = false;
        public bool MonitorGpu { get; set; } = false;
        public bool MonitorMemory { get; set; } = false;
        public bool MonitorDisks { get; set; } = false;
        public bool MonitorFans { get; set; } = false;
        public bool MonitorMotherboard { get; set; } = false;

        public override string ToString()
        {
            return $"Loaded Data Manager Config:\n" +
                $"MonitorCpu: {MonitorCpu}\n" +
                $"MonitorGpu: {MonitorGpu}\n" +
                $"MonitorMemory: {MonitorMemory}\n" +
                $"MonitorDisks: {MonitorDisks}\n" +
                $"MonitorFans: {MonitorFans}\n" +
                $"MonitorMotherboard: {MonitorMotherboard}";
        }
    }
}
