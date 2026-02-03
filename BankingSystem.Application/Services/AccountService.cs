using System.Transactions;
using BankingSystem.Application.DTOs.Account;
using BankingSystem.Application.DTOs.Transaction;
using BankingSystem.Application.Interfaces;
using BankingSystem.Domain.Entities;
using BankingSystem.Domain.Enums;
using BankingSystem.Domain.Interfaces;
using Transaction = BankingSystem.Domain.Entities.Transaction;
using TransactionStatus = BankingSystem.Domain.Enums.TransactionStatus;


namespace BankingSystem.Application.Services;


public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;


    public AccountService(IAccountRepository accountRepository,
        ITransactionRepository transactionRepository)
    {
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task<AccountResponseDto> CreateAccountAsync(CreateAccountDto dto)
    {
        var account = new Account
        {
            Id = Guid.NewGuid(),
            AccountNumber = Guid.NewGuid().ToString("N")[..10],
            Currency = dto.Currency,
            UserId = dto.UserId
        };


        await _accountRepository.AddAsync(account);


        return new AccountResponseDto
        {
            Id = account.Id,
            AccountNumber = account.AccountNumber,
            Balance = account.Balance,
            Currency = account.Currency
        };
    }

    public async Task DepositAsync(DepositDto dto)
    {
        var account = await _accountRepository.GetByIdAsync(dto.AccountId)
                      ?? throw new Exception("Account not found");


        account.Deposit(dto.Amount);


        await _transactionRepository.AddAsync(new Transaction
        {
            AccountId = account.Id,
            Amount = dto.Amount,
            Type = TransactionType.Deposit,
            Status = TransactionStatus.Completed
        });


        await _accountRepository.UpdateAsync(account);
    }
    public async Task WithdrawAsync(WithdrawDto dto)
    {
        var account = await _accountRepository.GetByIdAsync(dto.AccountId)
                      ?? throw new Exception("Account not found");


        account.Withdraw(dto.Amount);


        await _transactionRepository.AddAsync(new Transaction
        {
            AccountId = account.Id,
            Amount = dto.Amount,
            Type = TransactionType.Withdraw,
            Status = TransactionStatus.Completed
        });


        await _accountRepository.UpdateAsync(account);
    }

    public async Task TransferAsync(TransferDto dto)
    {
        if (dto.FromAccountId == dto.ToAccountId)
            throw new Exception("Cannot transfer to same account");


        var from = await _accountRepository.GetByIdAsync(dto.FromAccountId)
                   ?? throw new Exception("Source account not found");


        var to = await _accountRepository.GetByIdAsync(dto.ToAccountId)
                 ?? throw new Exception("Target account not found");


        from.Withdraw(dto.Amount);
        to.Deposit(dto.Amount);


        await _transactionRepository.AddAsync(new Transaction
        {
            AccountId = from.Id,
            Amount = dto.Amount,
            Type = TransactionType.Transfer,
            Status = TransactionStatus.Completed
        });


        await _transactionRepository.AddAsync(new Transaction
        {
            AccountId = to.Id,
            Amount = dto.Amount,
            Type = TransactionType.Transfer,
            Status = TransactionStatus.Completed
        });

        await _accountRepository.UpdateAsync(from);
        await _accountRepository.UpdateAsync(to);
    }
}