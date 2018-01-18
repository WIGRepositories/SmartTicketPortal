using Paysmart.Models;
using SmartTicketPortal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web.Http;

namespace SmartTicketPortal.Controllers
{
    public class PricingController : ApiController
    {
        [HttpGet]
        [Route("api/Pricing/CalculatePrice")]
        public DataTable CalculatePrice(decimal distance, int packageId)
        {
            LogTraceWriter traceWriter = new LogTraceWriter();
            SqlConnection conn = new SqlConnection();
            StringBuilder str = new StringBuilder();
            DataTable dt = new DataTable();
            try
            {

                //traceWriter.Trace(Request, "0", TraceLevel.Info, "{0}", "CalculatePrice....");

                //traceWriter.Trace(Request, "0", TraceLevel.Info, "{0}", "Input sent...." + str.ToString());

                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PSGetTripCost";

                cmd.Connection = conn;

                cmd.Parameters.Add("@distance", SqlDbType.Decimal).Value = distance;
                cmd.Parameters.Add("@packageId", SqlDbType.Decimal).Value = packageId;


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                //traceWriter.Trace(Request, "0", TraceLevel.Info, "{0}", "CalculatePrice successful....");

            }
            catch (Exception ex)
            {
                traceWriter.Trace(Request, "0", TraceLevel.Error, "{0}", "CalculatePrice...." + ex.Message.ToString());
                throw ex;
                //throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                SqlConnection.ClearPool(conn);
            }
            return dt;
        }
       
    }
}
