namespace HardwareMonitor.Models
{
    public class AppConfig
    {
        public bool StartTopMost { get; set; }
        public int Opacity { get; set; }
        public CpuDisplayConfig Cpu { get; set; }
        public MemoryDisplayConfig Memory { get; set; }
        public GpuDisplayConfig Gpu { get; set; }
        public HddDisplayConfig Hdd { get; set; }

        public override string ToString()
        {
            return $"Loaded config:\n" +
                $"StartTopMost: {StartTopMost}\n" +
                $"Opacity: {Opacity}\n" +
                $"{Cpu.ToString()}\n" +
                $"{Memory.ToString()}\n" +
                $"{Gpu.ToString()}\n" +
                $"{Hdd.ToString()}";
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

        public class HddDisplayConfig
        {
            public int TempYellowThreshold { get; set; }
            public int TempRedThreshold { get; set; }
            public int UsageYellowThreshold { get; set; }
            public int UsageRedThreshold { get; set; }

            public override string ToString()
            {
                return $"HDD display config:\n" +
                    $"HddTempYellowThreshold: {TempYellowThreshold}\n" +
                    $"HddTempRedThreshold: {TempRedThreshold}\n" +
                    $"HddUsageYellowThreshold: {UsageYellowThreshold}\n" +
                    $"HddUsageRedThreshold: {UsageRedThreshold}";
            }
        }
    }
}
