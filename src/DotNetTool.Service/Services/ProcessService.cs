using System.Text;
using System.Threading.Tasks;
using DotNetTool.Service.Models;
using DotNetTool.Service.Processes;

namespace DotNetTool.Service.Services
{
    internal class ProcessService : IProcessService
    {
        public Task<CliRunResult> RunCliCommandAsync(string command, string arguments)
        {
            var stringBuilder = new StringBuilder();
            var errorStringBuilder = new StringBuilder();

            var process = Process.StartProcess(command, arguments, null, std => stringBuilder.AppendLine(std), error => errorStringBuilder.AppendLine(error));
            process.WaitForExit();

            var output = process.ExitCode == 0 ? stringBuilder.ToString() : errorStringBuilder.ToString();
            var cliRunResult = new CliRunResult(process.ExitCode, output);
            return Task.FromResult(cliRunResult);
        }
    }
}
