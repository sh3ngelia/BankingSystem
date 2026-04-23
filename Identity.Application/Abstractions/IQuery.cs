using MediatR;

namespace Identity.Application.Abstractions;

public interface IQuery<TResponse> : IRequest<TResponse> { }