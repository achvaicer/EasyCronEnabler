using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Configuration.Install;

namespace EasyCronEnabler
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                if (!args.Any())
                    new EasyCronEnabler().LoopCheck();
                else
                {
                    switch (args[0].Trim())
                    {
                        case "/i":
                        case "/install":
                            Install();
                            break;
                        case "/u":
                        case "/uninstall":
                            Uninstall();
                            break;
                    }
                }
            }
            else
            {
                var servicesToRun = new ServiceBase[]
                    {
                        new EasyCronEnabler()
                    };
                ServiceBase.Run(servicesToRun);
            }
        }

        private static void Install()
        {
            try
            {
                var svc = new ServiceController(ProjectInstaller.ServiceName);
                Console.WriteLine(svc.DisplayName);
                if (svc.Status == ServiceControllerStatus.Running)
                    svc.Stop();
                Uninstall();
            }
            catch (InvalidOperationException) { }
            ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });
        }

        private static void Uninstall()
        {
            ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });
        }
    }
}
