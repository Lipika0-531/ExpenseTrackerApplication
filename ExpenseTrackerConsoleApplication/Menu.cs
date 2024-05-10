using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerConsoleApplication
{
    /// <summary>
    /// Contains menu to be displayed.
    /// </summary>
    public class Menu
    {
        UserAuthentication userAuthentication;
        FileManager fileManager;
        Services services;
        Category category;
        User user = null!;


        public Menu(FileManager fileManagerInstance, Category categoryInstance, Services servicesInstance, UserAuthentication userAuthenticationInstance, User user)
        {
            fileManager = fileManagerInstance;
            category = categoryInstance;
            services = servicesInstance;
            userAuthentication = userAuthenticationInstance;
            this.user = user;
        }

        /// <summary>
        /// MainMenu to be displayed.
        /// </summary>
        /// <returns>Task</returns>
        public async Task UserMenu()
        {

            bool isExited = false;
            while (!isExited)
            {
                Parser.DisplayMessages(ConsoleColor.Cyan, "1. Add Income");
                Parser.DisplayMessages(ConsoleColor.Cyan, "2. Add Expense");
                Parser.DisplayMessages(ConsoleColor.Cyan, "3. View Income");
                Parser.DisplayMessages(ConsoleColor.Cyan, "4. View Expense");
                Parser.DisplayMessages(ConsoleColor.Cyan, "5. Update Income");
                Parser.DisplayMessages(ConsoleColor.Cyan, "6. Update Expense");
                Parser.DisplayMessages(ConsoleColor.Cyan, "7. Delete Income");
                Parser.DisplayMessages(ConsoleColor.Cyan, "8. Delete Expense");
                Parser.DisplayMessages(ConsoleColor.Cyan, "9. View Statistics");
                Parser.DisplayMessages(ConsoleColor.Cyan, "10. Exit");

                Console.Write("Choose option : ");

                if (Enum.TryParse(Console.ReadLine(), out MenuOptions option))
                {
                    try
                    {
                        switch (option)
                        {
                            case MenuOptions.AddIncome:
                                Parser.DisplayMessages(ConsoleColor.Yellow, "Add Income");
                                Income incomeToAdd = services.GetPropertiesOfIncome();
                                user?.AddIncome(incomeToAdd);
                                break;
                            case MenuOptions.AddExpense:
                                Parser.DisplayMessages(ConsoleColor.Yellow, "Add Expense");
                                Expense expenseToAdd = services.GetPropertiesOfExpense();
                                user?.AddExpense(expenseToAdd);
                                break;
                            case MenuOptions.ViewIncomes:
                                Parser.DisplayMessages(ConsoleColor.Yellow, "View Income ");
                                user?.ViewIncome(user);
                                break;
                            case MenuOptions.ViewExpenses:
                                Parser.DisplayMessages(ConsoleColor.Yellow, "View Expense ");
                                user?.ViewExpense(user);
                                break;
                            case MenuOptions.UpdateIncomes:
                                user?.UpdatingValuesForUserIncome(user);
                                break;
                            case MenuOptions.UpdateExpenses:
                                user?.UpdatingValuesForExpense(user);
                                break;
                            case MenuOptions.RemoveIncome:
                                user?.DisplayDeleteMessageForIncome(user);
                                break;
                            case MenuOptions.RemoveExpense:
                                user?.DisplayDeleteMessageForExpense(user);
                                break;
                            case MenuOptions.ViewStatistic:
                                user?.ViewBalance(user);
                                break;
                            case MenuOptions.Exit:
                                ActiveUsers.ActiveUser!.UserName = user!.UserName;
                                Parser.DisplayMessages(ConsoleColor.DarkRed, "Exiting to Main Menu");
                                isExited = true;
                                break;
                            default:
                                Parser.DisplayMessages(ConsoleColor.Red, "Invalid Input !");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Parser.DisplayMessages(ConsoleColor.Red, $"Error: {ex}");
                    }
                }
            }

        }
    }
}
