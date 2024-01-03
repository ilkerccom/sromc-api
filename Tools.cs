using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SROMCapi
{
    public class Tools
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string CreateUnique(bool UseGuid = true, int Length = 48, bool UseOnlyUpper = false)
        {
            if (UseGuid)
            {
                return Guid.NewGuid().ToString();
            }
            else
            {
                const string valid = "abcdefghijklmnoprstuwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                const string validUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                StringBuilder res = new StringBuilder();
                Random rnd = new Random();
                for (int i = 0; i < Length; i++)
                {
                    if (UseOnlyUpper)
                    {
                        res.Append(validUpper[rnd.Next(validUpper.Length)]);
                    }
                    else
                    {
                        res.Append(valid[rnd.Next(valid.Length)]);
                    }
                }
                return res.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static int ServerIsOpen(string Status)
        {
            string _Status = Status.ToLower().Trim();

            if (_Status == "open")
                return 1;

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Capacity"></param>
        /// <returns></returns>
        public static int ServerCapacity(string Capacity)
        {
            string _Capacity = Capacity.ToLower().Trim();

            if (_Capacity == "full")
                return 100;
            else if (_Capacity == "crowded")
                return 99;
            else if (_Capacity == "populated")
                return 80;
            else if (_Capacity == "moderate")
                return 70;
            else if (_Capacity == "normal")
                return 50;
            else if (_Capacity == "easy")
                return 15;
            else if (_Capacity.Contains("/"))
            {
                int PlayersIn = Convert.ToInt32(_Capacity.Split('/')[0].ToString().Trim());
                int PlayersOut = Convert.ToInt32(_Capacity.Split('/')[1].Trim().Split(' ')[0].ToString().Trim());

                if (PlayersIn == 0)
                    return 0;
                else
                {
                    return 100 * PlayersIn / PlayersOut;
                }
            }

            return 0;
        }
    }
}
