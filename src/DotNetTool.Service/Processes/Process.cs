using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetTool.Service.Processes
{
    public static class Process
    {
        public static async Task<int> CompleteAsync(this System.Diagnostics.Process process, CancellationToken? cancellationToken = null)
        {
            return await Task.Run(() =>
            {
                process.WaitForExit();

                return Task.FromResult(process.ExitCode);
            }, cancellationToken ?? CancellationToken.None);
        }

        public static System.Diagnostics.Process StartProcess(string command,
                                                              string args,
                                                              string? workingDir = null,
                                                              Action<string>? stdOut = null,
                                                              Action<string>? stdErr = null,
                                                              params (string key, string value)[] environmentVariables)
        {
            args ??= "";

            var process = new System.Diagnostics.Process
            {
                StartInfo =
                {
                    Arguments = args,
                    FileName = command,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    UseShellExecute = false
                }
            };

            if (!string.IsNullOrWhiteSpace(workingDir)) process.StartInfo.WorkingDirectory = workingDir;

            if (environmentVariables.Length > 0)
                for (var i = 0; i < environmentVariables.Length; i++)
                {
                    var (key, value) = environmentVariables[i];
                    process.StartInfo.Environment.Add(key, value);
                }

            if (stdOut is not null)
                process.OutputDataReceived += (sender, eventArgs) =>
                {
                    if (eventArgs.Data is not null)
                    {
                        stdOut(eventArgs.Data);
                    }
                };

            if (stdErr is not null)
                process.ErrorDataReceived += (sender, eventArgs) =>
                {
                    if (eventArgs.Data is not null)
                    {
                        stdErr(eventArgs.Data);
                    }
                };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            return process;
        }
    }
}