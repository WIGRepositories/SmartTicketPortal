using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http.Tracing;

namespace Paysmart.Controllers
{
    class LogTraceWriter
    {

        public void Trace(System.Net.Http.HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            if (category == "0" || category == "1")
            {
                TraceRecord record = new TraceRecord(request, category, level);
                traceAction(record);
                string path = HttpContext.Current.Server.MapPath("~/Logs/AppLog.txt");
                try
                {
                    if (!File.Exists(path))
                        File.Create(path);

                    File.AppendAllText(path, DateTime.Now.ToString() + ": " + category + " -- " + record.Message + "\r\n");
                }
                catch { }
            }
        }

        internal void Trace(System.Net.Http.HttpRequestMessage Request, string p1, TraceLevel traceLevel, string p2, string p3)
        {
            throw new NotImplementedException();
        }
    }
}
