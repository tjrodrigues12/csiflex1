// Copyright (c) 2018 CSIFLEX, All Rights Reserved.


using System.IO;

namespace FocasAdapterAgentLibrary.Tools
{
    static class AdapterPort
    {
        public static void Set(string path, int port)
        {
            if (File.Exists(path))
            {
                var ini = new IniFile(path);
                ini.Write("port", " " + port, "adapter");
            }
        }

        public static int Get(string path)
        {
            if (File.Exists(path))
            {
                var ini = new IniFile(path);
                string s = ini.Read("port", "adapter");
                if (s != null)
                {
                    int port = -1;
                    int.TryParse(s, out port);
                    return port;
                }
            }

            return -1;
        }
    }
}
