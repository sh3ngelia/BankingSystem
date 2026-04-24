using MediatR;

namespace Loan.Application.Abstractions;
public interface IQuery<TResponse> : IRequest<TResponse> { }