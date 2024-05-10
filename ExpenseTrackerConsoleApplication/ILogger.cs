using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerConsoleApplication
{
    internal interface ILogger
    {
        /// <summary>
        /// Log errors.
        /// </summary>
        /// <param name="message">Message</param>
        public void LogErrors(string message);
    }
}
