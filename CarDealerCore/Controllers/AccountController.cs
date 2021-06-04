using System;
using System.Threading.Tasks;
using CarDealerCore.ViewModels;
using CarDealerCore.Data;
using CarDealerCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarDealerCore.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationContext _db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        
        public AccountController(
            ApplicationContext context, 
            UserManager<User> userManager, 
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        
        public async Task<IActionResult> UserPage()
        {
            User user = await _db.Users.FirstOrDefaultAsync(x => 
                x.UserName == HttpContext.User.Identity.Name);
            
            ChangePasswordViewModel changePasswordViewModel = 
                new ChangePasswordViewModel(user.Id, user.UserName);
            
            return View(changePasswordViewModel);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        
        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl)
        {
            return View();
        }
        
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = 
                    await _signInManager.PasswordSignInAsync(
                        model.Login, model.Password, false ,false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                         return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return LocalRedirect("~/Home/Index?handler=SetIdentity");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //User curr = await _db.Users.FirstOrDefaultAsync(x => x.UserName == model.Login);
                
                User user = new User {UserName = model.Login};
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.PasswordSignInAsync(
                        model.Login, model.Password, false ,false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UserPage(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    IdentityResult result =
                        await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError(String.Empty, "Ошибка: неправильный пароль!");
            }

            return View(model);
        }
    }
}