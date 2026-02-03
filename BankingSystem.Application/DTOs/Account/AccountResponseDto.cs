namespace BankingSystem.Application.DTOs.Account;
public class AccountResponseDto
{
    public Guid Id { get; set; }
    public string AccountNumber { get; set; } = null!;
    public decimal Balance { get; set; }
    public string Currency { get; set; } = null!;
}