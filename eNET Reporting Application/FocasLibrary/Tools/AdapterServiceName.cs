// Copyright (c) 2018 CSIFLEX, All Rights Reserved.


using System.IO;

namespace FocasLibrary.Tools
{
    static class AdapterSeviceName
    {
        public static void Set(string path, string serviceName)
        {
            if (File.Exists(path))
            {
                var ini = new IniFile(path);
                ini.Write("service", " " + serviceName, "adapter");
            }
        }

        public static string Get(string path)
        {
            if (File.Exists(path))
            {
                var ini = new IniFile(path);
                return ini.Read("service", "adapter");
            }

            return null;
        }
    }
}
