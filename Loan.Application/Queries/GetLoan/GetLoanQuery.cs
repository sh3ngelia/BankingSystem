using Loan.Application.Abstractions;
using Loan.Application.DTOs;

namespace Loan.Application.Queries.GetLoan;

public record GetLoanQuery(Guid LoanId) : IQuery<LoanDto?>;