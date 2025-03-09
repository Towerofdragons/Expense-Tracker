using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace Backend.Controllers
{
  public class UserController : Controller
  {
    private readonly TrackerDBContext _context;

    public  UserController (TrackerDBContext context)
    {
      _context =  context;
    }

    [Authorize(Roles = "Admin")]
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

        return RedirectToAction("LogIn");
      }
      return View(user);
    }

    public IActionResult LogIn()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> LogIn(User user)
    {
      var currentuser = await _context.Users
          .FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);

      Console.WriteLine(currentuser);
      if (currentuser != null)
      {
          var claims = new List<Claim>
          {
              new Claim(ClaimTypes.Name, currentuser.Name),
              new Claim(ClaimTypes.Email, currentuser.Email),
              new Claim("UserId", currentuser.UserId.ToString()) // Custom claim for User ID
          };

          // ✅ Ensure role claim is added BEFORE creating the ClaimsIdentity
          if (!string.IsNullOrEmpty(currentuser.Role))
          {
              claims.Add(new Claim(ClaimTypes.Role, currentuser.Role));
          }

          var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
          var authProperties = new AuthenticationProperties
          {
              IsPersistent = true,  
              ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
          };

          await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, 
              new ClaimsPrincipal(claimsIdentity), authProperties);


          Console.WriteLine($"User Authenticated: {User.Identity.IsAuthenticated}");
          Console.WriteLine($"User Role: {User.FindFirst(ClaimTypes.Role)?.Value}");

          //  Redirect based on role
          if (currentuser.Role == "Admin") 
          {
              return RedirectToAction("AdminDashboard", "Dashboard");
          }
          
          return RedirectToAction("Index", "Dashboard");
    }

    // Login failed
    ViewData["ErrorMessage"] = "Invalid email or password";
    return View(user);
    }

  
    public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }


  }
}