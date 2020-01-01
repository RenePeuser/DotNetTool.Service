using System.Threading.Tasks;
using DotNetTool.Service.ArgumentCheck;
using DotNetTool.Service.Models;
using DotNetTool.Service.Services;

namespace DotNetTool.Service.Commands
{
    internal class UpdateCommand
    {
        private readonly IProcessService _processService;

        internal UpdateCommand(IProcessService processService)
        {
            _processService = processService;
        }

        internal Task<CliRunResult> UpdateAsync(string toolName)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);

            return _processService.RunCliCommandAsync("dotnet", $"tool update {toolName} --global");
        }

        internal Task<CliRunResult> UpdateAsync(string toolName, string toolPath)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);
            Throw.IfNullOrWhiteSpace(() => toolPath);

            return _processService.RunCliCommandAsync("dotnet", $"tool update {toolName} --tool-path {toolPath}");
        }
    }
}