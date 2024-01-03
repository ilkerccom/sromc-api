using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SROMCapi.Models
{
    public class Task
    {
        public string Token { get; set; } 

        public string CharId { get; set; }

        public string Command { get; set; } = "";

        public string Arg1 { get; set; } = "";

        public string Arg2 { get; set; } = "";

        public string Arg3 { get; set; } = "";
    }
}
