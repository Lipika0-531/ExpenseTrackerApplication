using System.Runtime.CompilerServices;

namespace ExpenseTrackerConsoleApplication
{
    /// <summary>
    /// Driver class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Driver method.
        /// </summary>
        /// <returns>Task</returns>
        public static async Task Main()
        {
            Logger logger = new Logger(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Log.txt"));

            FileManager fileManager = new FileManager(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), logger);
            Category category = new Category();
            Services services = new Services(category, logger);
            UserAuthentication userAuthentication = new UserAuthentication(fileManager, services, category, logger);
            User user = null!;
            Menu menu = null!;
            string userData = string.Empty;

            string userNameToSignUp = string.Empty;
            string passwordToSignUp = string.Empty;

            try
            {

                bool isExited = false;
                while (!isExited)
                {
                    Parser.DisplayMessages(ConsoleColor.Cyan, "\nWelcome to Expense Tracker !");
                    Parser.DisplayMessages(ConsoleColor.Cyan, "1. Sign Up");
                    Parser.DisplayMessages(ConsoleColor.Cyan, "2. Log In");
                    Parser.DisplayMessages(ConsoleColor.Cyan, "3. Log Out");
                    Parser.DisplayMessages(ConsoleColor.Cyan, "4. Exit");
                    Console.Write("Choose an Option : ");

                    if (Enum.TryParse(Console.ReadLine(), out MainMenu choice))
                    {
                        switch (choice)
                        {
                            case MainMenu.SignUp:
                                Console.WriteLine("SignUp");
                                userNameToSignUp = Parser.ValidateInputs<string>("Enter UserName : ");
                                passwordToSignUp = Parser.ValidateInputs<string>("Enter Password : ");
                                userNameToSignUp = userAuthentication.CheckIfUserNameUnique(userNameToSignUp);
                                user = userAuthentication.UserSignIn(userNameToSignUp, passwordToSignUp, fileManager);
                                if(user != null)
                                {
                                    menu = new Menu(fileManager, category, services, userAuthentication, user);
                                    Parser.DisplayMessages(ConsoleColor.Green,"Successfully signed up");
                                }
                                else
                                {
                                    break;
                                }
                                break;
                            case MainMenu.LogIn:
                                Console.WriteLine("LogIn");
                                string userNameToLogin = Parser.ValidateInputs<string>("Enter User Name : ");
                                string passwordToLogin = Parser.ValidateInputs<string>("Enter Password : ");
                                User userLogin = new User(userNameToLogin, passwordToLogin, category, services, logger);
                                await userAuthentication.LogIn(userNameToLogin, passwordToLogin, userLogin);
                                user = userLogin;
                                menu = new Menu(fileManager, category, services, userAuthentication, user);
                                if(ActiveUsers.ActiveUser == null)
                                {
                                    break;
                                }
                                else if(ActiveUsers.ActiveUser!.UserName == user.UserName)
                                {
                                    await menu.UserMenu();
                                }

                                break;

                            case MainMenu.LogOut:

                                if(ActiveUsers.ActiveUser == null)
                                {
                                    Parser.DisplayMessages(ConsoleColor.Red, "Login or SignUp !");
                                }
                                else if (ActiveUsers.ActiveUser!.UserName == user!.UserName)
                                {
                                    Parser.DisplayMessages(ConsoleColor.Magenta, "Logging out !");
                                    logger.LogErrors("Logging out !");

                                    await userAuthentication.LogOut(user);
                                    fileManager.WriteDetailsToFile(user);
                                }
                                else
                                {
                                    Parser.DisplayMessages(ConsoleColor.Red, "Login or SignUp !");
                                }
                                break;

                            case MainMenu.Exit:
                                isExited = true;
                                
                                Parser.DisplayMessages(ConsoleColor.Green, $"ThankYou !");
                                Environment.Exit(0);
                                break;
                        }
                    }
                    else
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                logger.LogErrors(ex.Message);
            }

        }
    }
}