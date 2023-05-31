using POOBankManagerV2.Classes;
using System.Collections.Generic;

namespace DDDBankManager._4_IntrastructureData
{
    public class AccountRepository
    {
        private readonly List<Account> accounts;
        public AccountRepository() { }

        public AccountRepository(List<Account> accounts)
        {
            this.accounts = accounts;
        }

        public Account GetById(int accountNumber)
        {
            foreach (Account account in accounts)
            {
                if (account.AccountNumber == accountNumber)
                {
                    return account;
                }
            }
            return null;
        }

        public bool Set(Account accountObject)
        {
            for (var i = 0; i < accounts.Count; i++)
            {
                if (accountObject.AccountNumber == accounts[i].AccountNumber)
                {
                    accounts[i] = accountObject;
                    return true;
                }
            }
            accounts.Add(accountObject);
            return true;
        }

    }
}
