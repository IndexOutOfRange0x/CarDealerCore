using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CarDealerCore.ViewModels;
using CarDealerCore.Data;
using CarDealerCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarDealerCore.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationContext _db;
        private readonly ILogger<AccountController> _logger;

        public AccountController(ApplicationContext context, ILogger<AccountController> logger)
        {
            _db = context;
            _logger = logger;
        }
        
        public IActionResult UserPage()
        {
            return View();
        }
        
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            User user = await _db.Users
                .FirstOrDefaultAsync(X => 
                    X.Login == model.Login && X.Password == model.Password);
            Admin admin = await _db.Admins
                .FirstOrDefaultAsync(X => 
                    X.Login == model.Login && X.Password == model.Password);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Login)
            };
            if (user != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
            }
            else if (admin != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            else
            {
                ModelState.AddModelError("", "User not found");
                return View(model);
            }
            await HttpContext.SignInAsync("Cookie", 
                    new ClaimsPrincipal(
                    new ClaimsIdentity(claims, "Cookie")));
            
            return View();
        }
    }
}