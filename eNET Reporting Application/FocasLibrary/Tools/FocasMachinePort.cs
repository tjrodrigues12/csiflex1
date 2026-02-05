using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocasLibrary.Tools
{
    static class FocasMachinePort
    {
        public static void Set(string path, int port)
        {
            if (File.Exists(path))
            {
                var ini = new IniFile(path);
                ini.Write("port", " " + port, "focus");
            }
        }

        public static int Get(string path)
        {
            if (File.Exists(path))
            {
                var ini = new IniFile(path);
                string s = ini.Read("port", "focus");
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
