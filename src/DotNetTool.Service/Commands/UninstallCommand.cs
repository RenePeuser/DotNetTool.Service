using DotNetTool.Service.Services;

namespace DotNetTool.Service.Commands
{
    using System.Threading.Tasks;
    using ArgumentCheck;
    using Models;

    public class UninstallCommand
    {
        private readonly IProcessService _processService;

        public UninstallCommand(IProcessService processService)
        {
            _processService = processService;
        }

        public Task<CliRunResult> UninstallAsync(string toolName)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);

            return _processService.RunCliCommandAsync("dotnet", $"tool uninstall {toolName} --global");
        }

        public Task<CliRunResult> UninstallAsync(string toolName, string toolPath)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);

            return _processService.RunCliCommandAsync("dotnet", $"tool uninstall {toolName} --tool-path {toolPath}");
        }
    }
}