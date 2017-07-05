using Paysmart.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Paysmart.Controllers
{
    public class EOTPverficationController : ApiController
    {
        [HttpPost]
        [Route("api/Appusers/SavePostlist1")]
        public int SavePostlist1(UserAccount ocr)
        {


            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = "Data Source=localhost;initial catalog= Paysmart;integrated security=sspi;";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "InsUpdDelEOTPverification";

            cmd.Connection = conn;


            SqlParameter f = new SqlParameter("@flag", SqlDbType.VarChar);
            f.Value = ocr.flag;
            cmd.Parameters.Add(f);

            SqlParameter i = new SqlParameter("@id", SqlDbType.Int);
            i.Value = ocr.id;
            cmd.Parameters.Add(i);

            SqlParameter q1 = new SqlParameter("@Email", SqlDbType.VarChar, 50);
            q1.Value = ocr.Email;
            cmd.Parameters.Add(q1);

            SqlParameter e = new SqlParameter("@Emailotp", SqlDbType.VarChar, 10);
            e.Value = ocr.Emailotp;
            cmd.Parameters.Add(e);

            SqlParameter c = new SqlParameter("@Mobilenumber", SqlDbType.VarChar, 20);
            c.Value = ocr.Mobilenumber;
            cmd.Parameters.Add(c);



            conn.Open();
            int status = cmd.ExecuteNonQuery();




            conn.Close();
            return status;

        }
    }
}
