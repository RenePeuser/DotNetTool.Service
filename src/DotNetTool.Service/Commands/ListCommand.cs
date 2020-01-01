using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetTool.Service.Models;
using DotNetTool.Service.Extensions;
using DotNetTool.Service.Services;

namespace DotNetTool.Service.Commands
{
    internal class ListCommand
    {
        private readonly IProcessService _processService;

        internal ListCommand(IProcessService processService)
        {
            _processService = processService;
        }

        internal Task<IEnumerable<DotNetToolInfo>> ListAsync()
        {
            return GetAllInternalAsync(null);
        }

        internal Task<IEnumerable<DotNetToolInfo>> ListAsync(string path)
        {
            return GetAllInternalAsync(path);
        }

        private async Task<IEnumerable<DotNetToolInfo>> GetAllInternalAsync(string path)
        {
            // Package Id                              Version      Commands
            // ------------------------------------------------------------------------
            // gitversion.tool                         5.0.1        dotnet-gitversion

            var commandArguments = path.IsNullOrWhiteSpace() ? "tool list --global" : $"tool list --tool-path {path}";

            var listResult = await _processService.RunCliCommandAsync("dotnet", commandArguments);
            if (listResult.ExitCode != 0)
            {
                throw new Exception($"Error occured: '{listResult.Output}'");
            }

            var toolRows = listResult.Output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var dotNetToolRows = toolRows.Reverse().TakeWhile(toolRow => !toolRow.StartsWith("----")).ToList();
            var dotNetTools = dotNetToolRows.Select(row =>
            {
                var toolInfo = row.Split().FilterNullOrWhitespace().ToArray();
                return new DotNetToolInfo(toolInfo[0], toolInfo[1], toolInfo[2]);
            }).ToList();

            return dotNetTools;
        }
    }
}