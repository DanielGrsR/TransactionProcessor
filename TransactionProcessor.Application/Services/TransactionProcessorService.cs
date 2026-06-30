using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionProcessor.Application.DTOs;
using TransactionProcessor.Application.Interfaces;
using TransactionProcessor.Domain.Entities;
using TransactionProcessor.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace TransactionProcessor.Application.Services
{
    public class TransactionProcessorService : ITransactionProcessor
    {
        private readonly IAccountRepository _accountRepository;

        private readonly ITransactionRepository _transactionRepository;

        private readonly ILogger<TransactionProcessorService> _logger;

        public TransactionProcessorService(
            IAccountRepository accountRepository,
            ITransactionRepository transactionRepository,
            ILogger<TransactionProcessorService> logger)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _logger = logger;
        }

        public Task<ProcessResult> ProcessAsync(IEnumerable<Transaction> transactions)
        {
            var result = new ProcessResult();

            foreach (var transaction in transactions.OrderBy(t => t.Timestamp))
            {
                ProcessTransaction(transaction, result);
            }

            return Task.FromResult(result);
        }

        private void ProcessTransaction(Transaction transaction, ProcessResult result)
        {
            // 1. Validar duplicados
            if (_transactionRepository.Exists(transaction.TransactionId))
            {
                _logger.LogInformation("Transacción duplicada {TransactionId}", transaction.TransactionId);

                result.Duplicates++;
                result.Messages.Add($"Transacción duplicada: {transaction.TransactionId}");
                return;
            }

            // 2. Obtener cuenta
            var account = _accountRepository.Get(transaction.AccountId);

            if (account == null)
            {
                result.Rejected++;

                result.Messages.Add(
                    $"Transacción rechazada: {transaction.TransactionId} - No se encontro la cuenta");
                return;
            }

            // 3. Procesar operación
            switch (transaction.OperationType)
            {
                case OperationType.CREDIT:

                    account.Credit(transaction.Amount);

                    _accountRepository.Update(account);

                    _transactionRepository.Add(transaction.TransactionId);

                    result.Processed++;

                    result.Messages.Add(
                        $"Crédito aplicado correctamente: {transaction.TransactionId}");

                    break;

                case OperationType.DEBIT:

                    if (!account.Debit(transaction.Amount))
                    {
                        _logger.LogWarning(
                            "Transacción {TransactionId} rechazada por fondos insuficientes",
                            transaction.TransactionId);

                        result.Rejected++;

                        result.Messages.Add(
                            $"Transacción rechazada: {transaction.TransactionId} - Fondos insuficientes");

                        return;
                    }

                    _accountRepository.Update(account);

                    _transactionRepository.Add(transaction.TransactionId);

                    result.Processed++;

                    result.Messages.Add(
                        $"Débito aplicado correctamente: {transaction.TransactionId}");

                    break;

                default:

                    throw new InvalidOperationException(
                        $"Tipo de operación no soportada: {transaction.OperationType}");
            }
        }
    }
}
