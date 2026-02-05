using CSIFLEX.PartAnalyzer.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace CSIFLEX.PartAnalyzer.Views.Converters
{
    public class AggregateCycleTimeConverter : IValueConverter
    {
        public string CycleType { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (null == value)
            {
                return "null";
            }
            var retVal = string.Empty;

            var items = (ReadOnlyObservableCollection<object>)value;
            switch (CycleType)
            {
                case "On":
                    retVal = items.Select(x => x as GeniusDataViewModel)
                        .Select(x => x.ProductionPart.MachinePartPerformance.TotalCycleOnInSeconds)
                        .Sum()
                        .FromSecondsToHHMMSS();
                    break;
                case "Off":
                    retVal = items.Select(x => x as GeniusDataViewModel)
                        .Select(x => x.ProductionPart.MachinePartPerformance.TotalCycleOffInSeconds)
                        .Sum()
                        .FromSecondsToHHMMSS();
                    break;
                case "Setup":
                    retVal = items.Select(x => x as GeniusDataViewModel)
                        .Select(x => x.ProductionPart.MachinePartPerformance.TotalSetupInSeconds)
                        .Sum()
                        .FromSecondsToHHMMSS();
                    break;
                case "Other":
                    retVal = items.Select(x => x as GeniusDataViewModel)
                        .Select(x => x.ProductionPart.MachinePartPerformance.TotalOtherInSeconds)
                        .Sum()
                        .FromSecondsToHHMMSS();
                    break;
                case "Total":
                    retVal = items.Select(x => x as GeniusDataViewModel)
                        .Select(x => x.ProductionPart.MachinePartPerformance.TotalTimeInSeconds)
                        .Sum()
                        .FromSecondsToHHMMSS();
                    break;

                default:
                    break;
            }
             
            return retVal;
        }         

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
