using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelBook.Models;
namespace HotelBook.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        public ActionResult start()
        {
            
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        
        [HttpGet]
        public ActionResult Search()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Search(string searchstring, string searchstring1)
        {
            HotelDBContext hotelDb = new HotelDBContext();
            String name = "Null";
            String city = "Null";
           
            if (!String.IsNullOrEmpty(searchstring))
            {
                name = searchstring;
            }
            if (!String.IsNullOrEmpty(searchstring1))
            {
                city = searchstring1;
            }
            
            List<Customer> range = hotelDb.searchCustomer(name, city);

            return View("~/Views/Home/SearchView.cshtml", range);
        }
       

    }
}