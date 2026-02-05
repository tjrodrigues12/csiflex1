using System;
using System.ComponentModel;
using CSIFLEX.PartAnalyzer.Entities;
namespace CSIFLEX.PartAnalyzer.Views
{
    public class GeniusDataViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public GeniusDataViewModel(IProductionEntity productionOrder, ProductionPart part)
        {
            ProductionPart = part;
            Entity = productionOrder;
            var time = productionOrder.ProductionEndDate - productionOrder.ProductionStartDate;
            ReportedCycleTime =
                ((long)time.TotalSeconds).FromSecondsToHHMMSS();
            var productionTime = part.ProductionTasksEntity.ProductionEndDate - part.ProductionTasksEntity.ProductionStartDate;
            ReportedProductionTime = ((long)productionTime.TotalSeconds).FromSecondsToHHMMSS();
            ActualCycleTime = ProductionPart.MachinePartPerformance.TotalTimeInSeconds.FromSecondsToHHMMSS();
            ActualToReportedPercentage = string.Format("{0:N2}%", (ProductionPart.MachinePartPerformance.TotalTimeInSeconds / productionTime.TotalSeconds* 100));
            PartsMade = productionOrder.PlannedQuantity.ToString();
            ScrappedParts = productionOrder.RejectedQuantity.ToString();
            ScrappedPartsPercentage = string.Format("{0:N2}%", (productionOrder.RejectedQuantity / productionOrder.PlannedQuantity * 100));
            PartSetupTime = ProductionPart.MachinePartPerformance.TotalSetupInSeconds.FromSecondsToHHMMSS();
            PartOtherTime = ProductionPart.MachinePartPerformance.TotalOtherInSeconds.FromSecondsToHHMMSS();
            PartCycleOnTime = ProductionPart.MachinePartPerformance.TotalCycleOnInSeconds.FromSecondsToHHMMSS();
            PartCycleOffTime = ProductionPart.MachinePartPerformance.TotalCycleOffInSeconds.FromSecondsToHHMMSS();
            Title = $"{productionOrder.GetType().Name}: {productionOrder.Id}";
            MachineName = ProductionPart.MachinePartPerformance.MachineName;
            PartNumber = $"Part# {ProductionPart.MachinePartPerformance.PartNumber}";
            WorkId = $"ERP ID: {productionOrder.Id}";
            GroupProperty = productionOrder.Id;
        }

        public GeniusDataViewModel Instance => this;

        public string ReportedProductionTime { get; set; }
        public IProductionEntity Entity { get; set; }

        public ProductionPart ProductionPart { get; set; }

        public string GroupProperty { get; set; }
        public string Title { get; set; }

        public string MachineName { get; set; }

        public string ReportedCycleTime { get; set; }

        public string WorkId { get; set; }

        public string PartNumber { get; set; }

        public string ActualCycleTime { get; set; }

        public string PartsMade { get; set; }

        public string ActualToReportedPercentage { get; set; }

        public string ScrappedPartsPercentage { get; set; }

        public string ScrappedParts { get; set; }

        public string PartSetupTime { get; set; }

        public string PartOtherTime { get; set; }

        public string PartCycleOnTime { get; set; }

        public string PartCycleOffTime { get; set; }
        public bool GroupEquals(object other)
        {
            if (other is GeniusDataViewModel vm)
            {
                return vm.WorkId.Equals(WorkId);
            }
            return false;
        }
    }
}
