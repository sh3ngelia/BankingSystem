using Investment.Application.Abstractions;
using Investment.Application.DTOs;

namespace Investment.Application.Queries.GetUserPortfolios;

public class GetUserPortfoliosQueryHandler : IQueryHandler<GetUserPortfoliosQuery, IEnumerable<PortfolioDto>>
{
    private readonly IPortfolioRepository _portfolioRepository;

    public GetUserPortfoliosQueryHandler(IPortfolioRepository portfolioRepository)
    {
        _portfolioRepository = portfolioRepository;
    }

    public async Task<IEnumerable<PortfolioDto>> Handle(GetUserPortfoliosQuery query, CancellationToken ct)
    {
        var portfolios = await _portfolioRepository.GetByOwnerIdAsync(query.OwnerId, ct);

        return portfolios.Select(p => new PortfolioDto(
            p.Id,
            p.OwnerId,
            p.Name,
            p.TotalInvested,
            0,
            0,
            0,
            p.CreatedAt,
            Enumerable.Empty<PositionDto>()));
    }
}