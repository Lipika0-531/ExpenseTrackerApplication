using ConsoleTables;
using System.Text.RegularExpressions;
using static ExpenseTrackerConsoleApplication.User;

namespace ExpenseTrackerConsoleApplication
{
    /// <summary>
    /// Income and Expense services.
    /// </summary>
    public class Services
    {
        Category category;
        Logger logger;
        public Services(Category categoryInstance, Logger loggerInstance)
        {
            category = categoryInstance;
            logger = loggerInstance;
        }

        /// <summary>
        /// Get inputs for Expense.
        /// </summary>
        /// <returns>Expense</returns>
        public Expense GetPropertiesOfExpense()
        {
            DateOnly validDate;
            string date = Parser.ValidateInputsUsingRegex<string>(Constants.regexForDate, "Enter Date (yyyy/mm/dd) : ");
            while (!DateOnly.TryParse(date, out validDate))
            {
                date = Parser.ValidateInputsUsingRegex<string>(Constants.regexForDate, "Enter Date (yyyy/mm/dd) : ");
            }

            var category = GetCategoryForExpense();

            var amountMode = Parser.ValidateInputsUsingRegex<int>(Constants.regexForModes, "Choose Mode of cash : \n1.ECash \n2.Cash : ");

            int account = 0;
            ModesOfCash AmountMode;
            if (amountMode == 1)
            {
                AmountMode = ModesOfCash.ECash;
                account = Parser.ValidateInputsUsingRegex<int>(Constants.regexForAccountNumber, "Enter Account Number : ");
            }
            else
            {
                AmountMode = ModesOfCash.Cash;
            }
            var amount = Parser.ValidateInputsUsingRegex<decimal>(Constants.regexForAmount, "Enter amount : ");

            var note = Parser.ValidateInputs<string>("Enter notes : ");

            var newExpense = new Expense(validDate, category, account, AmountMode, note, amount);
            return newExpense;

        }

        /// <summary>
        /// Get Inputs for income.
        /// </summary>
        /// <returns>Income</returns>
        public Income GetPropertiesOfIncome()
        {
            DateOnly validDate;
            string date = Parser.ValidateInputsUsingRegex<string>(Constants.regexForDate, "Enter Date (yyyy/mm/dd) : ");
            while (!DateOnly.TryParse(date, out validDate))
            {
                date = Parser.ValidateInputsUsingRegex<string>(Constants.regexForDate, "Enter Date (yyyy/mm/dd) : ");
            }

            var category = GetCategoryForIncome();

            int account = 0;

            var amountMode = Parser.ValidateInputsUsingRegex<int>(Constants.regexForModes, "Choose Mode of cash : \n1.ECash \n2.Cash : ");
            ModesOfCash AmountMode;

            if (amountMode == 1)
            {
                AmountMode = ModesOfCash.ECash;
                account = Parser.ValidateInputsUsingRegex<int>(Constants.regexForAccountNumber, "Enter Account Number : ");

            }
            else
            {
                AmountMode = ModesOfCash.Cash;
            }

            var amount = Parser.ValidateInputsUsingRegex<decimal>(Constants.regexForAmount, "Enter amount : ");

            var note = Parser.ValidateInputs<string>("Enter notes : ");

            var newIncome = new Income(validDate, category, account, AmountMode, note, amount);
            return newIncome;


        }

        /// <summary>
        /// Get category for expense and income.
        /// </summary>
        /// <returns>string</returns>
        public string GetCategoryForExpense()
        {
            int i = 1;
            ConsoleTable categoryTable = new("S.No", "Category");
            foreach (var categoryValue in this.category.Categories)
            {
                categoryTable.AddRow(i, categoryValue);
                i++;
            }
            categoryTable.Write();

            var category = Parser.ValidateInputs<int>("Choose category from the above options : ");

            while (this.category.Categories.Count < category)
            {
                category = Parser.ValidateInputs<int>("Enter category was not found, Please enter valid category : ");
            }

            return this.category.Categories.ElementAt(category - 1);
        }

        /// <summary>
        /// Get category for income.
        /// </summary>
        /// <returns>category value</returns>
        public string GetCategoryForIncome()
        {
            int i = 1;
            ConsoleTable categoryTable = new("S.No", "Category");
            foreach (var categoryValue in this.category.IncomeCategories)
            {
                categoryTable.AddRow(i, categoryValue);
                i++;
            }
            categoryTable.Write();

            var category = Parser.ValidateInputs<int>("Choose category from the above options : ");

            while(this.category.IncomeCategories.Count < category)
            {
                category = Parser.ValidateInputs<int>("Enter category was not found, Please enter valid category : ");
            }
            
            return this.category.IncomeCategories.ElementAt(category - 1);
        }

        /// <summary>
        /// MainMenu for updating values.
        /// </summary>
        /// <returns>int</returns>
        public int UpdateMenu()
        {
            int i = 1;
            Console.WriteLine("Choose which to update : ");
            foreach (var updateValue in Enum.GetValues(typeof(Attributes)))
            {
                Console.WriteLine($"{i} . {updateValue}");
                i++;
            }
            _ = int.TryParse(Console.ReadLine(), out int choice);
            return choice;
        }
    }
}
