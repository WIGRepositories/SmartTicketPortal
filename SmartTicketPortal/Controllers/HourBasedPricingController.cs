using SmartTicketPortal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartTicketDashboard.Controllers
{
    public class HourBasedPricingController : ApiController
    {
        
        [HttpGet]
        [Route("api/HourBasedPricing/GetHourBasePricing")]
        public DataTable GetHourBasePricing()
        {
            DataTable Tbl = new DataTable();


            //connect to database
            SqlConnection conn = new SqlConnection();
            //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PSGetHourBasePricing";
            cmd.Connection = conn;

            SqlDataAdapter db = new SqlDataAdapter(cmd);
            db.Fill(Tbl);



            // int found = 0;
            return Tbl;

        }

        [HttpPost]
        [Route("api/HourBasedPricing/SaveHourBasePricing")]
        public DataTable SaveHourBasePricing(HourBase c)
        {
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PSInsUpdDelHourBasePricing";

            cmd.Connection = conn;

            SqlParameter insupddelflag = new SqlParameter("@insupddelflag", SqlDbType.VarChar, 10);
            insupddelflag.Value = c.insupddelflag;
            cmd.Parameters.Add(insupddelflag);

            SqlParameter q = new SqlParameter("@Id", SqlDbType.Int);
            q.Value = c.Id;
            cmd.Parameters.Add(q);

            SqlParameter v = new SqlParameter("@VehicleModelId", SqlDbType.Int);
            v.Value = c.VehicleModelId;
            cmd.Parameters.Add(v);

            //SqlParameter q1 = new SqlParameter("@VehicleModel", SqlDbType.VarChar, 50);
            //q1.Value = c.VehicleModel;
            //cmd.Parameters.Add(q1);

           SqlParameter e = new SqlParameter("@Hours", SqlDbType.VarChar, 5);
            e.Value = c.Hours;
            cmd.Parameters.Add(e);

            SqlParameter p = new SqlParameter("@Price", SqlDbType.Decimal);
            p.Value = c.Price;
            cmd.Parameters.Add(p);

            SqlParameter ft = new SqlParameter("@FromTime", SqlDbType.DateTime);
            ft.Value = c.FromTime;
            cmd.Parameters.Add(ft);

            SqlParameter tf = new SqlParameter("@ToTime", SqlDbType.DateTime);
            tf.Value = c.ToTime;
            cmd.Parameters.Add(tf);
         

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            return (dt);



        }

    }
}

  