using SmartTicketPortal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Tracing;

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



        [HttpPost]

        public HttpResponseMessage LicensePayment2(LicensePayments n)
        {

            LogTraceWriter traceWriter = new LogTraceWriter();
            traceWriter.Trace(Request, "0", TraceLevel.Info, "{0}", "SaveLicensePayments credentials....");
            SqlConnection conn = new SqlConnection();
            try
            {
                //connect to database

                // connetionString = "Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password";
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsUpdDelLicensePayments";
                cmd.Connection = conn;

                conn.Open();
                SqlParameter gs = new SqlParameter();
                gs.ParameterName = "@expiryOn";
                gs.SqlDbType = SqlDbType.DateTime;
                gs.Value = Convert.ToString(n.expiryOn);
                cmd.Parameters.Add(gs);

                SqlParameter gsa = new SqlParameter();
                gsa.ParameterName = "@Id";
                gsa.SqlDbType = SqlDbType.Int;
                gsa.Value = Convert.ToString(n.Id);
                cmd.Parameters.Add(gsa);

                SqlParameter gid = new SqlParameter();
                gid.ParameterName = "@licenseFor";
                gid.SqlDbType = SqlDbType.VarChar;
                gid.Value = n.licenseFor;
                cmd.Parameters.Add(gid);

                SqlParameter gsab = new SqlParameter();
                gsab.ParameterName = "@licenseId";
                gsab.SqlDbType = SqlDbType.Int;
                gsab.Value = Convert.ToString(n.licenseId);
                cmd.Parameters.Add(gsab);

                SqlParameter gidb = new SqlParameter();
                gidb.ParameterName = "@licenseType";
                gidb.SqlDbType = SqlDbType.VarChar;
                gidb.Value = n.licenseType;
                cmd.Parameters.Add(gidb);

                SqlParameter guid = new SqlParameter();
                guid.ParameterName = "@paidon";
                guid.SqlDbType = SqlDbType.DateTime;
                guid.Value = Convert.ToString(n.paidon);
                cmd.Parameters.Add(guid);

                SqlParameter guida = new SqlParameter();
                guida.ParameterName = "@renewedon";
                guida.SqlDbType = SqlDbType.DateTime;
                guida.Value = Convert.ToString(n.renewedon);
                cmd.Parameters.Add(guida);

                SqlParameter gidbc = new SqlParameter();
                gidbc.ParameterName = "@transId";
                gidbc.SqlDbType = SqlDbType.VarChar;
                gidbc.Value = n.transId;
                cmd.Parameters.Add(gidbc);

                cmd.ExecuteScalar();
                conn.Close();
                traceWriter.Trace(Request, "0", TraceLevel.Info, "{0}", "SaveLicensePayments Credentials completed.");
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                string str = ex.Message;
                traceWriter.Trace(Request, "1", TraceLevel.Info, "{0}", "Error in SaveLicensePayments:" + ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
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

