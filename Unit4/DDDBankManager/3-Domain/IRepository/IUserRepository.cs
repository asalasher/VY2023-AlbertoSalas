namespace DDDBankManager._4_IntrastructureData
{
    public interface IUserRepository
    {
        User GetById(int accountNumber);
        bool Set(User userObject);
    }
}