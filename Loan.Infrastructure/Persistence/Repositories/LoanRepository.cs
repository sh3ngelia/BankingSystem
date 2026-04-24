using Loan.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Loan.Infrastructure.Persistence.Repositories;

public class LoanRepository : ILoanRepository
{
    private readonly LoanDbContext _context;

    public LoanRepository(LoanDbContext context)
    {
        _context = context;
    }

    public async Task<Loan.Domain.Entities.Loan?> GetByIdAsync(
        Guid id, CancellationToken ct = default)
    {
        return await _context.Loans
            .Include(l => l.RepaymentSchedule)
                .ThenInclude(r => r.Installments)
            .FirstOrDefaultAsync(l => l.Id == id, ct);
    }

    public async Task<IEnumerable<Loan.Domain.Entities.Loan>> GetByApplicantIdAsync(
        Guid applicantId, CancellationToken ct = default)
    {
        return await _context.Loans
            .Where(l => l.ApplicantId == applicantId)
            .OrderByDescending(l => l.CreatedAt)
            .ToListAsync(ct);
    }

    public async Task AddAsync(
        Loan.Domain.Entities.Loan loan, CancellationToken ct = default)
    {
        await _context.Loans.AddAsync(loan, ct);
    }

    public async Task UpdateAsync(
        Loan.Domain.Entities.Loan loan, CancellationToken ct = default)
    {
        _context.Loans.Update(loan);
        await Task.CompletedTask;
    }
}