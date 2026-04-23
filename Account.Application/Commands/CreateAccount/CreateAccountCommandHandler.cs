using Account.Application.Abstractions;
using AccountEntity = Account.Domain.Entities.Account;
using BankingSystem.Shared.Results;

namespace Account.Application.Commands.CreateAccount;

public class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand, Guid>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAccountCommandHandler(
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateAccountCommand command, CancellationToken ct)
    {
        var account = AccountEntity.Create(
            command.OwnerId,
            command.Type,
            command.Currency);

        await _accountRepository.AddAsync(account, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return Result<Guid>.Success(account.Id);
    }
}