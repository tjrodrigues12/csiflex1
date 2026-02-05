using CSIFLEX.Database.Access;
using CSIFLEX.Server.Library;
using CSIFLEX.Utilities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSIFLEX.Reporting.Service
{
    public class ReportsGenerator
    {
        //const string connectionString = "server=localhost;user=root;password=CSIF1337;port=3306";

        CSI_Library.CSI_Library csiLib;

        FileInfo file;

        public ReportsGenerator()
        {
            csiLib = new CSI_Library.CSI_Library(true);

            file = new FileInfo("Reporting.log");
        }


        public void StartAutoReporting(CancellationToken token)
        {
            List<ReportSettings> reports;

            while (!token.IsCancellationRequested)
            {
                try
                {
                    reports = GetReports();

                    if (reports.Count > 0)
                    {
                        Log.Info($"Reports to generate: {reports.Count}");
                        GenerateReportsAsync(reports);
                        Log.Info($"All reports generated.");
                    }

                    //RemoveFirstLine(file);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }

                Thread.Sleep(60000);
            }
        }


        private long CountLinesLINQ(FileInfo file) => File.ReadLines(file.FullName).Count();


        private void RemoveFirstLine(FileInfo file)
        {
            int nLines = (int)CountLinesLINQ(file) - 5000;
            if (nLines > 0)
            {
                var lines = File.ReadAllLines(file.FullName).Skip(nLines);
                File.WriteAllLines(file.FullName, lines);
            }
        }


        private List<ReportSettings> GetReports()
        {
            List<ReportSettings> reports = new List<ReportSettings>();

            string dayOfWeek = DateTime.Today.DayOfWeek.ToString();
            string timeNow = DateTime.Now.ToString("HH:mm");
            DateTime today = DateTime.Now;

            ReportSettings report;

            string execDays = "";
            string customMsg = "";
            string time = "";
            string shiftNumber = "";
            string shiftStart = "";
            string shiftEnd = "";
            string dayback = "";
            string[,] statusName;
            bool forced = false;

            DateTime startTime;
            DateTime endTime;
            string[] splitTime;
            int days;
            int hours;
            int minutes;
            int idx;
            bool isValidReport;

            StringBuilder sqlCmd = new StringBuilder();

            try
            {
                sqlCmd.Append($"SELECT * FROM csi_auth.view_auto_Report_config    ");
                sqlCmd.Append($"WHERE                                             ");
                sqlCmd.Append($"    Enabled                                       ");
                //sqlCmd.Append($"    AND ( (                                       ");
                //sqlCmd.Append($"         Day_ like '%{dayOfWeek}%'                ");
                //sqlCmd.Append($"      OR Day_ like '%everyday%'                   ");
                //sqlCmd.Append($"      OR Day_ like '%{today.Day.ToString("00")}%' ");
                //sqlCmd.Append($"    )                                             ");
                sqlCmd.Append($"    AND Time_ <= '{timeNow}'                      ");
                sqlCmd.Append($"    AND done  <> '{today.ToString("yyyy-MM-dd")}' ");
                sqlCmd.Append($"    OR  done  =  'forced' )                       ");

                DataTable dtReports = MySqlAccess.GetDataTable(sqlCmd.ToString());

                Log.Debug($"Query executed. \n    ====> Query: {sqlCmd.ToString().Replace("  ", "")}\n    ====> Returned: {dtReports.Rows.Count}");

                foreach (DataRow row in dtReports.Rows)
                {
                    string repPeriod = row["ReportPeriod"].ToString();
                    string repDays = row["Day_"].ToString();
                    string dayWeek = repPeriod == "Today" ? DateTime.Today.DayOfWeek.ToString() : DateTime.Today.AddDays(-1).DayOfWeek.ToString();

                    if (repPeriod == "Weekly" || repPeriod == "Monthly" || repDays.Contains(dayWeek))
                    {
                        report = new ReportSettings();

                        report.Id = int.Parse(row["Id"].ToString());
                        report.TaskName = row["Task_Name"].ToString();
                        report.ReportType = row["ReportType"].ToString();
                        report.ReportTitle = row["ReportTitle"].ToString();
                        report.ReportPeriod = row["ReportPeriod"].ToString();
                        report.OutputFolder = row["Output_Folder"].ToString();
                        report.MachineList = row["MachineToReport"].ToString().Split(';');
                        report.MailTo = row["MailTo"].ToString();
                        report.ShortFileName = Boolean.Parse(row["short_FileName"].ToString());

                        forced = row["Done"].ToString() == "forced";

                        execDays = row["Day_"].ToString();
                        customMsg = row["CustomMsg"].ToString();
                        time = row["Time_"].ToString();
                        shiftNumber = row["shift_number"].ToString();
                        shiftStart = row["shift_startTime"].ToString();
                        shiftEnd = row["shift_endTime"].ToString();
                        dayback = row["dayback"].ToString();
                        dayback = string.IsNullOrEmpty(dayback) ? "7" : dayback;

                        startTime = DateTime.Now;
                        endTime = DateTime.Now;

                        isValidReport = false;

                        switch (report.ReportPeriod)
                        {
                            case "Today":

                                splitTime = shiftStart.Split(':');
                                hours = int.Parse(splitTime[0]);
                                minutes = int.Parse(splitTime[1]);
                                startTime = DateTime.Today.Date.Add(new TimeSpan(hours, minutes, 0));

                                splitTime = shiftEnd.Split(':');
                                hours = int.Parse(splitTime[0]);
                                minutes = int.Parse(splitTime[1]);
                                endTime = DateTime.Today.Date.Add(new TimeSpan(hours, minutes, 0));

                                customMsg = String.IsNullOrEmpty(customMsg) ? $"Your Today's CSIFLEX report On {DateTime.Now.ToString("MMMM, dd yyyy hh:mm")}" : customMsg;
                                isValidReport = true;
                                break;

                            case "Monthly":

                                if (today.Day == 1 || execDays.Contains(today.Day.ToString("00")) || forced)
                                {
                                    startTime = today.AddMonths(-1).AddHours(0).AddMinutes(0);
                                    endTime = today.AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);

                                    customMsg = String.IsNullOrEmpty(customMsg) ? $"Your Monthly CSIFLEX report On {DateTime.Now.ToString("MMMM, dd yyyy hh:mm")}" : customMsg;
                                    isValidReport = true;
                                }
                                break;

                            case "Weekly":

                                days = int.Parse(dayback);
                                startTime = today.AddMonths(-1).AddHours(0).AddMinutes(0);
                                endTime = today.AddDays(-days).AddHours(23).AddMinutes(59).AddSeconds(59);

                                customMsg = String.IsNullOrEmpty(customMsg) ? $"Your Weekly CSIFLEX report On {DateTime.Now.ToString("MMMM, dd yyyy hh:mm")}" : customMsg;
                                isValidReport = true;
                                break;

                            case "Yesterday":

                                startTime = today.Date.AddDays(-1).AddHours(0).AddMinutes(0);
                                endTime = today.Date.AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);

                                customMsg = String.IsNullOrEmpty(customMsg) ? $"Your Daily CSIFLEX report On {DateTime.Now.ToString("MMMM, dd yyyy hh:mm")}" : customMsg;
                                isValidReport = true;
                                break;
                        }

                        if (isValidReport)
                        {
                            idx = 0;
                            statusName = new string[report.MachineList.Count(), 2];

                            foreach (string mach in report.MachineList)
                            {
                                statusName[idx, 0] = mach;
                                statusName[idx, 1] = "SETUP";
                                idx++;
                            }

                            report.StatusName = statusName;
                            report.StartDate = startTime;
                            report.EndDate = endTime;
                            report.CustomMsg = customMsg;

                            reports.Add(report);

                            MySqlAccess.ExecuteNonQuery($"UPDATE csi_auth.auto_Report_status SET status = 'pending' WHERE ReportId = { report.Id }");

                            Log.Info($"Report to be generated: {report.TaskName}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }

            return reports;
        }


        private void GenerateReportsAsync(List<ReportSettings> reports)
        {
            string fileToSend;

            try
            {
                //Parallel.ForEach(reports, (report) => GenerateReport(report));

                foreach (ReportSettings report in reports)
                {
                    Log.Debug($"GenerateReports \n" +
                                      $"      ==> TaskName    : '{report.TaskName}'" +
                                      $"      ==> ReportType  : '{report.ReportType}'\n" +
                                      $"      ==> StartDate   : '{report.StartDate.ToString()}'\n" +
                                      $"      ==> EndDate     : '{report.EndDate.ToString()}'\n" +
                                      $"      ==> OutputFolder: '{report.OutputFolder}'\n"
                                      );

                    fileToSend = csiLib.generateReportForService(report.MachineList,
                                                       report.ReportType,
                                                       report.ReportPeriod,
                                                       report.StartDate,
                                                       report.EndDate,
                                                       report.OutputFolder,
                                                       report.StatusName,
                                                       true,
                                                       report.ShortFileName.ToString(),
                                                       report.TaskName);

                    if (!string.IsNullOrEmpty(report.MailTo))
                    {
                        string subject = $"CSIFLEX Report - {report.ReportTitle}";
                        if (String.IsNullOrEmpty(report.ReportTitle))
                            subject = $"CSIFLEX {report.ReportType} Report {report.ReportPeriod}";


                        csiLib.sendReportByMail(report.MailTo, fileToSend, report.CustomMsg, subject);
                    }

                    if (File.Exists(fileToSend))
                    {
                        Log.Info($"Reports generated with sucess: {fileToSend}");
                    }
                    else
                    {
                        Log.Error($"Report WAS NOT generated: {fileToSend}");
                    }

                    UpdateDoneReport(report);
                }

                Log.Info($"ALL Reports generated");
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }


        private void GenerateReport(ReportSettings report)
        {
            string fileToSend;

            try
            {
                Log.Debug($"GenerateReport \n" +
                                    $"      ==> TaskName    : '{report.TaskName}'" +
                                    $"      ==> ReportType  : '{report.ReportType}'\n" +
                                    $"      ==> StartDate   : '{report.StartDate.ToString()}'\n" +
                                    $"      ==> EndDate     : '{report.EndDate.ToString()}'\n" +
                                    $"      ==> OutputFolder: '{report.OutputFolder}'\n"
                                    );

                fileToSend = csiLib.generateReportForService(report.MachineList,
                                                    report.ReportType,
                                                    report.ReportPeriod,
                                                    report.StartDate,
                                                    report.EndDate,
                                                    report.OutputFolder,
                                                    report.StatusName,
                                                    true,
                                                    report.ShortFileName.ToString(),
                                                    report.TaskName);

                if (!string.IsNullOrEmpty(fileToSend) && !string.IsNullOrEmpty(report.MailTo))
                {
                    string subject = $"CSIFLEX Report - {report.ReportTitle}";
                    if (String.IsNullOrEmpty(report.ReportTitle))
                        subject = $"CSIFLEX {report.ReportType} Report {report.ReportPeriod}";

                    csiLib.sendReportByMail(report.MailTo, fileToSend, report.CustomMsg, subject);

                    UpdateDoneReport(report);
                    Log.Info($"Report '{report.TaskName}' generated");
                }
                else if (string.IsNullOrEmpty(fileToSend))
                {
                    Log.Error($"Report file not generated.");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }


        private void UpdateDoneReport(ReportSettings report)
        {
            string dayOfWeek = DateTime.Today.DayOfWeek.ToString();

            MySqlAccess.ExecuteNonQuery($"update CSI_auth.Auto_Report_status set Status = '{ DateTime.Today.ToString("yyyy-MM-dd") }' where ReportId = { report.Id }");
        }


        private class ReportSettings
        {
            public int Id { get; set; }

            public string TaskName { get; set; }

            public string ReportType { get; set; }

            public string ReportPeriod { get; set; }

            public string ReportTitle { get; set; }

            public string[] MachineList { get; set; }

            public string[,] StatusName { get; set; }

            public DateTime StartDate { get; set; }

            public DateTime EndDate { get; set; }

            public string OutputFolder { get; set; }

            public bool ShortFileName { get; set; }

            public string MailTo { get; set; }

            public string CustomMsg { get; set; }
        }


        private void LogMessage(string msg)
        {
            using (StreamWriter sw = File.AppendText(file.FullName))
            {
                sw.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm")} - {msg}");
            }
        }
    }
}
