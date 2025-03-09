
namespace Backend.Models
{
  public class IncomeExpenses
  {
    public Decimal TotalIncome;
    public Decimal TotalExpense;
    public List<Expense> Expense { get; set;} = new List<Expense>();
    public List<Income> Income { get; set; } = new List<Income>();
  }
}