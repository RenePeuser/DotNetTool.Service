using System.Linq;
using System.Threading.Tasks;

namespace DotNetTool.Service
{
    internal sealed class ExistsCommand
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

            var result = await _listCommand.ListAsync().ConfigureAwait(false);
            return result.FirstOrDefault(tool => tool.Name.ToUpperInvariant().EqualsTo(toolName.ToUpperInvariant()));
        }

        internal async Task<DotNetToolInfo?> ExistsAsync(string toolName, string localPath)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);
            Throw.IfNullOrWhiteSpace(() => localPath);

            var result = await _listCommand.ListAsync(localPath).ConfigureAwait(false);
            return result.FirstOrDefault(tool => tool.Name.ToUpperInvariant().EqualsTo(toolName.ToUpperInvariant()));
        }
    }
}
