using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionProcessor.Application.Interfaces
{
    public interface ITransactionRepository
    {
        bool Exists(string transactionId);

        bool Add(string transactionId);
    }
}
