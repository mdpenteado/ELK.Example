using ELK.Example.ELK.Example.Domain.Models;

namespace ELK.Example.Domain.Ports.Output
{
    public interface IWeatherForecastsInputPort
    {
        WeatherForecastModel GetWeatherForecastById(int id);
        IList<WeatherForecastModel> GetAllWeatherForecasts();
    }
}