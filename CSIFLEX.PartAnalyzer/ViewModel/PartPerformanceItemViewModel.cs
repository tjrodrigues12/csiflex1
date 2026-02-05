using CSIFLEX.Library;
using CSIFLEX.PartAnalyzer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSIFLEX.PartAnalyzer.ViewModel
{
    public class PartPerformanceItemViewModel : ViewModelBase
    {
        private string reportedCycleTime;
        private string actualCycleTime;
        private string actualToReportedPercentage;
        private string partsMade;
        private string scrappedParts;
        private string scrappedPartsPercentage;
        private string partSetupTime;
        private string partOtherTime;
        private string cycleOnTime;
        private string cycleOffTime;
        private string machineName;
        private string jobNumber;
        private string partNumber;

        public PartPerformanceItemViewModel(MachinePartPerformance part, CSIFLEX.GeniusConnector.RestApi.Entities.JobEntity job)
        {
            var time = job.ProductionEndDate - job.ProductionStartDate;
            ReportedCycleTime = time == null ? 
                "Part production still in progress"
                : ((long)time.Value.TotalSeconds).FromSecondsToHHMMSS();
            ActualCycleTime = part.TotalTimeInSeconds.FromSecondsToHHMMSS();
            ActualToReportedPercentage = time == null ?
                "Operator has no completed the job yet."
                : string.Format("{0:N2}%", (part.TotalTimeInSeconds / time.Value.TotalSeconds * 100));
            PartsMade = job.PlannedQuantity.ToString();
            ScrappedParts = job.RejectedQuantity.ToString();
            ScrappedPartsPercentage = string.Format("{0:N2}%", (job.RejectedQuantity / job.PlannedQuantity * 100));
            PartSetupTime = part.TotalSetupInSeconds.FromSecondsToHHMMSS();
            PartOtherTime = part.TotalOtherInSeconds.FromSecondsToHHMMSS();
            PartCycleOnTime = part.TotalCycleOnInSeconds.FromSecondsToHHMMSS();
            PartCycleOffTime = part.TotalCycleOffInSeconds.FromSecondsToHHMMSS(); 
        }
        public string MachineName
        {
            get => machineName;
            set
            {
                machineName = value;
                RaisePropertyChanged(nameof(MachineName));
            }
        }
        public string JobNumber
        {
            get => jobNumber;
            set
            {
                jobNumber = value;
                RaisePropertyChanged(nameof(JobNumber));
            }
        }
        public string PartNumber
        {
            get => partNumber;
            set
            {
                partNumber = value;
                RaisePropertyChanged(nameof(PartNumber));
            }
        }

        public string ReportedCycleTime
        {
            get => reportedCycleTime;
            set
            {
                reportedCycleTime = value;
                RaisePropertyChanged(nameof(ReportedCycleTime));
            }
        }
        public string ActualCycleTime
        {
            get => actualCycleTime;
            set
            {
                actualCycleTime = value;
                RaisePropertyChanged(nameof(ActualCycleTime));
            }
        }


        public string PartsMade
        {
            get => partsMade;
            set
            {
                actualCycleTime = value;
                RaisePropertyChanged(nameof(PartsMade));
            }
        }

        public string ActualToReportedPercentage
        {
            get => actualToReportedPercentage;
            set
            {
                reportedCycleTime = value;
                RaisePropertyChanged(nameof(ActualToReportedPercentage));
            }
        }
        public string ScrappedPartsPercentage
        {
            get => scrappedPartsPercentage;
            set
            {
                scrappedPartsPercentage = value;
                RaisePropertyChanged(nameof(scrappedPartsPercentage));
            }
        }


        public string ScrappedParts
        {
            get => scrappedParts;
            set
            {
                scrappedParts = value;
                RaisePropertyChanged(nameof(ScrappedParts));
            }
        }


        public string PartSetupTime
        {
            get => partSetupTime;
            set
            {
                partSetupTime = value;
                RaisePropertyChanged(nameof(PartSetupTime));
            }
        }

        public string PartOtherTime
        {
            get => partOtherTime;
            set
            {
                partOtherTime = value;
                RaisePropertyChanged(nameof(PartOtherTime));
            }
        }

        public string PartCycleOnTime
        {
            get => cycleOnTime;
            set
            {
                cycleOnTime = value;
                RaisePropertyChanged(nameof(PartCycleOnTime));
            }
        }

        public string PartCycleOffTime
        {
            get => cycleOffTime;
            set
            {
                cycleOffTime = value;
                RaisePropertyChanged(nameof(PartCycleOffTime));
            }
        }
    }
}
