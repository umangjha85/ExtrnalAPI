using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternalAPI.Models
{
    public class LoggerDataModel
    {
        public string Methodname { get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime ResponseTime { get; set; }
        public string Status { get; set; }
        public string Argument { get; set; }
        public int Letancy { get; set; }
        public string Exception { get; set; }
        public string ErrorMessage { get; set; }
        public string InnerException { get; set; }
        public string StckTrace { get; set; }


    }
}
