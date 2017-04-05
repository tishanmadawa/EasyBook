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

namespace HotelBook.Models
{
    public class Customer
    {   public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string cPassword { get; set; }
        public int attempt { get; set; }
        public string image { get; set; }
        public string rating { get; set; }
        public string ProfileName { get; set; }
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
    }
    public class MyAlbum
    {
        public int id { get; set; }
        public string user { get; set; }
        public string name { get; set; }
        public int number { get; set; }
        public string image { get; set; }
    }

    public class HotelDBContext 
    {
        
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
                        customer.attempt = Convert.ToInt32(dt.Rows[0]["Attempt"]);
                        customer.image = Convert.ToString(dt.Rows[0]["Image"]);
                        customer.address = Convert.ToString(dt.Rows[0]["Address"]);
                        customer.ProfileName= Convert.ToString(dt.Rows[0]["ProfileName"]);
                        customer.rating= Convert.ToString(dt.Rows[0]["Rating"]);
                        customer.state= Convert.ToString(dt.Rows[0]["State"]);
                        customer.city= Convert.ToString(dt.Rows[0]["City"]);


                        return customer;
                    }else
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

        public List<Package> Viewpackage(string email)
        {
        string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;

        String sql = "SELECT * FROM Package WHERE CusId=@email";

        var mod = new List<Package>();
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
    package.price = rdr["Price"].ToString();
    package.description = rdr["Description"].ToString();
    package.type = rdr["Type"].ToString();
    package.availableNo = rdr["AvailableNo"].ToString();

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
        using (SqlCommand cmd = new SqlCommand("INSERT INTO Package (Name,Price, Description,Type,CusId,AvailableNo) VALUES (@Name,@Price, @Description,@Type,1,@Available)"))
        {
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
        public List<MyAlbum> albums { get; set; }
        public List<album> albumImage { get; set; }
        public DateTime packagesDate { get; set; }
    }

    

    
    
}