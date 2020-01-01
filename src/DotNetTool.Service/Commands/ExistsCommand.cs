namespace DotNetTool.Service.Commands
{
    using System.Linq;
    using System.Threading.Tasks;
    using ArgumentCheck;
    using Models;

    public class ExistsCommand
    {
        private readonly ListCommand _listCommand;

        public ExistsCommand(ListCommand listCommand)
        {
            Throw.IfNull(() => listCommand);

            _listCommand = listCommand;
        }

        public async Task<DotNetToolInfo> ExistsAsync(string toolName)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);

            var result = await _listCommand.ListAsync();
            return result.FirstOrDefault(tool => tool.Name.ToLower() == toolName.ToLower());
        }

        public async Task<DotNetToolInfo> ExistsAsync(string toolName, string localPath)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);

            var result = await _listCommand.ListAsync(localPath);
            return result.FirstOrDefault(tool => tool.Name.ToLower() == toolName.ToLower());
        }
    }
}