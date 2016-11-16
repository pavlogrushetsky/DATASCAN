using System;

namespace DATASCAN.Communication.Common
{
    public class ModemLogEntry
    {
        public ModemStatus Status { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        public string Port { get; set; } = "";

        public string Message { get; set; }
    }
}