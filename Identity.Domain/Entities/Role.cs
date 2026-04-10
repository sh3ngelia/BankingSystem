using BankingSystem.Shared.BaseTypes;

namespace Identity.Domain.Entities;

public class Role : Entity
{
    public string Name { get; private set; }

    private Role() { }

    public static Role Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Role name cannot be empty.");

        return new Role { Name = name };
    }
}