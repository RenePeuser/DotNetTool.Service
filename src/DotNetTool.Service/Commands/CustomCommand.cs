namespace DotNetTool.Service.Commands
{
    using System.Threading.Tasks;
    using Models;
    using Services;

    internal class CustomCommand
    {
        private readonly IProcessService _processService;

        internal CustomCommand(IProcessService processService)
        {
            _processService = processService;
        }

        public Task<CliRunResult> RunAsync(string command, string argmuents)
        {
            return _processService.RunCliCommandAsync(command, argmuents);
        }
    }
}