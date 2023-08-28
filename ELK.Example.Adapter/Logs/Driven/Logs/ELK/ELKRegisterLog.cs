using ELK.Example.Domain.Ports.Input;
using ELK.Example.ELK.Example.Adapter.Logs.Driven.DTO.ELK;
using Newtonsoft.Json;
using Serilog;

namespace ELK.Example.ELK.Example.Adapter.Logs.Driven.Logs.ELK
{
    public class ELKRegisterLog : IELKRegisterLog
    {
        private readonly Serilog.ILogger _logger;

        public ELKRegisterLog()
        {
            _logger = new LoggerConfiguration()
                .WriteTo.LogstashHttp("http://localhost:5044")
                .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .CreateLogger();
        }
        public void PublishLog(LogModel logModel)
        {
            var elkLogDTO = new ELKLogDTO();
            elkLogDTO.Body = logModel.Body;
            elkLogDTO.RegisterId = Guid.NewGuid();
            elkLogDTO.StatusCode = logModel.StatusCode;
            elkLogDTO.Message = logModel.Message;
            elkLogDTO.Method = logModel.Method;
            elkLogDTO.Path = logModel.Path;

            _logger.Information(JsonConvert.SerializeObject(logModel));
        }
    }
}