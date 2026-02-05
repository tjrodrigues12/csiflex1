using CSIFLEX.Database.Access;
using CSIFLEX.eNetLibrary;
using CSIFLEX.eNetLibrary.Data;
using CSIFLEX.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFLEX.Server.Library
{
    public static class eNETHistory
    {
        private static List<CsvMachineModel> allMachHistory;

        public static void StartEnetLoadOeeFile(string year)
        {
            Task.Run(() => LoadEnetOeeFile(year));
        }

        private static void LoadEnetOeeFile(string year)
        {
            string eNetfile = Path.Combine(eNetServer.EnetReportsFolder, $"_OEE_{year}.CSV");

            if (!File.Exists(eNetfile))
            {
                return;
            }

            StringBuilder sqlCmd = new StringBuilder();
            sqlCmd.Append($"TRUNCATE csi_database.tempOee;  ");
            sqlCmd.Append($"LOAD DATA INFILE '{eNetfile}'   ");
            sqlCmd.Append($"INTO TABLE csi_database.tempOee ");
            sqlCmd.Append($"FIELDS TERMINATED BY ','        ");
            sqlCmd.Append($"LINES TERMINATED BY '\n'        ");
            sqlCmd.Append($"IGNORE 1 ROWS;                  ");

            try
            {
                MySqlAccess.ExecuteNonQuery(sqlCmd.ToString());

                sqlCmd.Clear();
                sqlCmd.Append($"REPLACE INTO CSI_Database.tbl_oee ");
                sqlCmd.Append($"(                          ");
                sqlCmd.Append($"  MACHINE    ,             ");
                sqlCmd.Append($"  trx_time   ,             ");
                sqlCmd.Append($"  Avail      ,             ");
                sqlCmd.Append($"  Performance,             ");
                sqlCmd.Append($"  Quality    ,             ");
                sqlCmd.Append($"  OEE        ,             ");
                sqlCmd.Append($"  HEADPALLET               ");
                sqlCmd.Append($")                          ");
                sqlCmd.Append($"  VALUES                   ");
                sqlCmd.Append($"(                          ");
                sqlCmd.Append($"  SELECT                   ");
                sqlCmd.Append($"      MachineName  ,       ");
                sqlCmd.Append($"      Time         ,       ");
                sqlCmd.Append($"      Availability ,       ");
                sqlCmd.Append($"      Performance  ,       ");
                sqlCmd.Append($"      Quality      ,       ");
                sqlCmd.Append($"      OEE          ,       ");
                sqlCmd.Append($"      HEADPALLET           ");
                sqlCmd.Append($"  FROM                     ");
                sqlCmd.Append($"      csi_database.tempOee ");
                sqlCmd.Append($")                          ");

                MySqlAccess.ExecuteNonQuery(sqlCmd.ToString());
            }
            catch (Exception ex){
                Console.Write(ex.Message);
            }
        }

        public static void StartLoadEnetMachineFile(string year)
        {
            Task.Run(() => LoadEnetMachineFile(year));
        }

        private static void LoadEnetMachineFile(string year)
        {
            string eNetfile = Path.Combine(eNetServer.EnetReportsFolder, $"_MACHINE_{year}.CSV");

            if (!File.Exists(eNetfile))
            {
                return;
            }

            StringBuilder sqlCmd = new StringBuilder();
            sqlCmd.Append($"TRUNCATE csi_database.tempMachine;  ");
            sqlCmd.Append($"LOAD DATA LOCAL INFILE '{eNetfile}'       ");
            sqlCmd.Append($"INTO TABLE csi_database.tempMachine ");
            sqlCmd.Append($"FIELDS TERMINATED BY ','            ");
            sqlCmd.Append($"LINES TERMINATED BY '\n'            ");
            sqlCmd.Append($"IGNORE 1 ROWS                       ");
            sqlCmd.Append($"(MachineName, @Date_, Shift, StartTime, EndTime, ElapsedTime, @MachineStatus, HeadPallet, @Comment) ");
            sqlCmd.Append($"SET Date_ = STR_TO_DATE(@Date_, '%m/%d/%Y'),                                                         ");
            sqlCmd.Append($"    MachineStatus = If(@MachineStatus <> '_PARTNUMBER', @MachineStatus, CONCAT('_PARTNO:', SUBSTRING_INDEX(@Comment, ';', 1))), ");
            sqlCmd.Append($"    Comment = @Comment;             ");

            try
            {
                MySqlAccess.ExecuteNonQuery(sqlCmd.ToString());

                var machines = MySqlAccess.GetDataTable("SELECT MachineName FROM csi_database.tempMachine GROUP BY MachineName");

                foreach(DataRow machine in machines.Rows)
                {
                    string machineName = machine["MachineName"].ToString();
                    string renamedMachine = RenameMachine(machine["MachineName"].ToString());

                    MySqlAccess.Validate_Database_Machine_Table(machineName);

                    MySqlQueries.InsertRenameMachine(machineName);

                    sqlCmd.Clear();
                    sqlCmd.Append($"INSERT IGNORE INTO             ");
                    sqlCmd.Append($"    CSI_Database.tbl_{renamedMachine} ");
                    sqlCmd.Append($"(                              ");
                    sqlCmd.Append($"    month_    ,                ");
                    sqlCmd.Append($"    day_      ,                ");
                    sqlCmd.Append($"    year_     ,                ");
                    sqlCmd.Append($"    time_     ,                ");
                    sqlCmd.Append($"    date_     ,                ");
                    sqlCmd.Append($"    status    ,                ");
                    sqlCmd.Append($"    shift     ,                ");
                    sqlCmd.Append($"    cycletime ,                ");
                    sqlCmd.Append($"    partnumber                 ");
                    sqlCmd.Append($")                              ");
                    sqlCmd.Append($"  SELECT                       ");
                    sqlCmd.Append($"       MONTH(Date_) as month_ ,");
                    sqlCmd.Append($"       DAY(Date_)   as day_   ,");
                    sqlCmd.Append($"       YEAR(Date_)  as year_  ,");
                    sqlCmd.Append($"       ADDTIME(Date_, StartTime) as time_,");
                    sqlCmd.Append($"       ADDTIME(Date_, StartTime) as date_,");
                    sqlCmd.Append($"       MachineStatus,                ");
                    sqlCmd.Append($"       Shift        ,                ");
                    sqlCmd.Append($"       ElapsedTime  ,                ");
                    sqlCmd.Append($"       Comment                       ");
                    sqlCmd.Append($"    FROM                             ");
                    sqlCmd.Append($"       csi_database.tempMachine      ");
                    sqlCmd.Append($"    WHERE                            ");
                    sqlCmd.Append($"       MachineName = '{machineName}';");

                    MySqlAccess.ExecuteNonQuery(sqlCmd.ToString());
                }
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private static string RenameMachine(string machine)
        {
            string res = machine;

            for (int ii = 32; ii <= 47; ii++){
                res = res.Replace(((char)ii).ToString(), $"_c{ ii }_");
            }

            for (int ii = 58; ii <= 64; ii++){
                res = res.Replace(((char)ii).ToString(), $"_c{ ii }_");
            }

            for (int ii = 91; ii <= 96; ii++)
            {
                if (ii != 95)
                    res = res.Replace(((char)ii).ToString(), $"_c{ ii }_");
            }

            for (int ii = 123; ii <= 126; ii++)
            {
                res = res.Replace(((char)ii).ToString(), $"_c{ ii }_");
            }

            return res;
        }

        public static int UpdateMachinesDatabase(bool importFullFile = false, int year = 0)
        {
            Dictionary<string, int> machines = new Dictionary<string, int>();

            DataTable dtMachines = MySqlAccess.GetDataTable("SELECT Id, EnetMachineName FROM csi_auth.tbl_ehub_conf;");

            foreach (DataRow machine in dtMachines.Rows)
            {
                if (!machines.ContainsKey(machine["EnetMachineName"].ToString()))
                    machines.Add(machine["EnetMachineName"].ToString(), int.Parse(machine["Id"].ToString()));
            }

            if (year == 0) year = DateTime.Today.Year;

            int total = 0;

            allMachHistory = eNetDataSource.GetEnetHistory(year);
            
            Parallel.ForEach(eNetServer.MonitoredMachines, (mach) => total += UpdateMachineDatabase(mach, machines[mach.MachineName], importFullFile, year));

            allMachHistory = null;

            return total;
        }

        private static int UpdateMachineDatabase(eNetMachineConfig machine, int machineId, bool importFullFile = false, int year = 0)
        {
            int total = 0;

            try
            {
                Log.Info($"===> Machine {machine.MachineName}");

                MySqlAccess.Validate_Database_Machine_Table(machine.MachineName);

                DateTime startDate = DateTime.Today.Date.AddDays(-7);
                StringBuilder sqlCmd = new StringBuilder();

                if (importFullFile)
                {
                    startDate = new DateTime(year, 1, 1);
                }
                else
                {
                    string maxDate = MySqlAccess.ExecuteScalar($"SELECT max(Date_) AS maxdate FROM CSI_Database.{ machine.MachineDbTable }").ToString();

                    if (maxDate == null || maxDate == "")
                        startDate = new DateTime(DateTime.Today.Year, 1, 1);
                    else
                        startDate = DateTime.Parse(maxDate).AddDays(-1);
                }

                Log.Info($"===> Machine {machine.MachineName}, startDate {startDate.ToLongDateString()} - {allMachHistory.Count} **");

                int count = 1;

                List<CsvMachineModel> machHistory = allMachHistory.FindAll(h => h.MachineName == machine.MachineName && h.StartTime >= startDate);

                Log.Info($"===> Machine {machine.MachineName}, startDate {startDate.ToLongDateString()} - {machHistory.Count}/{allMachHistory.Count}");

                //machine.AlwaysRecordCycleOn
                bool cycleOnTerminateBySetup = machine.GetAllowedCommands("SETUP").ContainsKey("CYCLE ON");

                string _partNumber = "";
                string _operator = "";
                bool inSetup = false;
                int shift = machHistory[0].Shift;
                DateTime shiftDate = machHistory[0].StartTime.Date;

                foreach (CsvMachineModel statusChange in machHistory)
                {
                    if (shift != statusChange.Shift)
                    {
                        shift = statusChange.Shift;
                        shiftDate = statusChange.StartTime.Date;
                    }

                    string status = statusChange.MachineStatus;
                    if (status.StartsWith("_PART"))
                    {
                        _partNumber = statusChange.Comment.Split(';')[0];
                        status = $"_PARTNO:{_partNumber}";
                    }

                    if (status == "SETUP")
                        inSetup = true;

                    if (inSetup && status == "CYCLE OFF")
                        inSetup = false;

                    if (inSetup && status == "CYCLE ON")
                        status = "SETUP-CYCLE ON";

                    sqlCmd.Append($"INSERT IGNORE INTO ");
                    sqlCmd.Append($"    CSI_Database.{machine.MachineDbTable}");
                    sqlCmd.Append($" (              ");
                    sqlCmd.Append($"    month_    , ");
                    sqlCmd.Append($"    day_      , ");
                    sqlCmd.Append($"    year_     , ");
                    sqlCmd.Append($"    ShiftDate , ");
                    sqlCmd.Append($"    date_     , ");
                    sqlCmd.Append($"    status    , ");
                    sqlCmd.Append($"    shift     , ");
                    sqlCmd.Append($"    cycletime , ");
                    sqlCmd.Append($"    partnumber, ");
                    sqlCmd.Append($"    operator  , ");
                    sqlCmd.Append($"    comments    ");
                    sqlCmd.Append($" )              ");
                    sqlCmd.Append($" VALUES         ");
                    sqlCmd.Append($" (              ");
                    sqlCmd.Append($"    { statusChange.StartTime.Month }      , ");
                    sqlCmd.Append($"    { statusChange.StartTime.Day }        , ");
                    sqlCmd.Append($"    { statusChange.StartTime.Year }       , ");
                    sqlCmd.Append($"   '{ shiftDate.ToString("yyyy-MM-dd") }' , ");
                    sqlCmd.Append($"   '{ statusChange.StartTime.ToString("yyyy-MM-dd HH:mm:ss") }', ");
                    sqlCmd.Append($"   '{ status }'                           , ");
                    sqlCmd.Append($"    { statusChange.Shift }                , ");
                    sqlCmd.Append($"    { statusChange.ElapsedTime }          , ");
                    sqlCmd.Append($"   '{ _partNumber }'                      , ");
                    sqlCmd.Append($"   '{ _operator }'                        , ");
                    sqlCmd.Append($"   '{ statusChange.Comment }'               ");
                    sqlCmd.Append($" );             ");

                    if (count == 50)
                    {
                        try
                        {
                            total += MySqlAccess.ExecuteNonQuery(sqlCmd.ToString());
                            Log.Info($"===> Machine {machine.MachineName}, total inserted {total}");
                        }
                        catch (Exception ex)
                        {
                            string err = ex.Message;
                        }
                        sqlCmd.Clear();
                        count = 0;
                    }
                    count++;
                }
                if (sqlCmd.Length > 0)
                {
                    try
                    {
                        total += MySqlAccess.ExecuteNonQuery(sqlCmd.ToString());
                        Log.Info($"===> Machine {machine.MachineName}, total inserted {total}");
                    }
                    catch (Exception ex)
                    {
                        string err = ex.Message;
                    }
                }

                ApplyChanges(machine, startDate);
                Log.Info($"===> Machine {machine.MachineName}, changes applied");
            }
            catch (Exception ex)
            {
                Log.Error($"===> Machine {machine.MachineName}", ex);

                string err = ex.Message;
            }

            return total;
        }

        private static void ApplyChanges(eNetMachineConfig machine, DateTime startDate)
        {
            DataTable tblChanges = MySqlAccess.GetDataTable($"SELECT * FROM csi_database.vw_adjustment WHERE MachineId = {machine.MachineId} AND Date >= {startDate.ToString("yyyy-MM-dd")} ORDER BY Id");

            StringBuilder sqlCmd = new StringBuilder();

            foreach (DataRow row in tblChanges.Rows)
            {
                int id = int.Parse(row["Id"].ToString());
                string action = row["Action"].ToString();
                string status = row["Status"].ToString();
                string date = DateTime.Parse(row["Date"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                string newDate = DateTime.Parse(row["NewDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                string shiftDate = DateTime.Parse(row["AdjustDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");

                sqlCmd.Clear();
                try
                {
                    switch (action){
                        case "D":
                            Log.Debug($"DELETE - {id}");

                            MySqlAccess.ExecuteNonQuery($"DELETE FROM csi_database.{machine.MachineDbTable} WHERE Status = '{ status }' AND Date_ = '{ date }'");
                            break;

                        case "I":
                            Log.Debug($"INSERT - {id}");

                            sqlCmd.Append($"INSERT INTO ");
                            sqlCmd.Append($"    CSI_Database.{machine.MachineDbTable}  ");
                            sqlCmd.Append($" (                                         ");
                            sqlCmd.Append($"    month_    ,                            ");
                            sqlCmd.Append($"    day_      ,                            ");
                            sqlCmd.Append($"    year_     ,                            ");
                            sqlCmd.Append($"    ShiftDate ,                            ");
                            sqlCmd.Append($"    date_     ,                            ");
                            sqlCmd.Append($"    status    ,                            ");
                            sqlCmd.Append($"    shift     ,                            ");
                            sqlCmd.Append($"    cycletime ,                            ");
                            sqlCmd.Append($"    partnumber,                            ");
                            sqlCmd.Append($"    operator  ,                            ");
                            sqlCmd.Append($"    comments                               ");
                            sqlCmd.Append($" )                                         ");
                            sqlCmd.Append($" VALUES                                    ");
                            sqlCmd.Append($" (                                         ");
                            sqlCmd.Append($"    { newDate.Substring(5, 2) }          , ");
                            sqlCmd.Append($"    { newDate.Substring(8, 2) }          , ");
                            sqlCmd.Append($"    { newDate.Substring(0, 4) }          , ");
                            sqlCmd.Append($"   '{ shiftDate }'                       , ");
                            sqlCmd.Append($"   '{ newDate }'                         , ");
                            sqlCmd.Append($"   '{ row["NewStatus"].ToString() }'     , ");
                            sqlCmd.Append($"    { row["Shift"].ToString() }          , ");
                            sqlCmd.Append($"    { row["NewCycletime"].ToString() }   , ");
                            sqlCmd.Append($"   '{ row["NewPartNumber"].ToString() }' , ");
                            sqlCmd.Append($"   '{ row["NewOperator"].ToString() }'   , ");
                            sqlCmd.Append($"   '{ row["NewComments"].ToString() }'     ");
                            sqlCmd.Append($" )                                         ");
                            sqlCmd.Append($" ON DUPLICATE KEY UPDATE                   ");
                            sqlCmd.Append($"    cycletime  =  {row["NewCycletime"].ToString()}  , ");
                            sqlCmd.Append($"    partnumber = '{row["NewPartNumber"].ToString()}', ");
                            sqlCmd.Append($"    operator   = '{row["NewOperator"].ToString()}'  , ");
                            sqlCmd.Append($"    comments   = '{row["NewComments"].ToString()}'    ");
                            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString());
                            break;

                        case "E":
                            Log.Debug($"EDIT - {id}");

                            sqlCmd.Append($"UPDATE                                                  ");
                            sqlCmd.Append($"    CSI_Database.{machine.MachineDbTable}               ");
                            sqlCmd.Append($" SET                                                    ");
                            sqlCmd.Append($"    month_     =  { newDate.Substring(5, 2) }         , ");
                            sqlCmd.Append($"    day_       =  { newDate.Substring(8, 2) }         , ");
                            sqlCmd.Append($"    year_      =  { newDate.Substring(0, 4) }         , ");
                            sqlCmd.Append($"    ShiftDate  = '{ shiftDate }'                      , ");
                            sqlCmd.Append($"    date_      = '{ newDate }'                        , ");
                            sqlCmd.Append($"    status     = '{ row["NewStatus"].ToString() }'    , ");
                            sqlCmd.Append($"    cycletime  =  { row["NewCycletime"].ToString() }  , ");
                            sqlCmd.Append($"    partnumber = '{ row["NewPartNumber"].ToString() }', ");
                            sqlCmd.Append($"    operator   = '{ row["NewOperator"].ToString() }'  , ");
                            sqlCmd.Append($"    comments   = '{ row["NewComments"].ToString() }'    ");
                            sqlCmd.Append($" WHERE                                                  ");
                            sqlCmd.Append($"    Status     = '{ status }' AND Date_ = '{ date }'    ");
                            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString());
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"Error Applying the changes - Machine: {machine.MachineName}, Action: {action}, Status: {status}, Date: {date}", ex);
                }
            }
        }
    }
}
