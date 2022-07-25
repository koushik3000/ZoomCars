using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ZoomCars.Models;
 


namespace ZoomCars.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        newCarContext context = new newCarContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

        /* public IActionResult DispCustomers()
         {
             List<Customer> list = context.Customers.ToList();
             return View(list);
         }
 */

      


      
        public IActionResult DispCars()
        {
            List<Car> list = context.Cars.ToList();
            return View(list);
        }

       
       /* public ActionResult Amount(int numberDays)
        {
            MyModel model = new MyModel();
            model.NumberOfDays = numberDays;
            model.Cars = context.Cars.ToList(); // get the car records here

            foreach (var car in model.Cars)
            {
                car.FullPrice = car.CarPrice * model.NumberOfDays;
            }

            return View(model);
        }*/


        public IActionResult First()
        {
            var items = context.Locations.ToList();
            if (items != null)
            {
                ViewBag.data = items;
            }

            TempData.Clear();
            return View();
        }

      
       
    }
}
