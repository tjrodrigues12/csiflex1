using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFLEX.PartAnalyzer.Entities
{
    public class MachinePartProduction
    {
        public string Machine { get; set; }
        public string PartNumber { get; set; }

        public override bool Equals(object obj)
        {
            if(obj is MachinePartProduction second)
            {
                return Machine.ToLowerInvariant() == second.Machine.ToLowerInvariant()
                    && PartNumber.ToLowerInvariant() == second.PartNumber.ToLowerInvariant();
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = 947494083;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Machine);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PartNumber);
            return hashCode;
        }
    }
}
