using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using CSIFLEX.PartAnalyzer.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSIFLEX.Library;

namespace CSIFLEX.PartAnalyzer.ViewModel
{
    public class PartPerformanceViewModel : ViewModelBase
    {
        private MachinePartPerformance partPerf;
        private SeriesCollection cycleSeries;
        private string[] xAxisLabels;
        private string[] yAxisLabels;
        private SeriesCollection cycleTimeCollection;
        private string title;
        public Func<double, string> Formatter { get; set; }

        public PartPerformanceViewModel(MachinePartPerformance partPerf)
        {
            this.partPerf = partPerf;

            Title = $"Cycle time performance for {partPerf.PartNumber} on machine {partPerf.MachineName}";

            CycleTimeCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<ObservableValue>
                    {
                        new ObservableValue(this.partPerf.TotalCycleOffInSeconds),
                        new ObservableValue(this.partPerf.TotalCycleOnInSeconds),
                        new ObservableValue(this.partPerf.TotalOtherInSeconds),
                        new ObservableValue(this.partPerf.TotalSetupInSeconds)
                    },

                    DataLabels = true,
                    LabelPoint = point => ((long)point.Y).FromSecondsToHHMMSS()
                }
            };
            Mapper = Mappers.Xy<ObservableValue>()
              .X((i, idx) => idx)
              .Y((i) => i.Value)
              .Fill(item =>
              {
                  return item.Value == (double)partPerf.TotalCycleOffInSeconds
                  ? Brushes.Red
                  : item.Value == (double)partPerf.TotalCycleOnInSeconds
                  ? Brushes.Green
                  : item.Value == (double)partPerf.TotalSetupInSeconds
                  ? Brushes.Blue
                  : Brushes.Yellow;
              });
            XAxisLabels = new[]
                {
                    "Cycle Off",
                    "Cylce On",
                    "Other",
                    "Setup"
                };

            Formatter = value => ((long)value).FromSecondsToHHMMSS();
        }

        public SeriesCollection CycleTimeCollection
        {
            get => cycleTimeCollection;
            set
            {
                cycleTimeCollection = value;
                RaisePropertyChanged(nameof(CycleTimeCollection));
            }
        }

        public SeriesCollection CycleSeries
        {
            get => cycleSeries;
            set
            {
                cycleSeries = value;
                RaisePropertyChanged(nameof(CycleSeries));
            }
        }
        public string[] XAxisLabels
        {
            get => xAxisLabels;
            set
            {
                RaisePropertyChanged(nameof(XAxisLabels));
            }
        } 

        public string Title
        {
            get => title;
            set
            {
                title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        public CartesianMapper<ObservableValue> Mapper
        {
            get; set;
        }
    }
}
