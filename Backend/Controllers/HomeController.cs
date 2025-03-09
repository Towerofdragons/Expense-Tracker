using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;

namespace Backend.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

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

    public class IncomeController : Controller
    {
        private readonly TrackerDBContext _context;

        public IncomeController(TrackerDBContext context)
        {
            _context = context;
        }

        public IActionResult Add(Income income)
        {
            if (income != null)
            {
                _context.Income.Add(income);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    
        // Edit Income - GET
        public IActionResult Edit(int id)
        {
            var income = _context.Income.Find(id);
            if (income == null)
            {
                return NotFound();
            }
            return View(income);
        }

        // Edit Income - POST
        [HttpPost]
        public IActionResult Edit(int id, Income updatedIncome)
        {
            var income = _context.Income.Find(id);
            if (income == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                income.Amount = updatedIncome.Amount;
                income.Description = updatedIncome.Description;
                income.IncomeDate = updatedIncome.IncomeDate;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(updatedIncome);
        }

        // Delete Income - GET
        // public IActionResult Delete(int id)
        // {
        //     var income = _context.Income.Find(id);
        //     if (income == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(income);
        // }

        // Delete Income - POST
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var income = _context.Income.Find(id);
            if (income == null)
            {
                return NotFound();
            }

            _context.Income.Remove(income);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }




    public class ExpenseController : Controller
    {
        private readonly TrackerDBContext _context;

        public ExpenseController(TrackerDBContext context)
        {
            _context = context;
        }

        public IActionResult Add(Expense expense)
        {
            if (expense != null)
            {
                _context.Expense.Add(expense);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Edit Expense - GET
        public IActionResult Edit(int id)
        {
            var expense = _context.Expense.Find(id);
            if (expense == null)
            {
                return NotFound();
            }
            return View(expense);
        }

        // Edit Expense - POST
        [HttpPost]
        public IActionResult Edit(int id, Expense updatedExpense)
        {
            var expense = _context.Expense.Find(id);
            if (expense == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                expense.Amount = updatedExpense.Amount;
                expense.Description = updatedExpense.Description;
                expense.ExpenseDate = updatedExpense.ExpenseDate;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(updatedExpense);
        }

        // Delete Expense - GET
        // public IActionResult Delete(int id)
        // {
        //     var expense = _context.Expense.Find(id);
        //     if (expense == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(expense);
        // }

        // Delete Expense - POST
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var expense = _context.Expense.Find(id);
            if (expense == null)
            {
                return NotFound();
            }

            _context.Expense.Remove(expense);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }

