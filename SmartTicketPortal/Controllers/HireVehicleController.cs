using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;



using System.Web.Http.Tracing;
using Paysmart.Models;
namespace SmartTicketPortal.Controllers
{
    public class HireVehicleController : ApiController
    {
        [HttpGet]
        [Route("api/HireVehicle/GetHireVehicle")]
        public DataTable GetHireVehicle(int srcId,int destId)
        {
            DataTable Tbl = new DataTable();

            //connect to database
            SqlConnection conn = new SqlConnection();
            //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetHireVehicle";
            cmd.Connection = conn;

            cmd.Parameters.Add("@srcId", SqlDbType.Int).Value = srcId;
            cmd.Parameters.Add("@destId", SqlDbType.Int).Value = destId;
            
            DataSet ds = new DataSet();
            SqlDataAdapter db = new SqlDataAdapter(cmd);
            db.Fill(ds);
            Tbl = ds.Tables[0];

            return Tbl;

        }
    
      
        [Route("api/HireVehicle/SaveBookingDetails")]
        public DataTable SaveBookingDetails(hirevehicle b)
        {
            LogTraceWriter traceWriter = new LogTraceWriter();
            SqlConnection conn = new SqlConnection();
            StringBuilder str = new StringBuilder();
            DataTable dt = new DataTable();
            try
            {
                traceWriter.Trace(Request, "0", TraceLevel.Info, "{0}", "Save HireVehicle Details....");

                str.Append("Username:" + b.Username + ",");
                str.Append("Firstname:" + b.Firstname + ",");
                str.Append("lastname:" + b.lastname + ",");
                str.Append("Email:" + b.Email + ",");

                str.Append("Mobilenumber:" + b.Mobilenumber + ",");
                

                traceWriter.Trace(Request, "0", TraceLevel.Info, "{0}", "Input sent...." + str.ToString());

                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsUpdDelPortalUsers";

                cmd.Connection = conn;

                SqlParameter i = new SqlParameter("@flag", SqlDbType.VarChar);
                i.Value = b.flag;
                cmd.Parameters.Add(i);

                SqlParameter ie = new SqlParameter("@Id", SqlDbType.Int);
                ie.Value = b.Id;
                cmd.Parameters.Add(ie);

                SqlParameter src = new SqlParameter("@Src", SqlDbType.VarChar);
                src.Value = b.Source;
                cmd.Parameters.Add(src);

                SqlParameter dest = new SqlParameter("@Dest", SqlDbType.VarChar, 20);
                dest.Value = b.destination;
                cmd.Parameters.Add(dest);

                SqlParameter co = new SqlParameter("@Firstname", SqlDbType.VarChar);
                co.Value = b.Firstname;
                cmd.Parameters.Add(co);

                SqlParameter cm = new SqlParameter("@lastname", SqlDbType.VarChar, 20);
                cm.Value = b.lastname;
                cmd.Parameters.Add(cm);

                SqlParameter bd = new SqlParameter("@Email", SqlDbType.VarChar,50);
                bd.Value = b.Email;
                cmd.Parameters.Add(bd);

                SqlParameter bt = new SqlParameter("@Mobilenumber", SqlDbType.VarChar);
                bt.Value = b.Mobilenumber;
                cmd.Parameters.Add(bt);

                SqlParameter dd = new SqlParameter("@Holdername", SqlDbType.VarChar);
                dd.Value = b.Holdername;
                cmd.Parameters.Add(dd);

                SqlParameter bc = new SqlParameter("@Cardno", SqlDbType.VarChar, 50);
                bc.Value = b.Cardnumber;
                cmd.Parameters.Add(bc);

                SqlParameter a = new SqlParameter("@ExpMonth", SqlDbType.VarChar);
                a.Value = b.ExpMonth;
                cmd.Parameters.Add(a);

                SqlParameter ps = new SqlParameter("@ExpYear", SqlDbType.VarChar, 50);
                ps.Value = b.ExpYear;
                cmd.Parameters.Add(ps);




                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);


               #region Mobile OTP
//                string eotp = dt.Rows[0]["bookingNumber"].ToString();
//                if (eotp != null)
//                {
//                    try
//                    {
//                        MailMessage mail = new MailMessage();
//                        string emailserver = System.Configuration.ConfigurationManager.AppSettings["emailserver"].ToString();

//                        string username = System.Configuration.ConfigurationManager.AppSettings["username"].ToString();
//                        string pwd = System.Configuration.ConfigurationManager.AppSettings["password"].ToString();
//                        string fromaddress = System.Configuration.ConfigurationManager.AppSettings["fromaddress"].ToString();
//                        string port = System.Configuration.ConfigurationManager.AppSettings["port"].ToString();

//                        SmtpClient SmtpServer = new SmtpClient(emailserver);

//                        mail.From = new MailAddress(fromaddress);
//                        mail.To.Add(fromaddress);
//                        mail.Subject = "Vehicle Registration - Email OTP";
//                        mail.IsBodyHtml = true;

//                        string verifcodeMail = @"<table>
//                                                        <tr>
//                                                            <td>
//                                                                <h2>Thank you for registering with PaySmart APP</h2>
//                                                                <table width=\""760\"" align=\""center\"">
//                                                                    <tbody style='background-color:#F0F8FF;'>
//                                                                        <tr>
//                                                                            <td style=\""font-family:'Zurich BT',Arial,Helvetica,sans-serif;font-size:15px;text-align:left;line-height:normal;background-color:#F0F8FF;\"" >
//<div style='padding:10px;border:#0000FF solid 2px;'>    <br /><br />
//                                                                             
//                                                       Your Vehicle is Booked:<h3>" + eotp + @" </h3>
//
//                                                        If you didn't make this request, <a href='http://154.120.237.198:52800'>click here</a> to cancel.
//
//                                                                                <br/>
//                                                                                <br/>             
//                                                                       
//                                                                                Warm regards,<br>
//                                                                                PAYSMART Customer Service Team<br/><br />
//</div>
//                                                                            </td>
//                                                                        </tr>
//
//                                                                    </tbody>
//                                                                </table>
//                                                            </td>
//                                                        </tr>
//
//                                                    </table>";


//                        mail.Body = verifcodeMail;
//                        //SmtpServer.Port = 465;
//                        //SmtpServer.Port = 587;
//                        SmtpServer.Port = Convert.ToInt32(port);
//                        SmtpServer.UseDefaultCredentials = false;

//                        SmtpServer.Credentials = new System.Net.NetworkCredential(username, pwd);
//                        SmtpServer.EnableSsl = true;
//                        //SmtpServer.TargetName = "STARTTLS/smtp.gmail.com";
//                        SmtpServer.Send(mail);

//                    }
//                    catch (Exception ex)
//                    {
//                        //throw ex;
//                        throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
//                    }
//                }
               #endregion Mobile OTP


                traceWriter.Trace(Request, "0", TraceLevel.Info, "{0}", "SaveBookingDetails successful....");

            }
            catch (Exception ex)
            {
                traceWriter.Trace(Request, "0", TraceLevel.Error, "{0}", "SaveBookingDetails...." + ex.Message.ToString());
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
