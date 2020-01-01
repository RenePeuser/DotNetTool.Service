using DotNetTool.Service.Services;

namespace DotNetTool.Service.Commands
{
    using System.Threading.Tasks;
    using ArgumentCheck;
    using Models;

    internal class InstallCommand
    {
        private readonly IProcessService _processService;

        internal InstallCommand(IProcessService processService)
        {
            _processService = processService;
        }

        internal Task<CliRunResult> InstallAsync(string toolName, string version)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);
            Throw.IfNullOrWhiteSpace(() => version);

            return _processService.RunCliCommandAsync("dotnet", $"tool install {toolName} --version {version} --global");
        }

        internal Task<CliRunResult> InstallAsync(string toolName, string version, string toolPath)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);
            Throw.IfNullOrWhiteSpace(() => version);
            Throw.IfNullOrWhiteSpace(() => toolPath);

            return _processService.RunCliCommandAsync("dotnet", $"tool install {toolName} --version {version} --tool-path {toolPath}");
        }
    }
}