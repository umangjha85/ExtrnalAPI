using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternalAPI
{
    public class WeatherDataModel
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF
        {
            get
            {
                return 32 + (int)(TemperatureC / 0.5556);
            }
        }

        public string Summary { get; set; }
    }
}
