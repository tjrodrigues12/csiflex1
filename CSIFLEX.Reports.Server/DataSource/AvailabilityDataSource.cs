using CSIFLEX.Reports.Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFLEX.Reports.Server.DataSource
{
    public static class AvailabilityDataSource
    {
        private static List<MachineCycleTime> machinesCycles = null;
        private static ReportParameters param;
        private static string startDate;
        private static string endDate;

        public static bool ProcessReportDataSource(ReportParameters param_)
        {
            return true;
        }
    }
}
