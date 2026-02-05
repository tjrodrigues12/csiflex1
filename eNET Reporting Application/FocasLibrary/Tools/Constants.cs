// Copyright (c) 2018 CSIFLEX, All Rights Reserved.

// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.IO;

namespace FocasLibrary.Tools
{
    public static class Names
    {
        public const string AGENT_SERVICE_NAME = "MTCFocasAgent Service";
    }

    public static class FolderNames
    {
        public const string AGENT = "Agent";
        public const string ADAPTERS = "Adapters";
        public const string ADAPTER_TEMPLATES = "Adapter Templates";
        public const string AGENT_TEMPLATE = "Agent Template";
        public const string DEVICE_TEMPLATES = "Device Templates";
    }

    public static class Files
    {
        public const string AGENT_EXE = "agent.exe";
        public const string ADAPTER_INI = "adapter.ini";
        public const string AGENT_CONFIG = "agent.cfg";
        public const string ADAPTER_EXE = "adapter.exe";
        public const string AGENT_DEVICES = "devices.xml";
        public const string DEVICE_TEMPLATE = "device.xml";
    }

    public static class Paths
    {
        /*Old Version Of Code ::::
        public static string AGENT_BIN = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName, FolderNames.AGENT, "bin");
        public static string AGENT_EXE = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName, FolderNames.AGENT, "bin", Files.AGENT_EXE);
        public static string AGENT_DEVICES = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName, FolderNames.AGENT, "bin", Files.AGENT_DEVICES);
        public static string AGENT_CONFIG = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName, FolderNames.AGENT, "bin", Files.AGENT_CONFIG);

        public static string ADAPTERS = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName, FolderNames.ADAPTERS);
        public static string ADAPTER_TEMPLATES = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName, FolderNames.ADAPTER_TEMPLATES);
        public static string DEVICE_TEMPLATE = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Files.DEVICE_TEMPLATE);
        */
        public static string ApplicationPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\CSI Flex Server";
        //public static string ApplicationPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\FocasApplication";
        public static string AGENT_BIN = Path.Combine(ApplicationPath, FolderNames.AGENT, "bin");
        public static string AGENT_EXE = Path.Combine(ApplicationPath, FolderNames.AGENT, "bin", Files.AGENT_EXE);
        public static string AGENT_DEVICES = Path.Combine(ApplicationPath, FolderNames.AGENT, "bin", Files.AGENT_DEVICES);
        public static string AGENT_CONFIG = Path.Combine(ApplicationPath, FolderNames.AGENT, "bin", Files.AGENT_CONFIG);

        public static string ADAPTERS = Path.Combine(ApplicationPath, FolderNames.ADAPTERS);
        public static string ADAPTER_TEMPLATES = Path.Combine(ApplicationPath, FolderNames.ADAPTER_TEMPLATES);
        public static string DEVICE_TEMPLATE = Path.Combine(ApplicationPath, FolderNames.AGENT, FolderNames.DEVICE_TEMPLATES, Files.DEVICE_TEMPLATE);

        public static string AGENT_TEMPLATE = Path.Combine(ApplicationPath, FolderNames.AGENT, FolderNames.AGENT_TEMPLATE);
        public static string AGENT_FOLDER_FORMAT = Path.Combine(ApplicationPath, FolderNames.AGENT, "bin","{0}");
    }
}
