using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CarDealerCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDealerCore.Controllers
{
    public class SalesController : Controller
    {
        private ApplicationContext db;
        public SalesController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> ShowAllSales()
        {
            return View(await db.Sales.ToListAsync());
        }
    }
}
