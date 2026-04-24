using Investment.Application.Abstractions;
using Investment.Application.DTOs;

namespace Investment.Application.Queries.GetPortfolio;

public record GetPortfolioQuery(Guid PortfolioId) : IQuery<PortfolioDto?>;