using System.Collections.Generic;

namespace CSIFLEX.eNETSettings.Library.Data
{
    public class DeptShifts
    {
        public string DeptName { get; set; }

        public Dictionary<string, DayShifts> ShiftDays { get; set; }

        public DeptShifts()
        {
            ShiftDays = new Dictionary<string, DayShifts>();
        }
    }

    public class DayShifts
    {
        public int ShiftDay { get; set; }

        public Dictionary<int, ShiftTime> Shifts {get;set;}

        public DayShifts()
        {
            Shifts = new Dictionary<int, ShiftTime>();
        }
    }

    public class ShiftTime
    {
        public int ShiftNumber { get; set; }

        public int ShiftStart { get; set; }

        public int ShiftEnd { get; set; }
    }

}
