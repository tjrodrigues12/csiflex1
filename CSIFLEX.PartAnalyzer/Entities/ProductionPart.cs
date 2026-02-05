using System;
using System.Collections.Generic;
using System.Text;

namespace CSIFLEX.PartAnalyzer.Entities
{
    public class ProductionPart
    {
        public ProductionTasksEntity ProductionTasksEntity { get; set; }
        public MachinePartPerformance MachinePartPerformance { get; set; }
    }
}
