using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Api
{
    public static class WeatherForecast
    {
        public static readonly Random random = new Random();
        public static readonly string[] summaries = new string[] {
            "Freezing",
            "Bracing",
            "Balmy",
            "Chilly"
        };

        [FunctionName("WeatherForecast")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var forecasts = new object[5];
            var currentDate = DateTime.Today;
            for (int i = 0; i < 5; i++)
            {
                var futureDate = currentDate.AddDays(i);
                var temperatureC = random.Next(-10, 35);
                forecasts[i] = new {
                    date = futureDate.ToString("yyyy-MM-dd"),
                    temperatureC = temperatureC,
                    summary = summaries[random.Next(0, summaries.Length)]
                };
            }
            return new OkObjectResult(forecasts);
        }
    }
}