using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TransactionProcessor.Domain.Entities;


namespace TransactionProcessor.Application.Interfaces
{
    public interface IAccountRepository
    {
        Account? Get(string accountId);

        IEnumerable<Account> GetAll();

        Account Create(string accountId);

        void Update(Account account);
    }
}
