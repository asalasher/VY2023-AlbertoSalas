using POOBankManagerV2.Classes;

namespace DDDBankManager._4_IntrastructureData
{
    public interface IAccountRepository
    {
        Account GetById(int accountNumber);
        bool Set(Account accountObject);
    }
}