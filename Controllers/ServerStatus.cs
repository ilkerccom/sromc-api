using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SROMCapi.Models;

namespace SROMCapi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ServerStatus
    {
        /// <summary>
        /// Get servers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Models.Server> Get()
        {
            return DatabaseController.GetServers();
        }
    }
}
