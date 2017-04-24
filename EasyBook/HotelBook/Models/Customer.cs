using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;
using System.Web.Helpers;

namespace HotelBook.Models
{
    public class Customer
    {   public string name { get; set; }
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string cPassword { get; set; }
        public int accept { get; set; }
        public string image { get; set; }
        public string rating { get; set; }
        public string ProfileName { get; set; }
        public string role { get; set; }
    }
    public class Daypack
    {
        public int id { get; set; }
        public int PackId { get; set; }
        public DateTime date { get; set; }
        public int number { get; set; }
    
    }
    public class events
    {
     
        public int id { get; set; }
        public DateTime date { get; set; }
        public string user { get; set; }
        [Required(ErrorMessage = "First name is required")]
        public int noDates { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string meant { get; set; }
        public string accomadation { get; set; }
        public string location { get; set; }
        public string description { get; set; }

       
    }
    public class Package
    {
        public int id { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string cusId { get; set; }
        public string availableNo { get; set; }
        public string number { get; set; }
    }
    public class MyAlbum
    {
        public int id { get; set; }
        public string user { get; set; }
        public string name { get; set; }
        public int number { get; set; }
        public string image { get; set; }
    }
    public class Search
    {
        public List<Customer> ranges { get; set; }
    }

    public class HotelDBContext 
    {
        //customer details
        //insert customer when signup
        public void Insert(Customer customer)
        {
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Customer (Name,Email, Password,State,City,Address) VALUES (@Name,@Email, @Password,@State,@City,@Address)"))
                {
                    cmd.Parameters.AddWithValue("@Name", customer.name);
                    cmd.Parameters.AddWithValue("@Email", customer.email);
                    cmd.Parameters.AddWithValue("@Password", customer.password);
                    cmd.Parameters.AddWithValue("@State", customer.state);
                    cmd.Parameters.AddWithValue("@City", customer.city);
                    cmd.Parameters.AddWithValue("@Address", customer.address);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        //search customer using customer email in Login
        public Customer Search(string email)
        {
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                Customer customer = new Models.Customer();
                SqlCommand com = new SqlCommand("SELECT * FROM Customer WHERE Email=@Email");
                com.CommandType = CommandType.Text;
                com.Connection = con;

                com.Parameters.AddWithValue("@Email", email);

                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Select().Length!= 0)
                    {
                        customer.password= Convert.ToString(dt.Rows[0]["Password"]);
                        customer.name= Convert.ToString(dt.Rows[0]["Name"]);
                        customer.email = Convert.ToString(dt.Rows[0]["Email"]);
                        customer.accept = Convert.ToInt32(dt.Rows[0]["Accept"]);
                        customer.image = Convert.ToString(dt.Rows[0]["Image"]);
                        customer.role= Convert.ToString(dt.Rows[0]["Role"]);
                        customer.address = Convert.ToString(dt.Rows[0]["Address"]);
                        customer.ProfileName= Convert.ToString(dt.Rows[0]["ProfileName"]);
                        customer.rating= Convert.ToString(dt.Rows[0]["Rating"]);
                        customer.state= Convert.ToString(dt.Rows[0]["State"]);
                        customer.city= Convert.ToString(dt.Rows[0]["City"]);
                        customer.id = Convert.ToInt32(dt.Rows[0]["Id"]);


                        return customer;
                    }else
                    {
                        return customer;
                    }
                   
                }
               
            }
        }
        //search customer using customer ID 
        public Customer SearchCustomer(int ID)
        {
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                Customer customer = new Models.Customer();
                SqlCommand com = new SqlCommand("SELECT * FROM Customer WHERE Id=@Id");
                com.CommandType = CommandType.Text;
                com.Connection = con;

                com.Parameters.AddWithValue("@Id", ID);

                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Select().Length != 0)
                    {
                        customer.password = Convert.ToString(dt.Rows[0]["Password"]);
                        customer.name = Convert.ToString(dt.Rows[0]["Name"]);
                        customer.email = Convert.ToString(dt.Rows[0]["Email"]);
                        customer.accept = Convert.ToInt32(dt.Rows[0]["Accept"]);
                        customer.image = Convert.ToString(dt.Rows[0]["Image"]);
                        customer.role = Convert.ToString(dt.Rows[0]["Role"]);

                        customer.address = Convert.ToString(dt.Rows[0]["Address"]);
                        customer.ProfileName = Convert.ToString(dt.Rows[0]["ProfileName"]);
                        customer.rating = Convert.ToString(dt.Rows[0]["Rating"]);
                        customer.state = Convert.ToString(dt.Rows[0]["State"]);
                        customer.city = Convert.ToString(dt.Rows[0]["City"]);
                        customer.id = Convert.ToInt32(dt.Rows[0]["Id"]);


                        return customer;
                    }
                    else
                    {
                        return customer;
                    }

                }

            }
        }
        public void addImage(Customer customer)
        {

            Debug.WriteLine(customer.email);
            Debug.WriteLine(customer.image);
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {

                using (SqlCommand cmd = new SqlCommand("UPDATE Customer SET Image=@image WHERE Email=@email"))
                {

                    cmd.Parameters.AddWithValue("@email", customer.email);
                    cmd.Parameters.AddWithValue("@image", customer.image);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public void addAlbum(string user, string album, int Number, string image)
        {
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Album (album,Customer,number,image) VALUES (@album,@Name,@Number,@image)"))
                {
                    cmd.Parameters.AddWithValue("@Name", user);
                    cmd.Parameters.AddWithValue("@album", album);
                    cmd.Parameters.AddWithValue("@Number", Number);
                    cmd.Parameters.AddWithValue("@image", image);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public List<Customer> searchCustomer(String name, String city)
        {
            String n = name;
            String c = city;
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;

            String sql = "SELECT * FROM Customer WHERE Name=@name or City=@city";

            var model = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", n);
                cmd.Parameters.AddWithValue("@city", c);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var customer = new Customer();
                    customer.name = rdr["Name"].ToString();
                    customer.address = rdr["Address"].ToString();
                    customer.email = rdr["Email"].ToString();
                    customer.state = rdr["State"].ToString();
                    customer.city = rdr["City"].ToString();
                    customer.image = rdr["Image"].ToString();
                    model.Add(customer);
                }
                return model;
            }
        }
        //added
        public void upsetting(Customer customer, string ee)
        {
            Debug.WriteLine(ee);
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {

                using (SqlCommand cmd = new SqlCommand("UPDATE Customer SET ProfileName=@proname,Rating=@rating,City=@city,State=@state,Address=@address,Image=@image WHERE Email=@ee"))
                {
                    cmd.Parameters.AddWithValue("@ee", ee);
                    cmd.Parameters.AddWithValue("@proname", customer.ProfileName);
                    cmd.Parameters.AddWithValue("@rating", customer.rating);
                    cmd.Parameters.AddWithValue("@city", customer.city);
                    cmd.Parameters.AddWithValue("@state", customer.state);
                    cmd.Parameters.AddWithValue("@address", customer.address);
                    cmd.Parameters.AddWithValue("@image", customer.image);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public List<Customer> Viewcustomer()
        {
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;

            String sql = "SELECT * FROM Customer WHERE Accept=@true and Role IS NULL";

            var model = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@true", "False");
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var customer = new Customer();
                    customer.name = rdr["Name"].ToString();
                    customer.address = rdr["Address"].ToString();
                    customer.email = rdr["Email"].ToString();
                    customer.state = rdr["State"].ToString();
                    customer.city = rdr["City"].ToString();





                    model.Add(customer);
                }
                return model;
            }
        }
        public void set(string email)
        {

            string h = email;
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {

                using (SqlCommand cmd = new SqlCommand("UPDATE Customer SET Accept='True' WHERE Email=@email"))
                {

                    cmd.Parameters.AddWithValue("@email", h);

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

        }
        public void sendMail(string email)
        {
            string h = email;
            // ProcessStartInfo sInfo = new ProcessStartInfo("http://mysite.com/");
            //Uri ur = new Uri("http://localhost:60818/CreatePro/login");
            Uri ur = new Uri("http://mysite.com/");

            try
            {
                // Initialize WebMail helper
                WebMail.SmtpServer = "smtp-pulse.com";
                WebMail.SmtpPort = 2525;
                WebMail.UserName = "tishanml993@gmail.com";
                WebMail.Password = "K8coLRHWo7Jcj";
                WebMail.From = "tishanml993@gmail.com";

                // Send email
                WebMail.Send(to: "cs4shalika@gmail.com",
                    subject: "Confirmation of Request",
                    body: "We add your hotel to HotelBook.Use this to make ypur profile" + ur
                );

            }
            catch (Exception ex)
            {
                //errorMessage = ex.Message;
            }
        }
        public void setr(string email)
        {
            string h = email;
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {

                using (SqlCommand cmd = new SqlCommand("DELETE FROM Customer WHERE Email=@email"))
                {

                    cmd.Parameters.AddWithValue("@email", h);

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public List<Customer> Viewuser()
        {
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;

            String sql = "SELECT * FROM Customer WHERE Accept=@true and Role IS NULL";

            var model = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@true", "True");
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var customer = new Customer();
                    customer.name = rdr["Name"].ToString();
                    customer.address = rdr["Address"].ToString();
                    customer.email = rdr["Email"].ToString();
                    customer.state = rdr["State"].ToString();
                    customer.city = rdr["City"].ToString();
                    customer.image = rdr["Image"].ToString();




                    model.Add(customer);
                }
                return model;
            }
        }
        public Customer Viewsettings()
        {

            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;

            String sql = "SELECT Name,City,Address,Email,Password FROM Customer WHERE Role=@role";


            using (SqlConnection conn = new SqlConnection(constr))
            {

                Customer customer = new Customer();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@role", "admin");
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    customer.email = rdr["Email"].ToString();
                    customer.address = rdr["Address"].ToString();
                    customer.name = rdr["Name"].ToString();
                    customer.password = rdr["Password"].ToString();
                    customer.city = rdr["City"].ToString();

                }
                return customer;
            }
        }
        public void upseting(Customer customer)
        {
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {

                using (SqlCommand cmd = new SqlCommand("UPDATE Customer SET Name=@name,City=@city,Address=@address,Email=@email,Password=@pass WHERE Role=@role"))
                {

                    cmd.Parameters.AddWithValue("@name", customer.name);

                    cmd.Parameters.AddWithValue("@email", customer.email);
                    cmd.Parameters.AddWithValue("@city", customer.city);
                    cmd.Parameters.AddWithValue("@pass", customer.password);
                    cmd.Parameters.AddWithValue("@address", customer.address);
                    cmd.Parameters.AddWithValue("@role", "admin");
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public void addAlbum(album newAlbum)
        {
           
            
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Images(Image,AlbumName,Customer) VALUES (@Images,@Album,@Owner)",con))
                {
                    cmd.Parameters.AddWithValue("@Images", newAlbum.image);
                    cmd.Parameters.AddWithValue("@Album", newAlbum.albumName);
                    cmd.Parameters.AddWithValue("@Owner", newAlbum.owner);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            
        }
        public void addEvent(events newEvent)
        {
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Events (Date,NoDays,StartTime,EndTime,Meant,Accomadation,Location,Description,Customer) VALUES (@date,@noDays,@startTime,@endTime,@meant,@accomadation,@location,@description,@customer)", con))
                {
                    cmd.Parameters.AddWithValue("@date", newEvent.date);
                    cmd.Parameters.AddWithValue("@noDays", newEvent.noDates);
                    cmd.Parameters.AddWithValue("@startTime", newEvent.startTime);
                    cmd.Parameters.AddWithValue("@endTime", newEvent.endTime);
                    cmd.Parameters.AddWithValue("@meant", newEvent.meant);
                    cmd.Parameters.AddWithValue("@accomadation", newEvent.accomadation);
                    cmd.Parameters.AddWithValue("@location", newEvent.location);
                    cmd.Parameters.AddWithValue("@description", newEvent.description);
                    cmd.Parameters.AddWithValue("@customer", newEvent.user);
                    Debug.WriteLine("user====" + newEvent.user);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public void editEvent(events eve)
        {
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Events SET Date=@date,NoDays=@noDays,StartTime=@startTime,EndTime=@endTime,Meant=@meant,Accomadation=@accomadation,Location=@location,Description=@description WHERE Id=@id"))
                {
                    cmd.Parameters.AddWithValue("@id", eve.id);
                    cmd.Parameters.AddWithValue("@accomadation", eve.accomadation);
                    cmd.Parameters.AddWithValue("@date", eve.date);
                    cmd.Parameters.AddWithValue("@description", eve.description);
                    cmd.Parameters.AddWithValue("@endTime", eve.endTime);
                    cmd.Parameters.AddWithValue("@location", eve.location);
                    cmd.Parameters.AddWithValue("@meant", eve.meant);
                    cmd.Parameters.AddWithValue("@noDates", eve.noDates);
                    cmd.Parameters.AddWithValue("@startTime", eve.startTime);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public void addTodayBooking(Daypack dayPack)
        {
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO dayPac (PacId,Date,number) VALUES (@packId,@date,@number)", con))
                {
                    cmd.Parameters.AddWithValue("@date", dayPack.date);
                    cmd.Parameters.AddWithValue("@packId", dayPack.PackId);
                    cmd.Parameters.AddWithValue("@number", dayPack.number);                    
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public void updateTodayBooking(Daypack dayPack)
        {
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE dayPac SET number=@number WHERE id=@id", con))
                {
                    
                    cmd.Parameters.AddWithValue("@number", dayPack.number);
                    cmd.Parameters.AddWithValue("@id", dayPack.id);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public Daypack viewTodayBooking(Daypack dayPack)
        {
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;

            String sql = "SELECT * FROM dayPac WHERE(PacId=@packId AND Date=@date)";

            Daypack newPack = new Daypack();
            using (SqlConnection conn = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@date", dayPack.date);
                cmd.Parameters.AddWithValue("@packId", dayPack.PackId);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    newPack.id = (int)rdr["Id"];
                    newPack.PackId = (int)rdr["PacId"];
                    newPack.date = (DateTime)rdr["Date"];
                    newPack.number = (int)rdr["number"];

                }
                return newPack;
            }
        }

        public List<events> getEvents(String email)
        {

            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                Customer customer = new Models.Customer();
                SqlCommand com = new SqlCommand("SELECT * FROM Events WHERE Customer=@Email");
                com.CommandType = CommandType.Text;
                com.Connection = con;

                com.Parameters.AddWithValue("@Email", email);

                using (SqlDataReader reader = com.ExecuteReader())
                {
                    List<events> eventList = new List<events>();

                    Debug.WriteLine(eventList.Count);
                    while (reader.Read())
                    {
                        events single = new events();
                        single.id = (int)reader["Id"];
                        single.accomadation = reader["Accomadation"].ToString();
                        single.date = (DateTime)reader["Date"];
                        single.description = reader["Description"].ToString();
                        single.endTime = reader["EndTime"].ToString();
                        single.location = reader["Location"].ToString();
                        single.meant = reader["Meant"].ToString();
                        single.noDates =(int)reader["NoDays"];
                        single.startTime = reader["StartTime"].ToString();
                        single.user = reader["Customer"].ToString();

                        eventList.Add(single);
                    }
                    Debug.WriteLine(eventList.Count);
                    eventList.Reverse();
                    return eventList;


                }

            }

        }
        public void deleteEvent(int id)
        {
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Events WHERE Id=@id"))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public events geteEvent(int id)
        {

            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;

            String sql = "SELECT * FROM Events WHERE Id=@x";

            events eve = new events();
            using (SqlConnection conn = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@x", id);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    eve.id = id;
                    eve.accomadation = rdr["Accomadation"].ToString();
                    eve.date = (DateTime)rdr["Date"];
                    eve.description = rdr["Description"].ToString();
                    eve.endTime = rdr["EndTime"].ToString();
                    eve.location = rdr["Location"].ToString();
                    eve.meant = rdr["Meant"].ToString();
                    eve.noDates = Convert.ToInt32(rdr["NoDays"]);
                    eve.startTime = rdr["StartTime"].ToString();
                   
                }
                return eve;
            }
        }
      /*  public void upsetting(Customer customer, string ee)
        {
            Debug.WriteLine(ee);
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {

                using (SqlCommand cmd = new SqlCommand("UPDATE Customer SET ProfileName=@proname,Rating=@rating,City=@city,State=@state,Address=@address,Image=@image WHERE Email=@ee"))
                {
                    cmd.Parameters.AddWithValue("@ee", ee);
                    cmd.Parameters.AddWithValue("@proname", customer.ProfileName);
                    cmd.Parameters.AddWithValue("@rating", customer.rating);
                    cmd.Parameters.AddWithValue("@city", customer.city);
                    cmd.Parameters.AddWithValue("@state", customer.state);
                    cmd.Parameters.AddWithValue("@address", customer.address);
                    cmd.Parameters.AddWithValue("@image", customer.image);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }*/

        public List<Package> Viewpackage(string email)
        {

        string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            Debug.WriteLine(email);
            String sql = "SELECT * FROM Package WHERE CusId=@email";

        List<Package> mod = new List<Package>();
        using (SqlConnection conn = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@email", email);
                conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var package = new Package();
    package.id = Convert.ToInt32(rdr["Id"]);
                package.name = rdr["Name"].ToString();
                    Debug.WriteLine(package.name);

                    package.price = rdr["Price"].ToString();
    package.description = rdr["Description"].ToString();
    package.type = rdr["Type"].ToString();
    package.availableNo = rdr["AvailableNo"].ToString();

    mod.Add(package);
            }
            return mod;
        }
    }
        public List<Package> seachPanel(DateTime date)
        {

            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            Debug.WriteLine(date);
            String sql = "SELECT P.name AS name,P.availableNo AS availableNo ,P.price AS price,D.number AS number FROM Package P,dayPac D WHERE P.ID=D.PacId AND D.Date=@date";

            List<Package> mod = new List<Package>();
            using (SqlConnection conn = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@date", date);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var package = new Package();
                    

                    package.price = rdr["price"].ToString();
                    package.name = rdr["name"].ToString();
                    package.number = ((int)rdr["availableNo"] -(int)rdr["number"]).ToString();
                    package.availableNo = rdr["availableNo"].ToString();

                    mod.Add(package);
                }
                return mod;
            }
        }
        public void InsertPack(Package pack)
{
    string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
    using (SqlConnection con = new SqlConnection(constr))
    {
        using (SqlCommand cmd = new SqlCommand("INSERT INTO Package (Name,Price, Description,Type,CusId,AvailableNo) VALUES (@Name,@Price, @Description,@Type,@email,@Available)"))
        {
            cmd.Parameters.AddWithValue("@Name", pack.name);
            cmd.Parameters.AddWithValue("@Price", pack.price);
            cmd.Parameters.AddWithValue("@Description", pack.description);
            cmd.Parameters.AddWithValue("@Type", pack.type);
                    cmd.Parameters.AddWithValue("@email", pack.cusId);
                    cmd.Parameters.AddWithValue("@Available", pack.availableNo);

            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}

public Package PackView(int id)
{

    string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;

    String sql = "SELECT * FROM Package WHERE Id=@x";

    Package package = new Package();
    using (SqlConnection conn = new SqlConnection(constr))
    {
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@x", id);
        conn.Open();
        SqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            package.id = id;
            Debug.WriteLine("tddd" + id);
            package.name = rdr["Name"].ToString();
            package.price = rdr["Price"].ToString();
            package.description = rdr["Description"].ToString();
            package.type = rdr["Type"].ToString();
            package.availableNo = rdr["AvailableNo"].ToString();

        }
        return package;
    }
}
public void EditPack(Package pack)
{
    Debug.WriteLine("old" + pack.id);
    string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
    using (SqlConnection con = new SqlConnection(constr))
    {
        using (SqlCommand cmd = new SqlCommand("UPDATE Package SET Name=@Name,Price=@Price,Description=@Description,Type=@Type,AvailableNo=@Available WHERE Id=@id"))
        {
            cmd.Parameters.AddWithValue("@id", pack.id);
            cmd.Parameters.AddWithValue("@Name", pack.name);
            cmd.Parameters.AddWithValue("@Price", pack.price);
            cmd.Parameters.AddWithValue("@Description", pack.description);
            cmd.Parameters.AddWithValue("@Type", pack.type);
            cmd.Parameters.AddWithValue("@Available", pack.availableNo);

            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
public void DeletePackage(int id)
{
    int i = id;
    string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
    using (SqlConnection con = new SqlConnection(constr))
    {

        using (SqlCommand cmd = new SqlCommand("DELETE FROM Package WHERE Id=@id"))
        {

            cmd.Parameters.AddWithValue("@id", i);

            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}

    }
    public class Post
    {
        public int id { get; set; }
        public string customerId { get; set; }
        public string title { get; set; }
        public string post { get; set; }
        public DateTime date { get; set; }

    }
    public class album
    {
        public int id { get; set; }
        public int number { get; set; }
        public string albumName { get; set; }
        public string image { get; set; }
        public string owner { get; set; }
        

    }
    public class PostDetails
    {
        public void Insert(Post post)
        {
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Post (CustomerId,Title,Post,Date) VALUES (@id,@title, @post,@date)"))
                {
                    cmd.Parameters.AddWithValue("@id", post.customerId);
                    cmd.Parameters.AddWithValue("@title", post.title);
                    cmd.Parameters.AddWithValue("@post", post.post);
                    cmd.Parameters.AddWithValue("@date", post.date);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public Post getPost(int id)
        {

            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;

        String sql = "SELECT * FROM Post WHERE Id=@x";

        Post post = new Post();
    using (SqlConnection conn = new SqlConnection(constr))
    {
        SqlCommand cmd = new SqlCommand(sql, conn);
    cmd.Parameters.AddWithValue("@x", id);
        conn.Open();
        SqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            post.id = id;
            Debug.WriteLine("tddd" + id);
            post.customerId = rdr["CustomerId"].ToString();
                    post.title = rdr["Title"].ToString();
                    post.post = rdr["Post"].ToString();
                    post.date = (DateTime)rdr["Date"];

}
        return post;
    }}
        public List<Post> getPosts(String email)
        {
            
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                Customer customer = new Models.Customer();
                SqlCommand com = new SqlCommand("SELECT * FROM Post WHERE CustomerId=@Email");
                com.CommandType = CommandType.Text;
                com.Connection = con;

                com.Parameters.AddWithValue("@Email", email);

                using (SqlDataReader reader = com.ExecuteReader())
                {
                    List<Post> posts = new List<Post>();
                   
                    Debug.WriteLine(posts.Count);
                    while (reader.Read())
                    {
                        Post post = new Post();
                        Debug.WriteLine(reader["CustomerId"].ToString());
                        post.id = (int)reader["Id"];
                        post.customerId=reader["CustomerId"].ToString();
                        post.title = reader["Title"].ToString();
                        post.post = reader["Post"].ToString();
                        post.date = (DateTime)reader["Date"];
                        posts.Add(post);
                    }
                    Debug.WriteLine(posts.Count);
                    return posts;


                }

            }

        }
        public void deletepost(int id)
        {
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Post WHERE Id=@id"))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public void editpost(Post post)
        {
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Post SET Title=@title,Post=@post WHERE Id=@id"))
                {
                    cmd.Parameters.AddWithValue("@id", post.id);
                    cmd.Parameters.AddWithValue("@title", post.title);
                    cmd.Parameters.AddWithValue("@post", post.post);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public List<album> getImages(String name)
        {

            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                Customer customer = new Models.Customer();
                SqlCommand com = new SqlCommand("SELECT * FROM Images WHERE AlbumName=@name");
                com.CommandType = CommandType.Text;
                com.Connection = con;

                com.Parameters.AddWithValue("@name", name);

                using (SqlDataReader reader = com.ExecuteReader())
                {
                    List<album> images = new List<album>();

                    Debug.WriteLine(images.Count);
                    while (reader.Read())
                    {
                        album post = new album();
                        post.image = reader["Image"].ToString();

                        images.Add(post);
                    }
                    Debug.WriteLine(images.Count);
                    return images;


                }


            }

        }

        public List<MyAlbum> getAlbums(String email)
        {

            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                Customer customer = new Models.Customer();
                SqlCommand com = new SqlCommand("SELECT * FROM Album WHERE Customer=@Email");
                com.CommandType = CommandType.Text;
                com.Connection = con;

                com.Parameters.AddWithValue("@Email", email);

                using (SqlDataReader reader = com.ExecuteReader())
                {
                    List<MyAlbum> albumsMy = new List<MyAlbum>();

                    Debug.WriteLine(albumsMy.Count);
                    while (reader.Read())
                    {
                        MyAlbum post = new MyAlbum();
                        post.number = (int)reader["number"];
                        post.id = (int)reader["Id"];
                        post.name = reader["album"].ToString();
                        post.image = reader["image"].ToString();
                        albumsMy.Add(post);
                    }
                    Debug.WriteLine(albumsMy.Count);
                    return albumsMy;


                }

            }

        }
    }
    public class customerProfile
    {
        public Customer customer { get; set; }
        public Post post { get; set; }
        public List<Post> posts { get; set; }
        public album image { get; set; }
        public HttpFileCollectionBase images { get; set; }
        public int number { get; set; }
        public events events { get; set; }
        public List<events> allEvents { get; set; }
        public List<Package> range { get; set; }
        public List<Package> searchpackage { get; set; }
        public List<MyAlbum> albums { get; set; }
        public List<album> albumImage { get; set; }
        public DateTime packagesDate { get; set; }
        public Package package { get; set; }
        public Daypack dayPack { get; set;}
    }

    

    
    
}