using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelBook.Models;
using System.Web.Mvc;
using System.IO;

namespace HotelBook.Controllers
{
    public class RentController : Controller
    {
        // GET: Rent
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Boarding(string searchstring)
        {
            HotelDB hotelDb = new HotelDB();

            
           
            String city = "Null";
            
            if (!String.IsNullOrEmpty(searchstring))
            {
                city = searchstring;
            }
            List<Boarding> range = hotelDb.viewBoarding(city);

            return View("~/Views/Rent/Boarding.cshtml", range);
        }

        [HttpGet]
        public ActionResult Boarding()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Settings()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Settings1(string searchstring)
        {
            String name = "Null";
            HotelDB hotelDb = new HotelDB();
            
            
            if (!String.IsNullOrEmpty(searchstring))
            {
                name = searchstring;
            }
            
            List<Boarding> range = hotelDb.viewAdd(name);

            return View("~/Views/Rent/Settings1.cshtml", range);
        }

        [HttpGet]
        public ActionResult Advertisement()
        {

            return View();
        }
        public ActionResult DeleteView(int id)
        {
            HotelDB hotelDb = new HotelDB();
            Boarding board=hotelDb.DeleteAddView(id);
            return View("~/Views/Rent/DeleteView.cshtml",board);
        }
        public ActionResult DeleteAdd(int id,Boarding b)
        {
            HotelDB hotelDb = new HotelDB();
            hotelDb.DeleteAdd(id,b);
            return View("~/Views/Rent/Settings.cshtml");
        }

        [HttpPost]
        public ActionResult advertisement(string searchstring, string searchstring1,string searchstring2, string searchstring3, string searchstring4, string searchstring5, string searchstring6, HttpPostedFileBase file)
        {
            var path= "";
            if (file!=null)
            {
                if (file.ContentLength > 0)
                {
                    if (Path.GetExtension(file.FileName).ToLower() == ".jpg")
                    {
                        path = Path.Combine(Server.MapPath("~/image/upload"),file.FileName);
                        file.SaveAs(path);
                        ViewBag.UploadSuccess = "true";
                    }
                }
            }
            HotelDB hotelDb = new HotelDB();


            String email = "Null";
            String name = "Null";
            String city = "Null";
            String telephone = "Null";
            String address = "Null";
            String password = "Null";
            String description = "Null";
            if (!String.IsNullOrEmpty(searchstring))
            {
                email = searchstring;
            }
            if (!String.IsNullOrEmpty(searchstring1))
            {
                name = searchstring1;
            }
            if (!String.IsNullOrEmpty(searchstring2))
            {
                address = searchstring2;
            }
            if (!String.IsNullOrEmpty(searchstring3))
            {
                city = searchstring3;
            }
            if (!String.IsNullOrEmpty(searchstring4))
            {
                telephone = searchstring4;
            }
            if (!String.IsNullOrEmpty(searchstring5))
            {
                description = searchstring5;
            }
            if (!String.IsNullOrEmpty(searchstring6))
            {
                password = searchstring6;
            }
            hotelDb.advertisement(email,name,address,city,telephone,description, password, file.FileName);

            return View();
        }
    }

}