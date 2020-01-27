using DotNetTool.Service.Commands;
using DotNetTool.Service.Services;

namespace DotNetTool.Service
{
    public static class DotNetToolFactory
    {
        public static DotNetTool Create()
        {
            var processService = new ProcessService(new ProcessBuilder());
            var installCommand = new InstallCommand(processService);
            var updateCommand = new UpdateCommand(processService);
            var uninstallCommand = new UninstallCommand(processService);
            var getAllCommand = new ListCommand(processService);
            var existsCommand = new ExistsCommand(getAllCommand);
            var customCommand = new CustomCommand(processService);

            return new DotNetTool(installCommand, updateCommand, existsCommand, uninstallCommand, getAllCommand, customCommand);
        }
    }
}