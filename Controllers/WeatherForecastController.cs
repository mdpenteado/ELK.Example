using Microsoft.AspNetCore.Mvc;

namespace ELK.Example.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<WeatherForecast> _weatherForecasts = new List<WeatherForecast>
        {
            new WeatherForecast
            {
                Id = 1,
                Date = new DateTime(2023, 08, 05),
                TemperatureC = 30,
                Summary = "Hot"
            },
            new WeatherForecast
            {
                Id = 2,
                Date = new DateTime(2023, 08, 06),
                TemperatureC = 25,
                Summary = "Warm"
            }
        };
        [HttpGet]
        public IActionResult GetAllWeatherForecasts()
        {
            return Ok(_weatherForecasts);
        }
        [HttpGet("{id}")]
        public IActionResult GetWeatherForecastById(int id)
        {
            //Não vamos nos preocupar com validações.
            return Ok(_weatherForecasts.Find(w => w.Id == id));
        }
        [HttpPost]
        public IActionResult AddWeatherForecast(WeatherForecast weatherForecast)
        {
            //Não vamos nos preocupar com validações.
            _weatherForecasts.Add(weatherForecast);
            return Ok(weatherForecast);
        }
    }
}