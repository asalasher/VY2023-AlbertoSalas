﻿using System;

namespace DDDBankManager
{
    public class Transaction : ITransaction
    {
        public decimal Quantity { get; set; }
        public string Id { get; set; }
        public DateTime Date { get; set; }

        public Transaction(decimal quantity)
        {
            Quantity = quantity;
            Id = Guid.NewGuid().ToString();
            Date = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Id:{Id} | Date:{Date} | Qty:{Quantity}";
        }

    }
}
