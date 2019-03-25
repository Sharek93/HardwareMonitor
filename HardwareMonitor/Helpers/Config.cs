﻿using HardwareMonitor.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareMonitor.Helpers
{
    public static class Config
    {
        public static AppConfig AppConfig { get; set; }

        public static bool LoadConfig()
        {
            try
            {
                AppConfig = new AppConfig
                {
                    Cpu = new AppConfig.CpuDisplayConfig(),
                    Memory = new AppConfig.MemoryDisplayConfig()
                };
                var configString = File.ReadAllText("AppConfig.json");
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
    }
}