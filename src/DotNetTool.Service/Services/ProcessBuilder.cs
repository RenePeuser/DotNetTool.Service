using System.Diagnostics;
using DotNetTool.Service.ArgumentCheck;

namespace DotNetTool.Service.Services
{
    public class ProcessBuilder : IProcessBuilder
    {
        public IProcess BuildFrom(string command, string arguments)
        {
            Throw.IfNullOrWhiteSpace(() => command);
            Throw.IfNullOrWhiteSpace(() => arguments);

            var process = new ProcessProxy(new Process());

            process.StartInfo.FileName = command;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            return process;
        }
    }
}
