namespace ExpenseManagementApp.Models;

public class Expense
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ExpenseStatus Status { get; set; }
}

public enum ExpenseStatus
{
    Submitted,
    Approved,
    Rejected
}
