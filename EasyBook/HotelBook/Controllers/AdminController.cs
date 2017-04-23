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

       
        public ActionResult Requests()
        {
            HotelDBContext hotelDb = new HotelDBContext();
            List<Customer> range = hotelDb.Viewcustomer();
            return View(range);
        }
        public void setAccept(string email)
        {
            HotelDBContext hotelDb = new HotelDBContext();
            Debug.WriteLine(email);

            string h = email;
            hotelDb.set(h);
            hotelDb.sendMail(h);
        }
        public void setReject(string email)
        {
            HotelDBContext hotelDb = new HotelDBContext();


            string h = email;
            hotelDb.setr(h);
        }

    }
}
