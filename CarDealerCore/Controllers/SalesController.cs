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
        [HttpGet]
        public IActionResult AddSalePage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddSale(Sale sale)
        {
            db.Sales.Add(sale);
            await db.SaveChangesAsync();
            return Redirect("~/Sales/ShowAllSales");
        }
        public async Task<IActionResult> EditSalesPage(int? id)
        {
            if (id != null)
            {
                Sale sale = await db.Sales.FirstOrDefaultAsync(p => p.Id == id);
                if (sale != null)
                    return View(sale);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditSale(Sale sale)
        {
            db.Sales.Update(sale);
            await db.SaveChangesAsync();
            return Redirect("~/Sales/ShowAllSales");
        }
        [HttpGet]
        [ActionName("DeleteSale")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Sale sale = await db.Sales.FirstOrDefaultAsync(p => p.Id == id);
                if (sale != null)
                    return View(sale);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSale(int? id)
        {
            if (id != null)
            {
                Sale sale = new Sale { Id = id.Value };
                db.Entry(sale).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return Redirect("~/Sales/ShowAllSales");
            }
            return NotFound();
        }
    }
}
