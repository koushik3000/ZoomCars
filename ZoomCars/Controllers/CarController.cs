using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZoomCars.Models;

namespace ZoomCars.Controllers
{
    public class CarController : Controller
    {
        newCarContext context = new newCarContext();

        public IActionResult First()
        {
            var items = context.Locations.ToList();
            if (items != null)
            {
                ViewBag.data = items;
            }
  
            return View();
        }

        public IActionResult Filter(string search, int seat)
        {

            var data = TempData["Model"].ToString();
            TempData.Keep("Model");
            var c = JsonConvert.DeserializeObject<List<Car>>(data);
            /*List<Car> products = JsonConvert.DeserializeObject<List<Car>>(TempData["Model"]);*/
            /* List<Car> c = JsonConvert.DeserializeObject<List<Car>>(TempData["Model"]);*/
            /*var c = context.Cars.Where(x => x.CarAvailable == "yes");*/

            /*  var modelData = TempData["employee"] as List<CarVM>;*/
            /* var data = TempData["employee"] as List<CarVM>;*/

            
          


            if ((String.IsNullOrEmpty(search)) && (seat == 0))
            {
                c = c.Where(x => x.CarAvailable == "yes").ToList();
            }
            else if (String.IsNullOrEmpty(search))
            {
                c =  c.Where(x => x.CarSeats == seat).ToList();
            }

            else if (seat == 0)
            {
                c =  c.Where(x => x.CarType == search).ToList();
            }
            else
            {
                c =  c.Where(x => x.CarSeats == seat && x.CarType == search).ToList();
            }
            return View(c.ToList());
  
        }

         
        public IActionResult Booking()
        {

            var id = Convert.ToInt32(TempData["carid"]);
            TempData["id2"] = id;

            var data = TempData["Model"].ToString();
            TempData.Keep("Model");
            var c = JsonConvert.DeserializeObject<List<Car>>(data);
            Car m = c.Where(x => x.Vin == id).SingleOrDefault();
            TempData["Amount"] = m.CarPrice;

            Car model = context.Cars.Where(x => x.Vin == id).SingleOrDefault();   
            return View(m);
            

        }


        public IActionResult CheckLogin(int vin)
        {
            TempData["carid"] = vin;
            if (TempData["UserName"] != null)
            {
                if(TempData["carid"] == null)
                {
                    return RedirectToAction("Filter");
                }
                else
                {
                    return RedirectToAction("Booking");
                }
                
            }
            else
            {
                return RedirectToAction("Login");
            }
                
        }

         
        public IActionResult Login()
        {
           
            return View();
        }

        
        public IActionResult Login2(string UName, string PWord)
        {
            var U = context.Customer1s.Where(x => x.Name == UName).SingleOrDefault();
            if (U != null)
            {
                if (U.Password == PWord)
                {


                    TempData["UserName"] = U.Name;
                    TempData["Id"] = U.CustomerId;
                    if(TempData["Locationid"] != null )
                    {
                        return RedirectToAction("Booking");
                    }
                    else
                    {
                        return RedirectToAction("First");
                    }
                }

                else
                { 
                    return RedirectToAction("LoginError");
                }

            }


            return RedirectToAction("LoginError");
        }

        public IActionResult LoginError()
        {
            return View();
        }


        public IActionResult Booked()
        {
            var vin = Convert.ToInt32(TempData["id2"]);
            Car model = context.Cars.Where(x => x.Vin == vin).SingleOrDefault();
            model.CarAvailable = "no";
            context.SaveChanges();

            var data = TempData["Model"].ToString();
            TempData.Keep("Model");
            var c = JsonConvert.DeserializeObject<List<Car>>(data);
            Car cat = c.Where(x => x.Vin == vin).SingleOrDefault();

            TempData["vin"] = vin;
            /*TempData["Amount"] = cat.CarPrice;*/


            var item = new Rental()
            {
                
                Amount = Convert.ToInt32(TempData["Amount"]),
                Vin = Convert.ToInt32(TempData["vin"]),
                PickUpDate = Convert.ToDateTime(TempData["FromDate"]),
                ReturnDate = Convert.ToDateTime(TempData["ToDate"]),

                LocationId = Convert.ToInt32(TempData["Locationid"]),
                CustomerId = Convert.ToInt32(TempData["Id"])

            };
            context.Rentals.Add(item);
            context.SaveChanges();
            return View();


        }

        

        public IActionResult price()
        {
            /* TempData["FromDate"] = FrmDate;
             TempData["ToDate"] = ToDate;
             TempData["Locationid"] = City;*/
            var FrmDate = Convert.ToDateTime(TempData["FromDate"]);
            TempData.Keep("FromDate");
            var ToDate = Convert.ToDateTime(TempData["ToDate"]);
            TempData.Keep("ToDate");

            var days = (int)ToDate.Subtract(FrmDate).TotalDays;
 
            var model = context.Cars.Where(x => x.CarAvailable == "yes").ToList();

            
            /* foreach (var item in model)
             {
                 var a = item.CarPrice * days;


             }*/
           
            List<Car> obj = model;
            foreach (var item in obj)
            {
                var a = item.CarPrice * days;

                item.CarPrice = a;
  
            }
            /*return RedirectToAction("Filter");*/


            TempData["Model"] = JsonConvert.SerializeObject(obj.ToList());
            return RedirectToAction("Filter");
    
        }

       public IActionResult CheckAvail(DateTime FrmDate, DateTime ToDate, string City)
        {
            TempData["FromDate"] = FrmDate;
            TempData["ToDate"] = ToDate;
            TempData["Locationid"] = City;

            var tdate = DateTime.Now;
            

            var q = (from pd in context.Rentals
                     join od in context.Cars on pd.Vin equals od.Vin
                     where (pd.ReturnDate < tdate)
                     select new
                     {
                         od.Vin
                     }).ToList();
            foreach (var item in q)
            {
               

                Car model = context.Cars.Where(x => x.Vin == item.Vin).SingleOrDefault();
                model.CarAvailable = "yes";
                context.SaveChanges();
            }
            var q1 = (from pd in context.Rentals
                     join od in context.Cars on pd.Vin equals od.Vin
                     where (pd.ReturnDate > tdate)
                     select new
                     {
                         od.Vin
                     }).ToList();
            foreach (var item in q1)
            {


                Car model = context.Cars.Where(x => x.Vin == item.Vin).SingleOrDefault();
                model.CarAvailable = "no";
                context.SaveChanges();
            }



            return RedirectToAction("price");
        }


        [HttpGet]
        public IActionResult Signup()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Signup(Customer1 cus)
        {

            var user = context.Customer1s.Where(x => x.Name == cus.Name).SingleOrDefault();
            if (user != null)
            {
                ViewBag.Notification = "This User Name is already taken";
                return View();
            }
            else
            {
                TempData["UserName"] = cus.Name;
                context.Add(cus);
                context.SaveChanges();
                if (TempData["Locationid"] != null)
                {
                    return RedirectToAction("Booking");
                }
                else
                {
                    return RedirectToAction("First");
                }


            }
        }



        public IActionResult Logout()
        {
            TempData.Remove("UserName");
            TempData.Remove("Locationid");

            return RedirectToAction("First", "Car");
        }
    }
}
