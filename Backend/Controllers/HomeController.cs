using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;

using Microsoft.AspNetCore.Authorization;

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

    public IActionResult Signup()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }
    public IActionResult IncomeForm()
    {
        return PartialView("_IncomeForm");
    }

    public IActionResult IncomeTable()
    {
        return PartialView("_IncomeTable");
    }

    public IActionResult ExpenseForm()
    {
        return PartialView("_ExpenseForm");
    }

    public IActionResult ExpenseTable()
    {
        return PartialView("_ExpenseTable");
    }

    public IActionResult EditExpenseForm()
    {
        return PartialView("_EditExpenseForm");
    }

    public IActionResult EditIncomeForm()
    {
        return PartialView("_EditIncomeForm");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}



    public class IncomeController : Controller
    {
        private readonly TrackerDBContext _context;
        private readonly ILogger<IncomeController> _logger;

        public IncomeController(TrackerDBContext context, ILogger<IncomeController> logger)
        {
            _context = context;
            _logger = logger;

        }

        [HttpPost]
        public IActionResult Add(Income income)
        {
            income.CategoryId = 1; // Categories set by default
            income.UserId = 1;

            income.User = null;
            income.Category = null;

            if (ModelState.IsValid)
            {
                income.CreatedAt = DateTime.UtcNow;
                _context.Income.Add(income);
                _context.SaveChanges();
                _logger.LogInformation("Income successfully added.");
                return RedirectToAction("Index", "Dashboard");
            }
            Console.WriteLine("Model not set");
            foreach (var key in ModelState.Keys)
            {
                foreach (var error in ModelState[key].Errors)
                {
                    Console.WriteLine($"Field: {key} - Error: {error.ErrorMessage}");
                }
            }
            return RedirectToAction("Index", "Dashboard", income);
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
        private readonly ILogger<IncomeController> _logger;

        public ExpenseController(TrackerDBContext context, ILogger<IncomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Add(Expense expense)
        {
            expense.CategoryId = 1; // Categories set by default
            expense.UserId = 1;

            expense.User = null;
            expense.Category = null;

            if (ModelState.IsValid)
            {
                expense.CreatedAt = DateTime.UtcNow;
                _context.Expense.Add(expense);
                _context.SaveChanges();
                _logger.LogInformation("expense successfully added.");
                return RedirectToAction("Index", "Dashboard");
            }
            Console.WriteLine("Model not set");
            foreach (var key in ModelState.Keys)
            {
                foreach (var error in ModelState[key].Errors)
                {
                    Console.WriteLine($"Field: {key} - Error: {error.ErrorMessage}");
                }
            }
            return RedirectToAction("Index", "Dashboard", expense);
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

