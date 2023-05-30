using System.Security.Claims;
using EduEatsApi.Services;
using EduEatsApi.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduEatsApi.Controllers;

public class AccountController : Controller
{
    private readonly DatabaseContext _context;

    public AccountController(DatabaseContext context)
    {
        _context = context;
    }
    
    public IActionResult Authorization() => View();
   
    
    
    [HttpPost]
    public async Task<IActionResult> Authorization(AuthorizationViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
            if (user != null)
            {
                await Authenticate(user.Email);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
        }
        return View(model);
    }
    
    private async Task Authenticate(string userName)
    {
        var claims = new List<Claim>
        {
            new (ClaimsIdentity.DefaultNameClaimType, userName)
        };

        var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
    }
    
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}