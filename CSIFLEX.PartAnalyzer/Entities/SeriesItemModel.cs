using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFLEX.PartAnalyzer.Entities
{
    public class SeriesItemModel
    {
        public DateTime DateTime { get; set; }
        public int Value { get; set; }

        public string Status { get; set; }
    }
}
