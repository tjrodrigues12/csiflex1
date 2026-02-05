using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFLEX.Migration
{
    public class CsiMachineConnector
    {
        public string MachineName { get; set; }
        public string MachineIp { get; set; }
        public string Manufacturer { get; set; }
        public string ConnectorType { get; set; }
        public string ControllerType { get; set; }
        public string EnetMachineName { get; set; }
        public int FocasPort { get; set; }
        public int AgentPort { get; set; }
        public string AgentServiceName { get; set; }
        public int AdapterPort { get; set; }
        public string AdapterServiceName { get; set; }
    }
}
