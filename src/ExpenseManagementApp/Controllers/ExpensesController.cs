using Microsoft.AspNetCore.Mvc;
using ExpenseManagementApp.Models;

namespace ExpenseManagementApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    // GET: api/expenses
    [HttpGet]
    public ActionResult<IEnumerable<Expense>> GetExpenses([FromQuery] string? filter = null)
    {
        var expenses = GetDummyExpenses();
        
        if (!string.IsNullOrWhiteSpace(filter))
        {
            expenses = expenses.Where(e => 
                e.Category.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                e.Description.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                e.Amount.ToString().Contains(filter)
            ).ToList();
        }
        
        return Ok(expenses);
    }

    // GET: api/expenses/{id}
    [HttpGet("{id}")]
    public ActionResult<Expense> GetExpense(int id)
    {
        var expense = GetDummyExpenses().FirstOrDefault(e => e.Id == id);
        
        if (expense == null)
        {
            return NotFound();
        }
        
        return Ok(expense);
    }

    // GET: api/expenses/pending
    [HttpGet("pending")]
    public ActionResult<IEnumerable<Expense>> GetPendingExpenses([FromQuery] string? filter = null)
    {
        var expenses = GetDummyExpenses()
            .Where(e => e.Status == ExpenseStatus.Submitted)
            .ToList();
        
        if (!string.IsNullOrWhiteSpace(filter))
        {
            expenses = expenses.Where(e => 
                e.Category.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                e.Description.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                e.Amount.ToString().Contains(filter)
            ).ToList();
        }
        
        return Ok(expenses);
    }

    // POST: api/expenses
    [HttpPost]
    public ActionResult<Expense> CreateExpense(Expense expense)
    {
        // In a real app, this would save to database
        expense.Id = new Random().Next(1000, 9999);
        expense.Status = ExpenseStatus.Submitted;
        
        return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
    }

    // PUT: api/expenses/{id}/approve
    [HttpPut("{id}/approve")]
    public ActionResult ApproveExpense(int id)
    {
        var expense = GetDummyExpenses().FirstOrDefault(e => e.Id == id);
        
        if (expense == null)
        {
            return NotFound();
        }
        
        // In a real app, this would update the database
        expense.Status = ExpenseStatus.Approved;
        
        return Ok(expense);
    }

    // PUT: api/expenses/{id}
    [HttpPut("{id}")]
    public ActionResult UpdateExpense(int id, Expense expense)
    {
        if (id != expense.Id)
        {
            return BadRequest();
        }
        
        // In a real app, this would update the database
        return Ok(expense);
    }

    // DELETE: api/expenses/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteExpense(int id)
    {
        var expense = GetDummyExpenses().FirstOrDefault(e => e.Id == id);
        
        if (expense == null)
        {
            return NotFound();
        }
        
        // In a real app, this would delete from database
        return NoContent();
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
            },
            new Expense
            {
                Id = 5,
                Date = new DateTime(2024, 1, 20),
                Category = "Travel",
                Amount = 120.00m,
                Description = "Hotel accommodation",
                Status = ExpenseStatus.Submitted
            },
            new Expense
            {
                Id = 6,
                Date = new DateTime(2023, 12, 14),
                Category = "Office Supplies",
                Amount = 99.50m,
                Description = "Desk accessories",
                Status = ExpenseStatus.Submitted
            }
        };
    }
}
