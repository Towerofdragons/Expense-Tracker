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
    private readonly ILogger<UserController> _logger;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public  UserController (TrackerDBContext context, ILogger<UserController> logger, SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context =  context;
        _logger = logger;
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
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
    public async Task<IActionResult> Register(UserRegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.Email = model.Email.ToLower();

            // Check if email is already taken
            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                ModelState.AddModelError("Email", "This email is already registered.");
                return View(model);
            }

            // Create user object
            var user = new User
            {
                Name = model.Name,
                UserName = model.Email,
                Email = model.Email
            };

            // Attempt to create the user
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Ensure "User" role exists before assigning
                if (!await _roleManager.RoleExistsAsync("User"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                }

                // Assign default role
                await _userManager.AddToRoleAsync(user, "User");

                TempData["SuccessMessage"] = "Registration successful! You can now log in.";
                return RedirectToAction("LogIn");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    _logger.LogWarning($"Registration error: {error.Description}");
                }
            }
        }

        _logger.LogWarning("User registration failed due to invalid model state.");
        return View(model);
    }



    public IActionResult LogIn()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> LogIn(UserLoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email.ToLower());

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
                
                if (result.Succeeded)
                {
                    // Get user claims
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim("UserId", user.Id)
                    };

                    var roles = await _userManager.GetRolesAsync(user);
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }


                    var claimsIdentity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, claimsPrincipal);


                    _logger.LogInformation($"User {user.Email} logged in successfully.");

                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "User");
                    }
                    foreach (var claim in User.Claims)
                    {
                        Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
                    }

                    return RedirectToAction("Index", "Dashboard");
                }
            }

            ViewData["ErrorMessage"] = "Invalid email or password";
            return View();
        }
  
    public async Task <IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }


  }
}