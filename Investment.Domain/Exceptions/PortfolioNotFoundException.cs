namespace Investment.Domain.Exceptions;

public class PortfolioNotFoundException : Exception
{
    public PortfolioNotFoundException(Guid portfolioId)
        : base($"Portfolio with id '{portfolioId}' was not found.") { }
}