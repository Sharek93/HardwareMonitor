namespace HardwareMonitor.Models
{
    public class AppConfig
    {
        public CpuDisplayConfig Cpu { get; set; }
        public MemoryDisplayConfig Memory { get; set; }

        public override string ToString()
        {
            return $"Loaded config:\n" +
                $"{Cpu.ToString()}\n" +
                $"{Memory.ToString()}";
        }

        public class CpuDisplayConfig
        {
            public int CpuTempYellowThreshold { get; set; }
            public int CpuTempRedThreshold { get; set; }
            public int CpuUsageYellowThreshold { get; set; }
            public int CpuUsageRedThreshold { get; set; }

            public override string ToString()
            {
                return $"CPU display config:\n" +
                    $"CpuTempYellowThreshold: {CpuTempYellowThreshold}\n" +
                    $"CpuTempRedThreshold: {CpuTempRedThreshold}\n" +
                    $"CpuUsageYellowThreshold: {CpuUsageYellowThreshold}\n" +
                    $"CpuUsageRedThreshold: {CpuUsageRedThreshold}";
            }
        } 
        
        public class MemoryDisplayConfig
        {
            public int MemoryUsageYellowThreshold { get; set; }
            public int MemoryUsageRedThreshold { get; set; }

            public override string ToString()
            {
                return $"MEMORY display config:\n" +
                    $"MemoryUsageYellowThreshold: {MemoryUsageYellowThreshold}\n" +
                    $"MemoryUsageRedThreshold: {MemoryUsageRedThreshold}";
            }
        }
    }
}
