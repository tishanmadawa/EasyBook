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

        
    }
}