// Copyright (c) 2018 CSIFLEX, All Rights Reserved.


using System.IO;
using FocasAdapterAgentLibrary.Tools;

namespace FocasAdapterAgentLibrary.Components
{
    public class AdapterInfo
    {
        public string Path { get; set; }

        public string DeviceName { get; set; }
        public string ServiceName { get; set; }
        public int Port { get; set; }

        public string FocusHost { get; set; }

        public static AdapterInfo Read(string path)
        {
            var info = new AdapterInfo();
            info.Path = path;
            info.DeviceName = Directory.GetParent(path).Name;
            info.ServiceName = AdapterSeviceName.Get(path);
            info.Port = AdapterPort.Get(path);
            info.FocusHost = AdapterFocusHost.Get(path);
            return info;
        }
    }
}
