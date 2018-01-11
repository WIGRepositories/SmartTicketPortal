using Paysmart.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;



namespace SmartTicketPortal.Controllers
{
    public class ComplaintsController : ApiController
    {
       [HttpPost]
       [Route("api/Complaints/ComplaintsPortal")]
        public DataTable ComplaintsPortal (Complaints Co)
        {
            SqlConnection conn = new SqlConnection();
            SqlCommand com = new SqlCommand();
           try
           {
               conn.ConnectionString = ConfigurationManager.ConnectionStrings["btposdb"].ToString();
               com.CommandType = CommandType.StoredProcedure;
               com.CommandText = "PSInsupdTroubleTicket";
               com.Connection = conn;

               SqlParameter f = new SqlParameter("@flag", SqlDbType.VarChar);
               f.Value = Co.flag;
               com.Parameters.Add(f);
               SqlParameter pn = new SqlParameter("@PhoneNumber", SqlDbType.VarChar);
               pn.Value = Co.PhoneNumber;
               com.Parameters.Add(pn);
               SqlParameter ei = new SqlParameter("@EmailId", SqlDbType.VarChar);
               ei.Value = Co.EmailId;
               com.Parameters.Add(ei);
               SqlParameter de = new SqlParameter("@Description", SqlDbType.VarChar);
               de.Value = Co.Description;
               com.Parameters.Add(de);
               SqlParameter ti = new SqlParameter("@TicketNo", SqlDbType.VarChar);
               ti.Value = Co.TicketNo;
               com.Parameters.Add(ti);
               SqlParameter na = new SqlParameter("@Name", SqlDbType.VarChar);
               na.Value = Co.Name;
               com.Parameters.Add(na);
               SqlParameter ca = new SqlParameter("@Category", SqlDbType.VarChar);
               ca.Value = Co.Category;
               com.Parameters.Add(ca);
               SqlParameter su = new SqlParameter("@Subject", SqlDbType.VarChar);
               su.Value = Co.Subject;
               com.Parameters.Add(su);
           }
           catch
           {
               Exception ex;
           }
           DataTable dt = new DataTable();
           SqlDataAdapter da = new SqlDataAdapter(com);
           da.Fill(dt);
           return dt;

        }
	}
}