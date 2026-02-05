using System;
using System.Collections;
using System.Collections.Generic;

namespace CSIFLEX.Reports.Server
{
    public class MachineCycleTime
    {
        public int MachineId { get; set; }

        public string MachineName { get; set; }

        public string MachineTable { get; set; }

        public string CycleStatus { get; set; }

        public decimal CycleTime { get; set; }

        public string StatusColor { get; set; }
    }
}
