using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SROMCapi.Models;

namespace SROMCapi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class User : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public UserInfo Create()
        {
            string RandomUsername = Tools.CreateUnique(false, 16, true);
            string RandomPassword = Tools.CreateUnique(false, 10, false);
            string Token = Tools.CreateUnique(false, 64, false);

            var User = DatabaseController.AddUser(RandomUsername, RandomPassword, Token);

            return new UserInfo
            {
                Id = -1,
                Username = User.Username,
                Password = User.Password,
                Token = User.Token
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="JSON"></param>
        /// <returns></returns>
        [HttpPost]
        public UserInfo Login([FromBody] Models.UserInfo JSON)
        {
            string Username = JSON.Username;
            string Password = JSON.Password;

            var User = DatabaseController.UserLogin(Username, Password);

            return new UserInfo
            {
                Id = -1,
                Username = User.Username,
                Password = User.Password,
                Token = User.Token
            };
        }
    }
}
