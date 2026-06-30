using Microsoft.AspNetCore.Mvc;
using TransactionProcessor.Application.Interfaces;
using TransactionProcessor.Domain.Entities;

namespace TransactionProcessor.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionProcessor _transactionProcessor;

    public TransactionsController(ITransactionProcessor transactionProcessor)
    {
        _transactionProcessor = transactionProcessor;
    }

    [HttpPost("process")]
    public async Task<IActionResult> ProcessTransactions(
        [FromBody] List<Transaction> transactions)
    {
        if (transactions == null || !transactions.Any())
        {
            return BadRequest("La transaccion esta vacia.");
        }

        var result = await _transactionProcessor.ProcessAsync(transactions);

        return Ok(result);
    }
}