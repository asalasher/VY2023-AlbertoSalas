using DDDBankManager._4_IntrastructureData;
using POOBankManagerV2.Classes;

namespace DDDBankManager
{
    public class AccountManager
    {
        public Repository Repository { get; set; }

        public AccountManager() { }

        public AccountManager(Repository repository)
        {
            Repository = repository;
        }

        public User GetUserByAccountNumber(int accountNumber)
        {
            foreach (var user in Repository.users)
            {
                if (user.AccountNumber == accountNumber)
                {
                    return user;
                }
            }
            return null;
        }

        public Account GetAccountByNumber(int accountNumber)
        {
            foreach (Account account in Repository.accounts)
            {
                if (account.AccountNumber == accountNumber)
                {
                    return account;
                }
            }
            return null;
        }

        public (decimal balance, string error) CalculateBalance(int accountNumber)
        {
            Account account = GetAccountByNumber(accountNumber);
            if (account == null)
            {
                return (0, "Account not found");
            }

            decimal balance = 0.0m;
            foreach (Transaction transaction in account.Transactions)
            {
                balance += transaction.Quantity;
            }

            return (balance, null);
        }

    }
}
