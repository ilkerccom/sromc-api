using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SROMCapi.Models
{
    public class Info
    {
        public string charId { get; set; }
        public string token { get; set; }
        public string status { get; set; }
        public string game { get; set; } = "*";
        public string character { get; set; }
        public string inventory { get; set; }
        public string storage { get; set; }
        public string storage_guild { get; set; }
        public string quests { get; set; }
        public string drops { get; set; }
        public string guild { get; set; }
        public string guild_union { get; set; }
        public string party { get; set; }
        public string monsters { get; set; }
        public string pets { get; set; }
        public string academy { get; set; }
        public string messages { get; set; }
        public string players { get; set; }
        public string taxi { get; set; }
        public string skills { get; set; }
        public string taskCompleted { get; set; }
    }
}
