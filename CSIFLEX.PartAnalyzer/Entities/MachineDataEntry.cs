using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFLEX.PartAnalyzer.Entities
{
    public class MachineDataEntry
    {
        public int month_ { get; set; }
        public int day_ { get; set; }
        public int year_ { get; set; }
        public string time_ { get; set; }
        public string Date_ { get; set; }
        public string status { get; set; }
        public int shift { get; set; }
        public int cycletime { get; set; }
        public string Partnumber { get; set; }
        public string MachineName { get; set; }

    }
}
