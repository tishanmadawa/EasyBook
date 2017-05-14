using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using HotelBook.Models;

using Microsoft;

using System.Diagnostics;
using GoogleMaps.LocationServices;

namespace HotelBook.Controllers
{
    
    public class LoginController : Controller
    {
        
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(Customer customer)
        {
            String password = customer.password;
            String email = customer.email;

            HotelDBContext hotelDb = new HotelDBContext();

            ValidationController validation = new ValidationController();
            if (validation.IsValidEmail(email))
            {
                customer = hotelDb.Search(email);
                String pass = customer.password;
                if (pass == null)
                {
                    ModelState.AddModelError("email", "Please signup");
                    return View();
                }
                else
                {
                    string role = customer.role;
                    char[] a = pass.ToCharArray();
                    String passw = pass.Replace(" ", "");
                    char[] b = role.ToCharArray();
                    String rolee = role.Replace(" ", "");
                    Debug.WriteLine(password);
                    Debug.WriteLine(passw + passw.Length);


                    if (passw == password)
                    {
                        if (customer.accept == 1)
                        {
                            Debug.WriteLine(customer.role);
                            if (rolee == "admin")
                            {
                                Debug.WriteLine("role=" + customer.role);
                                customerProfile cusprofile = new customerProfile();
                                cusprofile.customer = customer;
                                Session["Email"] = email;
                                return View("~/Views/Admin/Index.cshtml", cusprofile);
                            }
                            else
                            {
                                Customer customer1 = new Customer();

                                Session["Email"] = email;
                                Debug.WriteLine("enter=" + pass);
                                ProfileController profile = new ProfileController();
                                customerProfile cusprofile = new customerProfile();
                                customer.email = email;
                                cusprofile.searchpackage = new List<Package>();
                                cusprofile.customer = customer;
                                PostDetails postdetails = new PostDetails();
                                cusprofile.posts = postdetails.getPosts(email);
                                Debug.WriteLine(cusprofile.posts.Count);
                                Debug.WriteLine("image==" + cusprofile.customer.image);
                                return View("~/Views/Profile/AddPro.cshtml", customer);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("email", "Wait for accept the signup");
                            return View();
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("password", "Invalid password");
                        return View();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("email", "Please enter Correct Email");
                return View();
            }


        }

        //POST: Login
        [HttpPost]
        public ActionResult index(Customer customer)
        {
            String password = customer.password;
            String email = customer.email;
            
            HotelDBContext hotelDb = new HotelDBContext();
            
            ValidationController validation = new ValidationController();
            if (validation.IsValidEmail(email))
            {
                customer = hotelDb.SearchLogin(email);
                String pass = customer.password;
                user user = new user();
                user = hotelDb.SearchLoginUser(email);
                string pass1 = user.password;
                if (pass == null && pass1 == null)
                {
                    ModelState.AddModelError("email", "Please signup");
                    return View();
                } else if (pass1 != null)
                {
                    string passwor = pass1.Replace(" ","");
                    if (passwor == password)
                    {
                        Session["user"] = user.email;
                        return View("~/Views/Home/start.cshtml");
                    }else
                    {
                        ModelState.AddModelError("email", "Wait for accept the signup");
                        return View();
                    }
                }
                else if(pass != null)
                {
                    string role = customer.role;
                    char[] a = pass.ToCharArray();
                    String passw = pass.Replace(" ", "");
                    char[] b = role.ToCharArray();
                    String rolee = role.Replace(" ", "");
                    Debug.WriteLine(password);
                    Debug.WriteLine(passw + passw.Length);


                    if (passw == password)
                    {
                        if (customer.accept==1) {
                            Debug.WriteLine(customer.role);
                            if (rolee == "admin")
                            {
                                Debug.WriteLine("role="+customer.role);
                                customerProfile cusprofile = new customerProfile();
                                cusprofile.customer = customer;
                                Session["Email"] = email;
                                return View("~/Views/Admin/Index.cshtml", cusprofile);
                            }
                            else
                            {
                                Customer customer1 = new Customer();

                                Session["Email"] = email;
                                Debug.WriteLine("enter=" + pass);
                                ProfileController profile = new ProfileController();
                                customerProfile cusprofile = new customerProfile();
                                customer.email = email;
                                cusprofile.searchpackage = new List<Package>();
                                cusprofile.customer = customer;
                                PostDetails postdetails = new PostDetails();
                                cusprofile.posts = postdetails.getPosts(email);
                                Debug.WriteLine(cusprofile.posts.Count);
                                Debug.WriteLine("image==" + cusprofile.customer.image);
                                return View("~/Views/Profile/post.cshtml", cusprofile);
                            }
                        }else
                        {
                            ModelState.AddModelError("email", "Wait for accept the signup");
                            return View();
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("password", "Invalid password");
                        return View();
                    }
                }else
                {
                    ModelState.AddModelError("password", "Invalid password");
                    return View();
                }
            }else
            {
                ModelState.AddModelError("email", "Please enter Correct Email");
                return View();
            }
            

        }

        [HttpPost]
        public ActionResult bookingLogin(Customer customer,string emails)
        {
            String password = customer.password;
            String email = customer.email;

            HotelDBContext hotelDb = new HotelDBContext();

            ValidationController validation = new ValidationController();
            if (validation.IsValidEmail(email))
            {
                customer = hotelDb.SearchLogin(email);
                String pass = customer.password;
                user user = new user();
                user = hotelDb.SearchLoginUser(email);
                string pass1 = user.password;
                if (pass == null && pass1 == null)
                {
                    ModelState.AddModelError("email", "Please signup");
                    return View();
                }
                else if (pass1 != null)
                {
                    string passwor = pass1.Replace(" ", "");
                    if (passwor == password)
                    {
                        Session["user"] = user.email;
                        customerProfile customerpro = new customerProfile();
                        customerpro.customer = hotelDb.Search(emails);
                        customerpro.customer = hotelDb.Search(emails);
                        customerpro.searchpackage = new List<Models.Package>();
                        customerpro.range = hotelDb.Viewpackage(emails);
                        PostDetails postdetails = new PostDetails();
                        
                        
                        
                        return View("~/Views/Profile/payment.cshtml", customerpro);
                    }
                    else
                    {
                        ModelState.AddModelError("email", "Wait for accept the signup");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("password", "Invalid password");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("email", "Please enter Correct Email");
                return View();
            }


        }
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(Customer customer)
        {
            var address = "Stavanger, Norway";

            var locationService = new GoogleLocationService();
            var point = locationService.GetLatLongFromAddress(address);

            var latitude = point.Latitude;
            var longitude = point.Longitude;
            HotelDBContext hotelDb = new HotelDBContext();
            customer.map = "" + longitude + "," + latitude;
            hotelDb.Insert(customer);
            return View();
        }

        [HttpPost]
        public ActionResult userSignUp(Customer customer)
        {
            HotelDBContext hotelDb = new HotelDBContext();
            ValidationController validation = new ValidationController();
            if (validation.IsValidEmail(customer.email))
            {
                user User = new user();
                User.email = customer.email;
                User.name = customer.name;
                User.address = customer.address;
                User.password = customer.password;
                hotelDb.InsertUser(User);
               
                return View("~/Views/Home/start.cshtml");
            }
            else
            {
                ModelState.AddModelError("email", "Please Enter valid email");
                return View("~/Views/Login/signUp.cshtml",customer);
            }
           
        }


        [HttpPost]
        public ActionResult AddPro(Customer customer,HttpPostedFileBase fileupload,string email)
        {
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
          
            hotelDb.upSetting(customer,customer.email);
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
          
                return View("~/Views/profile/post.cshtml", cusprofile);
                
        }

        public ActionResult LogOut()
        {
            Session["Email"] = null;
            Session["user"] = null;
            return View("~/Views/Home/start.cshtml");
        }
    }
}