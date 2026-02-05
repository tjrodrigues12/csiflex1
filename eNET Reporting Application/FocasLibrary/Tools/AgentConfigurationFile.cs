// Copyright (c) 2018 CSIFLEX, All Rights Reserved.


using FocasLibrary.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace FocasLibrary.Tools
{
    public static class AgentConfigurationFile
    {

        public static int GetPort()
        {
            int result = 5000;
            string path = Paths.AGENT_CONFIG;

            if (File.Exists(path))
            {
                using (var reader = new StreamReader(path))
                {
                    string cfg = reader.ReadToEnd();
                    string s = GetValue("Port", cfg);
                    if (!string.IsNullOrEmpty(s))
                    {
                        int port = -1;
                        if (int.TryParse(s, out port)) return port;
                    }
                }
            }

            return result;
        }
        //public static void Set(string path, int port)
        //{
        //    if (File.Exists(path))
        //    {
        //        var ini = new IniFile(path);
        //        ini.Write("Port", " " + port,null);
        //    }
        //}
        //Add new function that Set Port Value to User Given Values
        //public static int SetPort(string result)
        //{
        //    int res = Convert.ToInt32(result);
        //    return res;
        //}
        public static string GetServiceName()
        {
            string result = null;

            string path = Paths.AGENT_CONFIG;

            if (File.Exists(path))
            {
                using (var reader = new StreamReader(path))
                {

                    string cfg = reader.ReadToEnd();
                    return GetValue("ServiceName", cfg);
                }
            }

            return result;
        }

        private static string GetValue(string key, string cfg)
        {
            /* :::::::::::::::::: Original Code :::::: 
             * // Find start of 'Key'
            int i = cfg.IndexOf(key);
            MessageBox.Show("Index Value of  ServiceName : " + Convert.ToString(i));
            if (i >= 0)
            {
                i = cfg.IndexOf('=', i);
                int z = cfg.IndexOf(Environment.NewLine, i + 1);

                // Get only 'Value' text
                return cfg.Substring(i + 1, z - i).Trim();
            }

            return null;*/

            // Find start of 'Key'
            int i = cfg.IndexOf(key);
            //MessageBox.Show("Index Value of  ServiceName : " + Convert.ToString(i));
            if (i >= 0)
            {
                i = cfg.IndexOf('=', i);
                //MessageBox.Show("Index of = sign : " + Convert.ToString(i));
                //int z = cfg.IndexOf(Environment.NewLine, i + 1);
                int z = cfg.IndexOf('\n', i + 1);
                //MessageBox.Show("Index of New Line Character : " + Convert.ToString(z));

                // Get only 'Value' text
                //MessageBox.Show("Value is : " + cfg.Substring(i + 1, z - i).Trim());
                return cfg.Substring(i + 1, z - i).Trim();
            }

            return null;
        }

        public static List<AgentAdapterInfo> GetAdapters()
        {
            return GetAdapters( Paths.AGENT_CONFIG);            
        }

        public static List<AgentAdapterInfo> GetAdapters(string agentConfigFile)
        {
            var result = new List<AgentAdapterInfo>();

            if (File.Exists(agentConfigFile))
            {
                using (var reader = new StreamReader(agentConfigFile))
                {
                    string cfg = "";

                    // Remove All Whitespace and linebreaks
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Trim().Length > 0) cfg += line.Trim();
                    }

                    // Find start of 'Adapters' section
                    int i = cfg.IndexOf("Adapters");
                    i = cfg.IndexOf('{', i);
                    int z = cfg.IndexOf("}", i + 1);

                    // Check if empty
                    if (z > i + 1)
                    {
                        z = cfg.IndexOf("}}", i + 1) + 2;

                        // Get only 'Adapters' section text
                        cfg = cfg.Substring(i, z - i);

                        i = cfg.IndexOf('{');

                        int x = 0;
                        while (x < cfg.Length - 1)
                        {
                            int j = cfg.IndexOf('{', i + 1);

                            string deviceName = cfg.Substring(i + 1, j - i - 1).Trim();

                            i = cfg.IndexOf("Port", j);
                            i = cfg.IndexOf('=', i);
                            j = cfg.IndexOf('}', i + 1);

                            string p = cfg.Substring(i + 1, j - i - 1).Trim();
                            int port = -1;
                            int.TryParse(p, out port);

                            if (!string.IsNullOrEmpty(deviceName) && port >= 0)
                            {
                                var info = new AgentAdapterInfo();
                                info.DeviceName = deviceName;
                                info.Port = port;
                                result.Add(info);
                            }

                            x = j + 1;
                            i = j;
                        }
                    }
                }
            }

            return result;
        }

        public static List<AgentAdapterInfo> GetAdaptersForAllAgents()
        {
            var result = new List<AgentAdapterInfo>();

            string path = Paths.AGENT_CONFIG;

            if (File.Exists(path))
            {
                using (var reader = new StreamReader(path))
                {
                    string cfg = "";

                    // Remove All Whitespace and linebreaks
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Trim().Length > 0) cfg += line.Trim();
                    }

                    // Find start of 'Adapters' section
                    int i = cfg.IndexOf("Adapters");
                    i = cfg.IndexOf('{', i);
                    int z = cfg.IndexOf("}", i + 1);

                    // Check if empty
                    if (z > i + 1)
                    {
                        z = cfg.IndexOf("}}", i + 1) + 2;

                        // Get only 'Adapters' section text
                        cfg = cfg.Substring(i, z - i);

                        i = cfg.IndexOf('{');

                        int x = 0;
                        while (x < cfg.Length - 1)
                        {
                            int j = cfg.IndexOf('{', i + 1);

                            string deviceName = cfg.Substring(i + 1, j - i - 1).Trim();

                            i = cfg.IndexOf("Port", j);
                            i = cfg.IndexOf('=', i);
                            j = cfg.IndexOf('}', i + 1);

                            string p = cfg.Substring(i + 1, j - i - 1).Trim();
                            int port = -1;
                            int.TryParse(p, out port);

                            if (!string.IsNullOrEmpty(deviceName) && port >= 0)
                            {
                                var info = new AgentAdapterInfo();
                                info.DeviceName = deviceName;
                                info.Port = port;
                                result.Add(info);
                            }

                            x = j + 1;
                            i = j;
                        }
                    }
                }
            }

            return result;
        }

        public static void WriteAdapters(List<AgentAdapterInfo> infos)
        {
            string path = Paths.AGENT_CONFIG;

            if (File.Exists(path))
            {
                string cfg = "";

                using (var reader = new StreamReader(path))
                {
                    cfg = reader.ReadToEnd();
                }

                // Find start of 'Adapters' section
                int x = cfg.IndexOf("Adapters");
                int i = cfg.IndexOf('{', x);
                int j = i + 1;

                char c1 = cfg[i];
                char c2 = cfg[j];

                // Look for two successive '}'
                while (j < cfg.Length && (!(c1 == '}' && c2 == '}') && !(c1 == '{' && c2 == '}')))
                {
                    var tmp = cfg[j];

                    if (!char.IsWhiteSpace(tmp))
                    {
                        c1 = c2;
                        c2 = tmp;
                    }

                    j++;
                }

                // Remove entire Adapters Section
                cfg = cfg.Remove(x, j - x);
                cfg = cfg.TrimEnd();

                string n = Environment.NewLine;
                string adapters = "Adapters {" + n + n;

                foreach (var info in infos)
                {
                    adapters += info.DeviceName.Replace(" ", "-") + " {" + n;
                    adapters += "Port = " + info.Port + n;
                    adapters += "}" + n + n;
                }

                adapters += "}" + n + n;

                cfg += n + n + adapters;

                File.WriteAllText(path, cfg);
            }
        }

        public static void WriteAdaptersForAgent(string AgentConfigFile ,List<AgentAdapterInfo> infos)
        {
            if (File.Exists(AgentConfigFile))
            {
                string cfg = "";

                using (var reader = new StreamReader(AgentConfigFile))
                {
                    cfg = reader.ReadToEnd();
                }

                // Find start of 'Adapters' section
                int x = cfg.IndexOf("Adapters");
                int i = cfg.IndexOf('{', x);
                int j = i + 1;

                char c1 = cfg[i];
                char c2 = cfg[j];

                // Look for two successive '}'
                while (j < cfg.Length && (!(c1 == '}' && c2 == '}') && !(c1 == '{' && c2 == '}')))
                {
                    var tmp = cfg[j];

                    if (!char.IsWhiteSpace(tmp))
                    {
                        c1 = c2;
                        c2 = tmp;
                    }

                    j++;
                }

                // Remove entire Adapters Section
                cfg = cfg.Remove(x, j - x);
                cfg = cfg.TrimEnd();

                string n = Environment.NewLine;
                string adapters = "Adapters {" + n + n;

                foreach (var info in infos)
                {
                    adapters += info.DeviceName.Replace(" ","-") + " {" + n;
                    adapters += "Port = " + info.Port + n;
                    adapters += "}" + n + n;
                }

                adapters += "}" + n + n;

                cfg += n + n + adapters;

                File.WriteAllText(AgentConfigFile, cfg);
            }
        }

    }
}
