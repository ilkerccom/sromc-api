using System;

namespace SROMCapi.Models
{
    public class Server
    {
        public string Game { get; set; }

        public string ServerName { get; set; }

        public int ServerCapacity { get; set; }

        public bool ServerStatus { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}
