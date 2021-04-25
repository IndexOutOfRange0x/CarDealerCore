using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CarDealerCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            return View(await db.Cars
                //.Where(Car => Car.IsSold == false)
                .ToListAsync());
        }
        [HttpGet]
        public IActionResult AddCarPage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCar(Car car)
        {
            db.Cars.Add(car);
            await db.SaveChangesAsync();
            return Redirect("~/Cars/ShowAllCars");
        }
        public async Task<IActionResult> EditCarPage(int? id)
        {
            if (id != null)
            {
                Car car = await db.Cars.FirstOrDefaultAsync(p => p.Id == id);
                if (car != null)
                    return View(car);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditCar(Car car)
        {
            db.Cars.Update(car);
            await db.SaveChangesAsync();
            return Redirect("~/Cars/ShowAllCars");
        }
        [HttpGet]
        [ActionName("DeleteCar")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Car car = await db.Cars.FirstOrDefaultAsync(p => p.Id == id);
                if (car != null)
                    return View(car);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCar(int? id)
        {
            if (id != null)
            {
                Car car = new Car { Id = id.Value };
                db.Entry(car).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return Redirect("~/Cars/ShowAllCars");
            }
            return NotFound();
        }
    }
}
