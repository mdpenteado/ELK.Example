using ELK.Example.ELK.Example.Adapter.Logs;

namespace ELK.Example.ELK.Example.Adapter.Logs.Facades
{
    public interface ILogFacade
    {
        void CreateLog(LogModel logModel);
    }
}
