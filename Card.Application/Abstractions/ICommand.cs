using BankingSystem.Shared.Results;
using MediatR;

namespace Card.Application.Abstractions;

public interface ICommand : IRequest<Result> { }

public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }
