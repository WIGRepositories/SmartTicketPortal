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
    public class PassengerController : ApiController
    {

        [HttpPost]
        [Route("api/Passenger/PsngrDetails")]
        public DataTable PsngrDetails(passenger y)
        {
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_InsPassengerDetails";

            cmd.Connection = conn;

          

            SqlParameter q = new SqlParameter("@Fname", SqlDbType.VarChar, 30);
            q.Value = y.Fname;
            cmd.Parameters.Add(q);

            SqlParameter q1 = new SqlParameter("@Lname", SqlDbType.VarChar, 30);
            q1.Value = y.Lname;
            cmd.Parameters.Add(q1);

            SqlParameter e = new SqlParameter("@Age", SqlDbType.Int);
            e.Value = y.Age;
            cmd.Parameters.Add(e);

            SqlParameter c = new SqlParameter("@Sex", SqlDbType.Int);
            c.Value = y.Sex;
            cmd.Parameters.Add(c);

            SqlParameter b = new SqlParameter("@datetime", SqlDbType.VarChar,30);
            b.Value = y.datetime;
            cmd.Parameters.Add(b);

            SqlParameter p = new SqlParameter("@Pnr_Id", SqlDbType.Int);
            p.Value = y.Pnr_Id;
            cmd.Parameters.Add(p);

            SqlParameter m = new SqlParameter("@Pnr_No", SqlDbType.VarChar, 20);
            m.Value = y.Pnr_No;
            cmd.Parameters.Add(m);

            SqlParameter t = new SqlParameter("@Identityproof", SqlDbType.VarChar,30);
            t.Value = y.Identityproof;
            cmd.Parameters.Add(t);






            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            return (dt);

            //Verify Passwordotp

        }
    }
}
