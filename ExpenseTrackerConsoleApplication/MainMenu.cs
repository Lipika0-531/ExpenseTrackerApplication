using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerConsoleApplication
{
    /// <summary>
    /// Represents main menu options.
    /// </summary>
    public enum MainMenu
    {
        /// <summary>
        /// To add new user.
        /// </summary>
        SignUp = 1,

        /// <summary>
        /// To login existing user.
        /// </summary>
        LogIn = 2,

        /// <summary>
        /// To log out user.
        /// </summary>
        LogOut = 3,

        /// <summary>
        /// To exit application.
        /// </summary>
        Exit = 4,
    }
}
