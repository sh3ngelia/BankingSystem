using Card.Application.Abstractions;

namespace Card.Application.Commands.ProcessCardTransaction;

public record ProcessCardTransactionCommand(
    Guid CardId,
    decimal Amount,
    string Currency,
    string MerchantName,
    string MerchantCategory) : ICommand<Guid>;