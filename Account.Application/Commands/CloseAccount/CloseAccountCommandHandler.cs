using Account.Application.Abstractions;
using BankingSystem.Shared.Results;

namespace Account.Application.Commands.CloseAccount;

public class CloseAccountCommandHandler : ICommandHandler<CloseAccountCommand>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CloseAccountCommandHandler(
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CloseAccountCommand command, CancellationToken ct)
    {
        var account = await _accountRepository.GetByIdAsync(command.AccountId, ct);
        if (account is null)
            return Result.Failure("Account not found.");

        account.Close();

        await _accountRepository.UpdateAsync(account, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}