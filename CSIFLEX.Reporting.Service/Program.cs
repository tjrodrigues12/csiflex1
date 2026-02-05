using CSIFLEX.Server.Library;
using System;
using Topshelf;

namespace CSIFLEX.Reporting.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            var exitCode = HostFactory.Run(x =>
            {
                x.Service<MainService>(srv =>
                {
                    srv.ConstructUsing(mainService => new MainService());
                    srv.WhenStarted(mainService => mainService.Start());
                    srv.WhenStopped(mainService => mainService.Stop());
                });
                x.RunAsLocalSystem();
                x.SetServiceName("CSIFlex_Reports_Generator_Service");
                x.SetDisplayName("CSIFLEX Reports Generator");
                x.SetDescription("Service to generate reports from the CSIFLEX server.");
            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}
