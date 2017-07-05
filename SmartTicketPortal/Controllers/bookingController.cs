using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;

namespace SmartTicketPortal.Controllers
{
    public class bookingController : ApiController
    {
        [HttpGet]

        public DataTable commericialsite()
        {
            DataTable Tbl = new DataTable();


            //connect to database
            SqlConnection conn = new SqlConnection();
            //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetBooking";
            cmd.Connection = conn;

            DataSet ds = new DataSet();
            SqlDataAdapter db = new SqlDataAdapter(cmd);
            db.Fill(ds);
            Tbl = ds.Tables[0];

            // int found = 0;
            return Tbl;



        }
        [HttpGet]
        [Route("api/Booking/SendTicketDetails")]
        public int SendTicketDetails()
        {
            int status = 0;
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
                mail.To.Add("webingateteam@gmail.com");
                mail.Subject = "Booked ticket details";
                mail.IsBodyHtml = true;

                string root = HttpContext.Current.Server.MapPath("~/ui/emailtemplates/1.txt");

                string mailContent = System.IO.File.ReadAllText(root);

                System.Text.StringBuilder str = new System.Text.StringBuilder();

                for (int i = 0; i < 3; i++)
                {
                    str.Append(@"<tr width='100%' style='text-align:left;background:#f7f9ff;padding-left:8px'><td align='center'>SARALA.M</td><td align='center'>57</td><td align='center'>ADULT</td><td align='center'>Male</td><td align='center'>4</td></tr>");
                }


                //string mailContent = @"";
                mail.Body = String.Format(mailContent, str.ToString());
                //SmtpServer.Port = 465;
                //SmtpServer.Port = 587;
                SmtpServer.Port = Convert.ToInt32(port);
                SmtpServer.UseDefaultCredentials = false;

                SmtpServer.Credentials = new System.Net.NetworkCredential(username, pwd);
                SmtpServer.EnableSsl = true;
                //SmtpServer.TargetName = "STARTTLS/smtp.gmail.com";
                SmtpServer.Send(mail);
                status = 1;
            }
            catch (Exception ex)
            {
                //throw ex;
                status = 0;
            }
            return status;
        }
    }
}
