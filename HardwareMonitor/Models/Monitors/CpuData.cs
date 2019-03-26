namespace HardwareMonitor.Models.Monitors
{
    public class CpuData
    {
        public CpuTemperature Temperature { get; set; }
        public CpuLoad Load { get; set; }
        public string Name { get; set; }

        public class CpuTemperature
        {
            public int? Core1 { get; set; }
            public int? Core2 { get; set; }
            public int? Core3 { get; set; }
            public int? Core4 { get; set; }
            public int? Total { get; set; }

            public override string ToString()
            {
                return $"Cpu cores temperature [#1: {Core1}%, #2: {Core2}%, #3: {Core3}%, #4: {Core4}%] average: {Total}%";
            }
        }

        public class CpuLoad
        {
            public float? Core1 { get; set; }
            public float? Core2 { get; set; }
            public float? Core3 { get; set; }
            public float? Core4 { get; set; }
            public float? Total { get; set; }

            public override string ToString()
            {
                return $"Cpu cores load [#1: {Core1:0.00}%, #2: {Core2:0.00}%, #3: {Core3:0.00}%, #4: {Core4:0.00}%] total: {Total:0.00}%";
            }
        }

        public override string ToString()
        {
            return $"CPU STATS:\nName: {Name}\n{Temperature.ToString()}\n{Load.ToString()}";
        }
    }
}
