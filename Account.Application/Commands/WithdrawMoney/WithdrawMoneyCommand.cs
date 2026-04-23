using Account.Application.Abstractions;

namespace Account.Application.Commands.WithdrawMoney;

public record WithdrawMoneyCommand(
    Guid AccountId,
    decimal Amount,
    string Currency) : ICommand;