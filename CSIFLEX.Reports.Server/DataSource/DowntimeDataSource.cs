using CSIFLEX.Database.Access;
using CSIFLEX.Reports.Server.Data;
using CSIFLEX.Server.Library.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFLEX.Reports.Server
{
    public static class DowntimeDataSource
    {
        private static List<MachineCycleTime> machinesCycles = null;
        private static List<MachineCycleTime> summaryList = null;
        private static List<MachinesTableRow> machinesTables = null;
        private static string startDate;
        private static string endDate;
        private const string noData = "NO DATA FOR THE REQUESTED PERIOD";
        private static ReportParameters param;

        private static string strConnection = "";

        static readonly object dataLock = new object();

        public static bool ProcessReportDataSource(ReportParameters param_, string strConnection_ = "")
        {
            try
            {
                param = param_;
                strConnection = strConnection_;

                if (param.Machines.Count == 0)
                    throw new Exception("No machines selected.");

                machinesCycles = new List<MachineCycleTime>();
                startDate = param.Start.ToString("yyyy-MM-dd HH:mm:ss");
                endDate = param.End.ToString("yyyy-MM-dd HH:mm:ss");

                Parallel.ForEach(param.Machines, (mach) => MachineSearch(mach, param.ReportPeriod == "Today", strConnection));

                summaryList = new List<MachineCycleTime>();

                List<string> machinesList = new List<string>();

                string color = "";
                string setupColor = "";
                string cycleStatus = "";

                foreach (MachineCycleTime cycle in machinesCycles)
                {
                    color = cycle.StatusColor;
                    cycleStatus = cycle.CycleStatus;

                    if (machinesList.FindIndex(m => m == cycle.MachineName) < 0)
                        machinesList.Add(cycle.MachineName);

                    if (cycleStatus != noData)
                    {
                        if (cycleStatus == "SETUP")
                        {
                            setupColor = color;
                        }
                        if (cycleStatus == "SETUP-CYCLE ON" && param.ShowConInSetup == 0)
                        {
                            cycleStatus = "SETUP";
                            color = setupColor;
                        }

                        MachineCycleTime sumary = summaryList.Find(s => s.CycleStatus == cycle.CycleStatus);
                        if (sumary != null)
                        {
                            sumary.CycleTime += cycle.CycleTime;
                        }
                        else
                        {
                            summaryList.Add(new MachineCycleTime()
                            {
                                MachineName = " Summary",
                                CycleStatus = cycleStatus,
                                CycleTime = cycle.CycleTime,
                                StatusColor = color
                            });
                        }
                    }
                }
                if (machinesList.Count > 1 || param.OnlySummary)
                    machinesCycles.InsertRange(0, summaryList);

                int mchIdx = 0;
                machinesTables = new List<MachinesTableRow>();
                MachinesTableRow tableRow = new MachinesTableRow();

                foreach (var mch in param.Machines.OrderBy(m => m.MachineName))
                {
                    switch (mchIdx)
                    {
                        case 0:
                            tableRow = new MachinesTableRow()
                            {
                                Column1 = mch.MachineName,
                                Column2 = "",
                                Column3 = "",
                                Column4 = "",
                                Column5 = ""
                            };
                            break;
                        case 1:
                            tableRow.Column2 = mch.MachineName;
                            break;
                        case 2:
                            tableRow.Column3 = mch.MachineName;
                            break;
                        case 3:
                            tableRow.Column4 = mch.MachineName;
                            break;
                        case 4:
                            tableRow.Column5 = mch.MachineName;
                            machinesTables.Add(tableRow);
                            break;
                    }
                    mchIdx++;
                    if (mchIdx > 4) mchIdx = 0;
                }

                if (mchIdx > 0) machinesTables.Add(tableRow);

                return true;

            } catch (Exception ex)
            {
                throw new Exception("Error while processing the Report Data Source.", ex);
            }
        }

        public static List<MachineCycleTime> GetMachines()
        {
            return machinesCycles;
        }

        public static List<MachineCycleTime> GetSummary()
        {
            return summaryList;
        }

        public static List<MachinesTableRow> GetMachinesTable()
        {
            return machinesTables;
        }

        private static void MachineSearch(MachineDBTable machineTable, bool todayReport = false, string strConnection_ = "")
        {
            string exclusion = "''";

            if (!param.Production)
            {
                exclusion = "'CYCLE ON', 'CYCLE OFF'";
            }
            if (!param.Setup)
            {
                if (exclusion.Length > 2)
                {
                    exclusion += ", 'SETUP', 'SETUP-CYCLE ON'";
                } else
                {
                    exclusion = "'SETUP', 'SETUP-CYCLE ON'";
                }
            }

            string dbTable = (todayReport ? "csi_machineperf." : "csi_database.") + machineTable.TableName;

            StringBuilder query = new StringBuilder();
            query.Append($"SELECT                                     ");
            query.Append($"   M.status,                               ");
            query.Append($"   sum(M.cycletime) as cycletime,          ");
            query.Append($"   C.color                                 ");
            query.Append($"FROM                                       ");
            query.Append($"   {dbTable} AS M                          ");
            query.Append($"   LEFT JOIN csi_database.tbl_colors AS C ON   ");
            query.Append($"   M.status = C.statut COLLATE utf8_unicode_ci ");
            query.Append($"WHERE                                      ");

            if (todayReport)
            {
                query.Append($"       M.shift IN ({param.Shift})      ");
            }
            else
            {
                query.Append($"       M.ShiftDate >= '{startDate}'    ");
                query.Append($"   AND M.ShiftDate <  '{endDate}'      ");

                if (param.Shift != "1,2,3")
                {
                    query.Append($"   AND M.shift IN ({param.Shift})      ");
                }
            }
            query.Append($"   AND M.cycletime <> 0                    ");
            query.Append($"   AND M.status NOT IN ({exclusion})       ");
            query.Append($"GROUP BY                                   ");
            query.Append($"   M.status,                               ");
            query.Append($"   C.color                                 ");
            query.Append($"ORDER BY                                   ");
            query.Append($"   cycletime                         ");

            int minCycleTime = param.EventMinMinutes * 60;

            try
            {
                var res = MySqlAccess.GetDataTable(query.ToString(), strConnection_);
                decimal cycleTime = 0;
                string cycleStatus = "";

                if (res.Rows.Count == 0)
                {
                    lock (dataLock)
                    {
                        machinesCycles.Add(new MachineCycleTime()
                        {
                            MachineName = machineTable.MachineName,
                            CycleStatus = noData,
                            CycleTime = 0,
                            StatusColor = "White"
                        });
                    }
                }
                else
                {
                    string color = "";
                    string setupColor = "";

                    foreach (DataRow row in res.Rows)
                    {
                        color = row["color"].ToString();
                        cycleTime = decimal.Parse(row["cycletime"].ToString());

                        if (cycleTime > minCycleTime)
                        {
                            cycleTime = cycleTime / (param.Scale == "Hours" ? 3600 : 60);
                            cycleStatus = row["status"].ToString();

                            if (cycleStatus == "SETUP")
                            {
                                setupColor = color;
                            }
                            if (cycleStatus == "SETUP-CYCLE ON" && param.ShowConInSetup == 0)
                            {
                                cycleStatus = "SETUP";
                                color = setupColor;
                            }

                            lock (dataLock)
                            {
                                machinesCycles.Add(new MachineCycleTime()
                                {
                                    MachineName = machineTable.MachineName,
                                    CycleStatus = cycleStatus,
                                    CycleTime = cycleTime,
                                    StatusColor = color
                                });
                                if (cycleStatus == "SETUP-CYCLE ON")
                                    machinesCycles.Add(new MachineCycleTime()
                                    {
                                        MachineName = machineTable.MachineName,
                                        CycleStatus = "SETUP",
                                        CycleTime = cycleTime,
                                        StatusColor = setupColor
                                    });
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
    }
}
