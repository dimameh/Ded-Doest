using System;
using System.Collections.Generic;
using VkNet;
using VkNet.Enums;
using VkNet.Enums.Filters;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Utils;

namespace VkBot
{
    class Program
    {
        static VkApi vkapi = new VkApi();
        static string[] Commands = { "хочу кушоть", "хочу поделиться", "hello" };

        static void Main(string[] args)
        {
            string Login = @"codemekh@gmail.com";
            string Password = @"rhbpbcrfgen";
            ulong ID = 6768611;

            /*Console.WriteLine("Montece VK bot");
            Console.Write("Login: ");
            Login = Console.ReadLine();
            Console.Write("Password: ");
            Password = Console.ReadLine();
            Console.Write("App ID: ");
            ID = ulong.Parse(Console.ReadLine());*/
            Console.WriteLine("Auth started...");
			//if (Auth(Login, Password, ID))
			//{
			//    Console.WriteLine("Auth completed.");
			//    var Friends = GetFriends();
			//    User I = vkapi.Users.Get(vkapi.UserId.Value);
			//    Console.WriteLine("-1:" + I.FirstName + " " + I.LastName);
			//    for (int i = 0; i < Friends.Count; i++)
			//    {
			//        Console.WriteLine(i + ":" + Friends[i].FirstName + " " + Friends[i].LastName);
			//    }
			//    Console.Write("Введите номер друга: ");
			//    int number = int.Parse(Console.ReadLine());
			//    while (number > Friends.Count && number != -1)
			//    {
			//        Console.WriteLine("Неверный номер!");
			//        Console.Write("Введите номер друга: ");
			//        number = int.Parse(Console.ReadLine());
			//    }
			//    if (number == -1)
			//    {
			//        Console.WriteLine("Выбраны вы: " + I.FirstName + " " + I.LastName);
			//        CheckMessages(I.Id);
			//    }else
			//    {
			//        Console.WriteLine("Выбран друг: " + Friends[number].FirstName + " " + Friends[number].LastName);
			//        CheckMessages(Friends[number].Id);
			//    }

			//    Console.WriteLine("Нажмите ENTER чтобы продолжить...");
			//    Console.ReadLine();
			//}
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
		        case 0:
			        SendMessage(CheckedUserID, "Где вы находитесь? (Город)");
			        GetSpotsFromCity(CheckedUserID,
				        vkapi.Messages.GetHistory(
					        new MessagesGetHistoryParams { UserId = CheckedUserID, Count = 5 }));
			        break;
		        case 1: 
			        break;
		        case 2:
			        SendMessage(CheckedUserID, "Где вы находитесь? (Город)");
			        break;
		        default: /*Error*/ break;
	        }
        }

	    private static void GetSpotsFromCity(long CheckedUserID, MessagesGetObject dialog)
	    {
		    string city = dialog.Messages[0].Body;
			List<Spot> spotList = new List<Spot>();
			//TODO: заполнить список точек из БД, выбирая по нужному городу.
			//SendMessage(CheckedUserID,);
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
    }
}
