using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionProcessor.Application.DTOs;
using TransactionProcessor.Domain.Entities;

namespace TransactionProcessor.Application.Interfaces
{
    public interface ITransactionProcessor
    {
        Task<ProcessResult> ProcessAsync(IEnumerable<Transaction> transactions);
    }
}
