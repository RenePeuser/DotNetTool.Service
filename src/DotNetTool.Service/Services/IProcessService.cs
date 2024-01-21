using System.Threading.Tasks;

namespace DotNetTool.Service
{
    internal interface IProcessService
    {
        Task<CliRunResult> RunCliCommandAsync(string command, string arguments);
    }
}
