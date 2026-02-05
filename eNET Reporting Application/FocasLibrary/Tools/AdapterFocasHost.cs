// Copyright (c) 2018 CSIFLEX, All Rights Reserved.



using System.IO;

namespace FocasLibrary.Tools
{
    static class AdapterFocusHost
    {
        public static void Set(string path, string host)
        {
            if (File.Exists(path))
            {
                var ini = new IniFile(path);
                ini.Write("host", " " + host, "focus");
            }
        }

        public static string Get(string path)
        {
            if (File.Exists(path))
            {
                var ini = new IniFile(path);
                return ini.Read("host", "focus");
            }

            return null;
        }
    }
}
