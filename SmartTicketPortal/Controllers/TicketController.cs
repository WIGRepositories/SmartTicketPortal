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



    public class TicketController : ApiController
    {

        [HttpPost]
        [Route("api/Ticket/TroubleTicket")]
        public DataTable TroubleTicket(Tickets ocr)
        {
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "InsupdTroubleTicket";

            cmd.Connection = conn;
            

                 SqlParameter q = new SqlParameter("@Id", SqlDbType.VarChar, 50);
            q.Value = ocr.id;
            cmd.Parameters.Add(q);             

            SqlParameter q1 = new SqlParameter("@Userid", SqlDbType.Int);
            q1.Value = ocr.Userid;
            cmd.Parameters.Add(q1);

            SqlParameter e = new SqlParameter("@EmailId", SqlDbType.VarChar, 50);
            e.Value = ocr.EmailId;
            cmd.Parameters.Add(e);

            SqlParameter c = new SqlParameter("@CreatedOn", SqlDbType.Date);
            c.Value = ocr.CreatedOn;
            cmd.Parameters.Add(c);

            SqlParameter b = new SqlParameter("@Onbehalfofph", SqlDbType.VarChar, 20);
            b.Value = ocr.Onbehalfofph;
            cmd.Parameters.Add(b);

            SqlParameter p = new SqlParameter("@PhoneNumber", SqlDbType.VarChar, 20);
            p.Value = ocr.PhoneNumber;
            cmd.Parameters.Add(p);

            SqlParameter m = new SqlParameter("@TicketNo", SqlDbType.VarChar, 20);
            m.Value = ocr.TicketNo;
            cmd.Parameters.Add(m);

            SqlParameter t = new SqlParameter("@Catid", SqlDbType.Int);
            t.Value = ocr.Catid;
            cmd.Parameters.Add(t);

            SqlParameter e2 = new SqlParameter("@Description", SqlDbType.VarChar, 250);
            e2.Value = ocr.Description;
            cmd.Parameters.Add(e2);

            SqlParameter e1 = new SqlParameter("@Emailsent", SqlDbType.Int);
            e1.Value = ocr.Emailsent;
            cmd.Parameters.Add(e1);

            SqlParameter s1 = new SqlParameter("@Smssent", SqlDbType.Int);
            s1.Value = ocr.Smssent;
            cmd.Parameters.Add(s1);

            SqlParameter n = new SqlParameter("@TicketTypeId", SqlDbType.Int);
            n.Value = ocr.TicketTypeId;
            cmd.Parameters.Add(n);


            SqlParameter s = new SqlParameter("@StatusId", SqlDbType.Int);
            s.Value = ocr.StatusId;
            cmd.Parameters.Add(s);

            SqlParameter f = new SqlParameter("@flag", SqlDbType.VarChar);
            f.Value = ocr.flag;
            cmd.Parameters.Add(f);


            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            return (dt);

            //Verify Passwordotp

        }


        [HttpGet]
        [Route("api/Ticket/TicketDetails")]
        public DataTable TicketDetails()
        {
            DataTable Tbl = new DataTable();


            //connect to database
            SqlConnection conn = new SqlConnection();
            //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetTicketDetails";
            cmd.Connection = conn;

            SqlDataAdapter db = new SqlDataAdapter(cmd);
            db.Fill(Tbl);





            // int found = 0;
            return Tbl;

        }

        [HttpGet]
        [Route("api/Ticket/TicketHistory")]
        public DataTable TicketHistory()
        {
            DataTable Tbl = new DataTable();


            //connect to database
            SqlConnection conn = new SqlConnection();
            //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetTicketHistory";
            cmd.Connection = conn;

            SqlDataAdapter db = new SqlDataAdapter(cmd);
            db.Fill(Tbl);



            // int found = 0;
            return Tbl;

        }

        [HttpGet]
        [Route("api/Ticket/Filecontent")]
        public DataTable Filecontent()
        {
            DataTable Tbl = new DataTable();


            //connect to database
            SqlConnection conn = new SqlConnection();
            //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Getfiledetails";
            cmd.Connection = conn;

            SqlDataAdapter db = new SqlDataAdapter(cmd);
            db.Fill(Tbl);



            // int found = 0;
            return Tbl;

        }

        [HttpGet]
        [Route("api/Ticket/AllTickets")]
        public DataTable AllTickets()
        {
            DataTable Tbl = new DataTable();


            //connect to database
            SqlConnection conn = new SqlConnection();
            //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Getalltickets";
            cmd.Connection = conn;

            SqlDataAdapter db = new SqlDataAdapter(cmd);
            db.Fill(Tbl);



            // int found = 0;
            return Tbl;

        }

        [HttpGet]
        [Route("api/Ticket/UserTickets")]
        public DataTable UserTickets()
        {
            DataTable Tbl = new DataTable();


            //connect to database
            SqlConnection conn = new SqlConnection();
            //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Getusertickets";
            cmd.Connection = conn;

            SqlDataAdapter db = new SqlDataAdapter(cmd);
            db.Fill(Tbl);



            // int found = 0;
            return Tbl;

        }
    }
}
