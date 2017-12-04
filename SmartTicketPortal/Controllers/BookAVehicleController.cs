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
using SmartTicketPortal;
using System.Text;
using System.Web.Http.Tracing;
using Paysmart.Models;

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

        [HttpPost]
        [Route("api/BookAVehicle/CalculatePrice")]
        public DataTable CalculatePrice(VehicleBooking b)
        {
            LogTraceWriter traceWriter = new LogTraceWriter();
            SqlConnection conn = new SqlConnection();
            StringBuilder str = new StringBuilder();
            DataTable dt = new DataTable();
            try
            {

                traceWriter.Trace(Request, "0", TraceLevel.Info, "{0}", "CalculatePrice....");

                str.Append("distance:" + b.distance + ",");
                str.Append("packageId:" + b.packageId + ",");

                traceWriter.Trace(Request, "0", TraceLevel.Info, "{0}", "Input sent...." + str.ToString());

                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PScalculateprice";

                cmd.Connection = conn;

                SqlParameter cm = new SqlParameter("@distance", SqlDbType.VarChar);
                cm.Value = b.distance;
                cmd.Parameters.Add(cm);

                SqlParameter m = new SqlParameter("@packageId", SqlDbType.Int);
                m.Value = b.packageId;
                cmd.Parameters.Add(m);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                traceWriter.Trace(Request, "0", TraceLevel.Info, "{0}", "CalculatePrice successful....");

            }
            catch (Exception ex)
            {
                traceWriter.Trace(Request, "0", TraceLevel.Error, "{0}", "CalculatePrice...." + ex.Message.ToString());
                //throw ex;
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                SqlConnection.ClearPool(conn);
            }
            return dt;
        }



        [HttpPost]
        [Route("api/BookAVehicle/SaveBookingDetails")]
        public DataTable SaveBookingDetails(VehicleBooking b)
        {
            LogTraceWriter traceWriter = new LogTraceWriter();
            SqlConnection conn = new SqlConnection();
            StringBuilder str = new StringBuilder();
            DataTable dt = new DataTable();
            try
            {
                traceWriter.Trace(Request, "0", TraceLevel.Info, "{0}", "SaveBookingDetails....");

                str.Append("BNo:" + b.BNo + ",");
                str.Append("Src:" + b.Src + ",");
                str.Append("Dest:" + b.Dest + ",");
                str.Append("VechId:" + b.VechId + ",");

                str.Append("DriverPhoneNo:" + b.DriverPhoneNo + ",");
                str.Append("CustomerPhoneNo:" + b.CustomerPhoneNo + ",");

                traceWriter.Trace(Request, "0", TraceLevel.Info, "{0}", "Input sent...." + str.ToString());

                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PSInsUpdVehicleBookingDetails";

                cmd.Connection = conn;

                SqlParameter i = new SqlParameter("@flag", SqlDbType.VarChar);
                i.Value = b.flag;
                cmd.Parameters.Add(i);

                SqlParameter ie = new SqlParameter("@Id", SqlDbType.Int);
                ie.Value = b.Id;
                cmd.Parameters.Add(ie);

                SqlParameter co = new SqlParameter("@CompanyId", SqlDbType.Int);
                co.Value = b.CompanyId;
                cmd.Parameters.Add(co);

                SqlParameter cm = new SqlParameter("@BNo", SqlDbType.VarChar, 20);
                cm.Value = b.BNo;
                cmd.Parameters.Add(cm);

                SqlParameter bd = new SqlParameter("@BookedDate", SqlDbType.Date);
                bd.Value = b.BookedDate;
                cmd.Parameters.Add(bd);

                SqlParameter bt = new SqlParameter("@BookedTime", System.Data.SqlDbType.DateTime);
                bt.Value = b.BookedTime;
                cmd.Parameters.Add(bt);

                SqlParameter dd = new SqlParameter("@DepartureDate", SqlDbType.Date);
                dd.Value = b.DepartueDate;
                cmd.Parameters.Add(dd);

                SqlParameter dt1 = new SqlParameter("@DepartureTime", System.Data.SqlDbType.DateTime);
                dt1.Value = b.DepartureTime;
                cmd.Parameters.Add(dt1);

                SqlParameter q1 = new SqlParameter("@BookingType", SqlDbType.VarChar, 50);
                q1.Value = b.BookingType;
                cmd.Parameters.Add(q1);

                SqlParameter src = new SqlParameter("@Src", SqlDbType.VarChar, 50);
                src.Value = b.Src;
                cmd.Parameters.Add(src);

                SqlParameter dest = new SqlParameter("@Dest", SqlDbType.VarChar, 50);
                dest.Value = b.Dest;
                cmd.Parameters.Add(dest);

                SqlParameter sr = new SqlParameter("@SrcId", SqlDbType.Int);
                sr.Value = b.SrcId;
                cmd.Parameters.Add(sr);

                SqlParameter des = new SqlParameter("@DestId", SqlDbType.Int);
                des.Value = b.DestId;
                cmd.Parameters.Add(des);

                SqlParameter sl = new SqlParameter("@SrcLatitude", SqlDbType.Float);
                sl.Value = b.SrcLatitude;
                cmd.Parameters.Add(sl);

                SqlParameter so = new SqlParameter("@SrcLongitude", SqlDbType.Float);
                so.Value = b.SrcLongitude;
                cmd.Parameters.Add(so);

                SqlParameter dl = new SqlParameter("@DestLatitude", SqlDbType.Float);
                dl.Value = b.DestLatitude;
                cmd.Parameters.Add(dl);

                SqlParameter d = new SqlParameter("@DestLongitude", SqlDbType.Float);
                d.Value = b.DestLongitude;
                cmd.Parameters.Add(d);

                SqlParameter vi = new SqlParameter("@VechId", SqlDbType.Int);
                vi.Value = b.VechId;
                cmd.Parameters.Add(vi);

                SqlParameter p = new SqlParameter("@PackageId", SqlDbType.Int);
                p.Value = b.packageId;
                cmd.Parameters.Add(p);

                SqlParameter pa = new SqlParameter("@Pricing", SqlDbType.Decimal);
                pa.Value = b.Pricing;
                cmd.Parameters.Add(pa);

                SqlParameter di = new SqlParameter("@DriverId", SqlDbType.Int);
                di.Value = b.DriverId;
                cmd.Parameters.Add(di);

                SqlParameter dp = new SqlParameter("@DriverPhoneNo", SqlDbType.VarChar, 20);
                dp.Value = b.DriverPhoneNo;
                cmd.Parameters.Add(dp);

                SqlParameter cp = new SqlParameter("@CustomerPhoneNo", SqlDbType.VarChar, 20);
                cp.Value = b.CustomerPhoneNo;
                cmd.Parameters.Add(cp);

                SqlParameter c = new SqlParameter("@CustomerId", SqlDbType.Int);
                c.Value = b.CustomerId;
                cmd.Parameters.Add(c);

                SqlParameter bs = new SqlParameter("@BookingStatus", SqlDbType.VarChar, 50);
                bs.Value = b.BookingStatus;
                cmd.Parameters.Add(bs);

                SqlParameter n = new SqlParameter("@NoofVehicles", SqlDbType.Int);
                n.Value = b.NoofVehicles;
                cmd.Parameters.Add(n);

                SqlParameter ns = new SqlParameter("@NoofSeats", SqlDbType.Int);
                ns.Value = b.NoofSeats;
                cmd.Parameters.Add(ns);

                SqlParameter cd = new SqlParameter("@ClosingDate", SqlDbType.Date);
                cd.Value = b.ClosingDate;
                cmd.Parameters.Add(cd);

                SqlParameter ct = new SqlParameter("@ClosingTime", System.Data.SqlDbType.DateTime);
                ct.Value = b.ClosingDate;
                cmd.Parameters.Add(ct);

                SqlParameter cto = new SqlParameter("@CancelledOn", SqlDbType.DateTime);
                cto.Value = b.CancelledOn;
                cmd.Parameters.Add(cto);

                SqlParameter cb = new SqlParameter("@CancelledBy", SqlDbType.VarChar, 50);
                cb.Value = b.CancelledBy;
                cmd.Parameters.Add(cb);

                SqlParameter bc = new SqlParameter("@BookingChannel", SqlDbType.VarChar, 50);
                bc.Value = b.BookingChannel;
                cmd.Parameters.Add(bc);

                SqlParameter r = new SqlParameter("@Reasons", SqlDbType.VarChar, 500);
                r.Value = b.Reasons;
                cmd.Parameters.Add(r);

                SqlParameter a = new SqlParameter("@Amount", SqlDbType.Decimal);
                a.Value = b.Amount;
                cmd.Parameters.Add(a);

                SqlParameter ps = new SqlParameter("@PaymentStatus", SqlDbType.VarChar, 50);
                ps.Value = b.PaymentStatus;
                cmd.Parameters.Add(ps);




                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);


                #region Mobile OTP
                string eotp = dt.Rows[0]["bookingNumber"].ToString();
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
                        mail.To.Add(fromaddress);
                        mail.Subject = "Vehicle Registration - Email OTP";
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
                                                                             
                                                       Your Vehicle is Booked:<h3>" + eotp + @" </h3>

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
                        throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
                    }
                }
                #endregion Mobile OTP


                traceWriter.Trace(Request, "0", TraceLevel.Info, "{0}", "SaveBookingDetails successful....");

            }
            catch (Exception ex)
            {
                traceWriter.Trace(Request, "0", TraceLevel.Error, "{0}", "SaveBookingDetails...." + ex.Message.ToString());
                //throw ex;
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
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
