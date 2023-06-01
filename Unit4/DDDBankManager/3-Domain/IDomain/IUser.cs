namespace DDDBankManager
{
    public interface IUser
    {
        int AccountNumber { get; set; }

        bool VerifyPassword(string password);
    }
}