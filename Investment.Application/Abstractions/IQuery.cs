using MediatR;

namespace Investment.Application.Abstractions;

public interface IQuery<TResponse> : IRequest<TResponse> { }
