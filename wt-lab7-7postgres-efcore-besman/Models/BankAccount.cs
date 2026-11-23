namespace wt_lab7_7postgres_efcore_besman.Models;

public class BankAccount
{
    public int Id { get; set; }
    public string AccountNumber { get; set; } = "";
    public string OwnerName { get; set; } = "";
    public decimal Balance { get; set; }
    public bool IsBlocked { get; set; }
    
    // Новое свойство 
    public DateTime CreatedAt { get; set; } 
}