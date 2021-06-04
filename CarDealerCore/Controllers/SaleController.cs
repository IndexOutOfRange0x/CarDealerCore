using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealerCore.Data;
using CarDealerCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarDealerCore.Controllers
{
    public class SaleController : Controller
    {
        private ApplicationContext _db;

        public SaleController(ApplicationContext db)
        {
            _db = db;
        }

        // GET
        [Authorize(Policy = "User")]
        public async Task<IActionResult> MySales()
        {
            List<Sale> mySales = await _db.Sales
                .Where(x => x.User.UserName == HttpContext.User.Identity.Name)
                .ToListAsync();
            return View(mySales);
        }
        
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AllSales(int Year)
        {
            Year = Year == 0 || Year < 2017 ? DateTime.Now.Year : Year;
            ViewBag.Year = Year;
            var sales = _db.Sales.ToList()
                .Where(x=>x.Date_Sold.Year == Year)
                .GroupBy(x => x.Date_Sold.Month)
                .Select(x => new
                {
                    x.Key,
                    Sum = x.Sum(s => s.Car.Price)
                })
                .OrderBy(x => x.Key);
            //DateTime dateTime = new DateTime(2021,12, 12);
            Dictionary<int, decimal> proceed = new Dictionary<int, decimal>();
            int counter = 1;
            foreach (var sale in sales)
            {
                if (counter < sale.Key)
                {
                    for (; counter < sale.Key; ++counter)
                        proceed[counter] = 0.0m;
                    proceed[sale.Key] = sale.Sum;
                    ++counter;
                }
                else
                    proceed[sale.Key] = sale.Sum;
            }

            while (counter <= 12)
                proceed[counter++] = 0.0m;
            ViewBag.sales = proceed;
            return View(await _db.Sales.ToListAsync());
        }


        public async Task<IActionResult> ChangeStatus(int id)
        {
            Sale sale = await _db.Sales.FindAsync(id);
            sale.Status = "Продано";
            _db.Update(sale);
            await _db.SaveChangesAsync();
            return RedirectToAction("AllSales", "Sale");
        }

    }
}