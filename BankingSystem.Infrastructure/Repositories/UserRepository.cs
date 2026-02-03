using BankingSystem.Domain.Entities;
using BankingSystem.Domain.Interfaces;
using BankingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;    

namespace BankingSystem.Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly BankingDbContext _context;
    public UserRepository(BankingDbContext context) => _context = context;


    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }


    public async Task<User?> GetByEmailAsync(string email) =>
        await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
}