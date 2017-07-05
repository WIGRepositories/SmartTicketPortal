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
    public class FaqsController : ApiController
    {

        [HttpGet]
        [Route("api/Faqs/Getfaqs")]
        public DataTable Getfaqs()
        {
            DataTable Tbl = new DataTable();


            //connect to database
            SqlConnection conn = new SqlConnection();
            //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Getfaqs";
            cmd.Connection = conn;

            SqlDataAdapter db = new SqlDataAdapter(cmd);
            db.Fill(Tbl);



            // int found = 0;
            return Tbl;

        }

        [HttpPost]
        [Route("api/Faqs/faqsdetails")]
        public DataTable faqsdetails(faqs f)
        {
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "InsUpdFaqs";

            cmd.Connection = conn;


            SqlParameter q = new SqlParameter("@Id", SqlDbType.Int);
            q.Value = f.Id;
            cmd.Parameters.Add(q);

            SqlParameter q1 = new SqlParameter("@Question", SqlDbType.VarChar, 500);
            q1.Value = f.Question;
            cmd.Parameters.Add(q1);

            SqlParameter e = new SqlParameter("@Answer", SqlDbType.VarChar, 500);
            e.Value = f.Answer;
            cmd.Parameters.Add(e);

            SqlParameter t = new SqlParameter("@Catid", SqlDbType.Int);
            t.Value = f.Catid;
            cmd.Parameters.Add(t);

            SqlParameter c = new SqlParameter("@CreatedOn", SqlDbType.Date);
            c.Value = f.CreatedOn;
            cmd.Parameters.Add(c);

            SqlParameter b = new SqlParameter("@Createdby", SqlDbType.Int);
            b.Value = f.Createdby;
            cmd.Parameters.Add(b);     

            

            SqlParameter f1 = new SqlParameter("@flag", SqlDbType.VarChar);
            f1.Value = f.flag;
            cmd.Parameters.Add(f1);


            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            return (dt);

            //Verify Passwordotp

        }
    }
}
