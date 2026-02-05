using System;
namespace CSIFLEX.PartAnalyzer.Entities
{
    [GeniusEntityMainId("WorkOrder")]
    public class WorkOrderEntity: IProductionEntity
    {
        public WorkOrderEntity()
        {

        }

        public int Id { get; set; }
        public string Job { get; set; }
        public string WorkOrder { get; set; }
        public string ParentWorkOrder { get; set; }
        public string Product { get; set; }
        public string Item { get; set; }
        public string Description1 { get; set; }
        public object Description2 { get; set; }
        public float PlannedQuantity { get; set; }
        public float ProducedQuantity { get; set; }
        public float RejectedQuantity { get; set; }
        public float UsedQuantity { get; set; }
        public DateTime ProducedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public string CustomerName { get; set; }
        public int QtyOfPrintedCopy { get; set; }
        public string WorkOrderStatus { get; set; }
        public string Link { get; set; }
        public string JobLink { get; set; }
        public string ItemLink { get; set; }
        public string ProductLink { get; set; }
        public string ItemSpecification1 { get; set; }
        public string ItemSpecification2 { get; set; }
        public string ItemSpecification3 { get; set; }
        public string ItemSpecification4 { get; set; }
        public string ItemSpecification5 { get; set; }
        public string ItemSpecification6 { get; set; }
        public string ItemSpecification7 { get; set; }
        public string ItemSpecification8 { get; set; }
        public string ItemSpecification9 { get; set; }
        public string ItemSpecification10 { get; set; }
        public string ItemSpecification11 { get; set; }
        public string ItemSpecification12 { get; set; }
        public string ItemSpecification13 { get; set; }
        public string ItemSpecification14 { get; set; }
        public string ItemSpecification15 { get; set; }
        public string ItemSpecification16 { get; set; }

        public string PartNumber => ProductLink;

        public DateTime ProductionStartDate => StartDate;

        public DateTime ProductionEndDate => ProducedDate;

        public string Description => $"{ItemSpecification1}\r\n{ItemSpecification2}\r\n{ItemSpecification3}\r\n{ItemSpecification4}"
            + $"\r\n{ItemSpecification5}\r\n{ItemSpecification6}\r\n{ItemSpecification7}\r\n{ItemSpecification8}"
            + $"\r\n{ItemSpecification9}\r\n{ItemSpecification10}\r\n{ItemSpecification11}\r\n{ItemSpecification12}"
            + $"\r\n{ItemSpecification13}\r\n{ItemSpecification14}\r\n{ItemSpecification15}\r\n{ItemSpecification16}";

        public string IdName =>nameof(WorkOrder);

        string IProductionEntity.Id => WorkOrder;
    }



}
