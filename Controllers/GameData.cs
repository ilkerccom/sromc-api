using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SROMCapi.Models;

namespace SROMCapi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GameData : ControllerBase
    {

        [HttpGet]
        public string GetServerInfo()
        {
            return "TEST => " + System.IO.Directory.GetCurrentDirectory();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="JSON"></param>
        /// <returns></returns>
        [HttpPost]
        public List<Monster> GetMonsters([FromBody] GetMonsters JSON)
        {
            return DatabaseController.GetMonsters(JSON.Page, JSON.Language, JSON.Keyword, JSON.Category, JSON.Level, 50);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> SilkroadGetMonsters([FromQuery] string Lang, [FromQuery] string Code)
        {
            string Result = "";

            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                string HTML = await reader.ReadToEndAsync();
                var Document = new HtmlDocument();
                Document.LoadHtml(HTML);

                foreach (HtmlNode Table in Document.DocumentNode.SelectNodes("//table[@class='table_item']"))
                {
                    string Name = Table.SelectSingleNode(".//tr[2]//td[2]").InnerText.Trim();
                    int Level = 0;
                    if (Table.SelectSingleNode(".//tr[2]//td[3]").InnerText.Trim().Contains('/'))
                    {
                        Level = Convert.ToInt32(Table.SelectSingleNode(".//tr[2]//td[3]").InnerText.Trim().Split('/')[0]);
                    }
                    else if (Table.SelectSingleNode(".//tr[2]//td[3]").InnerText.Trim().Length == 0)
                    {
                        Level = 0;
                    }
                    else
                    {
                        Level = Convert.ToInt32(Table.SelectSingleNode(".//tr[2]//td[3]").InnerText.Trim());
                    }
                    string DefenceType = Table.SelectSingleNode(".//tr[2]//td[4]").InnerText.Trim();
                    string AttackType = Table.SelectSingleNode(".//tr[2]//td[5]").InnerText.Trim();
                    string AttackMethod = Table.SelectSingleNode(".//tr[2]//td[6]").InnerText.Trim();
                    string Description = Table.SelectSingleNode(".//td[@class='text1']").InnerText.Trim();
                    string ImageURL = Table.SelectSingleNode(".//tr[2]//td[1]//img").Attributes["src"].Value;
                    string Image = Tools.CreateUnique(false, 12) + ".jpg";
                    string UniqueId = ImageURL.Substring(ImageURL.IndexOf("mon_img") + 7).Replace("/", "_").Replace(".jpg", "");

                    bool IsExists = DatabaseController.MonsterExists(UniqueId);

                    if (IsExists)
                    {
                        bool Update = DatabaseController.UpdateMonster(UniqueId, Description, Lang);

                        if (Update)
                        {
                            Result += String.Format("OK - {0} language updated to {1}", Name, Lang) + System.Environment.NewLine;
                        }
                        else
                        {
                            Result += String.Format("ERROR - {0} language cannot update to {1}", Name, Lang) + System.Environment.NewLine;
                        }
                    }
                    else
                    {
                        bool Add = DatabaseController.AddMonster(Name, Level, DefenceType, AttackType, AttackMethod, Description, Image, Code, Lang, UniqueId);

                        if (Add)
                        {
                            Result += String.Format("OK - {0} added for {1}", Name, Lang) + System.Environment.NewLine;

                            // Download image
                            using (var client = new WebClient())
                            {
                                client.DownloadFile(ImageURL, AppDomain.CurrentDomain.BaseDirectory + "/images/" + Image);
                            }
                        }
                        else
                        {
                            Result += String.Format("ERROR - {0} cannot add to {1}", Name, Lang) + System.Environment.NewLine;
                        }
                    }
                }
            }

            return Result;
        }
    }
}
