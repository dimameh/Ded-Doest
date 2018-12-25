using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using AngleSharp;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VkNet;
using VkNet.Enums;
using VkNet.Enums.Filters;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;
using VkNet.Utils;

namespace VkBot
{
    class Program
    {
        static VkApi vkapi = new VkApi();
        static string[] Commands = { "хочу кушоть", "хочу поделиться", "забрал еду", "команды" };

	    enum AveliableCommans
	    {
		    Eat = 0,
		    Share,
		    Took,
		    Commands
	    }
	    private static MySqlConnection BDConnection;

		#region BD

		class DBMySQLUtils
		{
			public static MySqlConnection
				GetDBConnection(string host, int port, string database, string username, string password)
			{
				// Connection String.
				String connString = "Server=" + host + ";Database=" + database
				                    + ";port=" + port + ";User Id=" + username + ";password=" + password;

				MySqlConnection conn = new MySqlConnection(connString);

				return conn;
			}
			}

		class DBUtils
	    {

		    public static MySqlConnection GetDBConnection()
	    {
		    string host = "tarangok.ru";
		    int port = 3306;
		    string database = "tarangok_db";
		    string username = "tarangok";
		    string password = "123555";

		    return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
	    }
	}

	#endregion

	static void Main(string[] args)
        {
            string Login = @"логин";
            string Password = @"пароль";
            ulong ID = 6768611;
			
			/*Console.WriteLine("Montece VK bot");
            Console.Write("Login: ");
            Login = Console.ReadLine();
            Console.Write("Password: ");
            Password = Console.ReadLine();
            Console.Write("App ID: ");
            ID = ulong.Parse(Console.ReadLine());*/
			Console.WriteLine("Auth started...");
			
	        if (Auth(Login, Password, ID))
	        {
		        Console.WriteLine("Auth completed.");

				MessagesGetObject messGets;

		        while (true)
		        {
			        messGets = vkapi.Messages.GetDialogs(new MessagesDialogsGetParams
			        {
				        Count = 5, Unread = true
			        });

			        if (messGets.TotalCount != 0)
			        {
				        foreach (var mes in messGets.Messages)
				        {
					        if (mes.UserId != null)
					        {
						        Command((long) mes.UserId, mes.Body);
					        }
				        }
					}
		        }
	        }

        }

        static bool Auth(string Login, string Password, ulong ID)
        {           
            try
            {
                vkapi.Authorize(new ApiAuthParams
                {
                    Login = Login,
                    Password = Password,
                    Settings = Settings.All,
                    ApplicationId = ID
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        static void CheckMessages(long CheckedUserID)
        {
	        var messGets = vkapi.Messages.GetDialogs(new MessagesDialogsGetParams
	        {
		        Count = 200,
				Unread = true
	        });

	        foreach (var mes in messGets.Messages)
	        {
		        Command(CheckedUserID, mes.Body);
	        }
		}

        static void Command(long CheckedUserID, string Body)
        {
            var CommandID = -1;
            for (var i = 0; i < Commands.Length; i++)
            {
	            if (Body.ToLower() != Commands[i])
	            {
		            continue;
	            }

	            CommandID = i;
	            break;
            }

	        if (CommandID == -1)
	        {
		        return;
	        }

	        switch (CommandID)
	        {
		        case (int)AveliableCommans.Eat:
			        GetSpotsFromCity(CheckedUserID,
				        vkapi.Messages.GetHistory(
					        new MessagesGetHistoryParams { UserId = CheckedUserID, Count = 5 }));
			        break;
		        case (int)AveliableCommans.Share:
			        SendMessage(CheckedUserID, "Заполните форму в следующем формате:");
					break;
		        case (int)AveliableCommans.Took:
			        
			        break;
		        default: /*Error*/ break;
	        }
        }

	    private static void GetSpotsFromCity(long CheckedUserID, MessagesGetObject dialog)
	    {
		    string city = dialog.Messages[0].Body;
		    List<Spot> spotList = GetSpotListByCity("Томск");
		    if (spotList == null)
		    {
			    SendMessage(CheckedUserID, "К сожалению в вашем городе уже всё съедено(");
				return;
		    }
		    //foreach (var spot in spotList)
		    //{
			    SendSpots(CheckedUserID,spotList);

			//}
	    }

	    private static List<Spot> GetSpotListByCity(string city)//!!
	    {
		    BDConnection = DBUtils.GetDBConnection();

			BDConnection.Open();
		    
		    string sql = "SELECT * FROM db WHERE city = \"Томск\"";
			// объект для выполнения SQL-запроса
		    MySqlCommand command = new MySqlCommand(sql, BDConnection);
		    // объект для чтения ответа сервера
		    MySqlDataReader reader = command.ExecuteReader();

		    List<Spot> spotList = new List<Spot>();
			// читаем результат
			while (reader.Read())
		    {
				// элементы массива [] - это значения столбцов из запроса SELECT
				for (var i = 0; i < reader.FieldCount/7; i++)
				{
					spotList.Add(new Spot
					{
						Id = reader.GetInt32("id"),
						Name = reader.GetString("name"),
						Description = reader.GetString("data"),
						City = reader.GetString("city"),
						Adress = new Complex(reader.GetDouble("x"), reader.GetDouble("y")),
						Photo = Url.Create(reader.GetString("photo"))
					});
				}

			}
		    reader.Close(); // закрываем reader
							// Закрыть соединение.
			BDConnection.Close();
			// Уничтожить объект, освободить ресурс.
		    BDConnection.Dispose();
			return spotList;
		}

		static VkCollection<User> GetFriends()
        {
            VkCollection<User> Friends = vkapi.Friends.Get(new FriendsGetParams
            {
                UserId = vkapi.UserId,
                Fields = ProfileFields.FirstName | ProfileFields.LastName,
                Order = FriendsOrder.Name
            });
            return Friends;
        }

        static void SendMessage(long ID, string Body)
        {
            vkapi.Messages.Send(new MessagesSendParams
            {
                UserId = ID,
                Message = Body
            });
        }

	    static void SendSpots(long ID, List<Spot> spotList)
	    {
		    string mess = String.Empty;
		    foreach (var spot in spotList)
		    {
			    mess = string.Concat(mess, spot + "\n______________________________\n");
		    }
		    vkapi.Messages.Send(new MessagesSendParams
		    {
			    UserId = ID,
			    Message = mess
			});
	    }
	}
}
