using SmartTicketPortal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace SmartTicketPortal.Controllers
{
    public class websiteuserinfoController : ApiController
    {
        [HttpGet]

        public DataTable GetWebsiteUserInfo(string logininfo)
        {
            DataTable Tbl = new DataTable();


            //connect to database
            SqlConnection conn = new SqlConnection();
            //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetWebsiteUserInfo";
            cmd.Connection = conn;

            SqlParameter psw = new SqlParameter("@logininfo", SqlDbType.VarChar, 20);
            psw.Value = logininfo;
            cmd.Parameters.Add(psw);


            SqlDataAdapter db = new SqlDataAdapter(cmd);
            db.Fill(Tbl);

            return Tbl;
        }
        [HttpPost]
        public HttpResponseMessage pos(WebsiteUserInfo b)
        {

            //connect to database
            SqlConnection conn = new SqlConnection();
            try
            {
                //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsUpdWebsiteUserInfo";
                cmd.Connection = conn;
                conn.Open();
                //string insertquery = "insert into login(UserName,Password,FirstName,LastName,MobileNo) values (@UserName,@Password,@FirstName,@lastName,@MobileNo)";



                //SqlCommand con=new SqlCommand(insertquery,conn);
                SqlParameter fstn = new SqlParameter();
                fstn.ParameterName = "@FirstName ";
                fstn.SqlDbType = SqlDbType.VarChar;
                fstn.Value = b.FirstName;
                cmd.Parameters.Add(fstn);

                SqlParameter lstn = new SqlParameter();
                lstn.ParameterName = "@LastName";
                lstn.SqlDbType = SqlDbType.VarChar;
                lstn.Value = b.LastName;
                cmd.Parameters.Add(lstn);

                SqlParameter usn = new SqlParameter();
                usn.ParameterName = "@UserName";
                usn.SqlDbType = SqlDbType.VarChar;
                usn.Value = b.UserName;
                cmd.Parameters.Add(usn);

                SqlParameter psw = new SqlParameter();
                psw.ParameterName = "@Password";
                psw.SqlDbType = SqlDbType.VarChar;
                psw.Value = b.Password;
                cmd.Parameters.Add(psw);


                SqlParameter Emadd = new SqlParameter();
                Emadd.ParameterName = "@EmailAddress";
                Emadd.SqlDbType = SqlDbType.VarChar;
                Emadd.Value = b.EmailAddress;
                cmd.Parameters.Add(Emadd);



                SqlParameter Mob = new SqlParameter();
                Mob.ParameterName = "@Mobile";
                Mob.SqlDbType = SqlDbType.VarChar;
                Mob.Value = b.Mobile;
                cmd.Parameters.Add(Mob);




                cmd.ExecuteScalar();
                conn.Close();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                string str = ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
        }

        [HttpGet]
        public DataTable GetBookedHistory(string emailid)
        {
            DataTable dt = new DataTable();

            //connect to database
            SqlConnection conn = new SqlConnection();
            //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetBookedHistory";
            cmd.Connection = conn;

            SqlParameter psw = new SqlParameter("@emailAddress", SqlDbType.VarChar, 20);
            psw.Value = emailid;
            cmd.Parameters.Add(psw);


            SqlDataAdapter db = new SqlDataAdapter(cmd);
            db.Fill(dt);


            return dt;
        }
    }
}
