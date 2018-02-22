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
    public class LicensePageController : ApiController
    {

       

        [HttpGet]
        [Route("api/LicensePage/GetLicense")]
        public DataSet GetLicense(int LicenseCatId)
        {
            DataTable Tbl = new DataTable();

            //connect to database
            SqlConnection conn = new SqlConnection();
            //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetLicensePageDetails";
            cmd.Connection = conn;


            SqlParameter mm = new SqlParameter("@catId", SqlDbType.Int);
            mm.Value = LicenseCatId;
            cmd.Parameters.Add(mm);

            DataSet ds = new DataSet();
            SqlDataAdapter db = new SqlDataAdapter(cmd);

            db.Fill(ds);
            // Tbl = ds.Tables[0];

            // int found = 0;
            return ds;
        }

        [HttpPost]
        public DataTable SaveLicence(LicensePage n)
        {
            DataTable Tbl = new DataTable();

            try
            {
                //connect to database
                SqlConnection conn = new SqlConnection();
                // connetionString = "Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password";
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

                SqlCommand cmd1 = new SqlCommand();
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.CommandText = "InsUpdDelLicenseDetails";
                cmd1.Connection = conn;

                conn.Open();
                SqlParameter gs = new SqlParameter();
                gs.ParameterName = "@LicenseType";
                gs.SqlDbType = SqlDbType.VarChar;
                gs.Value = Convert.ToString(n.LicenseType);
                cmd1.Parameters.Add(gs);

                SqlParameter gsa = new SqlParameter();
                gsa.ParameterName = "@Id";
                gsa.SqlDbType = SqlDbType.Int;
                gsa.Value = Convert.ToString(n.Id);
                cmd1.Parameters.Add(gsa);

                //SqlParameter gsa1 = new SqlParameter();
                //gsa1.ParameterName = "@LicenseCatId";
                //gsa1.SqlDbType = SqlDbType.Int;
                //gsa1.Value = Convert.ToString(n.LicenseCatId);
                //cmd1.Parameters.Add(gsa1);

                SqlParameter gid = new SqlParameter();
                gid.ParameterName = "@Unitprice";
                gid.SqlDbType = SqlDbType.VarChar;
                gid.Value = n.Unitprice;
                cmd1.Parameters.Add(gid);

                SqlParameter gsab = new SqlParameter();
                gsab.ParameterName = "@FeatureName";
                gsab.SqlDbType = SqlDbType.Int;
                gsab.Value = Convert.ToString(n.FeatureName);
                cmd1.Parameters.Add(gsab);


                SqlParameter gsab2 = new SqlParameter();
                gsab2.ParameterName = "@FeatureValue";
                gsab2.SqlDbType = SqlDbType.Int;
                gsab2.Value = Convert.ToString(n.FeatureValue);
                cmd1.Parameters.Add(gsab2);




                SqlParameter insupdflag = new SqlParameter("@insupdflag", SqlDbType.VarChar, 10);
                insupdflag.Value = n.insupdflag;
                cmd1.Parameters.Add(insupdflag);


                cmd1.ExecuteScalar();
                conn.Close();
                DataSet ds = new DataSet();
                //SqlDataAdapter db = new SqlDataAdapter(cmd);
                //db.Fill(ds);
                //Tbl = ds.Tables[0];
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            // int found = 0;
            return Tbl;

        }

       
    }
}
