namespace DotNetTool.Service.Commands
{
    using System.Linq;
    using System.Threading.Tasks;
    using ArgumentCheck;
    using Models;

    internal class ExistsCommand
    {
        private readonly ListCommand _listCommand;

        internal ExistsCommand(ListCommand listCommand)
        {
            Throw.IfNull(() => listCommand);

            _listCommand = listCommand;
        }

        internal async Task<DotNetToolInfo?> ExistsAsync(string toolName)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);

            var result = await _listCommand.ListAsync();
            return result.FirstOrDefault(tool => tool.Name.ToLower() == toolName.ToLower());
        }

        internal async Task<DotNetToolInfo?> ExistsAsync(string toolName, string localPath)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);
            Throw.IfNullOrWhiteSpace(() => localPath);

            var result = await _listCommand.ListAsync(localPath);
            return result.FirstOrDefault(tool => tool.Name.ToLower() == toolName.ToLower());
        }
    }
}