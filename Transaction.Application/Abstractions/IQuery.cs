using MediatR;

namespace Transaction.Application.Abstractions;

public interface IQuery<TResponse> : IRequest<TResponse> { }