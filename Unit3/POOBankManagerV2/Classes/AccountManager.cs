using System.Collections.Generic;

namespace POOBankManagerV2
{
    public class AccountManager
    {
        private List<User> Users { get; set; }
        public AccountManager(List<User> users)
        {
            Users = users;
        }

        public User GetUserByAccountNumber(string accountNumber)
        {
            foreach (var user in Users)
            {
                if (user.AccountNumber == accountNumber)
                {
                    return user;
                }
            }
            return null;
        }

    }
}
