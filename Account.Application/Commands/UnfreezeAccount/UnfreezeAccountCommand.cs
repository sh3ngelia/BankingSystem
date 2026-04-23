using Account.Application.Abstractions;

namespace Account.Application.Commands.UnfreezeAccount;

public record UnfreezeAccountCommand(Guid AccountId) : ICommand;