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
    public class UserpreferencesController : ApiController
    {

        [HttpGet]
        [Route("api/Userpreferences/Users")]
        public DataTable Users()
        {
            DataTable Tbl = new DataTable();


            //connect to database
            SqlConnection conn = new SqlConnection();
            //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetUserpreferences";
            cmd.Connection = conn;

            SqlDataAdapter db = new SqlDataAdapter(cmd);
            db.Fill(Tbl);



            // int found = 0;
            return Tbl;

        }

        [HttpPost]
        [Route("api/Userpreferences/preferences")]
        public DataTable preferences(Users r)
        {
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "InsupdUserPreferences";

            cmd.Connection = conn;         



            SqlParameter q = new SqlParameter("@Id", SqlDbType.Int);
            q.Value = r.Id;
            cmd.Parameters.Add(q);

            SqlParameter q1 = new SqlParameter("@userid", SqlDbType.Int);
            q1.Value = r.userid;
            cmd.Parameters.Add(q1);

            SqlParameter e = new SqlParameter("@email", SqlDbType.VarChar, 50);
            e.Value = r.email;
            cmd.Parameters.Add(e);

            SqlParameter c = new SqlParameter("@Address", SqlDbType.VarChar, 50);
            c.Value = r.Address;
            cmd.Parameters.Add(c);

            SqlParameter b = new SqlParameter("@Accountid", SqlDbType.Int);
            b.Value = r.Accountid;
            cmd.Parameters.Add(b);

            SqlParameter p = new SqlParameter("@preferenceTypeId", SqlDbType.Int);
            p.Value = r.preferenceTypeId;
            cmd.Parameters.Add(p);

            SqlParameter m = new SqlParameter("@preferenceId", SqlDbType.Int);
            m.Value = r.preferenceId;
            cmd.Parameters.Add(m);

          

            SqlParameter f = new SqlParameter("@flag", SqlDbType.VarChar);
            f.Value = r.flag;
            cmd.Parameters.Add(f);


            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            return (dt);

            //Verify Passwordotp

        }
    }
}
