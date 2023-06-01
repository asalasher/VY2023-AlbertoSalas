using System;

namespace DDDBankManager
{
    public interface ITransaction
    {
        DateTime Date { get; set; }
        string Id { get; set; }
        decimal Quantity { get; set; }

        string ToString();
    }
}