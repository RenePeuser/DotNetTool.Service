using System.Threading.Tasks;

namespace DotNetTool.Service
{
    internal sealed class UpdateCommand
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
