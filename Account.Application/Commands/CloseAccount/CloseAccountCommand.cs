using Account.Application.Abstractions;

namespace Account.Application.Commands.CloseAccount;

public record CloseAccountCommand(Guid AccountId) : ICommand;