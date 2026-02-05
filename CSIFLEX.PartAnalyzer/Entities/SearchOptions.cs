using System;
using System.Collections.Generic;
using System.Text;

namespace CSIFLEX.PartAnalyzer.Entities
{
    public class SearchOptions
    { 
        public bool IsIterateSearchOverDate { get; set; }
        public int HourWindowValue { get; set; }
        public int MinuteWindowValue { get; set; }
        public int TimeWindowSearchIterations { get; set; }
        public bool IsIteratingOverPartName { get; set; }
        public int IterativeSearchPartNameValue { get; set; }
        public bool IsSplittingHyphens { get; set; }
    }
}
