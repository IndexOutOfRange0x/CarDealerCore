using System.Threading.Tasks;
using CarDealerCore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarDealerCore.Controllers
{
    public class CarController : Controller
    {
        private ApplicationContext _db;
        private readonly ILogger<HomeController> _logger;

        public CarController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _db = context;
            _logger = logger;
        }
        // GET
        public async Task<IActionResult> GetCar(int id)
        {
            return View(await _db.Cars.FindAsync(id));
        }
        
        public IActionResult Buy()
        {
            return RedirectToAction("MySales", "Sale");
        }
        public IActionResult AddCar()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Buy(int? id)
        {
            return RedirectToAction("MySales", "Sale");
        }
    }
}