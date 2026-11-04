using Microsoft.AspNetCore.Mvc.RazorPages;
using wt_lab5_5razor_besman.Data;       // Подключение хранилища
using wt_lab5_5razor_besman.Models;     // Подключение модели

namespace wt_lab5_5razor_besman.Pages.BankAccounts;

public class IndexModel : PageModel
{
    public List<BankAccount> Accounts { get; set; } = new();

    public void OnGet()
    {
        Accounts = BankAccountStore.Accounts; // Берем данные из хранилища
    }
}