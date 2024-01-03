using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SROMCapi.Models
{
    public class Char
    {
        public bool Success { get; set; }

        public string CharId { get; set; }

        public Task Tasks { get; set; }

        public string MobileLastActionDate { get; set; }
    }
}
