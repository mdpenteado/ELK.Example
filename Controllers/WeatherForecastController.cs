using ELK.Example.Domain.Ports.Output;
using ELK.Example.ELK.Example.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ELK.Example.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastsInputPort _forecastInputPort;

        public WeatherForecastController(IWeatherForecastsInputPort forecastInputPort)
        {
            _forecastInputPort = forecastInputPort;
        }

        [HttpGet]
        public IActionResult GetAllWeatherForecasts()
        {
            return Ok(_forecastInputPort.GetAllWeatherForecasts());
        }
        [HttpGet("{id}")]
        public IActionResult GetWeatherForecastById(int id)
        {
            //Não vamos nos preocupar com validações.
            return Ok(_forecastInputPort.GetWeatherForecastById(id));
        }
        [HttpPost]
        public IActionResult AddWeatherForecast(WeatherForecastModel weatherForecast)
        {
            return Ok(weatherForecast);
        }
    }
}