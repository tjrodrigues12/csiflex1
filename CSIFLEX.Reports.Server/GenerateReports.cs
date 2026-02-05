using CSIFLEX.Database.Access;
using CSIFLEX.Reports.Server.Data;
using CSIFLEX.Server.Library;
using CSIFLEX.Server.Library.DataModel;
using CSIFLEX.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFLEX.Reports.Server
{
    public class GenerateReports
    {
        public bool Working { get; set; }

        public string ReportWorking { get; set; }

        string strConnection = "";

        public string GenerateReport(string reportName, string rdlcPath, string strConnection_ = "")
        {
            strConnection = strConnection_;

            DataTable dtReport = MySqlAccess.GetDataTable($"SELECT * FROM csi_auth.auto_report_config WHERE Task_name = '{reportName}'", strConnection);

            if (dtReport.Rows.Count == 0)
            {
                return "";
            }

            DataRow report = dtReport.Rows[0];

            ReportParameters parameters = new ReportParameters();
            parameters.MachinesName = report["MachinesToReport"].ToString().Split(';').ToList();
            parameters.OnlySummary = Boolean.Parse(report["OnlySummary"].ToString());
            parameters.OutputPath = report["Output_Folder"].ToString();
            parameters.Production = Boolean.Parse(report["Production"].ToString());
            parameters.RdlcPath = rdlcPath;
            parameters.ReportName = report["Task_name"].ToString();
            parameters.ReportPeriod = report["ReportPeriod"].ToString();
            parameters.ReportType = report["Task_name"].ToString();
            parameters.Scale = report["Scale"].ToString();
            parameters.Setup = Boolean.Parse(report["Setup"].ToString());
            parameters.Shift = report["shift_number"].ToString();
            parameters.ShortFileName = Boolean.Parse(report["short_filename"].ToString());
            parameters.Sorting = report["Sorting"].ToString();

            if (report["MachinesToReport"].ToString().ToUpper().StartsWith("ALL"))
                parameters.Machines = Support.GetMachineTables();
            else
                parameters.Machines = Support.GetMachineTables().FindAll(m => parameters.MachinesName.Contains(m.MachineName));

            Log.Debug($"Report: {reportName}");
            Log.Debug($"Machines: {parameters.MachinesName}");
            foreach (MachineDBTable mc in parameters.Machines)
            {
                Log.Debug($"==> {mc.MachineName}");
            }

            DateTime startdate = new DateTime();
            DateTime enddate = DateTime.Today.Date.AddSeconds(-1);

            if (parameters.ReportPeriod == "Yesterday")
            {
                startdate = DateTime.Today.Date.AddDays(-1);
            }
            else if (parameters.ReportPeriod == "Weekly")
            {
                startdate = DateTime.Today.Date.AddDays(int.Parse(report["dayback"].ToString()) * -1);
            }
            else if (parameters.ReportPeriod == "Monthly")
            {
                startdate = DateTime.Today.Date.AddMonths(-1);
            }

            parameters.Start = startdate;
            parameters.End = enddate;

            return GenerateReport(parameters);
        }

        public string GenerateReport(ReportParameters parameters)
        {
            if (parameters.ReportType == "Downtime")
            {
                DowntimeReport rep = new DowntimeReport(parameters);
                return rep.GenerateReport();
            }
            return "";
        }
    }
}
