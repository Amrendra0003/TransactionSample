﻿namespace Transaction.Repository
{
    public class Transactions
    {
        public long TransactionId { get; set; }
        public decimal TransactionAmount { get; set; }
        public string TransactionDescription { get; set; }
        public string TransactionDate { get; set; }
        public string TransactionStatus { get; set; }
    }
}
