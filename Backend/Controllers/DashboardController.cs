using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;


[Authorize]
public class DashboardController : Controller
{

    private readonly TrackerDBContext _context;
    private readonly UserManager<User> _userManager;
    public  DashboardController (TrackerDBContext context, UserManager<User> userManager)
    {
      _context =  context;
      _userManager = userManager;
    }

    public async Task<IncomeExpenses> GetModel(){
        var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
    


        var Expenses = await _context.Expense.Include(e => e.Category).Where(e => e.Id.ToString() == userId).ToListAsync();
        var Incomes = await _context.Income.Where(e => e.Id.ToString() == userId).ToListAsync();

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
            Expense = Expenses,
            Income = Incomes
        };

        return model;
        
    }

    public async Task<IActionResult> CategoriesChartData()
    {
        // Group income/expenses by category and sum amounts
        var categoryData = await _context.Expense
            .GroupBy(e => e.Category.Name)
            .Select(g => new 
            {
                Category = g.Key,
                TotalAmount = g.Sum(e => e.Amount)
            })
            .ToListAsync();

        return Json(categoryData); // Returns JSON for JavaScript to consume

    }

    public async Task<IActionResult> Index()
    {
        string userName = User.Identity.IsAuthenticated ? User.Identity.Name : "Guest";
        var categories = await _context.Categories.ToListAsync();
        
        ViewData["chartData"] = await CategoriesChartData();
        ViewData["Categories"] = categories ?? new List<Category>();
        ViewBag.UserName = userName;

        var model = await GetModel();
        return View(model);
    }

    public async Task<IActionResult> IncomeTable()
    {
        var model = await GetModel();
        return PartialView("_IncomeTable", model);
    }

    public async Task<IActionResult> ExpenseTable()
    {
        var model = await GetModel();
        var categories = await _context.Categories.ToListAsync();

        foreach(var category in categories)
        {
            Console.WriteLine(category.CategoryId);
        }

        ViewData["Categories"] = categories ?? new List<Category>();
        return PartialView("_ExpenseTable", model);
    }

    public async Task<IActionResult> EditExpenseForm(int Id)
    {   
        var categories = await _context.Categories.ToListAsync();
        ViewData["Categories"] = categories ?? new List<Category>();
    
        var expense = _context.Expense.Find(Id);
            if (expense == null)
            {
                return NotFound();
            }
        return PartialView("_EditExpenseForm", expense);
    }

    public async Task<IActionResult> EditIncomeForm(int Id)
    {
        var categories = await _context.Categories.ToListAsync();
        ViewData["Categories"] = categories ?? new List<Category>();
        var income = _context.Income.Find(Id);
        if (income == null)
        {
            return NotFound();
        }
        return PartialView("_EditIncomeForm", income);

    }

}