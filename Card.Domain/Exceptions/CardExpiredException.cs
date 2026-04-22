namespace Card.Domain.Exceptions;

public class CardExpiredException : Exception
{
    public CardExpiredException(Guid cardId)
        : base($"Card '{cardId}' is expired.") { }
}