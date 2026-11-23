using Microsoft.EntityFrameworkCore;
using wt_lab7_7postgres_efcore_besman.Models;

namespace wt_lab7_7postgres_efcore_besman.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<BankAccount> BankAccounts => Set<BankAccount>();

    // Метод для настройки модели и добавления начальных данных
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BankAccount>().HasData(
            new BankAccount
            {
                Id = 1,
                AccountNumber = "1234567890",
                OwnerName = "Иван Иванов",
                Balance = 2500,
                IsBlocked = false,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new BankAccount
            {
                Id = 2,
                AccountNumber = "0987654321",
                OwnerName = "Петр Петров",
                Balance = 80,
                IsBlocked = true,
                CreatedAt = new DateTime(2026, 1, 2, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}