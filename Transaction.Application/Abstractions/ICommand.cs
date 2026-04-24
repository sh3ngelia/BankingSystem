using BankingSystem.Shared.Results;
using MediatR;

namespace Transaction.Application.Abstractions;

public interface ICommand : IRequest<Result> { }

public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }