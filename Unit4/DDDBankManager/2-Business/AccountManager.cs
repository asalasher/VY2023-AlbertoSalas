using POOBankManagerV2.Classes;
using System.Collections.Generic;

namespace DDDBankManager
{
    public class AccountManager
    {
        private readonly Account account;

        public AccountManager() { }

        public AccountManager(Account account)
        {
            this.account = account;
        }

        public (decimal balance, string error) CalculateBalance(int accountNumber)
        {
            decimal balance = 0.0m;

            Account account = Program.accountRepository.GetById(accountNumber);
            if (account == null)
            {
                return (0, "account not found");

            }

            foreach (Transaction transaction in account.Transactions)
            {
                balance += transaction.Quantity;
            }

            return (balance, null);
        }

        public (User user, string error) AuthenticateUser(int accountNumber, string password)
        {
            User user = Program.userRepository.GetById(accountNumber);

            if (user != null && user.VerifyPassword(password))
            {
                return (user, null);
            }

            return (null, "Account number or password incorrect");
        }

        public (List<Transaction>, string error) GetAccountTrasactions(int accountNumber)
        {
            Account account = Program.accountRepository.GetById(accountNumber);
            if (account == null)
            {
                return (null, "account not found");

            }

            return (account.Transactions, null);
        }

    }
}
