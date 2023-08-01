using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;

namespace ELK.Example.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class WeatherForecastController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };


        public WeatherForecastController()
        {
            _logger = new LoggerConfiguration()
            .WriteTo.Http("http://localhost:5044", 1)
                .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .CreateLogger();
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.Information(JsonConvert.SerializeObject("Nunca mais eu vou salvar logs no banco de dados!"));

            var lista = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            return lista;
        }
    }
}