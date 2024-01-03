using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SROMCapi.Models
{
    public class AccountInfo
    {
        public string CharId { get; set; } = "0";

        public string Password { get; set; }

        public string Token { get; set; }

        public string Server { get; set; }

        public string CharName { get; set; }

        public string PlayerId { get; set; }
    }
}
