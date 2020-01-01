using System.Threading.Tasks;
using DotNetTool.Service.ArgumentCheck;
using DotNetTool.Service.Models;
using DotNetTool.Service.Services;

namespace DotNetTool.Service.Commands
{
    public class UpdateCommand
    {
        private readonly IProcessService _processService;

        public UpdateCommand(IProcessService processService)
        {
            _processService = processService;
        }

        public Task<CliRunResult> UpdateAsync(string toolName)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);

            return _processService.RunCliCommandAsync("dotnet", $"tool update {toolName} --global");
        }

        public Task<CliRunResult> UpdateAsync(string toolName, string toolPath)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);

            return _processService.RunCliCommandAsync("dotnet", $"tool update {toolName} --tool-path {toolPath}");
        }
    }
}