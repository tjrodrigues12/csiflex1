// Copyright (c) 2018 CSIFLEX, All Rights Reserved.

using CSIFLEX.Utilities;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;

namespace FocasLibrary.Tools
{
    public static class AdapterManagement
    {

        public static void Install(string exePath)
        {
            string dirPath = Path.GetDirectoryName(exePath);

            string cmd = "\"" + exePath + "\" install";

            var info = new ProcessStartInfo();
            info.FileName = "cmd";
            info.Arguments = "cmd /c cd \"" + dirPath + "\" & " + cmd;
            info.UseShellExecute = false;
            info.CreateNoWindow = true;
            info.WorkingDirectory = dirPath;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.Verb = "runas";

            try
            {
                var process = new Process();
                process.StartInfo = info;
                process.Start();
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        public static void Install(string exePath, string servicename)
        {

            string dirPath = Path.GetDirectoryName(exePath);

            string cmd = $"sc create { servicename } binPath= \"{ exePath }\" start= auto"; // For Auto write 'auto'

            var info = new ProcessStartInfo();
            info.FileName = "cmd";
            info.Arguments = $"cmd /c { cmd }";
            info.UseShellExecute = false;
            info.CreateNoWindow = true;
            info.WorkingDirectory = dirPath;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.Verb = "runas";

            try
            {
                var process = new Process();
                process.StartInfo = info;
                process.Start();
                process.WaitForExit();

                RegistryAdapter(servicename);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        private static void RegistryAdapter(string serviceName)
        {
            try
            {
            RegistryKey baseKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\\WOW6432Node");

            RegistryKey mtcKey = baseKey.CreateSubKey("MTConnect");

            RegistryKey srvKey = mtcKey.CreateSubKey(serviceName);

            srvKey.SetValue("Arguments", new string[] { }, RegistryValueKind.MultiString);

                srvKey.Close();
                mtcKey.Close();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }

        }


        public static void InstallMysql(string exePath, string servicename)
        {
            string dirPath = Path.GetDirectoryName(exePath);

            string IniFilePath = Path.Combine(dirPath.Replace("bin", ""), "my.ini"); //Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\CSI Flex Server\\mysql\\mysql-5.7.21-win32\\my.ini";

            // Old Logic string cmd = "\"" + exePath + "\" install";

            // New Logic by Bhavik :::
            //string cmd = "sc create " + (char)34 + servicename + (char)34 + " binPath= " + (char)34 + exePath + (char)34 + " start= delayed-auto"; // For Auto write 'auto'
            //Dim cmd As String = "sc create " & Chr(34) & ServiceName & Chr(34) & " binPath= " & Chr(34) & exePath & Chr(34) & ""
            
            //string cmd = $"\"{ dirPath }\\mysqld.exe\" --install { servicename } --defaults-file=\"{ IniFilePath }\"";
            string cmd = $"mysqld.exe --install { servicename } --defaults-file=\"{ IniFilePath }\"";

            var info = new ProcessStartInfo();
            info.FileName = "cmd";
            //info.Arguments = "cmd /c cd \"" + dirPath + "\\" + cmd;
            info.Arguments = "cmd /c " + cmd;
            info.UseShellExecute = false;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.Verb = "runas";

            try
            {
                //var process = new Process();
                //process.StartInfo = info;
                //process.Start();
                //process.WaitForExit();


                ProcessStartInfo procInfo = new ProcessStartInfo();
                procInfo.FileName = "cmd";
                procInfo.Arguments = "cmd /c " + cmd;
                procInfo.WorkingDirectory = dirPath;
                procInfo.RedirectStandardInput = false;
                procInfo.RedirectStandardOutput = true;
                procInfo.UseShellExecute = false;

                Process proc = Process.Start(procInfo);

                string res = proc.StandardOutput.ReadToEnd();

                using (StreamWriter file = new StreamWriter("ProcessLog.log", true))
                {
                    file.WriteLine(res);
                }
                proc.WaitForExit();



            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        public static void StartMysql(string serviceName)
        {
            string cmd = "net start " + serviceName;

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
            ServiceController sc;
            sc = new ServiceController(serviceName);
            sc.WaitForStatus(ServiceControllerStatus.Running);
        }
        public static void StopMysql(string serviceName)
        {
            string cmd = "net stop " + serviceName;

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
            ServiceController sc;
            sc = new ServiceController(serviceName);
            sc.WaitForStatus(ServiceControllerStatus.Stopped);
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

            try
            {
                var process = new Process();
                process.StartInfo = info;
                process.Start();
                process.WaitForExit();
                process.Close();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }


        public static void Start(string serviceName)
        {
            string cmd = "sc start " + serviceName;

            var info = new ProcessStartInfo();
            info.FileName = "cmd";
            info.Arguments = "cmd /c" + cmd;
            info.UseShellExecute = true;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.Verb = "runas";

            var process = new Process();
            process.StartInfo = info;

            try
            {
                process.Start();
                process.WaitForExit();
                ServiceController sc;
                sc = new ServiceController(serviceName);
                sc.WaitForStatus(ServiceControllerStatus.Running);
                sc.Close();
                process.Close();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        public static void Stop(string serviceName)
        {
            string cmd = "sc stop " + serviceName;

            var info = new ProcessStartInfo();
            info.FileName = "cmd";
            info.Arguments = "cmd /c" + cmd;
            info.UseShellExecute = true;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.Verb = "runas";

            try
            {
                var process = new Process();
                process.StartInfo = info;

                process.Start();
                process.WaitForExit();
                ServiceController sc;
                sc = new ServiceController(serviceName);
                sc.WaitForStatus(ServiceControllerStatus.Stopped);
                sc.Close();
                process.Close();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }
        public static void Restart(string servicename)
        {
            AdapterManagement.Stop(servicename);
            AdapterManagement.Start(servicename);
        }
    }
}
