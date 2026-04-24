using Investment.Application.Abstractions;

namespace Investment.Application.Commands.RenamePortfolio;

public record RenamePortfolioCommand(
    Guid PortfolioId,
    string NewName) : ICommand;