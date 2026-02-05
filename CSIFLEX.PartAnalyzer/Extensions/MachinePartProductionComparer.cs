using CSIFLEX.PartAnalyzer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFLEX.PartAnalyzer
{
    public class MachinePartProductionComparer : IEqualityComparer<MachinePartProduction>
    {
        public bool Equals(MachinePartProduction x, MachinePartProduction y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(MachinePartProduction obj)
        {
            return obj.GetHashCode();
        }
    }
}
