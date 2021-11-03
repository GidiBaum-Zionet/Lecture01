using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IWeatherGenerator
    {
        IEnumerable<WeatherForecast> GetData();
    }
}