using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;


namespace CSIServer
{
    [RunInstaller(true)]
    public class WSInstaller : System.Configuration.Install.Installer
    {
        public WSInstaller()
        {
            ServiceProcessInstaller process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;
            ServiceInstaller serviceAdmin = new ServiceInstaller();
            serviceAdmin.StartType = ServiceStartMode.Automatic;
            serviceAdmin.ServiceName = "CSIFLEX_Server";
            serviceAdmin.DisplayName = "CSIFLEX_Server";
            Installers.Add(process);
            Installers.Add(serviceAdmin);

        }



    }


}
