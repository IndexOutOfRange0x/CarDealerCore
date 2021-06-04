using System;
using System.Threading.Tasks;
using CarDealerCore.Data;
using CarDealerCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarDealerCore.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private ApplicationContext _db;

        public CarController(ApplicationContext context)
        {
            _db = context;
        }
        // GET
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCar(int id)
        {
            return View(await _db.Cars.FindAsync(id));
        }
        
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult AddCar()
        {
            return View();
        }
        
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AddCar(Car car)
        {
            
            Car c = await _db.Cars.FirstOrDefaultAsync(x => x.VIN == car.VIN);
            if (c == null)
            {
                _db.Cars.Add(car);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> Buy(int id)
        {
            User user = await _db.Users.FirstOrDefaultAsync(x => 
                x.UserName == User.Identity.Name);
            await _db.Sales.AddAsync(new Sale(user.Id, id, DateTime.Now, "Выполняется"));
            Car car = await _db.Cars.FindAsync(id);
            car.IsSold = true;
            _db.Cars.Update(car);
            await _db.SaveChangesAsync();
            return RedirectToAction("MySales", "Sale");
        }
    }
}