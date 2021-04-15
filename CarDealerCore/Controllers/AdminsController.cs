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
    }
}
