using BankingSystem.Shared.Results;
using Investment.Application.Abstractions;

namespace Investment.Application.Commands.RenamePortfolio;

public class RenamePortfolioCommandHandler : ICommandHandler<RenamePortfolioCommand>
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RenamePortfolioCommandHandler(
        IPortfolioRepository portfolioRepository,
        IUnitOfWork unitOfWork)
    {
        _portfolioRepository = portfolioRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RenamePortfolioCommand command, CancellationToken ct)
    {
        var portfolio = await _portfolioRepository.GetByIdAsync(command.PortfolioId, ct);
        if (portfolio is null)
            return Result.Failure("Portfolio not found.");

        portfolio.Rename(command.NewName);

        await _portfolioRepository.UpdateAsync(portfolio, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}