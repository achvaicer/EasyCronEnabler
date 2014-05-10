using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace EasyCronEnabler
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
             if (Environment.UserInteractive)
                new EasyCronEnabler().LoopCheck();
            else
             {
                 var servicesToRun = new ServiceBase[] 
                     { 
                         new EasyCronEnabler() 
                     };
                 ServiceBase.Run(servicesToRun);
             }
        }
}
