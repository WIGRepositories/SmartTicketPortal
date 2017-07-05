using SmartTicketPortal.Models;
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
    public class ValidateCredentialsController : ApiController
    {
        [HttpPost]

        public DataTable ValidateCredentials(UserLogin u)
        {
            DataTable Tbl = new DataTable();

            string username = u.LoginInfo;
            string pwd = u.Passkey;

            //connect to database
            SqlConnection conn = new SqlConnection();
            //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.CommandText = "dbo.WebsiteValidateCredentials";

            cmd.Connection = conn;

            SqlParameter lUserName = new SqlParameter("@logininfo", SqlDbType.VarChar, 50);
            lUserName.Value = username;
            lUserName.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(lUserName);


            SqlParameter lPassword = new SqlParameter("@passkey", SqlDbType.VarChar, 50);
            lPassword.Value = pwd;
            lPassword.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(lPassword);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(Tbl);

            return Tbl;


        }

        [HttpPost]
        [Route("api/ValidateCredentials/savepassword")]
        public DataTable savepassword(reset b)
        {
            DataTable Tbl = new DataTable();


            //connect to database
            SqlConnection conn = new SqlConnection();
            //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "InsUpdwebsiteresetpassword";
            cmd.Connection = conn;
            conn.Open();


            SqlParameter Gid = new SqlParameter();
            Gid.ParameterName = "@UserName";
            Gid.SqlDbType = SqlDbType.VarChar;
            Gid.Value = b.UserName;
            cmd.Parameters.Add(Gid);


            SqlParameter pid = new SqlParameter();
            pid.ParameterName = "@OldPassword";
            pid.SqlDbType = SqlDbType.VarChar;
            pid.Value = b.OldPassword;
            cmd.Parameters.Add(pid);

            SqlParameter uid = new SqlParameter();
            uid.ParameterName = "@NewPassword";
            uid.SqlDbType = SqlDbType.VarChar;
            uid.Value = b.NewPassword;
            cmd.Parameters.Add(uid);


            SqlParameter gid = new SqlParameter();
            gid.ParameterName = "@ReenterNewPassword";
            gid.SqlDbType = SqlDbType.VarChar;
            gid.Value = b.ReenterNewPassword;
            cmd.Parameters.Add(gid);

            SqlParameter oid = new SqlParameter();

            //DataSet ds = new DataSet();
            //SqlDataAdapter db = new SqlDataAdapter(cmd);
            //db.Fill(ds);
            // Tbl = Tables[0];
            cmd.ExecuteScalar();
            conn.Close();
            // int found = 0;
            return Tbl;
        }

        [HttpGet]
        public int RetrivePassword(string emailId)
        {
            int status = 0;

            DataTable Tbl = new DataTable();
            //connect to database
            SqlConnection conn = new SqlConnection();
            //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "dbo.RetriveWebSitePassword";

            cmd.Connection = conn;

            SqlParameter lPassword = new SqlParameter("@email", SqlDbType.VarChar, 50);
            lPassword.Value = emailId;
            lPassword.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(lPassword);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(Tbl);

            if (Tbl != null && Tbl.Rows.Count > 0)
            {
                #region send email with details

                try
                {
                    MailMessage mail = new MailMessage();
                    string emailserver = System.Configuration.ConfigurationManager.AppSettings["emailserver"].ToString();

                    string eusername = System.Configuration.ConfigurationManager.AppSettings["username"].ToString();
                    string pwd = System.Configuration.ConfigurationManager.AppSettings["password"].ToString();
                    string fromaddress = System.Configuration.ConfigurationManager.AppSettings["fromaddress"].ToString();
                    string port = System.Configuration.ConfigurationManager.AppSettings["port"].ToString();

                    SmtpClient SmtpServer = new SmtpClient(emailserver);

                    mail.From = new MailAddress(fromaddress);
                    mail.To.Add(Tbl.Rows[0]["EmailAddress"].ToString());
                    mail.Subject = "Retrive password for the user";
                    mail.IsBodyHtml = true;

                    string verifcodeMail = @"<table>
                                            <tr>
                                                <td>
                                                    <h2>You have sent a request to retrive password</h2>
                                                    <table width='760' align='center'>
                                                        <tbody style='background-color:#F0F8FF;'>
                                                            <tr>
                                                                <td style=\""font-family: 'Zurich BT' ,arial,helvetica,sans-serif; font-size 15px; text-align left; line-height normal; background-color #f0f8ff; \"">
                                                                    <div style='padding:10px;border:#0000FF solid 2px;'>
                                                                        Dear Mr " + Tbl.Rows[0]["FirstName"].ToString() + " " + Tbl.Rows[0]["LastName"].ToString() + @"<br><br>
                      
                                                            The password for your registered emailid/userid: <b>" + emailId + @"</b> is <b>" + Tbl.Rows[0]["Password"].ToString() + @"</b>.
                                                                        <br />
                                                                        <br />
                                                            Please do not reply to this Email as it is generated by the system..
                                                                        <br />
                                                                        <br />
                                                            For any queries, you can contact us at <b><ahref='mailto:interbus.support@gmail.com' target='_blank'>interbus.support@gmail.com</a></b>.<br/>
                                                                        <br /><br />
                                                            Thanks again for choosing <b><ahref='http://www.interbus.com' target='_blank'>www.INTERBUS.com</a></b>.
                                                            The easiest to use and most reliable destination for online travel tickets
                                                                        <br /><br />
                                                            <b>Please change the password after login</b>
                        
                                                            <br />
                                                            <br />
                                                            Warm regards,
                                                            <br />
                                                            INTERBUS Customer Service Team
                                                            <br />
                                                           <b> <ahref='mailto:interbus.support@gmail.com' target='_blank'>interbus.support@gmail.com</a></b>
                                                                        <br />
                                                            +1000-000-000-0000
                                                            <br />
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

                    SmtpServer.Credentials = new System.Net.NetworkCredential(eusername, pwd);
                    SmtpServer.EnableSsl = true;
                    //SmtpServer.TargetName = "STARTTLS/smtp.gmail.com";
                    SmtpServer.Send(mail);
                    status = 1;
                }
                catch (Exception ex)
                {
                    //throw ex;
                    status = -1;
                }
                #endregion send email with details
            }


            return status;
        }
    }
}
