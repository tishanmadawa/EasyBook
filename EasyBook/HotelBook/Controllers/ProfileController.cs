using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelBook.Models;
using System.Diagnostics;
using System.IO;

namespace HotelBook.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index(customerProfile customer)
        {
            return View(customer);
        }
        [HttpPost]
        public ActionResult addPic(customerProfile customer)
        {
            Debug.WriteLine(customer.number);
            HotelDBContext hotel = new HotelDBContext();
            customer.customer = hotel.Search(Session["Email"].ToString());
            return View("~/Views/Profile/photo.cshtml",customer);
        }
        public ActionResult cover(Customer customer, HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    var fileName = System.IO.Path.GetFileName(file.FileName);
                    var path = System.IO.Path.Combine(Server.MapPath("~/image/"), fileName);
                    file.SaveAs(path);
                    Debug.WriteLine(Session["Email"]);

                    customer.image = fileName;
                    Debug.WriteLine(customer.email);
                    HotelDBContext hotel = new HotelDBContext();
                    hotel.addImage(customer);

                }
            }
            catch
            {
                ViewBag.Message = "Upload failed";
                return RedirectToAction("Uploads");
            }
            return View("~/Views/Profile/index.cshtml", customer);
        }

        public ActionResult post(Customer customer,string email)
        {
            HotelDBContext hotel = new HotelDBContext();
            
            ProfileController profile = new ProfileController();
            customerProfile cusprofile = new customerProfile();
            cusprofile.customer = hotel.Search(email);
            Debug.WriteLine("email" + customer.email);
            
            PostDetails postdetails = new PostDetails();
            List<Post> postlist = postdetails.getPosts(customer.email);
            postlist.Reverse();
            cusprofile.posts = postlist;
            Debug.WriteLine(cusprofile.posts.Count);
            return View(cusprofile);
        }

        [HttpPost]
        public ActionResult addPost(customerProfile customerpro)
        {
            Customer customer = customerpro.customer;
            Post post = customerpro.post;
            post.customerId = customer.email;
            Debug.WriteLine("emailll" + customer.email);
            Debug.WriteLine(post.post);
            Debug.WriteLine(post.title);
            DateTime today = DateTime.Now;
            Debug.WriteLine("today" + today);
            post.date = today;
            customerpro.customer = customer;
            customerpro.post = post;

            PostDetails postdetails = new PostDetails();
            postdetails.Insert(post);
            List<Post> array = postdetails.getPosts(customer.email);
            array.Reverse();
            customerpro.post = null;
            customerpro.posts = array;
            ModelState.Clear();
            //return View("~/Views/Profile/post.cshtml", customerpro);
            return RedirectToAction("post", "Profile", customerpro);

        }

        
        public ActionResult deletePost(int blogPostId, String customerprofile)
        {
            
            customerProfile customerpro = new customerProfile();
            HotelDBContext hotel = new HotelDBContext();
            Customer customer = hotel.Search(customerprofile);
           
            PostDetails postdetails = new PostDetails();
            postdetails.deletepost(blogPostId);
            Debug.WriteLine("ayyo" + customerprofile);
           
            
            return RedirectToAction("post", "Profile", customer);
        }
        public ActionResult photo(customerProfile customer,string email)
        {
            HotelDBContext hotel = new HotelDBContext();
            Customer cus = hotel.Search(email);
            customer.customer = cus;
            return View("~/Views/Profile/album.cshtml", customer);
        }

        public ActionResult album(customerProfile customer, string email)
        {
            HotelDBContext hotel = new HotelDBContext();
            Customer cus = hotel.Search(email);
            customer.customer = cus;
            return View("~/Views/Profile/photo.cshtml", customer);
        }
        [HttpGet]
        public ActionResult settings(customerProfile customer, string email)
        {
            HotelDBContext hotel = new HotelDBContext();
            Customer cus = hotel.Search(email);
            customer.customer = cus;
            return View( customer);
        }
        [HttpPost]
        public ActionResult settings(Customer customer)
        {
            string ss = Session["email"].ToString();
            string base64 = Request.Form["imgCropped"];
            byte[] bytes = Convert.FromBase64String(base64.Split(',')[1]);
            using (FileStream stream = new FileStream(Server.MapPath("~/image/" + customer.ProfileName + ".jpg"), FileMode.Create))
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
            }
            customer.image = customer.ProfileName + ".jpg";
            HotelDBContext hotelDb = new HotelDBContext();
            hotelDb.upsetting(customer, ss);
            return View("~/Views/Profile/settings.cshtml");
        }
        [HttpPost]
        public ActionResult addAlbum(IEnumerable<HttpPostedFileBase> fileupload, customerProfile customerpro)
        {

            Debug.WriteLine("images" + fileupload.Count());

            for (int i = 0; i < fileupload.Count(); i++)
            {
                HttpPostedFileBase file = fileupload.ElementAt(i);
                try
                {
                    if (file.ContentLength > 0)
                    {
                        var fileName = System.IO.Path.GetFileName(file.FileName);
                        var path = System.IO.Path.Combine(Server.MapPath("~/image/"), fileName);
                        file.SaveAs(path);
                        Debug.WriteLine(Session["Email"]);

                        album newAlbum = new album();
                        newAlbum.albumName = customerpro.image.albumName;
                        Debug.WriteLine("addAlbum1");
                        newAlbum.image = fileName;
                        Debug.WriteLine("addAlbum2");
                        newAlbum.number = customerpro.number;
                        Debug.WriteLine("addAlbum3");
                        newAlbum.owner = Session["Email"].ToString();

                        Debug.WriteLine("addAlbum4");
                        HotelDBContext hotel = new HotelDBContext();
                        
                        hotel.addAlbum(newAlbum);

                    }
                }
                catch
                {
                    ViewBag.Message = "Upload failed";
                    
                }

            }

            return View("~/Views/Profile/photo.cshtml", customerpro);
        }

        public ActionResult viewEvents( string email)
        {
            customerProfile customer = new customerProfile();
            HotelDBContext hotel = new HotelDBContext();
            Customer cus = hotel.Search(email);
            customer.customer = cus;
            customer.allEvents = hotel.getEvents(email);
            Debug.WriteLine("image=" + customer.customer.image);
            return View(customer);
        }
         public ActionResult deleteEvents(int blogPostId, string customerprofile)
        {
            customerProfile customer = new customerProfile();
            HotelDBContext hotel = new HotelDBContext();
            hotel.deleteEvent(blogPostId);
            Customer cus = hotel.Search(customerprofile);
            customer.customer = cus;
            customer.allEvents = hotel.getEvents(customerprofile);
            Debug.WriteLine("image=" + customer.customer.image);

            return View("~/Views/Profile/viewEvents.cshtml", customer);
        }

        public ActionResult events(customerProfile customer, string email)
        {
            HotelDBContext hotel = new HotelDBContext();
            Customer cus = hotel.Search(email);
            customer.customer = cus;
            Debug.WriteLine("image=" + customer.customer.image);
            return View(customer);
        }

        [HttpPost]
        public ActionResult events(customerProfile customer)
        {
            events newEvent = customer.events;
            Debug.WriteLine(newEvent.accomadation);
            Debug.WriteLine(newEvent.date);
            Debug.WriteLine(newEvent.description);
            Debug.WriteLine(newEvent.endTime);
            Debug.WriteLine(newEvent.location);
            Debug.WriteLine(newEvent.meant);
            Debug.WriteLine(newEvent.noDates);
            Debug.WriteLine(newEvent.startTime);
            Debug.WriteLine(Session["Email"].ToString());
            newEvent.user = Session["Email"].ToString();
            HotelDBContext hotel = new HotelDBContext();
            Customer cus = hotel.Search(Session["Email"].ToString());
            customer.customer = cus;
            Debug.WriteLine("image=" + customer.customer.image);
            hotel.addEvent(newEvent);
            
            return View(customer);
        }
        public ActionResult Package()
        {
            HotelDBContext hotelDb = new HotelDBContext();
            List<Package> range = hotelDb.Viewpackage();
            return View(range);
        }
        [HttpGet]
        public ActionResult AddPackage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPackage(Package pack)
        {
            HotelDBContext hotelDb = new HotelDBContext();
            hotelDb.InsertPack(pack);
            return View("~/Views/Profile/Package.cshtml");
        }
        [HttpGet]
        public ActionResult EditPackage(int id)
        {
            HotelDBContext hotelDb = new HotelDBContext();
            Package pack = hotelDb.PackView(id);
            Debug.WriteLine("aaaaa" + pack.id);
            pack.id = id;
            return View(pack);
        }

        [HttpPost]
        public ActionResult EditPack(Package pac)
        {
            Debug.WriteLine(pac.id);
            HotelDBContext hotelDb = new HotelDBContext();
            hotelDb.EditPack(pac);

            return View("~/Views/Profile/Package.cshtml");
        }
        public ActionResult DeletePackage(int id)
        {
            HotelDBContext hotelDb = new HotelDBContext();
            hotelDb.DeletePackage(id);
            return View("~/Views/Profile/Package.cshtml");
        }

    }
}