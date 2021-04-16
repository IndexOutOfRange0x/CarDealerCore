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
        [HttpGet]
        public IActionResult AddUserPage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Redirect("~/Users/ShowAllUsers");
        }
        public async Task<IActionResult> EditUserPage(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(User user)
        {
            db.Users.Update(user);
            await db.SaveChangesAsync();
            return Redirect("~/Users/ShowAllUsers");
        }
        [HttpGet]
        [ActionName("DeleteUser")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int? id)
        {
            if (id != null)
            {
                User user = new User { Id = id.Value };
                db.Entry(user).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return Redirect("~/Users/ShowAllUsers");
            }
            return NotFound();
        }
    }
}
