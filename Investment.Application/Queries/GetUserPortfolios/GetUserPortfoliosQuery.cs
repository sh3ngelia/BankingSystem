using Investment.Application.Abstractions;
using Investment.Application.DTOs;

namespace Investment.Application.Queries.GetUserPortfolios;

public record GetUserPortfoliosQuery(Guid OwnerId) : IQuery<IEnumerable<PortfolioDto>>;