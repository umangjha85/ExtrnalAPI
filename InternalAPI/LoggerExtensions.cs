using CsvHelper;
using InternalAPI.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;


namespace InternalAPI
{
    public static class LoggerExtensions
    {
      
        public static void LogSoapApiResponseTime(string methodname,DateTime requestTime, DateTime 
            responseTime, string status, Exception ex )
        {
            try
            {
                string Excp, ErrMsg, InnExcp, Stackt;
                if (ex == null)
                {
                    Excp = ErrMsg = InnExcp = Stackt = "";

                }
                else
                {
                    Excp = ex.ToString();
                    ErrMsg = ex.Message;
                    InnExcp = ex.InnerException.ToString();
                    Stackt = ex.StackTrace.ToString();
                }
                
                var record = new List<LoggerDataModel>
                {
                    new LoggerDataModel
                    {
                        Methodname=methodname,
                        RequestTime=requestTime,
                        ResponseTime=responseTime,
                        Status=status,
                        Argument="{}",
                        Letancy=(int)(responseTime-requestTime).TotalMilliseconds,
                        Exception=Excp,
                        ErrorMessage=ErrMsg,
                        InnerException=InnExcp,
                        StckTrace=Stackt,
                    }
                };
                
                StreamWriter streamWriter;
                var logFileName = "LogSoapApiResponseTime" + DateTime.Now.ToString("MM_dd_yyyy") + ".csv";
                const string dir = "Logs";
                if (!Directory.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), dir)))
                {
                    Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        dir));
                }
                if (!File.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), dir, logFileName)))
                {
                    streamWriter = File.CreateText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), dir, logFileName));
                    streamWriter.Close();
                
                    using (streamWriter = File.AppendText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), dir, logFileName)))
                    {
                    
                        using (var csv = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                        {

                            csv.WriteRecords(record);
                        }
                    }
                }
                else
                {
                    using (streamWriter = File.AppendText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), dir, logFileName)))
                    {

                        using (var csv = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                        {
                            csv.Configuration.HasHeaderRecord = false;
                            csv.WriteRecords(record);
                        }
                    }
                }
            }
            catch (Exception exp)
            {

            }
        }


      
    }
}
