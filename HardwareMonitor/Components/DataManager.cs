using HardwareMonitor.Components.Monitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HardwareMonitor.Components
{
    public static class DataManager
    {
        public static bool IsRunning { get; private set; } = false;

        public static void Start()
        {
            IsRunning = true;
            Work();
        }

        public static void Stop()
        {
            IsRunning = false;
        }

        private static void Work()
        {
            Task.Run(() => {
                do
                {
                    CpuMonitor.UpdateInfo();
                    Thread.Sleep(1000);
                } while (IsRunning);
            });
        }
    }
}
