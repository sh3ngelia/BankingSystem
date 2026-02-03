using BankingSystem.Infrastructure.Data;

namespace BankingSystem.Infrastructure;
public static class InfrastructureExtensions
{
    public static async Task ExecuteTransactionAsync(this BankingDbContext context, Func<Task> action)
    {
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            await action();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}