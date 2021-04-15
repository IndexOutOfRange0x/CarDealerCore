using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CarDealerCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDealerCore.Controllers
{
    public class CarsController : Controller
    {
        private ApplicationContext db;
        public CarsController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> ShowAllCars()
        {
            return View(await db.Cars.ToListAsync());
        }
        public IActionResult AddCarPage()
        {
            return View();
        }
        public IActionResult AddCar(string vin, string mark, string model, string description, decimal price)
        {
            Car car = new Car()
            {
                VIN = vin,
                Mark = mark,
                Model = model,
                Description = description,
                IsSold = false,
                Price = price
            };

            db.Cars.Add(car);

            return Redirect("~/Home/Index");
        }
    }
}
