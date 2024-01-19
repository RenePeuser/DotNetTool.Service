using System.Threading.Tasks;
using DotNetTool.Service.Models;

namespace DotNetTool.Service.Services
{
    internal interface IProcessService
    {
        Task<CliRunResult> RunCliCommandAsync(string command, string arguments);
    }
}
