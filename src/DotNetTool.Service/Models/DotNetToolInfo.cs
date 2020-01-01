namespace DotNetTool.Service.Models
{
    using System.Diagnostics;

    [DebuggerDisplay("{" + nameof(Name) + "}")]
    public class DotNetToolInfo
    {
        public DotNetToolInfo(string name, string version, string command)
        {
            Name = name;
            Version = version;
            Command = command;
        }

        public string Name { get; }

        public string Version { get; }

        public string Command { get; }
    }
}