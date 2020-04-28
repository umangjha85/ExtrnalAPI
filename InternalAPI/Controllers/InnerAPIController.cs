using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
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
        private bool flag;
        List<WeatherDataModel> innerAPI = new List<WeatherDataModel>();
        private string _apiBaseURI = "https://localhost:44319/weatherforecast";

        public HttpClient GetClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_apiBaseURI);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        // GET: api/InnerAPI
        //[Route("api/InnerAPI")]
        [HttpGet]
        public List<WeatherDataModel> Get()             
        {
            try         
            {
                _requestTime = DateTime.Now;
                
                HttpResponseMessage responseMessage = GetClient().GetAsync("WeatherForecast").Result;
                responseTime = DateTime.Now;
                if (responseMessage.IsSuccessStatusCode)
                {
                    innerAPI = JsonConvert.DeserializeObject<List<WeatherDataModel>>
                    (responseMessage.Content.ReadAsStringAsync().Result); //resapi
                    flag = true;
                }
                Exception ex = new Exception();
                ex = null;
                string status = flag ? "Success":"Fail";
                LoggerExtensions.LogSoapApiResponseTime(nameof(Get), _requestTime, responseTime,status,ex);
                return innerAPI;
            }
            catch(Exception ex)
            {
              LoggerExtensions.LogSoapApiResponseTime(nameof(Get), _requestTime, responseTime, "Fail", ex);
              throw;
            }
            
        }

       
    }
}
