using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;

using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers;


[Authorize]
public class DashboardController : Controller
{

    private readonly TrackerDBContext _context;

    public  DashboardController (TrackerDBContext context)
    {
      _context =  context;
    }

    public IActionResult Index()
    {
        var Expenses = _context.Expense.ToList();
        var Incomes = _context.Income.ToList();

        decimal TotalIncomes = 0;
        decimal TotalExpenses = 0;

        foreach (var expense in Expenses)
        {
            TotalExpenses += (decimal)expense.Amount;
        }

        foreach (var income in Incomes)
        {
            TotalIncomes += (decimal)income.Amount;
        }

        
        var model = new IncomeExpenses
        {
            TotalExpense = TotalExpenses,
            TotalIncome = TotalIncomes,
            Expense = _context.Expense.ToList(),
            Income = _context.Income.ToList()
        };

        return View(model);
    }

   

    
}