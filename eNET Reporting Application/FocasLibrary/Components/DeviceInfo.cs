// Copyright (c) 2018 CSIFLEX, All Rights Reserved.



namespace FocasLibrary.Components
{
    public class DeviceInfo
    {
        /// <summary>
        /// Path to Agent files
        /// </summary>
        public string AgentFolderPath { get; set; }

        public int AgentPort { get; set; }


        /// <summary>
        /// Type of Adapter (0id, 30i, etc.);
        /// </summary>
        public string Adapter { get; set; }

        /// <summary>
        /// Path to Adapter files
        /// </summary>
        public string AdapterPath { get; set; }

        /// <summary>
        /// IP address of Fanuc Focas
        /// </summary>
        public string AdapterFocasIp { get; set; }

        public int AdapterPort { get; set; }
        public int FocasMachinePort { get; set; } //Set this value in Adapter.ini file
        public int MachineId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceId { get; set; }
        public string Uuid { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Serial { get; set; }
        public string Description { get; set; }
        public string AdapterServiceName { get; set; }
        public string AgentServiceName { get; set; }

    }
}
