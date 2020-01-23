using DotNetTool.Service.ArgumentCheck;

namespace DotNetTool.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Commands;
    using Models;


    public class DotNetTool : IDotNetTool
    {
        private readonly InstallCommand _installCommand;
        private readonly UpdateCommand _updateCommand;
        private readonly ExistsCommand _existsCommand;
        private readonly UninstallCommand _uninstallCommand;
        private readonly ListCommand _listCommand;

        internal DotNetTool(InstallCommand installCommand, UpdateCommand updateCommand, ExistsCommand existsCommand, UninstallCommand uninstallCommand, ListCommand listCommand)
        {
            Throw.IfNull(() => installCommand);
            Throw.IfNull(() => updateCommand);
            Throw.IfNull(() => existsCommand);
            Throw.IfNull(() => uninstallCommand);
            Throw.IfNull(() => listCommand);

            _installCommand = installCommand;
            _updateCommand = updateCommand;
            _existsCommand = existsCommand;
            _uninstallCommand = uninstallCommand;
            _listCommand = listCommand;
        }

        public Task<CliRunResult> UpdateAsync(string toolName)
        {
            return _updateCommand.UpdateAsync(toolName);
        }

        public Task<CliRunResult> UpdateAsync(string toolName, string toolPath)
        {
            return _updateCommand.UpdateAsync(toolName, toolPath);
        }

        public Task<IEnumerable<DotNetToolInfo>> GetAllInstalledAsync()
        {
            return _listCommand.ListAsync();
        }

        public Task<IEnumerable<DotNetToolInfo>> GetAllInstalledAsync(string localPath)
        {
            return _listCommand.ListAsync(localPath);
        }

        public Task<CliRunResult> InstallAsync(string toolName)
        {
            return _installCommand.InstallAsync(toolName);
        }

        public Task<CliRunResult> InstallAsync(string toolName, Uri source)
        {
            return _installCommand.InstallAsync(toolName, source);
        }

        public Task<CliRunResult> InstallAsync(string toolName, string version)
        {
            return _installCommand.InstallAsync(toolName, version);
        }

        public Task<CliRunResult> InstallAsync(string toolName, string version, Uri source)
        {
            return _installCommand.InstallAsync(toolName, version, source);
        }

        public Task<CliRunResult> InstallAsync(string toolName, string version, string toolPath)
        {
            return _installCommand.InstallAsync(toolName, version, toolPath);
        }

        public Task<CliRunResult> InstallAsync(string toolName, string version, string toolPath, Uri source)
        {
            return _installCommand.InstallAsync(toolName, version, toolPath, source);
        }

        public Task<CliRunResult> UninstallAsync(string toolName)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);

            return _uninstallCommand.UninstallAsync(toolName);
        }

        public Task<CliRunResult> UninstallAsync(string toolName, string toolPath)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);
            Throw.IfNullOrWhiteSpace(() => toolPath);

            return _uninstallCommand.UninstallAsync(toolName, toolPath);
        }

        public Task<DotNetToolInfo> ExistsAsync(string toolName)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);

            return _existsCommand.ExistsAsync(toolName);
        }

        public Task<DotNetToolInfo> ExistsAsync(string toolName, string localPath)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);

            return _existsCommand.ExistsAsync(toolName, localPath);
        }
    }
}
