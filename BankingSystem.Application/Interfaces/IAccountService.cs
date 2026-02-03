using BankingSystem.Application.DTOs.Account;
using BankingSystem.Application.DTOs.Transaction;

namespace BankingSystem.Application.Interfaces;
public interface IAccountService
{
    Task<AccountResponseDto> CreateAccountAsync(CreateAccountDto dto);
    Task DepositAsync(DepositDto dto);
    Task WithdrawAsync(WithdrawDto dto);
    Task TransferAsync(TransferDto dto);
}