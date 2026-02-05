using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFlex_ServiceLibrary.Model
{
    public class EhubConfModel
    {
        public int Id { get; set; }
        public string MonitoringId { get; set; }
        public string MachineName { get; set; }
        public int MonSetupId { get; set; }
        public int Con_type { get; set; }
        public DateTime LogDate { get; set; }
    }
}
