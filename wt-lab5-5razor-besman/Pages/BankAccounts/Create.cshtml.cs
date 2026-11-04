using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wt_lab5_5razor_besman.Data;
using wt_lab5_5razor_besman.Models;

namespace wt_lab5_5razor_besman.Pages.BankAccounts;

public class CreateModel : PageModel
{
    // [BindProperty] связывает поля формы с этим свойством
    [BindProperty]
    public BankAccount Account { get; set; } = new();

    public void OnGet()
    {
        // Пустой метод для показа формы
    }

    public IActionResult OnPost()
    {
        // Если данные из формы не прошли валидацию, возвращаем ту же страницу
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Присваиваем новый ID (максимальный + 1)
        Account.Id = BankAccountStore.Accounts.Any() 
            ? BankAccountStore.Accounts.Max(x => x.Id) + 1 
            : 1;

        // Добавляем в общее хранилище
        BankAccountStore.Accounts.Add(Account);

        // Перенаправляем на список счетов
        return RedirectToPage("./Index");
    }
}