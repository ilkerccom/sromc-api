using HtmlAgilityPack;
using Quartz;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SROMCapi.Tasks
{
    public class Tasks
    {
        public class TaskUpdateServerStats : IJob
        {
            /// <summary>
            /// Update server stats
            /// </summary>
            /// <param name="context"></param>
            /// <returns></returns>
            /// <exception cref="NotImplementedException"></exception>
            Task IJob.Execute(IJobExecutionContext context)
            {
                // Web HTML
                var URL = "https://stats.projecthax.com/capacity/";
                var Web = new HtmlWeb();
                var Doc = Web.Load(URL);

                // Variables
                string Game = "";

                // Foreach Tables
                foreach(HtmlNode Table in Doc.DocumentNode.SelectNodes("//table[@class='table table-bordered']"))
                {
                    Game = GetGameName(Table.SelectSingleNode(".//thead//tr//th[1]").InnerText);

                    // Foreach tr elements
                    foreach (HtmlNode Tr in Table.SelectNodes(".//tbody//tr"))
                    {
                        // First td
                        var Td = Tr.SelectSingleNode(".//td[1]");

                        // If not null
                        if(Td != null)
                        {
                            string Server = Td.InnerText.Replace("(R)", "").Replace("(C)", "").Trim().Trim();
                            int Capacity = Tools.ServerCapacity(Tr.SelectSingleNode(".//td[2]").InnerText.Trim());
                            int State = Tools.ServerIsOpen(Tr.SelectSingleNode(".//td[3]").InnerText);

                            // Save to database
                            bool isExists = Controllers.DatabaseController.ServerExists(Server);
                            if (isExists)
                            {
                                // Update
                                Controllers.DatabaseController.UpdateServer(Server, Capacity, State);
                                Console.WriteLine(Server + " (" + Game + ") updated!");
                            }
                            else
                            {
                                // Add
                                //Controllers.DatabaseController.AddServer(Game, Server, Capacity, State);
                            }
                        }
                    }
                }

                throw new NotImplementedException();
            }

            private string GetGameName(string name)
            {
                string Game = "Other";
                string Code = name.ToLower().Trim();

                if(Code == "ksro")
                {
                    return "kSRO";
                }
                else if(Code == "isro")
                {
                    return "iSRO";
                }
                else if (Code == "trsro")
                {
                    return "TRSRO";
                }
                else if (Code == "digeam")
                {
                    return "DIGEAM";
                }
                else if (Code == "digeam")
                {
                    return "DIGEAM";
                }
                else if (Code == "jsro")
                {
                    return "jSRO";
                }
                else if (Code == "jsro")
                {
                    return "jSRO";
                }
                else if (Code == "vsro")
                {
                    return "vSRO";
                }
                else if (Code == "cSROR")
                {
                    return "cSROR";
                }

                return Game;
            }
        }
    }
}
