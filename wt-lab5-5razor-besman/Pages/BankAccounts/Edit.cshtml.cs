using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wt_lab5_5razor_besman.Data;
using wt_lab5_5razor_besman.Models;

namespace wt_lab5_5razor_besman.Pages.BankAccounts;

public class EditModel : PageModel
{
    [BindProperty]
    public BankAccount Account { get; set; } = new();

    // GET: показываем форму с текущими данными
    public IActionResult OnGet(int id)
    {
        var existing = BankAccountStore.Accounts.FirstOrDefault(x => x.Id == id);
        if (existing == null) return NotFound();

        Account = existing;
        return Page();
    }

    // POST: сохраняем изменения
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        var existing = BankAccountStore.Accounts.FirstOrDefault(x => x.Id == Account.Id);
        if (existing == null) return NotFound();

        // Обновляем поля
        existing.Owner = Account.Owner;
        existing.AccountNumber = Account.AccountNumber;
        existing.Balance = Account.Balance;
        existing.Currency = Account.Currency;
        existing.IsActive = Account.IsActive;

        // Возвращаемся к списку
        return RedirectToPage("./Index");
    }
}