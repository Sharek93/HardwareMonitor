using HardwareMonitor.Models;
using HardwareMonitor.Resources;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;

namespace HardwareMonitor.Helpers
{
    public static class Config
    {
        private const string ConfigFileName = "AppConfig.json";
        public static AppConfig AppConfig { get; set; }

        public static bool LoadConfig()
        {
            try
            {
                AppConfig = new AppConfig
                {
                    Cpu = new AppConfig.CpuDisplayConfig(),
                    Memory = new AppConfig.MemoryDisplayConfig(),
                    Gpu = new AppConfig.GpuDisplayConfig(),
                    Hdd = new AppConfig.HddDisplayConfig()
                };
                var configString = File.ReadAllText(ConfigFileName);
                AppConfig = JsonConvert.DeserializeObject<AppConfig>(configString);
                Debug.WriteLine(AppConfig.ToString());
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Could not load config file: " + e.Message);
                return false;
            }
        }

        public static bool SaveConfig()
        {
            try
            {
                var configString = JsonConvert.SerializeObject(AppConfig, Formatting.Indented);
                File.WriteAllText(ConfigFileName, configString);
                Debug.WriteLine("Config saved.\n" + AppConfig.ToString());
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Could not save config file: " + e.Message);
                return false;
            }
        }
    }
}
