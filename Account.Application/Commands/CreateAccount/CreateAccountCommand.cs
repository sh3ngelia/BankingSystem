using Account.Application.Abstractions;
using Account.Domain.Enums;
using BankingSystem.Shared.Enums;

namespace Account.Application.Commands.CreateAccount;

public record CreateAccountCommand(
    Guid OwnerId,
    AccountType Type,
    Currency Currency) : ICommand<Guid>;