
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ExpenseTrackerConsoleApplication
{
    public class UserAuthentication
    {
        FileManager fileManager;
        Services services;
        Category category;
        Logger logger;

        public UserAuthentication(FileManager fileManagerInstance, Services serviceInstance, Category categoryInstance, Logger loggerInstance)
        {
            fileManager = fileManagerInstance;
            services = serviceInstance;
            category = categoryInstance;
            logger = loggerInstance;
        }

        /// <summary>
        /// User logIn.
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <param name="password">Password</param>
        /// <returns>Task</returns>
        public async Task LogIn(string userName, string password, User user)
        {
            if (fileManager.LogInDetailsToFile(userName, password, user))
            {
                int i = 0;
                await Task.Run(() =>
                {
                    while (i <= 3)
                    {
                        Console.Write(".");
                        Thread.Sleep(1000);
                        i++;
                    }

                    ActiveUsers.ActiveUser = user;
                    ActiveUsers.ActiveUser!.UserName = user.UserName;
                    Parser.DisplayMessages(ConsoleColor.Green, $"\nSuccessfully Logged in ! \n Welcome {userName}");
                    logger.LogErrors($"\nSuccessfully Logged in ! \n Welcome {userName}");
                    Thread.Sleep(100);
                });
            }
            else
            {
                logger.LogErrors("No User found ! \nTry Signing In or Try again! :)");
            }
        }

        /// <summary>
        /// User LogOut.
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <param name="password">Password</param>
        /// <returns>Task</returns>
        public async Task LogOut(User user)
        {
            if (ActiveUsers.ActiveUser != null)
            {
                int i = 0;
                await Task.Run(() =>
                {
                    while (i <= 3)
                    {
                        Console.Write(".");
                        Thread.Sleep(1000);
                        i++;
                    }
                    ActiveUsers.ActiveUser = null;
                    Parser.DisplayMessages(ConsoleColor.Green, $"Successfully Logged out ! \n ThankYou {user.UserName}");
                    logger.LogErrors($"Successfully Logged out ! \n ThankYou {user.UserName}");

                    Thread.Sleep(100);
                });
                Console.Write('\n');
            }
            else
            {
                Parser.DisplayMessages(ConsoleColor.Red, "No User found !");
                logger.LogErrors("No User found !");
            }

            return;
        }

        /// <summary>
        /// Users list.
        /// </summary>
        public static Dictionary<string, User> Users = new Dictionary<string, User>();


        /// <summary>
        /// User SignIn.
        /// </summary>
        /// <param name="userName">userName</param>
        /// <param name="password">Password</param>
        /// <returns>Added User</returns>
        public User UserSignIn(string userName, string password, FileManager fileManager)
        {
            var addedUser = new User(userName, password, category, services, logger);
            if (!fileManager.WriteSignInDetailsToFile(addedUser))
            {
                return null;
            }
            else
            {
                Users[userName] = addedUser;
                return addedUser;
            }

            
        }

        /// <summary>
        /// Check userName is unique.
        /// </summary>
        /// <param name="userName">userName</param>
        /// <returns>Unique userName</returns>
        public string CheckIfUserNameUnique(string userName)
        {
            while (Users.ContainsKey(userName))
            {
                userName = Parser.ValidateInputs<string>("UserName already taken, Enter valid UserName : ");
                logger.LogErrors("UserName already taken, Enter valid UserName : ");
            }
            return userName;
        }
    }
}
