using wt_lab5_5razor_besman.Models;

namespace wt_lab5_5razor_besman.Data;

public static class BankAccountStore
{
    public static List<BankAccount> Accounts { get; } = new()
    {
        new BankAccount { Id = 1, Owner = "Иванов И.И.", AccountNumber = "BY01 1111 2222 3333", Balance = 1500.50m, Currency = "BYN", IsActive = true },
        new BankAccount { Id = 2, Owner = "Петров П.П.", AccountNumber = "BY02 4444 5555 6666", Balance = 320.00m, Currency = "BYN", IsActive = true },
        new BankAccount { Id = 3, Owner = "Сидорова А.А.", AccountNumber = "BY03 7777 8888 9999", Balance = 0.00m, Currency = "BYN", IsActive = false },
        new BankAccount { Id = 4, Owner = "Бесман О.С.", AccountNumber = "BY04 1234 5678 9012", Balance = 1000.00m, Currency = "BYN", IsActive = true },
        new BankAccount { Id = 5, Owner = "Смирнов С.С.", AccountNumber = "BY05 9876 5432 1098", Balance = 250m, Currency = "EUR", IsActive = true }
    };
}