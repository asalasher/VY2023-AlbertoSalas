using POOBankManagerV2.Classes;
using System.Collections.Generic;

namespace DDDBankManager._4_IntrastructureData
{
    public class Repository
    {
        public List<User> users { get; set; }
        public List<Account> accounts { get; set; }

        public Repository() { }

        public Repository(List<User> users, List<Account> accounts)
        {
            this.users = users;
            this.accounts = accounts;
        }
    }
}
