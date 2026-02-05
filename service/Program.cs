using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using System.Reflection;
using System.Configuration.Install;
using System.ComponentModel;
using System.Windows.Forms;
using CSI_Library;


namespace CSIServer
{
    /// <summary>
    /// Version 2.0: 
    /// a) Added SMESSER code to get rid the annoying alert box 
    ///    at the end of installation/uninstallation.
    /// b) Added code to make this approch more secure (Thanks to PIEBALDconsult concern) 
    ///    by prompting the user, is he/she REALLY likes to install/uninstall the service.
    /// c) Moved the WSInstaller class to a WSInstaller.cs for a cleaner code
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
         

        static void Main()
        {

       


            bool _IsInstalled = false;
            bool serviceStarting = false; // Thanks to SMESSER's implementation V2.0
            string SERVICE_NAME = "CSIFLEX_Server";

            ServiceController[] services = ServiceController.GetServices();

            foreach (ServiceController service in services)
            {
                if (service.ServiceName.Equals(SERVICE_NAME))
                {
                    _IsInstalled = true;
                    if (service.Status == ServiceControllerStatus.StartPending)
                    {
                        // If the status is StartPending then the service was started via the SCM             
                        serviceStarting = true;
                    }
                    break;
                }
            }

            if (!serviceStarting)
            {
                if (_IsInstalled == true)
                {
                    // Thanks to PIEBALDconsult's Concern V2.0
                    DialogResult dr = new DialogResult();
                    dr = MessageBox.Show("Do you REALLY like to uninstall the " + SERVICE_NAME + "?", "Danger", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        SelfInstaller.UninstallMe();
                        MessageBox.Show("Successfully uninstalled the " + SERVICE_NAME, "Status",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    DialogResult dr = new DialogResult();
                    dr = MessageBox.Show("Do you REALLY like to install the " + SERVICE_NAME + "?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        SelfInstaller.InstallMe();
                        MessageBox.Show("Successfully installed the " + SERVICE_NAME, "Status",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                // Started from the SCM
                System.ServiceProcess.ServiceBase[] servicestorun;
                servicestorun = new System.ServiceProcess.ServiceBase[] { new CSIServer() };
                ServiceBase.Run(servicestorun);
            }
        }
    }

    public static class SelfInstaller
    {
        private static readonly string _exePath = Assembly.GetExecutingAssembly().Location;
        public static bool InstallMe()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(
                    new string[] { _exePath });
            }
            catch(Exception ex)
            {
                System.Console.WriteLine("Install Me Error:" + ex.Message);
                return false;
            }
            return true;
        }

        public static bool UninstallMe()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(
                    new string[] { "/u", _exePath });
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}