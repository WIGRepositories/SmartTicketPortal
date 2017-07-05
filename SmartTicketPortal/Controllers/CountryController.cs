using SmartTicketPortal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartTicketPortal.Controllers
{
   public class CountryController : ApiController
    {
         [HttpGet]

        public DataTable GetCountry()
        {
            DataTable Tbl = new DataTable();
            

            //connect to database
            SqlConnection conn = new SqlConnection();
            //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetCOUNTRY";
            cmd.Connection = conn;
            DataSet ds = new DataSet();
            SqlDataAdapter db = new SqlDataAdapter(cmd);
            db.Fill(ds);
            Tbl = ds.Tables[0];

           
            // int found = 0;
            return Tbl;
        }

        //[HttpPost]
        // public HttpResponseMessage SaveCountries(IEnumerable<Country> Country)
        //{

        //    LogTraceWriter traceWriter = new LogTraceWriter();
        //    traceWriter.Trace(Request, "0", TraceLevel.Info, "{0}", "SaveCountries ....");
        //    SqlConnection conn = new SqlConnection();
        //    try
        //    {
        //        //connect to database

        //        //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
        //        conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

        //        SqlCommand cmd = new SqlCommand();
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "InsUpdCountry";
        //        cmd.Connection = conn;
        //        conn.Open();

        //        foreach (Country c in Country)
        //        {

        //            SqlParameter rid = new SqlParameter();
        //            rid.ParameterName = "@Id";
        //            rid.SqlDbType = SqlDbType.Int;
        //            rid.Value = c.Id;
        //            cmd.Parameters.Add(rid);

        //            SqlParameter cmpid = new SqlParameter();
        //            cmpid.ParameterName = "@HasOperations";
        //            cmpid.SqlDbType = SqlDbType.Int;
        //            cmpid.Value = c.HasOperations;
        //            cmd.Parameters.Add(cmpid);


        //            cmd.ExecuteScalar();

        //            cmd.Parameters.Clear();
        //        }

        //        conn.Close();
        //        traceWriter.Trace(Request, "0", TraceLevel.Info, "{0}", "SaveCountries completed.");
        //        return new HttpResponseMessage(HttpStatusCode.OK);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (conn != null && conn.State == ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }
        //        string str = ex.Message;
        //        traceWriter.Trace(Request, "1", TraceLevel.Info, "{0}", "Error in SaveCountries:" + ex.Message);
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
        //    }
        //}
    }
    
}
