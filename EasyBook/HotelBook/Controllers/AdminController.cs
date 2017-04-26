using HotelBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
namespace HotelBook.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult request()

        {
            HotelDBContext hotelDb = new HotelDBContext();
            List<Customer> range = hotelDb.Viewcustomer();
            return View(range);
        }
        public ActionResult setAccept(string email)
        {
            HotelDBContext hotelDb = new HotelDBContext();
            Debug.WriteLine(email);

            string h = email;
            hotelDb.set(h);
            hotelDb.sendMail(h);
            return View("~/Views/Admin/request.cshtml");
        }
        public void setReject(string email)
        {
            HotelDBContext hotelDb = new HotelDBContext();


            string h = email;
            hotelDb.setr(h);

        }


        public ActionResult user()

        {
            HotelDBContext hotelDb = new HotelDBContext();
            List<Customer> range2 = hotelDb.Viewuser();
            return View(range2);
        }
        public ActionResult logout()
        {
            return View("~/Views/Home/index.cshtml");
        }
        [HttpGet]
        public ActionResult upsetting()
        {
            HotelDBContext hotelDb = new HotelDBContext();
            Customer range = hotelDb.Viewsettings();
            return View(range);

        }
        [HttpPost]
        public ActionResult upsetting(Customer customer)
        {
            HotelDBContext hotelDb = new HotelDBContext();
            hotelDb.upseting(customer);
            return View();
        }

    }
}
