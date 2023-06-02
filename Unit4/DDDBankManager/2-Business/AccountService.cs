using DDDBankManager._4_IntrastructureData;
using POOBankManagerV2.Classes;
using System.Collections.Generic;

namespace DDDBankManager
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository userRepository = Program.userRepository;
        private readonly IAccountRepository accountRepository = Program.accountRepository;

        public AccountService(IUserRepository userRepository, IAccountRepository accountRepository)
        {
            this.userRepository = userRepository;
            this.accountRepository = accountRepository;
        }

        private Account GetAccountById(int accountNumber)
        {
            return accountRepository.GetById(accountNumber);
        }

        private User GetUserById(int accountNumber)
        {
            return userRepository.GetById(accountNumber);
        }

        public (User user, string error) AuthenticateUser(int accountNumber, string password)
        {
            User user = GetUserById(accountNumber);
            if (user != null && user.VerifyPassword(password))
            {
                return (user, null);
            }

            return (null, "Account number or password incorrect");
        }

        public (List<string>, string error) GetAccountTrasactions(int accountNumber, TransactionType transactionType)
        {
            Account account = GetAccountById(accountNumber);
            if (account == null)
            {
                return (null, "account not found");
            }

            List<string> transactions = new List<string>();
            foreach (var transaction in account.Transactions)
            {
                if (transactionType == TransactionType.Income && transaction.Quantity > 0)
                {
                    transactions.Add(transaction.ToString());
                }
                else if (transactionType == TransactionType.Outcome && transaction.Quantity < 0)
                {
                    transactions.Add(transaction.ToString());
                }
                else
                {
                    transactions.Add(transaction.ToString());
                }
            }
            return (transactions, null);
        }

        public (decimal balance, string error) GetAccountBalance(int accountNumber)
        {
            Account account = GetAccountById(accountNumber);
            if (account == null)
            {
                return (0, "account not found");
            }

            return (account.GetBalance(), null);
        }

        public (bool status, string error) InsertMoney(int accountNumber, decimal amount)
        {
            Account account = GetAccountById(accountNumber);
            if (account == null)
            {
                return (false, "account not found");
            }

            Transaction transaction = new Transaction(amount);

            account.AddTransaction(transaction);
            bool status = accountRepository.Set(account);
            if (status)
            {
                return (true, null);
            }
            else
            {
                return (false, "transaction could not be saved correctly");
            }
        }

        public (bool status, string error) WithdrawMoney(int accountNumber, decimal amount)
        {
            Account account = GetAccountById(accountNumber);
            if (account == null)
            {
                return (false, "account not found");
            }

            Transaction transaction = new Transaction(-1 * amount);

            account.AddTransaction(transaction);
            bool status = accountRepository.Set(account);
            if (status)
            {
                return (true, null);
            }
            else
            {
                return (false, "transaction could not be saved correctly");
            }
        }

    }
}
