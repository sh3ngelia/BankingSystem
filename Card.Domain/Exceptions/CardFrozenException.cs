namespace Card.Domain.Exceptions;

public class CardFrozenException : Exception
{
    public CardFrozenException(Guid cardId)
        : base($"Card '{cardId}' is frozen.") { }
}