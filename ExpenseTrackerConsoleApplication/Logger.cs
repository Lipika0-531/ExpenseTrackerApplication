using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerConsoleApplication
{
    public class Logger : ILogger
    {
        /// <summary>
        /// File Path.
        /// </summary>
        public string FilePath;

        public Logger(string filePath)
        {
            FilePath = filePath;
        }

        /// <summary>
        /// Logs errors to file.
        /// </summary>
        /// <param name="message">Message to log</param>
        public void LogErrors(string message)
        {
            string timestamp = DateTime.Now.ToString();
            string logEntry = $"{timestamp},{message}";
            using StreamWriter writer = File.AppendText(FilePath);
            writer.WriteLine(logEntry);
        }
    }
}
