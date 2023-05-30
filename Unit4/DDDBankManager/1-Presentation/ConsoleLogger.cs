using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDBankManager
{
    public class ConsoleLogger
    {

        private readonly List<string> optionNames = new List<string>(){
                {"1. Money income"},
                {"2. Money outcome"},
                {"3. List all movements"},
                {"4. List incomes"},
                {"5. List outcomes"},
                {"6. Show current money"},
                {"7. Exit"},
        };

        private bool exit = false;
        private int numberOfAttempts = 0;
        private readonly int maxNumberOfAttempts;
        private User activeUser = null;

        public ConsoleLogger(int maxNumberOfAttempts)
        {
            this.maxNumberOfAttempts = maxNumberOfAttempts;
        }

        public void Run()
        {
            LogInUser();
            if (exit) { return; }

            do
            {
                PrintOptions();
                AskForOption();
            }
            while (!exit);
        }

        public void LogInUser()
        {
            numberOfAttempts = 0;
            Console.WriteLine("Introduce your account number");

            while (numberOfAttempts < maxNumberOfAttempts)
            {
                int inputAccount = AskForInteger("Introduce your account number", 1);
                string inputPassword = AskForString("Introduce your password");
                User user = new AccountManager.GetUserByAccountNumber(inputAccount);

                if (user != null)
                {
                    Console.WriteLine("Account number of password incorrect");
                    break;
                }

                numberOfAttempts++;
                Console.WriteLine("Account number of password incorrect");
                Console.WriteLine("Try again");
            }

            Console.WriteLine("You reached the maximum number of attempts. Try latter on.");
            exit = true;
        }

        public int AskForInteger(string consoleText, int minimumValue)
        {
            numberOfAttempts = 0;
            Console.WriteLine($"{consoleText}. It must be an integer greater or equal to {minimumValue}.");

            while (numberOfAttempts < maxNumberOfAttempts)
            {
                (int validatedInput, string error) = new InputValidator().ParseInteger(Console.ReadLine(), minimumValue);
                if (error is null)
                {
                    return validatedInput;
                }
                else
                {
                    numberOfAttempts++;
                    Console.WriteLine(error);
                    Console.WriteLine("Please make sure your input is correct");
                    Console.WriteLine($"{maxNumberOfAttempts - numberOfAttempts} attempts left");
                }
            }
            Console.WriteLine("Too many attempts, try again later");
            exit = true;
            return 0;
        }

        public decimal AskForDecimal(string consoleText, decimal minimumValue)
        {
            numberOfAttempts = 0;
            Console.WriteLine($"{consoleText}. It must be a decimal number greater or equal to {minimumValue}.");

            while (numberOfAttempts < maxNumberOfAttempts)
            {
                (decimal validatedInput, string error) = new InputValidator().ParseDecimal(Console.ReadLine(), minimumValue);

                if (error is null)
                {
                    return validatedInput;
                }
                else
                {
                    numberOfAttempts++;
                    Console.WriteLine(error);
                    Console.WriteLine("Please make sure your input is correct");
                    Console.WriteLine($"{maxNumberOfAttempts - numberOfAttempts} attempts left");
                }
            }
            Console.WriteLine("Too many attempts, try again later");
            exit = true;
            return 0;
        }

        public string AskForString(string consoleText)
        {
            numberOfAttempts = 0;
            Console.WriteLine($"{consoleText}. It must be a valid string");

            while (numberOfAttempts < maxNumberOfAttempts)
            {
                (string validatedInput, string error) = new InputValidator().ParseString(Console.ReadLine());

                if (error is null)
                {
                    return validatedInput;
                }
                else
                {
                    numberOfAttempts++;
                    Console.WriteLine(error);
                    Console.WriteLine("Please make sure your input is a positive integer");
                    Console.WriteLine($"{maxNumberOfAttempts - numberOfAttempts} attempts left");
                }
            }
            Console.WriteLine("Too many attempts, try again later");
            exit = true;
            return null;
        }

        public void PrintOptions()
        {
            foreach(string option in optionNames)
            {
                Console.WriteLine(option);
            }
        }

        public void AskForOption()
        {
            string chosenOption = AskForString("Introduce an option");
            if (exit) { return; }

            switch (chosenOption)
            {
                case "1":
                    // Insert money
                    WithdrawMoney();
                    break;

                case "2":
                    // Withdraw money
                    InsertMoney();
                    break;

                case "3":
                    PrintTransactions(TransactionType.All);
                    break;

                case "4":
                    PrintTransactions(TransactionType.Income);
                    break;

                case "5":
                    PrintTransactions(TransactionType.Outcome);
                    break;

                case "6":
                    PrintAccountBalance();
                    // Calculate balance
                    break;

                case "7":
                    Logout();
                    break;

                default:
                    Console.WriteLine("Option not available. Please introduce a number between 1 and 7");
                    break;
            }

        }

        public void InsertMoney()
        {
            decimal amount = AskForDecimal("Insert the amount you would like to insert", 0.1m);
        }

        public void WithdrawMoney()
        {
            decimal amount = AskForDecimal("Insert the amount you would like to withdraw", 0.1m);
        }

        public void Logout()
        {
            activeUser = null;
            Console.WriteLine("Logging you out...");
        }

        public void PrintAccountBalance()
        {
            Console.WriteLine("Your account balance is:");
            decimal balance = new AccountManager().CalculateBalance(activeUser.AccountNumber);
        }

        public void PrintTransactions(TransactionType transactionType)
        {
            List<Transaction> transactions = new TransactionManager().findById(activeUser.idAccount);

            if (Transactions.Count == 0)
            {
                Console.WriteLine("There are no transactions to show");
                return;
            }

            foreach (Transaction transaction in Transactions)
            {
                switch (transactionType)
                {
                    case TransactionType.Income:
                        if (transaction.Quantity > 0)
                        {
                            Console.WriteLine(transaction.ToString());
                        }
                        break;
                    case TransactionType.Outcome:
                        if (transaction.Quantity < 0)
                        {
                            Console.WriteLine(transaction.ToString());
                        }
                        break;
                    default:
                        Console.WriteLine(transaction.ToString());
                        break;
                }
            }
            return;
        }

    }
}
