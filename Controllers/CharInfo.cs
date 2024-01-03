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
    public class CharInfo : ControllerBase
    {
        /// <summary>
        /// All game info comes to here as JSON
        /// </summary>
        /// <param name="JSON">Game info</param>
        /// <returns></returns>
        [HttpPost]
        [RequestSizeLimit(100_000_000)]
        public Char Create([FromBody] Info JSON)
        {
            // Parse JSON
            string Token = JSON.token;
            string CharId = JSON.charId;


            // Update only status or all
            if(JSON.game == "*")
                DatabaseController.UpdateDataSimple(Token, CharId, JSON.status);
            else
                DatabaseController.UpdateData(Token, CharId, JSON.status, JSON.character, JSON.inventory, JSON.storage, JSON.quests, JSON.drops, JSON.guild, JSON.party, JSON.monsters, JSON.pets, JSON.academy, JSON.messages, JSON.players, JSON.taxi, JSON.guild_union, JSON.storage_guild, JSON.skills, JSON.game);

            if (JSON.taskCompleted == "True")
                DatabaseController.CompleteTask(CharId);
            
            // Return back
            return new Char
            {
                CharId = CharId,
                Success = true,
                Tasks = DatabaseController.GetTask(CharId),
                MobileLastActionDate = DatabaseController.GetCharLastAction(CharId).ToString("yyyy-MM-ddTHH:mm:ss")
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="JSON"></param>
        /// <returns></returns>
        [HttpPost]
        public TaskResult Add([FromBody] AddChar JSON)
        {
            string Token = JSON.Token;
            string CharId = JSON.CharId;
            string CharPassword = JSON.CharPassword;

            // Check token
            var User = DatabaseController.UserDetail(Token);
            if (User.Token != Token)
            {
                return new TaskResult { Success = false };
            }

            // Char creadatinals is correct
            var Login = DatabaseController.CharLogin(CharId, CharPassword);
            if (!Login)
            {
                return new TaskResult { Success = false };
            }

            var UpdateOwner = DatabaseController.UpdateCharOwner(CharId, CharPassword, User.Id);

            return new TaskResult { Success = UpdateOwner };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="JSON"></param>
        /// <returns></returns>
        [HttpPost]
        public List<Models.MyChar> Get([FromBody] GetChar JSON)
        {
            string Token = JSON.Token;

            // Check token
            var User = DatabaseController.UserDetail(Token);
            if (User.Token != Token)
            {
                return new List<Models.MyChar>();
            }

            return DatabaseController.GetChars(User.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="JSON"></param>
        /// <returns></returns>
        [HttpPost]
        public Info Detail([FromBody] DeleteChar JSON)
        {
            string CharId = JSON.CharId;
            string Token = JSON.Token;

            // Check token
            var User = DatabaseController.UserDetail(Token);
            if (User.Token != Token)
            {
                return new Info { };
            }

            // Check user
            bool CharIsExists = DatabaseController.CheckCharOwner(CharId, User.Id);
            if (!CharIsExists)
            {
                return new Info { };
            }

            // Update mobile last action
            DatabaseController.UpdateLastMobileAction(JSON.CharId);

            return DatabaseController.GetCharInfo(User.Id, CharId);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="JSON"></param>
        /// <returns></returns>
        [HttpPost]
        public TaskResult Delete([FromBody] DeleteChar JSON)
        {
            string CharId = JSON.CharId;
            string Token = JSON.Token;

            // Check token
            var User = DatabaseController.UserDetail(Token);
            if (User.Token != Token)
            {
                return new TaskResult { Success = false };
            }

            // Check user
            bool CharIsExists = DatabaseController.CheckCharOwner(CharId, User.Id);
            if (!CharIsExists)
            {
                return new TaskResult { Success = false };
            }

            return new TaskResult { Success = DatabaseController.UpdateCharOwner(CharId, "", User.Id, true) };
        }

        /// <summary>
        /// Get image url
        /// </summary>
        /// <param name="JSON"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ItemImage([FromQuery] string S)
        {
            // D:\vhosts\sromc.com\subdomains\api
            // C:\inetpub\vhosts\sromc.com\httpdocs\_icons\ 
            string IconPath = @"D:\vhosts\sromc.com\http\_icons\";
            string ServerName = S;
            string Image = DatabaseController.GetItemImage(ServerName);
            if(Image == null || Image == "")
                return PhysicalFile(IconPath + "default.png", "image/jpeg");

            return PhysicalFile(IconPath + Image, "image/jpeg");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="S"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SkillImage([FromQuery] string S)
        {
            // D:\vhosts\sromc.com\subdomains\api
            // C:\inetpub\vhosts\sromc.com\httpdocs\_icons\ 
            string IconPath = @"D:\vhosts\sromc.com\http\_icons\";
            string ServerName = S;
            string Image = DatabaseController.GetSkillImage(ServerName);
            if (Image == null || Image == "")
                return PhysicalFile(IconPath + "default.png", "image/jpeg");

            return PhysicalFile(IconPath + Image, "image/jpeg");
        }
    }
}
