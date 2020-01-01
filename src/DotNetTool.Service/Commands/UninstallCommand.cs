using DotNetTool.Service.Services;

namespace DotNetTool.Service.Commands
{
    using System.Threading.Tasks;
    using ArgumentCheck;
    using Models;

    internal class UninstallCommand
    {
        private readonly IProcessService _processService;

        internal UninstallCommand(IProcessService processService)
        {
            _processService = processService;
        }

        internal Task<CliRunResult> UninstallAsync(string toolName)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);

            return _processService.RunCliCommandAsync("dotnet", $"tool uninstall {toolName} --global");
        }

        internal Task<CliRunResult> UninstallAsync(string toolName, string toolPath)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);
            Throw.IfNullOrWhiteSpace(() => toolPath);

            return _processService.RunCliCommandAsync("dotnet", $"tool uninstall {toolName} --tool-path {toolPath}");
        }
    }
}