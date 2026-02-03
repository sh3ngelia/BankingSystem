using BankingSystem.Application.DTOs.Transaction;
using FluentValidation;

namespace BankingSystem.Application.Validators;
public class TransferDtoValidator : AbstractValidator<TransferDto>
{
    public TransferDtoValidator()
    {
        RuleFor(x => x.Amount).GreaterThan(0);
        RuleFor(x => x.FromAccountId).NotEqual(x => x.ToAccountId);
    }
}