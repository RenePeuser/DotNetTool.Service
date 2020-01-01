namespace DotNetTool.Service.Services
{
    internal interface IProcessBuilder
    {
        IProcess BuildFrom(string command, string arguments);
    }
}
