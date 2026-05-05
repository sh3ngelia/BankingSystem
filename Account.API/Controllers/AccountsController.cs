using Account.Application.Commands.CloseAccount;
using Account.Application.Commands.CreateAccount;
using Account.Application.Commands.DepositMoney;
using Account.Application.Commands.FreezeAccount;
using Account.Application.Commands.UnfreezeAccount;
using Account.Application.Commands.WithdrawMoney;
using Account.Application.Queries.GetAccount;
using Account.Application.Queries.GetAllAccounts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Account.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AccountsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount(
        [FromBody] CreateAccountCommand command,
        CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);
        if (result.IsFailure) return BadRequest(result.Error);
        return CreatedAtAction(nameof(GetAccount), new { id = result.Value }, result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAccount(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAccountQuery(id), ct);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpGet("owner/{ownerId:guid}")]
    public async Task<IActionResult> GetAllAccounts(Guid ownerId, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllAccountsQuery(ownerId), ct);
        return Ok(result);
    }

    [HttpPost("{id:guid}/deposit")]
    public async Task<IActionResult> Deposit(
        Guid id,
        [FromBody] DepositMoneyCommand command,
        CancellationToken ct)
    {
        var result = await _mediator.Send(command with { AccountId = id }, ct);
        if (result.IsFailure) return BadRequest(result.Error);
        return NoContent();
    }

    [HttpPost("{id:guid}/withdraw")]
    public async Task<IActionResult> Withdraw(
        Guid id,
        [FromBody] WithdrawMoneyCommand command,
        CancellationToken ct)
    {
        var result = await _mediator.Send(command with { AccountId = id }, ct);
        if (result.IsFailure) return BadRequest(result.Error);
        return NoContent();
    }

    [HttpPost("{id:guid}/freeze")]
    public async Task<IActionResult> Freeze(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new FreezeAccountCommand(id), ct);
        if (result.IsFailure) return BadRequest(result.Error);
        return NoContent();
    }

    [HttpPost("{id:guid}/unfreeze")]
    public async Task<IActionResult> Unfreeze(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new UnfreezeAccountCommand(id), ct);
        if (result.IsFailure) return BadRequest(result.Error);
        return NoContent();
    }

    [HttpPost("{id:guid}/close")]
    public async Task<IActionResult> Close(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new CloseAccountCommand(id), ct);
        if (result.IsFailure) return BadRequest(result.Error);
        return NoContent();
    }
}