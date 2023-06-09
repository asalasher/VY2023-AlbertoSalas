﻿using System;
using System.Collections.Generic;

namespace DDDBankManager
{
    public class ConsoleLogger
    {
        private IAccountService _accountService;
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

        public ConsoleLogger(int maxNumberOfAttempts, IAccountService accountService)
        {
            this.maxNumberOfAttempts = maxNumberOfAttempts;
            _accountService = accountService;
        }

        public void Run()
        {
            AuthenticateUser();
            if (exit) { return; }

            do
            {
                PrintOptions();
                AskForOption();
            }
            while (!exit);
        }

        public void AuthenticateUser()
        {
            numberOfAttempts = 0;
            Console.WriteLine("Introduce your account number");

            while (numberOfAttempts < maxNumberOfAttempts)
            {
                int inputAccount = AskForInteger("Introduce your account number", 1);
                string inputPassword = AskForString("Introduce your password");
                (User activeUser, string error) = _accountService.AuthenticateUser(inputAccount, inputPassword);

                if (error == null)
                {
                    this.activeUser = activeUser;
                    break;
                }

                numberOfAttempts++;
                Console.WriteLine(error);
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

        public decimal AskForDecimal(string consoleText, int minimumValue)
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
            foreach (string option in optionNames)
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
                    InsertMoney();
                    break;

                case "2":
                    WithdrawMoney();
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
            decimal amount = AskForDecimal("Insert the amount you would like to insert", 0);
            (bool status, string error) = _accountService.InsertMoney(activeUser.AccountNumber, amount);

            if (!status)
            {
                Console.WriteLine("Money not inserted correctly");
                Console.WriteLine(error);
            }

            Console.WriteLine("Money inserted correctly");
        }

        public void WithdrawMoney()
        {
            decimal amount = AskForDecimal("Insert the amount you would like to withdraw", 0);
            (bool status, string error) = _accountService.WithdrawMoney(activeUser.AccountNumber, amount);

            if (!status)
            {
                Console.WriteLine("Money not withdrawn correctly");
                Console.WriteLine(error);
            }

            Console.WriteLine("Money withdrawn correctly");
        }

        public void PrintAccountBalance()
        {
            (decimal balance, string error) = _accountService.GetAccountBalance(activeUser.AccountNumber);

            if (error != null)
            {
                Console.WriteLine(error);
            }

            Console.WriteLine($"Your account balance is:{balance}");
        }

        public void PrintTransactions(TransactionType transactionType)
        {
            (List<string> transactions, string error) = _accountService
                .GetAccountTrasactions(activeUser.AccountNumber, transactionType);

            if (error != null)
            {
                Console.WriteLine(error);
                return;
            }

            if (transactions.Count == 0)
            {
                Console.WriteLine("There are no transactions to show");
                return;
            }

            foreach (string transaction in transactions)
            {
                Console.WriteLine(transaction);
            }
        }

        public void Logout()
        {
            activeUser = null;
            Console.WriteLine("Logging you out...");
        }

    }
}
