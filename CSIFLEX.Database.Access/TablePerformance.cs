using CSIFLEX.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CSIFLEX.Database.Access
{
    public class TablePerformance
    {
        public TablePerformance()
        {

        }


        public void TruncateTempTable(string enetMachineName)
        {
            string machineTableName = MachineDbTableName(enetMachineName);
            MySqlAccess.ExecuteNonQuery($"TRUNCATE TABLE CSI_machineperf.{ machineTableName };");
        }


        public void UpdateTempTable(string enetMachineName, List<EnetData> lines)
        {
            string machineTableName = MachineDbTableName(enetMachineName);

            StringBuilder sqlCmd = new StringBuilder();

            foreach(EnetData line in lines)
            {
                int timeId = (int)line.EventDateTime.Subtract(new DateTime(2000, 1, 1)).TotalSeconds;

                if (!line.Status.StartsWith("_"))
                {
                    sqlCmd.Append($"INSERT IGNORE INTO        ");
                    sqlCmd.Append($"    CSI_machineperf.{ machineTableName } ");
                    sqlCmd.Append($" (                        ");
                    sqlCmd.Append($"    status              , ");
                    sqlCmd.Append($"    time                , ");
                    sqlCmd.Append($"    cycletime           , ");
                    sqlCmd.Append($"    shift               , ");
                    sqlCmd.Append($"    date                , ");
                    sqlCmd.Append($"    No_of_Head_Pallet     ");
                    sqlCmd.Append($" )                        ");
                    sqlCmd.Append($" VALUES                   ");
                    sqlCmd.Append($" (                        ");
                    sqlCmd.Append($"    '{ line.Status }'   , ");
                    sqlCmd.Append($"     { timeId }         , ");
                    sqlCmd.Append($"     0                  , ");
                    sqlCmd.Append($"     { line.Shift }     , ");
                    sqlCmd.Append($"    '{ line.EventDateTime.ToString("yyyy-MM-dd HH:mm:ss") }', ");
                    sqlCmd.Append($"     { line.HeadPallet }  ");
                    sqlCmd.Append($" );                       ");
                }
            }
            try
            {
                MySqlAccess.ExecuteNonQuery(sqlCmd.ToString());
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }

            sqlCmd.Clear();
            int lastTimeId = 0;
            int currTimeId = 0;
            int cycleTime = 0;
            var records = MySqlAccess.GetDataTable($"SELECT * FROM CSI_machineperf.{ machineTableName } WHERE cycletime = 0 AND No_of_Head_Pallet IN (0,1) ORDER BY time DESC");
            foreach (DataRow row in records.Rows)
            {
                currTimeId = int.Parse(row["time"].ToString());
                if (lastTimeId != 0)
                {
                    cycleTime = lastTimeId - currTimeId;
                    sqlCmd.Append($"UPDATE CSI_machineperf.{ machineTableName } SET cycletime = { cycleTime } WHERE time = { currTimeId }; ");
                }
                lastTimeId = currTimeId;
            }
            try
            {
                MySqlAccess.ExecuteNonQuery(sqlCmd.ToString());
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }
    
    
        public void UpdateMachineTable(string enetMachineName, List<EnetData> lines)
        {
            string machineTableName = MachineDbTableName(enetMachineName);

            StringBuilder sqlCmd = new StringBuilder();

            int count = 0;
            
            try
            {
                foreach(EnetData line in lines)
                {
                    if (!line.Status.StartsWith("_") && (line.HeadPallet == 0 || line.HeadPallet == 1))
                    {
                        sqlCmd.Append($"INSERT IGNORE INTO ");
                        sqlCmd.Append($"    CSI_Database.{machineTableName}");
                        sqlCmd.Append($" (              ");
                        sqlCmd.Append($"    month_    , ");
                        sqlCmd.Append($"    day_      , ");
                        sqlCmd.Append($"    year_     , ");
                        sqlCmd.Append($"    time_     , ");
                        sqlCmd.Append($"    date_     , ");
                        sqlCmd.Append($"    status    , ");
                        sqlCmd.Append($"    shift     , ");
                        sqlCmd.Append($"    cycletime   ");
                        sqlCmd.Append($" )              ");
                        sqlCmd.Append($" VALUES         ");
                        sqlCmd.Append($" (              ");
                        sqlCmd.Append($"    { line.EventDateTime.Month }    , ");
                        sqlCmd.Append($"    { line.EventDateTime.Day }      , ");
                        sqlCmd.Append($"    { line.EventDateTime.Year }     , ");
                        sqlCmd.Append($"   '{ line.EventDateTime.ToString("yyyy-MM-dd HH:mm:ss") }', ");
                        sqlCmd.Append($"   '{ line.EventDateTime.ToString("yyyy-MM-dd HH:mm:ss") }', ");
                        sqlCmd.Append($"   '{ line.Status }'                , ");
                        sqlCmd.Append($"    { line.Shift }                  , ");
                        sqlCmd.Append($"    { line.cycleTime }                ");
                        sqlCmd.Append($" );             ");
                        count++;
                    }

                    if(count >= 10)
                    {
                        MySqlAccess.ExecuteNonQuery(sqlCmd.ToString());
                        count = 0;
                        sqlCmd.Clear();
                    }
                }
                if (count > 0)
                {
                    MySqlAccess.ExecuteNonQuery(sqlCmd.ToString());
                }

                sqlCmd.Clear();
                int lastTimeId = 0;
                int currTimeId = 0;
                int cycleTime = 0;
                var records = MySqlAccess.GetDataTable($"SELECT * FROM CSI_Database.{ machineTableName } WHERE cycletime = 0 ORDER BY time_ DESC");
                foreach (DataRow row in records.Rows)
                {
                    DateTime currDate = DateTime.Parse(row["time_"].ToString());
                    currTimeId = (int)currDate.Subtract(new DateTime(2000, 1, 1)).TotalSeconds;

                    if (lastTimeId != 0)
                    {
                        cycleTime = lastTimeId - currTimeId;
                        sqlCmd.Append($"UPDATE CSI_Database.{ machineTableName } SET cycletime = { cycleTime } WHERE time_ = '{ currDate.ToString("yyyy-MM-dd HH:mm:ss") }'; ");
                    }
                    lastTimeId = currTimeId;
                }
                
                MySqlAccess.ExecuteNonQuery(sqlCmd.ToString());
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }


        public void CalcPerformance()
        {
            MySqlAccess.ExecuteNonQuery($"TRUNCATE TABLE CSI_machineperf.tbl_perf;");

            CalcPerformanceMachines();
            CalcPerformanceGroups();
        }


        private void CalcPerformanceMachines()
        {
            //List<MachineConfig> enetMachines = eNetServer.MonitoredMachines;

            DataTable enetMachines = MySqlAccess.GetDataTable($"SELECT Id, EnetMachineName FROM csi_auth.tbl_ehub_conf WHERE Monstate = 1;");

            DateTime startWeek = DateTime.Today.Date.AddDays(-(int)DateTime.Today.DayOfWeek);
            DateTime startMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime startDate = startMonth;
            if (startWeek < startMonth) startDate = startWeek;

            foreach (DataRow enetMachine in enetMachines.Rows)
            {
                int enetMachineId = int.Parse(enetMachine["Id"].ToString());
                string enetMachineName = enetMachine["EnetMachineName"].ToString();
                string machineTableName = MachineDbTableName(enetMachineName);

                List<StatusChange> statusChangesMonth = new List<StatusChange>();
                List<StatusChange> statusChangesWeek = new List<StatusChange>();

                try
                {
                    StringBuilder sqlCmd = new StringBuilder();
                    sqlCmd.Append($"SELECT                                             ");
                    sqlCmd.Append($"    Date_ 		AS Date     ,                      ");
                    sqlCmd.Append($"    shift		AS Shift    ,                      ");
                    sqlCmd.Append($"    status		AS Status   ,                      ");
                    sqlCmd.Append($"    cycletime	AS Cycletime                       ");
                    sqlCmd.Append($" FROM                                              ");
                    sqlCmd.Append($"    csi_database.{ machineTableName }              ");
                    sqlCmd.Append($" WHERE                                             ");
                    sqlCmd.Append($"    Date_ >= '{startMonth.ToString("yyyy-MM-dd")}' ");

                    sqlCmd.Append($"UNION                                              ");

                    sqlCmd.Append($"SELECT                                             ");
                    sqlCmd.Append($"    A.Date   	AS Date     ,                      ");
                    sqlCmd.Append($"    A.shift		AS Shift    ,                      ");
                    sqlCmd.Append($"    A.status	AS Status   ,                      ");
                    sqlCmd.Append($"    A.cycletime	AS Cycletime                       ");
                    sqlCmd.Append($" FROM                                              ");
                    sqlCmd.Append($"    csi_machineperf.{ machineTableName } AS A      ");
                    sqlCmd.Append($"    INNER JOIN                                     ");
                    sqlCmd.Append($"    (                                              ");
                    sqlCmd.Append($"        SELECT MAX(shift) shift FROM csi_machineperf.{ machineTableName }");
                    sqlCmd.Append($"    ) AS B                                         ");
                    sqlCmd.Append($"    ON A.shift = B.shift;                          ");

                    //DataTable dbChanges = MySqlAccess.GetDataTable($"SELECT * FROM csi_database.{ machineTableName } WHERE Date_ >= '{startMonth.ToString("yyyy-MM-dd")}' AND status NOT LIKE '\\_';");
                    DataTable dbChanges = MySqlAccess.GetDataTable(sqlCmd.ToString());

                    foreach (DataRow row in dbChanges.Rows)
                    {
                        DateTime changeDate = DateTime.Parse(row["Date"].ToString()).Date;

                        if (!row["status"].ToString().StartsWith("_"))
                        {
                            if (changeDate >= startMonth)
                            {
                                statusChangesMonth.Add(new StatusChange()
                                {
                                    status = row["status"].ToString(),
                                    cycletime = int.Parse(row["cycletime"].ToString())
                                });
                            }

                            if (changeDate >= startWeek)
                            {
                                statusChangesWeek.Add(new StatusChange()
                                {
                                    status = row["status"].ToString(),
                                    cycletime = int.Parse(row["cycletime"].ToString())
                                });
                            }
                        }
                    }

                    double totWeek = statusChangesWeek.Sum(s => s.cycletime);
                    double totWeekCON = statusChangesWeek.Where(s => s.status == "CYCLE ON").Sum(s => s.cycletime);
                    double totWeekCOFF = statusChangesWeek.Where(s => s.status == "CYCLE OFF").Sum(s => s.cycletime);
                    double totWeekSetup = statusChangesWeek.Where(s => s.status == "SETUP").Sum(s => s.cycletime);
                    double totWeekOthers = totWeek - totWeekCON - totWeekCOFF - totWeekSetup;

                    List<StatusChange> statusWeek = new List<StatusChange>();
                    statusWeek.Add(new StatusChange()
                    {
                        status = "CYCLE ON",
                        cycletime = totWeek > 0 ? (totWeekCON / totWeek) * 100 : 0
                    });
                    statusWeek.Add(new StatusChange()
                    {
                        status = "CYCLE OFF",
                        cycletime = totWeek > 0 ? (totWeekCOFF / totWeek) * 100 : 0
                    });
                    statusWeek.Add(new StatusChange()
                    {
                        status = "SETUP",
                        cycletime = totWeek > 0 ? (totWeekSetup / totWeek) * 100 : 0
                    });
                    statusWeek.Add(new StatusChange()
                    {
                        status = "OTHER",
                        cycletime = totWeek > 0 ? (totWeekOthers / totWeek) * 100 : 0
                    });

                    double totMonth = statusChangesMonth.Sum(s => s.cycletime);
                    double totMonthCON = statusChangesMonth.Where(s => s.status == "CYCLE ON").Sum(s => s.cycletime);
                    double totMonthCOFF = statusChangesMonth.Where(s => s.status == "CYCLE OFF").Sum(s => s.cycletime);
                    double totMonthSetup = statusChangesMonth.Where(s => s.status == "SETUP").Sum(s => s.cycletime);
                    double totMonthOthers = totMonth - totMonthCON - totMonthCOFF - totMonthSetup;

                    List<StatusChange> statusMonth = new List<StatusChange>();
                    statusMonth.Add(new StatusChange()
                    {
                        status = "CYCLE ON",
                        cycletime = totMonth > 0 ? (totMonthCON / totMonth) * 100 : 0
                    });
                    statusMonth.Add(new StatusChange()
                    {
                        status = "CYCLE OFF",
                        cycletime = totMonth > 0 ? (totMonthCOFF / totMonth) * 100 : 0
                    });
                    statusMonth.Add(new StatusChange()
                    {
                        status = "SETUP",
                        cycletime = totMonth > 0 ? (totMonthSetup / totMonth) * 100 : 0
                    });
                    statusMonth.Add(new StatusChange()
                    {
                        status = "OTHER",
                        cycletime = totMonth > 0 ? (totMonthOthers / totMonth) * 100 : 0
                    });

                    string perfWeek = JsonConvert.SerializeObject(statusWeek);
                    string perfMonth = JsonConvert.SerializeObject(statusMonth);

                    sqlCmd.Clear();
                    sqlCmd.Append($"INSERT INTO                     ");
                    sqlCmd.Append($"    csi_machineperf.tbl_perf    ");
                    sqlCmd.Append($"    (                           ");
                    sqlCmd.Append($"        machinename_,           ");
                    sqlCmd.Append($"        weekly_     ,           ");
                    sqlCmd.Append($"        monthly_                ");
                    sqlCmd.Append($"    )                           ");
                    sqlCmd.Append($"    VALUES                      ");
                    sqlCmd.Append($"    (                           ");
                    sqlCmd.Append($"        '{enetMachineName}'   , ");
                    sqlCmd.Append($"        '{perfWeek}'          , ");
                    sqlCmd.Append($"        '{perfMonth}'           ");
                    sqlCmd.Append($"    )                           ");
                    sqlCmd.Append($"ON DUPLICATE KEY UPDATE         ");
                    sqlCmd.Append($"    weekly_ = '{perfWeek}'  ,   ");
                    sqlCmd.Append($"    monthly_ = '{perfMonth}';   ");

                    MySqlAccess.ExecuteNonQuery(sqlCmd.ToString());
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }
        }
    
    
        private void CalcPerformanceGroups()
        {
            StringBuilder sqlCmd = new StringBuilder();
            sqlCmd.Append($"SELECT                                       ");
            sqlCmd.Append($"    G.groups            AS GroupName       , ");
            sqlCmd.Append($"    G.machines          AS MachineName     , ");
            sqlCmd.Append($"    G.machineId         AS MachineId       , ");
            sqlCmd.Append($"    T.EnetMachineName   AS EnetMachineName , ");
            sqlCmd.Append($"    T.MCH_WeeklyTarget  AS WeeklyTarget    , ");
            sqlCmd.Append($"    T.MCH_MonthlyTarget AS MonthlyTarget     ");
            sqlCmd.Append($"FROM                                         ");
            sqlCmd.Append($"    csi_database.tbl_groups G                ");
            sqlCmd.Append($"    INNER JOIN csi_auth.tbl_ehub_conf T      ");
            sqlCmd.Append($"        ON G.machineId = T.id                ");
            sqlCmd.Append($"       AND T.Monstate  = 1            ;      ");

            DataTable dbGroups = MySqlAccess.GetDataTable(sqlCmd.ToString());
            
            Dictionary<string, List<MachineTarget>> groups = new Dictionary<string, List<MachineTarget>>();

            foreach(DataRow row in dbGroups.Rows)
            {
                string group = row["GroupName"].ToString();
                string machine = row["EnetMachineName"].ToString();

                if (!string.IsNullOrEmpty(machine))
                {
                    if (!groups.ContainsKey(group))
                        groups.Add(group, new List<MachineTarget>());

                    var mch = groups[group].FirstOrDefault(m => m.MachineName == machine);
                    if (mch == null)
                        groups[group].Add(new MachineTarget()
                        {
                            MachineName = machine,
                            WeeklyTarget = int.Parse(row["WeeklyTarget"].ToString()),
                            MonthlyTarget = int.Parse(row["MonthlyTarget"].ToString())
                        }) ;
                }
            }

            DateTime startWeek = DateTime.Today.Date.AddDays(-(int)DateTime.Today.DayOfWeek);
            DateTime startMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime startDate = startMonth;
            if (startWeek < startMonth) startDate = startWeek;

            foreach (KeyValuePair<string, List<MachineTarget>> group in groups)
            {
                List<StatusChange> statusChangesMonth = new List<StatusChange>();
                List<StatusChange> statusChangesWeek = new List<StatusChange>();

                foreach(MachineTarget machTarget in group.Value)
                {
                    string machineTableName = MachineDbTableName(machTarget.MachineName);

                    DataTable dbChanges = MySqlAccess.GetDataTable($"SELECT * FROM csi_database.{machineTableName} WHERE Date_ >= '{startMonth.ToString("yyyy-MM-dd")}' AND status NOT LIKE '\\_';");

                    foreach (DataRow row in dbChanges.Rows)
                    {
                        DateTime changeDate = DateTime.Parse(row["Date_"].ToString()).Date;

                        if (changeDate >= startMonth)
                        {
                            statusChangesMonth.Add(new StatusChange()
                            {
                                status = row["status"].ToString(),
                                cycletime = int.Parse(row["cycletime"].ToString())
                            });
                        }

                        if (changeDate >= startWeek)
                        {
                            statusChangesWeek.Add(new StatusChange()
                            {
                                status = row["status"].ToString(),
                                cycletime = int.Parse(row["cycletime"].ToString())
                            });
                        }
                    }
                }

                double totWeek = statusChangesWeek.Sum(s => s.cycletime);
                double totWeekCON = statusChangesWeek.Where(s => s.status == "CYCLE ON").Sum(s => s.cycletime);
                double totWeekCOFF = statusChangesWeek.Where(s => s.status == "CYCLE OFF").Sum(s => s.cycletime);
                double totWeekSetup = statusChangesWeek.Where(s => s.status == "SETUP").Sum(s => s.cycletime);
                double totWeekOthers = totWeek - totWeekCON - totWeekCOFF - totWeekSetup;
                double totHrsWeekCON = totWeekCON / 3600;

                List<StatusChange> statusWeek = new List<StatusChange>();
                statusWeek.Add(new StatusChange()
                {
                    status = "CYCLE ON",
                    cycletime = totWeek > 0 ? (totWeekCON / totWeek) * 100 : 0
                });
                statusWeek.Add(new StatusChange()
                {
                    status = "CYCLE OFF",
                    cycletime = totWeek > 0 ? (totWeekCOFF / totWeek) * 100 : 0
                });
                statusWeek.Add(new StatusChange()
                {
                    status = "SETUP",
                    cycletime = totWeek > 0 ? (totWeekSetup / totWeek) * 100 : 0
                });
                statusWeek.Add(new StatusChange()
                {
                    status = "OTHER",
                    cycletime = totWeek > 0 ? (totWeekOthers / totWeek) * 100 : 0
                });

                List<StatusChange> statusHrsWeek = new List<StatusChange>();
                statusHrsWeek.Add(new StatusChange()
                {
                    status = "CYCLE ON",
                    cycletime = totHrsWeekCON
                });
                statusHrsWeek.Add(new StatusChange()
                {
                    status = "CYCLE OFF",
                    cycletime = (totWeekCOFF + totWeekSetup + totWeekOthers) / 3600
                });
                statusHrsWeek.Add(new StatusChange()
                {
                    status = "SETUP",
                    cycletime = 0
                });
                statusHrsWeek.Add(new StatusChange()
                {
                    status = "OTHER",
                    cycletime = 0
                });

                double totMonth = statusChangesMonth.Sum(s => s.cycletime);
                double totMonthCON = statusChangesMonth.Where(s => s.status == "CYCLE ON").Sum(s => s.cycletime);
                double totMonthCOFF = statusChangesMonth.Where(s => s.status == "CYCLE OFF").Sum(s => s.cycletime);
                double totMonthSetup = statusChangesMonth.Where(s => s.status == "SETUP").Sum(s => s.cycletime);
                double totMonthOthers = totMonth - totMonthCON - totMonthCOFF - totMonthSetup;
                double totHrsMonthCON = totMonthCON / 3600;

                List<StatusChange> statusMonth = new List<StatusChange>();
                statusMonth.Add(new StatusChange()
                {
                    status = "CYCLE ON",
                    cycletime = totMonth > 0 ? (totMonthCON / totMonth) * 100 : 0
                });
                statusMonth.Add(new StatusChange()
                {
                    status = "CYCLE OFF",
                    cycletime = totMonth > 0 ? (totMonthCOFF / totMonth) * 100 : 0
                });
                statusMonth.Add(new StatusChange()
                {
                    status = "SETUP",
                    cycletime = totMonth > 0 ? (totMonthSetup / totMonth) * 100 : 0
                });
                statusMonth.Add(new StatusChange()
                {
                    status = "OTHER",
                    cycletime = totMonth > 0 ? (totMonthOthers / totMonth) * 100 : 0
                });

                List<StatusChange> statusHrsMonth = new List<StatusChange>();
                statusHrsMonth.Add(new StatusChange()
                {
                    status = "CYCLE ON",
                    cycletime = totHrsMonthCON
                });
                statusHrsMonth.Add(new StatusChange()
                {
                    status = "CYCLE OFF",
                    cycletime = (totMonthCOFF + totMonthSetup + totMonthOthers) / 3600
                });
                statusHrsMonth.Add(new StatusChange()
                {
                    status = "SETUP",
                    cycletime = 0
                });
                statusHrsMonth.Add(new StatusChange()
                {
                    status = "OTHER",
                    cycletime = 0
                });

                string perfWeek = JsonConvert.SerializeObject(statusWeek);
                string perfMonth = JsonConvert.SerializeObject(statusMonth);

                string perfHrsWeek = JsonConvert.SerializeObject(statusHrsWeek);
                string perfHrsMonth = JsonConvert.SerializeObject(statusHrsMonth);

                int weekTarget = group.Value.Sum(t => t.WeeklyTarget);
                int monthTarget = group.Value.Sum(t => t.MonthlyTarget);

                sqlCmd.Clear();
                sqlCmd.Append($"INSERT INTO                          ");
                sqlCmd.Append($"    csi_machineperf.tbl_perf         ");
                sqlCmd.Append($"    (                                ");
                sqlCmd.Append($"        machinename_,                ");
                sqlCmd.Append($"        weekly_     ,                ");
                sqlCmd.Append($"        monthly_                     ");
                sqlCmd.Append($"    )                                ");
                sqlCmd.Append($"    VALUES                           ");
                sqlCmd.Append($"    (                                ");
                sqlCmd.Append($"        'Grp_{group.Key}' ,          ");
                sqlCmd.Append($"        '{perfWeek}'               , ");
                sqlCmd.Append($"        '{perfMonth}'                ");
                sqlCmd.Append($"    )                                ");
                sqlCmd.Append($"ON DUPLICATE KEY UPDATE              ");
                sqlCmd.Append($"    weekly_ = '{perfWeek}'  ,        ");
                sqlCmd.Append($"    monthly_ = '{perfMonth}';        ");

                sqlCmd.Append($"INSERT INTO                          ");
                sqlCmd.Append($"    csi_machineperf.tbl_perf         ");
                sqlCmd.Append($"    (                                ");
                sqlCmd.Append($"        machinename_,                ");
                sqlCmd.Append($"        weekly_     ,                ");
                sqlCmd.Append($"        monthly_                     ");
                sqlCmd.Append($"    )                                ");
                sqlCmd.Append($"    VALUES                           ");
                sqlCmd.Append($"    (                                ");
                sqlCmd.Append($"        'Sec_Grp_{group.Key}' ,      ");
                sqlCmd.Append($"        '{perfHrsWeek}'            , ");
                sqlCmd.Append($"        '{perfHrsMonth}'             ");
                sqlCmd.Append($"    )                                ");
                sqlCmd.Append($"ON DUPLICATE KEY UPDATE              ");
                sqlCmd.Append($"    weekly_ = '{perfHrsWeek}'  ,     ");
                sqlCmd.Append($"    monthly_ = '{perfHrsMonth}';     ");

                sqlCmd.Append($"INSERT INTO                          ");
                sqlCmd.Append($"    csi_machineperf.tbl_perf         ");
                sqlCmd.Append($"    (                                ");
                sqlCmd.Append($"        machinename_,                ");
                sqlCmd.Append($"        weekly_     ,                ");
                sqlCmd.Append($"        monthly_                     ");
                sqlCmd.Append($"    )                                ");
                sqlCmd.Append($"    VALUES                           ");
                sqlCmd.Append($"    (                                ");
                sqlCmd.Append($"       'TitleTarget_Grp_{group.Key}',");
                sqlCmd.Append($"       'Grp_{group.Key} {(int)totHrsWeekCON}h of {weekTarget}h'  , ");
                sqlCmd.Append($"       'Grp_{group.Key} {(int)totHrsMonthCON}h of {monthTarget}h'  ");
                sqlCmd.Append($"    )                                ");
                sqlCmd.Append($"ON DUPLICATE KEY UPDATE              ");
                sqlCmd.Append($"    weekly_  = 'Grp_{group.Key} {(int)totHrsWeekCON}h of {weekTarget}h',   ");
                sqlCmd.Append($"    monthly_ = 'Grp_{group.Key} {(int)totHrsMonthCON}h of {monthTarget}h'; ");

                MySqlAccess.ExecuteNonQuery(sqlCmd.ToString());
            }
        }


        private string MachineDbTableName(string machineName)
        {
            StringBuilder tableName = new StringBuilder();
            tableName.Append("tbl_");

            foreach (char ch in machineName)
            {
                int cod = ch;

                if ((cod >= 32 && cod <= 47) || (cod >= 58 && cod <= 64) || (cod >= 91 && cod <= 96 && cod != 95) || (cod >= 123 && cod <= 126))
                    tableName.Append($"_c{cod}_");
                else
                    tableName.Append(ch);
            }
            return tableName.ToString();
        }
    }

    public class EnetData
    {
        public int MachineId { get; set; }

        public int EnetMachineId { get; set; }

        public string EnetMachineName { get; set; }

        public DateTime EventDateTime { get; set; }

        public int Shift { get; set; }

        public DateTime ShiftDate { get; set; }

        public string Status { get; set; }

        public double cycleTime { get; set; }

        public int HeadPallet { get; set; }

        public string PartNr { get; set; }

        public string Operator { get; set; }

        public string Comment { get; set; }

        public string Raw { get; set; }
    }

    public class StatusChange
    {
        public string status { get; set; }

        public double cycletime { get; set; }
    }

    public class MachineTarget
    {
        public string MachineName { get; set; }

        public int WeeklyTarget { get; set; }

        public int MonthlyTarget { get; set; }
    }
}
