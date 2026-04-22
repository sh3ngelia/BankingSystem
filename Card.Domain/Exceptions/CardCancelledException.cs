namespace Card.Domain.Exceptions;

public class CardCancelledException : Exception
{
    public CardCancelledException(Guid cardId)
        : base($"Card '{cardId}' has been cancelled.") { }
}