using System.ComponentModel.DataAnnotations;

namespace wt_lab5_5razor_besman.Models;

public class BankAccount
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Укажите владельца")]
    [Display(Name = "Владелец")]
    public string Owner { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите номер счёта")]
    [Display(Name = "Номер счёта")]
    public string AccountNumber { get; set; } = string.Empty;

    [Range(0, 1_000_000, ErrorMessage = "Некорректный баланс")]
    [Display(Name = "Баланс")]
    public decimal Balance { get; set; }

    [Display(Name = "Валюта")]
    public string Currency { get; set; } = "BYN";

    [Display(Name = "Активен")]
    public bool IsActive { get; set; }
}