using System.Collections.Generic;

namespace DDDBankManager
{
    public interface IAccountService
    {
        (User user, string error) AuthenticateUser(int accountNumber, string password);
        (decimal balance, string error) GetAccountBalance(int accountNumber);
        (List<string>, string error) GetAccountTrasactions(int accountNumber, TransactionType transactionType);
        (bool status, string error) InsertMoney(int accountNumber, decimal amount);
        (bool status, string error) WithdrawMoney(int accountNumber, decimal amount);
    }
}