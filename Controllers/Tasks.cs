using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SROMCapi.Models;

namespace SROMCapi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Tasks : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="JSON"></param>
        /// <returns></returns>
        [HttpPost]
        public Models.TaskResult Create([FromBody] Models.Task JSON)
        {
            string Token = JSON.Token;
            string CharId = JSON.CharId;

            // Check token
            var User = DatabaseController.UserDetail(Token);
            if(User.Token != Token)
            {
                return new TaskResult {  Success = false };
            }

            // Check user
            bool CharIsExists = DatabaseController.CheckCharOwner(CharId, User.Id);
            if (!CharIsExists)
            {
                return new TaskResult { Success = false };
            }

            var AddTask = DatabaseController.AddTask(CharId, JSON.Command, JSON.Arg1, JSON.Arg2, JSON.Arg3);
            if (AddTask == true)
            {
                return new TaskResult { Success = true };
            }

            // Error adding db
            return new TaskResult { Success = false };
        }
    }
}
