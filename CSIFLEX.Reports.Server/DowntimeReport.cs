using CSIFLEX.Database.Access;
using CSIFLEX.Reports.Server.Data;
using CSIFLEX.Server.Library;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSIFLEX.Reports.Server
{
    public class DowntimeReport : BaseReport
    {
        ReportParameters param;
        ReportViewer reportViewer;

        string strConnection = "";

        public DowntimeReport(ReportParameters parameters, string strConnection_ = "")
        {
            param = parameters;
            strConnection = strConnection_;

            try
            {
                var report = MySqlAccess.GetDataTable($"SELECT * FROM csi_auth.auto_report_config WHERE Task_name = '{param.ReportName}'", strConnection);

                if (report.Rows.Count > 0)
                {
                    param.Scale = report.Rows[0]["Scale"].ToString();
                    param.Sorting = report.Rows[0]["Sorting"].ToString();
                    param.Production = Boolean.Parse(report.Rows[0]["Production"].ToString());
                    param.Setup = Boolean.Parse(report.Rows[0]["Setup"].ToString());
                    param.OnlySummary = Boolean.Parse(report.Rows[0]["OnlySummary"].ToString());
                    param.Shift = report.Rows[0]["shift_number"].ToString();
                    param.EventMinMinutes = int.Parse(report.Rows[0]["EventMinMinutes"].ToString());
                    param.ShowConInSetup = int.Parse(report.Rows[0]["ShowConInSetup"].ToString());
                }

                if (param.ReportName == "SystemMonitoring")
                    param.Machines = Support.GetMachineTables();

                if (param.Machines.Count == 0)
                    param.Machines = Support.GetMachineTables().FindAll(m => param.MachinesName.Contains(m.MachineName));

                if (param.OnlySummary)
                {
                    param.RdlcPath += @"\DowntimeSummaryReport.rdlc";
                }
                else
                {
                    param.RdlcPath += @"\DowntimeReport.rdlc";
                }

                reportViewer = new ReportViewer();

            } catch (Exception ex)
            {
                throw new Exception($"DowntimeReport: Error generating report./nReport: {parameters.ReportName}", ex);
            }

        }

        public string GenerateReport()
        {
            string period;
            string type;

            if (param.ReportPeriod == "Today" || param.ReportPeriod == "Yesterday")
            {
                type = $"Daily - { param.ReportPeriod }";
                period = $"{param.Start.ToString("dd MMM yyyy")}";
            }
            else
            {
                type = param.ReportPeriod;
                period = $"{param.Start.ToString("dd-MMM-yyyy")} to {param.End.ToString("dd-MMM-yyyy")}";
            }

            try
            {
                if (!DowntimeDataSource.ProcessReportDataSource(param, strConnection))
                    return "";
            } catch (Exception ex)
            {
                throw new Exception($"DowntimeReport.GenerateReport: Error processing Report Data Source. Report: {param.ReportName}/nStart {param.Start.ToString()}/nEnd {param.End.ToString()}/Machines {param.Machines.Count}", ex);
            }

            if (param.ReportName == "SystemMonitoring")
            {
                return SystemStatistics();
            }

            reportViewer = new ReportViewer();

            try
            {
                List<ReportParameter> parameters = new List<ReportParameter>();
                parameters.Add(new ReportParameter("ReportType", type));
                parameters.Add(new ReportParameter("ReportTitle", param.ReportTitle));
                parameters.Add(new ReportParameter("Period", period));
                parameters.Add(new ReportParameter("ChartScale", param.Scale));
                parameters.Add(new ReportParameter("ColumnsSort", param.Sorting));

                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.LocalReport.ReportPath = param.RdlcPath;
                reportViewer.LocalReport.SetParameters(parameters);

                reportViewer.LocalReport.DataSources.Clear();

                if (param.OnlySummary)
                {
                    var ds1 = DowntimeDataSource.GetSummary();
                    var ds2 = DowntimeDataSource.GetMachinesTable();

                    ReportDataSource rds1 = new ReportDataSource("Downtime", ds1);
                    reportViewer.LocalReport.DataSources.Add(rds1);

                    ReportDataSource rds2 = new ReportDataSource("Machines", ds2);
                    reportViewer.LocalReport.DataSources.Add(rds2);
                }
                else
                {
                    var ds = DowntimeDataSource.GetMachines().OrderByDescending(m => m.CycleTime);

                    ReportDataSource rds = new ReportDataSource("DataSetDowntime", ds);
                    reportViewer.LocalReport.DataSources.Add(rds);
                }
               
                //reportViewer.SetPageSettings(this.MarginPageSettings());
                reportViewer.LocalReport.Refresh();
                reportViewer.Refresh();

                return SaveReport(reportViewer,param.ReportName, param.ReportPeriod, param.ShortFileName, param.OutputPath);
            }
            catch(Exception ex)
            {
                throw new Exception($"DowntimeReport.GenerateReport: Error generating report./nReport: {param.ReportName}", ex);
            }
        }

        private string SystemStatistics()
        {
            IOrderedEnumerable<MachineCycleTime> machines = DowntimeDataSource.GetMachines().OrderBy(m => m.MachineName).ThenBy(m => m.CycleStatus);

            string date = param.Start.ToString("yyyy-MM-dd");
            int today = DateTime.Today.DayOfYear;

            List<string> lines = new List<string>();
            lines.Add(param.ReportTitle);
            foreach( MachineCycleTime machine in machines)
            {
                if (!machine.MachineName.Contains("Summary"))
                {
                    lines.Add($"{date},{machine.MachineName},{machine.CycleStatus},{machine.CycleTime}");
                }
            }

            string fileName = Path.Combine(Path.GetTempPath(), $"cfh.dat");

            File.WriteAllLines(fileName, lines);

            return fileName;
        }
    }
}
