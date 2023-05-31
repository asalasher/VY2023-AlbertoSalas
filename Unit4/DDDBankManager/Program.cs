using DDDBankManager._4_IntrastructureData;
using POOBankManagerV2.Classes;
using System.Collections.Generic;

namespace DDDBankManager
{
    public class Program
    {
        public static UserRepository userRepository;
        public static AccountRepository accountRepository;

        static void Main(string[] args)
        {
            // Mock data
            List<User> mockUsers = new List<User>()
            {
                new User(1111, "pw1"),
                new User(2222, "pw2"),
                new User(3333, "pw3"),
            };

            List<Account> mockAccounts = new List<Account>()
            {
                new Account(1111),
                new Account(2222),
                new Account(3333),
            };

            userRepository = new UserRepository(mockUsers);
            accountRepository = new AccountRepository(mockAccounts);

            new ConsoleLogger(3).Run();
        }
    }
}
