using MediatR;

namespace Card.Application.Abstractions; 
public interface IQuery<TResponse> : IRequest<TResponse> { }
