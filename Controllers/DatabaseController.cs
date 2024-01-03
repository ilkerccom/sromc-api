using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SROMCapi.Controllers
{
    public class DatabaseController
    {
        /// <summary>
        /// 
        /// </summary>
        public static string ConnectionString = "Data Source=" + Settings.DatabaseServer
                        + ";Initial Catalog=" + Settings.DatabaseName
                        + ";User ID=" + Settings.DatabaseUsername
                        + ";Password=" + Settings.DatabasePassword
                        + ";Connection Timeout=5";


        /// <summary>
        /// 
        /// </summary>
        /// <param name="CharId"></param>
        /// <param name="Password"></param>
        /// <param name="Token"></param>
        /// <param name="CharName"></param>
        /// <param name="Server"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static Models.AccountInfo AddChar(string Password, string Token, string CharName, string Server, string PlayerId)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                string query = "INSERT into Chars (Password,Token,CharName,Server,PlayerId,LastUpdate) VALUES (@Password,@Token,@CharName,@Server,@PlayerId,@LastUpdate)";
                SqlCommand cmd = new SqlCommand(query, Connection);
                Connection.Open();
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@Token", Token);
                cmd.Parameters.AddWithValue("@CharName", CharName);
                cmd.Parameters.AddWithValue("@Server", Server);
                cmd.Parameters.AddWithValue("@PlayerId", PlayerId);
                cmd.Parameters.AddWithValue("@LastUpdate", DateTime.Now);
                cmd.ExecuteNonQuery();
                Connection.Close();
                return GetCharId(Token);
            }
            catch
            {
                return new Models.AccountInfo();
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public static Models.UserInfo AddUser(string Username, string Password, string Token)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                string query = "INSERT into Accounts (Username, Password, Token) VALUES (@Username, @Password, @Token)";
                SqlCommand cmd = new SqlCommand(query, Connection);
                Connection.Open();
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@Token", Token);
                cmd.Parameters.AddWithValue("@Username", Username);
                cmd.ExecuteNonQuery();
                Connection.Close();
                return new Models.UserInfo
                {
                    Username = Username,
                    Password = Password,
                    Token = Token
                };
            }
            catch
            {
                return new Models.UserInfo();
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CharId"></param>
        /// <param name="Command"></param>
        /// <param name="Arg1"></param>
        /// <param name="Arg2"></param>
        /// <param name="Arg3"></param>
        /// <returns></returns>
        public static bool AddTask(string CharId, string Command, string Arg1, string Arg2, string Arg3)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                string query = "INSERT into Tasks (CharId,Task,Arg1,Arg2,Arg3) VALUES (@CharId,@Task,@Arg1,@Arg2,@Arg3)";
                SqlCommand cmd = new SqlCommand(query, Connection);
                Connection.Open();
                cmd.Parameters.AddWithValue("@CharId", CharId);
                cmd.Parameters.AddWithValue("@Task", Command);
                cmd.Parameters.AddWithValue("@Arg1", Arg1);
                cmd.Parameters.AddWithValue("@Arg2", Arg2);
                cmd.Parameters.AddWithValue("@Arg3", Arg3);
                cmd.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Game"></param>
        /// <param name="Server"></param>
        /// <param name="ServerCapacity"></param>
        /// <param name="ServerLevel"></param>
        /// <returns></returns>
        public static bool AddServer(string Game, string Server, int ServerCapacity, int ServerStatus)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                string query = "INSERT into Servers (Game,Server,ServerCapacity,ServerStatus) VALUES (@Game,@Server,@ServerCapacity,@ServerStatus)";
                SqlCommand cmd = new SqlCommand(query, Connection);
                Connection.Open();
                cmd.Parameters.AddWithValue("@Game", Game);
                cmd.Parameters.AddWithValue("@Server", Server);
                cmd.Parameters.AddWithValue("@ServerCapacity", ServerCapacity);
                cmd.Parameters.AddWithValue("@ServerStatus", ServerStatus);
                cmd.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="MonsterLevel"></param>
        /// <param name="DefenceType"></param>
        /// <param name="AttackType"></param>
        /// <param name="AttackMethod"></param>
        /// <param name="Description"></param>
        /// <param name="Image"></param>
        /// <param name="Code"></param>
        /// <param name="Language"></param>
        /// <returns></returns>
        public static bool AddMonster(string Name, int MonsterLevel, string DefenceType, string AttackType, string AttackMethod, string Description, string Image, string Code, string Language, string UniqueId)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                // Parse language
                Language = Language.ToUpper().Trim();

                // Avaliable languages
                List<string> AvailableLanguages = new List<string>
                {
                    "TR", "EN"
                };

                // Check
                if (!AvailableLanguages.Contains(Language))
                    return false;

                // Continue
                string query = "INSERT into Monsters (Name,MonsterLevel,DefenceType,AttackType,AttackMethod,Image,Code,UniqueId, Desc_" + Language + ") VALUES (@Name,@MonsterLevel,@DefenceType,@AttackType,@AttackMethod,@Image,@Code,@UniqueId,@Desc_" + Language + ")";
                SqlCommand cmd = new SqlCommand(query, Connection);
                Connection.Open();
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@MonsterLevel", MonsterLevel);
                cmd.Parameters.AddWithValue("@DefenceType", DefenceType);
                cmd.Parameters.AddWithValue("@AttackType", AttackType);
                cmd.Parameters.AddWithValue("@AttackMethod", AttackMethod);
                cmd.Parameters.AddWithValue("@Image", Image);
                cmd.Parameters.AddWithValue("@Code", Code);
                cmd.Parameters.AddWithValue("@UniqueId", UniqueId);
                cmd.Parameters.AddWithValue("@Desc_" + Language, Description);
                cmd.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="MonsterLevel"></param>
        /// <param name="Description"></param>
        /// <param name="Language"></param>
        /// <returns></returns>
        public static bool UpdateMonster(string UniqueId, string Description, string Language)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                // Parse language
                Language = Language.ToUpper().Trim();

                // Avaliable languages
                List<string> AvailableLanguages = new List<string>
                {
                    "TR", "EN"
                };

                // Check
                if (!AvailableLanguages.Contains(Language))
                    return false;

                // Continue
                string query = "UPDATE Monsters SET Desc_" + Language + "=@Desc_" + Language + " where UniqueId=@UniqueId";
                SqlCommand cmd = new SqlCommand(query, Connection);
                Connection.Open();
                cmd.Parameters.AddWithValue("@UniqueId", UniqueId);
                cmd.Parameters.AddWithValue("@Desc_" + Language, Description);
                cmd.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public static Models.UserInfo UserDetail(string Token)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                DataTable dataTable = new DataTable();
                string Query = "SELECT TOP(1) * FROM Accounts where Token=@Token";
                SqlCommand Cmd = new SqlCommand(Query, Connection);
                Connection.Open();
                Cmd.Parameters.AddWithValue("@Token", Token);
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(dataTable);
                Connection.Close();

                if (dataTable.Rows.Count > 0)
                {
                    return new Models.UserInfo
                    {
                        Id = Convert.ToInt32(dataTable.Rows[0]["Id"].ToString()),
                        Username = dataTable.Rows[0]["Username"].ToString(),
                        Token = dataTable.Rows[0]["Token"].ToString(),
                    };
                }

                return new Models.UserInfo();
            }
            catch
            {
                return new Models.UserInfo();
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Token"></param>
        public static void UserUpdateToken(string Token, string NewToken)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                string query = "UPDATE TOP(1) Accounts SET Token=@NewToken where Token=@Token";
                SqlCommand cmd = new SqlCommand(query, Connection);
                Connection.Open();
                cmd.Parameters.AddWithValue("@Token", Token);
                cmd.Parameters.AddWithValue("@NewToken", NewToken);
                cmd.ExecuteNonQuery();
                Connection.Close();
            }
            catch
            {

            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Game"></param>
        /// <param name="Server"></param>
        /// <param name="ServerCapacity"></param>
        /// <param name="ServerLevel"></param>
        /// <returns></returns>
        public static bool UpdateServer(string Game, string Server, int ServerCapacity, int ServerStatus)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                string query = "UPDATE Servers SET ServerCapacity=@ServerCapacity,ServerStatus=@ServerStatus,LastUpdate=@LastUpdate where Game=@Game and Server=@Server";
                SqlCommand cmd = new SqlCommand(query, Connection);
                Connection.Open();
                cmd.Parameters.AddWithValue("@Game", Game);
                cmd.Parameters.AddWithValue("@Server", Server);
                cmd.Parameters.AddWithValue("@ServerCapacity", ServerCapacity);
                cmd.Parameters.AddWithValue("@ServerStatus", ServerStatus);
                cmd.Parameters.AddWithValue("@LastUpdate", DateTime.UtcNow);
                cmd.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// Update with server name
        /// </summary>
        /// <param name="Server"></param>
        /// <param name="ServerCapacity"></param>
        /// <param name="ServerStatus"></param>
        /// <returns></returns>
        public static bool UpdateServer(string Server, int ServerCapacity, int ServerStatus)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                string query = "UPDATE Servers SET ServerCapacity=@ServerCapacity,ServerStatus=@ServerStatus,LastUpdate=@LastUpdate where Server=@Server";
                SqlCommand cmd = new SqlCommand(query, Connection);
                Connection.Open();
                cmd.Parameters.AddWithValue("@Server", Server);
                cmd.Parameters.AddWithValue("@ServerCapacity", ServerCapacity);
                cmd.Parameters.AddWithValue("@ServerStatus", ServerStatus);
                cmd.Parameters.AddWithValue("@LastUpdate", DateTime.UtcNow);
                cmd.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static Models.UserInfo UserLogin(string Username, string Password)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                DataTable dataTable = new DataTable();
                string Query = "SELECT TOP(1) * FROM Accounts where Username=@Username and Password=@Password";
                SqlCommand Cmd = new SqlCommand(Query, Connection);
                Connection.Open();
                Cmd.Parameters.AddWithValue("@Username", Username);
                Cmd.Parameters.AddWithValue("@Password", Password);
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(dataTable);
                Connection.Close();

                if (dataTable.Rows.Count > 0)
                {
                    // Genereate new Token
                    string NewToken = Tools.CreateUnique(false, 64);
                    UserUpdateToken(dataTable.Rows[0]["Token"].ToString(), NewToken);

                    // Get new User
                    var User = UserDetail(NewToken);
                    return new Models.UserInfo
                    {
                        Id = User.Id,
                        Username = User.Username,
                        Token = User.Token,
                    };
                }

                return new Models.UserInfo();
            }
            catch
            {
                return new Models.UserInfo();
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Password"></param>
        /// <param name="Token"></param>
        /// <param name="CharName"></param>
        /// <param name="Server"></param>
        /// <returns></returns>
        public static Models.AccountInfo UpdateChar(string Password, string Token, string CharName, string Server, int CharId)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                string query = "UPDATE TOP(1) Chars SET Password=@Password,Token=@Token,LastUpdate=@LastUpdate where CharId=@CharId";
                SqlCommand cmd = new SqlCommand(query, Connection);
                Connection.Open();
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@Token", Token);
                cmd.Parameters.AddWithValue("@CharName", CharName);
                cmd.Parameters.AddWithValue("@Server", Server);
                cmd.Parameters.AddWithValue("@CharId", CharId);
                cmd.Parameters.AddWithValue("@LastUpdate", DateTime.Now);
                cmd.ExecuteNonQuery();
                Connection.Close();
                return GetCharId(Token);
            }
            catch
            {
                return new Models.AccountInfo();
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CharId"></param>
        /// <param name="CharPassword"></param>
        /// <param name="Owner"></param>
        /// <returns></returns>
        public static bool UpdateCharOwner(string CharId, string CharPassword, int Owner, bool IsDelete = false)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                string query = "UPDATE TOP(1) Chars SET Owner=@Owner where CharId=@CharId and Password=@Password";

                if (IsDelete)
                {
                    query = "UPDATE TOP(1) Chars SET Owner=0 where CharId=@CharId";
                }

                SqlCommand cmd = new SqlCommand(query, Connection);
                Connection.Open();
                cmd.Parameters.AddWithValue("@Owner", Owner);
                cmd.Parameters.AddWithValue("@CharId", CharId);
                cmd.Parameters.AddWithValue("@Password", CharPassword);
                cmd.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="CharId"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static int UpdateData(string Token, string CharId, string Status, string Character, string Inventory, string Storage, string Quests, string Drops, string Guild, string Party, string Monsters, string Pets, string Academy, string Messages, string Players, string Taxi, string GuildUnion, string StorageGuild, string Skills, string Game)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                string query = "UPDATE TOP(1) Chars SET Status=@Status,Character=@Character,Inventory=@Inventory,Storage=@Storage,Quests=@Quests,Drops=@Drops,Guild=@Guild,Party=@Party,Monsters=@Monsters,Pets=@Pets,Academy=@Academy,Messages=@Messages,Players=@Players,Taxi=@Taxi,GuildUnion=@GuildUnion,StorageGuild=@StorageGuild,Skills=@Skills,LastUpdate=@LastUpdate,Game=@Game where CharId=@CharId and Token=@Token";
                SqlCommand cmd = new SqlCommand(query, Connection);
                Connection.Open();
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@Character", Character);
                cmd.Parameters.AddWithValue("@Inventory", Inventory);
                cmd.Parameters.AddWithValue("@Storage", Storage);
                cmd.Parameters.AddWithValue("@Quests", Quests);
                cmd.Parameters.AddWithValue("@Drops", Drops);
                cmd.Parameters.AddWithValue("@Guild", Guild);
                cmd.Parameters.AddWithValue("@Party", Party);
                cmd.Parameters.AddWithValue("@Monsters", Monsters);
                cmd.Parameters.AddWithValue("@Pets", Pets);
                cmd.Parameters.AddWithValue("@Academy", Academy);
                cmd.Parameters.AddWithValue("@Messages", Messages);
                cmd.Parameters.AddWithValue("@Players", Players);
                cmd.Parameters.AddWithValue("@Taxi", Taxi);
                cmd.Parameters.AddWithValue("@GuildUnion", GuildUnion);
                cmd.Parameters.AddWithValue("@StorageGuild", StorageGuild);
                cmd.Parameters.AddWithValue("@Skills", Skills);
                cmd.Parameters.AddWithValue("@Game", Game);
                cmd.Parameters.AddWithValue("@Token", Token);
                cmd.Parameters.AddWithValue("@CharId", CharId);
                cmd.Parameters.AddWithValue("@LastUpdate", DateTime.Now);
                cmd.ExecuteNonQuery();
                Connection.Close();
                return 1;
            }
            catch
            {
                return 0;
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        // Update simple
        public static bool UpdateDataSimple(string Token, string CharId, string Status)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                string query = "UPDATE TOP(1) Chars SET Status=@Status,LastUpdate=@LastUpdate where CharId=@CharId and Token=@Token";
                SqlCommand cmd = new SqlCommand(query, Connection);
                Connection.Open();
                cmd.Parameters.AddWithValue("@Token", Token);
                cmd.Parameters.AddWithValue("@CharId", CharId);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@LastUpdate", DateTime.Now);
                cmd.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        // LAst mobile action date update
        public static bool UpdateLastMobileAction(string CharId)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                string query = "UPDATE TOP(1) Chars SET MobileAppLastAction=@MobileAppLastAction where CharId=@CharId";
                SqlCommand cmd = new SqlCommand(query, Connection);
                Connection.Open();
                cmd.Parameters.AddWithValue("@CharId", CharId);
                cmd.Parameters.AddWithValue("@MobileAppLastAction", DateTime.UtcNow);
                cmd.ExecuteNonQuery();
                Connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Server"></param>
        /// <param name="CharName"></param>
        /// <returns></returns>
        public static int CharIsExists(string Server, string CharName)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                DataTable dataTable = new DataTable();
                string Query = "SELECT CharId FROM Chars where Server=@Server and CharName=@CharName";
                SqlCommand Cmd = new SqlCommand(Query, Connection);
                Cmd.Parameters.AddWithValue("@Server", Server);
                Cmd.Parameters.AddWithValue("@CharName", CharName);
                Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(dataTable);
                Connection.Close();

                if (dataTable.Rows.Count > 0)
                {
                    return Convert.ToInt32(dataTable.Rows[0]["CharId"].ToString());
                }

                return 0;
            }
            catch { return 0; }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CharId"></param>
        /// <returns></returns>
        public static bool CheckCharOwner(string CharId, int Owner)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                DataTable dataTable = new DataTable();
                string Query = "SELECT CharId FROM Chars where CharId=@CharId and Owner=@Owner";
                SqlCommand Cmd = new SqlCommand(Query, Connection);
                Cmd.Parameters.AddWithValue("@CharId", CharId);
                Cmd.Parameters.AddWithValue("@Owner", Owner);
                Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(dataTable);
                Connection.Close();

                if (dataTable.Rows.Count > 0)
                {
                    return true;
                }

                return false;
            }
            catch { return false; }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CharId"></param>
        /// <param name="CharPassword"></param>
        /// <returns></returns>
        public static bool CharLogin(string CharId, string CharPassword)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                DataTable dataTable = new DataTable();
                string Query = "SELECT CharId FROM Chars where CharId=@CharId and Password=@Password";
                SqlCommand Cmd = new SqlCommand(Query, Connection);
                Cmd.Parameters.AddWithValue("@CharId", CharId);
                Cmd.Parameters.AddWithValue("@Password", CharPassword);
                Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(dataTable);
                Connection.Close();

                if (dataTable.Rows.Count > 0)
                {
                    return true;
                }

                return false;
            }
            catch { return false; }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Owner"></param>
        /// <returns></returns>
        public static List<Models.MyChar> GetChars(int Owner)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            var CharList = new List<Models.MyChar>();
            try
            {
                DataTable dataTable = new DataTable();
                string Query = "SELECT Owner,CharId,CharName,Server,Game,Status,Character,LastUpdate,DATEDIFF(minute,LastUpdate, GETDATE()) AS Minutes, (SELECT ServerStatus FROM Servers WHERE(Server = Chars.Server)) AS ServerStatus FROM Chars where Owner=@Owner";
                SqlCommand Cmd = new SqlCommand(Query, Connection);
                Connection.Open();
                Cmd.Parameters.AddWithValue("@Owner", Owner);
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(dataTable);
                Connection.Close();

                foreach (DataRow row in dataTable.Rows)
                {
                    CharList.Add(new Models.MyChar()
                    {
                        CharId = row["CharId"].ToString(),
                        CharName = row["CharName"].ToString(),
                        Server = row["Server"].ToString(),
                        Game = row["Game"].ToString(),
                        Status = row["Status"].ToString(),
                        LastUpdate = row["LastUpdate"].ToString(),
                        LastUpdateMinutes = Convert.ToInt32(row["Minutes"].ToString()),
                        Character = row["Character"].ToString(),
                        ServerStatus = row["ServerStatus"].ToString().Trim().Length > 1 ? Boolean.Parse(row["ServerStatus"].ToString()) : true
                    });
                }

                return CharList;
            }
            catch
            {
                return CharList;
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="Language"></param>
        /// <param name="PerPage"></param>
        /// <returns></returns>
        public static List<Models.Monster> GetMonsters(int Page, string Language, string Keyword, string Category, int Level, int PerPage = 50)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            var MonsterList = new List<Models.Monster>();
            Language = Language.ToUpper().Trim();
            try
            {
                DataTable dataTable = new DataTable();
                string Query = "SELECT * FROM Monsters where MonsterLevel > 0 ORDER BY MonsterLevel OFFSET @Page ROWS FETCH NEXT @PerPage ROWS ONLY";
                if(Keyword.Length > 0)
                {
                    Query = "SELECT * FROM Monsters where MonsterLevel > 0 and Name LIKE '%' + @Keyword + '%' ORDER BY MonsterLevel OFFSET @Page ROWS FETCH NEXT @PerPage ROWS ONLY";
                }
                else if (Level != 0)
                {
                    Query = "SELECT * FROM Monsters where MonsterLevel >= @MinLevel AND MonsterLevel <= @MaxLevel AND MonsterLevel != 0  ORDER BY MonsterLevel OFFSET @Page ROWS FETCH NEXT @PerPage ROWS ONLY";
                }
                else if (Category.Length > 0)
                {
                    Query = "SELECT * FROM Monsters where Code=@Code ORDER BY MonsterLevel OFFSET @Page ROWS FETCH NEXT @PerPage ROWS ONLY";
                }
                SqlCommand Cmd = new SqlCommand(Query, Connection);
                Connection.Open();
                Cmd.Parameters.AddWithValue("@Page", Page * PerPage);
                Cmd.Parameters.AddWithValue("@PerPage", Page * PerPage + PerPage);

                if(Keyword.Length > 0)
                {
                    Cmd.Parameters.AddWithValue("@Keyword", Keyword);
                }
                else if (Level != 0)
                {
                    Cmd.Parameters.AddWithValue("@MinLevel", Level - 5);
                    Cmd.Parameters.AddWithValue("@MaxLevel", Level + 2);
                }
                else if(Category.Length > 0)
                {
                    Cmd.Parameters.AddWithValue("@Code", Category);
                }

                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(dataTable);
                Connection.Close();

                foreach (DataRow row in dataTable.Rows)
                {
                    MonsterList.Add(new Models.Monster()
                    {
                        Name = row["Name"].ToString(),
                        Level = Convert.ToInt16(row["MonsterLevel"].ToString()),
                        DefenceType = row["DefenceType"].ToString(),
                        AttackType = row["AttackType"].ToString(),
                        AttackMethod = row["AttackMethod"].ToString(),
                        Desc = row["Desc_" + Language].ToString(),
                        Image = row["Image"].ToString()
                    });
                }

                return MonsterList;
            }
            catch
            {
                return MonsterList;
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// Check server is exists
        /// </summary>
        /// <param name="Game"></param>
        /// <param name="Server"></param>
        /// <returns></returns>
        public static bool ServerExists(string Game, string Server)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                DataTable dataTable = new DataTable();
                string Query = "SELECT Id FROM Servers where Game=@Game and Server=@Server";
                SqlCommand Cmd = new SqlCommand(Query, Connection);
                Cmd.Parameters.AddWithValue("@Game", Game);
                Cmd.Parameters.AddWithValue("@Server", Server);
                Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(dataTable);
                Connection.Close();

                if (dataTable.Rows.Count > 0)
                {
                    return true;
                }

                return false;
            }
            catch { return false; }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// Server exists with only name
        /// </summary>
        /// <param name="Server"></param>
        /// <returns></returns>
        public static bool ServerExists(string Server)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                DataTable dataTable = new DataTable();
                string Query = "SELECT Id FROM Servers where Server=@Server";
                SqlCommand Cmd = new SqlCommand(Query, Connection);
                Cmd.Parameters.AddWithValue("@Server", Server);
                Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(dataTable);
                Connection.Close();

                if (dataTable.Rows.Count > 0)
                {
                    return true;
                }

                return false;
            }
            catch { return false; }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// Get all servers
        /// </summary>
        /// <returns></returns>
        public static List<Models.Server> GetServers()
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            List<Models.Server> ServerList = new List<Models.Server>();
            try
            {
                DataTable dataTable = new DataTable();
                string Query = "SELECT * FROM Servers order by Game";
                SqlCommand Cmd = new SqlCommand(Query, Connection);
                Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(dataTable);
                Connection.Close();

                foreach(DataRow Row in dataTable.Rows)
                {
                    ServerList.Add(new Models.Server
                    {
                        Game = Row["Game"].ToString(),
                        ServerName = Row["Server"].ToString(),
                        ServerCapacity = Convert.ToInt32(Row["ServerCapacity"].ToString()),
                        ServerStatus = Boolean.Parse(Row["ServerStatus"].ToString()),
                        LastUpdate = DateTime.Parse(Row["LastUpdate"].ToString())
                    });
                }

                return ServerList;
            }
            catch { return ServerList; }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Level"></param>
        /// <returns></returns>
        public static bool MonsterExists(string UniqueId)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                DataTable dataTable = new DataTable();
                string Query = "SELECT Id FROM Monsters where UniqueId=@UniqueId";
                SqlCommand Cmd = new SqlCommand(Query, Connection);
                Cmd.Parameters.AddWithValue("@UniqueId", UniqueId);
                Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(dataTable);
                Connection.Close();

                if (dataTable.Rows.Count > 0)
                {
                    return true;
                }

                return false;
            }
            catch { return false; }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Owner"></param>
        /// <param name="CharId"></param>
        /// <returns></returns>
        public static Models.Info GetCharInfo(int Owner, string CharId)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                DataTable dataTable = new DataTable();
                string Query = "SELECT * FROM Chars where Owner=@Owner and CharId=@CharId";
                SqlCommand Cmd = new SqlCommand(Query, Connection);
                Connection.Open();
                Cmd.Parameters.AddWithValue("@Owner", Owner);
                Cmd.Parameters.AddWithValue("@CharId", CharId);
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(dataTable);
                Connection.Close();

                if (dataTable.Rows.Count == 1)
                {
                    return new Models.Info
                    {
                        charId = dataTable.Rows[0]["CharId"].ToString(),
                        status = dataTable.Rows[0]["Status"].ToString(),
                        game = dataTable.Rows[0]["Game"].ToString(),
                        character = dataTable.Rows[0]["Character"].ToString(),
                        inventory = dataTable.Rows[0]["Inventory"].ToString(),
                        storage = dataTable.Rows[0]["Storage"].ToString(),
                        storage_guild = dataTable.Rows[0]["StorageGuild"].ToString(),
                        quests = dataTable.Rows[0]["Quests"].ToString(),
                        drops = dataTable.Rows[0]["Drops"].ToString(),
                        guild = dataTable.Rows[0]["Guild"].ToString(),
                        guild_union = dataTable.Rows[0]["GuildUnion"].ToString(),
                        party = dataTable.Rows[0]["Party"].ToString(),
                        monsters = dataTable.Rows[0]["Monsters"].ToString(),
                        pets = dataTable.Rows[0]["Pets"].ToString(),
                        academy = dataTable.Rows[0]["Academy"].ToString(),
                        messages = dataTable.Rows[0]["Messages"].ToString(),
                        players = dataTable.Rows[0]["Players"].ToString(),
                        taxi = dataTable.Rows[0]["Taxi"].ToString(),
                        skills = dataTable.Rows[0]["Skills"].ToString()
                    };
                }

                return new Models.Info { };
            }
            catch
            {
                return new Models.Info { };
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="CharId"></param>
        /// <returns></returns>
        public static DateTime GetCharLastAction(string CharId)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                DataTable dataTable = new DataTable();
                string Query = "SELECT TOP(1) MobileAppLastAction FROM Chars where CharId=@CharId";
                SqlCommand Cmd = new SqlCommand(Query, Connection);
                Connection.Open();
                Cmd.Parameters.AddWithValue("@CharId", CharId);
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(dataTable);
                Connection.Close();

                if (dataTable.Rows.Count == 1)
                {
                    DateTime Last = DateTime.Parse(dataTable.Rows[0]["MobileAppLastAction"].ToString());

                    return Last;
                }

                return DateTime.UtcNow.AddDays(-1);
            }
            catch
            {
                return DateTime.UtcNow.AddDays(-1);
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public static Models.AccountInfo GetCharId(string Token)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                DataTable dataTable = new DataTable();
                string Query = "SELECT TOP(1) * FROM Chars where Token=@Token";
                SqlCommand Cmd = new SqlCommand(Query, Connection);
                Connection.Open();
                Cmd.Parameters.AddWithValue("@Token", Token);
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(dataTable);
                Connection.Close();

                if (dataTable.Rows.Count > 0)
                {
                    return new Models.AccountInfo
                    {
                        CharId = dataTable.Rows[0]["CharId"].ToString(),
                        CharName = dataTable.Rows[0]["CharName"].ToString(),
                        PlayerId = dataTable.Rows[0]["PlayerId"].ToString(),
                        Server = dataTable.Rows[0]["Server"].ToString(),
                        Token = dataTable.Rows[0]["Token"].ToString(),
                    };
                }

                return new Models.AccountInfo();
            }
            catch
            {
                return new Models.AccountInfo();
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CharId"></param>
        /// <returns></returns>
        public static Models.Task GetTask(string CharId)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                DataTable dataTable = new DataTable();
                string Query = "SELECT TOP(1) * FROM Tasks where CharId=@CharId and Completed=0";
                SqlCommand Cmd = new SqlCommand(Query, Connection);
                Connection.Open();
                Cmd.Parameters.AddWithValue("@CharId", CharId);
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(dataTable);
                Connection.Close();

                if (dataTable.Rows.Count > 0)
                {
                    return new Models.Task
                    {
                        Command = dataTable.Rows[0]["Task"].ToString(),
                        Arg1 = dataTable.Rows[0]["Arg1"].ToString(),
                        Arg2 = dataTable.Rows[0]["Arg2"].ToString(),
                        Arg3 = dataTable.Rows[0]["Arg3"].ToString(),
                    };
                }

                return new Models.Task();
            }
            catch
            {
                return new Models.Task();
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// Get full image url
        /// </summary>
        /// <param name="ServerName"></param>
        /// <returns></returns>
        public static string GetItemImage(string ServerName)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                DataTable dataTable = new DataTable();
                string Query = "SELECT TOP(1) Icon FROM Items where ServerName=@ServerName";
                SqlCommand Cmd = new SqlCommand(Query, Connection);
                Connection.Open();
                Cmd.Parameters.AddWithValue("@ServerName", ServerName);
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(dataTable);
                Connection.Close();

                if (dataTable.Rows.Count > 0)
                {
                    return dataTable.Rows[0]["Icon"].ToString().Replace(".ddj", ".png");
                }

                return "";
            }
            catch
            {
                return "";
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        public static string GetSkillImage(string ServerName)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                DataTable dataTable = new DataTable();
                string Query = "SELECT TOP(1) Icon FROM Skills where ServerName=@ServerName";
                SqlCommand Cmd = new SqlCommand(Query, Connection);
                Connection.Open();
                Cmd.Parameters.AddWithValue("@ServerName", ServerName);
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(dataTable);
                Connection.Close();

                if (dataTable.Rows.Count > 0)
                {
                    return dataTable.Rows[0]["Icon"].ToString().Replace(".ddj", ".png");
                }

                return "";
            }
            catch
            {
                return "";
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// Complete a task
        /// </summary>
        /// <param name="Token"></param>
        public static void CompleteTask(string CharId)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            try
            {
                string query = "UPDATE TOP(1) Tasks SET Completed=1 where CharId=@CharId and Completed=0";
                SqlCommand cmd = new SqlCommand(query, Connection);
                Connection.Open();
                cmd.Parameters.AddWithValue("@CharId", CharId);
                cmd.ExecuteNonQuery();
                Connection.Close();
            }
            catch
            {

            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }


    }
}
