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
namespace HotelBook.Controllers
{
    
    public class LoginController : Controller
    {
        
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        //POST: Login
        [HttpPost]
        public ActionResult index(Customer customer)
        {
            String password = customer.password;
            String email = customer.email;
            
            HotelDBContext hotelDb = new HotelDBContext();
            customer = hotelDb.Search(email);
            String pass =customer.password;
            
            char[] a=pass.ToCharArray();
            String passw = pass.Replace(" ","");
            
            Debug.WriteLine(password);
            Debug.WriteLine(passw + passw.Length);
            ValidationController validation = new ValidationController();
            if (validation.IsValidEmail(email))
            {




                if (passw == password)
                {
                    Customer customer1 = new Customer();

                        Session["Email"] = email;
                        Debug.WriteLine("enter=" + pass);
                        ProfileController profile = new ProfileController();
                        customerProfile cusprofile = new customerProfile();
                        customer.email = email;
                        cusprofile.customer = customer;
                        PostDetails postdetails = new PostDetails();
                        cusprofile.posts = postdetails.getPosts(email);
                        Debug.WriteLine(cusprofile.posts.Count);
                        Debug.WriteLine("image=="+cusprofile.customer.image);
                    return View("~/Views/Profile/post.cshtml", cusprofile);
                   
                    
                }
                else
                {
                    customer.email = "";
                    customer.password = "Invald Password";
                    return View(customer);
                }
            }else
            {
                customer.email = "Invalid Email";
                customer.password = "";
                return View(customer);
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
            HotelDBContext hotelDb = new HotelDBContext();
            hotelDb.Insert(customer);
            return View();
        }

        public ActionResult LogOut()
        {
            Session["Email"] = null;
            return View("~/Views/Home/index.cshtml");
        }
    }
}