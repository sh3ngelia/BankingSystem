using BankingSystem.Domain.Enums;

namespace BankingSystem.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;


    public UserRole Role { get; set; }


    public ICollection<Account> Accounts { get; set; } = new List<Account>();
}