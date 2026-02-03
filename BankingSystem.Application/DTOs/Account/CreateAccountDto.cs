namespace BankingSystem.Application.DTOs.Account;
public class CreateAccountDto
{
    public Guid UserId { get; set; }
    public string Currency { get; set; } = "USD";
}