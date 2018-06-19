using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;

namespace SmartTicketPortal.Controllers
{
    public class AdvertisementController : ApiController
    {
        [HttpGet]
        [Route("api/Advertisement/GetAdvertisment")]
        public DataTable GetAdvertisment()
        {
            DataTable Tbl = new DataTable();
         
            //connect to database
            SqlConnection conn = new SqlConnection();
            //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Getadvertisement";
            cmd.Connection = conn;
            
            DataSet ds = new DataSet();
            SqlDataAdapter db = new SqlDataAdapter(cmd);
            db.Fill(ds);
            Tbl = ds.Tables[0];
           
            return Tbl;

        }
    

     //public DataTable GetActivityLog()
     //   {
     //       SqlConnection conn = new SqlConnection();
     //       conn.ConnectionString = ConfigurationManager.ConnectionStrings["btposdb"].ToString();

     //       SqlCommand cmd = new SqlCommand();
     //       cmd.Connection = conn;
     //       cmd.CommandType = CommandType.StoredProcedure;
     //       cmd.CommandText = "GetActivityLog";

     //       SqlDataAdapter db = new SqlDataAdapter(cmd);
     //       DataTable tbl = new DataTable();

     //       db.Fill(tbl);

     //       return tbl;

     //   }
}

}