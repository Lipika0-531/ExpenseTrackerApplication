using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerConsoleApplication
{
    /// <summary>
    /// Encode and Decode Password.
    /// </summary>
    public class Password
    {
        public Password()
        {
                
        }
        /// <summary>
        /// EncodedPassword.
        /// </summary>
        /// <param name="value">Value</param>
        public Password(string value)
        {
            this.EncodedPassword = EncodePassword(value);
        }

        public string EncodedPassword { get; set; }


        /// <summary>
        /// Encode Password.
        /// </summary>
        /// <param name="value">password</param>
        /// <returns>encoded password</returns>
        public string EncodePassword(string value)
        {
            byte[] bytes = new byte[value.Length];
            bytes = Encoding.UTF8.GetBytes(value);
            var encodedPassword = Convert.ToBase64String(bytes, 0, bytes.Length);
            return encodedPassword;
        }

        /// <summary>
        /// Check password with encodedPassword.
        /// </summary>
        /// <param name="password">Password</param>
        /// <returns>bool</returns>
        public bool PasswordChecker(string password)
        {
            var encodedPassword = EncodePassword(password);
            return (encodedPassword == this.EncodedPassword);
        }
    }
}
