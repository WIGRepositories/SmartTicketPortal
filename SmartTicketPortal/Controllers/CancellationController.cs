using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartTicketPortal.Controllers
{
    public class CancellationController : ApiController
    {
        [HttpGet]
        public DataTable RetriveTicketDetails(string ticketNo, string emailid, string mobileno)
        {
            DataTable dt = new DataTable();
            //connect to database
            SqlConnection conn = new SqlConnection();
            //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetAvailableServices";
            cmd.Parameters.Add("@ticketNo", SqlDbType.VarChar, 15).Value = ticketNo;
            cmd.Parameters.Add("@emailid", SqlDbType.VarChar, 15).Value = (emailid == null) ? "-1" : emailid;
            cmd.Parameters.Add("@mobileno", SqlDbType.VarChar, 15).Value = (mobileno == null) ? "-1" : mobileno;

            cmd.Connection = conn;


            SqlDataAdapter db = new SqlDataAdapter(cmd);
            db.Fill(dt);

            return dt;
        }
    }
}
