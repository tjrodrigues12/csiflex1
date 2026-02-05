// Copyright (c) 2018 CSIFLEX, All Rights Reserved.



using System.Diagnostics;
using System.IO;

namespace FocasAdapterAgentLibrary.Tools
{
    public static class AgentManagement
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
        public static void Start()
        {
            var info = new ProcessStartInfo();
            //Bhavik Added Below line of Code to Start Agent Service 
            info.UseShellExecute = true;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.FileName = "cmd";
            info.Arguments = "cmd /c sc start \"" + Names.AGENT_SERVICE_NAME + "\"";
            info.Verb = "runas";

            var process = new Process();
            process.StartInfo = info;

            process.Start();
            process.WaitForExit();
        }

        public static void Stop()
        {
            var info = new ProcessStartInfo();
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.FileName = "cmd";
            info.Arguments = "cmd /c sc stop \"" + Names.AGENT_SERVICE_NAME + "\"";
            info.Verb = "runas";

            var process = new Process();
            process.StartInfo = info;

            process.Start();
            process.WaitForExit();
        }

        public static void Restart()
        {
            AgentManagement.Stop();
            AgentManagement.Start();
        }

    }
}
