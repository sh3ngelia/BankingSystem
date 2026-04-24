using LoanEntity = Loan.Domain.Entities.Loan;

namespace Loan.Application.Abstractions;

public interface ILoanRepository
{
    Task<LoanEntity?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<LoanEntity>> GetByApplicantIdAsync(Guid applicantId, CancellationToken ct = default);
    Task AddAsync(LoanEntity loan, CancellationToken ct = default);
    Task UpdateAsync(LoanEntity loan, CancellationToken ct = default);
}