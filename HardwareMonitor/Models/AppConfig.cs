namespace HardwareMonitor.Models
{
    public class AppConfig
    {
        public CpuDisplayConfig Cpu { get; set; }

        public override string ToString()
        {
            return $"Loaded config:\n" +
                $"{Cpu.ToString()}";
        }

        public class CpuDisplayConfig
        {
            public int CpuTempYellowThreshold { get; set; }
            public int CpuTempRedThreshold { get; set; }
            public int CpuUsageYellowThreshold { get; set; }
            public int CpuUsageRedThreshold { get; set; }

            public override string ToString()
            {
                return $"Cpu display config:\n" +
                    $"CpuTempYellowThreshold: {CpuTempYellowThreshold}\n" +
                    $"CpuTempRedThreshold: {CpuTempRedThreshold}\n" +
                    $"CpuUsageYellowThreshold: {CpuUsageYellowThreshold}\n" +
                    $"CpuUsageRedThreshold: {CpuUsageRedThreshold}";
            }
        }        
    }
}
