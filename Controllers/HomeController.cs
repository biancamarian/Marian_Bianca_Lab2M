using Marian_Bianca_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Marian_Bianca_Lab2.Data;
using Marian_Bianca_Lab2.Models.LibraryViewModels;
using Marian_Bianca_Lab2.Models.CustomerViewModels;

namespace Marian_Bianca_Lab2.Controllers
{
    public class HomeController : Controller
    {
        private readonly LibraryContext _context;

        public HomeController(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<ActionResult> Statistics()
        {
            IQueryable<OrderGroup> data =
                                          from order in _context.Orders
                                          group order by order.OrderDate into dateGroup
                                          select new OrderGroup()
                                          {
                                              OrderDate = dateGroup.Key,
                                              BookCount = dateGroup.Count()
                                          };
            return View(await data.AsNoTracking().ToListAsync());
        }
        public async Task<ActionResult> CustomerStatistics()
        {
            IQueryable<CustomerGroup> data =
                from customer in _context.Customers
                group customer by customer.Name into customerGroup
                select new CustomerGroup()
                {
                    CustomerName = customerGroup.Key,
                    BookCount = customerGroup.Count()
                };
            return View(await data.AsNoTracking().ToListAsync());
        }
    }
}
