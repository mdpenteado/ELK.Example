using ELK.Example.ELK.Example.Adapter.Logs.Driven.DTO.ELK;
using ELK.Example.ELK.Example.Adapter.Logs.Driven.Logs.ELK;

namespace ELK.Example.ELK.Example.Adapter.Logs.Facades
{
    public class LogFacade : ILogFacade
    {
        private readonly IELKRegisterLog _elkLog;

        public LogFacade(IELKRegisterLog elkLog)
        {
            _elkLog = elkLog;
        }

        public void CreateLog(LogModel logModel)
        {
            _elkLog.PublishLog(logModel);
        }
    }
}