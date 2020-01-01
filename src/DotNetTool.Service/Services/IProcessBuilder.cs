namespace DotNetTool.Service.Services
{
    public interface IProcessBuilder
    {
        IProcess BuildFrom(string command, string arguments);
    }
}
