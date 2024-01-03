using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SROMCapi.Models;

namespace SROMCapi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Account : ControllerBase
    {
        /// <summary>
        /// Create an char account
        /// </summary>
        /// <param name="JSON"></param>
        /// <returns></returns>
        [HttpPost]
        public AccountInfo Create([FromBody] AccountInfo JSON)
        {
            // Info
            string Password = JSON.Password;
            string PlayerId = JSON.PlayerId;
            string CharName = JSON.CharName;
            string Server = JSON.Server;
            string Token = Tools.CreateUnique(false, 64);

            // Exists?
            int CharId = DatabaseController.CharIsExists(Server, CharName);

            // Update if exists
            if(CharId >= 1)
            {
                var Update = DatabaseController.UpdateChar(Password, Token, CharName, Server, CharId);
                if (Update.CharId != "0")
                {
                    return new AccountInfo
                    {
                        CharId = Update.CharId, // Create new
                        Token = Update.Token, // Created token
                        CharName = Update.CharName,
                        Server = Update.Server,
                        PlayerId = Update.PlayerId
                    };
                }
            }

            // Create account if doesnt exists
            var Add = DatabaseController.AddChar(Password, Token, CharName, Server, PlayerId);
            if(Add.CharId != "0")
            {
                return new AccountInfo
                {
                    CharId = Add.CharId, // Create new
                    Token = Add.Token, // Created token
                    CharName = Add.CharName,
                    Server = Add.Server,
                    PlayerId = Add.PlayerId
                };
            }

            // Return back if error
            return new AccountInfo { };
        }
    }
}
