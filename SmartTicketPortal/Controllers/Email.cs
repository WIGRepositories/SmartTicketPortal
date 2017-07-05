using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;

namespace Paysmart.Controllers
{
    public class Email : ApiController
    {

        public void sendMail()
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
                mail.To.Add("msrujansimha@gmail.com");
                mail.Subject = "Test Mail";
                mail.Body = "This is for testing SMTP mail from GMAIL";

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
                throw ex;
            }
        }
    }
}
