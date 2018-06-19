using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using SmartTicketPortal.Models;

namespace SmartTicketPortal.Controllers
{
    public class UserLicensePymtTransactionsController : ApiController
    {

        [HttpPost]
        [Route("api/UserLicensePymtTransactions/UserLicensePymtTransactions")]
        public DataTable UserLicensePymtTransactions(ULPTrasaction pt) {
            DataTable dtl = new DataTable();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();
            try {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsUpdDelUserLicensePymtTransactions";
                cmd.Connection = conn;

                conn.Open();
                SqlParameter id = new SqlParameter("@Id", SqlDbType.Int);
                id.Value = pt.Id;
                cmd.Parameters.Add(id);

                SqlParameter tid = new SqlParameter("@TransId", SqlDbType.VarChar,50);
                tid.Value = pt.TransId;
                cmd.Parameters.Add(tid);

                SqlParameter bt = new SqlParameter("@GatewayTransId", SqlDbType.VarChar, 255);
                bt.Value = pt.GatewayTransId;
                cmd.Parameters.Add(bt);

                SqlParameter td = new SqlParameter("@TransDate", SqlDbType.DateTime);
                td.Value = pt.TransDate;
                cmd.Parameters.Add(td);

                SqlParameter ulpid = new SqlParameter("@ULPymtId", SqlDbType.Int);
                ulpid.Value = pt.ULPymtId;
                cmd.Parameters.Add(ulpid);

                SqlParameter des = new SqlParameter("@Desc", SqlDbType.VarChar,250);
                des.Value = pt.Desc;
                cmd.Parameters.Add(des);

                SqlParameter ta = new SqlParameter("@Tax", SqlDbType.Decimal);
                ta.Value = pt.Tax;
                cmd.Parameters.Add(ta);

                SqlParameter dis = new SqlParameter("@Discount", SqlDbType.Decimal);
                dis.Value = pt.Discount;
                cmd.Parameters.Add(dis);

                SqlParameter ptid = new SqlParameter("@PymtTypeId", SqlDbType.Int);
                ptid.Value = pt.PymtTypeId;
                cmd.Parameters.Add(ptid);

                SqlParameter amt = new SqlParameter("@Amount", SqlDbType.Decimal);
                amt.Value = pt.Amount;
                cmd.Parameters.Add(amt);

                SqlParameter suid = new SqlParameter("@StatusId", SqlDbType.Decimal);
                suid.Value = pt.StatusId;
                cmd.Parameters.Add(suid);

                //SqlParameter lipt = new SqlParameter("@LicensePymtTransId", SqlDbType.VarChar,50);
                //lipt.Value = pt.LicensePymtTransId;
                //cmd.Parameters.Add(lipt);

                SqlParameter ilf = new SqlParameter("@insupddelflag", SqlDbType.VarChar, 50);
                ilf.Value = pt.insupddelflag;
                cmd.Parameters.Add(ilf);

                SqlDataAdapter db = new SqlDataAdapter(cmd);
                db.Fill(dtl);
               
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return dtl;  
        }

    }
}
