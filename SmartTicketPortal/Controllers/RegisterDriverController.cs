using Paysmart.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;

namespace SmartTicketPortal.Controllers
{
    public class RegisterDriverController : ApiController
    {
        [HttpPost]
        [Route("api/RegisterDriver/RegisterDrivers")]
        public DataTable RegisterDrivers(DriverAccount ocr)
        {
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PSInsUpdAppDrivers";

            cmd.Connection = conn;

            SqlParameter f = new SqlParameter("@flag", SqlDbType.VarChar);
            f.Value = ocr.flag;
            cmd.Parameters.Add(f);

            SqlParameter c = new SqlParameter("@Username", SqlDbType.VarChar, 20);
            c.Value = ocr.Username;
            cmd.Parameters.Add(c);

            SqlParameter ce = new SqlParameter("@Email", SqlDbType.VarChar, 50);
            ce.Value = ocr.Email;
            cmd.Parameters.Add(ce);


            SqlParameter cm = new SqlParameter("@Mobilenumber", SqlDbType.VarChar, 20);
            cm.Value = ocr.Mobilenumber;
            cmd.Parameters.Add(cm);

            SqlParameter q1 = new SqlParameter("@Password", SqlDbType.VarChar, 50);
            q1.Value = ocr.Password;
            cmd.Parameters.Add(q1);

            SqlParameter v = new SqlParameter("@Firstname", SqlDbType.VarChar, 50);
            v.Value = ocr.Firstname;
            cmd.Parameters.Add(v);

            SqlParameter v1 = new SqlParameter("@lastname", SqlDbType.VarChar, 50);
            v1.Value = ocr.lastname;
            cmd.Parameters.Add(v1);



            SqlParameter v2 = new SqlParameter("@AuthTypeId", SqlDbType.VarChar, 50);
            v2.Value = ocr.AuthTypeId;
            cmd.Parameters.Add(v2);

            SqlParameter u = new SqlParameter("@AltPhonenumber", SqlDbType.VarChar, 50);
            u.Value = ocr.AltPhonenumber;
            cmd.Parameters.Add(u);

            SqlParameter u1 = new SqlParameter("@Altemail", SqlDbType.VarChar, 50);
            u1.Value = ocr.Altemail;
            cmd.Parameters.Add(u1);

            SqlParameter i = new SqlParameter("@AccountNo", SqlDbType.VarChar, 50);
            i.Value = ocr.AccountNo;
            cmd.Parameters.Add(i);


            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            //[Mobileotp] ,[Emailotp]
            //send email otp\
            #region email opt
            string eotp = dt.Rows[0]["Emailotp"].ToString();
            if (eotp != null)
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
                    mail.To.Add(ocr.Email);
                    mail.Subject = "User registration - Email OTP";
                    mail.IsBodyHtml = true;

                    string verifcodeMail = @"<table>
                                                        <tr>
                                                            <td>
                                                                <h2>Thank you for registering with PaySmart APP</h2>
                                                                <table width=\""760\"" align=\""center\"">
                                                                    <tbody style='background-color:#F0F8FF;'>
                                                                        <tr>
                                                                            <td style=\""font-family:'Zurich BT',Arial,Helvetica,sans-serif;font-size:15px;text-align:left;line-height:normal;background-color:#F0F8FF;\"" >
<div style='padding:10px;border:#0000FF solid 2px;'>    <br /><br />
                                                                             
                                                       Your email OTP is:<h3>" + eotp + @" </h3>

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

                }
                catch (Exception ex)
                {
                    //throw ex;
                }

            }

            //send mobile otp


            // return dt;

            #endregion email otp

            //send mobile otp as SMS
            #region Mobile OTP
            string motp = dt.Rows[0]["Mobileotp"].ToString();
            if (motp != null)
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
                    mail.To.Add(ocr.Email);
                    mail.Subject = "User registration - Mobile OTP";
                    mail.IsBodyHtml = true;

                    string verifcodeMail = @"<table>
                                                        <tr>
                                                            <td>
                                                                <h2>Thank you for registering with PaySmart APP</h2>
                                                                <table width=\""760\"" align=\""center\"">
                                                                    <tbody style='background-color:#F0F8FF;'>
                                                                        <tr>
                                                                            <td style=\""font-family:'Zurich BT',Arial,Helvetica,sans-serif;font-size:15px;text-align:left;line-height:normal;background-color:#F0F8FF;\"" >
<div style='padding:10px;border:#0000FF solid 2px;'>    <br /><br />
                                                                             
                                                       Your Mobile OTP is:<h3>" + motp + @" </h3>

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

                }
                catch (Exception ex)
                {
                    //throw ex;
                }
            }
            #endregion Mobile OTP

            return dt;
        }

        [HttpPost]
        [Route("api/RegisterDriver/MOTPverifications")]
        public int MOTPverifications(UserAccount ocr)
        {

            int status = 0;
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PSDriversMOTPverifying";

            cmd.Connection = conn;


            SqlParameter q1 = new SqlParameter("@Mobilenumber", SqlDbType.VarChar, 20);
            q1.Value = ocr.Mobilenumber;
            cmd.Parameters.Add(q1);

            SqlParameter e = new SqlParameter("@Mobileotp", SqlDbType.VarChar, 10);
            e.Value = ocr.MVerificationCode;
            cmd.Parameters.Add(e);

            try
            {
                conn.Open();
                object statusres = cmd.ExecuteScalar();
                conn.Close();
                if (statusres != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    return Convert.ToInt32(statusres);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //Verify mobile otp

            return status;

        }


        [HttpPost]
        [Route("api/RegisterDriver/EOTPVerification")]

        public int EOTPVerification(UserAccount ocr)
        {
            int status = 0;

            SqlConnection conn = new SqlConnection();


            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PSDriversEOTPverification";

            cmd.Connection = conn;


            SqlParameter m = new SqlParameter("@Mobileno", SqlDbType.VarChar, 20);
            m.Value = ocr.Mobilenumber;
            cmd.Parameters.Add(m);

            SqlParameter q1 = new SqlParameter("@Email", SqlDbType.VarChar, 50);
            q1.Value = ocr.Email;
            cmd.Parameters.Add(q1);

            SqlParameter e = new SqlParameter("@Emailotp", SqlDbType.VarChar, 10);
            e.Value = ocr.EVerificationCode;
            cmd.Parameters.Add(e);

            try
            {
                conn.Open();
                object statusres = cmd.ExecuteScalar();
                conn.Close();
                if (statusres != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    return Convert.ToInt32(statusres);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //Verify mobile otp

            return status;
            //Verify Emailotp
        }



        [HttpPost]
        [Route("api/RegisterDriver/Passwordverification")]
        public DataTable Passwordverification(UserAccount ocr)
        {


            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PSDriversPasswordverification";

            cmd.Connection = conn;


            SqlParameter q1 = new SqlParameter("@Password", SqlDbType.VarChar, 50);
            q1.Value = ocr.Password;
            cmd.Parameters.Add(q1);

            SqlParameter e = new SqlParameter("@Passwordotp", SqlDbType.VarChar, 10);
            e.Value = ocr.Passwordotp;
            cmd.Parameters.Add(e);


            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            return (dt);

            //Verify Passwordotp

        }
    }
}
