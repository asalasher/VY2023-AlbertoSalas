using System.Collections.Generic;

namespace DDDBankManager._4_IntrastructureData
{
    public class UserRepository
    {
        private readonly List<User> users;

        public UserRepository() { }
        public UserRepository(List<User> users)
        {
            this.users = users;
        }

        public User GetById(int accountNumber)
        {
            foreach (var user in users)
            {
                if (user.AccountNumber == accountNumber)
                {
                    return user;
                }
            }
            return null;
        }

        public bool Set(User userObject)
        {
            for (var i = 0; i < users.Count; i++)
            {
                if (userObject.AccountNumber == users[i].AccountNumber)
                {
                    users[i] = userObject;
                    return true;
                }
            }
            users.Add(userObject);
            return true;
        }

    }
}
