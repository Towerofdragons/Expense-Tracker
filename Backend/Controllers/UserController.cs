using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;

namespace Backend.Controllers
{
  public class UserController : Controller
  {
    private readonly TrackerDBContext _context;

    public  UserController (TrackerDBContext context)
    {
      _context =  context;
    }

    public IActionResult Index()
    {
        var users = _context.Users.ToList();
        return View(users);
    }


    public IActionResult Register()
    {
      return View();
    }
  [HttpPost]
    public IActionResult Register(User user)
    {
      if (ModelState.IsValid)
      {
        user.UserId =  _context.Users.Count() + 1; // TODO -- Temp since in memory db doesn't auto increment
        _context.Users.Add(user);
        _context.SaveChanges();

        return RedirectToAction("Log in");
      }
      return View(user);
    }

  public IActionResult Login()
  {
    return View();
  }
  [HttpPost]
    public IActionResult LogIn(User user)
    {
      var currentuser = _context.Users
      .FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);

      if (currentuser != null)
      {
        return RedirectToAction("Index", "Home");
      }
      // Check model objects for given auth information
      ViewData["ErrorMessage"] = "Invalid email or password";
      return View(user);
    }


  }
}