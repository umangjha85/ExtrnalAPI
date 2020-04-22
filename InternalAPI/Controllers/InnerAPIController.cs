using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InternalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InnerAPIController : ControllerBase
    {

        private DateTime _requestTime;
        private DateTime responseTime;
        private bool flag=false;
        List<InnerAPI> innerAPI = new List<InnerAPI>();
        private string _apiBaseURI = "https://localhost:44319/weatherforecast";
       

        // GET: api/InnerAPI
        //[Route("api/InnerAPI")]
        [HttpGet]
        public List<InnerAPI> Get()             
        
        
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(_apiBaseURI);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _requestTime = DateTime.Now;
                HttpResponseMessage responseMessage = client.GetAsync("WeatherForecast").Result;
                responseTime = DateTime.Now;

                if (responseMessage.IsSuccessStatusCode)
                {

                    innerAPI = JsonConvert.DeserializeObject<List<InnerAPI>>
                    (responseMessage.Content.ReadAsStringAsync().Result); //resapi
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                TimeSpan letancy = responseTime - _requestTime;
                string status = flag.Equals("true") ? "Fail" : "Success";

                LoggerExtensions.LogSoapApiResponseTime(nameof(Get), _requestTime, responseTime,status, "",
                letancy, "", "", "");

                return innerAPI;
            }
            catch(Exception ex)
            {
                TimeSpan letancy = responseTime - _requestTime;
               
                var st = new StackTrace(ex, true);
                var line = st.GetFrame(0).GetFileLineNumber();
                
                LoggerExtensions.LogSoapApiResponseTime(nameof(Get), _requestTime, responseTime, "Fail", ex.ToString(),
                letancy, ex.Message.ToString(), ex.InnerException.ToString(), ex.StackTrace.ToString());

                 throw;
            }
            
        }

       
    }
}
