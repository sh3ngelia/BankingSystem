using BankingSystem.Application.DTOs.Transaction;
using FluentValidation;

namespace BankingSystem.Application.Validators;
public class DepositDtoValidator : AbstractValidator<DepositDto>
{
    public DepositDtoValidator()
    {
        RuleFor(x => x.Amount).GreaterThan(0);
    }
}