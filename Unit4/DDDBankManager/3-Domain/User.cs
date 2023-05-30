using System;
using System.Collections.Generic;

namespace DDDBankManager
{
    public class User
    {
        public int AccountNumber { get; set; }
        private string Password { get; set; }
        private List<Transaction> Transactions { get; set; }

        public User(int accountNumber, string password)
        {
            AccountNumber = accountNumber;
            Password = password;
        }

        public bool VerifyPassword(string password)
        {
            if (password == Password)
            {
                return true;
            }
            return false;
        }

    }

}
