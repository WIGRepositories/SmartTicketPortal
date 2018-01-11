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
        public int ContactsRequst(contact Co)
        {                    
                try
                {
                    MailMessage mail = new MailMessage();
                    string emailserver = System.Configuration.ConfigurationManager.AppSettings["emailserver"].ToString();
                    string name = Co.name;
                    string email = Co.email;
                    string category = Co.category;
                    string subject = Co.subject;
                    string message = Co.message;
                    string fromaddress = System.Configuration.ConfigurationManager.AppSettings["fromaddress"].ToString();
                    string username = System.Configuration.ConfigurationManager.AppSettings["username"].ToString();
                    string password = System.Configuration.ConfigurationManager.AppSettings["password"].ToString();
                    string port = System.Configuration.ConfigurationManager.AppSettings["port"].ToString();

                    SmtpClient SmtpServer = new SmtpClient(emailserver);

                    mail.From = new MailAddress(Co.email);
                    mail.To.Add(fromaddress);
                    mail.Subject = Co.subject;
                    mail.IsBodyHtml = true;

                    string verifcodeMail = @"<table>
                                                        <tr>
                                                            <td>
                                                                <h2>Thank you for Contacting Smart Ticket Portal</h2>
                                                                <table width=\""760\"" align=\""center\"">
                                                                    <tbody style='background-color:#F0F8FF;'>
                                                                        <tr>
                                                                            <td style=\""font-family:'Zurich BT',Arial,Helvetica,sans-serif;font-size:15px;text-align:left;line-height:normal;background-color:#F0F8FF;\"" >
<div style='padding:10px;border:#0000FF solid 2px;'>    <br /><br />
                                                                             
                                                       Your Name is:<h3>" + name + @"</h3>
                                                       Your Email is:<h3>" + email + @"</h3>
                                                       Your Subject is:<h3>" + subject + @"</h3>   
                                                       Your Category is:<h3>" + category + @"</h3>                                                     
                                                       Your Message is:<h3>" + message + @"</h3>

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

                    SmtpServer.Credentials = new System.Net.NetworkCredential(username,password);
                    SmtpServer.EnableSsl = true;
                    //SmtpServer.TargetName = "STARTTLS/smtp.gmail.com";
                    SmtpServer.Send(mail);
                    return 1;

                }
                catch (Exception ex)
                {
                    //throw ex;
                    return 0;
                }

                return 1;  
        }
        
    }
}