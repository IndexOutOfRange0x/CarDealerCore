using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealerCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDealerCore.Controllers
{
    public class AdminsController : Controller
    {
        private ApplicationContext db;
        public AdminsController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> ShowAllAdmins()
        {
            return View(await db.Admins.ToListAsync());
        }
        [HttpGet]
        public IActionResult AddAdminPage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAdmin(Admin admin)
        {
            db.Admins.Add(admin);
            await db.SaveChangesAsync();
            return Redirect("~/Admins/ShowAllAdmins");
        }
        public async Task<IActionResult> EditAdminPage(int? id)
        {
            if (id != null)
            {
                Admin admin = await db.Admins.FirstOrDefaultAsync(p => p.Id == id);
                if (admin != null)
                    return View(admin);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditAdmin(Admin admin)
        {
            db.Admins.Update(admin);
            await db.SaveChangesAsync();
            return Redirect("~/Admins/ShowAllAdmins");
        }
        [HttpGet]
        [ActionName("DeleteAdmin")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Admin admin = await db.Admins.FirstOrDefaultAsync(p => p.Id == id);
                if (admin != null)
                    return View(admin);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAdmin(int? id)
        {
            if (id != null)
            {
                Admin admin = new Admin { Id = id.Value };
                db.Entry(admin).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return Redirect("~/Admins/ShowAllAdmins");
            }
            return NotFound();
        }
    }
}
