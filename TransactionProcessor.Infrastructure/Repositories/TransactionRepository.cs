using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionProcessor.Application.Interfaces;

namespace TransactionProcessor.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ConcurrentDictionary<string, bool> _transactions = new();

        public bool Exists(string transactionId)
        {
            return _transactions.ContainsKey(transactionId);
        }

        public bool Add(string transactionId)
        {
            return _transactions.TryAdd(transactionId, true);
        }
    }
}
