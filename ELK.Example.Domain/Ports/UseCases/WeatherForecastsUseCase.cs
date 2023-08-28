using ELK.Example.Domain.Ports.Output;
using ELK.Example.ELK.Example.Domain.Models;

namespace ELK.Example.Domain.Ports.UseCases
{
    public class WeatherForecastsUseCase : IWeatherForecastsInputPort
    {
        private static List<WeatherForecastModel> _weatherForecasts = new List<WeatherForecastModel>
        {
            new WeatherForecastModel
            {
                Id = 1,
                Date = new DateTime(2023, 08, 05),
                TemperatureC = 30,
                Summary = "Hot"
            },
            new WeatherForecastModel
            {
                Id = 2,
                Date = new DateTime(2023, 08, 06),
                TemperatureC = 25,
                Summary = "Warm"
            }
        };
        public IList<WeatherForecastModel> GetAllWeatherForecasts()
        {
            return _weatherForecasts;
        }

        public WeatherForecastModel GetWeatherForecastById(int id)
        {
            return _weatherForecasts.Find(w => w.Id == id);
        }
    }
}