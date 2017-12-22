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
using System.Net.Mail;

namespace SmartTicketPortal.Controllers
{
    public class ContactController : ApiController
    {
        [HttpPost]
        [Route("api/Contact/ContactsRequst")]
        public DataTable ContactsRequst(contact Co)
        {
            
            SqlCommand cmd = new SqlCommand();
            try
            {
                            



                SqlParameter name = new SqlParameter("Name", SqlDbType.VarChar);
                name.Value = Co.name;
                cmd.Parameters.Add(name);
                SqlParameter em = new SqlParameter("email", SqlDbType.VarChar);
                em.Value = Co.email;
                cmd.Parameters.Add(em);
                SqlParameter cat = new SqlParameter("category", SqlDbType.VarChar);
                cat.Value = Co.category;
                cmd.Parameters.Add(cat);
                SqlParameter sub = new SqlParameter("subject", SqlDbType.VarChar);
                sub.Value = Co.subject;
                cmd.Parameters.Add(sub);
                SqlParameter mass = new SqlParameter("message", SqlDbType.VarChar);
                mass.Value = Co.message;
                cmd.Parameters.Add(mass);


            }
            catch
            {
                Exception ex;
            }
            DataTable dt = new DataTable();
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            sd.Fill(dt);
            #region password otp
            string potp = dt.ToString();
            if (potp != null)
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

                    mail.From = new MailAddress(Co.email);
                    mail.To.Add(fromaddress);
                    mail.Subject = "Contact";
                    mail.IsBodyHtml = true;

                    string verifcodeMail = @"<table>
                                                        <tr>
                                                            <td>
                                                                <h2>Thank you for registering with Smart Ticket</h2>
                                                                <table width=\""760\"" align=\""center\"">
                                                                    <tbody style='background-color:#F0F8FF;'>
                                                                        <tr>
                                                                            <td style=\""font-family:'Zurich BT',Arial,Helvetica,sans-serif;font-size:15px;text-align:left;line-height:normal;background-color:#F0F8FF;\"" >
<div style='padding:10px;border:#0000FF solid 2px;'>    <br /><br />
                                                                             
                                                       Your email OTP is:<h3>" + potp + @" </h3>

                                                        If you didn't make this request, <a href='http://154.120.237.198:52800'>click here</a> to cancel.

                                                                                <br/>
                                                                                <br/>             
                                                                       
                                                                                Warm regards,<br>
                                                                                PAYSMART Customer Service Team<br/><br />
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
                    // Status = 1;

                }
                catch (Exception ex)
                {
                    //throw ex;
                }
               
            #endregion password otp
               
            }
            return dt;
        }
    }
}