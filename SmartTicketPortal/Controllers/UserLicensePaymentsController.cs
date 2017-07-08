using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartTicketPortal.Controllers
{
    public class UserLicensePaymentsController : ApiController
    {
        [HttpGet]

        public DataTable UserLicensePymt()
        {
            DataTable Tbl = new DataTable();


            //connect to database
            SqlConnection conn = new SqlConnection();
            //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetUserLicensePayments";
            cmd.Connection = conn;

            DataSet ds = new DataSet();
            SqlDataAdapter db = new SqlDataAdapter(cmd);
            db.Fill(ds);
            Tbl = ds.Tables[0];

            // int found = 0;
            return Tbl;



        }


        //[HttpPost]
        //public HttpResponseMessage SaveType(UserLicensePayments b)
        //{

        //    //connect to database
        //    SqlConnection conn = new SqlConnection();
        //    try
        //    {
        //        //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
        //        conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

        //        SqlCommand cmd = new SqlCommand();
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "InsUpdDelULPymtTransDetails";
        //        cmd.Connection = conn;
        //        conn.Open();

        //        //SqlParameter Cid = new SqlParameter();
        //        //Cid.ParameterName = "@Id";
        //        //Cid.SqlDbType = SqlDbType.Int;
        //        //Cid.Value = Convert.ToInt32(b.Id);
        //        //cmd.Parameters.Add(Cid);

        //        SqlParameter fid = new SqlParameter();
        //        fid.ParameterName = "@ULPPymtTransId";
        //        fid.SqlDbType = SqlDbType.Int;
        //        fid.Value = Convert.ToInt32(b.ULPPymtTransId);
        //        cmd.Parameters.Add(fid);

        //        SqlParameter rid = new SqlParameter();
        //        rid.ParameterName = "@PaymentTypeId";
        //        rid.SqlDbType = SqlDbType.Int;
        //        rid.Value = Convert.ToInt32(b.PaymentTypeId);
        //        cmd.Parameters.Add(rid);

        //        SqlParameter Gid = new SqlParameter();
        //        Gid.ParameterName = "@StatusId";
        //        Gid.SqlDbType = SqlDbType.Int;
        //        Gid.Value = Convert.ToInt32(b.StatusId);
        //        cmd.Parameters.Add(Gid);


        //        SqlParameter hid = new SqlParameter();
        //        hid.ParameterName = "@Discount";
        //        hid.SqlDbType = SqlDbType.Decimal;
        //        hid.Value = b.Discount;
        //        cmd.Parameters.Add(hid);

        //        SqlParameter sid = new SqlParameter();
        //        sid.ParameterName = "@Tax";
        //        sid.SqlDbType = SqlDbType.Decimal;
        //        sid.Value = b.Tax;
        //        cmd.Parameters.Add(sid);

        //        SqlParameter jid = new SqlParameter();
        //        jid.ParameterName = "@Amount";
        //        jid.SqlDbType = SqlDbType.Decimal;
        //        jid.Value = b.Amount;
        //        cmd.Parameters.Add(jid);

        //        SqlParameter pDesc = new SqlParameter();
        //        pDesc.ParameterName = "@TransDate";
        //        pDesc.SqlDbType = SqlDbType.DateTime;
        //        pDesc.Value = b.TransDate;
        //        cmd.Parameters.Add(pDesc);




        //        cmd.ExecuteScalar();
        //        conn.Close();
        //        return new HttpResponseMessage(HttpStatusCode.OK);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (conn != null && conn.State == ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }
        //        string str = ex.Message;
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
        //    }
        //}
        public void Options() { }

    }
}

