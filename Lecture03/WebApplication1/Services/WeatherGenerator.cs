using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class WeatherGenerator : IWeatherGenerator
    {
        readonly ILogger<WeatherGenerator> _Logger;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherGenerator(ILogger<WeatherGenerator> logger)
        {
            _Logger = logger;

            _Logger.LogInformation("CTOR");
        }

        public void SetData()
        {

        }

        public IEnumerable<WeatherForecast> GetData()
        {

            _Logger.LogWarning("GetData");

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }
    }
}
