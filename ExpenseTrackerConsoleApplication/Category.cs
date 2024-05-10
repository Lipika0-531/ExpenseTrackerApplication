using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerConsoleApplication
{
    /// <summary>
    /// List of Category to be updated later.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// List of Category available.
        /// </summary>
        public List<string> Categories = new List<string>() { "Groceries", "Food", "Electronics", "Beauty"};

        /// <summary>
        /// List of Income Categories available.
        /// </summary>
        public List<string> IncomeCategories = new List<string>() { "Salary", "Loan", "Parent's Money", "Others" };
    }
}
