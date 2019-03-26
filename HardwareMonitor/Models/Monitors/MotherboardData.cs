using System.Collections.Generic;
using System.Text;

namespace HardwareMonitor.Models.Monitors
{
    public class MotherboardData
    {
        public string Name { get; set; }
        public List<InnerData> Data { get; set; }

        public class InnerData
        {
            public int Id { get; set; }
            public string Key { get; set; }
            public string Value { get; set; }
        }

        public override string ToString()
        {
            var returnString = new StringBuilder();
            returnString.Append($"MOTHERBOARD DATA:\nName: {Name}\n");
            for (int i = 0; i < Data.Count; i++)
            {
                InnerData item = Data[i];
                returnString.Append($"[{item.Id:00}]{item.Key}: {item.Value}\n");
            }
            return returnString.ToString();
        }
    }
}
