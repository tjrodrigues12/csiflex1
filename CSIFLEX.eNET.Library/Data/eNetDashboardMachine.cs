using System;

namespace CSIFLEX.eNET.Library.Data
{
    public class eNetDashboardMachine
    {
        public string MachineName { get; set; }

        public DateTime LastWriteTime { get; set; }

        public long LastFileChange { get; set; }

        public int Shift { get; set; }

        public string Status { get; set; }

        public DateTime StatusDateTime { get; set; }

        public string PartNumber { get; set; }

        public string OperatorCod { get; set; }

        public int CycloCount { get; set; }

        public long LastCycle { get; set; }

        public double CurrentCycle { get; set; }

        public long StatusTime { get; set; }

        public double ElapsedTime { get; set; }

        public int FeedOverride { get; set; }

        public int SpindleOverride { get; set; }

        public string Comment { get; set; }

        public string TempRecord { get; set; }
    }
}
