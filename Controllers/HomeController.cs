using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using newwebproject.Models;
using coreFormsAndValidations.models;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;


namespace newwebproject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController>? _logger;
    private readonly ApplicationDbContext? _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
    _logger = logger;
    _context = context;
    }



   

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult products()
    {
        return View();
    }

    public IActionResult advantages()
    {
        return View();
    }

    

    public IActionResult GetAccount()
    {
        return View();
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        // Query database to find user by username
        var user = await _context.Accounts.SingleOrDefaultAsync(u => u.Username == username);

        if (user != null && password == user.Password)
        {
            // Authentication successful
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("UsersName",user.Name),
                new Claim("Address", user.Address), // Address
                new Claim(ClaimTypes.Email, user.Email), // Email
                new Claim(ClaimTypes.MobilePhone, user.Phone.ToString()),
                // Add more claims if needed (e.g., roles)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                // Set properties like expiration, persistent cookie, etc.
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            var userName = User.Identity.Name;
            var name = User.FindFirst("UsersName")?.Value;
            var address = User.FindFirst("Address")?.Value; 
            var email = User.FindFirst(ClaimTypes.Email)?.Value; 
            var phone = User.FindFirst(ClaimTypes.MobilePhone)?.Value;
            
            TempData["AlertMessage"] = "login succesful";
            return RedirectToAction("contactus", "Home");
            
        }
        else
        {
            // Authentication failed
            // Add error message to be displayed on the login page
            TempData["AlertMessage"] = "login again";
            return RedirectToAction("Login");
        }
    }

    [HttpPost]
    public IActionResult PostAccount (Account account)
    {
        if(ModelState.IsValid)
        {   
            _context.Accounts.Add(account);
            _context.SaveChanges();
            TempData["AlertMessage"] = "Account created sucessfully";
            return RedirectToAction("GetAccount");
        }
        TempData["AlertMessage"] = "Try again enter your details";
        return RedirectToAction("GetAccount");
    }
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("contactus");
    }

    
    public IActionResult contactus()
    {   
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


}
