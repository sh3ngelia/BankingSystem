using BankingSystem.Application.DTOs.Transaction;
using FluentValidation;

namespace BankingSystem.Application.Validators; 
public class WithdrawDtoValidator : AbstractValidator<WithdrawDto>
{
    public WithdrawDtoValidator()
    {
        RuleFor(x => x.Amount).GreaterThan(0);
    }
}