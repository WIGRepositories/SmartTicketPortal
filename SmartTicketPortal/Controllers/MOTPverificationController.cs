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
    public class MOTPverificationController : ApiController
    {
        
        [HttpPost]
        [Route("api/Appusers/SavePostlist")]
        public int  SavePostlist(UserAccount ocr)
        {


            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "InsUpdDelMOTPverification";

            cmd.Connection = conn;


            SqlParameter f = new SqlParameter("@flag", SqlDbType.VarChar);
            f.Value = ocr.flag;
            cmd.Parameters.Add(f);

            SqlParameter i = new SqlParameter("@id", SqlDbType.Int);
            i.Value = ocr.id;
            cmd.Parameters.Add(i);

            SqlParameter q1 = new SqlParameter("@Mobilenumber", SqlDbType.VarChar, 20);
            q1.Value = ocr.Mobilenumber;
            cmd.Parameters.Add(q1);

            SqlParameter e = new SqlParameter("@Mobileotp", SqlDbType.VarChar, 10);
            e.Value = ocr.Mobileotp;
            cmd.Parameters.Add(e);




            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);


            return 1;

        }

    }
}
