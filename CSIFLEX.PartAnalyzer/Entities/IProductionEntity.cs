using System;
using System.Collections.Generic;
using System.Text;

namespace CSIFLEX.PartAnalyzer.Entities
{
    public interface IProductionEntity
    {
        public string Id { get; }
        public string PartNumber { get; }
        public DateTime ProductionStartDate { get; }
        public DateTime ProductionEndDate { get; }

        public float ProducedQuantity { get; }

        public float RejectedQuantity { get; }

        public float PlannedQuantity { get; }

        public string Description { get; }

        public string IdName { get; }
    }
}
