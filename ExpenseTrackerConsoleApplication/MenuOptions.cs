using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerConsoleApplication
{
    /// <summary>
    /// Represents the MainMenu options of the current user.
    /// </summary>
    public enum MenuOptions
    {
        /// <summary>
        /// To add new income to the current user income list.
        /// </summary>
        AddIncome = 1,

        /// <summary>
        /// To add new expense to the current user expense list.
        /// </summary>
        AddExpense = 2,

        /// <summary>
        /// To view all the incomes.
        /// </summary>
        ViewIncomes = 3,

        /// <summary>
        /// To view all the expenses.
        /// </summary>
        ViewExpenses = 4,

        /// <summary>
        /// To Update the income from current user income list.
        /// </summary>
        UpdateIncomes = 5,

        /// <summary>
        /// To Update the expense from the current user expense list.
        /// </summary>
        UpdateExpenses = 6,

        /// <summary>
        /// To remove income from the current user income list.
        /// </summary>
        RemoveIncome = 7,

        /// <summary>
        /// To remove expense from the current user expense list.
        /// </summary>
        RemoveExpense = 8,

        /// <summary>
        /// Displays statistics of income and expense.
        /// </summary>
        ViewStatistic = 9,

        /// <summary>
        /// Exiting to main menu.
        /// </summary>
        Exit = 10,

    }
}
