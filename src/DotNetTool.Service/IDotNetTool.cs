using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetTool.Service.Models;

namespace DotNetTool.Service
{
    public interface IDotNetTool
    {
        Task<CliRunResult> UpdateAsync(string toolName);
        Task<CliRunResult> UpdateAsync(string toolName, string toolPath);
        Task<IEnumerable<DotNetToolInfo>> GetAllInstalledAsync();
        Task<IEnumerable<DotNetToolInfo>> GetAllInstalledAsync(string localPath);
        Task<CliRunResult> RunAsync(string command, string arguments);
        Task<CliRunResult> InstallAsync(string toolName);
        Task<CliRunResult> InstallAsync(string toolName, Uri source);
        Task<CliRunResult> InstallAsync(string toolName, string version);
        Task<CliRunResult> InstallAsync(string toolName, string version, Uri source);
        Task<CliRunResult> InstallAsync(string toolName, string version, string toolPath);

        Task<CliRunResult> InstallAsync(string toolName,
                                        string version,
                                        string toolPath,
                                        Uri source);

        Task<CliRunResult> UninstallAsync(string toolName);
        Task<CliRunResult> UninstallAsync(string toolName, string toolPath);
        Task<DotNetToolInfo?> ExistsAsync(string toolName);
        Task<DotNetToolInfo?> ExistsAsync(string toolName, string localPath);
    }
}
