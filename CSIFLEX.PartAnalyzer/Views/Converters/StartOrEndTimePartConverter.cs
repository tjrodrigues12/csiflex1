using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace CSIFLEX.PartAnalyzer.Views.Converters
{
    public class StartOrEndTimePartConverter: IValueConverter
    {
        public string TimeType { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (null == value)
            {
                return "null";
            }
            var retVal = string.Empty;

            var items = (ReadOnlyObservableCollection<object>)value;
            var toAnalyze = items.Select(x => x as GeniusDataViewModel)
                        .OrderBy(x => x.ProductionPart.MachinePartPerformance.ProductionStartTime);
            switch (TimeType)
            {
                case "Start":
                    retVal = toAnalyze.First().ProductionPart.MachinePartPerformance.ProductionStartTime.ToString();
                    break;
                case "End":
                    retVal = toAnalyze.Last().ProductionPart.MachinePartPerformance.ProductionStartTime.ToString();
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
