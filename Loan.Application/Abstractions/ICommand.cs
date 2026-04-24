using BankingSystem.Shared.Results;
using MediatR;

namespace Loan.Application.Abstractions;

public interface ICommand : IRequest<Result> { }

public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }