using Account.Application.Abstractions;

namespace Account.Application.Commands.FreezeAccount;

public record FreezeAccountCommand(Guid AccountId) : ICommand;