namespace DotNetTool.Service.Models
{
    using System.Diagnostics;

    [DebuggerDisplay("ExitCode: '{" + nameof(ExitCode) + "}'")]
    public class CliRunResult
    {
        public CliRunResult(int exitCode, string output)
        {
            ExitCode = exitCode;
            Output = output;
        }

        public int ExitCode { get; }

        public string Output { get; }
    }
}