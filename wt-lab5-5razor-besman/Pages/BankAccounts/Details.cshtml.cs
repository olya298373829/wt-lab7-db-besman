using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wt_lab5_5razor_besman.Data;
using wt_lab5_5razor_besman.Models;

namespace wt_lab5_5razor_besman.Pages.BankAccounts;

public class DetailsModel : PageModel
{
    public BankAccount? Account { get; set; }

    public IActionResult OnGet(int id)
    {
        Account = BankAccountStore.Accounts.FirstOrDefault(x => x.Id == id);
        if (Account == null) return NotFound();
        return Page();
    }
}