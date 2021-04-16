using Microsoft.AspNetCore.Mvc;

namespace CarDealerCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
