using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionProcessor.Domain.Entities
{
    public class Account
    {
        public string AccountId { get; }

        public decimal Balance { get; private set; }

        public Account(string accountId)
        {
            AccountId = accountId;
            Balance = 0;
        }

        public void Credit(decimal amount)
        {
            Balance += amount;
        }

        public bool Debit(decimal amount)
        {
            if (Balance < amount)
                return false;

            Balance -= amount;

            return true;
        }
    }
}
