using Microsoft.AspNetCore.Mvc.RazorPages;
using ExpenseManagementApp.Models;

namespace ExpenseManagementApp.Pages;

public class ExpensesModel : PageModel
{
    public List<Expense> Expenses { get; set; } = new List<Expense>();
    public string FilterText { get; set; } = string.Empty;

    public void OnGet(string? filter)
    {
        FilterText = filter ?? string.Empty;
        Expenses = GetDummyExpenses();

        if (!string.IsNullOrWhiteSpace(FilterText))
        {
            Expenses = Expenses.Where(e =>
                e.Category.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                e.Description.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                e.Amount.ToString().Contains(FilterText)
            ).ToList();
        }
    }

    private static List<Expense> GetDummyExpenses()
    {
        return new List<Expense>
        {
            new Expense
            {
                Id = 1,
                Date = new DateTime(2024, 1, 15),
                Category = "Travel",
                Amount = 120.00m,
                Description = "Train tickets to London",
                Status = ExpenseStatus.Submitted
            },
            new Expense
            {
                Id = 2,
                Date = new DateTime(2023, 1, 10),
                Category = "Food",
                Amount = 69.00m,
                Description = "Team lunch",
                Status = ExpenseStatus.Submitted
            },
            new Expense
            {
                Id = 3,
                Date = new DateTime(2023, 12, 4),
                Category = "Office Supplies",
                Amount = 99.50m,
                Description = "Printer paper and toner",
                Status = ExpenseStatus.Approved
            },
            new Expense
            {
                Id = 4,
                Date = new DateTime(2023, 11, 18),
                Category = "Transport",
                Amount = 19.20m,
                Description = "Taxi to client meeting",
                Status = ExpenseStatus.Approved
            }
        };
    }
}
