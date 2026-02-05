using CSIFLEX.Database.Access;
using CSIFLEX.Reports.Server.Data;
using CSIFLEX.Server.Library.DataModel;
using CSIFLEX.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFLEX.Reports.Server.DataSource
{
    public class AvailabilityOldDataSource
    {
        private static List<DataSet_data> list_data = null;
        private static List<DataSet_history> list_data_history = null;
        private static List<DataSet_history_daily> list_data_history_daily = null;
        private static List<DataSet_timeline> list_Timeline = null;
        private static List<DataSet_4Reasons> list_4Reasons = null;

        private static ReportParameters param;
        private static string startDate;
        private static string endDate;
        private static string strConnection;

        private const string cycleOn = "CYCLE ON;_CON";
        private const string cycleOff = "CYCLE OFF;_COFF";
        private const string setup = "SETUP;_SETUP";
        private const string setupCon = "SETUP;_SETUP;SETUP-CYCLE ON";
        private const string production = "CYCLE ON;_CON;CYCLE OFF;_COFF;SETUP;_SETUP";

        static readonly object dataLock = new object();
        static readonly object histLock = new object();

        private static DateTime firstDayPeriod = new DateTime();
        private static DateTime firstDayHistory = new DateTime();
        private static DateTime firstDay4ReasonPeriod = new DateTime();

        private static int firstWeekDay;
        private static int lastWeekDay;

        private static Dictionary<DateTime, DateTime> dicValidDays;

        public static bool ProcessOldReportDataSource(ReportParameters param_, string strConnection_ = "")
        {
            try
            {
                param = param_;
                strConnection = strConnection_;

                if (param.Machines.Count == 0)
                    return false;

                list_data = new List<DataSet_data>();
                list_data_history = new List<DataSet_history>();
                list_data_history_daily = new List<DataSet_history_daily>();
                list_Timeline = new List<DataSet_timeline>();
                list_4Reasons = new List<DataSet_4Reasons>();

                startDate = param.Start.ToString("yyyy-MM-dd");
                endDate = param.End.ToString("yyyy-MM-dd");

                firstWeekDay = param.firstWeekDay;
                lastWeekDay = param.lastWeekDay;

                if (param.ReportPeriod == "Today" || param.ReportPeriod == "Yesterday")
                {
                    startDate = param.Start.AddDays(-13).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else if (param.ReportPeriod == "Weekly")
                {
                    //firstDayPeriod = DateTime.Today.AddDays(-((int)DateTime.Today.DayOfWeek)).AddDays(-7);
                    firstDayPeriod = param.Start;
                    firstDay4ReasonPeriod = firstDayPeriod.AddDays(-(7 * 4));
                    firstDayHistory = firstDayPeriod.AddDays(-(7 * 14));

                    startDate = firstDayHistory.ToString("yyyy-MM-dd HH:mm:ss");
                    endDate = param.End.ToString("yyyy-MM-dd HH:mm:ss");

                    //startDate = param.Start.AddDays(-7 * 17).ToString("yyyy-MM-dd HH:mm:ss");

                    dicValidDays = new Dictionary<DateTime, DateTime>();

                    DateTime refDate = DateTime.ParseExact(startDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).Date;
                    DateTime weekDateStart = new DateTime();
                    bool startPeriod = false;

                    while (refDate <= DateTime.ParseExact(endDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).Date)
                    {
                        if ((int)refDate.DayOfWeek == param.firstWeekDay)
                        {
                            weekDateStart = refDate.Date;
                            startPeriod = true;
                        }
                        if (startPeriod)
                        {
                            dicValidDays.Add(refDate.Date, weekDateStart);
                        }
                        if ((int)refDate.DayOfWeek == param.lastWeekDay)
                        {
                            startPeriod = false;
                        }
                        refDate = refDate.AddDays(1);
                    }
                }
                else if (param.ReportPeriod == "Monthly")
                {
                    firstDayPeriod = DateTime.Today.AddDays(-((int)DateTime.Today.Day - 1)).AddMonths(-1);
                    firstDay4ReasonPeriod = firstDayPeriod.AddMonths(-4);
                    firstDayHistory = firstDayPeriod.AddMonths(-6);

                    startDate = firstDayHistory.ToString("yyyy-MM-dd HH:mm:ss");
                    endDate = firstDayPeriod.AddMonths(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");

                    //startDate = param.Start.AddMonths(-3).ToString("yyyy-MM-dd HH:mm:ss");
                }

                Parallel.ForEach(param.Machines, (mach) => LoadDataSource(mach));

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while processing the Report Data Source.", ex);
            }
        }


        private static void LoadDataSource(MachineDBTable machine)
        {
            list_data.Add(new DataSet_data()
            {
                MchId = machine.MachineId,
                MchName = machine.MachineName,
                CycleOff = 0,
                CycleOn = 0,
                SumOther = 0,
                SumSetup = 0,
                Totalcycletime = 0,
                shift = 1,
                StatusExpr = "SETUP"
            });

            if (param.ReportPeriod == "Today")
            {
                LoadTodayCycles(machine);
            }

            LoadCycles(machine);
        }

        private static void LoadTodayCycles(MachineDBTable machine)
        {
            //: $"DAYOFWEEK(date_) NOT IN (1,7) AND date_ BETWEEN '{ startDate }' AND '{ endDate }'"

            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append($"SELECT                                ");
            sqlCmd.Append($"  *                                   ");
            sqlCmd.Append($"FROM                                  ");
            sqlCmd.Append($"  csi_machineperf.{machine.TableName} ");
            sqlCmd.Append($"WHERE                                 ");
            sqlCmd.Append($"  shift IN ({ param.Shift })          ");
            sqlCmd.Append($"  AND status not like '\\_%'          ");

            try
            {
                DataTable dtMachine = MySqlAccess.GetDataTable(sqlCmd.ToString(), strConnection);

                if (dtMachine.Rows.Count == 0)
                    return;

                foreach (DataRow cycleTime in dtMachine.Rows)
                {
                    DataSet_data data = list_data.Find(m => m.MchName == machine.MachineName && m.shift == int.Parse(cycleTime["shift"].ToString()));

                    if (data == null)
                    {
                        list_data.Add(CreateDataSetData(cycleTime, machine.MachineName));
                    }
                    else
                    {
                        AddToDataSetData(data, cycleTime);
                    }

                    if (long.Parse(cycleTime["cycletime"].ToString()) != 0)
                    {
                        string reasonName = cycleOn.Contains(cycleTime["status"].ToString()) ? "CYCLE ON" :
                                         cycleOff.Contains(cycleTime["status"].ToString()) ? "CYCLE OFF" :
                                         setup.Contains(cycleTime["status"].ToString()) ? "SETUP" : cycleTime["status"].ToString();

                        if (cycleTime["status"].ToString().ToUpper() == "SETUP-CYCLE ON")
                            reasonName = "SETUP";

                        list_Timeline.Add(new DataSet_timeline()
                        {
                            MchName = machine.MachineName,

                            ReasonName = reasonName,

                            cycletime = long.Parse(cycleTime["cycletime"].ToString()),
                            shift = int.Parse(cycleTime["shift"].ToString()),
                            time_ = DateTime.Parse(cycleTime["date"].ToString())
                        });

                        if (param.ShowConInSetup > 0 && cycleTime["status"].ToString().ToUpper() == "SETUP-CYCLE ON")
                            reasonName = "SETUP-CYCLE ON";

                        if (!production.Contains(reasonName))
                        {
                            DataSet_4Reasons reason = list_4Reasons.Find(r => r.MchName == machine.MachineName && r.ReasonName == reasonName);

                            if (reason == null)
                            {
                                list_4Reasons.Add(new DataSet_4Reasons()
                                {
                                    MchName = machine.MachineName,
                                    ReasonName = reasonName,
                                    CycleTime = long.Parse(cycleTime["cycletime"].ToString()),
                                    Relevance = reasonName == "SETUP-CYCLE ON" ? 0 : 1
                                });
                            }
                            else
                            {
                                reason.CycleTime += long.Parse(cycleTime["cycletime"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                Console.Write(ex.Message);
            }
        }

        private static void LoadCycles(MachineDBTable machine)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append($"SELECT                              ");
            sqlCmd.Append($"   *                                ");
            sqlCmd.Append($" FROM                               ");
            sqlCmd.Append($"   csi_database.{machine.TableName} ");
            sqlCmd.Append($" USE INDEX()                        ");
            sqlCmd.Append($" WHERE                              ");
            sqlCmd.Append($"   ShiftDate BETWEEN '{ startDate }'");
            sqlCmd.Append($"                 AND '{ endDate }'  ");
            sqlCmd.Append($" AND Shift  IN ({ param.Shift })    ");
            sqlCmd.Append($" AND Status NOT LIKE '\\_%'         ");

            try
            {
                DataTable dtMachine = MySqlAccess.GetDataTable(sqlCmd.ToString(), strConnection);

                if (dtMachine.Rows.Count == 0)
                    return;

                foreach (DataRow row in dtMachine.Rows)
                {
                    switch (param.ReportPeriod)
                    {
                        case "Today":
                        case "Yesterday":
                            ProccessDaily(row, machine.MachineName);
                            break;

                        case "Weekly":
                            ProccessWeekly(row, machine.MachineName);
                            break;

                        case "Monthly":
                            ProccessMonthly(row, machine.MachineName);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                Console.Write(ex.Message);
            }
        }


        private static void ProccessDaily(DataRow cycleTime, string machineName)
        {
            DateTime date = DateTime.Parse(cycleTime["ShiftDate"].ToString());

            string dayHist = date.ToString("dd MMM");
            string sort = date.ToString("yyyyMMdd");
            string dbStatus = cycleTime["status"].ToString();

            string rowStatus = cycleOn.Contains(dbStatus) ? "CYCLE ON" :
                               cycleOff.Contains(dbStatus) ? "CYCLE OFF" :
                               setupCon.Contains(dbStatus) ? "SETUP" : dbStatus;

            long rowCycletime = long.Parse(cycleTime["cycletime"].ToString());

            if (param.ReportPeriod == "Yesterday" && date >= param.Start)
            {
                lock (dataLock)
                {
                    DataSet_data data = list_data.Find(c => c.MchName == machineName && c.shift == int.Parse(cycleTime["shift"].ToString()));

                    if (data == null)
                    {
                        list_data.Add(CreateDataSetData(cycleTime, machineName));
                    }
                    else
                    {
                        AddToDataSetData(data, cycleTime);
                    }

                    if (rowCycletime != 0)
                    {
                        if (cycleTime["status"].ToString().ToUpper() == "SETUP-CYCLE ON")
                        {
                            if (param.ShowConInSetup > 1)
                                rowStatus = "CYCLE ON";
                            else
                                rowStatus = "SETUP";
                        }

                        int shift = int.Parse(cycleTime["shift"].ToString());

                        list_Timeline.Add(new DataSet_timeline()
                        {
                            MchName = machineName,
                            ReasonName = rowStatus,
                            cycletime = rowCycletime,
                            shift = int.Parse(cycleTime["shift"].ToString()),
                            time_ = DateTime.Parse(cycleTime["Date_"].ToString())
                        });


                        if (cycleTime["status"].ToString().ToUpper() == "SETUP-CYCLE ON")
                        {
                            switch (param.ShowConInSetup)
                            {
                                case 0:
                                    rowStatus = "SETUP";
                                    break;
                                case 1:
                                case 2:
                                    rowStatus = cycleTime["status"].ToString().ToUpper();
                                    break;
                            }
                        }

                        //if (param.ShowConInSetup > 0 && cycleTime["status"].ToString().ToUpper() == "SETUP-CYCLE ON")
                        //    rowStatus = "SETUP-CYCLE ON";

                        if (!production.Contains(rowStatus))
                        {
                            DataSet_4Reasons reason = list_4Reasons.Find(r => r.MchName == machineName && r.ReasonName == rowStatus);

                            if (reason == null)
                            {
                                list_4Reasons.Add(new DataSet_4Reasons()
                                {
                                    MchName = machineName,
                                    ReasonName = rowStatus,
                                    CycleTime = rowCycletime,
                                    Relevance = 1
                                });
                            }
                            else
                            {
                                reason.CycleTime += rowCycletime;
                            }
                        }
                    }
                }
            }

            lock (histLock)
            {
                DateTime dtCycle = DateTime.Parse(cycleTime["ShiftDate"].ToString()).Date;

                DataSet_history_daily dataHist = list_data_history_daily.Find(c => c.MchName == machineName && c.WeekNumber == dtCycle);

                if (dataHist == null)
                {
                    DataSet_history_daily data = new DataSet_history_daily();
                    data.MchName = machineName;
                    data.WeekNumber = dtCycle;
                    data.SortField = sort;
                    data.Totalcycletime = rowCycletime;
                    data.CycleOn = cycleOn.Contains(rowStatus) ? rowCycletime : 0;
                    data.CycleOff = cycleOff.Contains(rowStatus) ? rowCycletime : 0;
                    data.SumSetup = setupCon.Contains(rowStatus) && !cycleOn.Contains(rowStatus) ? rowCycletime : 0;
                    data.SumOther = !production.Contains(rowStatus) && !setupCon.Contains(rowStatus) ? rowCycletime : 0;

                    list_data_history_daily.Add(data);
                }
                else
                {
                    dataHist.Totalcycletime += rowCycletime;
                    dataHist.CycleOn += cycleOn.Contains(rowStatus) ? rowCycletime : 0;
                    dataHist.CycleOff += cycleOff.Contains(rowStatus) ? rowCycletime : 0;
                    dataHist.SumSetup += setupCon.Contains(rowStatus) && !cycleOn.Contains(rowStatus) ? rowCycletime : 0;
                    dataHist.SumOther += !production.Contains(rowStatus) && !setupCon.Contains(rowStatus) ? rowCycletime : 0;
                }
            }
        }

        private static void ProccessWeekly(DataRow cycleTime, string machineName)
        {
            DateTime date = DateTime.Parse(cycleTime["ShiftDate"].ToString());

            if (!dicValidDays.ContainsKey(date.Date))
                return;

            int dayOfWeek = (int)date.DayOfWeek;
            DateTime weekStart = date.Date.AddDays(dayOfWeek * -1);
            int relevance = 0;

            if (date >= firstDayPeriod)
            {
                lock (dataLock)
                {
                    DataSet_data data = list_data.Find(c => c.MchName == machineName);

                    if (data == null)
                    {
                        list_data.Add(CreateDataSetData(cycleTime, machineName));
                    }
                    else
                    {
                        AddToDataSetData(data, cycleTime);
                    }
                }
            }

            lock (histLock)
            {
                DateTime dtCycle = DateTime.Parse(cycleTime["ShiftDate"].ToString());
                int weekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(dtCycle, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);

                DataSet_history dataHist = list_data_history.Find(c => c.MchName == machineName && c.WeekNumber == weekNumber);

                if (date >= firstDayPeriod)
                    relevance = 1;
                else if (date >= firstDay4ReasonPeriod && date < firstDayPeriod)
                    relevance = 2;

                if (dataHist == null)
                {
                    list_data_history.Add(new DataSet_history()
                    {
                        MchName = machineName,
                        WeekStart = dicValidDays[date.Date],
                        SortField = dicValidDays[date.Date].ToString("yyyyMMdd"),
                        YearNumber = dtCycle.Year,
                        MonthNumber = dtCycle.Month,
                        WeekNumber = weekNumber,
                        Totalcycletime = long.Parse(cycleTime["cycletime"].ToString()),
                        CycleOn = cycleOn.Contains(cycleTime["status"].ToString()) ? long.Parse(cycleTime["cycletime"].ToString()) : 0,
                        CycleOff = cycleOff.Contains(cycleTime["status"].ToString()) ? long.Parse(cycleTime["cycletime"].ToString()) : 0,
                        SumSetup = setup.Contains(cycleTime["status"].ToString()) ? long.Parse(cycleTime["cycletime"].ToString()) : 0,
                        SumOther = !production.Contains(cycleTime["status"].ToString()) ? long.Parse(cycleTime["cycletime"].ToString()) : 0,
                        Relevance = relevance
                    });
                }
                else
                {
                    dataHist.Totalcycletime += long.Parse(cycleTime["cycletime"].ToString());
                    dataHist.CycleOn += cycleOn.Contains(cycleTime["status"].ToString()) ? long.Parse(cycleTime["cycletime"].ToString()) : 0;
                    dataHist.CycleOff += cycleOff.Contains(cycleTime["status"].ToString()) ? long.Parse(cycleTime["cycletime"].ToString()) : 0;
                    dataHist.SumSetup += setup.Contains(cycleTime["status"].ToString()) ? long.Parse(cycleTime["cycletime"].ToString()) : 0;
                    dataHist.SumOther += !production.Contains(cycleTime["status"].ToString()) ? long.Parse(cycleTime["cycletime"].ToString()) : 0;
                }

                if (long.Parse(cycleTime["cycletime"].ToString()) > 0 && !production.Contains(cycleTime["status"].ToString()))
                {

                    DataSet_4Reasons reason = list_4Reasons.Find(r => r.MchName == machineName && r.ReasonName == cycleTime["status"].ToString() && r.Relevance == relevance);

                    if (reason == null)
                    {
                        list_4Reasons.Add(new DataSet_4Reasons()
                        {
                            MchName = machineName,
                            ReasonName = cycleTime["status"].ToString(),
                            CycleTime = long.Parse(cycleTime["cycletime"].ToString()),
                            Relevance = relevance
                        });
                    }
                    else
                    {
                        reason.CycleTime += long.Parse(cycleTime["cycletime"].ToString());
                    }
                }

            }
        }

        private static void ProccessMonthly(DataRow cycleTime, string machineName)
        {
            DateTime date = DateTime.Parse(cycleTime["ShiftDate"].ToString());

            string dayHist = date.ToString("MMM-yy");
            string sort = date.ToString("yyyyMM");
            int relevance = 0;

            if (date >= firstDayPeriod)
            {
                lock (dataLock)
                {
                    DataSet_data data = list_data.Find(c => c.MchName == machineName);

                    if (data == null)
                    {
                        list_data.Add(CreateDataSetData(cycleTime, machineName));
                    }
                    else
                    {
                        AddToDataSetData(data, cycleTime);
                    }
                }
            }

            lock (histLock)
            {
                DataSet_history dataHist = list_data_history.Find(c => c.MchName == machineName && c.YearNumber == date.Year && c.MonthNumber == date.Month);

                if (date >= firstDayPeriod)
                    relevance = 1;
                else if (date >= firstDay4ReasonPeriod && date < firstDayPeriod)
                    relevance = 2;

                if (dataHist == null)
                {
                    list_data_history.Add(new DataSet_history()
                    {
                        MchName = machineName,
                        SortField = sort,
                        YearNumber = date.Year,
                        MonthNumber = date.Month,
                        xaxis = date.ToString("MMM yy"),
                        Totalcycletime = long.Parse(cycleTime["cycletime"].ToString()),
                        CycleOn = cycleOn.Contains(cycleTime["status"].ToString()) ? long.Parse(cycleTime["cycletime"].ToString()) : 0,
                        CycleOff = cycleOff.Contains(cycleTime["status"].ToString()) ? long.Parse(cycleTime["cycletime"].ToString()) : 0,
                        SumSetup = setup.Contains(cycleTime["status"].ToString()) ? long.Parse(cycleTime["cycletime"].ToString()) : 0,
                        SumOther = !production.Contains(cycleTime["status"].ToString()) ? long.Parse(cycleTime["cycletime"].ToString()) : 0,
                        Relevance = relevance
                    });
                }
                else
                {
                    dataHist.Totalcycletime += long.Parse(cycleTime["cycletime"].ToString());
                    dataHist.CycleOn += cycleOn.Contains(cycleTime["status"].ToString()) ? long.Parse(cycleTime["cycletime"].ToString()) : 0;
                    dataHist.CycleOff += cycleOff.Contains(cycleTime["status"].ToString()) ? long.Parse(cycleTime["cycletime"].ToString()) : 0;
                    dataHist.SumSetup += setup.Contains(cycleTime["status"].ToString()) ? long.Parse(cycleTime["cycletime"].ToString()) : 0;
                    dataHist.SumOther += !production.Contains(cycleTime["status"].ToString()) ? long.Parse(cycleTime["cycletime"].ToString()) : 0;
                }

                if (long.Parse(cycleTime["cycletime"].ToString()) > 0 && !production.Contains(cycleTime["status"].ToString()))
                {
                    DataSet_4Reasons reason = list_4Reasons.Find(r => r.MchName == machineName && r.ReasonName == cycleTime["status"].ToString() && r.Relevance == relevance);

                    if (reason == null)
                    {
                        list_4Reasons.Add(new DataSet_4Reasons()
                        {
                            MchName = machineName,
                            ReasonName = cycleTime["status"].ToString(),
                            CycleTime = long.Parse(cycleTime["cycletime"].ToString()),
                            Relevance = relevance
                        });
                    }
                    else
                    {
                        reason.CycleTime += long.Parse(cycleTime["cycletime"].ToString());
                    }
                }
            }
        }


        public static List<DataSet_data> GetDataSet_Data()
        {
            return list_data;
        }

        public static List<DataSet_history> GetDataSet_History()
        {
            return list_data_history.OrderBy(h => h.SortField).ToList();
        }

        public static List<DataSet_history_daily> GetDataSet_History_Daily()
        {
            return list_data_history_daily.OrderBy(h => h.SortField).ToList();
        }

        public static List<DataSet_timeline> GetDataSet_Timeline()
        {
            return list_Timeline;
        }

        public static List<DataSet_4Reasons> GetDataSet_4Reasons()
        {
            return list_4Reasons.OrderByDescending(r => r.CycleTime).ToList();
        }

        private static DataSet_data CreateDataSetData(DataRow cycleTime, string machineName)
        {
            long rowCycletime = long.Parse(cycleTime["cycletime"].ToString());
            string status = cycleTime["status"].ToString();

            return new DataSet_data()
            {
                MchName = machineName,
                Totalcycletime = rowCycletime,
                CycleOn = cycleOn.Contains(status) ? rowCycletime : 0,
                CycleOff = cycleOff.Contains(status) ? rowCycletime : 0,
                SumSetup = setupCon.Contains(status) && !production.Contains(status) ? rowCycletime : 0,
                SumOther = !production.Contains(status) && !setupCon.Contains(status) ? rowCycletime : 0,
                shift = int.Parse(cycleTime["shift"].ToString()),
                StatusExpr = "SETUP"
            };
        }

        private static void AddToDataSetData(DataSet_data data, DataRow cycleTime)
        {
            long rowCycletime = long.Parse(cycleTime["cycletime"].ToString());
            string status = cycleTime["status"].ToString();

            data.Totalcycletime += rowCycletime;
            //data.CycleOn += cycleOn.Contains(status) ? rowCycletime : 0;
            //data.CycleOff += cycleOff.Contains(status) ? rowCycletime : 0;
            //data.SumSetup += setupCon.Contains(status) && !production.Contains(status) ? rowCycletime : 0;
            //data.SumSetup += "SETUP;_SETUP;SETUP-CYCLE ON".Contains("CYCLE ON") && !"CYCLE ON;_CON;CYCLE OFF;_COFF;SETUP;_SETUP".Contains("SETUP-CYCLE ON") ? rowCycletime : 0;
            //data.SumOther += !production.Contains(status) && !setupCon.Contains(status) ? rowCycletime : 0;

            if (status == "CYCLE ON")
                data.CycleOn += rowCycletime;

            if (status == "CYCLE OFF")
                data.CycleOff += rowCycletime;

            if (status == "SETUP" || status == "SETUP-CYCLE ON")
                data.SumSetup += rowCycletime;

            if (!production.Contains(status) && status != "SETUP-CYCLE ON")
                data.SumOther += rowCycletime;
        }
    }

    public class DataSet_data
    {
        public int MchId { get; set; }

        public string MchName { get; set; }

        public long Totalcycletime { get; set; }

        public long CycleOn { get; set; }

        public long CycleOff { get; set; }

        public long SumSetup { get; set; }

        public long SumOther { get; set; }

        public string StatusExpr { get; set; }

        public int shift { get; set; }
    }

    public class DataSet_timeline
    {
        public string MchName { get; set; }

        public string ReasonName { get; set; }

        public DateTime time_ { get; set; }

        public int shift { get; set; }

        public long cycletime { get; set; }
    }

    public class DataSet_history
    {
        public string MchName { get; set; }

        public long Totalcycletime { get; set; }

        public long CycleOn { get; set; }

        public long CycleOff { get; set; }

        public long SumSetup { get; set; }

        public long SumOther { get; set; }

        public string StatusExpr { get; set; }

        public int WeekNumber { get; set; }

        public DateTime WeekStart { get; set; }

        public int YearNumber { get; set; }

        public int MonthNumber { get; set; }

        public string xaxis { get; set; }

        public string SortField { get; set; }

        public int Relevance { get; set; }
    }

    public class DataSet_history_daily
    {
        public string MchName { get; set; }

        public long Totalcycletime { get; set; }

        public long CycleOn { get; set; }

        public long CycleOff { get; set; }

        public long SumSetup { get; set; }

        public long SumOther { get; set; }

        public string StatusExpr { get; set; }

        public DateTime WeekNumber { get; set; }

        public DateTime WeekStart { get; set; }

        public int YearNumber { get; set; }

        public int MonthNumber { get; set; }

        public string xaxis { get; set; }

        public string SortField { get; set; }
    }

    public class DataSet_PartNo
    {
        public string MchName { get; set; }

        public string partName { get; set; }

        public int shift { get; set; }

        public DateTime date_ { get; set; }
    }

    public class DataSet_4Reasons
    {
        public string MchName { get; set; }

        public string ReasonName { get; set; }

        public long CycleTime { get; set; }

        public int Relevance { get; set; }
    }
}
