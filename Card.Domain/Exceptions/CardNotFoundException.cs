namespace Card.Domain.Exceptions;

public class CardNotFoundException : Exception
{
    public CardNotFoundException(Guid cardId)
        : base($"Card with id '{cardId}' was not found.") { }
}