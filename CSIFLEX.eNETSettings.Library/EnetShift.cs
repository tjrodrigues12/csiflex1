using CSIFLEX.eNETSettings.Library.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFLEX.eNETSettings.Library
{
    public static class EnetShift
    {
        public static List<DeptShifts> GetDepartShift(string enetPath, string dept = "")
        {
            List<DeptShifts> depart = new List<DeptShifts>();

            string shiftFilePath = Path.Combine(enetPath, "_SETUP", "ShiftSetup2.csys");

            if (!File.Exists(shiftFilePath))
                return depart;

            var lines = File.ReadAllLines(shiftFilePath);

            int deptNum = 1;

            while (deptNum < (int)(lines.Length / 4))
            {
                var line1 = lines[1].Split(',');

                DeptShifts deptShifts = new DeptShifts();
                deptShifts.DeptName = line1[0];


                DayShifts dayShifts = new DayShifts();
                dayShifts.ShiftDay = 0;

                ShiftTime shiftTime = new ShiftTime()
                {
                    ShiftNumber = 1,
                    ShiftStart = int.Parse(line1[1]),
                    ShiftEnd = int.Parse(line1[2])
                };
                dayShifts.Shifts.Add(1, shiftTime);

                shiftTime = new ShiftTime()
                {
                    ShiftNumber = 2,
                    ShiftStart = int.Parse(line1[3]),
                    ShiftEnd = int.Parse(line1[4])
                };
                dayShifts.Shifts.Add(2, shiftTime);

                shiftTime = new ShiftTime()
                {
                    ShiftNumber = 3,
                    ShiftStart = int.Parse(line1[5]),
                    ShiftEnd = int.Parse(line1[6])
                };
                dayShifts.Shifts.Add(3, shiftTime);

                deptNum++;
            }

            return depart;
        }
    }
}
