using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerConsoleApplication
{
    /// <summary>
    /// Income of the User.
    /// </summary>
    public class Income : Transaction
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Income"/> class.
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="category">Category</param>
        /// <param name="amount">Amount</param>
        /// <param name="amountModes">AmountModes</param>
        /// <param name="note">Notes</param>
        /// <param name="account">Account</param>
        public Income(DateOnly date, string category, int account, ModesOfCash amountModes,string note, decimal amount = 0) 
        {
            DateOnly = date;
            Category = category;
            Amount = amount;
            AmountMode = amountModes;
            Account = account;
            Note = note;
        }
    }
}
