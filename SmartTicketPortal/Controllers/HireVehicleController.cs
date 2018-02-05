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
    public class HireVehicleController : ApiController
    {
        [HttpGet]
        [Route("api/HireVehicle/GetHireVehicle")]
        public DataTable GetHireVehicle(int srcId,int destId)
        {
            DataTable Tbl = new DataTable();

            //connect to database
            SqlConnection conn = new SqlConnection();
            //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetHireVehicle";
            cmd.Connection = conn;

            cmd.Parameters.Add("@srcId", SqlDbType.Int).Value = srcId;
            cmd.Parameters.Add("@destId", SqlDbType.Int).Value = destId;
            
            DataSet ds = new DataSet();
            SqlDataAdapter db = new SqlDataAdapter(cmd);
            db.Fill(ds);
            Tbl = ds.Tables[0];

            return Tbl;

        }
    }
}
