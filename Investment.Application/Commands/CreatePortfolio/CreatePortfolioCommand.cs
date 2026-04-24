using Investment.Application.Abstractions;

namespace Investment.Application.Commands.CreatePortfolio;

public record CreatePortfolioCommand(
    Guid OwnerId,
    string Name) : ICommand<Guid>;