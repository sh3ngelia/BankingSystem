namespace BankingSystem.Domain.Entities;

public class Account
{
    public Guid Id { get; set; }
    public string AccountNumber { get; set; } = null!;
    public decimal Balance { get; private set; }
    public string Currency { get; set; } = "USD";


    public Guid UserId { get; set; }
    public User User { get; set; } = null!;


    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();


    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero");


        Balance += amount;
    }


    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero");


        if (Balance < amount)
            throw new InvalidOperationException("Insufficient funds");


        Balance -= amount;
    }
}