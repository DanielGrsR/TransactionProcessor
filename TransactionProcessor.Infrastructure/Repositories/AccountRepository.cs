using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionProcessor.Application.Interfaces;
using TransactionProcessor.Domain.Entities;

namespace TransactionProcessor.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ConcurrentDictionary<string, Account> _accounts = new();

        public Account? Get(string accountId)
        {
            _accounts.TryGetValue(accountId, out var account);

            return account;
        }

        public IEnumerable<Account> GetAll()
        {
            return _accounts.Values;
        }

        public Account Create(string accountId)
        {
            return _accounts.GetOrAdd(accountId,
                id => new Account(id));
        }

        public void Update(Account account)
        {
            _accounts[account.AccountId] = account;
        }
    }
}
