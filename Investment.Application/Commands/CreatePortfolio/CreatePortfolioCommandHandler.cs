using BankingSystem.Shared.Results;
using Investment.Application.Abstractions;
using Investment.Domain.Entities;
using Investment.Domain.Events;

namespace Investment.Application.Commands.CreatePortfolio;

public class CreatePortfolioCommandHandler : ICommandHandler<CreatePortfolioCommand, Guid>
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IEventBus _eventBus;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePortfolioCommandHandler(
        IPortfolioRepository portfolioRepository,
        IEventBus eventBus,
        IUnitOfWork unitOfWork)
    {
        _portfolioRepository = portfolioRepository;
        _eventBus = eventBus;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreatePortfolioCommand command, CancellationToken ct)
    {
        var portfolio = Portfolio.Create(command.OwnerId, command.Name);

        await _portfolioRepository.AddAsync(portfolio, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        await _eventBus.PublishAsync(new PortfolioCreatedEvent(
            portfolio.Id,
            portfolio.OwnerId,
            portfolio.Name,
            DateTime.UtcNow), ct);

        return Result<Guid>.Success(portfolio.Id);
    }
}