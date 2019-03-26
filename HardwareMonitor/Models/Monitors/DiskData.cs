namespace HardwareMonitor.Models.Monitors
{
    public class DiskData
    {
        public string Name { get; set; }
        public float? Used { get; set; }
        public float? Temperature { get; set; }

        public override string ToString()
        {
            var temp = Temperature != null ? Temperature.ToString() : "no data";
            return $"DISK {Name} DATA:\n" +
                $"Storage used: {Used}\n" +
                $"Temperature: {temp}";
        }
    }
}
