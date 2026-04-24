using Loan.Application.Abstractions;
using Loan.Application.DTOs;

namespace Loan.Application.Queries.GetUserLoans;

public class GetUserLoansQueryHandler : IQueryHandler<GetUserLoansQuery, IEnumerable<LoanDto>>
{
    private readonly ILoanRepository _loanRepository;

    public GetUserLoansQueryHandler(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public async Task<IEnumerable<LoanDto>> Handle(GetUserLoansQuery query, CancellationToken ct)
    {
        var loans = await _loanRepository.GetByApplicantIdAsync(query.ApplicantId, ct);

        return loans.Select(loan => new LoanDto(
            loan.Id,
            loan.ApplicantId,
            loan.Amount,
            loan.InterestRate.Value,
            loan.TermMonths,
            loan.Type.ToString(),
            loan.Status.ToString(),
            loan.CreditScore?.Value,
            loan.RejectionReason,
            loan.CreatedAt,
            loan.ApprovedAt,
            null));
    }
}