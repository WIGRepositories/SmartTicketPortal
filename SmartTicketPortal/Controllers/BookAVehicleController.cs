using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using SmartTicketPortal.Models;

namespace SmartTicketDashboard.Controllers
{
    public class BookAVehicleController : ApiController
    {

        public DataTable GetAvailablebooking(int srcId, int destId)
        {
            DataTable Tbl = new DataTable();


            //connect to database
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetAvailableServices";
            cmd.Parameters.Add("@srcId", SqlDbType.Int).Value = srcId;
            cmd.Parameters.Add("@destId", SqlDbType.Int).Value = destId;
            cmd.Connection = conn;

            DataSet ds = new DataSet();
            SqlDataAdapter db = new SqlDataAdapter(cmd);
            db.Fill(ds);
            Tbl = ds.Tables[0];
            int a = Tbl.Rows.Count;

            return Tbl;
        }
        [HttpGet]
        [Route("api/BookAVehicle/GetBookingHistory")]
        public DataTable GetBookingHistory(string PhoneNo)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "HVGetHistory";
            cmd.Parameters.Add("@PhoneNo", SqlDbType.VarChar,50).Value = PhoneNo;
            cmd.Connection = conn;
            DataSet ds = new DataSet();
            SqlDataAdapter db = new SqlDataAdapter(cmd);
            db.Fill(ds);
            dt = ds.Tables[0];

            return dt;

        }


        [HttpPost]
        [Route("api/BookAVehicle/booking")]
        public int booking(UserLocation b)
        {
            int Status = 0;
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "HVInsUpdbooking";

            cmd.Connection = conn;

            SqlParameter i = new SqlParameter("@flag", SqlDbType.VarChar);
            i.Value = b.flag;
            cmd.Parameters.Add(i);

            SqlParameter cm = new SqlParameter("@BNo", SqlDbType.Int);
            cm.Value = b.BNo;
            cmd.Parameters.Add(cm);

            SqlParameter q1 = new SqlParameter("@BookingType", SqlDbType.VarChar, 255);
            q1.Value = b.BookingType;
            cmd.Parameters.Add(q1);

            SqlParameter v = new SqlParameter("@ReqVehicle", SqlDbType.VarChar, 255);
            v.Value = b.ReqVehicle;
            cmd.Parameters.Add(v);

            SqlParameter v1 = new SqlParameter("@Customername", SqlDbType.VarChar, 255);


            v1.Value = b.Customername;
            cmd.Parameters.Add(v1);


            SqlParameter v2 = new SqlParameter("@CusID", SqlDbType.VarChar, 255);
            v2.Value = b.CusID;
            cmd.Parameters.Add(v2);


            SqlParameter f = new SqlParameter("@PhoneNo", SqlDbType.VarChar, 50);
            f.Value = b.PhoneNo;
            cmd.Parameters.Add(f);

            SqlParameter A = new SqlParameter("@AltPhoneNo", SqlDbType.VarChar, 255);
            A.Value = b.AltPhoneNo;
            cmd.Parameters.Add(A);

            SqlParameter C = new SqlParameter("@CAddress", SqlDbType.NVarChar);
            C.Value = b.CAddress;
            cmd.Parameters.Add(C);

            SqlParameter P = new SqlParameter("@PickupAddress", SqlDbType.VarChar, 255);
            P.Value = b.PickupAddress;
            cmd.Parameters.Add(P);

            SqlParameter P1 = new SqlParameter("@LandMark", SqlDbType.VarChar, 255);
            P1.Value = b.LandMark;
            cmd.Parameters.Add(P1);


            SqlParameter P2 = new SqlParameter("@Package", SqlDbType.VarChar, 255);
            P2.Value = b.Package;
            cmd.Parameters.Add(P2);



            SqlParameter D1 = new SqlParameter("@PickupPalce", SqlDbType.VarChar, 255);
            D1.Value = b.PickupPalce;
            cmd.Parameters.Add(D1);

            SqlParameter D = new SqlParameter("@DropPalce", SqlDbType.VarChar, 255);
            D.Value = b.DropPalce;
            cmd.Parameters.Add(D);

            SqlParameter r = new SqlParameter("@ReqType", SqlDbType.VarChar, 255);
            r.Value = b.ReqType;
            cmd.Parameters.Add(r);

            SqlParameter E = new SqlParameter("@ExtraCharge", SqlDbType.Int);
            E.Value = b.ExtraCharge;
            cmd.Parameters.Add(E);

            SqlParameter N = new SqlParameter("@NoofVehicle", SqlDbType.Int);
            N.Value = b.NoofVehicle;
            cmd.Parameters.Add(N);

            SqlParameter rt = new SqlParameter("@ExecutiveName", SqlDbType.VarChar, 255);
            rt.Value = b.ExecutiveName;
            cmd.Parameters.Add(rt);

            SqlParameter vi = new SqlParameter("@VID", SqlDbType.Int);
            vi.Value = b.VID;
            cmd.Parameters.Add(vi);

            SqlParameter bs = new SqlParameter("@BookingStatus", SqlDbType.VarChar, 255);
            bs.Value = b.BookingStatus;
            cmd.Parameters.Add(bs);

            SqlParameter cs = new SqlParameter("@CustomerSMS", SqlDbType.VarChar, 255);
            cs.Value = b.CustomerSMS;
            cmd.Parameters.Add(cs);

            SqlParameter cr = new SqlParameter("@CancelReason", SqlDbType.VarChar, 255);
            cr.Value = b.CancelReason;
            cmd.Parameters.Add(cr);

            SqlParameter cb = new SqlParameter("@CBNo", SqlDbType.VarChar, 255);
            cb.Value = b.CBNo;
            cmd.Parameters.Add(cb);

            SqlParameter mb = new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 255);
            mb.Value = b.ModifiedBy;
            cmd.Parameters.Add(mb);

            SqlParameter cby = new SqlParameter("@CancelBy", SqlDbType.VarChar, 255);
            cby.Value = b.CancelBy;
            cmd.Parameters.Add(cby);


            SqlParameter rb = new SqlParameter("@ReconfirmedBy", SqlDbType.VarChar, 255);
            rb.Value = b.ReconfirmedBy;
            cmd.Parameters.Add(rb);


            SqlParameter s = new SqlParameter("@AssignedBy", SqlDbType.VarChar, 255);
            s.Value = b.AssignedBy;
            cmd.Parameters.Add(s);

            SqlParameter c = new SqlParameter("@latitude", SqlDbType.Float);
            c.Value = b.lat;
            cmd.Parameters.Add(c);

            SqlParameter ce = new SqlParameter("@longitude", SqlDbType.Float);
            ce.Value = b.lng;
            cmd.Parameters.Add(ce);




            conn.Open();
            object motpStr = cmd.ExecuteScalar();
            conn.Close();



            //[Mobileotp] ,[Emailotp]
            //send email otp\

            #region Mobile OTP
            string motp = motpStr.ToString();
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
                    Status = 1;

                }
                catch (Exception ex)
                {
                    //throw ex;
                }
            }
            #endregion Mobile OTP

            return Status;

        }
    }

        
 
}
