using ELK.Example.ELK.Example.Adapter.Logs;

namespace ELK.Example.Domain.Ports.Input
{
    public interface IWeatherForecastsOutputPort
    {
        void PublishLog(LogModel logModel);
    }
}