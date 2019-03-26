namespace HardwareMonitor.Models.Monitors
{
    public class GpuData
    {
        public string Name { get; set; }
        public float? Temperature { get; set; }
        public float? FanSpeed { get; set; }
        public float? CoreClock { get; set; }
        public float? MemoryClock { get; set; }
        public float? ShaderClock { get; set; }
        public float? CoreLoad { get; set; }
        public float? MemoryControlLoad { get; set; }
        public float? VideoEngineLoad { get; set; }
        public float? FanControll { get; set; }
        public float? MemoryTotal { get; set; }
        public float? MemoryUsed { get; set; }
        public float? MemoryFree { get; set; }
        public float? MemoryLoad { get; set; }

        public override string ToString()
        {
            return $"GPU STATUS:\n" +
                $"Name: {Name}\n" +
                $"Temperature: {Temperature}C\n" +
                $"FanControll: {FanControll} [FanSpeed: {FanSpeed}RPM]\n" +
                $"CoreLoad: {CoreLoad}% [CoreClock: {CoreClock}MHz]\n" +
                $"MemoryControlLoad: {MemoryControlLoad}% [MemoryClock: {MemoryClock}MHz]\n" +
                $"VideoEngineLoad: {VideoEngineLoad}% [ShaderClock: {ShaderClock}MHz]\n" +
                $"MemoryTotal: {MemoryTotal}MB\n" +
                $"MemoryUsed: {MemoryUsed:0}MB ({MemoryLoad:0.00}%)\n" +
                $"MemoryFree: {MemoryFree:0}MB";
        }
    }
}
