using System;
using System.Threading.Tasks;
using DotNetTool.Service.ArgumentCheck;
using DotNetTool.Service.Extensions;
using DotNetTool.Service.Models;

namespace DotNetTool.Service.Services
{
    public class ProcessService : IProcessService
    {
        private readonly IProcessBuilder _processBuilder;

        public ProcessService(IProcessBuilder processBuilder)
        {
            Throw.IfNull(() => processBuilder);

            _processBuilder = processBuilder;
        }

        public Task<CliRunResult> RunCliCommandAsync(string command, string arguments)
        {
            var process = _processBuilder.BuildFrom(command, arguments);
            var tcs = new TaskCompletionSource<CliRunResult>();
            process.EnableRaisingEvents = true;
            process.Exited += (_, __) =>
            {
                var readToEnd = process.StandardOutput.ReadToEnd();
                tcs.TrySetResult(new CliRunResult(process.ExitCode, readToEnd));
            };

            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.Start().IfFalseThen(() => tcs.SetException(new Exception($"Failed to start cli command: {command} {arguments}")));
            return tcs.Task;
        }

        public Task<CliRunResult> StartCliCommandAsync(string command, string arguments)
        {
            var process = _processBuilder.BuildFrom(command, arguments);
            var tcs = new TaskCompletionSource<CliRunResult>();
            process.EnableRaisingEvents = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            var start = process.Start();
            if (start)
            {
                var cliRunResult = new CliRunResult(0, $"Program: '{command}' successfully started");
                tcs.SetResult(cliRunResult);
            }
            else
            {
                tcs.SetException(new Exception($"Failed to start cli command: {command} {arguments}"));
            }
            return tcs.Task;
        }
    }
}
