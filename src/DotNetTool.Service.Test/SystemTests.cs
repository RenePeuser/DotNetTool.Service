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
        private DotNetTool _dotNetTool;
        private string _toolPath;
        private const string DotNetToolToInstall = "DotNetTool.Builder";
        private const string DotNetToolVersionToInstall = "0.5.6-beta";
        private const string OlderDotNetToolVersion = "0.5.5-beta";

        [TestInitialize]
        public async Task Init()
        {
            _toolPath = Path.Combine(Environment.CurrentDirectory, "Tool");
            _dotNetTool = DotNetToolFactory.Create();

            var existingTool = await _dotNetTool.ExistsAsync(DotNetToolToInstall);
            if (existingTool != null)
            {
                var uninstallResult = await _dotNetTool.UninstallAsync(DotNetToolToInstall);
                if (uninstallResult.ExitCode != 0)
                {
                    throw new Exception(uninstallResult.Output);
                }
            }
        }

        [TestMethod]
        public async Task Install_Command_Should_Install_Expected_Tool()
        {
            var installResult = await _dotNetTool.InstallAsync(DotNetToolToInstall, DotNetToolVersionToInstall);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);
        }

        [TestMethod]
        public async Task Install_Command_Should_Install_Expected_Tool_Also_With_Tool_Path()
        {
            var installResult = await _dotNetTool.InstallAsync(DotNetToolToInstall, DotNetToolVersionToInstall, _toolPath);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);
        }

        [TestMethod]
        public async Task Uninstall_Command_Should_Uninstall_Expected_Tool()
        {
            var installResult = await _dotNetTool.InstallAsync(DotNetToolToInstall, DotNetToolVersionToInstall);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);

            var uninstallResult = await _dotNetTool.UninstallAsync(DotNetToolToInstall);
            Assert.AreEqual(0, uninstallResult.ExitCode, uninstallResult.Output);
        }

        [TestMethod]
        public async Task Uninstall_Command_Should_Uninstall_Expected_Tool_Also_With_Tool_Path()
        {
            var installResult = await _dotNetTool.InstallAsync(DotNetToolToInstall, DotNetToolVersionToInstall, _toolPath);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);

            var uninstallResult = await _dotNetTool.UninstallAsync(DotNetToolToInstall, _toolPath);
            Assert.AreEqual(0, uninstallResult.ExitCode, uninstallResult.Output);
        }

        [TestMethod]
        public async Task Update_Command_Should_Update_Existing_Version()
        {
            var gitversionTool = "GitVersion.Tool";

            var existingTool = await _dotNetTool.ExistsAsync(gitversionTool);
            if (existingTool != null)
            {
                var uninstallResult = await _dotNetTool.UninstallAsync(gitversionTool);
                Assert.AreEqual(0, uninstallResult.ExitCode, uninstallResult.Output);
            }

            var installResult = await _dotNetTool.InstallAsync(gitversionTool, "5.1.2");
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);

            var updateResult = await _dotNetTool.UpdateAsync(gitversionTool);
            Assert.AreEqual(0, updateResult.ExitCode, updateResult.Output);
        }

        [TestMethod]
        public async Task Update_Command_Should_Update_Existing_Version_Also_With_Tool_Path()
        {
            var gitversionTool = "GitVersion.Tool";

            var existingTool = await _dotNetTool.ExistsAsync(gitversionTool, _toolPath);
            if (existingTool != null)
            {
                var uninstallResult = await _dotNetTool.UninstallAsync(gitversionTool, _toolPath);
                Assert.AreEqual(0, uninstallResult.ExitCode, uninstallResult.Output);
            }

            var installResult = await _dotNetTool.InstallAsync(gitversionTool, "5.1.2", _toolPath);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);

            var updateResult = await _dotNetTool.UpdateAsync(gitversionTool, _toolPath);
            Assert.AreEqual(0, updateResult.ExitCode, updateResult.Output);
        }

        [TestMethod]
        public async Task Update_Should_Not_Work_With_PreRelease_Versions()
        {
            var installResult = await _dotNetTool.InstallAsync(DotNetToolToInstall, OlderDotNetToolVersion);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);

            var updateResult = await _dotNetTool.UpdateAsync(DotNetToolToInstall);
            Assert.AreEqual(1, updateResult.ExitCode, updateResult.Output);
        }

        [TestMethod]
        public async Task Update_Should_Not_Work_With_PreRelease_Versions_Also_With_Tool_Path()
        {
            var installResult = await _dotNetTool.InstallAsync(DotNetToolToInstall, OlderDotNetToolVersion, _toolPath);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);

            var updateResult = await _dotNetTool.UpdateAsync(DotNetToolToInstall, _toolPath);
            Assert.AreEqual(1, updateResult.ExitCode, updateResult.Output);
        }

        [TestMethod]
        public async Task Exists_Command_Should_Return_Existing_Tool()
        {
            var installResult = await _dotNetTool.InstallAsync(DotNetToolToInstall, DotNetToolVersionToInstall);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);

            var exist = await _dotNetTool.ExistsAsync(DotNetToolToInstall);
            Assert.AreEqual(exist.Name.ToLower(), DotNetToolToInstall.ToLower());
        }

        [TestMethod]
        public async Task Exists_Command_Should_Return_Existing_Tool_Also_With_Tool_Path()
        {
            var installResult = await _dotNetTool.InstallAsync(DotNetToolToInstall, DotNetToolVersionToInstall, _toolPath);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);

            var exist = await _dotNetTool.ExistsAsync(DotNetToolToInstall, _toolPath);
            Assert.AreEqual(exist.Name.ToLower(), DotNetToolToInstall.ToLower());
        }

        [TestMethod]
        public async Task List_Command_Should_Return_All_Installed_Tools()
        {
            var installResult = await _dotNetTool.InstallAsync(DotNetToolToInstall, DotNetToolVersionToInstall);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);

            var installedTools = await _dotNetTool.GetAllInstalledAsync();
            Assert.IsTrue(installedTools.Any(tool => tool.Name.ToLower() == DotNetToolToInstall.ToLower()));
        }

        [TestMethod]
        public async Task List_Command_Should_Return_All_Installed_Tools_From_Tool_Path()
        {
            var installResult = await _dotNetTool.InstallAsync(DotNetToolToInstall, DotNetToolVersionToInstall, _toolPath);
            Assert.AreEqual(0, installResult.ExitCode, installResult.Output);

            var installedTools = await _dotNetTool.GetAllInstalledAsync(_toolPath);
            Assert.IsTrue(installedTools.Any(tool => tool.Name.ToLower() == DotNetToolToInstall.ToLower()));
        }

        [TestCleanup]
        public async Task CleanupAsync()
        {
            var globalExistingTool = await _dotNetTool.ExistsAsync(DotNetToolToInstall);
            if (globalExistingTool != null)
            {
                var uninstallResult = await _dotNetTool.UninstallAsync(DotNetToolToInstall);
                if (uninstallResult.ExitCode != 0)
                {
                    throw new Exception(uninstallResult.Output);
                }
            }

            var localTool = await _dotNetTool.ExistsAsync(DotNetToolToInstall, _toolPath);
            if (localTool != null)
            {
                var uninstallResult = await _dotNetTool.UninstallAsync(DotNetToolToInstall, _toolPath);
                if (uninstallResult.ExitCode != 0)
                {
                    throw new Exception(uninstallResult.Output);
                }
            }
        }
    }
}
