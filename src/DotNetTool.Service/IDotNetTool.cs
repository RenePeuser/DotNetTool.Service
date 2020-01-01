using System.Collections.Generic;

namespace DotNetTool.Service
{
    using System.Threading.Tasks;
    using Models;

    internal interface IDotNetTool
    {
        Task<CliRunResult> InstallAsync(string toolName, string version);
        Task<CliRunResult> InstallAsync(string toolName, string version, string toolPath);

        Task<CliRunResult> UninstallAsync(string toolName);
        Task<CliRunResult> UninstallAsync(string toolName, string toolPath);

        Task<CliRunResult> UpdateAsync(string toolName);
        Task<CliRunResult> UpdateAsync(string toolName, string toolPath);

        Task<DotNetToolInfo> ExistsAsync(string toolName);
        Task<DotNetToolInfo> ExistsAsync(string toolName, string localPath);

        Task<IEnumerable<DotNetToolInfo>> GetAllInstalledAsync();
        Task<IEnumerable<DotNetToolInfo>> GetAllInstalledAsync(string localPath);
    }
}
