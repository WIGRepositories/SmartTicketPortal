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
    public class UserLicensesController : ApiController
    {
        [HttpGet]
        [Route("api/UserLicenses/getUserLicenses")]
        public DataTable getUserLicenses()
        {
            DataTable Tbl = new DataTable();


            //connect to database
            SqlConnection conn = new SqlConnection();
            //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetUserLicense";
            cmd.Connection = conn;

            DataSet ds = new DataSet();
            SqlDataAdapter db = new SqlDataAdapter(cmd);
            db.Fill(ds);
            Tbl = ds.Tables[0];

            // int found = 0;
            return Tbl;



        }

        [HttpGet]
        [Route("api/UserLicenses/getFleetLicenses")]
        public DataTable getFleetLicenses(string fleetcode)
        {
            DataTable Tbl = new DataTable();


            //connect to database
            SqlConnection conn = new SqlConnection();
            //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetfleetownerLicense";
            cmd.Connection = conn;
            cmd.Parameters.Add("@fleetcode", SqlDbType.VarChar).Value = fleetcode;
            DataSet ds = new DataSet();
            SqlDataAdapter db = new SqlDataAdapter(cmd);
            db.Fill(ds);
            Tbl = ds.Tables[0];

            // int found = 0;
            return Tbl;



        }

        [HttpPost]
        [Route("api/UserLicenses/SaveUserLicenseDetails")]
        public DataTable SaveUserLicenseDetails(UserLicenseDetails userlicense)
        {
            SqlConnection conn = new SqlConnection();
            DataTable Tbl = new DataTable();
            try
            {
                //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

                SqlCommand cmd1 = new SqlCommand();
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.CommandText = "InsUpdDelUserLicense";
                cmd1.Connection = conn;
                conn.Open();


                SqlParameter vid1 = new SqlParameter();
                vid1.ParameterName = "@UserId";
                vid1.SqlDbType = SqlDbType.Int;
                vid1.Value = userlicense.UserId;
                cmd1.Parameters.Add(vid1);

                SqlParameter ccd1 = new SqlParameter();
                ccd1.ParameterName = "@FOId";
                ccd1.SqlDbType = SqlDbType.Int;
                ccd1.Value = userlicense.FOId;
                cmd1.Parameters.Add(ccd1);

                SqlParameter pu = new SqlParameter();
                pu.ParameterName = "@LicenseTypeId";
                pu.SqlDbType = SqlDbType.Int;
                pu.Value = userlicense.LicenseTypeId;
                cmd1.Parameters.Add(pu);

                SqlParameter pid = new SqlParameter();
                pid.ParameterName = "@GracePeriod";
                pid.SqlDbType = SqlDbType.Int;
                pid.Value = userlicense.GracePeriod;
                cmd1.Parameters.Add(pid);

                SqlParameter fd1 = new SqlParameter();
                fd1.ParameterName = "@ActualExpiry";
                fd1.SqlDbType = SqlDbType.DateTime;
                fd1.Value = userlicense.ActualExpiry;
                cmd1.Parameters.Add(fd1);



                SqlParameter fid = new SqlParameter();
                fid.ParameterName = "@StartDate";
                fid.SqlDbType = SqlDbType.DateTime;
                fid.Value = userlicense.StartDate;
                cmd1.Parameters.Add(fid);

                SqlParameter sid = new SqlParameter();
                sid.ParameterName = "@LastUpdatedOn";
                sid.SqlDbType = SqlDbType.DateTime;
                sid.Value = userlicense.LastUpdatedOn;
                cmd1.Parameters.Add(sid);

                SqlParameter td1 = new SqlParameter();
                td1.ParameterName = "@ExpiryOn";
                td1.SqlDbType = SqlDbType.DateTime;
                td1.Value = userlicense.ExpiryOn;
                cmd1.Parameters.Add(td1);

                SqlParameter hid = new SqlParameter();
                hid.ParameterName = "@StatusId";
                hid.SqlDbType = SqlDbType.Int;
                hid.Value = userlicense.StatusId;
                cmd1.Parameters.Add(hid);


                SqlParameter nActive = new SqlParameter("@Active", SqlDbType.Int);
                nActive.Value = userlicense.Active;
                cmd1.Parameters.Add(nActive);

                SqlParameter yid = new SqlParameter();
                yid.ParameterName = "@RenewFreqTypeId";
                yid.SqlDbType = SqlDbType.Int;
                yid.Value = userlicense.RenewFreqTypeId;
                cmd1.Parameters.Add(yid);

                SqlParameter flag = new SqlParameter();
                flag.ParameterName = "@insupddelflag";
                flag.SqlDbType = SqlDbType.VarChar;
                flag.Value = userlicense.insupddelflag;
                cmd1.Parameters.Add(flag);


                SqlDataAdapter db = new SqlDataAdapter(cmd1);
                db.Fill(Tbl);

                return Tbl;
            }
            catch (Exception ex)
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                string str = ex.Message;
                return Tbl;
            }
        }

        [HttpPost]
        [Route("api/UserLicenses/SaveUserLicensePayment")]
        public DataSet SaveUserLicensePayment(ULLicense n)
        {
            SqlConnection conn = new SqlConnection();
            DataSet ds = new DataSet();
            try
            {
                // connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

                SqlCommand cmd1 = new SqlCommand();
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.CommandText = "InsUpdDelUserLicensePayments";
                cmd1.Connection = conn;
                conn.Open();

                SqlParameter aid = new SqlParameter();
                aid.ParameterName = "@ULId";
                aid.SqlDbType = SqlDbType.Int;
                aid.Value = n.ULId;
                cmd1.Parameters.Add(aid);

                SqlParameter trid = new SqlParameter();
                trid.ParameterName = "@TransId";
                trid.SqlDbType = SqlDbType.VarChar;
                trid.Value = n.TransId;
                //trid.Value = "TR"+Common.GetRandomInvoiceNumber();
                cmd1.Parameters.Add(trid);

                SqlParameter s1id = new SqlParameter();
                s1id.ParameterName = "@StatusId";
                s1id.SqlDbType = SqlDbType.Int;
                s1id.Value = n.StatusId;
                cmd1.Parameters.Add(s1id);

                SqlParameter cci = new SqlParameter();
                cci.ParameterName = "@LicensePymtTransId";
                cci.SqlDbType = SqlDbType.Int;
                cci.Value = n.LicensePymtTransId;
                cmd1.Parameters.Add(cci);

                SqlParameter tid = new SqlParameter();
                tid.ParameterName = "@IsRenewal";
                tid.SqlDbType = SqlDbType.Int;
                tid.Value = n.IsRenewal;
                cmd1.Parameters.Add(tid);



                SqlParameter dd = new SqlParameter();
                dd.ParameterName = "@Amount";
                dd.SqlDbType = SqlDbType.Decimal;
                dd.Value = n.Amount;
                cmd1.Parameters.Add(dd);

                SqlParameter lid = new SqlParameter();
                lid.ParameterName = "@UnitPrice";
                lid.SqlDbType = SqlDbType.Decimal;
                lid.Value = n.UnitPrice;
                cmd1.Parameters.Add(lid);

                SqlParameter kki = new SqlParameter();
                kki.ParameterName = "@Units";
                kki.SqlDbType = SqlDbType.Decimal;
                kki.Value = n.Units;
                cmd1.Parameters.Add(kki);

                SqlParameter qqi = new SqlParameter();
                qqi.ParameterName = "@CreatedOn";
                qqi.SqlDbType = SqlDbType.DateTime;
                qqi.Value = n.CreatedOn;
                cmd1.Parameters.Add(qqi);

                SqlParameter flag = new SqlParameter();
                flag.ParameterName = "@insupddelflag";
                flag.SqlDbType = SqlDbType.VarChar;
                flag.Value = n.insupddelflag;
                cmd1.Parameters.Add(flag);

                SqlDataAdapter db = new SqlDataAdapter(cmd1);
                db.Fill(ds);



                return ds;
            }
            catch (Exception ex)
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                string str = ex.Message;
                return ds;
            }
        }

        [HttpPost]
        [Route("api/UserLicenses/SaveULConfirmDetails")]
        public DataTable SaveULConfirmDetails(ULConfirmDetails ulconfirm)
        {
            DataTable Tbl = new DataTable();
            SqlConnection conn = new SqlConnection();
            try
            {
                // connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

                SqlCommand cmd1 = new SqlCommand();
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.CommandText = "InsUpdDelUserLicenseConfirmDetails";
                cmd1.Connection = conn;
                conn.Open();

                SqlParameter foId = new SqlParameter();
                foId.ParameterName = "@foId";
                foId.SqlDbType = SqlDbType.Int;
                foId.Value = ulconfirm.foId;
                cmd1.Parameters.Add(foId);

                SqlParameter userId = new SqlParameter();
                userId.ParameterName = "@userId";
                userId.SqlDbType = SqlDbType.Int;
                userId.Value = ulconfirm.userId;
                cmd1.Parameters.Add(userId);

                SqlParameter aid = new SqlParameter();
                aid.ParameterName = "@ULId";
                aid.SqlDbType = SqlDbType.Int;
                aid.Value = ulconfirm.ULId;
                cmd1.Parameters.Add(aid);

                SqlParameter ULPymtId = new SqlParameter();
                ULPymtId.ParameterName = "@ULPymtId";
                ULPymtId.SqlDbType = SqlDbType.Int;
                ULPymtId.Value = ulconfirm.ULPymtId;
                cmd1.Parameters.Add(ULPymtId);


                SqlParameter trid = new SqlParameter();
                trid.ParameterName = "@TransId";
                trid.SqlDbType = SqlDbType.VarChar;
                trid.Value = ulconfirm.TransId;
                cmd1.Parameters.Add(trid);

                SqlParameter s1id = new SqlParameter();
                s1id.ParameterName = "@GatewayTransId";
                s1id.SqlDbType = SqlDbType.VarChar;
                s1id.Value = ulconfirm.GatewayTransId;
                cmd1.Parameters.Add(s1id);

                SqlParameter cci = new SqlParameter();
                cci.ParameterName = "@address";
                cci.SqlDbType = SqlDbType.VarChar;
                cci.Value = ulconfirm.address;
                cmd1.Parameters.Add(cci);

                SqlParameter tid = new SqlParameter();
                tid.ParameterName = "@IsRenewal";
                tid.SqlDbType = SqlDbType.Int;
                tid.Value = ulconfirm.IsRenewal;
                cmd1.Parameters.Add(tid);



                SqlParameter dd = new SqlParameter();
                dd.ParameterName = "@Amount";
                dd.SqlDbType = SqlDbType.Decimal;
                dd.Value = ulconfirm.Amount;
                cmd1.Parameters.Add(dd);

                SqlParameter lid = new SqlParameter();
                lid.ParameterName = "@itemId";
                lid.SqlDbType = SqlDbType.Int;
                lid.Value = ulconfirm.itemId;
                cmd1.Parameters.Add(lid);

                SqlParameter kki = new SqlParameter();
                kki.ParameterName = "@Units";
                kki.SqlDbType = SqlDbType.Decimal;
                kki.Value = ulconfirm.Units;
                cmd1.Parameters.Add(kki);

                SqlParameter pu = new SqlParameter();
                pu.ParameterName = "@POSUnits";
                pu.SqlDbType = SqlDbType.Decimal;
                pu.Value = ulconfirm.POSUnits;
                cmd1.Parameters.Add(pu);

                SqlParameter flag = new SqlParameter();
                flag.ParameterName = "@insupddelflag";
                flag.SqlDbType = SqlDbType.VarChar;
                flag.Value = ulconfirm.insupddelflag;
                cmd1.Parameters.Add(flag);

                SqlDataAdapter db = new SqlDataAdapter(cmd1);
                db.Fill(Tbl);


                int status;

                #region Mobile OTP
                string lcode = Tbl.Rows[0]["licenseCode"].ToString();
                string rcode = Tbl.Rows[0]["TransId"].ToString();
                string username1 = Tbl.Rows[0]["username"].ToString();
                string shipOrder = Tbl.Rows[0]["shipOrder"].ToString();
                string expiryon = Tbl.Rows[0]["expiryon"].ToString();
                string startdate = Tbl.Rows[0]["startdate"].ToString();
                string emailid = Tbl.Rows[0]["emailid"].ToString();

                if (lcode != null)
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
                        mail.To.Add(emailid);
                        mail.Subject = "Driver registration - Mobile OTP";
                        mail.IsBodyHtml = true;

                        string verifcodeMail = @"<table>
                                                            <tr>
                                                                <td>
                                                                    <h2>Thank you for registering with PaySmart APP</h2>
                                                                    <table width=\""760\"" align=\""center\"">
                                                                        <tbody style='background-color:#F0F8FF;'>
                                                                            <h3>Congratulations!!</h3>
                                                                                <h4>Your license payment is processed successfully and below are the details</h4>
                                                                                <h3>Fleet Owner License Details</h3> 
																				Your payment transaction Id:<b> " + ulconfirm.TransId + @"</b>
																			    <br />
																				Your license code is:<b> " + lcode + @"</b>
																			    <br />
																				License Activated on  : <b>" + startdate + @"</b>
																				 <br />
																				License Expiry on     :<b> " + expiryon + @"</b>
																				<br />																				
											Your Smart Ticket dashboard login user name            : <b>" + username1 + @"</b>
																				 <br />
											Your Smart Ticket dashboard login one time password is   : <b>" + username1 + @"</b>
																				<br />
																				<h5>Please contact interbus admin for any further information!</h5>
																				<center><h3> Happy ticketing!</h3></center>
																				For any queries/concerns please contact INTERBUS adminitrator - admin@interbus.com
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
                        //traceWriter.Trace(Request, "0", TraceLevel.Error, "{0}", "Register driver Email otp failed...." + ex.Message);
                        throw ex;
                    }
                }
                #endregion Mobile OTP


                return Tbl;
            }
            catch (Exception ex)
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                string str = ex.Message;
                return Tbl;
            }


        }
      
    }
}
