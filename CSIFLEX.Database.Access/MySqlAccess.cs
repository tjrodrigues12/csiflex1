using CSIFLEX.Utilities;
using Encryption.Utilities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSIFLEX.Database.Access
{
    public static class MySqlAccess
    {
        const string mySqlConnectionString = "server=localhost;user=root;password=CSIF1337;port=3306;Convert Zero Datetime=True;Allow User Variables=True;SslMode=None;AllowPublicKeyRetrieval=true;Connection Timeout=1800;Command Timeout=0;";

        private static string authTablesBckp = "";
        public static string AuthTablesBckp
        {
            get { return authTablesBckp; }
        }

        private static void AddAuthTableBckp(string table)
        {
            if (string.IsNullOrEmpty(authTablesBckp))
                authTablesBckp = table;
            else
                authTablesBckp = $"{authTablesBckp} {table}";
        }

        private static string databaseTablesBckp = "";
        public static string DatabaseTablesBckp
        {
            get { return databaseTablesBckp; }
        }

        private static void AddDatabaseTableBckp(string table)
        {
            if (string.IsNullOrEmpty(databaseTablesBckp))
                databaseTablesBckp = table;
            else
                databaseTablesBckp = $"{databaseTablesBckp} {table}";
        }

        private static string connString = "";
        public static void SetConnectingString(string str)
        {
            connString = str;
        }

        public static bool hasConnection(string connectionString = "")
        {
            if (connectionString == "") connectionString = connString;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    return connection.State == System.Data.ConnectionState.Open;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
        }


        public static string hasConnection2(string connectionString = "")
        {
            if (connectionString == "") connectionString = connString;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    return (connection.State == System.Data.ConnectionState.Open).ToString();
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


        public static int ExecuteNonQuery(string sqlCmd, string connectionString = "")
        {
            if (connectionString == "") connectionString = connString;

            try
            {
                MySqlCommand command;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    command = new MySqlCommand(sqlCmd, connection);
                    command.CommandTimeout = 0;
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                if (!sqlCmd.Contains("IDENTIFIED BY"))
                    Log.Error($"Query: {sqlCmd}", ex);

                throw new Exception($"ExecuteNonQuery - {ex.Message}", ex);
            }
        }


        public static int ExecuteNonQuery(MySqlCommand sqlCmd, string connectionString = "")
        {
            if (connectionString == "") connectionString = connString;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    sqlCmd.Connection = connection;
                    connection.Open();
                    return sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Query: {sqlCmd}", ex);
                throw new Exception($"ExecuteNonQuery - {ex.Message}", ex);
            }
        }


        public static object ExecuteScalar(string sqlCmd, string connectionString = "")
        {
            if (connectionString == "") connectionString = connString;

            try
            {
                MySqlCommand command;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    command = new MySqlCommand(sqlCmd, connection);
                    connection.Open();
                    return command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Query: {sqlCmd}", ex);
                throw new Exception($"ExecuteScalar - {ex.Message}", ex);
            }
        }


        public static DataTable GetDataTable(string sqlCmd, string connectionString = "")
        {
            if (connectionString == "") connectionString = connString;

            DataTable dataTable = new DataTable();
            MySqlCommand command;
            MySqlDataReader reader;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    command = new MySqlCommand(sqlCmd, connection);
                    connection.Open();
                    reader = command.ExecuteReader();
                    dataTable.Load(reader);

                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Query: {sqlCmd}", ex);
                throw new Exception($"GetDataTable - {ex.Message}", ex);
            }
        }


        public static DataTable GetUser(string userName, string connectionString = "")
        {
            if (connectionString == "") connectionString = connString;

            DataTable dataTable = new DataTable();
            MySqlCommand command;
            MySqlDataReader reader;

            string sqlCmd = $"SELECT * FROM csi_auth.Users WHERE username_ = '{userName}'";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    command = new MySqlCommand(sqlCmd, connection);
                    connection.Open();
                    reader = command.ExecuteReader();
                    dataTable.Load(reader);

                    return dataTable;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }


        public static void Validate_Database(string connectionString = mySqlConnectionString)
        {
            string connectString = "server=localhost;user=root;port=3306;SslMode=None;";

            try
            {
                ExecuteNonQuery("ALTER USER 'root'@'localhost' IDENTIFIED BY 'CSIF1337'; FLUSH PRIVILEGES;", connectString);
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("using password: NO"))
                    throw new Exception($"Validate_Database - {ex.Message}", ex);
            }

            //try
            //{
            //    //ExecuteNonQuery("SET SQL_SAFE_UPDATES = 0; GRANT SELECT ON *.* TO 'CSIFlexMobile'@'localhost' IDENTIFIED BY 'CSIFM1337';", connectionString);
            //    ExecuteNonQuery("SET SQL_SAFE_UPDATES = 0; GRANT SELECT ON *.* TO 'CSIFlexMobile'@'localhost';", connectionString);
            //}
            //catch (Exception ex)
            //{
            //    if (!ex.Message.Contains("using password: NO"))
            //        throw new Exception($" - {ex.Message}", ex);
            //}

            try
            {
                ExecuteNonQuery("CREATE USER IF NOT EXISTS 'root'@'%'             IDENTIFIED BY 'CSIF1337';  ", connectionString);
                ExecuteNonQuery("CREATE USER IF NOT EXISTS 'cfdbview'@'%'         IDENTIFIED BY 'C$ifprtvw'; ", connectionString);
                ExecuteNonQuery("CREATE USER IF NOT EXISTS 'client'@'%'           IDENTIFIED BY 'csiflex123';", connectionString);
                ExecuteNonQuery("CREATE USER IF NOT EXISTS 'client'@'127.0.0.1'   IDENTIFIED BY 'csiflex123';", connectionString);
            }
            catch (Exception) { }

            Validate_CsiAuth_Database(connectionString);

            Validate_CsiDatabase_Database(connectionString);

            Validate_MachinePerf_Database(connectionString);

            Validate_Monitoring_Database(connectionString);

            StringBuilder grantCmd = new StringBuilder();
            grantCmd.Append("GRANT ALL    ON `csi_database`.*                 TO 'client'@'%' ; ");
            grantCmd.Append("GRANT SELECT ON `csi_auth`.*                     TO 'client'@'%' ; ");
            grantCmd.Append("GRANT SELECT ON `csi_machineperf`.*              TO 'client'@'%' ; ");
            grantCmd.Append("GRANT SELECT ON `monitoring`.*                   TO 'client'@'%' ; ");
            grantCmd.Append("GRANT UPDATE ON `csi_auth`.`auto_report_config`  TO 'client'@'%' ; ");
            grantCmd.Append("GRANT UPDATE ON `csi_auth`.`tbl_license`         TO 'client'@'%' ; ");

            grantCmd.Append("GRANT SELECT ON `csi_auth`.*        TO 'client'@'127.0.0.1' ; ");
            grantCmd.Append("GRANT SELECT ON `csi_database`.*    TO 'client'@'127.0.0.1' ; ");
            grantCmd.Append("GRANT SELECT ON `csi_machineperf`.* TO 'client'@'127.0.0.1' ; ");
            grantCmd.Append("GRANT SELECT ON `monitoring`.*      TO 'client'@'127.0.0.1' ; ");
            grantCmd.Append("GRANT ALL    ON `csi_auth`.*        TO 'root'@'%'           ; ");
            grantCmd.Append("GRANT ALL    ON `csi_database`.*    TO 'root'@'%'           ; ");
            grantCmd.Append("GRANT ALL    ON `csi_machineperf`.* TO 'root'@'%'           ; ");
            grantCmd.Append("GRANT ALL    ON `monitoring`.*      TO 'root'@'%'           ; ");

            ExecuteNonQuery(grantCmd.ToString(), connectionString);
        }


        #region VALIDATE CSI_AUTH DATABASE TABLES =================================================================================================================================

        public static void Validate_CsiAuth_Database(string connectionString = mySqlConnectionString)
        {
            string sqlCmd = "CREATE DATABASE IF NOT EXISTS CSI_auth CHARACTER SET utf8 COLLATE utf8_unicode_ci;";

            try
            {
                ExecuteNonQuery(sqlCmd);
            }
            catch (Exception ex) { throw new Exception($"Validate_CsiAuth_Database1 - {ex.Message}", ex); }

            try
            {
                //Validate_Auth_AutoReportConfig(connectionString);

                Validate_Auth_AutoReportConfig_Table(connectionString);

                Validate_Auth_Auto_Report_Status_Table(connectionString);

                Validate_Auth_FtpConfig_Table(connectionString);

                Validate_Auth_AutoReportStatus_Table(connectionString);

                Validate_Auth_CsiConnector_Table(connectionString);

                Validate_Auth_CsiOtherSettings_Table(connectionString);

                Validate_Auth_CsiOtherSettingsValues_Table(connectionString);

                Validate_Auth_EhubConf_Table(connectionString);

                Validate_Auth_Machines_Settings_Table(connectionString);

                Validate_Auth_EmailReports_Table(connectionString);

                Validate_Auth_FileStatus_Table(connectionString);

                Validate_Auth_MachStatus_Table(connectionString);

                Validate_Auth_License_Table(connectionString);

                Validate_Auth_MtcFocasConditions_Table(connectionString);

                Validate_Auth_ServiceConfig_Table(connectionString);

                Validate_Auth_UpdateStatus_Table(connectionString);

                Validate_Auth_Users_Table(connectionString);

                Recreate_Auth_ConnectorsValues_View(connectionString);

                Recreate_Auth_MachinesSettingsValues_View(connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_CsiAuth_Database2 - {ex.Message}", ex);
            }
        }


        public static void Validate_Auth_AutoReportConfig(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS CSI_auth.AutoReportConfig ");
            sqlCmd.Append("(                                                    ");
            sqlCmd.Append("   ReportId        INT AUTO_INCREMENT PRIMARY KEY,   ");
            sqlCmd.Append("   ReportName      VARCHAR(100),                     ");
            sqlCmd.Append("   ReportType      VARCHAR(100),                     ");
            sqlCmd.Append("   ReportPeriod    VARCHAR(20),                      ");
            sqlCmd.Append("   GenerationDays  VARCHAR(255),                     ");
            sqlCmd.Append("   GenerationTime  VARCHAR(10),                      ");
            sqlCmd.Append("   OutputFolder    VARCHAR(255),                     ");
            sqlCmd.Append("   ShortFileName   BOOLEAN NOT NULL DEFAULT False,   ");
            sqlCmd.Append("   Machines        TEXT,                             ");
            sqlCmd.Append("   Dayback         INT,                              ");
            sqlCmd.Append("   ShiftNumber     INT,                              ");
            sqlCmd.Append("   ShiftStartTime  varchar(45),                      ");
            sqlCmd.Append("   ShiftEndTime    varchar(45),                      ");
            sqlCmd.Append("   EmailTo         TEXT,                             ");
            sqlCmd.Append("   CustomMsg       TEXT,                             ");
            sqlCmd.Append("   LastDone        VARCHAR(20) NOT NULL DEFAULT ''   ");
            sqlCmd.Append(");                                                   ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);

                var newTable = GetDataTable("SELECT * FROM CSI_auth.AutoReportConfig;");
                if (newTable.Rows.Count == 0)
                {
                    var oldTable = GetDataTable("SELECT * FROM CSI_auth.Auto_Report_config;");
                    if (oldTable.Rows.Count > 0)
                    {
                        sqlCmd.Clear();

                        foreach (DataRow row in oldTable.Rows)
                        {
                            string reportType = row["Report_Type"].ToString().Contains("Availability") ? "Availability" : "Downtime";
                            string reportPeriod = row["Report_Type"].ToString().Contains("Today") ? "Daily - Today" :
                                                  row["Report_Type"].ToString().Contains("Yesterday") ? "Daily - Yesterday" :
                                                  row["Report_Type"].ToString().Contains("Weekly") ? "Weekly" : "Monthly";

                            int dayBack;
                            if (!int.TryParse(row["dayback"].ToString(), out dayBack))
                                dayBack = 0;

                            int shiftNumber;
                            if (!int.TryParse(row["shift_number"].ToString(), out shiftNumber))
                                shiftNumber = 0;

                            sqlCmd.Append($"INSERT INTO CSI_auth.AutoReportConfig       ");
                            sqlCmd.Append($"(                                           ");
                            sqlCmd.Append($"    ReportName                           ,  ");
                            sqlCmd.Append($"    ReportType                           ,  ");
                            sqlCmd.Append($"    ReportPeriod                         ,  ");
                            sqlCmd.Append($"    GenerationDays                       ,  ");
                            sqlCmd.Append($"    GenerationTime                       ,  ");
                            sqlCmd.Append($"    OutputFolder                         ,  ");
                            sqlCmd.Append($"    ShortFileName                        ,  ");
                            sqlCmd.Append($"    Machines                             ,  ");
                            sqlCmd.Append($"    Dayback                              ,  ");
                            sqlCmd.Append($"    ShiftNumber                          ,  ");
                            sqlCmd.Append($"    ShiftStartTime                       ,  ");
                            sqlCmd.Append($"    ShiftEndTime                         ,  ");
                            sqlCmd.Append($"    EmailTo                              ,  ");
                            sqlCmd.Append($"    CustomMsg                            ,  ");
                            sqlCmd.Append($"    LastDone                                ");
                            sqlCmd.Append($")                                           ");
                            sqlCmd.Append($"VALUES                                      ");
                            sqlCmd.Append($"(                                           ");
                            sqlCmd.Append($"   '{ row["Task_name"].ToString() }'      , ");
                            sqlCmd.Append($"   '{ reportType }'                       , ");
                            sqlCmd.Append($"   '{ reportPeriod }'                     , ");
                            sqlCmd.Append($"   '{ row["Day_"].ToString() }'           , ");
                            sqlCmd.Append($"   '{ row["Time_"].ToString() }'          , ");
                            sqlCmd.Append($"   '{ row["Output_Folder"].ToString() }'  , ");
                            sqlCmd.Append($"    { row["short_filename"].ToString() }  , ");
                            sqlCmd.Append($"   '{ row["MachineToReport"].ToString() }', ");
                            sqlCmd.Append($"    { dayBack }                           , ");
                            sqlCmd.Append($"    { shiftNumber }                       , ");
                            sqlCmd.Append($"   '{ row["shift_starttime"].ToString() }', ");
                            sqlCmd.Append($"   '{ row["shift_endtime"].ToString() }'  , ");
                            sqlCmd.Append($"   '{ row["MailTo"].ToString() }'         , ");
                            sqlCmd.Append($"   '{ row["CustomMsg"].ToString() }'      , ");
                            sqlCmd.Append($"   '{ row["done"].ToString() }'             ");
                            sqlCmd.Append($");                                          ");
                        }
                        ExecuteNonQuery(sqlCmd.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Auth_AutoReportConfig - {ex.Message}", ex);
            }

            AddAuthTableBckp("AutoReportConfig");
        }


        /// <summary>
        /// Create the table CSI_auth.Auto_Report_config if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Auth_AutoReportConfig_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS CSI_auth.Auto_Report_config            ");
            sqlCmd.Append("(                                                                 ");
            sqlCmd.Append("   Id               INT NOT NULL AUTO_INCREMENT PRIMARY KEY,      ");
            sqlCmd.Append("   Task_name        varchar(255),                                 ");
            sqlCmd.Append("   Day_             text,                                         ");
            sqlCmd.Append("   Time_            text,                                         ");
            sqlCmd.Append("   ReportType       VARCHAR(100) NOT NULL DEFAULT '',             ");
            sqlCmd.Append("   ReportPeriod     VARCHAR(20),                                  ");
            sqlCmd.Append("   ReportTitle      VARCHAR(200) NOT NULL DEFAULT '',             ");
            sqlCmd.Append("   Output_Folder    text,                                         ");
            sqlCmd.Append("   MachineToReport  text,                                         ");
            sqlCmd.Append("   MailTo           text,                                         ");
            sqlCmd.Append("   Email_Time       text,                                         ");
            sqlCmd.Append("   done             varchar(255) not null DEFAULT '10',           ");
            sqlCmd.Append("   dayback          text,                                         ");
            sqlCmd.Append("   timeback         text,                                         ");
            sqlCmd.Append("   CustomMsg        text,                                         ");
            sqlCmd.Append("   Scale            varchar(10) ,                                 ");
            sqlCmd.Append("   Production       boolean DEFAULT 1,                            ");
            sqlCmd.Append("   Setup            boolean DEFAULT 1,                            ");
            sqlCmd.Append("   OnlySummary      boolean DEFAULT 0,                            ");
            sqlCmd.Append("   Enabled          boolean DEFAULT 1,                            ");
            sqlCmd.Append("   Sorting          varchar(20) NOT NULL DEFAULT 'Value',         ");
            sqlCmd.Append("   EventMinMinutes  int     DEFAULT 0,                            ");
            sqlCmd.Append("   shift_number     varchar(45) COLLATE utf8_unicode_ci NOT NULL, ");
            sqlCmd.Append("   shift_starttime  varchar(45) COLLATE utf8_unicode_ci NOT NULL, ");
            sqlCmd.Append("   shift_endtime    varchar(45) COLLATE utf8_unicode_ci NOT NULL, ");
            sqlCmd.Append("   short_filename   varchar(45) COLLATE utf8_unicode_ci NOT NULL DEFAULT 'False', ");
            sqlCmd.Append("   ShowConInSetup   INT     DEFAULT 2,                            ");
            sqlCmd.Append("   UNIQUE INDEX `Task_name_UNIQUE` (`Task_name` ASC)              ");
            sqlCmd.Append(")                                                                 ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);

                if (!ExistsColumn("Id", "CSI_auth.Auto_Report_config", connectionString))
                {
                    sqlCmd.Clear();
                    sqlCmd.Append($"ALTER TABLE csi_auth.auto_report_config DROP PRIMARY KEY;");
                    sqlCmd.Append($"ALTER TABLE csi_auth.auto_report_config ADD id int NOT NULL AUTO_INCREMENT PRIMARY KEY FIRST;");
                    sqlCmd.Append($"ALTER TABLE csi_auth.auto_report_config ADD UNIQUE INDEX `Task_name_UNIQUE` (`Task_name` ASC);");
                    ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                }

                bool exists = ExecuteScalar("SELECT EXISTS(SELECT * FROM information_schema.statistics WHERE table_schema = 'CSI_AUTH' AND table_name = 'auto_report_config' AND index_name = 'Task_name_UNIQUE');").ToString() == "1";

                if (!exists)
                {
                    sqlCmd.Clear();
                    sqlCmd.Append($"ALTER TABLE csi_auth.auto_report_config ADD UNIQUE INDEX `Task_name_UNIQUE` (`Task_name` ASC);");
                    ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                }

                if (!ExistsColumn("short_filename", "CSI_auth.Auto_Report_config", connectionString))
                    AddColumn("short_filename", "CSI_auth.Auto_Report_config", "varchar(45) COLLATE utf8_unicode_ci NOT NULL DEFAULT 'False'", connectionString);

                if (!ExistsColumn("ReportType", "CSI_auth.Auto_Report_config", connectionString))
                    AddColumn("ReportType", "CSI_auth.Auto_Report_config", "VARCHAR(100)", connectionString);

                if (!ExistsColumn("ReportTitle", "CSI_auth.Auto_Report_config", connectionString))
                    AddColumn("ReportTitle", "CSI_auth.Auto_Report_config", "VARCHAR(200) NOT NULL DEFAULT ''", connectionString);

                if (!ExistsColumn("ReportPeriod", "CSI_auth.Auto_Report_config", connectionString))
                    AddColumn("ReportPeriod", "CSI_auth.Auto_Report_config", "VARCHAR(100)", connectionString);

                if (!ExistsColumn("Scale", "CSI_auth.Auto_Report_config", connectionString))
                    AddColumn("Scale", "CSI_auth.Auto_Report_config", "VARCHAR(10)", connectionString);

                if (!ExistsColumn("Production", "CSI_auth.Auto_Report_config", connectionString))
                    AddColumn("Production", "CSI_auth.Auto_Report_config", "boolean DEFAULT 1", connectionString);

                if (!ExistsColumn("Setup", "CSI_auth.Auto_Report_config", connectionString))
                    AddColumn("Setup", "CSI_auth.Auto_Report_config", "boolean DEFAULT 1", connectionString);

                if (!ExistsColumn("OnlySummary", "CSI_auth.Auto_Report_config", connectionString))
                    AddColumn("OnlySummary", "CSI_auth.Auto_Report_config", "boolean DEFAULT 0", connectionString);

                if (!ExistsColumn("Enabled", "CSI_auth.Auto_Report_config", connectionString))
                    AddColumn("Enabled", "CSI_auth.Auto_Report_config", "boolean DEFAULT 1", connectionString);

                if (!ExistsColumn("EventMinMinutes", "CSI_auth.Auto_Report_config", connectionString))
                    AddColumn("EventMinMinutes", "CSI_auth.Auto_Report_config", "int DEFAULT 0", connectionString);

                if (!ExistsColumn("Sorting", "CSI_auth.Auto_Report_config", connectionString))
                    AddColumn("Sorting", "CSI_auth.Auto_Report_config", "varchar(20) NOT NULL DEFAULT 'Value'", connectionString);

                if (!ExistsColumn("ShowConInSetup", "CSI_auth.Auto_Report_config", connectionString))
                    AddColumn("ShowConInSetup", "CSI_auth.Auto_Report_config", "int DEFAULT 2", connectionString);
                else if (GetColumnType("ShowConInSetup", "Auto_Report_config", connectionString) != MySQLColumnType.INTEGER)
                {
                    sqlCmd.Clear();
                    sqlCmd.Append($"ALTER TABLE `csi_auth`.`auto_report_config` CHANGE COLUMN `ShowConInSetup` `ShowConInSetup` INT NULL DEFAULT 2; ");
                    sqlCmd.Append($"UPDATE `csi_auth`.`auto_report_config` SET `ShowConInSetup` = 2 WHERE `ShowConInSetup` = 1; ");
                    try
                    {
                        ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Changing column `auto_report_config`.`ShowConInSetup`", ex);
                    }
                }

                if (ExistsColumn("Report_Type", "CSI_auth.Auto_Report_config", connectionString))
                {
                    sqlCmd.Clear();
                    sqlCmd.Append("UPDATE CSI_auth.Auto_Report_config  ");
                    sqlCmd.Append("SET                                 ");
                    sqlCmd.Append("   ReportType   = CASE WHEN ( LOCATE('Availability', Report_Type) > 0 ) THEN 'Availability' ELSE 'Downtime' END,                    ");
                    sqlCmd.Append("   ReportPeriod = CASE WHEN ( LOCATE('Daily', Report_Type) > 0 AND LOCATE('Today', Report_Type) )     THEN 'Daily - Today'     ELSE ");
                    sqlCmd.Append("                  CASE WHEN ( LOCATE('Daily', Report_Type) > 0 AND LOCATE('Yesterday', Report_Type) ) THEN 'Daily - Yesterday' ELSE ");
                    sqlCmd.Append("                  CASE WHEN ( LOCATE('Weekly', Report_Type) > 0 ) THEN 'Weekly' ELSE 'Monthly' END END END                          ");
                    sqlCmd.Append("WHERE                               ");
                    sqlCmd.Append("   ReportType  IS NULL              ");

                    ExecuteNonQuery(sqlCmd.ToString());
                }

                bool hasSystemMonitoring = Convert.ToInt16(ExecuteScalar("SELECT count(*) FROM CSI_auth.Auto_Report_config WHERE Task_name = 'SystemMonitoring'").ToString()) > 0;
                if (!hasSystemMonitoring)
                {
                    sqlCmd.Clear();
                    sqlCmd.Append($"INSERT INTO csi_auth.auto_report_config  ");
                    sqlCmd.Append($" (                                       ");
                    sqlCmd.Append($"    Task_name        ,                   ");
                    sqlCmd.Append($"    Day_             ,                   ");
                    sqlCmd.Append($"    Time_            ,                   ");
                    sqlCmd.Append($"    ReportType       ,                   ");
                    sqlCmd.Append($"    ReportTitle      ,                   ");
                    sqlCmd.Append($"    ReportPeriod     ,                   ");
                    sqlCmd.Append($"    Output_Folder    ,                   ");
                    sqlCmd.Append($"    MachineToReport  ,                   ");
                    sqlCmd.Append($"    MailTo           ,                   ");
                    sqlCmd.Append($"    done             ,                   ");
                    sqlCmd.Append($"    Scale            ,                   ");
                    sqlCmd.Append($"    Production       ,                   ");
                    sqlCmd.Append($"    Setup            ,                   ");
                    sqlCmd.Append($"    OnlySummary      ,                   ");
                    sqlCmd.Append($"    Enabled          ,                   ");
                    sqlCmd.Append($"    Sorting          ,                   ");
                    sqlCmd.Append($"    EventMinMinutes  ,                   ");
                    sqlCmd.Append($"    shift_number     ,                   ");
                    sqlCmd.Append($"    shift_starttime  ,                   ");
                    sqlCmd.Append($"    shift_endtime    ,                   ");
                    sqlCmd.Append($"    short_filename                       ");
                    sqlCmd.Append($" )                                       ");
                    sqlCmd.Append($" VALUES                                  ");
                    sqlCmd.Append($" (                                       ");
                    sqlCmd.Append($"    @Task_name       ,                   ");
                    sqlCmd.Append($"    @Day_            ,                   ");
                    sqlCmd.Append($"    @Time_           ,                   ");
                    sqlCmd.Append($"    @ReportType      ,                   ");
                    sqlCmd.Append($"    @ReportTitle     ,                   ");
                    sqlCmd.Append($"    @ReportPeriod    ,                   ");
                    sqlCmd.Append($"    @Output_Folder   ,                   ");
                    sqlCmd.Append($"    @MachineToReport ,                   ");
                    sqlCmd.Append($"    @MailTo          ,                   ");
                    sqlCmd.Append($"    @done            ,                   ");
                    sqlCmd.Append($"    @Scale           ,                   ");
                    sqlCmd.Append($"    @Production      ,                   ");
                    sqlCmd.Append($"    @Setup           ,                   ");
                    sqlCmd.Append($"    @OnlySummary     ,                   ");
                    sqlCmd.Append($"    @Enabled         ,                   ");
                    sqlCmd.Append($"    @Sorting         ,                   ");
                    sqlCmd.Append($"    @EventMinMinutes ,                   ");
                    sqlCmd.Append($"    @shift_number    ,                   ");
                    sqlCmd.Append($"    @shift_starttime ,                   ");
                    sqlCmd.Append($"    @shift_endtime   ,                   ");
                    sqlCmd.Append($"    @short_filename                      ");
                    sqlCmd.Append($" )                                       ");

                    MySqlCommand sqlCommand = new MySqlCommand(sqlCmd.ToString());
                    sqlCommand.Parameters.AddWithValue("@Task_name", "SystemMonitoring");
                    sqlCommand.Parameters.AddWithValue("@Day_", "Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday");
                    sqlCommand.Parameters.AddWithValue("@Time_", "09:00");
                    sqlCommand.Parameters.AddWithValue("@ReportType", "Downtime");
                    sqlCommand.Parameters.AddWithValue("@ReportTitle", "Monitoring Report");
                    sqlCommand.Parameters.AddWithValue("@ReportPeriod", "Yesterday");
                    sqlCommand.Parameters.AddWithValue("@Output_Folder", Path.GetTempPath());
                    sqlCommand.Parameters.AddWithValue("@MachineToReport", "All machines");
                    sqlCommand.Parameters.AddWithValue("@MailTo", "sh@csiflex.com");
                    sqlCommand.Parameters.AddWithValue("@done", "pending");
                    sqlCommand.Parameters.AddWithValue("@Scale", "Minutes");
                    sqlCommand.Parameters.AddWithValue("@Production", 1);
                    sqlCommand.Parameters.AddWithValue("@Setup", 1);
                    sqlCommand.Parameters.AddWithValue("@OnlySummary", 1);
                    sqlCommand.Parameters.AddWithValue("@Enabled", 1);
                    sqlCommand.Parameters.AddWithValue("@Sorting", "Value");
                    sqlCommand.Parameters.AddWithValue("@EventMinMinutes", 0);
                    sqlCommand.Parameters.AddWithValue("@shift_number", "1,2,3");
                    sqlCommand.Parameters.AddWithValue("@shift_starttime", "00:01");
                    sqlCommand.Parameters.AddWithValue("@shift_endtime", "24:00");
                    sqlCommand.Parameters.AddWithValue("@short_filename", "False");
                    MySqlAccess.ExecuteNonQuery(sqlCommand);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Auth_AutoReportConfig_Table - {ex.Message}", ex);
            }

            AddAuthTableBckp("auto_report_config");
        }


        /// <summary>
        /// Create the table CSI_auth.Auto_Report_status if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Auth_Auto_Report_Status_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS CSI_auth.Auto_Report_status  ");
            sqlCmd.Append("(                                                       ");
            sqlCmd.Append("   ReportId         INT NOT NULL PRIMARY KEY,           ");
            sqlCmd.Append("   Status           varchar(45) NOT NULL                ");
            sqlCmd.Append(");                                                      ");

            sqlCmd.Append("INSERT IGNORE INTO csi_auth.auto_report_status          ");
            sqlCmd.Append("   SELECT                                               ");
            sqlCmd.Append("     Id, Done                                           ");
            sqlCmd.Append("   FROM                                                 ");
            sqlCmd.Append("     csi_auth.auto_report_config;                       ");

            sqlCmd.Append($"CREATE OR REPLACE VIEW csi_auth.view_auto_report_config AS  ");
            sqlCmd.Append($"SELECT                                                      ");
            sqlCmd.Append($"	C.`id`,                                                 ");
            sqlCmd.Append($"    C.`Task_name`,                                          ");
            sqlCmd.Append($"    C.`Day_`,                                               ");
            sqlCmd.Append($"    C.`Time_`,                                              ");
            sqlCmd.Append($"    C.`ReportType`,                                         ");
            sqlCmd.Append($"    C.`ReportPeriod`,                                       ");
            sqlCmd.Append($"    C.`ReportTitle`,                                        ");
            sqlCmd.Append($"    C.`Output_Folder`,                                      ");
            sqlCmd.Append($"    C.`MachineToReport`,                                    ");
            sqlCmd.Append($"    C.`MailTo`,                                             ");
            sqlCmd.Append($"    C.`Email_Time`,                                         ");
            sqlCmd.Append($"    S.`status` AS `Done`,                                   ");
            sqlCmd.Append($"    C.`dayback`,                                            ");
            sqlCmd.Append($"    C.`timeback`,                                           ");
            sqlCmd.Append($"    C.`CustomMsg`,                                          ");
            sqlCmd.Append($"    C.`Scale`,                                              ");
            sqlCmd.Append($"    C.`Production`,                                         ");
            sqlCmd.Append($"    C.`Setup`,                                              ");
            sqlCmd.Append($"    C.`OnlySummary`,                                        ");
            sqlCmd.Append($"    C.`Enabled`,                                            ");
            sqlCmd.Append($"    C.`Sorting`,                                            ");
            sqlCmd.Append($"    C.`EventMinMinutes`,                                    ");
            sqlCmd.Append($"    C.`shift_number`,                                       ");
            sqlCmd.Append($"    C.`shift_starttime`,                                    ");
            sqlCmd.Append($"    C.`shift_endtime`,                                      ");
            sqlCmd.Append($"    C.`short_filename`                                      ");
            sqlCmd.Append($"FROM                                                        ");
            sqlCmd.Append($"    `csi_auth`.`auto_report_config` AS C                    ");
            sqlCmd.Append($" LEFT JOIN                                                  ");
            sqlCmd.Append($"	`csi_auth`.`auto_report_status` AS S ON C.Id = S.ReportId; ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Auth_Auto_Report_Status_Table - {ex.Message}", ex);
            }
        }


        /// <summary>
        /// Create the table CSI_auth.ftpconfig if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Auth_FtpConfig_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS CSI_auth.ftpconfig ");
            sqlCmd.Append("(                                             ");
            sqlCmd.Append("   ftpip       varchar(255),                  ");
            sqlCmd.Append("   ftppwd      varchar(255),                  ");
            sqlCmd.Append("   EnetPCName  varchar(255),                  ");
            sqlCmd.Append("   EnetPCPwd   varchar(255)                   ");
            sqlCmd.Append(")                                             ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Auth_FtpConfig_Table - {ex.Message}", ex);
            }
        }


        /// <summary>
        /// Create the table CSI_auth.tbl_autoreport_status if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Auth_AutoReportStatus_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS CSI_auth.tbl_autoreport_status          ");
            sqlCmd.Append("(                                                                  ");
            sqlCmd.Append("   `autoreport_status` varchar(5) COLLATE utf8_unicode_ci NOT NULL ");
            sqlCmd.Append(")                                                                  ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);

                DataTable dt = GetDataTable("SELECT * FROM CSI_auth.tbl_autoreport_status");
                if (dt.Rows.Count == 0)
                    ExecuteNonQuery("INSERT IGNORE INTO CSI_auth.tbl_autoreport_status VALUES ( 'False' )");
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Auth_AutoReportStatus_Table - {ex.Message}", ex);
            }
        }


        /// <summary>
        /// Create the table CSI_auth.tbl_CSIConnector if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Auth_CsiConnector_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS CSI_auth.tbl_CSIConnector                       ");
            sqlCmd.Append("(                                                                          ");
            sqlCmd.Append("   `Id`                 int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,       ");
            sqlCmd.Append("   `MachineId`          int(11) NOT NULL DEFAULT 0,                        ");
            sqlCmd.Append("   `MachineName`        varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL, ");
            sqlCmd.Append("   `MTCMachine`         varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL, ");
            sqlCmd.Append("   `MachineIP`          varchar(255),                                      ");
            sqlCmd.Append("   `ConnectorType`      varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL, ");
            sqlCmd.Append("   `CurrentStatus`      varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL, ");
            sqlCmd.Append("   `ConditionStr`       text         COLLATE utf8_unicode_ci,              ");
            sqlCmd.Append("   `PartnoID`           varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL, ");
            sqlCmd.Append("   `ProgramID`          varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL, ");
            sqlCmd.Append("   `FeedOverrideID`     varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL, ");
            sqlCmd.Append("   `SpindleOverrideID`  varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL, ");
            sqlCmd.Append("   `eNETMachineName`    varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL, ");
            sqlCmd.Append("   `FocasPort`          varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL, ");
            sqlCmd.Append("   `ControllerType`     varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL, ");
            sqlCmd.Append("   `AgentIP`            varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL, ");
            sqlCmd.Append("   `AgentPort`          varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL, ");
            sqlCmd.Append("   `Manufacturer`       varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL, ");
            sqlCmd.Append("   `AgentServiceName`   varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL, ");
            sqlCmd.Append("   `AgentExeLocation`   varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL, ");
            sqlCmd.Append("   `AdapterServiceName` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL, ");
            sqlCmd.Append("   `AdapterExeLocation` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL, ");
            sqlCmd.Append("   `AdapterPort`        varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL, ");
            sqlCmd.Append("   `MonitoringUnitId`   int(11) NOT NULL DEFAULT 0,                        ");
            sqlCmd.Append(" UNIQUE KEY `UniqueMachineName` (`MachineName`),                           ");
            sqlCmd.Append(" UNIQUE KEY `UniqueEnetMachine` (`eNETMachineName`)                        ");
            sqlCmd.Append(")                                                                          ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);

                if (!ExistsColumn("Id", "CSI_auth.tbl_CSIConnector"))
                {
                    AddColumn("Id", "CSI_auth.tbl_CSIConnector", "INT PRIMARY KEY AUTO_INCREMENT FIRST", connectionString);
                }

                if (!ExistsColumn("MachineId", "CSI_auth.tbl_CSIConnector"))
                {
                    AddColumn("MachineId", "CSI_auth.tbl_CSIConnector", "INT NOT NULL DEFAULT 0 AFTER `Id`", connectionString);

                    sqlCmd.Clear();
                    sqlCmd.Append("UPDATE                                               ");
                    sqlCmd.Append("     csi_auth.tbl_csiconnector AS C                  ");
                    sqlCmd.Append("SET                                                  ");
                    sqlCmd.Append("     MachineId =                                     ");
                    sqlCmd.Append("     ( SELECT Id FROM csi_auth.tbl_ehub_conf AS M    ");
                    sqlCmd.Append("       WHERE M.EnetMachineName = C.EnetMachineName );");
                    ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                }

                if (!ExistsColumn("MTCMachine", "CSI_auth.tbl_CSIConnector"))
                {
                    AddColumn("MTCMachine", "CSI_auth.tbl_CSIConnector", "varchar(255) DEFAULT NULL AFTER `MachineName`", connectionString);

                    ExecuteNonQuery("UPDATE CSI_auth.tbl_CSIConnector SET MTCMachine = MachineName;");
                }

                if (!ExistsColumn("MonitoringUnitId", "CSI_auth.tbl_CSIConnector"))
                {
                    AddColumn("MonitoringUnitId", "CSI_auth.tbl_CSIConnector", "int(11) DEFAULT 0", connectionString);

                    ExecuteNonQuery("UPDATE CSI_auth.tbl_CSIConnector SET MonitoringUnitId = 0;");
                }

                if (!ExistsIndex("UniqueEnetMachine", "CSI_auth.tbl_CSIConnector", "eNETMachineName", connectionString))
                {
                    AddUpdateIndex("UniqueEnetMachine", "CSI_auth.tbl_CSIConnector", "UNIQUE", "eNETMachineName", connectionString);
                }

                if (!ExistsIndex("UniqueMachineName", "CSI_auth.tbl_CSIConnector", "MachineName", connectionString))
                {
                    AddUpdateIndex("UniqueMachineName", "CSI_auth.tbl_CSIConnector", "UNIQUE", "MachineName", connectionString);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Auth_CsiConnector_Table - {ex.Message}", ex);
            }

            AddAuthTableBckp("tbl_CSIConnector");
        }


        /// <summary>
        /// Create the table CSI_auth.tbl_csiothersettings if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Auth_CsiOtherSettings_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS CSI_auth.tbl_csiothersettings          ");
            sqlCmd.Append("(                                                                 ");
            sqlCmd.Append("   `ConnectorID`               int,                               ");
            sqlCmd.Append("   `Machine_Name`              varchar(200) NOT NULL,             ");
            sqlCmd.Append("   `IP_Address`                varchar(100) NOT NULL,             ");
            sqlCmd.Append("   `PartNumber_Variable`       varchar(100),                      ");
            sqlCmd.Append("   `PartNumber_Prefix1`        varchar(45),                       ");
            sqlCmd.Append("   `PartNumber_Filter1Start`   varchar(45),                       ");
            sqlCmd.Append("   `PartNumber_Filter1End`     varchar(45),                       ");
            sqlCmd.Append("   `PartNumber_Filter2Apply`   tinyint(4)   DEFAULT '0' ,         ");
            sqlCmd.Append("   `PartNumber_Prefix2`        varchar(45),                       ");
            sqlCmd.Append("   `PartNumber_Filter2Start`   varchar(45),                       ");
            sqlCmd.Append("   `PartNumber_Filter2End`     varchar(45),                       ");
            sqlCmd.Append("   `PartNumber_Filter3Apply`   tinyint(4)   DEFAULT '0' ,         ");
            sqlCmd.Append("   `PartNumber_Prefix3`        varchar(45),                       ");
            sqlCmd.Append("   `PartNumber_Filter3Start`   varchar(45),                       ");
            sqlCmd.Append("   `PartNumber_Filter3End`     varchar(45),                       ");
            sqlCmd.Append("   `ProgramNumber_Variable`    varchar(100),                      ");
            sqlCmd.Append("   `ProgramNumber_FilterStart` varchar(45),                       ");
            sqlCmd.Append("   `ProgramNumber_FilterEnd`   varchar(45),                       ");
            sqlCmd.Append("   `Operation_Available`       tinyint(4)   DEFAULT '0' ,         ");
            sqlCmd.Append("   `Operation_FilterStart`     varchar(45),                       ");
            sqlCmd.Append("   `Operation_FilterEnd`       varchar(45),                       ");
            sqlCmd.Append("   `Feedrate_Variable`         varchar(100),                      ");
            sqlCmd.Append("   `Feedrate_Min`              varchar(45),                       ");
            sqlCmd.Append("   `Feedrate_Max`              varchar(45),                       ");
            sqlCmd.Append("   `Rapid_Variable`            varchar(100),                      ");
            sqlCmd.Append("   `Rapid_Min`                 varchar(45),                       ");
            sqlCmd.Append("   `Rapid_Max`                 varchar(45),                       ");
            sqlCmd.Append("   `Spindle_Variable`          varchar(100),                      ");
            sqlCmd.Append("   `Spindle_Min`               varchar(45),                       ");
            sqlCmd.Append("   `Spindle_Max`               varchar(45),                       ");
            sqlCmd.Append("   `PartCount_Variable`        varchar(100) DEFAULT NULL,         ");
            sqlCmd.Append("   `PartRequire_Variable`      varchar(100) DEFAULT NULL,         ");
            sqlCmd.Append("   `ActivePallet_Variable`     varchar(100) DEFAULT NULL,         ");
            sqlCmd.Append("   `ActivePallet_StartWith`    varchar(100) DEFAULT NULL,         ");
            sqlCmd.Append("   `ActivePallet_EndWith`      varchar(100) DEFAULT NULL,         ");
            sqlCmd.Append("   `ActivePallet_ToMU`         tinyint(4)   NOT NULL DEFAULT '0', ");
            sqlCmd.Append("   `WarningPressure`           INT          DEFAULT  0 ,          ");
            sqlCmd.Append("   `CriticalPressure`          INT          DEFAULT  0 ,          ");
            sqlCmd.Append("   `WarningTemperature`        INT          DEFAULT  0 ,          ");
            sqlCmd.Append("   `CriticalTemperature`       INT          DEFAULT  0 ,          ");
            sqlCmd.Append("   `MCSDelay`                  INT          DEFAULT  0 ,          ");
            sqlCmd.Append("   `DelayScale`                varchar(3)   DEFAULT 'sec',        ");
            sqlCmd.Append("   `EnableMCS`                 tinyint(4)   DEFAULT '0',          ");
            sqlCmd.Append("   `SaveDataRaw`               tinyint(4)   DEFAULT '0',          ");
            sqlCmd.Append("   `COnDuringSetup`            tinyint(4)   DEFAULT '0',          ");
            sqlCmd.Append("   `SaveProdOnly`              INT          DEFAULT  0 ,          ");
            sqlCmd.Append("   `DelayForCycleOff`          INT          DEFAULT  0 ,          ");
            sqlCmd.Append("   `DelayForCycleOffScale`     varchar(3)   DEFAULT 'sec',        ");
            sqlCmd.Append(" PRIMARY KEY(`ConnectorId`)                                       ");
            sqlCmd.Append(")                                                               ; ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);

                if (!ExistsColumn("PartNumber_Variable", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("PartNumber_Variable", "CSI_auth.tbl_csiothersettings", "varchar(100) DEFAULT NULL AFTER `IP_Address`", connectionString);

                    if (ExistsColumn("Part_No_Var", "CSI_auth.tbl_csiothersettings", connectionString))
                    {
                        sqlCmd.Clear();
                        sqlCmd.Append("UPDATE CSI_auth.tbl_csiothersettings SET PartNumber_Variable = Part_No_Var; ");
                        sqlCmd.Append("ALTER TABLE `csi_auth`.`tbl_csiothersettings` DROP COLUMN `Part_No_Var`; ");
                        ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                    }
                }

                if (ExistsColumn("Part_No_Value", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    ExecuteNonQuery("ALTER TABLE `csi_auth`.`tbl_csiothersettings` DROP COLUMN `Part_No_Value`; ", connectionString);
                }

                if (!ExistsColumn("PartNumber_Prefix1", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("PartNumber_Prefix1", "CSI_auth.tbl_csiothersettings", "varchar(45) DEFAULT NULL AFTER `PartNumber_Variable`", connectionString);
                }

                if (!ExistsColumn("PartNumber_Filter1Start", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("PartNumber_Filter1Start", "CSI_auth.tbl_csiothersettings", "varchar(45) DEFAULT NULL AFTER `PartNumber_Prefix1`", connectionString);

                    if (ExistsColumn("Part_No_StartWith", "CSI_auth.tbl_csiothersettings", connectionString))
                    {
                        sqlCmd.Clear();
                        sqlCmd.Append("UPDATE CSI_auth.tbl_csiothersettings SET PartNumber_Filter1Start = Part_No_StartWith; ");
                        sqlCmd.Append("ALTER TABLE `csi_auth`.`tbl_csiothersettings` DROP COLUMN `Part_No_StartWith`; ");
                        ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                    }
                }

                if (!ExistsColumn("PartNumber_Filter1End", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("PartNumber_Filter1End", "CSI_auth.tbl_csiothersettings", "varchar(45) DEFAULT NULL AFTER `PartNumber_Filter1Start`", connectionString);

                    if (ExistsColumn("Part_No_EndWith", "CSI_auth.tbl_csiothersettings", connectionString))
                    {
                        sqlCmd.Clear();
                        sqlCmd.Append("UPDATE CSI_auth.tbl_csiothersettings SET PartNumber_Filter1End = Part_No_EndWith; ");
                        sqlCmd.Append("ALTER TABLE `csi_auth`.`tbl_csiothersettings` DROP COLUMN `Part_No_EndWith`; ");
                        ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                    }
                }


                if (!ExistsColumn("PartNumber_Filter2Apply", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("PartNumber_Filter2Apply", "CSI_auth.tbl_csiothersettings", "tinyint DEFAULT 0 AFTER `PartNumber_Filter1End`", connectionString);
                }

                if (!ExistsColumn("PartNumber_Prefix2", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("PartNumber_Prefix2", "CSI_auth.tbl_csiothersettings", "varchar(45) DEFAULT NULL AFTER `PartNumber_Filter2Apply`", connectionString);
                }

                if (!ExistsColumn("PartNumber_Filter2Start", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("PartNumber_Filter2Start", "CSI_auth.tbl_csiothersettings", "varchar(45) DEFAULT NULL AFTER `PartNumber_Prefix2`", connectionString);
                }

                if (!ExistsColumn("PartNumber_Filter2End", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("PartNumber_Filter2End", "CSI_auth.tbl_csiothersettings", "varchar(45) DEFAULT NULL AFTER `PartNumber_Filter2Start`", connectionString);
                }


                if (!ExistsColumn("PartNumber_Filter3Apply", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("PartNumber_Filter3Apply", "CSI_auth.tbl_csiothersettings", "tinyint DEFAULT 0 AFTER `PartNumber_Filter2End`", connectionString);
                }

                if (!ExistsColumn("PartNumber_Prefix3", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("PartNumber_Prefix3", "CSI_auth.tbl_csiothersettings", "varchar(45) DEFAULT NULL AFTER `PartNumber_Filter3Apply`", connectionString);
                }

                if (!ExistsColumn("PartNumber_Filter3Start", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("PartNumber_Filter3Start", "CSI_auth.tbl_csiothersettings", "varchar(45) DEFAULT NULL AFTER `PartNumber_Prefix3`", connectionString);
                }

                if (!ExistsColumn("PartNumber_Filter3End", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("PartNumber_Filter3End", "CSI_auth.tbl_csiothersettings", "varchar(45) DEFAULT NULL AFTER `PartNumber_Filter3Start`", connectionString);
                }


                if (!ExistsColumn("ProgramNumber_Variable", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("ProgramNumber_Variable", "CSI_auth.tbl_csiothersettings", "varchar(100) DEFAULT NULL AFTER `PartNumber_Filter3End`", connectionString);

                    if (ExistsColumn("Prog_No_Var", "CSI_auth.tbl_csiothersettings", connectionString))
                    {
                        sqlCmd.Clear();
                        sqlCmd.Append("UPDATE CSI_auth.tbl_csiothersettings SET ProgramNumber_Variable = Prog_No_Var; ");
                        sqlCmd.Append("ALTER TABLE `csi_auth`.`tbl_csiothersettings` DROP COLUMN `Prog_No_Var`; ");
                        ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                    }
                }

                if (ExistsColumn("Prog_No_Value", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    ExecuteNonQuery("ALTER TABLE `csi_auth`.`tbl_csiothersettings` DROP COLUMN `Prog_No_Value`; ", connectionString);
                }

                if (!ExistsColumn("ProgramNumber_FilterStart", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("ProgramNumber_FilterStart", "CSI_auth.tbl_csiothersettings", "varchar(45) DEFAULT NULL AFTER `ProgramNumber_Variable`", connectionString);

                    if (ExistsColumn("Prog_No_StartWith", "CSI_auth.tbl_csiothersettings", connectionString))
                    {
                        sqlCmd.Clear();
                        sqlCmd.Append("UPDATE CSI_auth.tbl_csiothersettings SET ProgramNumber_FilterStart = Prog_No_StartWith; ");
                        sqlCmd.Append("ALTER TABLE `csi_auth`.`tbl_csiothersettings` DROP COLUMN `Prog_No_StartWith`; ");
                        ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                    }
                }

                if (!ExistsColumn("ProgramNumber_FilterEnd", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("ProgramNumber_FilterEnd", "CSI_auth.tbl_csiothersettings", "varchar(45) DEFAULT NULL AFTER `ProgramNumber_FilterStart`", connectionString);

                    if (ExistsColumn("Prog_No_EndWith", "CSI_auth.tbl_csiothersettings", connectionString))
                    {
                        sqlCmd.Clear();
                        sqlCmd.Append("UPDATE CSI_auth.tbl_csiothersettings SET ProgramNumber_FilterEnd = Prog_No_EndWith; ");
                        sqlCmd.Append("ALTER TABLE `csi_auth`.`tbl_csiothersettings` DROP COLUMN `Prog_No_EndWith`; ");
                        ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                    }
                }

                if (!ExistsColumn("Operation_Available", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("Operation_Available", "CSI_auth.tbl_csiothersettings", "tinyint DEFAULT 0 AFTER `ProgramNumber_FilterEnd`", connectionString);
                }

                if (!ExistsColumn("Operation_FilterStart", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("Operation_FilterStart", "CSI_auth.tbl_csiothersettings", "varchar(45) DEFAULT NULL AFTER `Operation_Available`", connectionString);
                }

                if (!ExistsColumn("Operation_FilterEnd", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("Operation_FilterEnd", "CSI_auth.tbl_csiothersettings", "varchar(45) DEFAULT NULL AFTER `Operation_FilterStart`", connectionString);
                }


                if (!ExistsColumn("PartRequired_Variable", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("PartRequired_Variable", "CSI_auth.tbl_csiothersettings", "varchar(100) DEFAULT NULL AFTER `Operation_FilterEnd`", connectionString);

                    if (ExistsColumn("Part_Req_Var", "CSI_auth.tbl_csiothersettings", connectionString))
                    {
                        sqlCmd.Clear();
                        sqlCmd.Append("UPDATE CSI_auth.tbl_csiothersettings SET PartRequired_Variable = Part_Req_Var; ");
                        sqlCmd.Append("ALTER TABLE `csi_auth`.`tbl_csiothersettings` DROP COLUMN `Part_Req_Var`; ");
                        ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                    }
                }

                if (ExistsColumn("Part_Req_Value", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    ExecuteNonQuery("ALTER TABLE `csi_auth`.`tbl_csiothersettings` DROP COLUMN `Part_Req_Value`; ", connectionString);
                }


                if (!ExistsColumn("PartCount_Variable", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("PartCount_Variable", "CSI_auth.tbl_csiothersettings", "varchar(100) DEFAULT NULL AFTER `PartRequired_Variable`", connectionString);

                    if (ExistsColumn("Part_Count_Var", "CSI_auth.tbl_csiothersettings", connectionString))
                    {
                        sqlCmd.Clear();
                        sqlCmd.Append("UPDATE CSI_auth.tbl_csiothersettings SET PartCount_Variable = Part_Count_Var; ");
                        sqlCmd.Append("ALTER TABLE `csi_auth`.`tbl_csiothersettings` DROP COLUMN `Part_Count_Var`; ");
                        ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                    }
                }

                if (ExistsColumn("Part_Count_Value", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    ExecuteNonQuery("ALTER TABLE `csi_auth`.`tbl_csiothersettings` DROP COLUMN `Part_Count_Value`; ", connectionString);
                }


                if (!ExistsColumn("Feedrate_Variable", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("Feedrate_Variable", "CSI_auth.tbl_csiothersettings", "varchar(100) DEFAULT NULL AFTER `PartCount_Variable`", connectionString);

                    if (ExistsColumn("FeedRateOver_var", "CSI_auth.tbl_csiothersettings", connectionString))
                    {
                        sqlCmd.Clear();
                        sqlCmd.Append("UPDATE CSI_auth.tbl_csiothersettings SET Feedrate_Variable = FeedRateOver_var; ");
                        sqlCmd.Append("ALTER TABLE `csi_auth`.`tbl_csiothersettings` DROP COLUMN `FeedRateOver_var`; ");
                        ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                    }
                }

                if (ExistsColumn("FeedRateOver_Value", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    sqlCmd.Clear();
                    sqlCmd.Append("ALTER TABLE `csi_auth`.`tbl_csiothersettings` DROP COLUMN `FeedRateOver_Value`; ");
                    ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                }

                if (!ExistsColumn("Feedrate_Min", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("Feedrate_Min", "CSI_auth.tbl_csiothersettings", "varchar(45) DEFAULT NULL AFTER `Feedrate_Variable`", connectionString);

                    if (ExistsColumn("Feed_MIN", "CSI_auth.tbl_csiothersettings", connectionString))
                    {
                        sqlCmd.Clear();
                        sqlCmd.Append("UPDATE CSI_auth.tbl_csiothersettings SET Feedrate_Min = Feed_MIN; ");
                        sqlCmd.Append("ALTER TABLE `csi_auth`.`tbl_csiothersettings` DROP COLUMN `Feed_MIN`; ");
                        ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                    }
                }

                if (!ExistsColumn("Feedrate_Max", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("Feedrate_Max", "CSI_auth.tbl_csiothersettings", "varchar(45) DEFAULT NULL AFTER `Feedrate_Min`", connectionString);

                    if (ExistsColumn("Feed_MAX", "CSI_auth.tbl_csiothersettings", connectionString))
                    {
                        sqlCmd.Clear();
                        sqlCmd.Append("UPDATE CSI_auth.tbl_csiothersettings SET Feedrate_Max = Feed_MAX; ");
                        sqlCmd.Append("ALTER TABLE `csi_auth`.`tbl_csiothersettings` DROP COLUMN `Feed_MAX`; ");
                        ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                    }
                }


                if (!ExistsColumn("Rapid_Variable", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("Rapid_Variable", "CSI_auth.tbl_csiothersettings", "varchar(100) DEFAULT NULL AFTER `Feedrate_Max`", connectionString);

                    if (ExistsColumn("RapidOver_var", "CSI_auth.tbl_csiothersettings", connectionString))
                    {
                        sqlCmd.Clear();
                        sqlCmd.Append("UPDATE CSI_auth.tbl_csiothersettings SET Rapid_Variable = RapidOver_var; ");
                        sqlCmd.Append("ALTER TABLE `csi_auth`.`tbl_csiothersettings` DROP COLUMN `RapidOver_var`; ");
                        ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                    }
                }

                if (ExistsColumn("RapidOver_Value", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    ExecuteNonQuery("ALTER TABLE `csi_auth`.`tbl_csiothersettings` DROP COLUMN `RapidOver_Value`; ", connectionString);
                }


                if (!ExistsColumn("Spindle_Variable", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("Spindle_Variable", "CSI_auth.tbl_csiothersettings", "varchar(100) DEFAULT NULL AFTER `Rapid_Max`", connectionString);

                    if (ExistsColumn("SpindleOver_var", "CSI_auth.tbl_csiothersettings", connectionString))
                    {
                        sqlCmd.Clear();
                        sqlCmd.Append("UPDATE CSI_auth.tbl_csiothersettings SET Spindle_Variable = SpindleOver_var; ");
                        sqlCmd.Append("ALTER TABLE `csi_auth`.`tbl_csiothersettings` DROP COLUMN `SpindleOver_var`; ");
                        ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                    }
                }

                if (ExistsColumn("SpindleOver_Value", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    ExecuteNonQuery("ALTER TABLE `csi_auth`.`tbl_csiothersettings` DROP COLUMN `SpindleOver_Value`; ", connectionString);
                }



                if (!ExistsColumn("ActivePallet_Var", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("ActivePallet_Var", "CSI_auth.tbl_csiothersettings", "varchar(100) DEFAULT NULL", connectionString);
                }

                if (ExistsColumn("ActivePallet_Value", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    ExecuteNonQuery("ALTER TABLE `csi_auth`.`tbl_csiothersettings` DROP COLUMN `ActivePallet_Value`; ", connectionString);
                }

                if (!ExistsColumn("ActivePallet_StartWith", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("ActivePallet_StartWith", "CSI_auth.tbl_csiothersettings", "varchar(100) DEFAULT NULL", connectionString);
                }

                if (!ExistsColumn("ActivePallet_EndWith", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("ActivePallet_EndWith", "CSI_auth.tbl_csiothersettings", "varchar(100) DEFAULT NULL", connectionString);
                }

                if (!ExistsColumn("ActivePallet_ToMU", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("ActivePallet_ToMU", "CSI_auth.tbl_csiothersettings", "tinyint(4) DEFAULT '0'", connectionString);
                    ExecuteNonQuery("UPDATE CSI_auth.tbl_csiothersettings SET ActivePallet_ToMU = 0;");
                }

                if (!ExistsColumn("WarningPressure", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("WarningPressure", "CSI_auth.tbl_csiothersettings", "INT COLLATE utf8_unicode_ci DEFAULT 0 AFTER `ActivePallet_ToMU`", connectionString);
                }

                if (!ExistsColumn("CriticalPressure", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("CriticalPressure", "CSI_auth.tbl_csiothersettings", "INT COLLATE utf8_unicode_ci DEFAULT 0 AFTER `WarningPressure`", connectionString);
                }

                if (!ExistsColumn("WarningTemperature", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("WarningTemperature", "CSI_auth.tbl_csiothersettings", "INT COLLATE utf8_unicode_ci DEFAULT 0 AFTER `CriticalPressure`", connectionString);
                }

                if (!ExistsColumn("CriticalTemperature", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("CriticalTemperature", "CSI_auth.tbl_csiothersettings", "INT COLLATE utf8_unicode_ci DEFAULT 0 AFTER `WarningTemperature`", connectionString);
                }

                if (!ExistsColumn("MCSDelay", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("MCSDelay", "CSI_auth.tbl_csiothersettings", "INT COLLATE utf8_unicode_ci DEFAULT 0 AFTER `CriticalTemperature`", connectionString);
                }

                if (!ExistsColumn("DelayScale", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("DelayScale", "CSI_auth.tbl_csiothersettings", "varchar(3) COLLATE utf8_unicode_ci DEFAULT 'sec' AFTER `MCSDelay`", connectionString);
                }

                if (!ExistsColumn("EnableMCS", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("EnableMCS", "CSI_auth.tbl_csiothersettings", "tinyint(4) DEFAULT '0'", connectionString);
                    ExecuteNonQuery("UPDATE CSI_auth.tbl_csiothersettings SET EnableMCS = 0;", connectionString);
                }

                if (!ExistsColumn("DelayForCycleOff", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("DelayForCycleOff", "CSI_auth.tbl_csiothersettings", "INT DEFAULT 0", connectionString);
                }

                if (!ExistsColumn("DelayForCycleOffScale", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("DelayForCycleOffScale", "CSI_auth.tbl_csiothersettings", "varchar(3) DEFAULT 'sec'", connectionString);
                }

                if (!ExistsColumn("SaveDataRaw", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("SaveDataRaw", "CSI_auth.tbl_csiothersettings", "tinyint(4) DEFAULT '0'", connectionString);
                    ExecuteNonQuery("UPDATE CSI_auth.tbl_csiothersettings SET SaveDataRaw = 0;", connectionString);
                }

                if (!ExistsColumn("COnDuringSetup", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("COnDuringSetup", "CSI_auth.tbl_csiothersettings", "tinyint(4) DEFAULT '1' AFTER `SaveDataRaw`", connectionString);
                    ExecuteNonQuery("UPDATE CSI_auth.tbl_csiothersettings SET COnDuringSetup = 1;", connectionString);
                }

                if (!ExistsColumn("SaveProdOnly", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    AddColumn("SaveProdOnly", "CSI_auth.tbl_csiothersettings", "INT DEFAULT 0", connectionString);
                    ExecuteNonQuery("UPDATE CSI_auth.tbl_csiothersettings SET SaveProdOnly = 1;", connectionString);
                }

                if (!ExistsColumn("ConnectorID", "CSI_auth.tbl_csiothersettings", connectionString))
                {
                    sqlCmd.Clear();
                    sqlCmd.Append("ALTER TABLE CSI_auth.tbl_csiothersettings ADD ConnectorID INT FIRST;");

                    sqlCmd.Append($"ALTER TABLE `csi_auth`.`tbl_csiothersettings`                   ");
                    sqlCmd.Append($"  CHANGE COLUMN `ConnectorID` `ConnectorID` INT(11) NOT NULL,   ");
                    sqlCmd.Append($"  DROP PRIMARY KEY,                                             ");
                    sqlCmd.Append($"  ADD PRIMARY KEY(`ConnectorID`);                               ");

                    ExecuteNonQuery(sqlCmd.ToString(), connectionString);

                    DataTable connectors = GetDataTable("SELECT id, MachineName, MachineIP, eNETMachineName FROM CSI_auth.tbl_CSIConnector");

                    foreach (DataRow connector in connectors.Rows)
                    {
                        sqlCmd.Clear();
                        sqlCmd.Append($"UPDATE CSI_auth.tbl_csiothersettings                            ");
                        sqlCmd.Append($" SET                                                            ");
                        sqlCmd.Append($"     ConnectorID = {connector["id"].ToString()}                 ");
                        sqlCmd.Append($" WHERE                                                          ");
                        sqlCmd.Append($"     Machine_Name = '{connector["eNETMachineName"].ToString()}' ");
                        sqlCmd.Append($" AND IP_Address   = '{connector["MachineIP"].ToString()}';      ");

                        ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Auth_CsiOtherSettings_Table - {ex.Message}", ex);
            }

            AddAuthTableBckp("tbl_csiothersettings");
        }


        public static void Validate_Auth_CsiOtherSettingsValues_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS CSI_auth.tbl_csiothersettingsvalues  ");
            sqlCmd.Append("(                                                               ");
            sqlCmd.Append("   `ConnectorID`         int          ,                         ");
            sqlCmd.Append("   `Machine_Name`        varchar(200) NOT NULL,                 ");
            sqlCmd.Append("   `PartNumber_Value`    varchar(100) DEFAULT NULL,             ");
            sqlCmd.Append("   `ProgramNumber_Value` varchar(100) DEFAULT NULL,             ");
            sqlCmd.Append("   `Operation_Value`     varchar(100) DEFAULT NULL,             ");
            sqlCmd.Append("   `PartRequired_Value`  INT          DEFAULT 0,                ");
            sqlCmd.Append("   `PartCount_Value`     INT          DEFAULT 0,                ");
            sqlCmd.Append("   `Feedrate_Value`      varchar(10)  DEFAULT NULL,             ");
            sqlCmd.Append("   `Spindle_Value`       varchar(10)  DEFAULT NULL,             ");
            sqlCmd.Append("   `Rapid_Value`         varchar(10)  DEFAULT NULL,             ");
            sqlCmd.Append("   `ActivePallet_Value`  varchar(10)  DEFAULT NULL,             ");
            sqlCmd.Append(" PRIMARY KEY(`ConnectorID`)                                     ");
            sqlCmd.Append(")                                                               ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);

                if (!ExistsColumn("PartNumber_Value", "CSI_auth.tbl_csiothersettingsvalues", connectionString))
                {
                    AddColumn("PartNumber_Value", "CSI_auth.tbl_csiothersettingsvalues", "varchar(100) DEFAULT NULL AFTER Machine_Name", connectionString);

                    if (ExistsColumn("Part_No_Value", "CSI_auth.tbl_csiothersettingsvalues", connectionString))
                    {
                        ExecuteNonQuery("ALTER TABLE `csi_auth`.`tbl_csiothersettingsvalues` DROP COLUMN `Part_No_Value`; ", connectionString);
                    }
                }

                if (!ExistsColumn("ProgramNumber_Value", "CSI_auth.tbl_csiothersettingsvalues", connectionString))
                {
                    AddColumn("ProgramNumber_Value", "CSI_auth.tbl_csiothersettingsvalues", "varchar(100) DEFAULT NULL AFTER PartNumber_Value", connectionString);

                    if (ExistsColumn("Prog_No_Value", "CSI_auth.tbl_csiothersettingsvalues", connectionString))
                    {
                        ExecuteNonQuery("ALTER TABLE `csi_auth`.`tbl_csiothersettingsvalues` DROP COLUMN `Prog_No_Value`; ", connectionString);
                    }
                }

                if (!ExistsColumn("Operation_Value", "CSI_auth.tbl_csiothersettingsvalues", connectionString))
                {
                    AddColumn("Operation_Value", "CSI_auth.tbl_csiothersettingsvalues", "varchar(100) DEFAULT NULL AFTER ProgramNumber_Value", connectionString);
                }

                if (!ExistsColumn("PartRequired_Value", "CSI_auth.tbl_csiothersettingsvalues", connectionString))
                {
                    AddColumn("PartRequired_Value", "CSI_auth.tbl_csiothersettingsvalues", "INT DEFAULT 0 AFTER Operation_Value", connectionString);

                    if (ExistsColumn("Part_Req_Value", "CSI_auth.tbl_csiothersettingsvalues", connectionString))
                    {
                        ExecuteNonQuery("ALTER TABLE `csi_auth`.`tbl_csiothersettingsvalues` DROP COLUMN `Part_Req_Value`; ", connectionString);
                    }
                }

                if (!ExistsColumn("PartCount_Value", "CSI_auth.tbl_csiothersettingsvalues", connectionString))
                {
                    AddColumn("PartCount_Value", "CSI_auth.tbl_csiothersettingsvalues", "INT DEFAULT 0 AFTER PartRequired_Value", connectionString);

                    if (ExistsColumn("Part_Count_Value", "CSI_auth.tbl_csiothersettingsvalues", connectionString))
                    {
                        ExecuteNonQuery("ALTER TABLE `csi_auth`.`tbl_csiothersettingsvalues` DROP COLUMN `Part_Count_Value`; ", connectionString);
                    }
                }

                if (!ExistsColumn("Feedrate_Value", "CSI_auth.tbl_csiothersettingsvalues", connectionString))
                {
                    AddColumn("Feedrate_Value", "CSI_auth.tbl_csiothersettingsvalues", "varchar(10) DEFAULT NULL AFTER PartCount_Value", connectionString);

                    if (ExistsColumn("FeedrateOver_Value", "CSI_auth.tbl_csiothersettingsvalues", connectionString))
                    {
                        ExecuteNonQuery("ALTER TABLE `csi_auth`.`tbl_csiothersettingsvalues` DROP COLUMN `FeedrateOver_Value`; ", connectionString);
                    }
                }

                if (!ExistsColumn("Spindle_Value", "CSI_auth.tbl_csiothersettingsvalues", connectionString))
                {
                    AddColumn("Spindle_Value", "CSI_auth.tbl_csiothersettingsvalues", "varchar(10) DEFAULT NULL AFTER Feedrate_Value", connectionString);

                    if (ExistsColumn("SpindleOver_Value", "CSI_auth.tbl_csiothersettingsvalues", connectionString))
                    {
                        ExecuteNonQuery("ALTER TABLE `csi_auth`.`tbl_csiothersettingsvalues` DROP COLUMN `SpindleOver_Value`; ", connectionString);
                    }
                }

                if (!ExistsColumn("Rapid_Value", "CSI_auth.tbl_csiothersettingsvalues", connectionString))
                {
                    AddColumn("Rapid_Value", "CSI_auth.tbl_csiothersettingsvalues", "varchar(10) DEFAULT NULL AFTER Spindle_Value", connectionString);

                    if (ExistsColumn("RapidOver_Value", "CSI_auth.tbl_csiothersettingsvalues", connectionString))
                    {
                        ExecuteNonQuery("ALTER TABLE `csi_auth`.`tbl_csiothersettingsvalues` DROP COLUMN `RapidOver_Value`; ", connectionString);
                    }
                }


                int qtdSettings = int.Parse(ExecuteScalar("SELECT COUNT(*) AS S FROM csi_auth.tbl_csiothersettings").ToString());
                int qtdValues = int.Parse(ExecuteScalar("SELECT COUNT(*) AS V FROM csi_auth.tbl_csiothersettingsvalues").ToString());

                sqlCmd.Clear();
                if (qtdSettings > qtdValues)
                {
                    sqlCmd.Append($"INSERT IGNORE INTO csi_auth.tbl_csiothersettingsvalues");
                    sqlCmd.Append($"    SELECT                                            ");
                    sqlCmd.Append($"        ConnectorId,                                  ");
                    sqlCmd.Append($"        Machine_Name,                                 ");
                    sqlCmd.Append($"        '',                                           ");
                    sqlCmd.Append($"        '',                                           ");
                    sqlCmd.Append($"        '',                                           ");
                    sqlCmd.Append($"        0,                                            ");
                    sqlCmd.Append($"        0,                                            ");
                    sqlCmd.Append($"        '',                                           ");
                    sqlCmd.Append($"        '',                                           ");
                    sqlCmd.Append($"        '',                                           ");
                    sqlCmd.Append($"        ''                                            ");
                    sqlCmd.Append($"    FROM                                              ");
                    sqlCmd.Append($"        csi_auth.tbl_csiothersettings;                ");

                    ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                }

                sqlCmd.Clear();
                sqlCmd.Append("CREATE OR REPLACE                                                        ");
                sqlCmd.Append("    ALGORITHM = UNDEFINED                                                ");
                sqlCmd.Append("    DEFINER = `root`@`localhost`                                         ");
                sqlCmd.Append("    SQL SECURITY DEFINER                                                 ");
                sqlCmd.Append("VIEW `csi_auth`.`view_csiothersettings`  AS                              ");
                sqlCmd.Append("    SELECT                                                               ");
                sqlCmd.Append("        `s`.`ConnectorID`                AS `ConnectorID`,               ");
                sqlCmd.Append("        `s`.`Machine_Name`               AS `Machine_Name`,              ");
                sqlCmd.Append("        `s`.`IP_Address`                 AS `IP_Address`,                ");
                sqlCmd.Append("        `s`.`PartNumber_Variable`        AS `PartNumber_Variable`,       ");
                sqlCmd.Append("        `v`.`PartNumber_Value`           AS `PartNumber_Value`,          ");
                sqlCmd.Append("        `s`.`PartNumber_Prefix1`         AS `PartNumber_Prefix1`,        ");
                sqlCmd.Append("        `s`.`PartNumber_Filter1Start`    AS `PartNumber_Filter1Start`,   ");
                sqlCmd.Append("        `s`.`PartNumber_Filter1End`      AS `PartNumber_Filter1End`,     ");
                sqlCmd.Append("        `s`.`PartNumber_Filter2Apply`    AS `PartNumber_Filter2Apply`,   ");
                sqlCmd.Append("        `s`.`PartNumber_Prefix2`         AS `PartNumber_Prefix2`,        ");
                sqlCmd.Append("        `s`.`PartNumber_Filter2Start`    AS `PartNumber_Filter2Start`,   ");
                sqlCmd.Append("        `s`.`PartNumber_Filter2End`      AS `PartNumber_Filter2End`,     ");
                sqlCmd.Append("        `s`.`PartNumber_Filter3Apply`    AS `PartNumber_Filter3Apply`,   ");
                sqlCmd.Append("        `s`.`PartNumber_Prefix3`         AS `PartNumber_Prefix3`,        ");
                sqlCmd.Append("        `s`.`PartNumber_Filter3Start`    AS `PartNumber_Filter3Start`,   ");
                sqlCmd.Append("        `s`.`PartNumber_Filter3End`      AS `PartNumber_Filter3End`,     ");
                sqlCmd.Append("        `s`.`ProgramNumber_Variable`     AS `ProgramNumber_Variable`,    ");
                sqlCmd.Append("        `v`.`ProgramNumber_Value`        AS `ProgramNumber_Value`,       ");
                sqlCmd.Append("        `s`.`ProgramNumber_FilterStart`  AS `ProgramNumber_FilterStart`, ");
                sqlCmd.Append("        `s`.`ProgramNumber_FilterEnd`    AS `ProgramNumber_FilterEnd`,   ");
                sqlCmd.Append("        `s`.`Operation_Available`        AS `Operation_Available`,       ");
                sqlCmd.Append("        `v`.`Operation_Value`            AS `Operation_Value`,           ");
                sqlCmd.Append("        `s`.`Operation_FilterStart`      AS `Operation_FilterStart`,     ");
                sqlCmd.Append("        `s`.`Operation_FilterEnd`        AS `Operation_FilterEnd`,       ");
                sqlCmd.Append("        `s`.`PartRequired_Variable`      AS `PartRequired_Variable`,     ");
                sqlCmd.Append("        `v`.`PartRequired_Value`         AS `PartRequired_Value`,        ");
                sqlCmd.Append("        `s`.`PartCount_Variable`         AS `PartCount_Variable`,        ");
                sqlCmd.Append("        `v`.`PartCount_Value`            AS `PartCount_Value`,           ");
                sqlCmd.Append("        `s`.`FeedRate_Variable`          AS `FeedRate_Variable`,         ");
                sqlCmd.Append("        `v`.`Feedrate_Value`             AS `Feedrate_Value`,            ");
                sqlCmd.Append("        `s`.`Feedrate_Min`               AS `Feedrate_Min`,              ");
                sqlCmd.Append("        `s`.`Feedrate_Max`               AS `Feedrate_Max`,              ");
                sqlCmd.Append("        `s`.`Spindle_Variable`           AS `Spindle_Variable`,          ");
                sqlCmd.Append("        `v`.`Spindle_Value`              AS `Spindle_Value`,             ");
                sqlCmd.Append("        `s`.`Spindle_Min`                AS `Spindle_Min`,               ");
                sqlCmd.Append("        `s`.`Spindle_Max`                AS `Spindle_Max`,               ");
                sqlCmd.Append("        `s`.`Rapid_Variable`             AS `Rapid_Variable`,            ");
                sqlCmd.Append("        `v`.`Rapid_Value`                AS `Rapid_Value`,               ");
                sqlCmd.Append("        `s`.`Rapid_Min`                  AS `Rapid_Min`,                 ");
                sqlCmd.Append("        `s`.`Rapid_Max`                  AS `Rapid_Max`,                 ");
                sqlCmd.Append("        `s`.`ActivePallet_Var`           AS `ActivePallet_Var`,          ");
                sqlCmd.Append("        `v`.`ActivePallet_Value`         AS `ActivePallet_Value`,        ");
                sqlCmd.Append("        `s`.`ActivePallet_StartWith`     AS `ActivePallet_StartWith`,    ");
                sqlCmd.Append("        `s`.`ActivePallet_EndWith`       AS `ActivePallet_EndWith`,      ");
                sqlCmd.Append("        `s`.`ActivePallet_ToMU`          AS `ActivePallet_ToMU`,         ");
                sqlCmd.Append("        `s`.`WarningPressure`            AS `WarningPressure`,           ");
                sqlCmd.Append("        `s`.`CriticalPressure`           AS `CriticalPressure`,          ");
                sqlCmd.Append("        `s`.`WarningTemperature`         AS `WarningTemperature`,        ");
                sqlCmd.Append("        `s`.`CriticalTemperature`        AS `CriticalTemperature`,       ");
                sqlCmd.Append("        `s`.`MCSDelay`                   AS `MCSDelay`,                  ");
                sqlCmd.Append("        `s`.`DelayScale`                 AS `DelayScale`,                ");
                sqlCmd.Append("        `s`.`DelayForCycleOff`           AS `DelayForCycleOff`,          ");
                sqlCmd.Append("        `s`.`DelayForCycleOffScale`      AS `DelayForCycleOffScale`,     ");
                sqlCmd.Append("        `s`.`EnableMCS`                  AS `EnableMCS`,                 ");
                sqlCmd.Append("        `s`.`SaveDataRaw`                AS `SaveDataRaw`,               ");
                sqlCmd.Append("        `s`.`COnDuringSetup`             AS `COnDuringSetup`,            ");
                sqlCmd.Append("        `s`.`SaveProdOnly`               AS `SaveProdOnly`               ");
                sqlCmd.Append("    FROM                                                                 ");
                sqlCmd.Append("        (`csi_auth`.`tbl_csiothersettings` `s`                           ");
                sqlCmd.Append("        LEFT JOIN `csi_auth`.`tbl_csiothersettingsvalues` `v`            ");
                sqlCmd.Append("                    ON ((`s`.`ConnectorID` = `v`.`ConnectorID`)))        ");
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Auth_CsiOtherSettingsValues_Table - {ex.Message}", ex);
            }
        }


        private static void Recreate_Auth_CsiOtherSettings_View(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            try
            {
                sqlCmd.Append($"CREATE OR REPLACE VIEW `csi_auth`.`view_csiothersettings` AS       ");
                sqlCmd.Append($"    SELECT                                                         ");
                sqlCmd.Append($"        `s`.`ConnectorID` AS `ConnectorID`,                        ");
                sqlCmd.Append($"        `s`.`Machine_Name` AS `Machine_Name`,                      ");
                sqlCmd.Append($"        `s`.`IP_Address` AS `IP_Address`,                          ");
                sqlCmd.Append($"        `s`.`Part_No_Var` AS `Part_No_Var`,                        ");
                sqlCmd.Append($"        `v`.`Part_No_Value` AS `Part_No_Value`,                    ");
                sqlCmd.Append($"        `s`.`Prog_No_Var` AS `Prog_No_Var`,                        ");
                sqlCmd.Append($"        `v`.`Prog_No_Value` AS `Prog_No_Value`,                    ");
                sqlCmd.Append($"        `s`.`Part_Req_Var` AS `Part_Req_Var`,                      ");
                sqlCmd.Append($"        `v`.`Part_Req_Value` AS `Part_Req_Value`,                  ");
                sqlCmd.Append($"        `s`.`Part_Count_Var` AS `Part_Count_Var`,                  ");
                sqlCmd.Append($"        `v`.`Part_Count_Value` AS `Part_Count_Value`,              ");
                sqlCmd.Append($"        `s`.`FeedRateOver_Var` AS `FeedRateOver_Var`,              ");
                sqlCmd.Append($"        `v`.`FeedRateOver_Value` AS `FeedRateOver_Value`,          ");
                sqlCmd.Append($"        `s`.`SpindleOver_Var` AS `SpindleOver_Var`,                ");
                sqlCmd.Append($"        `v`.`SpindleOver_Value` AS `SpindleOver_Value`,            ");
                sqlCmd.Append($"        `s`.`RapidOver_Var` AS `RapidOver_Var`,                    ");
                sqlCmd.Append($"        `v`.`RapidOver_Value` AS `RapidOver_Value`,                ");
                sqlCmd.Append($"        `s`.`Part_No_StartWith` AS `Part_No_StartWith`,            ");
                sqlCmd.Append($"        `s`.`Part_No_EndWith` AS `Part_No_EndWith`,                ");
                sqlCmd.Append($"        `s`.`Prog_No_StartWith` AS `Prog_No_StartWith`,            ");
                sqlCmd.Append($"        `s`.`Prog_No_EndWith` AS `Prog_No_EndWith`,                ");
                sqlCmd.Append($"        `s`.`Feed_MIN` AS `Feed_MIN`,                              ");
                sqlCmd.Append($"        `s`.`Feed_MAX` AS `Feed_MAX`,                              ");
                sqlCmd.Append($"        `s`.`Spindle_MIN` AS `Spindle_MIN`,                        ");
                sqlCmd.Append($"        `s`.`Spindle_MAX` AS `Spindle_MAX`,                        ");
                sqlCmd.Append($"        `s`.`Rapid_MIN` AS `Rapid_MIN`,                            ");
                sqlCmd.Append($"        `s`.`Rapid_MAX` AS `Rapid_MAX`,                            ");
                sqlCmd.Append($"        `s`.`ActivePallet_Var` AS `ActivePallet_Var`,              ");
                sqlCmd.Append($"        `v`.`ActivePallet_Value` AS `ActivePallet_Value`,          ");
                sqlCmd.Append($"        `s`.`ActivePallet_StartWith` AS `ActivePallet_StartWith`,  ");
                sqlCmd.Append($"        `s`.`ActivePallet_EndWith` AS `ActivePallet_EndWith`,      ");
                sqlCmd.Append($"        `s`.`ActivePallet_ToMU` AS `ActivePallet_ToMU`,            ");
                sqlCmd.Append($"        `s`.`EnableMCS` AS `EnableMCS`,                            ");
                sqlCmd.Append($"        `s`.`SaveDataRaw` AS `SaveDataRaw`,                        ");
                sqlCmd.Append($"        `s`.`COnDuringSetup` AS `COnDuringSetup`                   ");
                sqlCmd.Append($"    FROM                                                           ");
                sqlCmd.Append($"        (`csi_auth`.`tbl_csiothersettings` `s`                     ");
                sqlCmd.Append($"        LEFT JOIN `csi_auth`.`tbl_csiothersettingsvalues` `v` ON ((`s`.`ConnectorID` = `v`.`ConnectorID`))); ");

                ExecuteNonQuery(sqlCmd.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception($"Recreate_Auth_CsiOtherSettings_View - {ex.Message}", ex);
            }
        }


        private static void Recreate_Auth_ConnectorsValues_View(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            try
            {
                sqlCmd.Append($"CREATE OR REPLACE VIEW `csi_auth`.`view_connectors_values` AS ");
                sqlCmd.Append($"    SELECT                                                    ");
                sqlCmd.Append($"        `a`.`Id` AS `Id`,                                     ");
                sqlCmd.Append($"        `a`.`MachineId` AS `MachineId`,                       ");
                sqlCmd.Append($"        `b`.`Feedrate_MIN` AS `Feedrate_MIN`,                 ");
                sqlCmd.Append($"        `b`.`Feedrate_MAX` AS `Feedrate_MAX`,                 ");
                sqlCmd.Append($"        `c`.`FeedRate_Value` AS `FeedRate_Value`,             ");
                sqlCmd.Append($"        `c`.`FeedRate_Value` AS `FeedRateOver_Value`,         ");
                sqlCmd.Append($"        `b`.`Spindle_MIN` AS `Spindle_MIN`,                   ");
                sqlCmd.Append($"        `b`.`Spindle_MAX` AS `Spindle_MAX`,                   ");
                sqlCmd.Append($"        `c`.`Spindle_Value` AS `Spindle_Value`,               ");
                sqlCmd.Append($"        `c`.`Spindle_Value` AS `SpindleOver_Value`,           ");
                sqlCmd.Append($"        `b`.`Rapid_MIN` AS `Rapid_MIN`,                       ");
                sqlCmd.Append($"        `b`.`Rapid_MAX` AS `Rapid_MAX`,                       ");
                sqlCmd.Append($"        `c`.`Rapid_Value` AS `Rapid_Value`,                   ");
                sqlCmd.Append($"        `c`.`Rapid_Value` AS `RapidOver_Value`                ");
                sqlCmd.Append($"    FROM                                                      ");
                sqlCmd.Append($"        ((`csi_auth`.`tbl_csiconnector` `a`                   ");
                sqlCmd.Append($"        LEFT JOIN `csi_auth`.`tbl_csiothersettings` `b` ON ((`a`.`Id` = `b`.`ConnectorID`)))  ");
                sqlCmd.Append($"        JOIN `csi_auth`.`tbl_csiothersettingsvalues` `c` ON ((`a`.`Id` = `c`.`ConnectorID`))) ");

                ExecuteNonQuery(sqlCmd.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception($"Recreate_Auth_ConnectorsValues_View - {ex.Message}", ex);
            }
        }


        private static void Recreate_Auth_MachinesSettingsValues_View(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();
            try
            {
                sqlCmd.Append($"CREATE OR REPLACE VIEW csi_auth.view_machines_settings_values AS  ");
                sqlCmd.Append($"   SELECT                                   ");
                sqlCmd.Append($"    M.id,                                   ");
                sqlCmd.Append($"    M.machine_name,                         ");
                sqlCmd.Append($"    M.EnetMachineName,                      ");
                sqlCmd.Append($"    M.monitoring_filename,                  ");
                sqlCmd.Append($"    M.EnetDept,                             ");
                sqlCmd.Append($"    M.machine_label,                        ");
                sqlCmd.Append($"    M.Monstate,                             ");
                sqlCmd.Append($"    S.CurrentStatus,                        ");
                sqlCmd.Append($"    S.NextStatus,                           ");
                sqlCmd.Append($"    C.Id AS ConnectorId,                    ");
                sqlCmd.Append($"    C.Feedrate_Min AS Feed_MIN,             ");
                sqlCmd.Append($"    C.Feedrate_Max AS Feed_MAX,             ");
                sqlCmd.Append($"    C.Feedrate_Value AS Feed_Value,         ");
                sqlCmd.Append($"    C.Spindle_Min,                          ");
                sqlCmd.Append($"    C.Spindle_Max,                          ");
                sqlCmd.Append($"    C.Spindle_Value AS Spindle_Value,       ");
                sqlCmd.Append($"    C.Rapid_Min,                            ");
                sqlCmd.Append($"    C.Rapid_Max,                            ");
                sqlCmd.Append($"    C.Rapid_Value AS Rapid_Value            ");
                sqlCmd.Append($"FROM                                        ");
                sqlCmd.Append($"    csi_auth.tbl_ehub_conf AS M             ");
                sqlCmd.Append($"    LEFT JOIN csi_auth.tbl_machines_settings  AS S ON M.id = S.MachineId ");
                sqlCmd.Append($"    LEFT JOIN csi_auth.view_connectors_values AS C ON M.id = C.Machineid ");

                ExecuteNonQuery(sqlCmd.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception($"Recreate_Auth_MachinesSettingsValues_View - {ex.Message}", ex);
            }
        }


        /// <summary>
        /// Create the table CSI_auth.tbl_ehub_conf if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Auth_EhubConf_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS CSI_auth.tbl_ehub_conf            ");
            sqlCmd.Append("(                                                            ");
            sqlCmd.Append("   `id`                  int(11) NOT NULL AUTO_INCREMENT,    ");
            sqlCmd.Append("   `monitoring_id`       varchar(20) NOT NULL,               ");
            sqlCmd.Append("   `machine_name`        varchar(200) NOT NULL,              ");
            sqlCmd.Append("   `EnetMachineName`     varchar(100) NOT NULL DEFAULT '',   ");
            sqlCmd.Append("   `machine_label`       varchar(200) NOT NULL,              ");
            sqlCmd.Append("   `Con_type`            int(2) NOT NULL,                    ");
            sqlCmd.Append("   `monitoring_filename` varchar(200) NOT NULL,              ");
            sqlCmd.Append("   `Monstate`            int(2) NOT NULL,                    ");
            sqlCmd.Append("   `MCH_DailyTarget`     int(11) NOT NULL DEFAULT '0',       ");
            sqlCmd.Append("   `MCH_WeeklyTarget`    int(11) NOT NULL DEFAULT '0',       ");
            sqlCmd.Append("   `MCH_MonthlyTarget`   int(11) NOT NULL DEFAULT '0',       ");
            sqlCmd.Append("   `ftpfilename`         varchar(100) NOT NULL,              ");
            sqlCmd.Append("   `CurrentStatus`       varchar(200) NOT NULL,              ");
            sqlCmd.Append("   `CurrentPartNumber`   varchar(200) NOT NULL,              ");
            sqlCmd.Append("   `EnetDept`            varchar(45) NOT NULL DEFAULT ' ',   ");
            sqlCmd.Append("   `MTC_Machine_name`    varchar(200) NOT NULL DEFAULT ' ',  ");
            sqlCmd.Append("   `TH_State`            int(11) NOT NULL DEFAULT '0',       ");
            sqlCmd.Append("   `Redirect`            varchar(10) NOT NULL DEFAULT '0,0', ");
            sqlCmd.Append(" PRIMARY KEY(`id`),                                          ");
            sqlCmd.Append(" UNIQUE KEY `id` (`id`),                                     ");
            sqlCmd.Append(" UNIQUE KEY `monitoring_id` (`monitoring_id`)                ");
            sqlCmd.Append(")                                                            ");
            sqlCmd.Append(" ENGINE = InnoDB DEFAULT CHARSET = latin1;                   ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Auth_EhubConf_Table - {ex.Message}", ex);
            }

            if (!ExistsColumn("TH_State", "CSI_auth.tbl_ehub_conf", connectionString))
                AddColumn("TH_State", "CSI_auth.tbl_ehub_conf", "INT(11) NOT NULL DEFAULT '0' AFTER `MTC_Machine_name`", connectionString);

            if (!ExistsColumn("EnetMachineName", "CSI_auth.tbl_ehub_conf", connectionString))
            {
                AddColumn("EnetMachineName", "CSI_auth.tbl_ehub_conf", "varchar(100) NOT NULL DEFAULT '' AFTER `Machine_name`", connectionString);
                ExecuteNonQuery("UPDATE CSI_auth.tbl_ehub_conf SET EnetMachineName = Machine_name;", connectionString);
            }

            if (!ExistsColumn("Redirect", "CSI_auth.tbl_ehub_conf", connectionString))
                AddColumn("Redirect", "CSI_auth.tbl_ehub_conf", "varchar(10) NOT NULL DEFAULT '0,0'", connectionString);

            AddAuthTableBckp("tbl_ehub_conf");
        }


        public static void Validate_Auth_Machines_Settings_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS CSI_auth.tbl_machines_settings    ");
            sqlCmd.Append("(                                                            ");
            sqlCmd.Append("   `MachineId`           int(11) NOT NULL ,                  ");
            sqlCmd.Append("   `CurrentStatus`       varchar(200) NOT NULL DEFAULT '',   ");
            sqlCmd.Append("   `NextStatus`          varchar(200) NOT NULL DEFAULT '',   ");
            sqlCmd.Append("   `CurrentPartNumber`   varchar(200) NOT NULL DEFAULT '',   ");
            sqlCmd.Append(" PRIMARY KEY(`MachineId`),                                   ");
            sqlCmd.Append(" UNIQUE KEY `MachineId` (`MachineId`)                        ");
            sqlCmd.Append(")                                                            ");
            sqlCmd.Append(" ENGINE = InnoDB DEFAULT CHARSET = latin1;                   ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Auth_Machines_Settings_Table - {ex.Message}", ex);
            }

        }


        /// <summary>
        /// Create the table CSI_auth.tbl_emailreports if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Auth_EmailReports_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS CSI_auth.tbl_emailreports ( ");
            sqlCmd.Append("   `id`           INTEGER      NOT NULL AUTO_INCREMENT,");
            sqlCmd.Append("   `senderemail`  VARCHAR(100) NOT NULL,               ");
            sqlCmd.Append("   `senderpwd`    VARCHAR(100),                        ");
            sqlCmd.Append("   `smtphost`     VARCHAR(255),                        ");
            sqlCmd.Append("   `smtpport`     INTEGER     ,                        ");
            sqlCmd.Append("   `requirecred`  BOOLEAN     ,                        ");
            sqlCmd.Append("   `usessl`       BOOLEAN NOT NULL DEFAULT 1,          ");
            sqlCmd.Append("   `isdefault`    BOOLEAN NOT NULL DEFAULT 0,          ");
            sqlCmd.Append("   `isused`       BOOLEAN NOT NULL DEFAULT 0,          ");
            sqlCmd.Append(" PRIMARY KEY (`id`)                         ,          ");
            sqlCmd.Append(" UNIQUE  KEY `senderemail_UNIQUE` (`senderemail`)   ); ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Auth_EmailReports_Table - {ex.Message}", ex);
            }

            if (!ExistsColumn("id", "CSI_auth.tbl_emailreports", connectionString))
            {
                ExecuteNonQuery("DROP TABLE CSI_auth.tbl_emailreports;", connectionString);
                Validate_Auth_EmailReports_Table(connectionString);
            }

            if (!ExistsColumn("isused", "CSI_auth.tbl_emailreports", connectionString))
                AddColumn("isused", "CSI_auth.tbl_emailreports", "BOOLEAN NOT NULL DEFAULT 0", connectionString);

            if (!ExistsColumn("usessl", "CSI_auth.tbl_emailreports", connectionString))
                AddColumn("usessl", "CSI_auth.tbl_emailreports", "BOOLEAN NOT NULL DEFAULT 1", connectionString);


            if (!ExistsColumn("isdefault", "CSI_auth.tbl_emailreports", connectionString))
            {
                AddColumn("isdefault", "CSI_auth.tbl_emailreports", "BOOLEAN NOT NULL DEFAULT 0", connectionString);

                if (ExecuteNonQuery($"UPDATE CSI_auth.tbl_emailreports Set isdefault = 1 WHERE senderemail ='reports@csiflex.com';") == 0)
                {
                    InsertDefaultEmailReport("reports@csiflex.com", true);
                    InsertDefaultEmailReport("custom@csiflex.com", false);
                }
            }
            else
            {
                var emails = GetDataTable("SELECT * FROM CSI_auth.tbl_emailreports");

                if (emails.Rows.Count == 0)
                {
                    InsertDefaultEmailReport("reports@csiflex.com", true);
                    InsertDefaultEmailReport("custom@csiflex.com", false);
                }
            }
        }


        private static void InsertDefaultEmailReport(string senderemail, bool defaul)
        {
            StringBuilder sqlInsert = new StringBuilder();
            HashHelper hashHelper = new HashHelper();

            int isdefault = defaul ? 1 : 0;
            string email_password = hashHelper.AES_Encrypt("t4Solutions", "pass");

            sqlInsert.Append($"INSERT IGNORE INTO CSI_auth.tbl_emailreports ");
            sqlInsert.Append($"(                               ");
            sqlInsert.Append($"   `senderemail`,               ");
            sqlInsert.Append($"   `senderpwd`,                 ");
            sqlInsert.Append($"   `smtphost`,                  ");
            sqlInsert.Append($"   `smtpport`,                  ");
            sqlInsert.Append($"   `requirecred`,               ");
            sqlInsert.Append($"   `usessl`,                    ");
            sqlInsert.Append($"   `isdefault`,                 ");
            sqlInsert.Append($"   `isused`                     ");
            sqlInsert.Append($")                               ");
            sqlInsert.Append($"VALUES                          ");
            sqlInsert.Append($"(                               ");
            sqlInsert.Append($"   '{ senderemail }',           ");
            sqlInsert.Append($"   '{ email_password }',        ");
            sqlInsert.Append($"   'smtp.gmail.com',            ");
            sqlInsert.Append($"   '587', '1', '1',             ");
            sqlInsert.Append($"   '{ isdefault }',             ");
            sqlInsert.Append($"   '{ isdefault }'              ");
            sqlInsert.Append($")                               ");

            ExecuteNonQuery(sqlInsert.ToString());
        }


        /// <summary>
        /// Create the table CSI_auth.tbl_file_status if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Auth_FileStatus_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS CSI_auth.tbl_file_status ");
            sqlCmd.Append("(                                                   ");
            sqlCmd.Append("   `num_id_`    int(11) NOT NULL AUTO_INCREMENT,    ");
            sqlCmd.Append("   `log_date`   datetime DEFAULT NULL,              ");
            sqlCmd.Append("   `file_name`  varchar(255) DEFAULT NULL,          ");
            sqlCmd.Append("   `file_hash`  varbinary(1000) DEFAULT NULL,       ");
            sqlCmd.Append("  PRIMARY KEY(num_id_),                             ");
            sqlCmd.Append("  UNIQUE  KEY(num_id_)                              ");
            sqlCmd.Append(")                                                   ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Auth_FileStatus_Table - {ex.Message}", ex);
            }
        }


        /// <summary>
        /// Create the table CSI_auth.tbl_mach_status if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Auth_MachStatus_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS CSI_auth.tbl_mach_status ");
            sqlCmd.Append("(                                                   ");
            sqlCmd.Append("   `Id`         int(11) NOT NULL AUTO_INCREMENT,    ");
            sqlCmd.Append("   `MachineId`  int(11) NOT NULL,                   ");
            sqlCmd.Append("   `Status`     varchar(45) NOT NULL,               ");
            sqlCmd.Append("  PRIMARY KEY (`Id`),                               ");
            sqlCmd.Append("  UNIQUE KEY `Id_UNIQUE` (`Id`),                    ");
            sqlCmd.Append("  UNIQUE KEY `MachId_Status_Unique` (`MachineId`,`Status`) ");
            sqlCmd.Append(")                                                   ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Auth_MachStatus_Table - {ex.Message}", ex);
            }
        }


        public static void Validate_Auth_License_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS CSI_auth.tbl_license ");
            sqlCmd.Append("(                                               ");
            sqlCmd.Append("   `UniqueId`          char(36),                ");
            sqlCmd.Append("   `CompanyId`         char(36),                ");
            sqlCmd.Append("   `ProductId`         int,                     ");
            sqlCmd.Append("   `ProductName`       varchar(100),            ");
            sqlCmd.Append("   `LicensesQuantity`  int,                     ");
            sqlCmd.Append("   `CompanyName`       varchar(100),            ");
            sqlCmd.Append("   `ComputerName`      varchar(100),            ");
            sqlCmd.Append("   `ComputerId`        varchar(100),            ");
            sqlCmd.Append("   `ContactName`       varchar(100),            ");
            sqlCmd.Append("   `ContactEmail`      varchar(255),            ");
            sqlCmd.Append("   `ContactPhone`      varchar(100),            ");
            sqlCmd.Append("   `LicenseType`       varchar(100),            ");
            sqlCmd.Append("   `LicenseDate`       datetime,                ");
            sqlCmd.Append("   `ExpiryDate`        datetime,                ");
            sqlCmd.Append("   `HashCode`          varchar(255),            ");
            sqlCmd.Append("   `LicenseStatus`     varchar(100),            ");
            sqlCmd.Append("  PRIMARY KEY(UniqueId)                         ");
            sqlCmd.Append(")                                               ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);

                if (!ExistsColumn("ProductId", "CSI_auth.tbl_license", connectionString))
                {
                    AddColumn("ProductId", "CSI_auth.tbl_license", "int AFTER `CompanyId`", connectionString);

                    sqlCmd.Clear();
                    sqlCmd.Append("UPDATE CSI_auth.tbl_license SET ProductId = CASE       ");
                    sqlCmd.Append("     WHEN ProductName = 'CSIFLEX Server'        THEN 1 ");
                    sqlCmd.Append("     WHEN ProductName = 'CSIFLEX Focas/MTC'     THEN 2 ");
                    sqlCmd.Append("     WHEN ProductName = 'CSIFLEX Client.Server' THEN 3 ");
                    sqlCmd.Append("     END                                             ; ");
                    ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Auth_License_Table - {ex.Message}", ex);
            }

            AddAuthTableBckp("tbl_license");
        }


        /// <summary>
        /// Create the table CSI_auth.tbl_mtcfocasconditions if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Auth_MtcFocasConditions_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS CSI_auth.tbl_mtcfocasconditions            ");
            sqlCmd.Append("(                                                                     ");
            sqlCmd.Append("   `ConnectorID`       int          ,                                 ");
            sqlCmd.Append("   `Machine_Name`      varchar(200) COLLATE utf8_unicode_ci Not NULL, ");
            sqlCmd.Append("   `IP_Address`        varchar(100) COLLATE utf8_unicode_ci Not NULL, ");
            sqlCmd.Append("   `Status`            varchar(100) COLLATE utf8_unicode_ci Not NULL, ");
            sqlCmd.Append("   `Condition`         varchar(500) COLLATE utf8_unicode_ci Not NULL, ");
            sqlCmd.Append("   `Machine_Type`      varchar(100) COLLATE utf8_unicode_ci Not NULL, ");
            sqlCmd.Append("   `delay`             varchar(50)  COLLATE utf8_unicode_ci Not NULL, ");
            sqlCmd.Append("   `delayScale`        varchar(3)   COLLATE utf8_unicode_ci DEFAULT 'sec', ");
            sqlCmd.Append("   `CsdOnSetup`        tinyint(4)   DEFAULT '0',                      ");
            sqlCmd.Append("   `StatusDisabled`    tinyint(4)   DEFAULT '0',                      ");
            sqlCmd.Append(" UNIQUE KEY `unique_status_for_machine` (`Machine_Name`,`Status`),    ");
            sqlCmd.Append(" UNIQUE KEY `machine_condition_exists`  (`Machine_Name`,`Condition`)  ");
            sqlCmd.Append(")                                                                     ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);

                if (!ExistsColumn("ConnectorID", "CSI_auth.tbl_mtcfocasconditions"))
                {
                    sqlCmd.Clear();
                    sqlCmd.Append("ALTER TABLE CSI_auth.tbl_mtcfocasconditions ADD ConnectorID INT FIRST;");
                    ExecuteNonQuery(sqlCmd.ToString(), connectionString);

                    DataTable connectors = GetDataTable("SELECT id, MachineName, MachineIP, eNETMachineName FROM CSI_auth.tbl_CSIConnector");

                    foreach (DataRow connector in connectors.Rows)
                    {
                        sqlCmd.Clear();
                        sqlCmd.Append($"UPDATE CSI_auth.tbl_mtcfocasconditions                          ");
                        sqlCmd.Append($" SET                                                            ");
                        sqlCmd.Append($"     ConnectorID = {connector["id"].ToString()}                 ");
                        sqlCmd.Append($" WHERE                                                          ");
                        sqlCmd.Append($"     Machine_Name = '{connector["eNETMachineName"].ToString()}' ");
                        sqlCmd.Append($" AND IP_Address   = '{connector["MachineIP"].ToString()}';      ");

                        ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                    }
                }

                if (!ExistsColumn("delayScale", "CSI_auth.tbl_mtcfocasconditions", connectionString))
                {
                    AddColumn("delayScale", "CSI_auth.tbl_mtcfocasconditions", "varchar(3) COLLATE utf8_unicode_ci DEFAULT 'sec' AFTER `delay`", connectionString);
                    ExecuteNonQuery("UPDATE CSI_auth.tbl_mtcfocasconditions SET delayScale = 'sec';");
                }

                if (!ExistsColumn("CsdOnSetup", "CSI_auth.tbl_mtcfocasconditions", connectionString))
                {
                    AddColumn("CsdOnSetup", "CSI_auth.tbl_mtcfocasconditions", "tinyint(4) DEFAULT '0' AFTER `delayScale`", connectionString);
                    ExecuteNonQuery("UPDATE CSI_auth.tbl_mtcfocasconditions SET CsdOnSetup = 0;");
                }

                if (!ExistsColumn("StatusDisabled", "CSI_auth.tbl_mtcfocasconditions", connectionString))
                {
                    AddColumn("StatusDisabled", "CSI_auth.tbl_mtcfocasconditions", "tinyint(4) DEFAULT '0'", connectionString);
                    ExecuteNonQuery("UPDATE CSI_auth.tbl_mtcfocasconditions SET StatusDisabled = 0;");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Auth_MtcFocasConditions_Table - {ex.Message}", ex);
            }

            AddAuthTableBckp("tbl_mtcfocasconditions");
        }


        /// <summary>
        /// Create the table CSI_auth.tbl_serviceconfig if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Auth_ServiceConfig_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS CSI_auth.tbl_serviceconfig ");
            sqlCmd.Append("(                                                     ");
            sqlCmd.Append("   `loadingAsCON`  BOOLEAN                            ");
            sqlCmd.Append(");                                                    ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Auth_ServiceConfig_Table - {ex.Message}", ex);
            }

            DataTable dt = GetDataTable("SELECT * FROM CSI_auth.tbl_serviceconfig");
            if (dt.Rows.Count == 0)
                ExecuteNonQuery("INSERT IGNORE INTO CSI_auth.tbl_serviceconfig VALUES ( False )");
        }


        /// <summary>
        /// Create the table CSI_auth.tbl_updatestatus if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Auth_UpdateStatus_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS CSI_auth.tbl_updatestatus ");
            sqlCmd.Append("(                                                    ");
            sqlCmd.Append("   `initialdbload`  BOOLEAN                          ");
            sqlCmd.Append(")                                                    ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Auth_UpdateStatus_Table - {ex.Message}", ex);
            }

            DataTable dt = GetDataTable("SELECT * FROM CSI_auth.tbl_updatestatus");
            if (dt.Rows.Count == 0)
                ExecuteNonQuery("INSERT IGNORE INTO CSI_auth.tbl_updatestatus VALUES ( False )");
        }


        /// <summary>
        /// Create the table CSI_auth.Users if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Auth_Users_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS CSI_auth.Users  (                ");
            sqlCmd.Append("   Id                 INT          NOT NULL AUTO_INCREMENT, ");
            sqlCmd.Append("   username_          VARCHAR(100) NOT NULL,                ");
            sqlCmd.Append("   Name_              VARCHAR(100) NOT NULL,                ");
            sqlCmd.Append("   firstname_         VARCHAR(100),                         ");
            sqlCmd.Append("   displayname        VARCHAR(100),                         ");
            sqlCmd.Append("   password_          VARCHAR(255),                         ");
            sqlCmd.Append("   salt_              VARCHAR(255),                         ");
            sqlCmd.Append("   email_             VARCHAR(100),                         ");
            sqlCmd.Append("   usertype           VARCHAR(100),                         ");
            sqlCmd.Append("   machines           TEXT,                                 ");
            sqlCmd.Append("   ovr_interval       INT(11)      DEFAULT 0 ,              ");
            sqlCmd.Append("   refId              VARCHAR(100) DEFAULT NULL,            ");
            sqlCmd.Append("   title              VARCHAR(100),                         ");
            sqlCmd.Append("   dept               VARCHAR(100),                         ");
            sqlCmd.Append("   phoneext           VARCHAR(20) ,                         ");
            sqlCmd.Append("   EditTimeline       TINYINT(4)   NOT NULL DEFAULT 0 ,     ");
            sqlCmd.Append("   EditMasterPartData TINYINT(4)   NOT NULL DEFAULT 0 ,     ");
            sqlCmd.Append("   PRIMARY KEY(`Id`)              ,                         ");
            sqlCmd.Append("   UNIQUE INDEX `Name_UNIQUE`  (`username_` ASC)     ,      ");
            sqlCmd.Append("   UNIQUE INDEX `refid_UNIQUE` (`RefId` ASC)        );      ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Auth_Users_Table1 - {ex.Message}", ex);
            }

            if (!ExistsColumn("Id", "CSI_auth.Users", connectionString))
            {
                try
                {
                    AddColumn("Id", "CSI_auth.Users", "INT FIRST", connectionString);

                    DataTable dtUsers = GetDataTable(@"SELECT username_ FROM CSI_auth.Users", connectionString);
                    int userId = 1;

                    foreach (DataRow row in dtUsers.Rows)
                    {
                        ExecuteNonQuery($"UPDATE CSI_auth.Users SET Id = {userId++} WHERE username_ = '{row["username_"]}'", connectionString);
                    }

                    ExecuteNonQuery("ALTER TABLE CSI_auth.Users CHANGE COLUMN `Id` `Id` INT(11) NOT NULL AUTO_INCREMENT, DROP PRIMARY KEY, ADD PRIMARY KEY(`Id`), ADD UNIQUE INDEX `Name_Unique` (`username_`);", connectionString);
                }
                catch (Exception ex)
                {
                    Log.Error("Validate_Auth_Users_Table2", ex);
                    //throw new Exception($"Validate_Auth_Users_Table2 - {ex.Message}", ex);
                }
            }

            try
            {
                ExecuteNonQuery("UPDATE csi_auth.users SET refid = id WHERE refid = '' OR refid IS NULL;", connectionString);
            }
            catch (Exception ex)
            {
                Log.Error("Validate_Auth_Users_Table3", ex);
                //throw new Exception($"Validate_Auth_Users_Table3 - {ex.Message}", ex);
            }

            int exists = int.Parse(ExecuteScalar("SELECT count(*) FROM information_schema.STATISTICS WHERE table_schema = 'CSI_auth' AND table_name = 'Users' AND index_name = 'refid_UNIQUE';", connectionString).ToString());

            if (exists == 0)
            {
                //ExecuteNonQuery("UPDATE csi_auth.users SET refid = NULL WHERE refid = '';", connectionString);
                ExecuteNonQuery("ALTER TABLE `csi_auth`.`users` CHANGE COLUMN `refId` `refId` VARCHAR(100) DEFAULT NULL, ADD UNIQUE INDEX `refId_UNIQUE` (`refId` ASC)", connectionString);
            }


            #region Check the type of the column "machines" and change to type TEXT
            //---------------------------------------------------------------------
            string type = "";

            DataTable dtTypes = GetDataTable(@"SELECT column_name, data_type FROM information_schema.columns WHERE table_schema = 'CSI_auth' AND table_name = 'Users'");

            foreach (DataRow row in dtTypes.Rows)
            {
                if (row["column_name"].ToString() == "machines")
                {
                    type = row["data_type"].ToString();
                }
            }
            if (type.ToUpper() == "VARCHAR")
            {
                ExecuteNonQuery("ALTER TABLE CSI_auth.Users MODIFY machines TEXT;");
            }
            //---------------------------------------------------------------------
            #endregion


            if (!ExistsColumn("salt_", "CSI_auth.Users", connectionString))
            {
                AddColumn("salt_", "CSI_auth.Users", "VARCHAR(255)", connectionString);

                DataTable dt = GetDataTable("SELECT * FROM CSI_auth.Users");

                HashHelper hashHelper = new HashHelper();

                foreach (DataRow row in dt.Rows)
                {
                    if (!String.IsNullOrEmpty(row["password_"].ToString()))
                    {
                        var derivedKey = HashHelper.CreatePBKDF2Hash(hashHelper.AES_Decrypt(row["password_"].ToString(), "pass"));
                        var pass_hash = Convert.ToBase64String(derivedKey.Hash);
                        var pass_salt = Convert.ToBase64String(derivedKey.Salt);

                        ExecuteNonQuery($"UPDATE CSI_auth.users Set password_ = '{ pass_hash }', salt_ = '{ pass_salt }' WHERE username_ = '{ row["username_"] }';", connectionString);
                    }
                }
            }

            if (!ExistsColumn("ovr_interval", "CSI_auth.Users", connectionString))
                AddColumn("ovr_interval", "CSI_auth.Users", "INT(11) NOT NULL DEFAULT 0 AFTER machines", connectionString);

            if (!ExistsColumn("displayname", "CSI_auth.Users", connectionString))
                AddColumn("displayname", "CSI_auth.Users", "VARCHAR(100)", connectionString);

            if (!ExistsColumn("title", "CSI_auth.Users", connectionString))
                AddColumn("title", "CSI_auth.Users", "VARCHAR(100)", connectionString);

            if (!ExistsColumn("dept", "CSI_auth.Users", connectionString))
                AddColumn("dept", "CSI_auth.Users", "VARCHAR(100)", connectionString);

            if (!ExistsColumn("phoneext", "CSI_auth.Users", connectionString))
                AddColumn("phoneext", "CSI_auth.Users", "VARCHAR(20)", connectionString);

            if (!ExistsColumn("EditTimeline", "CSI_auth.Users", connectionString))
                AddColumn("EditTimeline", "CSI_auth.Users", "tinyint(4) NOT NULL DEFAULT 0", connectionString);

            if (ExistsColumn("EditMasterPartNumber", "CSI_auth.Users", connectionString))
                ExecuteNonQuery("ALTER TABLE CHANGE COLUMN `EditMasterPartNumber` `EditMasterPartData` TINYINT(4) NOT NULL DEFAULT '0' ;", connectionString);

            if (!ExistsColumn("EditMasterPartData", "CSI_auth.Users", connectionString))
                AddColumn("EditMasterPartData", "CSI_auth.Users", "tinyint(4) NOT NULL DEFAULT 0", connectionString);

            DataTable dtAdmin = GetDataTable("SELECT * FROM CSI_auth.Users WHERE username_ = 'Admin'");
            if (dtAdmin.Rows.Count == 0)
            {
                var derivedKey = HashHelper.CreatePBKDF2Hash("admin");

                sqlCmd.Clear();
                sqlCmd.Append($"INSERT IGNORE INTO CSI_auth.Users ");
                sqlCmd.Append($"(                                 ");
                sqlCmd.Append($"    username_,                    ");
                sqlCmd.Append($"    Name_,                        ");
                sqlCmd.Append($"    firstname_,                   ");
                sqlCmd.Append($"    password_,                    ");
                sqlCmd.Append($"    salt_,                        ");
                sqlCmd.Append($"    email_,                       ");
                sqlCmd.Append($"    usertype,                     ");
                sqlCmd.Append($"    machines,                     ");
                sqlCmd.Append($"    refId                         ");
                sqlCmd.Append($") VALUES (                        ");
                sqlCmd.Append($"    'Admin',                      ");
                sqlCmd.Append($"    'Admin',                      ");
                sqlCmd.Append($"    'Admin',                      ");
                sqlCmd.Append($"    '{ Convert.ToBase64String(derivedKey.Hash) }', ");
                sqlCmd.Append($"    '{ Convert.ToBase64String(derivedKey.Salt) }', ");
                sqlCmd.Append($"    'admin@admin',                ");
                sqlCmd.Append($"    'Admin',                      ");
                sqlCmd.Append($"    'ALL',                        ");
                sqlCmd.Append($"    ''                            ");
                sqlCmd.Append($")                                 ");

                ExecuteNonQuery(sqlCmd.ToString());

                MessageBox.Show("CSIFLEX created a database for the users account, you can use the default user account to configure the system, and users. \n\n" +
                    "Default account : username : Admin , password : admin\n\n" +
                    "Please make sure to have the latest version of eNETDNC running.\n" +
                    "If you are having problems creating the database, try to regenerate the CSV files from eNET.");
            }

            AddAuthTableBckp("Users");
        }

        #endregion


        #region VALIDATE CSI_DATABASE DATABASE TABLES =============================================================================================================================


        public static void Validate_CsiDatabase_Database(string connectionString = mySqlConnectionString)
        {
            string sqlCmd = "CREATE DATABASE IF NOT EXISTS CSI_database CHARACTER SET utf8 COLLATE utf8_unicode_ci;";

            try
            {
                ExecuteNonQuery(sqlCmd);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_CsiDatabase_Database - {ex.Message}", ex);
            }

            try
            {
                //Validate_Database_Logs_Table(connectionString);
                Validate_Database_Adjustment_Table(connectionString);

                Validate_Database_AdjustRows_Table(connectionString);

                Validate_Database_Colors_Table(connectionString);

                Validate_Database_CsiFlexVersion_Table(connectionString);

                Validate_Database_DeviceConfig2_Table(connectionString);

                Validate_Database_DeviceConfig_Table(connectionString);

                Validate_Database_Messages_Table(connectionString);

                Validate_Database_Devices_Table(connectionString);

                Validate_Database_ExternalSource_Table(connectionString);

                Validate_Database_MachineState_Table(connectionString);

                Validate_Database_FullData_Table(connectionString);

                Validate_Database_Groups_Table(connectionString);

                Validate_Database_EnetGroups_Table(connectionString);

                Validate_Database_LiveStatut_Table(connectionString);

                Validate_Database_Oee_Table(connectionString);

                Validate_Database_TempOee_Table(connectionString);

                Validate_Database_Operator_Table(connectionString);

                Validate_Database_PartNumber_Table(connectionString);

                Validate_Database_MasterPartNumbers_Table(connectionString);

                Validate_Database_HistoryPartNumbers_Table(connectionString);

                Validate_Database_RenameMachines_Table(connectionString);

                Validate_Database_ReportedParts_Table(connectionString);

                Validate_Database_RMPort_Table(connectionString);

                Validate_Database_UserConfig_Table(connectionString);

                Validate_Database_TempMachine_Table(connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($" - {ex.Message}", ex);
            }
        }


        /// <summary>
        /// Create the table csi_database.tbl_adjustment if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Database_Adjustment_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS csi_database.tbl_adjustment ( ");
            sqlCmd.Append("   `Id`           INT         NOT NULL AUTO_INCREMENT,   ");
            sqlCmd.Append("   `MachineId`    int(11)     NOT NULL DEFAULT 0,        ");
            sqlCmd.Append("   `Machine`      varchar(45) NOT NULL,                  ");
            sqlCmd.Append("   `AdjustDate`   datetime    NOT NULL,                  ");
            sqlCmd.Append("   `Shift`        int(11)     NOT NULL,                  ");
            sqlCmd.Append("   `Description`  text        NOT NULL,                  ");
            sqlCmd.Append("   `CreatedWhen`  datetime    NOT NULL,                  ");
            sqlCmd.Append("   `CreatedBy`    varchar(45) NOT NULL,                  ");
            sqlCmd.Append("   PRIMARY KEY (`Id`),                                   ");
            sqlCmd.Append("   UNIQUE KEY `Id_UNIQUE` (`Id`)                         ");
            sqlCmd.Append(")                                                        ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Database_Adjustment_Table - {ex.Message}", ex);
            }

            AddDatabaseTableBckp("tbl_adjustment");
        }


        /// <summary>
        /// Create the table csi_database.tbl_adjust_rows if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Database_AdjustRows_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS csi_database.tbl_adjust_rows ( ");
            sqlCmd.Append("   `Id`            int(11)      NOT NULL AUTO_INCREMENT,  ");
            sqlCmd.Append("   `AdjustmentId`  int(11)      NOT NULL DEFAULT '0',     ");
            sqlCmd.Append("   `Action`        varchar(1)   NOT NULL,                 ");
            sqlCmd.Append("   `Machine`       varchar(45)  NOT NULL,                 ");
            sqlCmd.Append("   `Date`          datetime     NOT NULL,                 ");
            sqlCmd.Append("   `NewDate`       datetime     NOT NULL,                 ");
            sqlCmd.Append("   `Status`        varchar(45)  DEFAULT NULL,             ");
            sqlCmd.Append("   `NewStatus`     varchar(45)  DEFAULT NULL,             ");
            sqlCmd.Append("   `PartNumber`    varchar(45)  DEFAULT NULL,             ");
            sqlCmd.Append("   `NewPartNumber` varchar(45)  DEFAULT NULL,             ");
            sqlCmd.Append("   `Operator`      varchar(45)  DEFAULT NULL,             ");
            sqlCmd.Append("   `NewOperator`   varchar(45)  DEFAULT NULL,             ");
            sqlCmd.Append("   `Cycletime`     int(11)      DEFAULT NULL,             ");
            sqlCmd.Append("   `NewCycletime`  int(11)      DEFAULT NULL,             ");
            sqlCmd.Append("   `Comments`      varchar(500) DEFAULT NULL,             ");
            sqlCmd.Append("   `NewComments`   varchar(500) DEFAULT NULL,             ");
            sqlCmd.Append("   PRIMARY KEY (`Id`),                                    ");
            sqlCmd.Append("   UNIQUE KEY `Id_UNIQUE` (`Id`),                         ");
            sqlCmd.Append("   KEY `IDX_ADJUST_DATE` (`AdjustmentId`,`Date`)          ");
            sqlCmd.Append(")                                                       ; ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);

                if (!ExistsColumn("PartNumber", "csi_database.tbl_adjust_rows", connectionString))
                    AddColumn("PartNumber", "csi_database.tbl_adjust_rows", "varchar(45) NULL AFTER `NewStatus`", connectionString);

                if (!ExistsColumn("NewPartNumber", "csi_database.tbl_adjust_rows", connectionString))
                    AddColumn("NewPartNumber", "csi_database.tbl_adjust_rows", "varchar(45) NULL AFTER `PartNumber`", connectionString);

                if (!ExistsColumn("Operator", "csi_database.tbl_adjust_rows", connectionString))
                    AddColumn("Operator", "csi_database.tbl_adjust_rows", "varchar(45) NULL AFTER `NewPartNumber`", connectionString);

                if (!ExistsColumn("NewOperator", "csi_database.tbl_adjust_rows", connectionString))
                    AddColumn("NewOperator", "csi_database.tbl_adjust_rows", "varchar(45) NULL AFTER `Operator`", connectionString);

                if (!ExistsColumn("Comments", "csi_database.tbl_adjust_rows", connectionString))
                    AddColumn("Comments", "csi_database.tbl_adjust_rows", "varchar(500) NULL", connectionString);

                if (!ExistsColumn("NewComments", "csi_database.tbl_adjust_rows", connectionString))
                    AddColumn("NewComments", "csi_database.tbl_adjust_rows", "varchar(500) NULL", connectionString);

                sqlCmd.Clear();
                sqlCmd.Append($"CREATE OR REPLACE VIEW csi_database.vw_adjustment AS     ");
                sqlCmd.Append($"SELECT                                                   ");
                sqlCmd.Append($"    r.Id            AS Id,                               ");
                sqlCmd.Append($"    a.MachineId     AS MachineId,                        ");
                sqlCmd.Append($"    a.Machine       AS Machine,                          ");
                sqlCmd.Append($"    a.AdjustDate    AS AdjustDate,                       ");
                sqlCmd.Append($"    a.Shift         AS Shift,                            ");
                sqlCmd.Append($"    a.Description   AS Description,                      ");
                sqlCmd.Append($"    a.CreatedWhen   AS CreatedWhen,                      ");
                sqlCmd.Append($"    a.CreatedBy     AS CreatedBy,                        ");
                sqlCmd.Append($"    r.Action        AS Action,                           ");
                sqlCmd.Append($"    r.Date          AS Date,                             ");
                sqlCmd.Append($"    r.NewDate       AS NewDate,                          ");
                sqlCmd.Append($"    r.Status        AS Status,                           ");
                sqlCmd.Append($"    r.NewStatus     AS NewStatus,                        ");
                sqlCmd.Append($"    r.PartNumber    AS PartNumber,                       ");
                sqlCmd.Append($"    r.NewPartNumber AS NewPartNumber,                    ");
                sqlCmd.Append($"    r.Operator      AS Operator,                         ");
                sqlCmd.Append($"    r.NewOperator   AS NewOperator,                      ");
                sqlCmd.Append($"    r.Cycletime     AS Cycletime,                        ");
                sqlCmd.Append($"    r.NewCycletime  AS NewCycletime,                     ");
                sqlCmd.Append($"    r.Comments      AS Comments,                         ");
                sqlCmd.Append($"    r.NewComments   AS NewComments                       ");
                sqlCmd.Append($"FROM                                                     ");
                sqlCmd.Append($"    csi_database.tbl_adjustment a                        ");
                sqlCmd.Append($"JOIN                                                     ");
                sqlCmd.Append($"    csi_database.tbl_adjust_rows r                       ");
                sqlCmd.Append($"            ON a.Id = r.AdjustmentId;                    ");

                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Database_AdjustRows_Table - {ex.Message}", ex);
            }

            AddDatabaseTableBckp("tbl_adjust_rows");
        }


        /// <summary>
        /// Create the table csi_database.tbl_colors if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Database_Logs_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS csi_database.tbl_logs   ( ");
            sqlCmd.Append("   `Id`       INT           NOT NULL AUTO_INCREMENT, ");
            sqlCmd.Append("   `System`   VARCHAR(45)   NOT NULL,                ");
            sqlCmd.Append("   `Date`     DATETIME      NOT NULL,                ");
            sqlCmd.Append("   `Type`     VARCHAR(45)   NOT NULL,                ");
            sqlCmd.Append("   `Message`  VARCHAR(1000) NOT NULL,                ");
            sqlCmd.Append("   PRIMARY KEY (`Id`),                               ");
            sqlCmd.Append("   UNIQUE INDEX `Id_UNIQUE` (`Id` ASC)               ");
            sqlCmd.Append(")                                                    ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Database_Logs_Table - {ex.Message}", ex);
            }
        }


        /// <summary>
        /// Create the table csi_database.tbl_colors if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Database_Colors_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS csi_database.tbl_colors ( ");
            sqlCmd.Append("   `statut` varchar(255) PRIMARY KEY,                ");
            sqlCmd.Append("   `color`  text                                     ");
            sqlCmd.Append(")                                                    ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Database_Colors_Table - {ex.Message}", ex);
            }

            ExecuteNonQuery("INSERT IGNORE INTO CSI_Database.tbl_colors (statut, color) VALUES('NOT CONNECTED','#ABABAB');");

            AddDatabaseTableBckp("tbl_colors");
        }


        /// <summary>
        /// Create the table csi_database.tbl_csiflex_version if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Database_CsiFlexVersion_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS csi_database.tbl_csiflex_version ( ");
            sqlCmd.Append("   `version` integer PRIMARY KEY                              ");
            sqlCmd.Append(")                                                    ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Database_CsiFlexVersion_Table - {ex.Message}", ex);
            }
        }


        /// <summary>
        /// Create the table CSI_Database.tbl_deviceConfig if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Database_DeviceConfig_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE if not exists CSI_Database.tbl_deviceConfig                 ");
            sqlCmd.Append("(                                                                        ");
            sqlCmd.Append("    `deviceId`               INT PRIMARY KEY,                            ");
            sqlCmd.Append("    `name`                   varchar(255),                               ");
            sqlCmd.Append("    `IP`                     varchar(255),                               ");
            sqlCmd.Append("    `messages`               varchar(255) Not NULL Default 'off',        ");
            sqlCmd.Append("    `piechart`               varchar(255) Not NULL Default 'off',        ");
            sqlCmd.Append("    `piecharttime`           varchar(255) Not NULL Default 'Weekly',     ");
            sqlCmd.Append("    `refreshbrowser`         varchar(255) Not NULL Default '',           ");
            sqlCmd.Append("    `livestatusdelay`        varchar(255) Not NULL Default 'on',         ");
            sqlCmd.Append("    `detail_livestatusdelay` varchar(255) Not NULL Default '6',          ");
            sqlCmd.Append("    `datetime`               varchar(255) Not NULL Default 'off',        ");
            sqlCmd.Append("    `customlogo`             varchar(255) Not NULL Default '',           ");
            sqlCmd.Append("    `detail_customlogo`      varchar(255) Not NULL Default '',           ");
            sqlCmd.Append("    `temperature`            varchar(255) Not NULL Default 'off',        ");
            sqlCmd.Append("    `degree`                 varchar(255) Not NULL Default 'Fahrenheit', ");
            sqlCmd.Append("    `detail_temperature`     varchar(255) Not NULL Default '',           ");
            sqlCmd.Append("    `lastcycle`              varchar(255) Not NULL Default 'off',        ");
            sqlCmd.Append("    `currentcycle`           varchar(255) Not NULL Default 'off',        ");
            sqlCmd.Append("    `elapsedtime`            varchar(255) Not NULL Default 'off',        ");
            sqlCmd.Append("    `fullscreen`             varchar(255) Not NULL Default 'off',        ");
            sqlCmd.Append("    `livestatusposition`     varchar(255) Not NULL Default 'right',      ");
            sqlCmd.Append("    `IFrame`                 varchar(255) Not NULL Default 'off',        ");
            sqlCmd.Append("    `detail_IFrame`          varchar(255) Not NULL Default '',           ");
            sqlCmd.Append("    `rotation`               varchar(255) Not NULL Default 'off',        ");
            sqlCmd.Append("    `PieChartBy`             varchar(255) ,                              ");
            sqlCmd.Append("    `DisplayWhat`            varchar(255) ,                              ");
            sqlCmd.Append("    `LogoBarHidden`          varchar(5) Not NULL Default 'off',          ");
            sqlCmd.Append("    `DarkTheme`              varchar(5) Not NULL Default 'off'           ");
            sqlCmd.Append(");                                                                       ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);

                if (!ExistsColumn("deviceId", "csi_database.tbl_deviceConfig", connectionString))
                {
                    sqlCmd.Clear();
                    sqlCmd.Append("ALTER TABLE csi_database.tbl_deviceConfig ADD deviceId INT;");

                    if (ExecuteScalar($"SELECT COUNT(*) AS qttPri FROM information_schema.columns WHERE table_schema = 'csi_database' AND table_name = 'tbl_deviceconfig' AND COLUMN_KEY = 'PRI';").ToString() != "0")
                        sqlCmd.Append("ALTER TABLE csi_database.tbl_deviceconfig DROP PRIMARY KEY;");

                    ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                }

                if (!ExistsColumn("LogoBarHidden", "csi_database.tbl_deviceconfig", connectionString))
                    AddColumn("LogoBarHidden", "csi_database.tbl_deviceconfig", "varchar(5) Not NULL Default 'off'", connectionString);

                if (!ExistsColumn("DarkTheme", "csi_database.tbl_deviceconfig", connectionString))
                    AddColumn("DarkTheme", "csi_database.tbl_deviceconfig", "varchar(5) Not NULL Default 'off'", connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Database_DeviceConfig_Table - {ex.Message}", ex);
            }

            AddDatabaseTableBckp("tbl_deviceConfig");
        }


        /// <summary>
        /// Create the table CSI_Database.tbl_deviceConfig2 if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Database_DeviceConfig2_Table(string connectionString = mySqlConnectionString)
        {

            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS csi_database.tbl_deviceconfig2             ");
            sqlCmd.Append("(                                                                     ");
            sqlCmd.Append("   `deviceId`          INT PRIMARY KEY,                               ");
            sqlCmd.Append("   `name`              varchar(255) COLLATE utf8_unicode_ci NOT NULL, ");
            sqlCmd.Append("   `IP_adress`         varchar(255) COLLATE utf8_unicode_ci NOT NULL, ");
            sqlCmd.Append("   `timeline`          int(11)      NOT NULL DEFAULT '0',             ");
            sqlCmd.Append("   `trends`            int(11)      NOT NULL DEFAULT '0',             ");
            sqlCmd.Append("   `trendspercent`     int(11)      NOT NULL DEFAULT '100',           ");
            sqlCmd.Append("   `trendcompare`      varchar(255) COLLATE utf8_unicode_ci NOT NULL DEFAULT 'The actual shift',    ");
            sqlCmd.Append("   `ProdpercentOn`     varchar(45)  COLLATE utf8_unicode_ci NOT NULL DEFAULT 'On',                  ");
            sqlCmd.Append("   `DateFormat`        varchar(255) COLLATE utf8_unicode_ci NOT NULL DEFAULT 'dd-MM-yyyy HH:mm:ss', ");
            sqlCmd.Append("   `devicetype`        varchar(255) COLLATE utf8_unicode_ci NOT NULL DEFAULT 'Computer',            ");
            sqlCmd.Append("   `scale`             int(11)      NOT NULL DEFAULT '100',                        ");
            //sqlCmd.Append("   `ChartByGroups`     int(11)      DEFAULT '0',                                   ");
            sqlCmd.Append("   `DisplayByGroups`   int(11)      DEFAULT '0',                                   ");
            sqlCmd.Append("   `OrderBy`           int(11)      DEFAULT '0',                                   ");
            sqlCmd.Append("   `browserzoom`       int(11)      NOT NULL DEFAULT '100',                        ");
            sqlCmd.Append("   `FeedrateOver`      varchar(45)  COLLATE utf8_unicode_ci NOT NULL DEFAULT 'on', ");
            sqlCmd.Append("   `SpindleOver`       varchar(45)  COLLATE utf8_unicode_ci NOT NULL DEFAULT 'on', ");
            sqlCmd.Append("   `RapidOver`         varchar(45)  COLLATE utf8_unicode_ci NOT NULL DEFAULT 'on', ");
            sqlCmd.Append("   `TimeLineBarHeight` int(2)       NOT NULL DEFAULT '100',                        ");
            sqlCmd.Append("   `MachineNameText`   int(2)       NOT NULL DEFAULT '100',                        ");
            sqlCmd.Append("   `MachineNameWidth`  int(2)       NOT NULL DEFAULT '100',                        ");
            sqlCmd.Append("   `Operator`          varchar(5)   NOT NULL DEFAULT 'on' ,                        ");
            sqlCmd.Append("   `OperatorName`      varchar(5)   NOT NULL DEFAULT 'on' ,                        ");
            sqlCmd.Append("   `RemoveLastRow`     varchar(5)   Not NULL Default 'off',                        ");
            sqlCmd.Append("   `PartNumberColumn`  varchar(5)   Not NULL Default 'on' ,                        ");
            sqlCmd.Append("   `CountColumn`       varchar(5)   Not NULL Default 'on' ,                        ");
            sqlCmd.Append("   `CountFormat`       varchar(25)  Not NULL Default 'CC' ,                        ");
            sqlCmd.Append("   `UseMachineLabel`   varchar(5)   Not NULL Default 'off',                        ");
            sqlCmd.Append("   `DisplayPressure`   int(1)       Not NULL Default 0                             ");
            sqlCmd.Append(")                                                                                  ");
            sqlCmd.Append("ENGINE = InnoDB DEFAULT CHARSET = utf8 COLLATE = utf8_unicode_ci;                  ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);

                //ExecuteNonQuery("UPDATE CSI_Database.tbl_deviceConfig2 SET ByGroup = 0 WHERE ByGroup IS NULL", connectionString);

                if (!ExistsColumn("deviceId", "csi_database.tbl_deviceconfig2", connectionString))
                {
                    sqlCmd.Clear();
                    sqlCmd.Append("ALTER TABLE csi_database.tbl_deviceconfig2 ADD deviceId INT;");

                    if (ExecuteScalar($"SELECT COUNT(*) AS qttPri FROM information_schema.columns WHERE table_schema = 'csi_database' AND table_name = 'tbl_deviceconfig2' AND COLUMN_KEY = 'PRI';").ToString() != "0")
                        sqlCmd.Append("ALTER TABLE csi_database.tbl_deviceconfig2 DROP PRIMARY KEY;");

                    ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                }

                //if (ExistsColumn("ByGroup", "csi_database.tbl_deviceconfig2", connectionString))
                //{
                //    ExecuteNonQuery("ALTER TABLE csi_database.tbl_deviceconfig2 CHANGE COLUMN `ByGroup` `ChartByGroups` INT(11) DEFAULT '0'", connectionString);
                //}

                if (!ExistsColumn("DisplayByGroups", "csi_database.tbl_deviceconfig2", connectionString))
                    AddColumn("DisplayByGroups", "csi_database.tbl_deviceconfig2", "INT(11) DEFAULT '0'", connectionString);

                if (!ExistsColumn("ProdpercentOn", "csi_database.tbl_deviceconfig2", connectionString))
                    AddColumn("ProdpercentOn", "csi_database.tbl_deviceconfig2", "VARCHAR(45) Not NULL DEFAULT 'Off' AFTER `trendcompare`", connectionString);

                if (!ExistsColumn("OrderBy", "csi_database.tbl_deviceconfig2", connectionString))
                    AddColumn("OrderBy", "csi_database.tbl_deviceconfig2", "int(11) NOT NULL DEFAULT '0'", connectionString);

                //if (!ExistsColumn("ChartByGroups", "csi_database.tbl_deviceconfig2", connectionString))
                //    AddColumn("ChartByGroups", "csi_database.tbl_deviceconfig2", "int(11) DEFAULT '0'", connectionString);

                if (!ExistsColumn("DisplayByGroups", "csi_database.tbl_deviceconfig2", connectionString))
                    AddColumn("DisplayByGroups", "csi_database.tbl_deviceconfig2", "int(11) DEFAULT '0'", connectionString);

                if (!ExistsColumn("TimeLineBarHeight", "csi_database.tbl_deviceconfig2", connectionString))
                    AddColumn("TimeLineBarHeight", "csi_database.tbl_deviceconfig2", "int(2) NOT NULL DEFAULT '100'", connectionString);

                if (!ExistsColumn("MachineNameText", "csi_database.tbl_deviceconfig2", connectionString))
                    AddColumn("MachineNameText", "csi_database.tbl_deviceconfig2", "int(2) NOT NULL DEFAULT '100'", connectionString);

                if (!ExistsColumn("MachineNameWidth", "csi_database.tbl_deviceconfig2", connectionString))
                    AddColumn("MachineNameWidth", "csi_database.tbl_deviceconfig2", "int(2) NOT NULL DEFAULT '100'", connectionString);

                if (!ExistsColumn("RemoveLastRow", "csi_database.tbl_deviceconfig2", connectionString))
                    AddColumn("RemoveLastRow", "csi_database.tbl_deviceconfig2", "varchar(5) Not NULL Default 'off'", connectionString);

                if (!ExistsColumn("PartNumberColumn", "csi_database.tbl_deviceconfig2", connectionString))
                    AddColumn("PartNumberColumn", "csi_database.tbl_deviceconfig2", "varchar(5) Not NULL Default 'on'", connectionString);

                if (!ExistsColumn("CountColumn", "csi_database.tbl_deviceconfig2", connectionString))
                    AddColumn("CountColumn", "csi_database.tbl_deviceconfig2", "varchar(5) Not NULL Default 'on'", connectionString);

                if (!ExistsColumn("CountFormat", "csi_database.tbl_deviceconfig2", connectionString))
                    AddColumn("CountFormat", "csi_database.tbl_deviceconfig2", "varchar(25) Not NULL Default 'CC'", connectionString);

                if (!ExistsColumn("Operator", "csi_database.tbl_deviceconfig2", connectionString))
                    AddColumn("Operator", "csi_database.tbl_deviceconfig2", "varchar(5) Not NULL Default 'on'", connectionString);

                if (!ExistsColumn("OperatorName", "csi_database.tbl_deviceconfig2", connectionString))
                    AddColumn("OperatorName", "csi_database.tbl_deviceconfig2", "varchar(5) Not NULL Default 'on'", connectionString);

                if (!ExistsColumn("UseMachineLabel", "csi_database.tbl_deviceconfig2", connectionString))
                    AddColumn("UseMachineLabel", "csi_database.tbl_deviceconfig2", "varchar(5) Not NULL Default 'off'", connectionString);

                if (!ExistsColumn("DisplayPressure", "csi_database.tbl_deviceconfig2", connectionString))
                    AddColumn("DisplayPressure", "csi_database.tbl_deviceconfig2", "int(1) Not NULL Default 0", connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Database_DeviceConfig2_Table - {ex.Message}", ex);
            }

            AddDatabaseTableBckp("tbl_deviceConfig2");
        }


        /// <summary>
        /// Create the table CSI_Database.tbl_devices if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Database_Devices_Table(string connectionString = mySqlConnectionString)
        {

            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS csi_database.tbl_devices ");
            sqlCmd.Append("(                                                   ");
            sqlCmd.Append("    `id`         INT PRIMARY KEY AUTO_INCREMENT,    ");
            sqlCmd.Append("    `IP_adress`  varchar(255),                      ");
            sqlCmd.Append("    `deviceName` varchar(255),                      ");
            sqlCmd.Append("    `machines`   text,                              ");
            sqlCmd.Append("    `groups`     text                               ");
            sqlCmd.Append(")                                                   ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);

                if (!ExistsColumn("id", "csi_database.tbl_devices", connectionString))
                {
                    sqlCmd.Clear();
                    sqlCmd.Append("ALTER TABLE csi_database.tbl_devices DROP PRIMARY KEY;");
                    sqlCmd.Append("ALTER TABLE csi_database.tbl_devices ADD id INT PRIMARY KEY AUTO_INCREMENT;");
                    ExecuteNonQuery(sqlCmd.ToString(), connectionString);

                    DataTable devices = GetDataTable("SELECT id, IP_adress FROM csi_database.tbl_devices");

                    foreach (DataRow device in devices.Rows)
                    {
                        sqlCmd.Clear();
                        sqlCmd.Append($"UPDATE CSI_Database.tbl_deviceConfig  SET deviceId = {device["id"].ToString()} WHERE IP        = '{device["IP_adress"].ToString()}'; ");
                        sqlCmd.Append($"UPDATE CSI_Database.tbl_deviceConfig2 SET deviceId = {device["id"].ToString()} WHERE IP_adress = '{device["IP_adress"].ToString()}'; ");
                        sqlCmd.Append($"UPDATE CSI_Database.tbl_messages      SET deviceId = {device["id"].ToString()} WHERE IP_adress = '{device["IP_adress"].ToString()}'; ");

                        ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                    }

                    sqlCmd.Clear();
                    sqlCmd.Append("ALTER TABLE csi_database.tbl_deviceconfig  ADD PRIMARY KEY(deviceId);");
                    sqlCmd.Append("ALTER TABLE csi_database.tbl_deviceconfig2 ADD PRIMARY KEY(deviceId);");
                    ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Database_Devices_Table - {ex.Message}", ex);
            }

            AddDatabaseTableBckp("tbl_devices");
        }


        /// <summary>
        /// Create the table CSI_Database.tbl_externalsource if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Database_ExternalSource_Table(string connectionString = mySqlConnectionString)
        {

            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS csi_database.tbl_externalsource ");
            sqlCmd.Append("(                                                          ");
            sqlCmd.Append("    `dashboardip_`  varchar(255),                          ");
            sqlCmd.Append("    `externalip_`   varchar(255),                          ");
            sqlCmd.Append("    `externalname_` varchar(255)                           ");
            sqlCmd.Append(")                                                          ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Database_ExternalSource_Table - {ex.Message}", ex);
            }
        }


        public static void Validate_Database_MachineState_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS csi_database.tbl_machinestate    ");
            sqlCmd.Append("(                                                           ");
            sqlCmd.Append("    `Id`              int(11)      NOT NULL AUTO_INCREMENT, ");
            sqlCmd.Append("    `MachineId`       int(11)      NOT NULL               , ");
            sqlCmd.Append("    `EventDateTime`   datetime     NOT NULL               , ");
            sqlCmd.Append("    `Shift`           int(1)       DEFAULT 0              , ");
            sqlCmd.Append("    `Status`          varchar(45)  NOT NULL               , ");
            sqlCmd.Append("    `SystemStatus`    varchar(45)  NOT NULL DEFAULT ''    , ");
            sqlCmd.Append("    `Comment`         varchar(256) DEFAULT ''             , ");
            sqlCmd.Append("    `PartNumber`      varchar(100) DEFAULT ''             , ");
            sqlCmd.Append("    `ProgramNumber`   varchar(100) DEFAULT ''             , ");
            sqlCmd.Append("    `Operation`       varchar(100) DEFAULT ''             , ");
            sqlCmd.Append("    `OperatorRefId`   varchar(100) DEFAULT ''             , ");
            sqlCmd.Append("    `PartCount`       int(11)      DEFAULT '0'            , ");
            sqlCmd.Append("    `PartRequired`    int(11)      DEFAULT '0'            , ");
            sqlCmd.Append("    `TotalPartCount`  int(11)      DEFAULT '0'            , ");
            sqlCmd.Append("    `FeedOverride`    int(11)      DEFAULT '0'            , ");
            sqlCmd.Append("    `RapidOverride`   int(11)      DEFAULT '0'            , ");
            sqlCmd.Append("    `SpindleOverride` int(11)      DEFAULT '0'            , ");
            sqlCmd.Append("  PRIMARY KEY(`Id`)                                       , ");
            sqlCmd.Append("  UNIQUE KEY `Id_UNIQUE` (`Id`)                           , ");
            sqlCmd.Append("  KEY `IDX_MachineId`  (`MachineId` ,`EventDateTime`)     , ");
            sqlCmd.Append("  KEY `IDX_PartNumber` (`PartNumber`,`MachineId`)           ");
            sqlCmd.Append(") ;                                                         ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);

                if (ExistsColumn("ENETStatus", "csi_database.tbl_machinestate", connectionString))
                    ExecuteNonQuery("ALTER TABLE `csi_database`.`tbl_machinestate` CHANGE COLUMN `ENETStatus` `SystemStatus` VARCHAR(45) NOT NULL ; ", connectionString);

                if (!ExistsColumn("Comment", "csi_database.tbl_machinestate", connectionString))
                    AddColumn("Comment", "csi_database.tbl_machinestate", "varchar(256) DEFAULT '' AFTER SystemStatus", connectionString);

                if (!ExistsColumn("ProgramNumber", "csi_database.tbl_machinestate", connectionString))
                    AddColumn("ProgramNumber", "csi_database.tbl_machinestate", "varchar(100) DEFAULT '' AFTER PartNumber", connectionString);

                if (!ExistsColumn("Operation", "csi_database.tbl_machinestate", connectionString))
                    AddColumn("Operation", "csi_database.tbl_machinestate", "varchar(100) DEFAULT '' AFTER ProgramNumber", connectionString);

                if (!ExistsColumn("OperatorRefId", "csi_database.tbl_machinestate", connectionString))
                    AddColumn("OperatorRefId", "csi_database.tbl_machinestate", "varchar(100) DEFAULT '' AFTER Operation", connectionString);

                if (!ExistsColumn("Shift", "csi_database.tbl_machinestate", connectionString))
                    AddColumn("Shift", "csi_database.tbl_machinestate", "INT(1) DEFAULT 0 AFTER EventDateTime", connectionString);

                //ExecuteNonQuery("ALTER TABLE `csi_database`.`tbl_machinestate` ADD INDEX `IDX_PartNumber` (`PartNumber` ASC, `Operation` ASC, `MachineId` ASC);", connectionString);

            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }

            try
            {
                sqlCmd.Clear();
                sqlCmd.Append($"CREATE OR REPLACE VIEW                              ");
                sqlCmd.Append($"        `csi_database`.`vw_machinestate` AS         ");
                sqlCmd.Append($"    SELECT                                          ");
                sqlCmd.Append($"        `s`.`Id`              AS `Id`             , ");
                sqlCmd.Append($"        `s`.`MachineId`       AS `MachineId`      , ");
                sqlCmd.Append($"        `m`.`machine_name`    AS `Machine_name`   , ");
                sqlCmd.Append($"        `s`.`EventDateTime`   AS `EventDateTime`  , ");
                sqlCmd.Append($"        `s`.`Status`          AS `Status`         , ");
                sqlCmd.Append($"        `s`.`Shift`           AS `Shift`          , ");
                sqlCmd.Append($"        `s`.`Comment`         AS `Comment`        , ");
                sqlCmd.Append($"        `s`.`PartNumber`      AS `PartNumber`     , ");
                sqlCmd.Append($"        `s`.`Operation`       AS `Operation`      , ");
                sqlCmd.Append($"        `s`.`OperatorRefId`   AS `OperatorRefId`  , ");
                sqlCmd.Append($"        `s`.`PartCount`       AS `PartCount`      , ");
                sqlCmd.Append($"        `s`.`FeedOverride`    AS `FeedOverride`   , ");
                sqlCmd.Append($"        `s`.`RapidOverride`   AS `RapidOverride`  , ");
                sqlCmd.Append($"        `s`.`SpindleOverride` AS `SpindleOverride`  ");
                sqlCmd.Append($"    FROM                                            ");
                sqlCmd.Append($"        (`csi_database`.`tbl_machinestate` `s`      ");
                sqlCmd.Append($"        JOIN `csi_auth`.`tbl_ehub_conf` `m` ON ((`s`.`MachineId` = `m`.`id`))); ");

                sqlCmd.Append($"GRANT SELECT ON `csi_database`.`vw_machinestate` TO 'cfdbview'@'%';");

                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }


        public static void Validate_Database_FullData_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS csi_database.tbl_fulldata        ");
            sqlCmd.Append("(                                                           ");
            sqlCmd.Append("    `Id`              int(11)      NOT NULL AUTO_INCREMENT, ");
            sqlCmd.Append("    `MachineId`       int(11)      NOT NULL               , ");
            sqlCmd.Append("    `EventDateTime`   datetime     NOT NULL               , ");
            sqlCmd.Append("    `Status`          varchar(45)  NOT NULL               , ");
            sqlCmd.Append("    `ENETStatus`      varchar(45)  NOT NULL               , ");
            sqlCmd.Append("    `FullData`        mediumtext                          , ");
            sqlCmd.Append("  PRIMARY KEY(`Id`)                                       , ");
            sqlCmd.Append("  UNIQUE KEY `Id_UNIQUE` (`Id`)                           , ");
            sqlCmd.Append("  KEY `IDX_MachineId` (`MachineId`,`EventDateTime`)         ");
            sqlCmd.Append(") ;                                                         ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }


        /// <summary>
        /// Create the table CSI_Database.tbl_Groups if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Database_Groups_Table(string connectionString = mySqlConnectionString)
        {

            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS csi_database.tbl_Groups            ");
            sqlCmd.Append("(                                                             ");
            sqlCmd.Append("    `users`     varchar(255),                                 ");
            sqlCmd.Append("    `groups`    varchar(255),                                 ");
            sqlCmd.Append("    `machines`  varchar(255),                                 ");
            sqlCmd.Append("    `machineId` varchar(5)  NOT NULL DEFAULT '0',             ");
            sqlCmd.Append("     CONSTRAINT pk_GM PRIMARY KEY (users, `groups`, machines) ");
            sqlCmd.Append(")                                                             ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);

                if (!ExistsColumn("machineId", "csi_database.tbl_Groups", connectionString))
                {
                    AddColumn("machineId", "csi_database.tbl_Groups", "varchar(5) NOT NULL DEFAULT '0'", connectionString);

                    DataTable machines = GetDataTable("SELECT * FROM csi_auth.tbl_ehub_conf;");
                    DataTable groups = GetDataTable($"SELECT * FROM csi_database.tbl_groups WHERE machines <> ''");

                    foreach (DataRow row in groups.Rows)
                    {
                        string machineName = row["machines"].ToString();
                        int machineId = (int)machines.Rows.Cast<DataRow>().Where(r => r["EnetMachineName"].ToString() == machineName).Select(r => r["Id"]).FirstOrDefault();

                        ExecuteNonQuery($"UPDATE csi_database.tbl_Groups SET machineId = { machineId } WHERE machines = '{ machineName }'", connectionString);
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                MessageBox.Show($"Error validating table tbl_Groups: {ex.Message}");
            }
        }


        public static void Validate_Database_EnetGroups_Table(string connectionString = mySqlConnectionString)
        {

            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS csi_database.tbl_EnetGroups         ");
            sqlCmd.Append(" (                                                             ");
            sqlCmd.Append("    `groupName` varchar(50) NOT NULL DEFAULT '',               ");
            sqlCmd.Append("    `machine`   varchar(50) NOT NULL DEFAULT '',               ");
            sqlCmd.Append("     CONSTRAINT pk_EnetGrp PRIMARY KEY (`groupName`, `machine`)");
            sqlCmd.Append(" )                                                             ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Database_EnetGroups_Table - {ex.Message}", ex);
            }
        }


        /// <summary>
        /// Create the table CSI_Database.tbl_livestatut if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Database_LiveStatut_Table(string connectionString = mySqlConnectionString)
        {

            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS csi_database.tbl_livestatut ");
            sqlCmd.Append("(                                                      ");
            sqlCmd.Append("    `machinename_`  text,                              ");
            sqlCmd.Append("    `statut_`       text,                              ");
            sqlCmd.Append("    `partnumber_`   text,                              ");
            sqlCmd.Append("    `updatedat_`    datetime,                          ");
            sqlCmd.Append("    `lastcycle_`    text,                              ");
            sqlCmd.Append("    `currentcycle_` text,                              ");
            sqlCmd.Append("    `elapsedtime_`  text,                              ");
            sqlCmd.Append("    `timeline_`     longtext,                          ");
            sqlCmd.Append("    `shift_`        text,                              ");
            sqlCmd.Append("    `legendstatus_` text                               ");
            sqlCmd.Append(")                                                      ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Database_LiveStatut_Table - {ex.Message}", ex);
            }
        }


        /// <summary>
        /// Create the table csi_database.tbl_messages if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Database_Messages_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS csi_database.tbl_messages (          ");
            sqlCmd.Append("   `Id`         INT NOT NULL AUTO_INCREMENT,                    ");
            sqlCmd.Append("   `DeviceId`   INT NOT NULL,                                   ");
            sqlCmd.Append("   `Name`       VARCHAR(255) NOT NULL,                          ");
            sqlCmd.Append("   `IP_adress`  varchar(255),                                   ");
            sqlCmd.Append("   `Message`    VARCHAR(255) NOT NULL,                          ");
            sqlCmd.Append("   `Priority`   VARCHAR(255) DEFAULT NULL,                      ");
            sqlCmd.Append(" PRIMARY KEY (`Id`),                                            ");
            sqlCmd.Append(" UNIQUE INDEX `Id_UNIQUE`   (`Id` ASC),                         ");
            sqlCmd.Append(" UNIQUE INDEX `alltogether` (`DeviceId`, `Message`, `Priority`) ");
            sqlCmd.Append(");                                                              ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);

                if (!ExistsColumn("Id", "csi_database.tbl_messages", connectionString))
                {
                    AddColumn("Id", "csi_database.tbl_messages", "INT NULL FIRST", connectionString);

                    //ExecuteNonQuery("UPDATE csi_database.tbl_messages JOIN (SELECT @rank := 0) r SET Id = @rank:=@rank+1;", connectionString);
                    ExecuteNonQuery("ALTER TABLE csi_database.tbl_messages MODIFY Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY;", connectionString);
                }

                if (!ExistsColumn("DeviceId", "csi_database.tbl_messages", connectionString))
                {
                    ExecuteNonQuery("ALTER TABLE csi_database.tbl_messages ADD deviceId INT;", connectionString);
                }

                if (ExistsColumn("messages", "csi_database.tbl_messages", connectionString))
                {
                    ExecuteNonQuery("ALTER TABLE csi_database.tbl_messages DROP INDEX `alltogether`;", connectionString);
                    ExecuteNonQuery("ALTER TABLE csi_database.tbl_messages CHANGE COLUMN `messages` `Message` VARCHAR(255) NOT NULL;", connectionString);
                    ExecuteNonQuery("ALTER TABLE csi_database.tbl_messages ADD UNIQUE INDEX `ALLTOGETHER` (`deviceId` ASC, `Message` ASC, `Priority` ASC);", connectionString);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Database_Messages_Table - {ex.Message}", ex);
            }

            AddDatabaseTableBckp("tbl_messages");
        }


        public static void Validate_Database_Oee_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS csi_database.tbl_oee ( ");
            sqlCmd.Append("   `MACHINE`     varchar(75) NOT NULL,            ");
            sqlCmd.Append("   `trx_time`    datetime    NOT NULL,            ");
            sqlCmd.Append("   `Avail`       float       NOT NULL,            ");
            sqlCmd.Append("   `Performance` float       NOT NULL,            ");
            sqlCmd.Append("   `Quality`     float       NOT NULL,            ");
            sqlCmd.Append("   `OEE`         float       NOT NULL,            ");
            sqlCmd.Append("   `HEADPALLET`  int,                             ");
            sqlCmd.Append("   index ( trx_time )                             ");
            sqlCmd.Append(")                                                 ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Database_Oee_Table - {ex.Message}", ex);
            }
        }


        public static void Validate_Database_TempOee_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS csi_database.tempOee ( ");
            sqlCmd.Append("   `MachineName`  varchar(25) NOT NULL,           ");
            sqlCmd.Append("   `Date`         varchar(15) NOT NULL,           ");
            sqlCmd.Append("   `Time`         varchar(15) NOT NULL,           ");
            sqlCmd.Append("   `Availability` varchar(15) NOT NULL,           ");
            sqlCmd.Append("   `Performance`  varchar(15) NOT NULL,           ");
            sqlCmd.Append("   `Quality`      varchar(15) NOT NULL,           ");
            sqlCmd.Append("   `OEE`          varchar(15) NOT NULL,           ");
            sqlCmd.Append("   `HEADPALLET`   varchar(15)                     ");
            sqlCmd.Append(")                                                 ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Database_TempOee_Table - {ex.Message}", ex);
            }
        }


        /// <summary>
        /// Create the table csi_database.tbl_operator if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Database_Operator_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS csi_database.tbl_operator ( ");
            sqlCmd.Append("   `OPERATOR`         varchar(150) NOT NULL,           ");
            sqlCmd.Append("   `PARTNO`           varchar(255) NOT NULL,           ");
            sqlCmd.Append("   `QUANTITY`         int  NOT NULL,                   ");
            sqlCmd.Append("   `AVGCYCLETIME`     time NOT NULL,                   ");
            sqlCmd.Append("   `GOODCYCLE`        int  NOT NULL,                   ");
            sqlCmd.Append("   `AVGGOODCYCLETIME` time NOT NULL,                   ");
            sqlCmd.Append("   `trx_date`         date NOT NULL,                   ");
            sqlCmd.Append("   `shift`            int  NOT NULL,                   ");
            sqlCmd.Append("   `HEADPALLET`       int,                             ");
            sqlCmd.Append("   index ( trx_date )                                  ");
            sqlCmd.Append(")                                                      ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);

                sqlCmd.Clear();
                sqlCmd.Append($"SELECT EXISTS(                         ");
                sqlCmd.Append($"    SELECT * FROM                      ");
                sqlCmd.Append($"        information_schema.tables      ");
                sqlCmd.Append($"    WHERE                              ");
                sqlCmd.Append($"        TABLE_SCHEMA = 'csi_database'  ");
                sqlCmd.Append($"    AND TABLE_TYPE   = 'VIEW'          ");
                sqlCmd.Append($"    AND TABLE_NAME   = 'vw_operator'   ");
                sqlCmd.Append($");                                     ");

                string existsView = ExecuteScalar(sqlCmd.ToString()).ToString();

                if (existsView != "1")
                {
                    sqlCmd.Clear();
                    sqlCmd.Append($"CREATE VIEW `csi_database`.`vw_operator` AS          ");
                    sqlCmd.Append($"    SELECT                                           ");
                    sqlCmd.Append($"        `OPERATOR`         AS `OPERATOR`        ,    ");
                    sqlCmd.Append($"        `PARTNO`           AS `PARTNO`          ,    ");
                    sqlCmd.Append($"        `QUANTITY`         AS `QUANTITY`        ,    ");
                    sqlCmd.Append($"        `AVGCYCLETIME`     AS `AVGCYCLETIME`    ,    ");
                    sqlCmd.Append($"        `GOODCYCLE`        AS `GOODCYCLE`       ,    ");
                    sqlCmd.Append($"        `AVGGOODCYCLETIME` AS `AVGGOODCYCLETIME`,    ");
                    sqlCmd.Append($"        `trx_date`         AS `DATE`            ,    ");
                    sqlCmd.Append($"        `shift`            AS `shift`                ");
                    sqlCmd.Append($"    FROM                                             ");
                    sqlCmd.Append($"        `csi_database`.`tbl_operator`    ;           ");

                    sqlCmd.Append($"GRANT SELECT ON `csi_database`.`vw_operator` TO 'cfdbview'@'%';");

                    ExecuteNonQuery(sqlCmd.ToString());
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Database_Operator_Table - {ex.Message}", ex);
            }
        }


        /// <summary>
        /// Create the table csi_database.tbl_partnumber if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Database_PartNumber_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS csi_database.tbl_partnumber ( ");
            sqlCmd.Append("   `start_time`          datetime NOT NULL,              ");
            sqlCmd.Append("   `end_time`            datetime NOT NULL,              ");
            sqlCmd.Append("   `elapsed_time`        int NOT NULL,                   ");
            sqlCmd.Append("   `machine`             varchar(255) NOT NULL,          ");
            sqlCmd.Append("   `HEADPALLET`          int NOT NULL,                   ");
            sqlCmd.Append("   `shift`               int NOT NULL,                   ");
            sqlCmd.Append("   `Partno`              varchar(255) not null,          ");
            sqlCmd.Append("   `total_cycle`         int NOT NULL,                   ");
            sqlCmd.Append("   `good_cycle`          int not null,                   ");
            sqlCmd.Append("   `short_cycle`         int not null,                   ");
            sqlCmd.Append("   `long_cycle`          int not null,                   ");
            sqlCmd.Append("   `avg_good_cycle_time` time not null,                  ");
            sqlCmd.Append("   `max_cycle_time`      time not null,                  ");
            sqlCmd.Append("   `min_cycle_time`      time not null,                  ");
            sqlCmd.Append("   UNIQUE KEY `UNQ_partnumber` (`machine`,`start_time`), ");
            sqlCmd.Append("   KEY        `start_time`     (`start_time`)            ");
            sqlCmd.Append(")                                                        ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);

                DataTable idx = GetDataTable("SHOW index FROM CSI_Database.tbl_partnumber WHERE Key_name = 'UNQ_partnumber';");

                if (idx.Rows.Count == 0)
                {
                    sqlCmd.Clear();
                    sqlCmd.Append($"TRUNCATE TABLE `csi_database`.`tbl_partnumber`;                         ");
                    sqlCmd.Append($"ALTER TABLE `csi_database`.`tbl_partnumber`                             ");
                    sqlCmd.Append($"    ADD UNIQUE INDEX `UNQ_partnumber` (`machine` ASC, `start_time` ASC);");
                    ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                }

                sqlCmd.Clear();
                sqlCmd.Append($"SELECT EXISTS(                         ");
                sqlCmd.Append($"    SELECT * FROM                      ");
                sqlCmd.Append($"        information_schema.tables      ");
                sqlCmd.Append($"    WHERE                              ");
                sqlCmd.Append($"        TABLE_SCHEMA = 'csi_database'  ");
                sqlCmd.Append($"    AND TABLE_TYPE   = 'VIEW'          ");
                sqlCmd.Append($"    AND TABLE_NAME   = 'vw_partnumber' ");
                sqlCmd.Append($");                                     ");

                string existsView = ExecuteScalar(sqlCmd.ToString()).ToString();

                if (existsView != "1")
                {
                    sqlCmd.Clear();
                    sqlCmd.Append($"CREATE VIEW `csi_database`.`vw_partnumber` AS");
                    sqlCmd.Append($"    SELECT                                   ");
                    sqlCmd.Append($"        `start_time`   AS `start_time`  ,    ");
                    sqlCmd.Append($"        `end_time`     AS `end_time`    ,    ");
                    sqlCmd.Append($"        `elapsed_time` AS `elapsed_time`,    ");
                    sqlCmd.Append($"        `machine`      AS `machine`     ,    ");
                    sqlCmd.Append($"        `shift`        AS `shift`       ,    ");
                    sqlCmd.Append($"        `Partno`       AS `Partno`           ");
                    sqlCmd.Append($"    FROM                                     ");
                    sqlCmd.Append($"        `csi_database`.`tbl_partnumber`    ; ");

                    sqlCmd.Append($"GRANT SELECT ON `csi_database`.`vw_partnumber` TO 'cfdbview'@'%';");

                    ExecuteNonQuery(sqlCmd.ToString());
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Database_PartNumber_Table - {ex.Message}", ex);
            }
        }


        public static void Validate_Database_PartNumber_Settings_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS                                ");
            sqlCmd.Append("        `csi_database`.`tbl_partnumber_settings` (        ");
            sqlCmd.Append("   `Id`                  INT NOT NULL AUTO_INCREMENT,     ");
            sqlCmd.Append("   `PartNumber`          VARCHAR(100) NOT NULL,           ");
            sqlCmd.Append("   `MachineId`           INT NOT NULL,                    ");
            sqlCmd.Append("   `Operation`           INT NOT NULL DEFAULT 0,          ");
            sqlCmd.Append("   `MinTimeCycle`        INT NOT NULL,                    ");
            sqlCmd.Append("   `MaxTimeCycle`        INT NOT NULL,                    ");
            sqlCmd.Append("   `CycleCountMultipler` INT NOT NULL DEFAULT 1,          ");
            sqlCmd.Append("  PRIMARY KEY(`Id`),                                      ");
            sqlCmd.Append("  UNIQUE INDEX `Id_UNIQUE` (`Id` ASC),                    ");
            sqlCmd.Append("  UNIQUE INDEX `PartNumber_MachineId_UNIQUE`              ");
            sqlCmd.Append("     (`PartNumber` ASC, `MachineId` ASC, `Operation` ASC) ");
            sqlCmd.Append(")                                                         ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Database_PartNumber_Settings_Table - {ex.Message}", ex);
            }
        }


        public static void Validate_Database_MasterPartNumbers_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS                                          ");
            sqlCmd.Append("        `csi_database`.`tbl_masterpartnumbers` (                    ");
            sqlCmd.Append("   `Id`                       INT          NOT NULL AUTO_INCREMENT, ");
            sqlCmd.Append("   `MachineId`                INT          NOT NULL,                ");
            sqlCmd.Append("   `PartNumber`               VARCHAR(100) NOT NULL,                ");
            sqlCmd.Append("   `Operation`                VARCHAR(100) NULL,                    ");
            sqlCmd.Append("   `CycleTime`                INT          NOT NULL DEFAULT 0,      ");
            sqlCmd.Append("   `DisplayCycleTimeScale`    VARCHAR(1)   NOT NULL DEFAULT 'S',    ");
            sqlCmd.Append("   `CycleMultiplier`          INT          NOT NULL DEFAULT 1,      ");
            sqlCmd.Append("   `SetupTime`                INT          NOT NULL DEFAULT 0,      ");
            sqlCmd.Append("   `DisplaySetupTimeScale`    VARCHAR(1)   NOT NULL DEFAULT 'M',    ");
            sqlCmd.Append("   `PartLoad`                 INT          NOT NULL DEFAULT 0,      ");
            sqlCmd.Append("   `DisplayPartLoadTimeScale` VARCHAR(1)   NOT NULL DEFAULT 'M',    ");
            sqlCmd.Append("   `CreatedWhen`              DATETIME     NOT NULL,                ");
            sqlCmd.Append("   `CreatedBy`                INT          NOT NULL,                ");
            sqlCmd.Append("       PRIMARY KEY(`Id`),                                           ");
            sqlCmd.Append("       UNIQUE INDEX `Id_UNIQUE` (`Id` ASC),                         ");
            sqlCmd.Append("       UNIQUE INDEX `PartNumber_UNIQUE` (`MachineId` ASC, `PartNumber` ASC, `Operation` ASC), ");
            sqlCmd.Append("       INDEX `PartNumber_IDX` (`PartNumber` ASC, `MachineId` ASC, `Operation` ASC), ");
            sqlCmd.Append("       INDEX `MachineId_IDX`  (`MachineId` ASC, `PartNumber` ASC, `Operation` ASC));");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);

                if (ExistsColumn("CycleTimeScale", "csi_database.tbl_masterpartnumbers", connectionString))
                    ExecuteNonQuery("ALTER TABLE `csi_database`.`tbl_masterpartnumbers` CHANGE COLUMN `CycleTimeScale` `DisplayCycleTimeScale` VARCHAR(1) NOT NULL DEFAULT 'S' ; ", connectionString);

                if (ExistsColumn("SetupTimeScale", "csi_database.tbl_masterpartnumbers", connectionString))
                    ExecuteNonQuery("ALTER TABLE `csi_database`.`tbl_masterpartnumbers` CHANGE COLUMN `SetupTimeScale` `DisplaySetupTimeScale` VARCHAR(1) NOT NULL DEFAULT 'M' ; ", connectionString);

                if (ExistsColumn("PartLoadScale", "csi_database.tbl_masterpartnumbers", connectionString))
                    ExecuteNonQuery("ALTER TABLE `csi_database`.`tbl_masterpartnumbers` CHANGE COLUMN `PartLoadScale` `DisplayPartLoadTimeScale` VARCHAR(1) NOT NULL DEFAULT 'M' ; ", connectionString);

                sqlCmd.Clear();
                sqlCmd.Append($"CREATE OR REPLACE VIEW csi_database.vw_masterpartnumbers AS    ");
                sqlCmd.Append($" SELECT                                                        ");
                sqlCmd.Append($"    pn.Id                       AS Id,                         ");
                sqlCmd.Append($"    pn.PartNumber               AS PartNumber,                 ");
                sqlCmd.Append($"    pn.MachineId                AS MachineId,                  ");
                sqlCmd.Append($"    pn.Operation                AS Operation,                  ");
                sqlCmd.Append($"    pn.CycleTime                AS CycleTime,                  ");
                sqlCmd.Append($"    pn.DisplayCycleTimeScale    AS DisplayCycleTimeScale,      ");
                sqlCmd.Append($"    pn.CycleMultiplier          AS CycleMultiplier,            ");
                sqlCmd.Append($"    pn.SetupTime                AS SetupTime,                  ");
                sqlCmd.Append($"    pn.DisplaySetupTimeScale    AS DisplaySetupTimeScale,      ");
                sqlCmd.Append($"    pn.PartLoad                 AS PartLoad,                   ");
                sqlCmd.Append($"    pn.DisplayPartLoadTimeScale AS DisplayPartLoadTimeScale,   ");
                sqlCmd.Append($"    pn.CreatedWhen              AS CreatedWhen,                ");
                sqlCmd.Append($"    pn.CreatedBy                AS CreatedBy,                  ");
                sqlCmd.Append($"    mc.machine_name             AS Machine_Name,               ");
                sqlCmd.Append($"    us.displayname              AS UserDisplayName             ");
                sqlCmd.Append($"FROM                                                           ");
                sqlCmd.Append($"    csi_database.tbl_masterpartnumbers pn                      ");
                sqlCmd.Append($"    LEFT JOIN csi_auth.tbl_ehub_conf mc ON pn.MachineId = mc.id");
                sqlCmd.Append($"    LEFT JOIN csi_auth.users         us ON pn.CreatedBy = us.Id");

                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Database_MasterPartNumbers_Table - {ex.Message}", ex);
            }

            AddDatabaseTableBckp("tbl_masterpartnumbers");
        }


        public static void Validate_Database_HistoryPartNumbers_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS                                          ");
            sqlCmd.Append("        `csi_database`.`tbl_historypartnumbers` (                   ");
            sqlCmd.Append("   `Id`                       INT          NOT NULL AUTO_INCREMENT, ");
            sqlCmd.Append("   `PartNumber`               VARCHAR(100) NOT NULL,                ");
            sqlCmd.Append("   `MachineId`                INT          NOT NULL,                ");
            sqlCmd.Append("   `Operation`                VARCHAR(100) NULL,                    ");
            sqlCmd.Append("   `CycleTime`                INT          NOT NULL DEFAULT 0,      ");
            sqlCmd.Append("   `DisplayCycleTimeScale`    VARCHAR(1)   NOT NULL DEFAULT 'S',    ");
            sqlCmd.Append("   `CycleMultiplier`          INT          NOT NULL DEFAULT 1,      ");
            sqlCmd.Append("   `SetupTime`                INT          NOT NULL DEFAULT 0,      ");
            sqlCmd.Append("   `DisplaySetupTimeScale`    VARCHAR(1)   NOT NULL DEFAULT 'M',    ");
            sqlCmd.Append("   `PartLoad`                 INT          NOT NULL DEFAULT 0,      ");
            sqlCmd.Append("   `DisplayPartLoadTimeScale` VARCHAR(1)   NOT NULL DEFAULT 'M',    ");
            sqlCmd.Append("   `CreatedWhen`              DATETIME     NOT NULL,                ");
            sqlCmd.Append("   `CreatedBy`                INT          NOT NULL,                ");
            sqlCmd.Append("       PRIMARY KEY(`Id`),                                           ");
            sqlCmd.Append("       UNIQUE INDEX `Id_UNIQUE` (`Id` ASC),                         ");
            sqlCmd.Append("       INDEX `PartNumber_IDX` (`PartNumber` ASC, `MachineId` ASC, `Operation` ASC), ");
            sqlCmd.Append("       INDEX `MachineId_IDX`  (`MachineId` ASC, `PartNumber` ASC, `Operation` ASC));");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);

                if (ExistsColumn("CycleTimeScale", "csi_database.tbl_historypartnumbers", connectionString))
                    ExecuteNonQuery("ALTER TABLE `csi_database`.`tbl_historypartnumbers` CHANGE COLUMN `CycleTimeScale` `DisplayCycleTimeScale` VARCHAR(1) NOT NULL DEFAULT 'S' ; ", connectionString);

                if (ExistsColumn("SetupTimeScale", "csi_database.tbl_historypartnumbers", connectionString))
                    ExecuteNonQuery("ALTER TABLE `csi_database`.`tbl_historypartnumbers` CHANGE COLUMN `SetupTimeScale` `DisplaySetupTimeScale` VARCHAR(1) NOT NULL DEFAULT 'M' ; ", connectionString);

                if (ExistsColumn("PartLoadScale", "csi_database.tbl_historypartnumbers", connectionString))
                    ExecuteNonQuery("ALTER TABLE `csi_database`.`tbl_historypartnumbers` CHANGE COLUMN `PartLoadScale` `DisplayPartLoadTimeScale` VARCHAR(1) NOT NULL DEFAULT 'M' ; ", connectionString);

                sqlCmd.Clear();
                sqlCmd.Append($"CREATE OR REPLACE VIEW csi_database.vw_historypartnumbers AS   ");
                sqlCmd.Append($" SELECT                                                        ");
                sqlCmd.Append($"    pn.Id                       AS Id,                         ");
                sqlCmd.Append($"    pn.PartNumber               AS PartNumber,                 ");
                sqlCmd.Append($"    pn.MachineId                AS MachineId,                  ");
                sqlCmd.Append($"    pn.Operation                AS Operation,                  ");
                sqlCmd.Append($"    pn.CycleTime                AS CycleTime,                  ");
                sqlCmd.Append($"    pn.DisplayCycleTimeScale    AS DisplayCycleTimeScale,      ");
                sqlCmd.Append($"    pn.CycleMultiplier          AS CycleMultiplier,            ");
                sqlCmd.Append($"    pn.SetupTime                AS SetupTime,                  ");
                sqlCmd.Append($"    pn.DisplaySetupTimeScale    AS DisplaySetupTimeScale,      ");
                sqlCmd.Append($"    pn.PartLoad                 AS PartLoad,                   ");
                sqlCmd.Append($"    pn.DisplayPartLoadTimeScale AS DisplayPartLoadTimeScale,   ");
                sqlCmd.Append($"    pn.CreatedWhen              AS CreatedWhen,                ");
                sqlCmd.Append($"    pn.CreatedBy                AS CreatedBy,                  ");
                sqlCmd.Append($"    mc.machine_name             AS Machine_Name,               ");
                sqlCmd.Append($"    us.displayname              AS UserDisplayName             ");
                sqlCmd.Append($"FROM                                                           ");
                sqlCmd.Append($"    csi_database.tbl_historypartnumbers pn                     ");
                sqlCmd.Append($"    LEFT JOIN csi_auth.tbl_ehub_conf mc ON pn.MachineId = mc.id");
                sqlCmd.Append($"    LEFT JOIN csi_auth.users         us ON pn.CreatedBy = us.Id");

                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Database_HistoryPartNumbers_Table - {ex.Message}", ex);
            }
        }


        /// <summary>
        /// Create the table CSI_Database.tbl_renamemachines if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Database_RenameMachines_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS csi_database.tbl_renamemachines ");
            sqlCmd.Append("(                                                          ");
            sqlCmd.Append("    `table_name`    varchar(255) PRIMARY KEY,              ");
            sqlCmd.Append("    `original_name` varchar(255)                           ");
            sqlCmd.Append(")                                                          ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Database_RenameMachines_Table - {ex.Message}", ex);
            }
        }


        public static void Validate_Database_ReportedParts_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS csi_database.tbl_reportedparts ");
            sqlCmd.Append("(                                                         ");
            sqlCmd.Append("    `machine`          VARCHAR(25) PRIMARY KEY,           ");
            sqlCmd.Append("    `trx_time`         DATETIME    NOT NULL,              ");
            sqlCmd.Append("    `total_parts`      INT         NOT NULL,              ");
            sqlCmd.Append("    `bad_parts`        INT         NOT NULL,              ");
            sqlCmd.Append("    `HEADPALLET`       INT         NOT NULL,              ");
            sqlCmd.Append("    `ideal_cycle_time` INT         NOT NULL,              ");
            sqlCmd.Append("  INDEX ( trx_time )                                      ");
            sqlCmd.Append(")                                                         ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Database_ReportedParts_Table - {ex.Message}", ex);
            }
        }


        /// <summary>
        /// Create the table CSI_Database.tbl_rm_port if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Database_RMPort_Table(string connectionString = mySqlConnectionString)
        {

            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS csi_database.tbl_rm_port ");
            sqlCmd.Append("(                                                   ");
            sqlCmd.Append("    `port`    integer                               ");
            sqlCmd.Append(")                                                   ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Database_RMPort_Table - {ex.Message}", ex);
            }
        }


        /// <summary>
        /// Create the table CSI_Database.tbl_userconfig if it doesn't exist
        /// </summary>
        /// <param name="connectionString">Optional - Connection string with the database. If it is not informed it is used the default database</param>
        public static void Validate_Database_UserConfig_Table(string connectionString = mySqlConnectionString)
        {

            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS csi_database.tbl_userconfig ");
            sqlCmd.Append("(                                                      ");
            sqlCmd.Append("    `name`   varchar(255) PRIMARY KEY,                 ");
            sqlCmd.Append("    `state`  text                                      ");
            sqlCmd.Append(")                                                      ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Database_UserConfig_Table - {ex.Message}", ex);
            }
        }


        public static void Validate_Database_Machine_Table(string machineName, string connectionString = mySqlConnectionString)
        {
            string tableName = $"csi_database.{ Util.MachineDbTableName(machineName) }";
            string viewName = $"vw_{machineName.ReplaceSymbols("")}";

            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append($"CREATE TABLE IF NOT EXISTS { tableName }          ");
            sqlCmd.Append($"(                                                 ");
            sqlCmd.Append($"    `month_`      int(11) DEFAULT NULL,           ");
            sqlCmd.Append($"    `day_`        int(11) DEFAULT NULL,           ");
            sqlCmd.Append($"    `year_`       int(11) DEFAULT NULL,           ");
            sqlCmd.Append($"    `ShiftDate`   datetime DEFAULT NULL,          ");
            sqlCmd.Append($"    `Date_`       datetime DEFAULT NULL,          ");
            sqlCmd.Append($"    `status`      varchar(255) DEFAULT NULL,      ");
            sqlCmd.Append($"    `shift`       int(11) DEFAULT NULL,           ");
            sqlCmd.Append($"    `cycletime`   int(11) DEFAULT NULL,           ");
            sqlCmd.Append($"    `Partnumber`  varchar(255) DEFAULT NULL,      ");
            sqlCmd.Append($"    `Operation`   int(11) DEFAULT 0,              ");
            sqlCmd.Append($"    `Operator`    varchar(255) DEFAULT NULL,      ");
            sqlCmd.Append($"    `Comments`    varchar(255) DEFAULT NULL,      ");
            sqlCmd.Append($"    `Deleted`     TINYINT NULL DEFAULT 0,         ");
            sqlCmd.Append($" UNIQUE KEY `DateStatus` (`Date_`,`status`),      ");
            sqlCmd.Append($" INDEX      `Date_` (`Date_`)                     ");
            sqlCmd.Append($")                                                ;");
            //sqlCmd.Append($" ENGINE=InnoDB DEFAULT CHARSET=utf8;              ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error($"Error creating the machine {machineName} table.", ex);

                //string q = $"SELECT COUNT(TABLE_NAME) FROM information_schema.TABLES WHERE TABLE_SCHEMA LIKE 'csi_database' AND TABLE_TYPE LIKE 'BASE TABLE' AND TABLE_NAME = '{tableName}';";
                //if (ExecuteNonQuery(q, connectionString) == 0)
                //    throw;
            }

            try
            {

                if (!ExistsColumn("Id", tableName, connectionString))
                {
                    sqlCmd.Clear();
                    sqlCmd.Append($"ALTER TABLE {tableName}                                ");
                    sqlCmd.Append($"    ADD COLUMN `Id` INT NOT NULL AUTO_INCREMENT FIRST, ");
                    sqlCmd.Append($"    ADD PRIMARY KEY(`Id`)                            , ");
                    sqlCmd.Append($"    ADD UNIQUE INDEX `Id_UNIQUE` (`Id` ASC)          ; ");
                    ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                }

                if (!ExistsColumn("Operation", tableName, connectionString))
                    AddColumn("Operation", tableName, "INT(11) Default 0", connectionString);

                if (!ExistsColumn("Operator", tableName, connectionString))
                    AddColumn("Operator", tableName, "varchar(255) Default NULL", connectionString);

                if (!ExistsColumn("Comments", tableName, connectionString))
                    AddColumn("Comments", tableName, "varchar(255) Default NULL", connectionString);

                if (ExistsColumn("Time_", tableName, connectionString))
                {
                    try
                    {
                        sqlCmd.Clear();
                        sqlCmd.Append($"ALTER TABLE {tableName} ");
                        sqlCmd.Append($"    DROP INDEX `time_` ;");
                        ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                    }
                    catch (Exception) { }

                    try
                    {
                        sqlCmd.Clear();
                        sqlCmd.Append($"ALTER TABLE {tableName}  ");
                        sqlCmd.Append($"    CHANGE COLUMN `time_` `ShiftDate` DATE NULL DEFAULT NULL;");
                        ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                    }
                    catch (Exception) { }

                    try
                    {
                        sqlCmd.Clear();
                        sqlCmd.Append($"ALTER TABLE {tableName}  ");
                        sqlCmd.Append($"    ADD UNIQUE INDEX `DateStatus` (`Date_` ASC, `status` ASC);");
                        ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                    }
                    catch (Exception) { }

                    ExecuteNonQuery($"UPDATE {tableName} SET ShiftDate = Date(`ShiftDate`);", connectionString);

                    UpdateShiftDate(tableName, connectionString);
                }

                //sqlCmd.Clear();
                //sqlCmd.Append($"SELECT EXISTS(                           ");
                //sqlCmd.Append($"    SELECT * FROM                        ");
                //sqlCmd.Append($"        information_schema.tables        ");
                //sqlCmd.Append($"    WHERE                                ");
                //sqlCmd.Append($"        TABLE_SCHEMA = 'csi_database'    ");
                //sqlCmd.Append($"    AND TABLE_TYPE   = 'VIEW'            ");
                //sqlCmd.Append($"    AND TABLE_NAME   = '{viewName}'      ");
                //sqlCmd.Append($");                                       ");

                //string existsView = ExecuteScalar(sqlCmd.ToString()).ToString();
                //if (existsView != "1")
                //{
                //}

                sqlCmd.Clear();
                sqlCmd.Append($"CREATE OR REPLACE VIEW  ");
                sqlCmd.Append($"   `csi_database`.`{viewName}` AS        ");
                sqlCmd.Append($"    SELECT                               ");
                sqlCmd.Append($"        `Id`            AS `Id`        , ");
                sqlCmd.Append($"        '{machineName}' AS `Machine`   , ");
                sqlCmd.Append($"        `Date_`         AS `Date`      , ");
                sqlCmd.Append($"        `status`        AS `Status`    , ");
                sqlCmd.Append($"        `shift`         AS `Shift`     , ");
                sqlCmd.Append($"        `ShiftDate`     AS `ShiftDate` , ");
                sqlCmd.Append($"        `cycletime`     AS `CycleTime`   ");
                sqlCmd.Append($"    FROM                                 ");
                sqlCmd.Append($"        { tableName }                    ");
                sqlCmd.Append($"    WHERE                                ");
                sqlCmd.Append($"        (NOT(`status` LIKE '\\_%'))    ; ");

                sqlCmd.Append($"GRANT SELECT ON `csi_database`.`{viewName}` TO 'cfdbview'@'%';");

                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error($"Error updating the machine {machineName} table.", ex);
                throw new Exception($"Validate_Database_Machine_Table - {ex.Message}", ex);
            }
        }


        public static void Validate_Database_TempMachine_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append($"CREATE TABLE IF NOT EXISTS csi_database.tempMachine   ");
            sqlCmd.Append($"(                                                     ");
            sqlCmd.Append($"    `MachineName`   varchar(20) DEFAULT NULL,         ");
            sqlCmd.Append($"    `Date_`         datetime    DEFAULT NULL,         ");
            sqlCmd.Append($"    `Shift`         varchar(10) DEFAULT NULL,         ");
            sqlCmd.Append($"    `StartTime`     varchar(10) DEFAULT NULL,         ");
            sqlCmd.Append($"    `EndTime`       varchar(10) DEFAULT NULL,         ");
            sqlCmd.Append($"    `ElapsedTime`   varchar(10) DEFAULT NULL,         ");
            sqlCmd.Append($"    `MachineStatus` varchar(50) DEFAULT NULL,         ");
            sqlCmd.Append($"    `HeadPallet`    varchar(10) DEFAULT NULL,         ");
            sqlCmd.Append($"    `Comment`       varchar(255) DEFAULT NULL,        ");
            sqlCmd.Append($"  KEY idxMachineName (MachineName)                    ");
            sqlCmd.Append($")                                                     ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Database_TempMachine_Table - {ex.Message}", ex);
            }
        }


        private static void UpdateShiftDate(string tableName, string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            DataTable dt = GetDataTable($"SELECT * FROM { tableName }", connectionString);

            if (dt != null)
            {
                int id = 0;
                DateTime firstDateTime = new DateTime();
                DateTime eventDateTime = new DateTime();
                DateTime shiftDate = new DateTime(1, 1, 1);
                int currentShift = 0;
                int shift = 0;

                foreach (DataRow row in dt.Rows)
                {
                    shift = int.Parse(row["shift"].ToString());

                    if (shift != currentShift)
                    {
                        currentShift = shift;
                        shiftDate = DateTime.Parse(row["Date_"].ToString());
                        firstDateTime = shiftDate;

                        if (!string.IsNullOrEmpty(sqlCmd.ToString()))
                        {
                            ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                            sqlCmd.Clear();
                        }
                    }

                    id = int.Parse(row["Id"].ToString());
                    eventDateTime = DateTime.Parse(row["Date_"].ToString());

                    if (eventDateTime < firstDateTime)
                    {
                        sqlCmd.Append($"UPDATE {tableName} SET Date_ = '{eventDateTime.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss")}' WHERE Id = {id}; ");
                    }
                }

                if (!string.IsNullOrEmpty(sqlCmd.ToString()))
                {
                    ExecuteNonQuery(sqlCmd.ToString(), connectionString);
                }
            }
        }

        #endregion


        #region VALIDATE CSI_MACHINEPERF DATABASE TABLES ==========================================================================================================================


        public static void Validate_MachinePerf_Database(string connectionString = mySqlConnectionString)
        {
            string sqlCmd = "CREATE DATABASE IF NOT EXISTS csi_machineperf;";

            try
            {
                ExecuteNonQuery(sqlCmd);

                Validate_MachinePerf_Perf_Table();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"UpdateShiftDate - {ex.Message}", ex);
            }
        }


        public static void Delete_All_MachinePerf_Tables(string connectionString = mySqlConnectionString)
        {
            string sqlCmd = "SELECT " +
                            "    CONCAT( 'DROP TABLE IF EXISTS `', table_schema, '`.`', table_name, '`;' ) As DropCommand " +
                            "FROM " +
                            "    information_schema.tables " +
                            "WHERE " +
                            "    table_schema = 'csi_machineperf'; ";

            DataTable dtTables = GetDataTable(sqlCmd, connectionString);

            foreach (DataRow row in dtTables.Rows)
            {
                ExecuteNonQuery(row["DropCommand"].ToString());
            }

            Validate_MachinePerf_Perf_Table();
        }


        public static void Validate_MachinePerf_Perf_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS csi_machineperf.tbl_perf ");
            sqlCmd.Append("(                                                   ");
            sqlCmd.Append("   `machinename_`  varchar(255),                    ");
            sqlCmd.Append("   `weekly_`       MEDIUMTEXT,                      ");
            sqlCmd.Append("   `monthly_`      MEDIUMTEXT,                      ");
            sqlCmd.Append("  PRIMARY KEY (machinename_)                        ");
            sqlCmd.Append(")                                                   ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_MachinePerf_Perf_Table - {ex.Message}", ex);
            }
        }


        public static void Validate_Perf_Machine_Table(string machineName, string connectionString = mySqlConnectionString)
        {
            string tableName = $"csi_machineperf.{ Util.MachineDbTableName(machineName) }";

            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append($"CREATE TABLE IF NOT EXISTS { tableName }              ");
            sqlCmd.Append($"(                                                     ");
            sqlCmd.Append($"    `Id`                INT NOT NULL AUTO_INCREMENT,  ");
            sqlCmd.Append($"    `status`            varchar(45),                  ");
            sqlCmd.Append($"    `time`              integer,                      ");
            sqlCmd.Append($"    `cycletime`         integer,                      ");
            sqlCmd.Append($"    `shift`             integer,                      ");
            sqlCmd.Append($"    `date`              datetime,                     ");
            sqlCmd.Append($"    `No_of_Head_Pallet` integer DEFAULT '0',          ");
            sqlCmd.Append($"    `partnumber`        varchar(255) DEFAULT '',      ");
            sqlCmd.Append($"    `operator`          varchar(255) DEFAULT '',      ");
            sqlCmd.Append($"    `comments`          varchar(255) DEFAULT '',      ");
            sqlCmd.Append($" PRIMARY KEY(`Id`),                                   ");
            sqlCmd.Append($" UNIQUE INDEX `Id_UNIQUE` (`Id` ASC),                 ");
            sqlCmd.Append($" UNIQUE KEY ( `time`, `status`, `No_of_Head_Pallet` ) ");
            sqlCmd.Append($")                                                     ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);

                if (!ExistsColumn("partnumber", tableName))
                {
                    AddColumn("partnumber", tableName, "varchar(255) DEFAULT NULL", connectionString);
                }
                if (!ExistsColumn("operator", tableName))
                {
                    AddColumn("operator", tableName, "varchar(255) DEFAULT NULL", connectionString);
                }
                if (!ExistsColumn("comments", tableName))
                {
                    AddColumn("comments", tableName, "varchar(255) DEFAULT NULL", connectionString);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"Validate_Perf_Machine_Table - {ex.Message}", ex);
            }
        }


        #endregion


        #region VALIDATE MONITORING DATABASE TABLES ==========================================================================================================================


        public static void Validate_Monitoring_Database(string connectionString = mySqlConnectionString)
        {
            string sqlCmd = "CREATE DATABASE IF NOT EXISTS monitoring;";

            try
            {
                ExecuteNonQuery(sqlCmd);

                Validate_Monitoring_Metrics_Table(connectionString);

                Validate_Monitoring_MonitoringBoards_Table(connectionString);

                Validate_Monitoring_Sensors_Table(connectionString);

                Validate_Monitoring_SensorReadings_Table(connectionString);

                Validate_Monitoring_SensorCurrentReadings_Table(connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($" - {ex.Message}", ex);
            }
        }


        public static void Validate_Monitoring_Metrics_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS monitoring.metrics (             ");
            sqlCmd.Append("   `Id`         INT NOT NULL AUTO_INCREMENT,                ");
            sqlCmd.Append("   `Name`       VARCHAR(50) NOT NULL,                       ");
            sqlCmd.Append("   `Unit`       VARCHAR(50) DEFAULT NULL,                   ");
            sqlCmd.Append("   `Code`       VARCHAR(10) NOT NULL,                       ");
            sqlCmd.Append("   PRIMARY KEY(`Id`),                                       ");
            sqlCmd.Append("   UNIQUE KEY `metric` (`name`)                             ");
            sqlCmd.Append(")                                                           ");
            sqlCmd.Append("ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci; ");

            sqlCmd.Append("INSERT IGNORE INTO monitoring.metrics ");
            sqlCmd.Append("(                              ");
            sqlCmd.Append("     Id   ,                    ");
            sqlCmd.Append("     Name ,                    ");
            sqlCmd.Append("     Unit ,                    ");
            sqlCmd.Append("     Code                      ");
            sqlCmd.Append(") VALUES (                     ");
            sqlCmd.Append("     1          ,              ");
            sqlCmd.Append("     'Pressure' ,              ");
            sqlCmd.Append("     'psi'      ,              ");
            sqlCmd.Append("     'P'                       ");
            sqlCmd.Append(");                             ");

            sqlCmd.Append("INSERT IGNORE INTO monitoring.metrics ");
            sqlCmd.Append("(                              ");
            sqlCmd.Append("     Id   ,                    ");
            sqlCmd.Append("     Name ,                    ");
            sqlCmd.Append("     Unit ,                    ");
            sqlCmd.Append("     Code                      ");
            sqlCmd.Append(") VALUES (                     ");
            sqlCmd.Append("     2             ,           ");
            sqlCmd.Append("     'Temperature' ,           ");
            sqlCmd.Append("     'C'           ,           ");
            sqlCmd.Append("     'T'                       ");
            sqlCmd.Append(");                             ");

            sqlCmd.Append("INSERT IGNORE INTO monitoring.metrics ");
            sqlCmd.Append("(                              ");
            sqlCmd.Append("     Id   ,                    ");
            sqlCmd.Append("     Name ,                    ");
            sqlCmd.Append("     Unit ,                    ");
            sqlCmd.Append("     Code                      ");
            sqlCmd.Append(") VALUES (                     ");
            sqlCmd.Append("     3          ,              ");
            sqlCmd.Append("     'Battery'  ,              ");
            sqlCmd.Append("     '%'        ,              ");
            sqlCmd.Append("     'B'                       ");
            sqlCmd.Append(");                             ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($" - {ex.Message}", ex);
            }
        }


        public static void Validate_Monitoring_MonitoringBoards_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS monitoring.monitoringboards        ");
            sqlCmd.Append("(                                                             ");
            sqlCmd.Append("   `Id`             INT(11)      NOT NULL AUTO_INCREMENT,     ");
            sqlCmd.Append("   `CompanyId`      INT(11)      NOT NULL DEFAULT 0,          ");
            sqlCmd.Append("   `SerialNumber`   varchar(100) NOT NULL,                    ");
            sqlCmd.Append("   `Model`          varchar(50)  NOT NULL,                    ");
            sqlCmd.Append("   `Mac`            varchar(20)  NOT NULL,                    ");
            sqlCmd.Append("   `Name`           varchar(50)  NOT NULL,                    ");
            sqlCmd.Append("   `Manufacturer`   varchar(100) NOT NULL,                    ");
            sqlCmd.Append("   `Description`    text         ,                            ");
            sqlCmd.Append("   `IpAddress`      varchar(20)  DEFAULT NULL,                ");
            sqlCmd.Append("   `Firmware`       varchar(20)  NOT NULL,                    ");
            sqlCmd.Append("   `Target`         varchar(100) DEFAULT NULL,                ");
            sqlCmd.Append("   `Tags`           text         ,                            ");
            sqlCmd.Append("   `data`           text         ,                            ");
            sqlCmd.Append("   `CreatedAt`      datetime     NOT NULL,                    ");
            sqlCmd.Append("   `Enabled`        tinyint(4)   NOT NULL DEFAULT '1',        ");
            sqlCmd.Append("   `Deleted`        tinyint(4)   NOT NULL DEFAULT '0',        ");
            sqlCmd.Append(" UNIQUE KEY(`Id`)                                             ");
            sqlCmd.Append(") ENGINE = InnoDB AUTO_INCREMENT = 11 DEFAULT CHARSET = utf8; ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($" - {ex.Message}", ex);
            }
        }


        public static void Validate_Monitoring_Sensors_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS monitoring.sensors           ");
            sqlCmd.Append("(                                                       ");
            sqlCmd.Append("   `Id`           INT(11)      NOT NULL AUTO_INCREMENT, ");
            sqlCmd.Append("   `BoardId`      INT(11)      NOT NULL DEFAULT 0,      ");
            sqlCmd.Append("   `Mac`          varchar(20)  NOT NULL,                ");
            sqlCmd.Append("   `Name`         varchar(50)  NOT NULL,                ");
            sqlCmd.Append("   `Description`  text         ,                        ");
            sqlCmd.Append("   `SerialNumber` varchar(50)  NOT NULL,                ");
            sqlCmd.Append("   `Manufacturer` varchar(50)  NOT NULL,                ");
            sqlCmd.Append("   `Model`        varchar(50)  DEFAULT NULL,            ");
            sqlCmd.Append("   `Type`         varchar(50)  DEFAULT NULL,            ");
            sqlCmd.Append("   `Group`        varchar(50)  DEFAULT NULL,            ");
            sqlCmd.Append("   `Target`       varchar(100) DEFAULT NULL,            ");
            sqlCmd.Append("   `Tags`         text         ,                        ");
            sqlCmd.Append("   `data`         text         ,                        ");
            sqlCmd.Append("   `Deleted`      tinyint(4)   NOT NULL DEFAULT '0',    ");
            sqlCmd.Append("  UNIQUE KEY(`Id`)                                      ");
            sqlCmd.Append(") ENGINE = InnoDB DEFAULT CHARSET = utf8;               ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($" - {ex.Message}", ex);
            }
        }


        public static void Validate_Monitoring_SensorReadings_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS monitoring.sensorreadings          ");
            sqlCmd.Append("(                                                             ");
            sqlCmd.Append("   `Id`             bigint(20)   NOT NULL AUTO_INCREMENT,     ");
            sqlCmd.Append("   `SensorId`       int(11)      NOT NULL DEFAULT 0,          ");
            sqlCmd.Append("   `OccuredAt`      datetime     NOT NULL,                    ");
            sqlCmd.Append("   `MetricId`       int(11)      NOT NULL DEFAULT 0,          ");
            sqlCmd.Append("   `Value`          decimal(10,3) NOT NULL,                   ");
            sqlCmd.Append("   `Data`           varchar(100) DEFAULT NULL,                ");
            sqlCmd.Append("  UNIQUE KEY(`Id`),                                           ");
            sqlCmd.Append("  KEY `IX_SensorReadings_MetricId` (`MetricId`),              ");
            sqlCmd.Append("  KEY `IX_SensorReadings_SensorId` (`SensorId`),              ");
            sqlCmd.Append("  CONSTRAINT `FK_SensorReadings_Metrics` FOREIGN KEY(`MetricId`) REFERENCES `monitoring`.`metrics` (`Id`),                  ");
            sqlCmd.Append("  CONSTRAINT `FK_SensorReadings_Sensors` FOREIGN KEY(`SensorId`) REFERENCES `monitoring`.`sensors` (`Id`) ON DELETE CASCADE ");
            sqlCmd.Append(") ENGINE = InnoDB DEFAULT CHARSET = utf8;                     ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($" - {ex.Message}", ex);
            }
        }


        public static void Validate_Monitoring_SensorCurrentReadings_Table(string connectionString = mySqlConnectionString)
        {
            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append("CREATE TABLE IF NOT EXISTS monitoring.sensorcurrentreadings    ");
            sqlCmd.Append("(                                                              ");
            sqlCmd.Append("   `Id`                 bigint(20)    NOT NULL AUTO_INCREMENT, ");
            sqlCmd.Append("   `MachineId`          int(11)       NOT NULL DEFAULT 0,      ");
            sqlCmd.Append("   `BoardName`          varchar(45)   DEFAULT NULL,            ");
            sqlCmd.Append("   `Timestamp`          datetime      NOT NULL,                ");
            sqlCmd.Append("   `CurrentTime`        datetime      NOT NULL,                ");
            sqlCmd.Append("   `IsMonitoring`       tinyint(4)    NOT NULL,                ");
            sqlCmd.Append("   `IsSensorAvailable`  tinyint(4)    NOT NULL,                ");
            sqlCmd.Append("   `IsOverride`         tinyint(4)    NOT NULL,                ");
            sqlCmd.Append("   `IsAlarming`         tinyint(4)    NOT NULL,                ");
            sqlCmd.Append("   `IsCSD`              tinyint(4)    NOT NULL,                ");
            sqlCmd.Append("   `CurrentPallet`      varchar(45)   DEFAULT NULL,            ");
            sqlCmd.Append("   `SensorName`         varchar(45)   DEFAULT NULL,            ");
            sqlCmd.Append("   `SensorGroup`        varchar(45)   DEFAULT NULL,            ");
            sqlCmd.Append("   `Metric`             varchar(45)   DEFAULT NULL,            ");
            sqlCmd.Append("   `Value`              decimal(10,3) NOT NULL,                ");
            sqlCmd.Append("  UNIQUE KEY(`Id`)                                             ");
            sqlCmd.Append(") ENGINE = InnoDB DEFAULT CHARSET = utf8;                      ");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);

                if (!ExistsColumn("IsSensorAvailable", "monitoring.sensorcurrentreadings", connectionString))
                {
                    AddColumn("IsSensorAvailable", "monitoring.sensorcurrentreadings", "tinyint(4) NOT NULL AFTER `IsMonitoring`", connectionString);
                }

                if (!ExistsColumn("CurrentTime", "monitoring.sensorcurrentreadings", connectionString))
                {
                    AddColumn("CurrentTime", "monitoring.sensorcurrentreadings", "datetime AFTER `Timestamp`", connectionString);
                    ExecuteNonQuery("UPDATE monitoring.sensorcurrentreadings SET CurrentTime = Timestamp", connectionString);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($" - {ex.Message}", ex);
            }
        }

        #endregion


        public static bool ExistsColumn(string column, string table, string connectionString = mySqlConnectionString)
        {
            var col = GetColumnNames(table, connectionString).FirstOrDefault(c => c == column.ToUpper());

            return !String.IsNullOrEmpty(col);
        }


        public static List<string> GetColumnNames(string table, string connectionString = mySqlConnectionString)
        {
            List<string> columns = new List<string>();

            string[] schema = table.Split('.');

            string sqlCmd = $"SELECT column_name, data_type FROM information_schema.columns WHERE table_schema = '{schema[0]}' AND table_name = '{schema[1]}';";

            try
            {
                DataTable dt = GetDataTable(sqlCmd, connectionString);

                foreach (DataRow row in dt.Rows)
                {
                    columns.Add(row[0].ToString().ToUpper());
                }

                return columns;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }


        public static string GetColumnType(string column, string table, string connectionString = mySqlConnectionString)
        {
            string sqlCmd = $"SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = '{table}' AND COLUMN_NAME = '{column}';";

            try
            {
                var dataType = ExecuteScalar(sqlCmd, connectionString).ToString();

                if (String.IsNullOrEmpty(dataType))
                    return "";
                else
                    return dataType;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }

        }


        public static void AddColumn(string columnName, string table, string columnType, string connectionString = mySqlConnectionString)
        {
            string sqlCmd = $"ALTER TABLE {table} ADD COLUMN `{columnName}` {columnType}";

            if (ExistsColumn(columnName, table, connectionString))
                return;

            try
            {
                ExecuteNonQuery(sqlCmd, connectionString);
            }
            catch (Exception ex)
            {
                throw new Exception($" - {ex.Message}", ex);
            }
        }


        public static bool ExistsIndex(string indexName, string table, string columns, string connectionString = mySqlConnectionString)
        {
            string cmd = $"SHOW INDEX FROM {table} WHERE Key_name = '{ indexName }'";

            DataTable indexTable = GetDataTable(cmd, connectionString);

            if (string.IsNullOrEmpty(columns))
                return indexTable.Rows.Count > 0;


            string[] cols = columns.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => $"{s.Trim().ToUpper()}").ToArray();

            int idx = 0;
            string colName;
            foreach (DataRow row in indexTable.Rows)
            {
                colName = row["Column_name"].ToString().ToUpper();
                if (cols.Length < idx + 1 || colName != cols[idx++])
                    return false;
            }

            if (cols.Length >= idx + 1)
                return false;

            return true;
        }


        public static void AddUpdateIndex(string indexName, string table, string indexType, string columns, string connectionString = mySqlConnectionString)
        {
            string[] cols = columns.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => $"'{s.Trim()}'").ToArray();

            if (indexType.ToUpper() != "INDEX")
                indexType += " INDEX";

            StringBuilder sqlCmd = new StringBuilder();

            sqlCmd.Append($"ALTER TABLE {table} ");

            if (ExistsIndex(indexName, table, "", connectionString))
                sqlCmd.Append($"DROP INDEX {indexName}, ");

            sqlCmd.Append($"ADD { indexType } `{indexName}` ({ string.Join(",", columns) })");

            try
            {
                ExecuteNonQuery(sqlCmd.ToString(), connectionString);
            }
            catch (Exception ex)
            {
                throw new Exception($" - {ex.Message}", ex);
            }
        }


        public static string GetFieldValue(string query, string field, string connectionString = mySqlConnectionString)
        {
            try
            {
                DataTable dt = GetDataTable(query);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    return row[field].ToString();
                }
                else
                    return "";

            }
            catch (Exception ex)
            {
                throw new Exception($" - {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Function to insert a registry in a table
        /// </summary>
        /// <param name="table">Name of the table that will receive the registry</param>
        /// <param name="columns">Columns of the table</param>
        /// <param name="values">Values of each column</param>
        /// <param name="allowDuplicating"></param>
        /// <param name="connectionString"></param>
        public static void InputGenericRecord(string table, string[] columns, string[] values, bool allowDuplicating = false, string connectionString = mySqlConnectionString)
        {
            try
            {
                DataTable dt = GetDataTable($"SELECT * FROM {table} LIMIT 1", connectionString);

                StringBuilder queryColumns = new StringBuilder();
                StringBuilder queryValues = new StringBuilder();

                int idx;
                bool firstParam = true;

                foreach (DataColumn column in dt.Columns)
                {
                    idx = Array.IndexOf(columns, column.ColumnName);

                    if (idx >= 0)
                    {
                        if (!firstParam)
                        {
                            queryColumns.Append(", ");
                            queryValues.Append(", ");
                        }

                        queryColumns.Append($"`{columns[idx]}`");

                        switch (column.DataType.Name)
                        {
                            case "String":
                                if (values[idx].Contains("\""))
                                {
                                    values[idx] = values[idx].Replace("\"", "'");
                                }

                                queryValues.Append($"\"{values[idx]}\"");

                                break;

                            case "Int32":
                                if (String.IsNullOrEmpty(values[idx]))
                                    queryValues.Append($"0");
                                else
                                    queryValues.Append($"{values[idx]}");
                                break;
                            default:
                                queryValues.Append($"{values[idx]}");
                                break;
                        }

                        firstParam = false;
                        idx++;
                    }
                }

                string sqlCmd;

                if (allowDuplicating)
                    sqlCmd = $"INSERT INTO {table} ( {queryColumns.ToString()} ) VALUES ( {queryValues.ToString()} );";
                else
                    sqlCmd = $"INSERT IGNORE INTO {table} ( {queryColumns.ToString()} ) VALUES ( {queryValues.ToString()} );";

                ExecuteNonQuery(sqlCmd, connectionString);

            }
            catch (Exception ex)
            {
                throw new Exception($"Insert Error \n{ex.Message}", ex);
            }
        }
    }

    public static class MySQLColumnType
    {
        public const string BOOLEAN = "tinyint";
        public const string INTEGER = "int";
        public const string TEXT = "text";
        public const string VARCHAR = "varchar";
    }
}
