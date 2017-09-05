using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Tracing;

namespace SmartTicketPortal.Controllers
{
    public class TypesController : ApiController
    {
        [HttpGet]
        public DataTable TypesByGroupId(int groupid)
        {
            DataTable Tbl = new DataTable();

            LogTraceWriter traceWriter = new LogTraceWriter();
            traceWriter.Trace(Request, "0", TraceLevel.Info, "{0}", "GetTypesByGroupId credentials....");

            //connect to database
            SqlConnection conn = new SqlConnection();
            //connetionString="Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["btposdb"].ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetTypesByGroupId";
            cmd.Connection = conn;

            SqlParameter Gid = new SqlParameter();
            Gid.ParameterName = "@typegrpid";
            Gid.SqlDbType = SqlDbType.Int;
            Gid.Value = groupid;
            cmd.Parameters.Add(Gid);

            DataSet ds = new DataSet();
            SqlDataAdapter db = new SqlDataAdapter(cmd);
            db.Fill(ds);
            Tbl = ds.Tables[0];

            //prepare a file
            StringBuilder str = new StringBuilder();

            str.Append(string.Format("test\n{0}", groupid.ToString()));



            traceWriter.Trace(Request, "0", TraceLevel.Info, "{0}", "GetTypesByGroupId Credentials completed.");
            // int found = 0;
            return Tbl;
        }
    }
}
