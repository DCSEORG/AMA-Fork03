using Microsoft.AspNetCore.Mvc.RazorPages;
using ExpenseManagementApp.Models;

namespace ExpenseManagementApp.Pages;

public class ApproveModel : PageModel
{
    public List<Expense> PendingExpenses { get; set; } = new List<Expense>();
    public string FilterText { get; set; } = string.Empty;

    public void OnGet(string? filter)
    {
        FilterText = filter ?? string.Empty;
        PendingExpenses = GetDummyPendingExpenses();

        if (!string.IsNullOrWhiteSpace(FilterText))
        {
            PendingExpenses = PendingExpenses.Where(e =>
                e.Category.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                e.Description.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                e.Amount.ToString().Contains(FilterText)
            ).ToList();
        }
    }

    private static List<Expense> GetDummyPendingExpenses()
    {
        return new List<Expense>
        {
            new Expense
            {
                Id = 1,
                Date = new DateTime(2024, 1, 20),
                Category = "Travel",
                Amount = 120.00m,
                Description = "Hotel accommodation",
                Status = ExpenseStatus.Submitted
            },
            new Expense
            {
                Id = 2,
                Date = new DateTime(2023, 12, 14),
                Category = "Office Supplies",
                Amount = 99.50m,
                Description = "Desk accessories",
                Status = ExpenseStatus.Submitted
            }
        };
    }
}
