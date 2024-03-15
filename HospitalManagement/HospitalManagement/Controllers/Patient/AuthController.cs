using HospitalManagement.Database;
using HospitalManagement.ViewModels.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HospitalManagement.Controllers.Patient;

public class AuthController : Controller
{
    private readonly HospitalDbContext _hospitalDbContext;

    public AuthController(HospitalDbContext hospitalDbContext)
    {
        _hospitalDbContext = hospitalDbContext;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var user = _hospitalDbContext.Users
                             .FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

        if(user == null)
        {
            ModelState.AddModelError("Email", "Password or email is wrong");

            return View();
        }


        var claims = new List<Claim>()
        {
             new Claim("Id", user.Id.ToString())
        };
        var claimIdentity = new ClaimsIdentity(claims, "Cookies");
        var claimPricipal = new ClaimsPrincipal(claimIdentity);

      await  HttpContext.SignInAsync("Cookies", claimPricipal);

        return RedirectToAction("index", "home");
    }
}
