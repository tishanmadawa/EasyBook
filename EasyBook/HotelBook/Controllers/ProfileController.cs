using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelBook.Models;
using System.Diagnostics;
using System.IO;
using GoogleMaps.LocationServices;

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
            customer.searchpackage = new List<Models.Package>();
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
            if (email==null)
            {
                return View("~/Views/Login/index.cshtml");

            }
            else
            {
                HotelDBContext hotel = new HotelDBContext();

                ProfileController profile = new ProfileController();
                customerProfile cusprofile = new customerProfile();
                cusprofile.customer = hotel.Search(email);
                Debug.WriteLine("email" + customer.email);
                cusprofile.searchpackage = new List<Models.Package>();

                PostDetails postdetails = new PostDetails();
                List<Post> postlist = postdetails.getPosts(customer.email);
                postlist.Reverse();
                cusprofile.posts = postlist;
                Debug.WriteLine("Role="+cusprofile.customer.role);

                String rolee = cusprofile.customer.role.Replace(" ", "");
                if (rolee == "admin")
                {
                    return View("~/Views/Admin/Index.cshtml");
                }else
                {
                    return View(cusprofile);
                }
                
            }
            
        }
        public ActionResult payment(customerProfile customerpro, string email)
        {
            HotelDBContext hotel = new HotelDBContext();
            customerpro.customer = hotel.Search(email);
            customerpro.searchpackage = new List<Models.Package>();
            customerpro.range = hotel.Viewpackage(email);
            PostDetails postdetails = new PostDetails();
            
            if (Session["user"]==null)
            {
                customerpro = null;
                Session["hotel"] = email;
                return View("~/Views/Login/bookingLogin.cshtml",customerpro);
            }
            else
            {
                return View(customerpro);
            }
            
        }
        [HttpPost]
        public ActionResult payments(customerProfile customerpro ,string mail)
        {
            HotelDBContext hotel = new HotelDBContext();
            customerpro.customer = hotel.Search(mail);
            customerpro.searchpackage = new List<Models.Package>();
            customerpro.range = hotel.Viewpackage(mail);
            PostDetails postdetails = new PostDetails();
            customerpro.payment.userId = 1;
            hotel.insertPayment(customerpro.payment);
            customerpro.payment =new Payment();
            customerpro.posts = postdetails.getPosts(mail);
            
            return View("~/Views/Profile/post.cshtml", customerpro);
        }


        [HttpPost]
        public ActionResult addPost(customerProfile customerpro)
        {
            Customer customer = customerpro.customer;
            Post post = customerpro.post;
            post.customerId = customer.email;
            string cusemail = customer.email;
            Debug.WriteLine("emailll" + customer.email);
            Debug.WriteLine(post.post);
            customerpro.searchpackage = new List<Models.Package>();

            Debug.WriteLine(post.title);
            DateTime today = DateTime.Now;
            Debug.WriteLine("today" + today);
            post.date = today;
            customerpro.customer = customer;
            customerpro.post = post;

            PostDetails postdetails = new PostDetails();
            postdetails.Insert(post);
            List<Post> array = postdetails.getPosts(cusemail);
            Debug.WriteLine("emaill111l" + cusemail);
            array.Reverse();
            customerpro.post = null;
            customerpro.posts = array;
            ModelState.Clear();
            //return View("~/Views/Profile/post.cshtml", customerpro);
            return View("~/Views/Profile/post.cshtml", customerpro);

        }

        
        public ActionResult deletePost(int blogPostId, String customerprofile)
        {
            
            customerProfile customerpro = new customerProfile();
            HotelDBContext hotel = new HotelDBContext();
            Customer customer = hotel.Search(customerprofile);
            customerpro.searchpackage = new List<Models.Package>();

            PostDetails postdetails = new PostDetails();
            postdetails.deletepost(blogPostId);
            Debug.WriteLine("ayyo" + customerprofile);
           
            
            return RedirectToAction("post", "Profile", customer);
        }
        public ActionResult editPost(int blogPostId, String customerprofile)
        {

            customerProfile customerpro = new customerProfile();
            HotelDBContext hotel = new HotelDBContext();
            Customer customer = hotel.Search(customerprofile);
            customerpro.searchpackage = new List<Models.Package>();
            PostDetails postdetails = new PostDetails();
            customerpro.posts = postdetails.getPosts(customerprofile);
            customerpro.customer = customer;
            customerpro.post = postdetails.getPost(blogPostId);
            return View("~/Views/Profile/post.cshtml", customerpro);
        }

        public ActionResult editPostSingle(customerProfile customerpro)
        {

            HotelDBContext hotel = new HotelDBContext();
            Customer customer = hotel.Search(Session["Email"].ToString());
            customerpro.searchpackage = new List<Models.Package>();
            Debug.WriteLine("new" + customerpro.post.title);
            Debug.WriteLine("id" + customerpro.post.id);
            PostDetails postdetails = new PostDetails();
            postdetails.editpost(customerpro.post);


            return RedirectToAction("post", "Profile", customer);
        }
        public ActionResult photo(customerProfile customer, string email)
        {
            HotelDBContext hotel = new HotelDBContext();
            Customer cus = hotel.Search(email);
            customer.customer = cus;
            PostDetails postD = new PostDetails();
            customer.albums = postD.getAlbums(email);
            customer.searchpackage = new List<Models.Package>();

            return View("~/Views/Profile/album.cshtml", customer);
        }

        public ActionResult album(customerProfile customer, string email)
        {
            HotelDBContext hotel = new HotelDBContext();
            Customer cus = hotel.Search(email);
            customer.customer = cus;
            customer.searchpackage = new List<Models.Package>();

            return View("~/Views/Profile/photo.cshtml", customer);
        }

        public ActionResult searchTable(customerProfile customer, string email)
        {
            if (email != null)
            {
                HotelDBContext hotel = new HotelDBContext();
                Customer cus = hotel.Search(email);
                Daypack pack = new Daypack();
                customer.dayPack = pack;
                customer.searchpackage = hotel.Viewpackage(email);
                customer.customer = cus;

                return View(customer);
            }
            else
            {
                return View("~/Views/Login/Index.cshtml");
            }
        }

        [HttpPost]
        public ActionResult addAlbum(IEnumerable<HttpPostedFileBase> fileupload, customerProfile customerpro)
        {
            HotelDBContext hotel = new HotelDBContext();
            String image = fileupload.ElementAt(0).FileName;
            hotel.addAlbum(Session["Email"].ToString(), customerpro.image.albumName, customerpro.number, image);
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
                        customerpro.searchpackage = new List<Package>();
                        Debug.WriteLine("addAlbum4");


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
        [HttpGet]
        public ActionResult albumView(string id, string email, int number)
        {
            customerProfile customer = new customerProfile();
            HotelDBContext hotel = new HotelDBContext();
            Customer cus = hotel.Search(email);
            PostDetails postD = new PostDetails();
            customer.customer = cus;
            customer.albumImage = postD.getImages(id);
            customer.searchpackage = new List<Models.Package>();

            return View(customer);
        }
        [HttpGet]
        public ActionResult settings(customerProfile customer, string email)
        {
            HotelDBContext hotel = new HotelDBContext();
            Customer cus = hotel.Search(email);
            customer.customer = cus;
            customer.searchpackage = new List<Models.Package>();

            return View( customer);
        }

        [HttpPost]
        public ActionResult upsettings(customerProfile customer1, HttpPostedFileBase fileupload, string email)
        {
            Customer customer = customer1.customer;
            var address = customer.name;
            Debug.WriteLine(fileupload.FileName);
            customer.image = fileupload.FileName;
            var fileName = System.IO.Path.GetFileName(fileupload.FileName);
            var path = System.IO.Path.Combine(Server.MapPath("~/image/"), fileName);
            fileupload.SaveAs(path);
            var locationService = new GoogleLocationService();
            var point = locationService.GetLatLongFromAddress(address);

            var latitude = point.Latitude;
            var longitude = point.Longitude;
            HotelDBContext hotelDb = new HotelDBContext();

            hotelDb.upSetting(customer, customer.email);
            HotelDBContext hotel = new HotelDBContext();

            ProfileController profile = new ProfileController();
            customerProfile cusprofile = new customerProfile();
            cusprofile.customer = hotel.Search(email);
            Debug.WriteLine("email" + customer.email);
            cusprofile.searchpackage = new List<Models.Package>();

            PostDetails postdetails = new PostDetails();
            List<Post> postlist = postdetails.getPosts(email);
            postlist.Reverse();
            cusprofile.posts = postlist;
            Debug.WriteLine("Role=" + cusprofile.customer.role);

            String rolee = cusprofile.customer.role.Replace(" ", "");

            return View("~/Views/profile/settings.cshtml", cusprofile);

        }
        
        [HttpPost]
        public ActionResult todayBooking(customerProfile customer,int id)
        {
            Debug.WriteLine("pack=" + id);
            Debug.WriteLine("pack=" + customer.dayPack.date);
            Debug.WriteLine("pack=" + customer.dayPack.number);
            Debug.WriteLine("pack=" + Session["Email"].ToString());
            string email = Session["Email"].ToString();
            customer.dayPack.PackId = id;
            HotelDBContext hotel = new HotelDBContext();
            Daypack old = hotel.viewTodayBooking(customer.dayPack);
            if (old.id==0) {
                hotel.addTodayBooking(customer.dayPack);
            }else
            {
                old.number = old.number + customer.dayPack.number;
                hotel.updateTodayBooking(old);
            }
            
            Customer cus = hotel.Search(email);
            customer.searchpackage = hotel.Viewpackage(email);
            customer.customer = cus;
            customer.dayPack = null;

            return View("~/Views/Profile/searchTable.cshtml", customer);

        }

        public ActionResult viewEvents( string email)
        {
            customerProfile customer = new customerProfile();
            HotelDBContext hotel = new HotelDBContext();
            Customer cus = hotel.Search(email);
            customer.customer = cus;
            customer.allEvents = hotel.getEvents(email);
            Debug.WriteLine("image=" + customer.customer.image);
            customer.searchpackage = new List<Models.Package>();

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
            customer.searchpackage = new List<Models.Package>();

            return View("~/Views/Profile/viewEvents.cshtml", customer);
        }

        public ActionResult editEvent(int blogPostId, string customerprofile)
        {
            customerProfile customer = new customerProfile();
            HotelDBContext hotel = new HotelDBContext();
            Customer cus = hotel.Search(customerprofile);
            customer.customer = cus;
            customer.allEvents = hotel.getEvents(customerprofile);
            customer.events = hotel.geteEvent(blogPostId);
            customer.searchpackage = new List<Models.Package>();
            return View(customer);
        }
        [HttpPost]
        public ActionResult editEvent(customerProfile customer,int id)
        {
            
            HotelDBContext hotel = new HotelDBContext();
            
            Customer cus = hotel.Search(Session["Email"].ToString());
            customer.customer = cus;
            customer.searchpackage = new List<Models.Package>();
            PostDetails post = new PostDetails();
            customer.events.id = id;
            Debug.WriteLine("image=" + customer.events.id);
            hotel.editEvent(customer.events);
            customer.allEvents = hotel.getEvents(customer.customer.email);
            return View(customer);
        }
        public ActionResult events(customerProfile customer, string email)
        {
            HotelDBContext hotel = new HotelDBContext();
            Customer cus = hotel.Search(email);
            customer.customer = cus;
            Debug.WriteLine("image=" + customer.customer.image);
            customer.searchpackage = new List<Models.Package>();

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
            customer.searchpackage = new List<Models.Package>();

            return View(customer);
        }
        public ActionResult Package(customerProfile customer, string email)
        {
            HotelDBContext hotelDb = new HotelDBContext();
            customer.range = hotelDb.Viewpackage(email);
            customer.searchpackage = new List<Models.Package>();

            HotelDBContext hotel = new HotelDBContext();
            Customer cus = hotel.Search(email);
            Debug.WriteLine("imageemail=" + email);
            customer.customer = cus;

            return View(customer);
        }
        
        public ActionResult AddPackage(customerProfile customer, string email)
        {
            if (email != null)
            {
                HotelDBContext hotelDb = new HotelDBContext();

                customer.range = hotelDb.Viewpackage(email);
                HotelDBContext hotel = new HotelDBContext();
                Customer cus = hotel.Search(email);
                customer.customer = cus;
                customer.package = new Package();
                customer.searchpackage = new List<Models.Package>();

                return View(customer);
            }
            else
            {
                
                return View("~/Views/Login/Index.cshtml");
            }
        }

        [HttpPost]
        public ActionResult addPackage(customerProfile customer)
        {
            HotelDBContext hotelDb = new HotelDBContext();
            Package package = customer.package;
            Debug.WriteLine("aaaaa" + package.name);
            //customer.package.cusId = (string)Session["Email"].ToString();
            // hotelDb.InsertPack(customer.package);

            //customer.range = hotelDb.Viewpackage(Session["Email"].ToString());
            customer.searchpackage = new List<Models.Package>();

            return View("~/Views/Profile/Package.cshtml",customer);
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
        public ActionResult getPack(customerProfile customer,string email)
        {
            HotelDBContext hotelDb = new HotelDBContext();
            customer.searchpackage=hotelDb.seachPanel(customer.packagesDate);
            if (customer.searchpackage.Count==0)
            {
                customer.searchpackage = hotelDb.Viewpackage(email);
                    }
            customer.customer = hotelDb.Search(email);
            PostDetails post = new PostDetails();
            customer.posts = post.getPosts(email);

            return View("~/Views/Profile/post.cshtml",customer);
        }


    }
}