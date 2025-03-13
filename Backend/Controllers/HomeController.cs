using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}



    public class IncomeController : Controller
    {
        private readonly TrackerDBContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<IncomeController> _logger;

        public IncomeController(TrackerDBContext context, ILogger<IncomeController> logger)
        {
            _context = context;
            _logger = logger;

        }

        [HttpPost]
        public IActionResult Add(Income income)
        {
            income.CategoryId = 1; // TODO Categories set by default TODO

            //var userId = _userManager.GetUserId(User);
            
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //Console.WriteLine(ClaimTypes.Name);
            var userID = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            if (string.IsNullOrEmpty(userID))
            {
                _logger.LogWarning("User ID is null. Ensure the user is logged in.");
                return RedirectToAction("Login", "Account");
            }


            income.Id = userID;

            Console.WriteLine(userID);
            income.Category = null;

            Income newIncome = new Income
            {
                Id = userID,  // Assign the correct user ID
                CategoryId = 1,
                Amount = income.Amount,
                Description = income.Description,
                IncomeDate = income.IncomeDate,
                CreatedAt = DateTime.UtcNow
            };

            
                _context.Income.Add(newIncome);
                _context.SaveChanges();
                _logger.LogInformation("Income successfully added.");
                return RedirectToAction("Index", "Dashboard");
            
            Console.WriteLine("Model not set");
            foreach (var key in ModelState.Keys)  // TODO
            {
                foreach (var error in ModelState[key].Errors)
                {
                    Console.WriteLine($"Field: {key} - Error: {error.ErrorMessage}");
                }
            }
            return RedirectToAction("Index", "Dashboard");
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
                return RedirectToAction("Index", "Dashboard", income);
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
        public IActionResult Delete(int id)
        {
            var income = _context.Income.Find(id);
            if (income == null)
            {
                return NotFound();
            }

            _context.Income.Remove(income);
            _context.SaveChanges();
            return RedirectToAction("Index", "Dashboard");
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
            var userID = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            if (string.IsNullOrEmpty(userID))
            {
                _logger.LogWarning("User ID is null. Ensure the user is logged in.");
                return RedirectToAction("Login", "Account");
            }


            expense.Id = userID;

            Console.WriteLine(userID);
            expense.Category = null;

            Expense newExpense = new Expense
            {
                Id = userID,  // Assign the correct user ID
                CategoryId = expense.CategoryId,
                Amount = expense.Amount,
                Description = expense.Description,
                ExpenseDate = expense.ExpenseDate,
                CreatedAt = DateTime.UtcNow
            };

            
                _context.Expense.Add(newExpense);
                _context.SaveChanges();
                _logger.LogInformation("Income successfully added.");
                return RedirectToAction("Index", "Dashboard");

            Console.WriteLine("Model not set"); // TODO
            foreach (var key in ModelState.Keys)
            {
                foreach (var error in ModelState[key].Errors)
                {
                    Console.WriteLine($"Field: {key} - Error: {error.ErrorMessage}");
                }
            }
            return RedirectToAction("Index", "Dashboard");
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
        public IActionResult Edit(string id, Expense updatedExpense)
        {
            var expense = _context.Expense.Find(id);
            if (expense == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                expense.Id = id;
                expense.Amount = updatedExpense.Amount;
                expense.Description = updatedExpense.Description;
                expense.ExpenseDate = updatedExpense.ExpenseDate;
                _context.SaveChanges();
                return RedirectToAction("Index", "Dashboard", expense);
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
        public IActionResult Delete(int id)
        {
            var expense = _context.Expense.Find(id);
            if (expense == null)
            {
                return NotFound();
            }

            _context.Expense.Remove(expense);
            _context.SaveChanges();

            Console.WriteLine($"Deleted expense {id}");
            return RedirectToAction("Index", "Dashboard");
        }
    }

