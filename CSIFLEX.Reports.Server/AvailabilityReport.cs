using CSIFLEX.Reports.Server.Data;
using CSIFLEX.Server.Library;
using CSIFLEX.Server.Library.DataModel;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFLEX.Reports.Server
{
    public class AvailabilityReport : BaseReport
    {
        string typeOfReport;
        DateTime startDate;
        DateTime endDate;
        bool shortName = false;
        List<MachineTable> machines;

        int periodHours = 0;
        string scale = "Hours";
        string period = "";

        string reportPath = "../../Reports/DowntimeReport.rdlc";
        string saveFile = "";

        ReportViewer reportViewer;

        public AvailabilityReport(ReportParameters parameters)
        {
            typeOfReport = parameters.Type;
            startDate = parameters.Start;
            endDate = parameters.End;
            shortName = parameters.ShortFileName;

            if (parameters.Machines.Count == 0)
                machines = Support.GetMachineTables().FindAll(m => parameters.Machines.ToArray().ToString().Contains(m.MachineName));
            else
                machines = parameters.Machines;

            periodHours = (int)(endDate - startDate).TotalHours;

            if (!String.IsNullOrEmpty(parameters.RdlcPath))
                reportPath = parameters.RdlcPath;

            saveFile = parameters.SaveFileFullPath;

            reportViewer = new ReportViewer();
        }

        public string GenerateReport()
        {

            ReportViewer reportViewer = new ReportViewer();

            bool isSetup = true;
            string TypeDePeriode = "";
            string type = "";

            generateSqlQuery(machineList);

            ReportParameter[] paramet = new ReportParameter[3];

            reportViewer.ProcessingMode = ProcessingMode.Local;

            if (period.Contains("Today"))
            {
                reportViewer.LocalReport.ReportPath = Path.Combine(reportPath, "reports_templates", $"{(isSetup ? "mainDaily" : "EventMainDaily")}.rdlc"); 
                TypeDePeriode = "y";
                type = "Today";
            }
            else if (period.Contains("Weekly"))
            {
                reportViewer.LocalReport.ReportPath = Path.Combine(reportPath, "reports_templates", $"{(isSetup ? "mainWeekly" : "EventMainReport")}.rdlc");
                TypeDePeriode = "ww";
                type = "weekly";
            }
            else if (period.Contains("Monthly"))
            {
                reportViewer.LocalReport.ReportPath = Path.Combine(reportPath, "reports_templates", $"{(isSetup ? "mainMonthly" : "EventMainMonthly")}.rdlc");
                TypeDePeriode = "m";
                type = "monthly";
            }
            else if (period.Contains("Yesterday"))
            {
                reportViewer.LocalReport.ReportPath = Path.Combine(reportPath, "reports_templates", $"{(isSetup ? "mainDaily" : "EventMainDaily")}.rdlc");
                TypeDePeriode = "y";
                type = "Yesterday";
            }

            reportViewer.LocalReport.Refresh();

            //DateAffectation(ReportStartDate, ReportEndDate)

            paramet[0] = new ReportParameter("reportType", TypeDePeriode);
            paramet[1] = new ReportParameter("startdate", startDate.ToString("dd-MMM-yyyy hh:mm"));
            paramet[2] = new ReportParameter("enddate", endDate.ToString("dd-MMM-yyyy hh:mm"));

            reportViewer.LocalReport.SetParameters(paramet);

            ReloadReport(reportViewer)

            reportViewer.RefreshReport();

            return SaveReport(reportViewer, typeOfReport, period, shortName, saveFile);
        }
    }
}
