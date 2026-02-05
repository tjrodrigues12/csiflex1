using CSIFLEX.PartAnalyzer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFLEX.PartAnalyzer.Extensions
{
    public static  class MachineItemExtensions
    {
        public static SeriesItemModel ToSeriesItemModel(this MachineDataEntry @this, string status)
        {
            return new SeriesItemModel()
            {
                DateTime = DateTime.Parse(@this.Date_),
                Value = @this.cycletime,
                Status = status
            };
        }
    }
}
