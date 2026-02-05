using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CSIFLEX.PartAnalyzer.Entities
{
    public class ProductionTasksEntity : IProductionEntity
    {
        public string Id { get; set; }
        public string JobCode { get; set; }
        public string WorkOrderCode { get; set; }
        public string OperationCode { get; set; }
        public string ItemCode { get; set; }
        public float SetupTime { get; set; }
        public float OperationTime { get; set; }
        public float PlannedQuantity { get; set; }
        public DateTime TaskStartDate { get; set; }
        public DateTime TaskEndDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public float CumulativeHours { get; set; }
        public float ProducedQuantity { get; set; }
        public string MachineCode { get; set; }
        public bool IsCompleted { get; set; }   
        public string Predecessors { get; set; }
        public float ResourceQuantity { get; set; }
        public float PercentCompleted { get; set; } 
        public DateTime ConstraintDate { get; set; }
        public string TaskType { get; set; }
        public DateTime Deadline { get; set; }
        public float SetupTimeOriginal { get; set; }
        public float OperationTimeOriginal { get; set; }
        public float DelayOriginal { get; set; }
        public object RoutingId { get; set; }
        public string ToolingCode { get; set; }
        public object InspectionCode { get; set; }
        public string OperationDescription1 { get; set; }
        public string OperationDescription2 { get; set; }
        public string OperationDescription3 { get; set; }
        public int Link { get; set; }
        public float NumberOfUnitByHour { get; set; }
        public string Formula { get; set; }
        public float CycleTime { get; set; }
        public object SequenceNumber { get; set; }
        public object SchedulingCalculationCompleted { get; set; }
        public object SchedulingUserForced { get; set; }
        public bool IsCompleteSetup { get; set; } 
        public string FullSearch { get; set; }
        public float RejectedQuantity { get; set; }
        public float CumulativeHoursEmp { get; set; }
        public float CumulativeHoursMach { get; set; }
        public float CumulativeSetupEmp { get; set; }
        public float CumulativeSetupMach { get; set; }
        public object OperationEntity { get; set; }
        public object ProductionLot { get; set; }
        public object Note { get; set; }

        public string PartNumber => ItemCode;

        public DateTime ProductionStartDate => TaskStartDate;

        public DateTime ProductionEndDate => TaskEndDate;

        public string Description => $"\r\n{OperationDescription1}\r\n{OperationDescription2}\r\n{OperationDescription3}";

        public string IdName => nameof(Id);
    }

    

}
