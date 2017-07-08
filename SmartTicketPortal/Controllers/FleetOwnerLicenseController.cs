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
     public class FleetOwnerLicenseController : ApiController
        {

            [HttpGet]
            public DataSet validatefleetowner(string fleetownercode)
            {
                //connect to database
                SqlConnection conn = new SqlConnection();
                //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ValidateFleetOwnerCode";
                cmd.Connection = conn;

                //conn.Open();
                SqlParameter code = new SqlParameter("@fleetownercode", SqlDbType.VarChar, 15);
                code.Value = fleetownercode;
                cmd.Parameters.Add(code);

                SqlParameter mm = new SqlParameter("@result", SqlDbType.Int);
                mm.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(mm);

                DataSet ds = new DataSet();
                SqlDataAdapter db = new SqlDataAdapter(cmd);

                db.Fill(ds);

                int result = -1;
                result = Convert.ToInt32(mm.Value);

                DataTable dt = new DataTable();
                dt.Columns.Add("result");
                DataRow dr = dt.NewRow();
                dr[0] = result;

                dt.Rows.Add(dr);

                ds.Tables.Add(dt);

                return ds;

            }


            [HttpGet]
            public int updatebtpos(string fleetownercode, string units)
            {
                //connect to database
                SqlConnection conn = new SqlConnection();
                //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "updatebtpos";
                cmd.Connection = conn;

                conn.Open();
                SqlParameter code = new SqlParameter("@fleetownercode", SqlDbType.VarChar, 10);
                code.Value = fleetownercode;
                cmd.Parameters.Add(code);

                SqlParameter posunits = new SqlParameter("@units", SqlDbType.Int);
                posunits.Value = Convert.ToInt32(units);
                cmd.Parameters.Add(posunits);


                SqlParameter mm = new SqlParameter("@result", SqlDbType.Int);
                mm.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(mm);

                cmd.ExecuteNonQuery();

                conn.Close();

                int result = -1;
                result = Convert.ToInt32(mm.Value);
                return result;

            }

            [HttpGet]
            public int registerpos(string fleetownercode, string ipconfig)
            {
                //connect to database
                SqlConnection conn = new SqlConnection();
                //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "registerbtpos";
                    cmd.Connection = conn;

                    conn.Open();
                    SqlParameter code = new SqlParameter("@fleetownercode", SqlDbType.VarChar, 10);
                    code.Value = fleetownercode.Trim();
                    cmd.Parameters.Add(code);

                    SqlParameter posunits = new SqlParameter("@ipconfig", SqlDbType.VarChar, 20);
                    posunits.Value = ipconfig.Trim();
                    cmd.Parameters.Add(posunits);


                    SqlParameter mm = new SqlParameter("@result", SqlDbType.Int);
                    mm.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(mm);

                    cmd.ExecuteNonQuery();

                    conn.Close();

                    int result = -1;
                    result = Convert.ToInt32(mm.Value);
                    return result;
                }
                catch
                {
                    return -1;
                }
            }


            [HttpPost]
            [Route("api/FleetOwnerLicense/CreateNewFOR")]
            public DataTable CreateNewFOR(FleetOwnerRequest fleetOwnerRequest)
            {
                DataTable Tbl = new DataTable();
                SqlConnection conn = new SqlConnection();
                try
                {

                    //connect to database
                    //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
                    conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "InsUpdFleetOwnerRequestDetails";
                    cmd.Connection = conn;
                    //conn.Open();    

                    //User details
                    SqlParameter FRFirstName = new SqlParameter("@FirstName", SqlDbType.VarChar, 50);
                    FRFirstName.Value = fleetOwnerRequest.FirstName;
                    cmd.Parameters.Add(FRFirstName);

                    SqlParameter FRMiddleName = new SqlParameter("@MiddleName", SqlDbType.VarChar, 50);
                    FRMiddleName.Value = fleetOwnerRequest.MiddleName;
                    cmd.Parameters.Add(FRMiddleName);

                    SqlParameter LastName = new SqlParameter("@LastName", SqlDbType.VarChar, 50);
                    LastName.Value = fleetOwnerRequest.LastName;
                    cmd.Parameters.Add(LastName);

                    SqlParameter PhoneNo = new SqlParameter("@PhoneNo", SqlDbType.VarChar, 20);
                    PhoneNo.Value = fleetOwnerRequest.PhoneNo;
                    cmd.Parameters.Add(PhoneNo);

                    SqlParameter AltPhoneNo = new SqlParameter("@AltPhoneNo", SqlDbType.VarChar, 20);
                    AltPhoneNo.Value = fleetOwnerRequest.AltPhoneNo;
                    cmd.Parameters.Add(AltPhoneNo);

                    SqlParameter FREmailAddress = new SqlParameter("@EmailAddress", SqlDbType.VarChar, 50);
                    FREmailAddress.Value = fleetOwnerRequest.EmailAddress;
                    cmd.Parameters.Add(FREmailAddress);
                    
                    SqlParameter Address = new SqlParameter("@Address", SqlDbType.VarChar, 250);
                    Address.Value = fleetOwnerRequest.Address;
                    cmd.Parameters.Add(Address);


                    SqlParameter Gender = new SqlParameter("@Gender", SqlDbType.Int);
                    Gender.Value = fleetOwnerRequest.Gender;
                    cmd.Parameters.Add(Gender);

                    SqlParameter userPhoto = new SqlParameter("@userPhoto", SqlDbType.VarChar);
                    userPhoto.Value = fleetOwnerRequest.userPhoto;
                    cmd.Parameters.Add(userPhoto);

                    //Company details

                    SqlParameter FRCompanyName = new SqlParameter("@CompanyName", SqlDbType.VarChar, 50);
                    FRCompanyName.Value = fleetOwnerRequest.CompanyName;
                    cmd.Parameters.Add(FRCompanyName);

                    SqlParameter CmpEmailAddress = new SqlParameter("@CmpEmailAddress", SqlDbType.VarChar, 20);
                    CmpEmailAddress.Value = fleetOwnerRequest.CmpEmailAddress;
                    cmd.Parameters.Add(CmpEmailAddress);

                    SqlParameter CmpTitle = new SqlParameter("@CmpTitle", SqlDbType.VarChar, 50);
                    CmpTitle.Value = fleetOwnerRequest.CmpTitle;
                    cmd.Parameters.Add(CmpTitle);

                    SqlParameter CmpCaption = new SqlParameter("@CmpCaption", SqlDbType.VarChar, 50);
                    CmpCaption.Value = fleetOwnerRequest.CmpCaption;
                    cmd.Parameters.Add(CmpCaption);

                    SqlParameter FleetSize = new SqlParameter("@FleetSize", SqlDbType.Int);
                    FleetSize.Value = fleetOwnerRequest.FleetSize;
                    cmd.Parameters.Add(FleetSize);

                    SqlParameter StaffSize = new SqlParameter("@StaffSize", SqlDbType.Int);
                    StaffSize.Value = fleetOwnerRequest.StaffSize;
                    cmd.Parameters.Add(StaffSize);

                    SqlParameter Country = new SqlParameter("@Country", SqlDbType.Int);
                    Country.Value = fleetOwnerRequest.Country;
                    cmd.Parameters.Add(Country);

                    SqlParameter Code = new SqlParameter("@Code", SqlDbType.VarChar, 50);
                    Code.Value = fleetOwnerRequest.Code;
                    cmd.Parameters.Add(Code);

                    SqlParameter CmpFax = new SqlParameter("@CmpFax", SqlDbType.VarChar, 50);
                    CmpFax.Value = fleetOwnerRequest.CmpFax;
                    cmd.Parameters.Add(CmpFax);

                  

                    SqlParameter CmpAddress = new SqlParameter("@CmpAddress", SqlDbType.VarChar, 500);
                    CmpAddress.Value = fleetOwnerRequest.CmpAddress;
                    cmd.Parameters.Add(CmpAddress);

                    SqlParameter CmpAltAddress = new SqlParameter("@CmpAltAddress", SqlDbType.VarChar, 250);
                    CmpAltAddress.Value = fleetOwnerRequest.CmpAltAddress;
                    cmd.Parameters.Add(CmpAltAddress);

                   

                    SqlParameter state = new SqlParameter("@state", SqlDbType.Int);
                    state.Value = fleetOwnerRequest.state;
                    cmd.Parameters.Add(state);

                    SqlParameter ZipCode = new SqlParameter("@ZipCode", SqlDbType.VarChar, 50);
                    ZipCode.Value = fleetOwnerRequest.ZipCode;
                    cmd.Parameters.Add(ZipCode);
     

                    SqlParameter CmpPhoneNo = new SqlParameter("@CmpPhoneNo", SqlDbType.VarChar, 50);
                    CmpPhoneNo.Value = fleetOwnerRequest.CmpPhoneNo;
                    cmd.Parameters.Add(CmpPhoneNo);

                    SqlParameter CmpAltPhoneNo = new SqlParameter("@CmpAltPhoneNo", SqlDbType.VarChar, 50);
                    CmpAltPhoneNo.Value = fleetOwnerRequest.CmpAltPhoneNo;
                    cmd.Parameters.Add(CmpAltPhoneNo);


                    SqlParameter CurrentSystemInUse = new SqlParameter("@CurrentSystemInUse", SqlDbType.VarChar, 50);
                    CurrentSystemInUse.Value = fleetOwnerRequest.CurrentSystemInUse;
                    cmd.Parameters.Add(CurrentSystemInUse);

                    SqlParameter howdidyouhearaboutus = new SqlParameter("@howdidyouhearaboutus", SqlDbType.VarChar, 50);
                    howdidyouhearaboutus.Value = fleetOwnerRequest.howdidyouhearaboutus;
                    cmd.Parameters.Add(howdidyouhearaboutus);

                    SqlParameter SentNewProductsEmails = new SqlParameter("@SentNewProductsEmails", SqlDbType.Int);
                    SentNewProductsEmails.Value = fleetOwnerRequest.SendNewProductsEmails;
                    cmd.Parameters.Add(SentNewProductsEmails);

                    SqlParameter Agreetotermsandconditions = new SqlParameter("@Agreetotermsandconditions", SqlDbType.Int);
                    Agreetotermsandconditions.Value = fleetOwnerRequest.Agreetotermsandconditions;
                    cmd.Parameters.Add(Agreetotermsandconditions);

                    SqlParameter CmpLogo = new SqlParameter("@CmpLogo", SqlDbType.VarChar);
                    CmpLogo.Value = fleetOwnerRequest.CmpLogo;
                    cmd.Parameters.Add(CmpLogo);

                    //SqlParameter insupdflag = new SqlParameter("@insupdflag", SqlDbType.VarChar);
                    //insupdflag.Value = fleetOwnerRequest.insupdflag;
                    //cmd.Parameters.Add(insupdflag);
                    
                    //General details                     

                   // cmd.ExecuteScalar();
                    // conn.Close();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(Tbl);

                    int status;
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
                            mail.To.Add(fleetOwnerRequest.EmailAddress);
                            mail.Subject = "INTERBUS Fleet Owner registration status";
                            mail.IsBodyHtml = true;

                            string verifcodeMail = @"<table>
                                                        <tr>
                                                            <td>
                                                                <h2>Thank you for registering with INTERBUS as fleet owner!</h2>
                                                                <table width=\""760\"" align=\""center\"">
                                                                    <tbody style='background-color:#F0F8FF;'>
                                                                        <tr>
                                                                            <td style=\""font-family:'Zurich BT',Arial,Helvetica,sans-serif;font-size:15px;text-align:left;line-height:normal;background-color:#F0F8FF;\"" >
<div style='padding:10px;border:#0000FF solid 2px;'>                                                                                
<h3>Congratulations!!</h3>
                                                                                <h4>You have been successfully registered with INTERBUS as a fleet owner</h4>
                                                                                <h3>Fleet Owner Code Details</h3>                                                                                
                                                                                Your fleet owner code is:<h3>" + Tbl.Rows[0]["FleetOwnerCode"].ToString() + @"</h3>  


Use the above fleet owner code to buy/renew license and for any further correspondence with INTERBUS.
                                                                                <br /><br />
                                                        

                                                        If you didn't make this request, <a href='http://154.120.237.198:52800'>click here</a> to cancel.

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

                            SmtpServer.Credentials = new System.Net.NetworkCredential(eusername, pwd);
                           
                            SmtpServer.EnableSsl = true;
                        
                            SmtpServer.Send(mail);
                            status = 1;
                        }
                        catch (Exception ex)
                        {
                            //throw ex;
                            status = -1;
                        }

                        //update if email is sent

                        #endregion send email with details
                    }

                    return Tbl;
                }

                catch (Exception ex)
                {

                    if (conn != null && conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    throw (ex);
                    //string str = ex.Message;
                    //Tbl.Columns.Add("status");
                    //Tbl.Columns.Add("details");
                    //Tbl.Rows.Add(new string[]{"0",ex.Message});

                    //string str = ex.Message;
                    //return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);

                }
                // int found = 0;
                //  return Tbl;
            }

            public void Options()
            {

            }

        }
    }
