namespace HardwareMonitor.Models
{
    public class AppConfig
    {
        public CpuDisplayConfig Cpu { get; set; }
        public MemoryDisplayConfig Memory { get; set; }
        public GpuDisplayConfig Gpu { get; set; }

        public override string ToString()
        {
            return $"Loaded config:\n" +
                $"{Cpu.ToString()}\n" +
                $"{Memory.ToString()}\n" +
                $"{Gpu.ToString()}";
        }

        public class CpuDisplayConfig
        {
            public int TempYellowThreshold { get; set; }
            public int TempRedThreshold { get; set; }
            public int UsageYellowThreshold { get; set; }
            public int UsageRedThreshold { get; set; }

            public override string ToString()
            {
                return $"CPU display config:\n" +
                    $"CpuTempYellowThreshold: {TempYellowThreshold}\n" +
                    $"CpuTempRedThreshold: {TempRedThreshold}\n" +
                    $"CpuUsageYellowThreshold: {UsageYellowThreshold}\n" +
                    $"CpuUsageRedThreshold: {UsageRedThreshold}";
            }
        } 
        
        public class MemoryDisplayConfig
        {
            public int UsageYellowThreshold { get; set; }
            public int UsageRedThreshold { get; set; }

            public override string ToString()
            {
                return $"MEMORY display config:\n" +
                    $"MemoryUsageYellowThreshold: {UsageYellowThreshold}\n" +
                    $"MemoryUsageRedThreshold: {UsageRedThreshold}";
            }
        }

        public class GpuDisplayConfig
        {
            public int TempYellowThreshold { get; set; }
            public int TempRedThreshold { get; set; }

            public override string ToString()
            {
                return $"GPU display config:\n" +
                    $"GpuTempYellowThreshold: {TempYellowThreshold}\n" +
                    $"GpuTempRedThreshold: {TempRedThreshold}";
            }
        }
    }
}
