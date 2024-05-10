using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ExpenseTrackerConsoleApplication
{
    /// <summary>
    /// Adding and Reading Files.
    /// </summary>
    public class FileManager 
    {
        string filePath;
        Logger logger;
        public FileManager(string userFilePath, Logger loggerInstance)
        {
            filePath = userFilePath;
            logger = loggerInstance;
        }

        /// <summary>
        /// Frame user filePath with userName.
        /// </summary>
        /// <param name="user">User details</param>
        /// <returns>userFile Path</returns>
        public string FrameUserFilePath(User user)
        {
            string userDetailsFile = $"{user.UserName}.json";
            return userDetailsFile;
        }

        /// <summary>
        /// Write Log in details to file.
        /// </summary>
        /// <param name="userNameLogin">userNameLogin</param>
        /// <param name="loginPassword">loginPassword</param>
        /// <param name="user">User details</param>
        /// <returns>True if Logged In.</returns>
        public bool LogInDetailsToFile(string userNameLogin, string loginPassword, User user)
        {
            string userDetailsFile = FrameUserFilePath(user);

            if (!File.Exists(Path.Combine(filePath, userDetailsFile)))
            {
                Parser.DisplayMessages(ConsoleColor.Red, "User not found ! Try signing in or Try again !");
                logger.LogErrors("User not found ! Try signing in or Try again !");
                return false;
            }
            else
            {
                string content = File.ReadAllText(Path.Combine(filePath, userDetailsFile), Encoding.UTF8);
                User userNew = JsonConvert.DeserializeObject<User>(content)!;
                user = userNew;
                Password password = new Password(loginPassword);

                if (user.UserName == userNameLogin && user.Password.EncodedPassword == password.EncodedPassword)
                {
                    return true;
                }
                else
                {
                    Parser.DisplayMessages(ConsoleColor.Red, "UserName and Password doesn't Match !");
                    logger.LogErrors("UserName and Password doesn't Match !");
                    return false;
                }
            }
        }

        /// <summary>
        /// Write sign In details to file.
        /// </summary>
        /// <param name="user">user details</param>
        /// <returns>if Signed in</returns>
        public bool WriteSignInDetailsToFile(User user)
        {
            string userDetailsFile = FrameUserFilePath(user);
            if (File.Exists(Path.Combine(filePath, userDetailsFile)))
            {
                Parser.DisplayMessages(ConsoleColor.Red, "User already exists ! Please LogIn !");
                return false;
            }
            else
            {
                using FileStream writer = File.Create(Path.Combine(filePath, userDetailsFile));
                using StreamWriter sw = new StreamWriter(writer);
                string userData = SerializeFile(user);
                sw.Write(userData);
                return true;
            }
        }

        /// <summary>
        /// Writing back the user details.
        /// </summary>
        /// <param name="user">user</param>
        public void WriteDetailsToFile(User user)
        {
            string userDetailsFile = FrameUserFilePath(user);
            if (File.Exists(Path.Combine(filePath, userDetailsFile)))
            {
                using FileStream writer = File.OpenWrite(Path.Combine(filePath, userDetailsFile));
                using StreamWriter sw = new StreamWriter(writer);
                string userData = SerializeFile(user);
                sw.Write(userData);
            }
        }
        /// <summary>
        /// Serialize file.
        /// </summary>
        /// <param name="user">user details</param>
        /// <returns>user data</returns>
        public string SerializeFile(User user)
        {
            string jsonUserData = JsonConvert.SerializeObject(user);
            return jsonUserData;
        }

    }
}
