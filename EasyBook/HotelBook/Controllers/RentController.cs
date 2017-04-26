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
        public ActionResult Boarding(string searchstring, string searchstring1)
        {
            HotelDBContext hotelDb = new HotelDBContext();

            
            String state = "Null";
            String city = "Null";
            if (!String.IsNullOrEmpty(searchstring))
            {
                state = searchstring;
            }
            if (!String.IsNullOrEmpty(searchstring1))
            {
                city = searchstring1;
            }
            List<Boarding> range = hotelDb.viewBoarding(state, city);

            return View("~/Views/Rent/Boarding.cshtml", range);
        }

        [HttpGet]
        public ActionResult Boarding()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Signup()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Signup(Boarding boarding)
        {
            HotelDBContext hotelDb = new HotelDBContext();
            hotelDb.Insertboarding(boarding);
            return View();
        }

        [HttpGet]
        public ActionResult Advertisement()
        {

            return View();
        }
        [HttpPost]
        public ActionResult advertisement(string searchstring, string searchstring1,string searchstring2,HttpPostedFileBase file)
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
            HotelDBContext hotelDb = new HotelDBContext();


            String email = "Null";
            String name = "Null";
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
                description = searchstring2;
            }
            hotelDb.advertisement(email, name,description,file.FileName);

            return View();
        }
    }

}