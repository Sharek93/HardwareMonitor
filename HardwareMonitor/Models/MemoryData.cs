namespace HardwareMonitor.Models
{
    public class MemoryData
    {
        public string Name { get; set; }
        public float MemoryUsed { get; set; }
        public float MemoryAvailable { get; set; }
        public float MemoryTotal { get; set; }
        public float MemoryUsePercent { get; set; }

        public override string ToString()
        {
            return $"MEMORY STATUS:\n" +
                $"Name: {Name}\n" +
                $"Total: {MemoryTotal}GB\n" +
                $"Used: {MemoryUsed}GB ({MemoryUsePercent}%)\n" +
                $"Available: {MemoryAvailable}GB";
        }
    }
}
