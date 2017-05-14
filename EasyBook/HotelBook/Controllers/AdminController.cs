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
            List<Customer> range = hotelDb.Viewcustomer();
            string h = email;
            hotelDb.set(h);
            hotelDb.sendMail(h);
            return View("~/Views/Admin/request.cshtml",range);
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

        public ActionResult post()

        {
            admin hotelDb = new admin();
            // List<DateTime> arr = hotelDb.GetPostData();
            //  List<DateTime> arr2 = hotelDb.GetAlbumData();
            // List<DateTime> arr3 = hotelDb.GetEventData();
            // var all = arr3.Concat(arr2).Concat(arr).ToList();
            /// List<DateTime> lok = all.Distinct().ToList();
            DateTime today = DateTime.Today;
            DateTime daye = today.AddDays(-14).Date;
            Debug.WriteLine(daye);
            MyViewModel viewModel = new MyViewModel();
            viewModel.TheSecondTable = hotelDb.getPostEvents(daye);
            viewModel.TheFirstTable = hotelDb.getPostPosts(daye);
            viewModel.TheThirdTable = hotelDb.getAlbumName(daye);
            viewModel.ThefourthTable = hotelDb.getImg(daye);
            viewModel.crntdate = DateTime.Today.Date;




            /*  foreach (var item in viewModel.TheThirdTable)
              {
                  Debug.WriteLine(item);
                  viewModel.ThefourthTable= hotelDb.getImages(item.albumName);
              }
                  Debug.WriteLine("added");


         */
            return View(viewModel);
        }

        public ActionResult doPostReject(int id)
        {
            admin hotelDb = new admin();
            hotelDb.doPostRejects(id);
            DateTime today = DateTime.Today;
            DateTime daye = today.AddDays(-14).Date;
            Debug.WriteLine(daye);
            MyViewModel viewModel = new MyViewModel();
            viewModel.TheSecondTable = hotelDb.getPostEvents(daye);
            viewModel.TheFirstTable = hotelDb.getPostPosts(daye);
            viewModel.TheThirdTable = hotelDb.getAlbumName(daye);
            viewModel.ThefourthTable = hotelDb.getImg(daye);
            viewModel.crntdate = DateTime.Today.Date;



            return View("~/Views/Admin/post.cshtml", viewModel);
        }

        public ActionResult doPostDelete(int id)
        {
            admin hotelDb = new admin();
            hotelDb.doPostDelete(id);
            DateTime today = DateTime.Today;
            DateTime daye = today.AddDays(-14).Date;
            Debug.WriteLine(daye);
            MyViewModel viewModel = new MyViewModel();
            viewModel.TheSecondTable = hotelDb.getPostEvents(daye);
            viewModel.TheFirstTable = hotelDb.getPostPosts(daye);
            viewModel.TheThirdTable = hotelDb.getAlbumName(daye);
            viewModel.ThefourthTable = hotelDb.getImg(daye);
            viewModel.crntdate = DateTime.Today.Date;



            return View("~/Views/Admin/post.cshtml", viewModel);
        }
        public ActionResult doPostAccept(int id)
        {
            admin hotelDb = new admin();
            hotelDb.doPostAccept(id);
            DateTime today = DateTime.Today;
            DateTime daye = today.AddDays(-14).Date;
            Debug.WriteLine(daye);
            MyViewModel viewModel = new MyViewModel();
            viewModel.TheSecondTable = hotelDb.getPostEvents(daye);
            viewModel.TheFirstTable = hotelDb.getPostPosts(daye);
            viewModel.TheThirdTable = hotelDb.getAlbumName(daye);
            viewModel.ThefourthTable = hotelDb.getImg(daye);
            viewModel.crntdate = DateTime.Today.Date;



            return View("~/Views/Admin/post.cshtml", viewModel);
        }
        public ActionResult doEventDelete(int id)
        {
            admin hotelDb = new admin();
            hotelDb.doEventDelete(id);
            DateTime today = DateTime.Today;
            DateTime daye = today.AddDays(-14).Date;
            Debug.WriteLine(daye);
            MyViewModel viewModel = new MyViewModel();
            viewModel.TheSecondTable = hotelDb.getPostEvents(daye);
            viewModel.TheFirstTable = hotelDb.getPostPosts(daye);
            viewModel.TheThirdTable = hotelDb.getAlbumName(daye);
            viewModel.ThefourthTable = hotelDb.getImg(daye);
            viewModel.crntdate = DateTime.Today.Date;



            return View("~/Views/Admin/post.cshtml", viewModel);
        }
        public ActionResult doEventRejects(int id)
        {
            admin hotelDb = new admin();
            hotelDb.doEventRejects(id);
            DateTime today = DateTime.Today;
            DateTime daye = today.AddDays(-14).Date;
            Debug.WriteLine(daye);
            MyViewModel viewModel = new MyViewModel();
            viewModel.TheSecondTable = hotelDb.getPostEvents(daye);
            viewModel.TheFirstTable = hotelDb.getPostPosts(daye);
            viewModel.TheThirdTable = hotelDb.getAlbumName(daye);
            viewModel.ThefourthTable = hotelDb.getImg(daye);
            viewModel.crntdate = DateTime.Today.Date;



            return View("~/Views/Admin/post.cshtml", viewModel);
        }
        public ActionResult doEventAccept(int id)
        {
            admin hotelDb = new admin();

            hotelDb.doEventAccept(id);
            DateTime today = DateTime.Today;
            DateTime daye = today.AddDays(-14).Date;
            Debug.WriteLine(daye);
            MyViewModel viewModel = new MyViewModel();
            viewModel.TheSecondTable = hotelDb.getPostEvents(daye);
            viewModel.TheFirstTable = hotelDb.getPostPosts(daye);
            viewModel.TheThirdTable = hotelDb.getAlbumName(daye);
            viewModel.ThefourthTable = hotelDb.getImg(daye);
            viewModel.crntdate = DateTime.Today.Date;


            return View("~/Views/Admin/post.cshtml", viewModel);
        }
        public ActionResult doAlbumDelete(int id)
        {
            admin hotelDb = new admin();
            hotelDb.doAlbumDelete(id);
            DateTime today = DateTime.Today;
            DateTime daye = today.AddDays(-14).Date;
            Debug.WriteLine(daye);
            MyViewModel viewModel = new MyViewModel();
            viewModel.TheSecondTable = hotelDb.getPostEvents(daye);
            viewModel.TheFirstTable = hotelDb.getPostPosts(daye);
            viewModel.TheThirdTable = hotelDb.getAlbumName(daye);
            viewModel.ThefourthTable = hotelDb.getImg(daye);
            viewModel.crntdate = DateTime.Today.Date;



            return View("~/Views/Admin/post.cshtml", viewModel);
        }
        public ActionResult doAlbumRejects(int id)
        {
            admin hotelDb = new admin();
            hotelDb.doAlbumRejects(id);
            DateTime today = DateTime.Today;
            DateTime daye = today.AddDays(-14).Date;
            Debug.WriteLine(daye);
            MyViewModel viewModel = new MyViewModel();
            viewModel.TheSecondTable = hotelDb.getPostEvents(daye);
            viewModel.TheFirstTable = hotelDb.getPostPosts(daye);
            viewModel.TheThirdTable = hotelDb.getAlbumName(daye);
            viewModel.ThefourthTable = hotelDb.getImg(daye);
            viewModel.crntdate = DateTime.Today.Date;



            return View("~/Views/Admin/post.cshtml", viewModel);
        }
        public ActionResult doAlbumAccept(int id)
        {
            admin hotelDb = new admin();

            hotelDb.doAlbumAccept(id);
            DateTime today = DateTime.Today;
            DateTime daye = today.AddDays(-14).Date;
            Debug.WriteLine(daye);
            MyViewModel viewModel = new MyViewModel();
            viewModel.TheSecondTable = hotelDb.getPostEvents(daye);
            viewModel.TheFirstTable = hotelDb.getPostPosts(daye);
            viewModel.TheThirdTable = hotelDb.getAlbumName(daye);
            viewModel.ThefourthTable = hotelDb.getImg(daye);
            viewModel.crntdate = DateTime.Today.Date;


            return View("~/Views/Admin/post.cshtml", viewModel);
        }

    }
}
