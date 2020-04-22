using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InternalAPI
{
    public static class LoggerExtensions
    {


        public class loggercsv
        {
            public string Methodname { get; set; }
            public DateTime RequestTime { get; set; }
            public DateTime ResponseTime { get; set; }
            public string Status { get; set; }
            public string Argument { get; set; }
            public TimeSpan Letancy { get; set; }
            public string Exception { get; set; }
            public string ErrorMessage { get; set; }
            public string InnerException { get; set; }
            public string StckTrace { get; set; }

        }

        

        public static void LogSoapApiResponseTime(string methodname,DateTime requestTime, DateTime 
            responseTime, string status, string exception, TimeSpan letancy,string errormessage,string innerexception,
            string errortrace)
        {

            try
            {

                var record = new List<loggercsv>
                {
                    new loggercsv
                    {
                        Methodname=methodname,
                        RequestTime=requestTime,
                        ResponseTime=responseTime,
                        Status=status,
                        Argument="{}",
                        Letancy=letancy,
                        Exception=exception,
                        ErrorMessage=errormessage,
                        InnerException=innerexception,
                        StckTrace=errortrace,
                    }
                };
                string path = @"C:\Users\ATEAM\source\repos\ExtrnalAPI\InternalAPI\Logger.csv";

                if(File.Exists(path))
                {
                    using (StreamWriter file = File.AppendText(path))
                    using (var csv = new CsvWriter(file, CultureInfo.InvariantCulture))
                    {
                        csv.Configuration.HasHeaderRecord = false;
                        csv.WriteRecords(record);
                    }
                }
                else
                {
                    using (StreamWriter file = File.CreateText(path))
                    using (var csv = new CsvWriter(file, CultureInfo.InvariantCulture))
                    {
                       
                        csv.WriteRecords(record);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }


      
    }
}
