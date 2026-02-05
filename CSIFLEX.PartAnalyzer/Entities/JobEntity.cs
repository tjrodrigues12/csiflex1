using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CSIFLEX.PartAnalyzer.Entities
{
    [GeniusEntityMainId("Job")]
    public class JobEntity : IProductionEntity
    { 
        public string Id { get; set; }
        public string Job { get; set; } 
        public string Product { get; set; }
        public string ProductRevision { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public float PlannedQuantity { get; set; }
        public float ProducedQuantity { get; set; }
        public float RejectedQuantity { get; set; } 
        public DateTime ClosingDate { get; set; }   
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdate { get; set; } 
        public DateTime IsReservedDate { get; set; }
        public DateTime ProductionEndDate { get; set; } 
        public string ProductLink { get; set; } 
        public string FullSearch { get; set; }
        public DateTime ProductionStartDate { get; set; } 
        public string ProductSpecification1 { get; set; }
        public string ProductSpecification2 { get; set; }
        public string ProductSpecification3 { get; set; }
        public string ProductSpecification4 { get; set; }
        public string ProductSpecification5 { get; set; }
        public string ProductSpecification6 { get; set; }
        public string ProductSpecification7 { get; set; }
        public string ProductSpecification8 { get; set; }
        public string Productpecification9 { get; set; }
        public string ProductSpecification10 { get; set; }
        public string ProductSpecification11 { get; set; }
        public string ProductSpecification12 { get; set; }
        public string ProductSpecification13 { get; set; }
        public string ProductSpecification14 { get; set; }
        public string ProductSpecification15 { get; set; }
        public string ProductSpecification16 { get; set; }
        public bool ProgressPercentProcessed { get; set; }

        [JsonIgnore]
        public string PartNumber => ProductLink;
        [JsonIgnore]
        public string Description => $"{ProductSpecification1}\r\n{ProductSpecification2}\r\n{ProductSpecification3}\r\n{ProductSpecification4}"
            + $"\r\n{ProductSpecification5}\r\n{ProductSpecification6}\r\n{ProductSpecification7}\r\n{ProductSpecification8}"
            + $"\r\n{Productpecification9}\r\n{ProductSpecification10}\r\n{ProductSpecification11}\r\n{ProductSpecification12}"
            + $"\r\n{ProductSpecification13}\r\n{ProductSpecification14}\r\n{ProductSpecification15}\r\n{ProductSpecification16}";
        [JsonIgnore]
          string IProductionEntity.Id => Job;

        [JsonIgnore]
        public string IdName => nameof(Job);
    }

}
