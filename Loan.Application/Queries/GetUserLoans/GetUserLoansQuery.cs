using Loan.Application.Abstractions;
using Loan.Application.DTOs;

namespace Loan.Application.Queries.GetUserLoans;

public record GetUserLoansQuery(Guid ApplicantId) : IQuery<IEnumerable<LoanDto>>;