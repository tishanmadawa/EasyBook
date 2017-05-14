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

    public class MyViewModel
    {
        public List<Post> TheFirstTable { get; set; }
        public List<events> TheSecondTable { get; set; }
        public List<album> TheThirdTable { get; set; }
        public DateTime crntdate { get; set; }
        public List<image> ThefourthTable { get; set; }

    }



    public class admin 
    {     
      
        

   
   
  
        //post part of admin

       public List<events> getPostEvents(DateTime dd)
        {

            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                
                SqlCommand com = new SqlCommand("SELECT * FROM Events WHERE Date>=@ddd");
                com.CommandType = CommandType.Text;
                com.Connection = con;

                com.Parameters.AddWithValue("@ddd", dd);

                using (SqlDataReader reader = com.ExecuteReader())
                {
                    List<events> eventList = new List<events>();

                 
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
                        single.noDates = (int)reader["NoDays"];
                        single.startTime = reader["StartTime"].ToString();
                        single.user = reader["Customer"].ToString();
                        single.accept= reader["Accept"].ToString();
                        eventList.Add(single);
                    }
                    Debug.WriteLine(eventList.Count);
                  
                    return eventList;


                }

            }

        }
        public List<Post> getPostPosts(DateTime dd)
        {

            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                Customer customer = new Models.Customer();
                SqlCommand com = new SqlCommand("SELECT * FROM Post WHERE Date>=@dd"); 
                com.CommandType = CommandType.Text;
                com.Connection = con;

                com.Parameters.AddWithValue("@dd", dd);

                using (SqlDataReader reader = com.ExecuteReader())
                {
                    List<Post> posts = new List<Post>();

                    Debug.WriteLine(posts.Count);
                    while (reader.Read())
                    {
                        Post post = new Post();
                        Debug.WriteLine(reader["CustomerId"].ToString());
                        post.id = (int)reader["Id"];
                        post.customerId = reader["CustomerId"].ToString();
                        post.title = reader["Title"].ToString();
                        post.post = reader["Post"].ToString();
                        post.date = (DateTime)reader["Date"];
                        post.accept = reader["Accept"].ToString();
                        posts.Add(post);
                    }
                    Debug.WriteLine(posts.Count);
                    return posts;


                }

            }

        }
        public List<album> getAlbumName(DateTime dd)
        {

            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
             
                SqlCommand com = new SqlCommand("SELECT * FROM Album WHERE Date>=@dd");
                com.CommandType = CommandType.Text;
                com.Connection = con;

                com.Parameters.AddWithValue("@dd", dd);

                using (SqlDataReader reader = com.ExecuteReader())
                {
                    List<album> album1 = new List<album>();

               
                    while (reader.Read())
                    {
                        album album2 = new album();
                       album2.id= (int)reader["Id"];
                        album2.albumName= reader["album"].ToString();
                        album2.accept = reader["Accept"].ToString();
                        album2.date= (DateTime)reader["Date"];
                        album2.image= reader["image"].ToString();
                        album1.Add(album2);
                    }
                   
                    return album1;


                }

            }

        }
        public List<image> getImg(DateTime dd)
        {

            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();

                SqlCommand com = new SqlCommand("SELECT * FROM Images WHERE Date>=@dd");
                com.CommandType = CommandType.Text;
                com.Connection = con;

                com.Parameters.AddWithValue("@dd", dd);

                using (SqlDataReader reader = com.ExecuteReader())
                {
                    List<image> album1 = new List<image>();


                    while (reader.Read())
                    {
                        image album2 = new image();

                        album2.albumName = reader["AlbumName"].ToString();
                       
                        album2.date = (DateTime)reader["Date"];
                        album2.imag = reader["Image"].ToString();
                        album1.Add(album2);
                    }

                    return album1;


                }

            }

        }

        public void doPostRejects(int id)
        {
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Post SET Accept=@acc WHERE id=@id"))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@acc","Reject");
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    
        public void doPostAccept(int id)
        {

            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Post SET Accept=@acc WHERE id=@id"))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@acc", "Accept");
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public void doPostDelete(int id)
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
        //button of event
        public void doEventRejects(int id)
        {
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Events SET Accept=@acc WHERE id=@id"))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@acc", "Reject");
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public void doEventAccept(int id)
        {

            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Events SET Accept=@acc WHERE id=@id"))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@acc", "Accept");
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public void doEventDelete(int id)
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

        //album button
        public void doAlbumRejects(int id)
        {
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Album SET Accept=@acc WHERE id=@id"))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@acc", "Reject");
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public void doAlbumAccept(int id)
        {

            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Album SET Accept=@acc WHERE id=@id"))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@acc", "Accept");
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public void doAlbumDelete(int id)
        {

            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Album WHERE Id=@id"))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
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
  
    
    
}