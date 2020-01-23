using DotNetTool.Service.Services;

namespace DotNetTool.Service.Commands
{
    using System;
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

        internal Task<CliRunResult> InstallAsync(string toolName)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);

            return _processService.RunCliCommandAsync("dotnet", $"tool install {toolName} --global");
        }

        internal Task<CliRunResult> InstallAsync(string toolName, Uri version)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);

            return _processService.RunCliCommandAsync("dotnet", $"tool install {toolName} --global --add-source {version.AbsoluteUri}");
        }

        internal Task<CliRunResult> InstallAsync(string toolName, string version)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);
            Throw.IfNullOrWhiteSpace(() => version);

            return _processService.RunCliCommandAsync("dotnet", $"tool install {toolName} --version {version} --global");
        }

        internal Task<CliRunResult> InstallAsync(string toolName, string version, Uri uri)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);
            Throw.IfNullOrWhiteSpace(() => version);

            return _processService.RunCliCommandAsync("dotnet", $"tool install {toolName} --version {version} --global --add-source {uri.AbsoluteUri}");
        }

        internal Task<CliRunResult> InstallAsync(string toolName, string version, string toolPath)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);
            Throw.IfNullOrWhiteSpace(() => version);
            Throw.IfNullOrWhiteSpace(() => toolPath);

            return _processService.RunCliCommandAsync("dotnet", $"tool install {toolName} --version {version} --tool-path {toolPath}");
        }

        internal Task<CliRunResult> InstallAsync(string toolName, string version, string toolPath, Uri source)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);
            Throw.IfNullOrWhiteSpace(() => version);
            Throw.IfNullOrWhiteSpace(() => toolPath);

            return _processService.RunCliCommandAsync("dotnet", $"tool install {toolName} --version {version} --tool-path {toolPath} --add-source {source.AbsoluteUri}");
        }
    }
}