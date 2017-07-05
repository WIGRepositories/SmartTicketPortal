using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Paysmart.Models;

namespace Paysmart.Controllers
{
    public class UsercardsController : ApiController
    {

        [HttpGet]
        [Route("api/Usercards/Getcards")]
        public DataTable Getcards()
        {
            DataTable Tbl = new DataTable();


            //connect to database
            SqlConnection conn = new SqlConnection();
            //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Getusercards";
            cmd.Connection = conn;

            SqlDataAdapter db = new SqlDataAdapter(cmd);
            db.Fill(Tbl);



            // int found = 0;
            return Tbl;

        }

        [HttpPost]
        [Route("api/Usercards/carddetails")]
        public DataTable carddetails(Usercards c)
        {
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "InsupdUsercard";

            cmd.Connection = conn;

         
            SqlParameter q = new SqlParameter("@Id", SqlDbType.Int);
            q.Value = c.Id;
            cmd.Parameters.Add(q);

            SqlParameter q1 = new SqlParameter("@AccId", SqlDbType.Int);
            q1.Value = c.AccId;
            cmd.Parameters.Add(q1);

            SqlParameter e = new SqlParameter("@Cardno", SqlDbType.VarChar, 20);
            e.Value = c.Cardno;
            cmd.Parameters.Add(e);

            SqlParameter t = new SqlParameter("@ccv", SqlDbType.VarChar, 5);
            t.Value = c.ccv;
            cmd.Parameters.Add(t);

            SqlParameter c1 = new SqlParameter("@Expyear", SqlDbType.VarChar, 10);
            c1.Value = c.Expyear;
            cmd.Parameters.Add(c1);

            SqlParameter b = new SqlParameter("@expmonth", SqlDbType.VarChar, 10);
            b.Value = c.expmonth;
            cmd.Parameters.Add(b);

            SqlParameter b1 = new SqlParameter("@Statusid", SqlDbType.Int);
            b1.Value = c.Statusid;
            cmd.Parameters.Add(b1);

            SqlParameter b2 = new SqlParameter("@isDefault", SqlDbType.VarChar, 10);
            b2.Value = c.isDefault;
            cmd.Parameters.Add(b2);                      


            SqlParameter f1 = new SqlParameter("@flag", SqlDbType.VarChar);
            f1.Value = c.flag;
            cmd.Parameters.Add(f1);


            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            return (dt);



        }

       
    }
}
