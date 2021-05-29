using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarDealerCore.Controllers
{
    public class SaleController : Controller
    {
        // GET
        public IActionResult MySales()
        {
            return View();
        }
        
        [Authorize(Policy = "Admin")]
        public IActionResult AllSales()
        {
            return View();
        }
    }
}