using Account.Application.Abstractions;

namespace Account.Application.Commands.DepositMoney;

public record DepositMoneyCommand(
    Guid AccountId,
    decimal Amount,
    string Currency) : ICommand;