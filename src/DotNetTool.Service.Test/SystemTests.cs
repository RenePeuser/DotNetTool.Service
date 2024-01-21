using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetTool.Service.Test
{
    [TestClass]
    public class SystemTests
    {
        private const string DotNetToolToInstall = "DotNetTool.Builder";
        private const string DotNetToolVersionToInstall = "0.5.6-beta";
        private const string OlderDotNetToolVersion = "0.5.5-beta";
        private readonly Uri _sourceUri = new("https://api.nuget.org/v3/index.json");
        private DotNetTool _dotNetTool = null!;
        private string _toolPath = null!;

        [TestInitialize]
        public async Task Init()
        {
            _toolPath = Path.Combine(Environment.CurrentDirectory, "Tool");
            _dotNetTool = DotNetToolFactory.Create();

            var existingTool = await _dotNetTool.ExistsAsync(DotNetToolToInstall).ConfigureAwait(false);
            if (existingTool != null)
            {
                var uninstallResult = await _dotNetTool.UninstallAsync(DotNetToolToInstall).ConfigureAwait(false);
                if (uninstallResult.ExitCode != 0)
                {
                    throw new DotNetToolException(uninstallResult.Output);
                }
            }
        }

        [TestMethod]
        public async Task Run_Custom_Command()
        {
            var installResult = await _dotNetTool.RunAsync("dotnet", "tool list -g").ConfigureAwait(false);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);
        }

        [TestMethod]
        public async Task Install_No_Version_Command_Should_Install_Expected_Tool()
        {
            var installResult = await _dotNetTool.InstallAsync(DotNetToolToInstall).ConfigureAwait(false);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);
        }

        [TestMethod]
        public async Task Install_With_Uri_Command_Should_Install_Expected_Tool()
        {
            var installResult = await _dotNetTool.InstallAsync(DotNetToolToInstall, _sourceUri).ConfigureAwait(false);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);
        }

        [TestMethod]
        public async Task Install_Command_Should_Install_Expected_Tool()
        {
            var installResult = await _dotNetTool.InstallAsync(DotNetToolToInstall, DotNetToolVersionToInstall).ConfigureAwait(false);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);
        }

        [TestMethod]
        public async Task Install_Command_Should_Install_Expected_Tool_With_Uri()
        {
            var installResult = await _dotNetTool.InstallAsync(DotNetToolToInstall, DotNetToolVersionToInstall, _sourceUri).ConfigureAwait(false);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);
        }

        [TestMethod]
        public async Task Install_Command_Should_Install_Expected_Tool_Also_With_Tool_Path()
        {
            var installResult = await _dotNetTool.InstallAsync(DotNetToolToInstall, DotNetToolVersionToInstall, _toolPath).ConfigureAwait(false);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);
        }

        [TestMethod]
        public async Task Install_Command_Should_Install_Expected_Tool_Also_With_Tool_Path_With_Uri()
        {
            var installResult = await _dotNetTool.InstallAsync(DotNetToolToInstall, DotNetToolVersionToInstall, _toolPath, _sourceUri).ConfigureAwait(false);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);
        }

        [TestMethod]
        public async Task Uninstall_Command_Should_Uninstall_Expected_Tool()
        {
            var installResult = await _dotNetTool.InstallAsync(DotNetToolToInstall, DotNetToolVersionToInstall).ConfigureAwait(false);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);

            var uninstallResult = await _dotNetTool.UninstallAsync(DotNetToolToInstall).ConfigureAwait(false);
            Assert.AreEqual(0, uninstallResult.ExitCode, uninstallResult.Output);
        }

        [TestMethod]
        public async Task Uninstall_Command_Should_Uninstall_Expected_Tool_Also_With_Tool_Path()
        {
            var installResult = await _dotNetTool.InstallAsync(DotNetToolToInstall, DotNetToolVersionToInstall, _toolPath).ConfigureAwait(false);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);

            var uninstallResult = await _dotNetTool.UninstallAsync(DotNetToolToInstall, _toolPath).ConfigureAwait(false);
            Assert.AreEqual(0, uninstallResult.ExitCode, uninstallResult.Output);
        }

        [TestMethod]
        public async Task Update_Command_Should_Update_Existing_Version()
        {
            var gitversionTool = "GitVersion.Tool";

            var existingTool = await _dotNetTool.ExistsAsync(gitversionTool).ConfigureAwait(false);
            if (existingTool != null)
            {
                var uninstallResult = await _dotNetTool.UninstallAsync(gitversionTool).ConfigureAwait(false);
                Assert.AreEqual(0, uninstallResult.ExitCode, uninstallResult.Output);
            }

            var installResult = await _dotNetTool.InstallAsync(gitversionTool, "5.1.2").ConfigureAwait(false);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);

            var updateResult = await _dotNetTool.UpdateAsync(gitversionTool).ConfigureAwait(false);
            Assert.AreEqual(0, updateResult.ExitCode, updateResult.Output);
        }

        [TestMethod]
        public async Task Update_Command_Should_Update_Existing_Version_Also_With_Tool_Path()
        {
            var gitversionTool = "GitVersion.Tool";

            var existingTool = await _dotNetTool.ExistsAsync(gitversionTool, _toolPath).ConfigureAwait(false);
            if (existingTool != null)
            {
                var uninstallResult = await _dotNetTool.UninstallAsync(gitversionTool, _toolPath).ConfigureAwait(false);
                Assert.AreEqual(0, uninstallResult.ExitCode, uninstallResult.Output);
            }

            var installResult = await _dotNetTool.InstallAsync(gitversionTool, "5.1.2", _toolPath).ConfigureAwait(false);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);

            var updateResult = await _dotNetTool.UpdateAsync(gitversionTool, _toolPath).ConfigureAwait(false);
            Assert.AreEqual(0, updateResult.ExitCode, updateResult.Output);
        }

        [Ignore]
        [TestMethod]
        public async Task Update_Should_Not_Work_With_PreRelease_Versions()
        {
            var installResult = await _dotNetTool.InstallAsync(DotNetToolToInstall, OlderDotNetToolVersion).ConfigureAwait(false);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);

            var updateResult = await _dotNetTool.UpdateAsync(DotNetToolToInstall).ConfigureAwait(false);
            Assert.AreEqual(1, updateResult.ExitCode, updateResult.Output);
        }

        [Ignore]
        [TestMethod]
        public async Task Update_Should_Not_Work_With_PreRelease_Versions_Also_With_Tool_Path()
        {
            var installResult = await _dotNetTool.InstallAsync(DotNetToolToInstall, OlderDotNetToolVersion, _toolPath).ConfigureAwait(false);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);

            var updateResult = await _dotNetTool.UpdateAsync(DotNetToolToInstall, _toolPath).ConfigureAwait(false);
            Assert.AreEqual(1, updateResult.ExitCode, updateResult.Output);
        }

        [TestMethod]
        public async Task Exists_Command_Should_Return_Existing_Tool()
        {
            var installResult = await _dotNetTool.InstallAsync(DotNetToolToInstall, DotNetToolVersionToInstall).ConfigureAwait(false);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);

            var exist = await _dotNetTool.ExistsAsync(DotNetToolToInstall).ConfigureAwait(false);
            Assert.AreEqual(exist?.Name.ToUpperInvariant(), DotNetToolToInstall.ToUpperInvariant());
        }

        [TestMethod]
        public async Task Exists_Command_Should_Return_Existing_Tool_Also_With_Tool_Path()
        {
            var installResult = await _dotNetTool.InstallAsync(DotNetToolToInstall, DotNetToolVersionToInstall, _toolPath).ConfigureAwait(false);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);

            var exist = await _dotNetTool.ExistsAsync(DotNetToolToInstall, _toolPath).ConfigureAwait(false);
            Assert.AreEqual(exist?.Name.ToUpperInvariant(), DotNetToolToInstall.ToUpperInvariant());
        }

        [TestMethod]
        public async Task List_Command_Should_Return_All_Installed_Tools()
        {
            var installResult = await _dotNetTool.InstallAsync(DotNetToolToInstall, DotNetToolVersionToInstall).ConfigureAwait(false);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);

            var installedTools = await _dotNetTool.GetAllInstalledAsync().ConfigureAwait(false);
            Assert.IsTrue(installedTools.Any(tool => tool.Name.ToUpperInvariant().Equals(DotNetToolToInstall.ToUpperInvariant(), StringComparison.OrdinalIgnoreCase)));
        }

        [TestMethod]
        public async Task List_Command_Should_Return_All_Installed_Tools_From_Tool_Path()
        {
            var installResult = await _dotNetTool.InstallAsync(DotNetToolToInstall, DotNetToolVersionToInstall, _toolPath).ConfigureAwait(false);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);

            var installedTools = await _dotNetTool.GetAllInstalledAsync(_toolPath).ConfigureAwait(false);
            Assert.IsTrue(installedTools.Any(tool => tool.Name.Equals(DotNetToolToInstall, StringComparison.OrdinalIgnoreCase)));
        }

        [TestCleanup]
        public async Task CleanupAsync()
        {
            var globalExistingTool = await _dotNetTool.ExistsAsync(DotNetToolToInstall).ConfigureAwait(false);
            if (globalExistingTool != null)
            {
                var uninstallResult = await _dotNetTool.UninstallAsync(DotNetToolToInstall).ConfigureAwait(false);
                if (uninstallResult.ExitCode != 0)
                {
                    throw new DotNetToolException(uninstallResult.Output);
                }
            }

            // Timing sometimes test folder is cleaned up already, so no need to uninstall anything.
            if (Directory.Exists(_toolPath))
            {
                var localTool = await _dotNetTool.ExistsAsync(DotNetToolToInstall, _toolPath).ConfigureAwait(false);
                if (localTool != null)
                {
                    var uninstallResult = await _dotNetTool.UninstallAsync(DotNetToolToInstall, _toolPath).ConfigureAwait(false);
                    if (uninstallResult.ExitCode != 0)
                    {
                        throw new DotNetToolException(uninstallResult.Output);
                    }
                }
            }
        }
    }
}
