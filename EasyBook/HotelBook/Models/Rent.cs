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

    public class Boarding
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string telephone { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string bPassword { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public string status { get; set; }

    }
    

    public class HotelDB 
    {
        

        
        public List<Boarding> viewBoarding(string city)
        {
            
            String c = city;
            String status = "Accept";
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;

            String sql = "SELECT * FROM Boarding WHERE City=@city";

            var board = new List<Boarding>();
            using (SqlConnection conn = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
               
                cmd.Parameters.AddWithValue("@city", c);
                cmd.Parameters.AddWithValue("@accept", status);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var boarding = new Boarding();
                    boarding.name = rdr["Name"].ToString();
                    boarding.address = rdr["Address"].ToString();
                    boarding.email = rdr["Email"].ToString();
                    boarding.telephone = rdr["Telephone"].ToString();
                    boarding.city = rdr["City"].ToString();
                    boarding.description = rdr["Description"].ToString();
                    boarding.image = rdr["image"].ToString();
                    boarding.status = rdr["Status"].ToString();
                    board.Add(boarding);
                }
                return board;
            }
        }
        public List<Boarding> viewAdd(string name)
        {

            String n = name;
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;

            String sql = "SELECT * FROM Boarding WHERE Name=@name";

            var board = new List<Boarding>();
            using (SqlConnection conn = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@name", n);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var boarding = new Boarding();
                    boarding.id = (int)rdr["Id"];
                    boarding.name = rdr["Name"].ToString();
                    boarding.address = rdr["Address"].ToString();
                    boarding.email = rdr["Email"].ToString();
                    boarding.telephone = rdr["Telephone"].ToString();
                    boarding.city = rdr["City"].ToString();
                    boarding.description = rdr["Description"].ToString();
                    boarding.image = rdr["image"].ToString();
                    boarding.status = rdr["Status"].ToString();
                    board.Add(boarding);
                }
                return board;
            }
        }
        public void DeleteAdd(int id,Boarding b)
        {
            int i = id;
            String pass = b.password;
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {

                using (SqlCommand cmd = new SqlCommand("DELETE FROM Boarding WHERE Id=@id AND Password=@password"))
                {

                    cmd.Parameters.AddWithValue("@id", i);
                    cmd.Parameters.AddWithValue("@password", pass);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public void advertisement(string email,string name,string address, string city, string telephone, string description, string password, string image)
        {
            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Boarding (Name,Email, Password,Telephone,City,Address,Description,image,Status) VALUES (@Name,@Email, @Password,@Telephone,@City,@Address,@Description,@Image,null)"))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Telephone",telephone);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@City", city);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Image", image);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public Boarding DeleteAddView(int id)
        {

            string constr = ConfigurationManager.ConnectionStrings["HotelDBContext"].ConnectionString;

            String sql = "SELECT * FROM Boarding WHERE Id=@x";

            Boarding b = new Boarding();
            using (SqlConnection conn = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@x", id);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    b.id = id;
                    b.password = rdr["Password"].ToString();

                }
                return b;
            }
        }
    }
    



}