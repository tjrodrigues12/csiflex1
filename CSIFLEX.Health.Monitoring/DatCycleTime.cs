using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFLEX.Health.Monitoring
{
    public class CycleStatus
    {
        public int CustomerId { get; set; }

        public int MachineId { get; set; }

        public string MachineName { get; set; }

        public string Status { get; set; }

        public DateTime SummaryDate { get; set; }

        public long Cycletime { get; set; }
    }
}
