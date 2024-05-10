using ExpenseTrackerConsoleApplication;
using System.Globalization;

namespace ExpenseTrackerTests
{
    public class ExpenseTrackerApplicationTests
    {
        Category categoryInstance;
        FileManager fileManagerInstance;
        Services serviceInstance;
        UserAuthentication userAuthenticationInstance;
        User userInstance;
        Logger loggerInstance;
        public ExpenseTrackerApplicationTests()
        {
            loggerInstance = new Logger(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Log.txt"));
            categoryInstance = new Category();
            serviceInstance = new Services(categoryInstance, loggerInstance);
            fileManagerInstance = new FileManager(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), loggerInstance);
            userAuthenticationInstance = new UserAuthentication(fileManagerInstance, serviceInstance, categoryInstance, loggerInstance);
        }


        [Theory]
        [InlineData("LapTop")]
        public void CategoryValueToBeAdded_CategoriesList_IsAddedInCategoriesList(string input)
        {
            //Arrange
            Category category = new Category();

            //Act
            category.Categories.Add(input);

            //Assert
            Assert.Contains(input, category.Categories);
        }

        [Theory]
        [InlineData("Sru", "U3J1")]
        public void PasswordAndEncodedPassword_EncodingPassword_IsActualPasswordAndEncodePasswordSame(string input, string actualOutput)
        {
            //Arrange
            Password password = new Password(input);

            //Assert
            Assert.Equal(password.EncodedPassword, actualOutput);
        }


        [Theory]
        [InlineData("Sru")]
        public void Password_EncodingPassword_IsEncoded(string input)
        {
            //Arrange
            Password password = new Password(input);

            //Act
            var expectedOutput = password.PasswordChecker(input);

            //Assert
            Assert.True(expectedOutput);
        }


        [Theory]
        [InlineData("Lipika", "Lipi")]
        public async Task UserNameAndPassword_LoggingOut_IsLoggedOut(string userName, string userPassword)
        {
            //Arrange
            User user = new User(userName, userPassword, categoryInstance, serviceInstance, loggerInstance);
            ActiveUsers.ActiveUser!.UserName = user.UserName;

            //Act
            await userAuthenticationInstance.LogOut(user);

            //Assert
            Assert.True(ActiveUsers.ActiveUser == null);
        }

        [Theory]
        [InlineData("Lipika", "Lipi")]
        public void UserNameAndPassword_LoggingIn_IsLoggedIn(string userName, string userPassword)
        {
            //Arrange
            User user = new User(userName, userPassword, categoryInstance, serviceInstance, loggerInstance);

            //Act
            bool isLogged =  fileManagerInstance.LogInDetailsToFile(userName, userPassword, user);

            //Assert
            Assert.True(isLogged);
        }

        [Theory]
        [InlineData("Test", "TestName")]
        public void UserNameAndPassword_IncorrectLoggingDetails_IsLoggedIn(string userName, string userPassword)
        {
            //Arrange
            User user = new User(userName, userPassword, categoryInstance, serviceInstance, loggerInstance);

            //Act
            bool isLogged = fileManagerInstance.LogInDetailsToFile(userName, userPassword, user);

            //Assert
            Assert.False(isLogged);
        }

        [Theory]
        [InlineData("Lipika", "Lipi")]
        public void UserNameAndPassword_SigningIn_IsSignedIn(string userName, string password)
        {
            //Act
            var user = userAuthenticationInstance.UserSignIn(userName, password, fileManagerInstance);

            //Assert
            Assert.Equal(userName, user.UserName);
        }

        [Theory]
        [InlineData("Lipika", "Lipi")]
        public void UserNameAndPassword_AddingIncome_IsIncomeAdded(string userName, string password)
        {
            //Arrange
            User user = new User(userName, password, categoryInstance, serviceInstance, loggerInstance);
            Income income = new Income(DateOnly.Parse("2023/10/10"), "Food", 555, ModesOfCash.Cash, "From Testing!", 5);

            //Act
            user.Incomes.Add(income);

            //Assert
            Assert.Contains(income, user.Incomes);
        }

        [Fact]
        public void UserNameAndPassword_WhenAddingNullIncome_IsExceptionThrown()
        {
            //Arrange
            User user = new User("Lipika", "Lipi", categoryInstance, serviceInstance, loggerInstance);
            Income income = null!;

            //Assert
            Assert.Throws<Exception>(() => user.AddIncome(income));
        }
        [Theory]
        [InlineData("Lipika", "Lipi")]
        public void UserNameAndPassword_AddingExpense_IsExpenseAdded(string userName, string password)
        {
            //Arrange
            User user = new User(userName, password, categoryInstance, serviceInstance, loggerInstance);
            Expense expense = new Expense(DateOnly.Parse("2023/10/10"), "Food", 555, ModesOfCash.Cash, "From Testing!", 5);

            //Act
            user.Expenses.Add(expense);

            //Assert
            Assert.Contains(expense, user.Expenses);
        }

        [Fact]
        public void UserNameAndPassword_WhenAddingNullExpense_IsExceptionThrown()
        {
            //Arrange
            User user = new User("Lipika", "Lipi", categoryInstance, serviceInstance, loggerInstance);
            Expense expense = null!;

            //Assert
            Assert.Throws<Exception>(() => user.AddExpense(expense!));
        }

        [Theory]
        [InlineData("Lipika", "Lipi", 10)]
        public void UserNamePasswordAndTotalExpense_TotalExpense_ExpectedOutput(string userName, string password, decimal expectedOutput)
        {
            //Arrange
            User user = new User(userName, password, categoryInstance, serviceInstance, loggerInstance);

            Expense expense1 = new Expense(DateOnly.Parse("2023/10/10"), "Food", 5, ModesOfCash.Cash, "From Testing!", 5);
            user.Expenses.Add(expense1);

            Expense expense2 = new Expense(DateOnly.Parse("2023/10/10"), "Food", 5, ModesOfCash.Cash, "From Testing!", 5);
            user.Expenses.Add(expense2);

            //Act
            decimal actualOutput = user.ViewTotalExpense(user);

            //Assert
            Assert.Equal(expectedOutput, actualOutput);
        }

        [Theory]
        [InlineData("Lipika", "Lipi", 10)]
        public void UserNamePasswordAndTotalIncome_TotalIncome_ExpectedOutput(string userName, string password, double expectedOutput)
        {
            //Arrange
            User user = new User(userName, password, categoryInstance, serviceInstance, loggerInstance);

            Income income1 = new Income(DateOnly.Parse("2023/10/10"), "Food", 5, ModesOfCash.Cash, "From Testing!", 5);
            user.Incomes.Add(income1);

            Income income2 = new Income(DateOnly.Parse("2023/10/10"), "Food", 5, ModesOfCash.Cash, "From Testing!", 5);
            user.Incomes.Add(income2);

            //Act
            decimal actualOutput = user.ViewTotalIncome(user);

            //Assert
            Assert.Equal(expectedOutput.ToString(), actualOutput.ToString());
        }

        [Theory]
        [InlineData("TestName", "TestPassword", 1, "11/10/2023")]
        public void UserNamePasswordAndIndexAndValueToUpdate_UpdateDateForExpense_IsUpdated(string userName, string userPassword, int indexToUpdate, string expectedOutput)
        {
            //Arrange
            User user = new User(userName, userPassword, categoryInstance, serviceInstance, loggerInstance);

            Expense expenseData = new Expense(DateOnly.Parse("2023/10/10"), "Food", 5, ModesOfCash.Cash, "From Testing!", 5);
            user.Expenses.Add(expenseData);

            //Act
            user.UpdateDateForExpense(indexToUpdate, DateOnly.Parse(expectedOutput));

            //Arrange
            Assert.Equal(DateOnly.Parse(expectedOutput), user.Expenses[indexToUpdate - 1].DateOnly);
        }

        [Theory]
        [InlineData("TestName", "TestPassword", 1, 10)]
        public void UserNamePasswordAndIndexAndValueToUpdate_UpdateAmountForExpense_IsUpdated(string userName, string userPassword, int indexToUpdate, decimal expectedOutput)
        {
            //Arrange
            User user = new User(userName, userPassword, categoryInstance, serviceInstance, loggerInstance);

            Expense expenseData = new Expense(DateOnly.Parse("2023/10/10"), "Food", 5, ModesOfCash.Cash, "From Testing!", 5);

            //Act
            user.Expenses.Add(expenseData);
            user.UpdateAmountForExpense(indexToUpdate, expectedOutput);

            //Assert
            Assert.Equal(expectedOutput, user.Expenses[indexToUpdate - 1].Amount);
        }

        [Theory]
        [InlineData("TestName", "TestPassword", 1, ModesOfCash.ECash)]
        public void UserNamePasswordAndIndexAndValueToUpdate_UpdateModeOfCashForExpense_IsUpdated(string userName, string userPassword, int indexToUpdate, ModesOfCash mode)
        {
            //Arrange
            User user = new User(userName, userPassword, categoryInstance, serviceInstance, loggerInstance);
            Expense expenseData = new Expense(DateOnly.Parse("2023/10/10"), "Food", 5, ModesOfCash.Cash, "From Testing!", 5);

            //Act
            user.Expenses.Add(expenseData);
            user.UpdateModeOfCashForExpense(indexToUpdate, mode);

            //Assert
            Assert.Equal(mode, user.Expenses[indexToUpdate - 1].AmountMode);
        }

        [Theory]
        [InlineData("TestName", "TestPassword", 1, 555)]
        public void UserNamePasswordAndIndexAndValueToUpdate_UpdateAccountNumberForExpense_IsUpdated(string userName, string userPassword, int indexToUpdate, int expectedOutput)
        {
            //Arrange
            User user = new User(userName, userPassword, categoryInstance, serviceInstance, loggerInstance);
            Expense expenseData = new Expense(DateOnly.Parse("2023/10/10"), "Food", 5, ModesOfCash.Cash, "From Testing!", 5);

            //Act
            user.Expenses.Add(expenseData);
            user.UpdateAccountNumberForExpense(indexToUpdate, expectedOutput);

            //Assert
            Assert.Equal(expectedOutput, user.Expenses[indexToUpdate - 1].Account);
        }

        [Theory]
        [InlineData("TestName", "TestPassword", 1, "Food")]
        public void UserNamePasswordAndIndexAndValueToUpdate_UpdateCategoryForExpense_IsUpdated(string userName, string userPassword, int indexToUpdate, string expectedOutput)
        {
            //Arrange
            User user = new User(userName, userPassword, categoryInstance, serviceInstance, loggerInstance);
            Expense expenseData = new Expense(DateOnly.Parse("2023/10/10"), "Groceries", 5, ModesOfCash.Cash, "From Testing!", 5);

            //Act
            user.Expenses.Add(expenseData);
            user.UpdateCategoryForExpense(indexToUpdate, expectedOutput);

            //Assert
            Assert.Equal(expectedOutput, user.Expenses[indexToUpdate - 1].Category);
        }

        [Theory]
        [InlineData("TestName", "TestPassword", 1, "NotesForExpense")]
        public void UserNamePasswordAndIndexAndValueToUpdate_UpdateNotesForExpense_IsUpdated(string userName, string userPassword, int indexToUpdate, string expectedOutput)
        {
            //Arrange
            User user = new User(userName, userPassword, categoryInstance, serviceInstance, loggerInstance);
            Expense expenseData = new Expense(DateOnly.Parse("2023/10/10"), "Groceries", 5, ModesOfCash.Cash, "From Testing!", 5);

            //Act
            user.Expenses.Add(expenseData);
            user.UpdateNotesForExpense(indexToUpdate, expectedOutput);

            //Assert
            Assert.Equal(expectedOutput, user.Expenses[indexToUpdate - 1].Note);
        }

        [Theory]
        [InlineData("Test", "TestPassword", 1, "11/10/11")]
        public void UserNamePasswordAndIndexAndValueToUpdate_UpdateDateForIncome_IsUpdated(string userName, string userPassword, int indexToUpdate, string expectedOutput)
        {
            //Arrange
            User user = new User(userName, userPassword, categoryInstance, serviceInstance, loggerInstance);
            Income income = new Income(DateOnly.Parse("2023/10/10"), "Food", 5, ModesOfCash.Cash, "From Testing!", 5);

            //Act
            user.Incomes.Add(income);
            user.UpdateDateForIncome(indexToUpdate, DateOnly.Parse(expectedOutput));

            //Assert
            Assert.Equal(DateOnly.Parse(expectedOutput), user.Incomes[indexToUpdate - 1].DateOnly);

        }

        [Theory]
        [InlineData("TestName", "TestPassword", 1, 10)]
        public void UserNamePasswordAndIndexAndValueToUpdate_UpdateAmountForIncome_IsUpdated(string userName, string userPassword, int indexToUpdate, decimal expectedOutput)
        {
            //Arrange
            User user = new User(userName, userPassword, categoryInstance, serviceInstance, loggerInstance);
            Income income = new Income(DateOnly.Parse("2023/10/10"), "Food", 5, ModesOfCash.Cash, "From Testing!", 5);

            //Act
            user.Incomes.Add(income);
            user.UpdateAmountForIncome(indexToUpdate, expectedOutput);

            //Assert
            Assert.Equal(expectedOutput, user.Incomes[indexToUpdate - 1].Amount);
        }

        [Theory]
        [InlineData("TestName", "TestPassword", 1, ModesOfCash.ECash)]
        public void UserNamePasswordAndIndexAndValueToUpdate_UpdateModeOfCashForIncome_IsUpdated(string userName, string userPassword, int indexToUpdate, ModesOfCash mode)
        {
            //Arrange
            User user = new User(userName, userPassword, categoryInstance, serviceInstance, loggerInstance);
            Income income = new Income(DateOnly.Parse("2023/10/10"), "Food", 5, ModesOfCash.ECash, "From Testing!", 5);

            //Act
            user.Incomes.Add(income);
            user.UpdateModeOfCashForIncome(indexToUpdate, mode);

            //Assert
            Assert.Equal(mode, user.Incomes[indexToUpdate - 1].AmountMode);

        }


        [Theory]
        [InlineData("TestName", "TestPassword", 1, 555)]
        public void UserNamePasswordAndIndexAndValueToUpdate_UpdateAccountNumberForIncome_IsUpdated(string userName, string userPassword, int indexToUpdate, int expectedOutput)
        {
            //Arrange
            User user = new User(userName, userPassword, categoryInstance, serviceInstance, loggerInstance);
            Income income = new Income(DateOnly.Parse("2023/10/10"), "Food", 5, ModesOfCash.Cash, "From Testing!", 5);

            //Act
            user.Incomes.Add(income);
            user.UpdateAccountNumberForIncome(indexToUpdate, expectedOutput);

            //Assert
            Assert.Equal(expectedOutput, user.Incomes[indexToUpdate - 1].Account);
        }

        [Theory]
        [InlineData("TestName", "TestPassword", 1, "Food")]
        public void UserNamePasswordAndIndexAndValueToUpdate_UpdateCategoryForIncome_IsUpdated(string userName, string userPassword, int indexToUpdate, string expectedOutput)
        {
            //Arrange
            User user = new User(userName, userPassword, categoryInstance, serviceInstance, loggerInstance);
            Income income = new Income(DateOnly.Parse("2023/10/10"), "Food", 5, ModesOfCash.Cash, "From Testing!", 5);

            //Act
            user.Incomes.Add(income);
            user.UpdateCategoryForIncome(indexToUpdate, expectedOutput);

            //Assert
            Assert.Equal(expectedOutput, user.Incomes[indexToUpdate - 1].Category);
        }

        [Theory]
        [InlineData("TestName", "TestPassword", 1, "NotesForIncome")]
        public void UserNamePasswordAndIndexAndValueToUpdate_UpdateNotesForIncome_IsUpdated(string userName, string userPassword, int indexToUpdate, string expectedOutput)
        {
            //Arrange
            User user = new User(userName, userPassword, categoryInstance, serviceInstance, loggerInstance);
            Income income = new Income(DateOnly.Parse("2023/10/10"), "Food", 5, ModesOfCash.Cash, "From Testing!", 5);

            //Act
            user.Incomes.Add(income);
            user.UpdateNotesForIncome(indexToUpdate, expectedOutput);

            //Assert
            Assert.Equal(expectedOutput, user.Incomes[indexToUpdate - 1].Note);
        }

        [Theory]
        [InlineData("TestName", "TestPassword", 1)]
        public void UserNamePasswordAndIndexAndValueToDelete_DeleteIncome_IsDeleted(string userName, string userPassword, int indexToDelete)
        {
            //Arrange
            User user = new User(userName, userPassword, categoryInstance, serviceInstance, loggerInstance);
            Income income1 = new Income(DateOnly.Parse("2023/10/10"), "Food", 5, ModesOfCash.Cash, "From Testing!", 5);
            Income income2 = new Income(DateOnly.Parse("2023/10/10"), "Food", 5, ModesOfCash.Cash, "From Testing!", 5);

            //Act

            user.Incomes.Add(income1);
            user.Incomes.Add(income2);

            user.DeleteIncome(indexToDelete, user);

            //Assert
            Assert.Equal(indexToDelete , user.Incomes.Count);

        }

        [Theory]
        [InlineData("TestName", "TestPassword", 3)]
        public void UserNamePasswordAndIndexAndValueToDelete_DeleteIncomeWhenIndexNotValid_IsExceptionThrown(string userName, string userPassword, int indexToUpdate)
        {
            //Arrange
            User user = new User(userName, userPassword, categoryInstance, serviceInstance, loggerInstance);
            Income income1 = new Income(DateOnly.Parse("2023/10/10"), "Food", 5, ModesOfCash.Cash, "From Testing!", 5);
            Income income2 = new Income(DateOnly.Parse("2023/10/10"), "Food", 5, ModesOfCash.Cash, "From Testing!", 5);

            //Act
            user.Incomes.Add(income1);
            user.Incomes.Add(income2);

            //Assert
            Assert.Throws<Exception>(() => user.DeleteIncome(indexToUpdate, user));

        }



        [Theory]
        [InlineData("TestName", "TestPassword", 1)]
        public void UserNamePasswordAndIndexAndValueToDelete_DeleteExpense_IsDeleted(string userName, string userPassword, int indexToDelete)
        {
            //Arrange
            User user = new User(userName, userPassword, categoryInstance, serviceInstance, loggerInstance);
            Expense expenseData1 = new Expense(DateOnly.Parse("2023/10/10"), "Groceries", 5, ModesOfCash.Cash, "From Testing!", 5);
            Expense expenseData2 = new Expense(DateOnly.Parse("2023/10/10"), "Groceries", 5, ModesOfCash.Cash, "From Testing!", 5);

            //Act
            user.Expenses.Add(expenseData1);
            user.Expenses.Add(expenseData2);

            user.DeleteExpense(indexToDelete, user);

            //Assert
            Assert.Equal(indexToDelete , user.Expenses.Count);
        }

        [Theory]
        [InlineData("TestName", "TestPassword", 3)]
        public void UserNamePasswordAndIndexAndValueToDelete_DeleteExpenseWhenIndexNotValid_IsExceptionThrown(string userName, string userPassword, int indexToDelete)
        {
            //Arrange
            User user = new(userName, userPassword, categoryInstance, serviceInstance, loggerInstance);
            Expense expenseData1 = new Expense(DateOnly.Parse("2023/10/10"), "Groceries", 5, ModesOfCash.Cash, "From Testing!", 5);
            Expense expenseData2 = new Expense(DateOnly.Parse("2023/10/10"), "Groceries", 5, ModesOfCash.Cash, "From Testing!", 5);

            //Act
            user.Expenses.Add(expenseData1);
            user.Expenses.Add(expenseData2);

            //Assert
            Assert.Throws<Exception>(() => user.DeleteExpense(indexToDelete, user));

        }

        [Theory]
        [InlineData("TestName", "TestPassword", "TestName.json")]
        public void UserNamePassword_CreatingFileNamedAsUserName_IsFileCreated(string userName, string userPassword, string expectedOutput)
        {
            //Arrange
            User user = new User(userName, userPassword, categoryInstance, serviceInstance, loggerInstance);

            //Act
            var actualOutput = fileManagerInstance.FrameUserFilePath(user);

            //Assert
            Assert.Equal(expectedOutput, actualOutput);
        }

        [Theory]
        [InlineData("Lipika", "Lipi")]
        public void UserNamePassword_LogInDetailsToFile_IsLogin(string userName, string userPassword)
        {
            //Arrange
            User user = new User(userName, userPassword, categoryInstance, serviceInstance, loggerInstance);

            //Act
            var actualOutput = fileManagerInstance.LogInDetailsToFile(userName, userPassword, user);

            //Assert
            Assert.True(actualOutput);
        }

        [Theory]
        [InlineData("Lipika", "Lipi", "{\"UserName\":\"Lipika\",\"Password\":{\"EncodedPassword\":\"TGlwaQ==\"},\"Incomes\":[],\"Expenses\":[]}")]
        public void UserNamePassword_SerializeObject_SerializedData(string userName, string userPassword, string expectedOutput)
        {
            //Arrange
            User user = new User(userName, userPassword, categoryInstance, serviceInstance, loggerInstance);

            //Act
            var actualOuput = fileManagerInstance.SerializeFile(user);

            //Assert
            Assert.Equal(expectedOutput, actualOuput);
        }

    }

}