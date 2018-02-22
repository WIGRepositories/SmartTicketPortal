using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using Paysmart.Models;

namespace SmartTicketPortal.Controllers
{
    public class GetaLyftController : ApiController
    {


        [HttpPost]
        [Route("api/GetaLyft/GetaLyftOtpgeneration")]
        public DataTable GetaLyftOtpgeneration(getalyft b)
        {

            DataTable Tbl = new DataTable();
            //// int userid = b.Id;
            // //connect to database
            SqlConnection conn = new SqlConnection();
            try
            {
                //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();


                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PSInsUpdVehicleBookingDetails";
                cmd.Connection = conn;
                //     conn.Open();       

                SqlParameter id = new SqlParameter("@CustomerPhoneNo", SqlDbType.VarChar, 50);
                id.Value = b.Mobilenumber;
                cmd.Parameters.Add(id);

                SqlParameter emailVerificationCode = new SqlParameter("@BooKingOTP", SqlDbType.VarChar, 15);
                emailVerificationCode.Value = null;
                emailVerificationCode.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(emailVerificationCode);


                //  conn.Open();

                //   object outputid = cmd.ExecuteScalar();

                //  conn.Close();
                //  userid = (int)outputid;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(Tbl);

                //retrive the code and send email to the user
                object val = emailVerificationCode.Value;
                if (val != null)
                {
                    try
                    {
                        MailMessage mail = new MailMessage();
                        string emailserver = System.Configuration.ConfigurationManager.AppSettings["emailserver"].ToString();

                        string username = System.Configuration.ConfigurationManager.AppSettings["username"].ToString();
                        string pwd = System.Configuration.ConfigurationManager.AppSettings["password"].ToString();
                        string fromaddress = System.Configuration.ConfigurationManager.AppSettings["fromaddress"].ToString();
                        string port = System.Configuration.ConfigurationManager.AppSettings["port"].ToString();

                        SmtpClient SmtpServer = new SmtpClient(emailserver);

                        mail.From = new MailAddress(fromaddress);
                        mail.To.Add(fromaddress);
                        mail.Subject = "User registration";
                        mail.IsBodyHtml = true;

                        string verifcodeMail = @"<table>
                                                        <tr>
                                                            <td>
                                                                <h2>Thank you for registering with INTERBUS</h2>
                                                                <table width=\""760\"" align=\""center\"">
                                                                    <tbody style='background-color:#F0F8FF;'>
                                                                        <tr>
                                                                            <td style=\""font-family:'Zurich BT',Arial,Helvetica,sans-serif;font-size:15px;text-align:left;line-height:normal;background-color:#F0F8FF;\"" >
<div style='padding:10px;border:#0000FF solid 2px;'>                                                                                
<h3>Congratulations!!</h3>
                                                                                <h4>You have been successfully registered with INTERBUS </h4>
                                                                                <h3>Verify your email address</h3>
                                                                                <br />
                                                                                To finish setting up this INTERBUS account, we just need to make sure this email address is yours.

                                                                                <br /><br />
                                                                                <a href='http://154.120.237.198:52800' style=\""background-color: #2672ec;padding:10px\""><b> Verify &lt; sample email address &gt; </b></a>
                                                                                <br /><br />
                                                        Or you may be asked to enter this security code:<h3>E000000 </h3>

                                                        If you didn't make this request, <a href='http://154.120.237.198:52800'>click here</a> to cancel.

                                                                                <br/>
                                                                                <br/>             
                                                                       
                                                                                Warm regards,<br>
                                                                                INTERBUS Customer Service Team<br/><br />
</div>
                                                                            </td>
                                                                        </tr>

                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>

                                                    </table>";


                        mail.Body = verifcodeMail;
                        //SmtpServer.Port = 465;
                        //SmtpServer.Port = 587;
                        SmtpServer.Port = Convert.ToInt32(port);
                        SmtpServer.UseDefaultCredentials = false;

                        SmtpServer.Credentials = new System.Net.NetworkCredential(username, pwd);
                        SmtpServer.EnableSsl = true;
                        //SmtpServer.TargetName = "STARTTLS/smtp.gmail.com";
                        SmtpServer.Send(mail);

                    }
                    catch (Exception ex)
                    {
                        //throw ex;
                    }

                }


                return Tbl;
            }

            catch (Exception ex)
            {
                //if (conn.State == ConnectionState.Open)
                //{
                //    conn.Close();
                //}
                return Tbl;
            }
        }
            
    }
}
