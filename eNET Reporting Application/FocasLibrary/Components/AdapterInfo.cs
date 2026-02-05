// Copyright (c) 2018 CSIFLEX, All Rights Reserved.

// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System.IO;
using FocasLibrary.Tools;

namespace FocasLibrary.Components
{
    public class AdapterInfo
    {
        public string Path { get; set; }

        public string DeviceName { get; set; }
        public string ServiceName { get; set; }
        public int Port { get; set; }
        public int MachinePort { get; set; }
        public string FocusHost { get; set; }

        public static AdapterInfo Read(string path)
        {
            var info = new AdapterInfo();
            info.Path = path;
            info.DeviceName = Directory.GetParent(path).Name;
            info.ServiceName = AdapterSeviceName.Get(path);
            info.Port = AdapterPort.Get(path);
            info.MachinePort  = FocasMachinePort.Get(path);
            info.FocusHost = AdapterFocusHost.Get(path);
            return info;
        }
    }
}
