// Copyright (c) 2018 CSIFLEX, All Rights Reserved.


using System.Diagnostics;
using System.IO;

namespace FanucAdapterAgentConfiguration.Tools
{
    static class AdapterManagement
    {

        public static void Install(string exePath)
        {
            string dirPath = Path.GetDirectoryName(exePath);

            string cmd = "\"" + exePath + "\" install";

            var info = new ProcessStartInfo();
            info.FileName = "cmd";
            info.Arguments = "cmd /c cd \"" + dirPath + "\" & " + cmd;
            info.UseShellExecute = true;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.Verb = "runas";

            var process = new Process();
            process.StartInfo = info;

            process.Start();
            process.WaitForExit();
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

            process.Start();
            process.WaitForExit();
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

            var process = new Process();
            process.StartInfo = info;

            process.Start();
            process.WaitForExit();
        }

    }
}
