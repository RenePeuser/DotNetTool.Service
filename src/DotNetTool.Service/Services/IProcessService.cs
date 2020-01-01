using System.Threading.Tasks;
using DotNetTool.Service.Models;

namespace DotNetTool.Service.Services
{
    public interface IProcessService
    {
        Task<CliRunResult> RunCliCommandAsync(string command, string arguments);
        Task<CliRunResult> StartCliCommandAsync(string command, string arguments);
    }
}
