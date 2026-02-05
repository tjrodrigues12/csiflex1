// Copyright (c) 2018 CSIFLEX, All Rights Reserved.


using CSIFLEX.Utilities;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;

namespace FocasLibrary.Tools
{
    public static class AgentManagement
    {
        public static void Install(string exePath)
        {
            string dirPath = Path.GetDirectoryName(exePath);

            string cmd = "\"" + exePath + "\" install";
            // string cmd = "sc create ""+Names.AGENT_SERVICE_NAME+"" binPath= "+Paths.ADAPTERS +"";
            var info = new ProcessStartInfo();
            info.FileName = "cmd";
            info.Arguments = "cmd /c cd \"" + dirPath + "\" & " + cmd;
            info.UseShellExecute = false;
            //info.UseShellExecute = true;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.Verb = "runas";

            var process = new Process();
            process.StartInfo = info;

            process.Start();
            process.WaitForExit();
        }

        public static void Install(string exePath, string serviceName)
        {
            string dirPath = Path.GetDirectoryName(exePath);

            //string cmd = "\"" + exePath + "\" install";

            string cmd = $"sc create { serviceName } binPath= \"{ Path.Combine(exePath, "agent.exe") }\" start= auto";

            var info = new ProcessStartInfo();
            info.FileName = "cmd";
            info.Arguments = $"cmd /c { cmd }";
            info.UseShellExecute = false;
            //info.UseShellExecute = true;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.Verb = "runas";

            var process = new Process();
            process.StartInfo = info;

            process.Start();
            process.WaitForExit();

            RegistryAgent(serviceName, Path.Combine(exePath, "agent.cfg"));
        }


        private static void RegistryAgent(string serviceName, string cfgPath)
        {
            try
            {
                RegistryKey baseKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\\WOW6432Node");

                RegistryKey mtcKey = baseKey.CreateSubKey("MTConnect");

                RegistryKey srvKey = mtcKey.CreateSubKey(serviceName);

                srvKey.SetValue("ConfigurationFile", cfgPath);

                srvKey.Close();
                mtcKey.Close();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }


        public static void Uninstall(string serviceName)
        {
            string cmd = "sc delete " + serviceName;

            var info = new ProcessStartInfo();
            info.FileName = "cmd";
            info.Arguments = "cmd /c" + cmd;
            info.UseShellExecute = true;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.Verb = "runas";

            var process = new Process();
            process.StartInfo = info;

            process.Start();
            process.WaitForExit();
        }
        /*
        public static void Start()
        {
            Start(Names.AGENT_SERVICE_NAME);
        }

        public static void Stop()
        {
            var info = new ProcessStartInfo();
            info.CreateNoWindow = true;
            info.UseShellExecute = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.FileName = "cmd";
            info.Arguments = "cmd /c sc stop \"" + Names.AGENT_SERVICE_NAME + "\"";
            info.Verb = "runas";

            var process = new Process();
            process.StartInfo = info;

            process.Start();
            process.WaitForExit();
            ServiceController sc;
            sc = new ServiceController(Names.AGENT_SERVICE_NAME);
            sc.WaitForStatus(ServiceControllerStatus.Stopped);
        }

        public static void Restart()
        {
            AgentManagement.Stop();
            AgentManagement.Start();
        }
        */

        public static void Start(string serviceName)
        {
            var info = new ProcessStartInfo();
            //Bhavik Added Below line of Code to Start Agent Service 
            info.UseShellExecute = true;
            info.UseShellExecute = true;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.FileName = "cmd";
            info.Arguments = "cmd /c sc start \"" + serviceName + "\"";
            info.Verb = "runas";

            var process = new Process();
            process.StartInfo = info;

            process.Start();
            process.WaitForExit();
            ServiceController sc;
            sc = new ServiceController(serviceName);
            sc.WaitForStatus(ServiceControllerStatus.Running);
        }

        public static void Stop(string serviceName)
        {
            var info = new ProcessStartInfo();
            info.CreateNoWindow = true;
            info.UseShellExecute = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.FileName = "cmd";
            info.Arguments = "cmd /c sc stop \"" + serviceName + "\"";
            info.Verb = "runas";

            var process = new Process();
            process.StartInfo = info;

            process.Start();
            process.WaitForExit();
            ServiceController sc;
            sc = new ServiceController(serviceName);
            sc.WaitForStatus(ServiceControllerStatus.Stopped);
        }

        public static void Restart(string serviceName)
        {
            AgentManagement.Stop(serviceName);
            AgentManagement.Start(serviceName);
        }

    }
}
