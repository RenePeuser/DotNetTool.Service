using System;
using System.Diagnostics;
using System.IO;
using DotNetTool.Service.ArgumentCheck;
using DotNetTool.Service.Models;

namespace DotNetTool.Service.Services
{
    internal class ProcessProxy : DisposableBase, IProcess
    {
        private readonly Process _process;

        internal ProcessProxy(Process process)
        {
            Throw.IfNull(() => process);

            _process = process;
        }

        public event EventHandler Exited;

        public bool Start()
        {
            return _process.Start();
        }

        public StreamReader StandardOutput => _process.StandardOutput;

        public bool EnableRaisingEvents
        {
            get => _process.EnableRaisingEvents;
            set
            {
                _process.EnableRaisingEvents = value;
                if (value)
                {
                    AttachEvents();
                }
                else
                {
                    DetachEvents();
                }
            }
        }

        public ProcessStartInfo StartInfo => _process.StartInfo;

        public int ExitCode => _process.ExitCode;

        private void DetachEvents()
        {
            _process.Exited -= ProcessExitedEventHandler;
        }

        private void AttachEvents()
        {
            _process.Exited += ProcessExitedEventHandler;
        }

        private void ProcessExitedEventHandler(object sender, EventArgs e)
        {
            Exited?.Invoke(sender, e);
        }

        protected override void DisposeManagedResources()
        {
            DetachEvents();
        }
    }
}
