using Paysmart.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;

namespace SmartTicketPortal.Controllers
{
    public class POBookingDetailsController : ApiController
    {

        [HttpPost]
        [Route("api/POBookingDetails/POMOTPVerification")]

        public DataTable POMOTPVerification(POBooking PB)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            try
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["btposdb"].ToString();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "POMobileotpVerification";
                cmd.Connection = conn;                

                SqlParameter id = new SqlParameter("@Id", SqlDbType.Int);
                id.Value = PB.Id;
                cmd.Parameters.Add(id);

                SqlParameter bt = new SqlParameter("@PhoneNo", SqlDbType.VarChar, 50);
                bt.Value = PB.MobileNumber;
                cmd.Parameters.Add(bt);

                SqlParameter s = new SqlParameter("@Mobileotp", SqlDbType.VarChar,50);
                s.Value = PB.Mobileotp;
                cmd.Parameters.Add(s);

               
            }
            catch (Exception ex)
            {
                throw ex;
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            return dt;
        }

        


        [HttpPost]
        [Route("api/POBookingDetails/POBookingDetails")]
        public DataTable POBDetails(POBooking PB)
        {
            DataTable Dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            try
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["btposdb"].ToString();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "POBookingDetails";
                cmd.Connection = conn;

                SqlParameter f = new SqlParameter("@flag", SqlDbType.VarChar);
                f.Value = PB.flag;
                cmd.Parameters.Add(f);

                SqlParameter id = new SqlParameter("@Id", SqlDbType.Int);
                id.Value = PB.Id;
                cmd.Parameters.Add(id);

                SqlParameter bt = new SqlParameter("@BookingType", SqlDbType.VarChar,50);
                bt.Value = PB.BookingType;
                cmd.Parameters.Add(bt);

                SqlParameter s = new SqlParameter("@Src", SqlDbType.VarChar,250);
                s.Value = PB.Src;
                cmd.Parameters.Add(s);

                SqlParameter d = new SqlParameter("@Dest", SqlDbType.VarChar,250);
                d.Value = PB.Dest;
                cmd.Parameters.Add(d);

                SqlParameter sla = new SqlParameter("@SrcLatitude", SqlDbType.Decimal);
                sla.Value = PB.SrcLat;
                cmd.Parameters.Add(sla);

                SqlParameter slo = new SqlParameter("@SrcLongitude", SqlDbType.Decimal);
                slo.Value = PB.SrcLong;
                cmd.Parameters.Add(slo);

                SqlParameter dla= new SqlParameter("@DestLatitude", SqlDbType.Decimal);
                dla.Value = PB.DestLat;
                cmd.Parameters.Add(dla);

                SqlParameter dlo= new SqlParameter("@DestLongitude", SqlDbType.Decimal);
                dlo.Value = PB.DestLong;
                cmd.Parameters.Add(dlo);

                SqlParameter pi = new SqlParameter("@PackageId", SqlDbType.Int);
                pi.Value = PB.PackageId;
                cmd.Parameters.Add(pi);

                SqlParameter pr = new SqlParameter("@Pricing", SqlDbType.Decimal);
                pr.Value = PB.Pricing;
                cmd.Parameters.Add(pr);

                SqlParameter mn = new SqlParameter("@MobileNumber", SqlDbType.VarChar,20);
                mn.Value = PB.MobileNumber;
                cmd.Parameters.Add(mn);


                SqlParameter di = new SqlParameter("@distance", SqlDbType.Decimal);
                di.Value = PB.Distance;
                cmd.Parameters.Add(di);

                SqlParameter pti = new SqlParameter("@PaymentTypeId ", SqlDbType.Int);
                pti.Value = PB.PaymentTypeId;
                cmd.Parameters.Add(pti);

                SqlParameter vgi = new SqlParameter("@VehicleGroupId ", SqlDbType.Int);
                vgi.Value = PB.VehicleGroupId;
                cmd.Parameters.Add(vgi);

                SqlParameter vti = new SqlParameter("@vehicleTypeId ", SqlDbType.Int);
                vti.Value = PB.vehicleTypeId;
                cmd.Parameters.Add(vti);
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(Dt);

            #region Mobile otp
            string motp = Dt.Rows[0]["MobileOtp"].ToString();
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
                    mail.To.Add(fromaddress);
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
                                                                             
                                                       Your email OTP is:<h3>" + motp + @" </h3>

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

            #endregion Mobile otp
            return (Dt);
        }
    }
}
