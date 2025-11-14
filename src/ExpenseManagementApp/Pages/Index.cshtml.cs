using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ExpenseManagementApp.Models;

namespace ExpenseManagementApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    [BindProperty]
    public Expense NewExpense { get; set; } = new Expense();

    public bool SubmitSuccess { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        SubmitSuccess = false;
    }

    public IActionResult OnPost()
    {
        // In a real application, this would save to database
        _logger.LogInformation("Expense submitted: {Category} - {Amount}", 
            NewExpense.Category, NewExpense.Amount);
        
        SubmitSuccess = true;
        ModelState.Clear();
        NewExpense = new Expense();
        
        return Page();
    }
}
