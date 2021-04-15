using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CarDealerCore.Models;
using Microsoft.EntityFrameworkCore;


namespace CarDealerCore.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationContext db;
        public UsersController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> ShowAllUsers()
        {
            return View(await db.Users.ToListAsync());
        }
    }
}
