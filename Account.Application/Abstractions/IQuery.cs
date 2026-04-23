using MediatR;

namespace Account.Application.Abstractions;
public interface IQuery<TResponse> : IRequest<TResponse> { }
