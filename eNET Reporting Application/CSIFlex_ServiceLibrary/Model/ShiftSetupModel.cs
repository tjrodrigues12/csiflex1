using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFlex_ServiceLibrary.Model
{
    public class ShiftSetupModel
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string DayName { get; set; }
        public string ShiftName { get; set; }
        public int ShiftStart { get; set; }
        public int ShiftEnd { get; set; }
        public int Break1Start { get; set; }
        public int Break1End { get; set; }
        public int Break2Start { get; set; }
        public int Break2End { get; set; }
        public int Break3Start { get; set; }
        public int Break3End { get; set; }
        public DateTime LogDate { get; set; }
    }
}
