using CSIFLEX.Server.Library.DataModel;
using System;
using System.Collections.Generic;

namespace CSIFLEX.Reports.Server.Data
{
    public class ReportParameters
    {
        public string ReportName { get; set; }

        public string ReportType { get; set; }

        public string ReportTitle { get; set; }

        public string ReportPeriod { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int firstWeekDay { get; set; }

        public int lastWeekDay { get; set; }

        public string Shift { get; set; }

        public string RdlcPath { get; set; }

        public string OutputPath { get; set; }

        public bool ShortFileName { get; set; }

        public string Scale { get; set; }

        public bool Production { get; set; }

        public bool Setup { get; set; }

        public string Sorting { get; set; }

        public int EventMinMinutes { get; set; }

        public bool OnlySummary { get; set; }

        public int ShowConInSetup { get; set; }

        public List<string> MachinesName { get; set; }

        public List<MachineDBTable> Machines { get; set; }


        public ReportParameters()
        {
            Machines = new List<MachineDBTable>();
        }
    }
}
