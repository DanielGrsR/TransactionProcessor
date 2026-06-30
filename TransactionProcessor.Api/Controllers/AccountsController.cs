using Microsoft.AspNetCore.Mvc;
using TransactionProcessor.Application.Interfaces;

namespace TransactionProcessor.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountRepository _accountRepository;

    public AccountsController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    [HttpGet]
    public IActionResult GetAccounts()
    {
        return Ok(_accountRepository.GetAll());
    }


    [HttpGet("{accountId}")]
    public IActionResult GetAccount(string accountId)
    {
        var account = _accountRepository.Get(accountId);

        if (account == null)
            return NotFound();

        return Ok(account);
    }
}