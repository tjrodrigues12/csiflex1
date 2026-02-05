using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFlex_ServiceLibrary.Model
{
    public class MonSetupModel
    {
        public int Id { get; set; }
        public string MonitoringId { get; set; }
        public string MachineName { get; set; }
        public int MonitoringOnTrack { get; set; }
        public string MachineTempFilename { get; set; }
        public int MonStateOn { get; set; }
        public int PartCount { get; set; }

        public int PartMultiplierMN { get; set; }
        public int CyclIdentifierIC { get; set; }

        public int MinIdealCI { get; set; }

        public int MaxPercCa { get; set; }
        public string CurrentStatus { get; set; }
        public int LastCycleTime { get; set; }

        public int ElapsedTime { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string MachinePartNo { get; set; }
        public string DepartmentName { get; set; }
        public int ResetCounterNM { get; set; }
    }
}
