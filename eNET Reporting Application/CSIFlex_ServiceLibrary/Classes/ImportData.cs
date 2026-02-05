using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFlex_ServiceLibrary.Classes
{
    public class ImportData
    {
        public static MySqlConnection mysqlcon = null;
        public static CSI_Library.CSI_Library csi_lib = null;
        private DataLayer _dataLayer = null;
        private ReadFiles _readFiles = null;
        private string serverName;
        private string serverProgramData;
        private string serverENETPath;

        public ImportData()
        {
            mysqlcon = new MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString);
            csi_lib = new CSI_Library.CSI_Library();
            serverName = ConfigurationManager.AppSettings["SERVER_NAME"];
            serverProgramData = serverName + ConfigurationManager.AppSettings["SERVER_PROGRAM_DATA"];
            serverENETPath = serverName + ConfigurationManager.AppSettings["SERVER_ENET_PATH"];
            _dataLayer = new DataLayer();
            _readFiles = new ReadFiles();
        }

        public void LoadFiles_Load()
        {
            try
            {
                if (mysqlcon != null && mysqlcon.State == ConnectionState.Closed)
                { mysqlcon.Open(); }
                string EXISTSmonsetup = "show tables from csi_dashboard LIKE 'tbl_monsetup_data';";
                MySqlCommand cmdEXISTSmonsetup = new MySqlCommand(EXISTSmonsetup, mysqlcon);
                MySqlDataReader readerEXISTSmonsetup = cmdEXISTSmonsetup.ExecuteReader();
                if (readerEXISTSmonsetup.HasRows)
                {
                    readerEXISTSmonsetup.Close();
                    string SELECTmonsetup = "SELECT * FROM csi_dashboard.tbl_monsetup_data";
                    MySqlCommand cmdSELECTmonsetup = new MySqlCommand(SELECTmonsetup, mysqlcon);
                    //readerEXISTS.Close();

                    MySqlDataReader readerSELECTmonsetup = cmdSELECTmonsetup.ExecuteReader();
                    DataTable dtSELECTmonsetup = new DataTable();
                    dtSELECTmonsetup.Load(readerSELECTmonsetup);
                    if (dtSELECTmonsetup.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtSELECTmonsetup.Rows)
                        {
                            // dr["file_size"] = 0; //Column No : 4
                            string UPDATEoldshift = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET old_shift_name_=' '";
                            MySqlCommand cmdUPDATEoldshift = new MySqlCommand(UPDATEoldshift, mysqlcon);
                            cmdUPDATEoldshift.ExecuteNonQuery();
                        }
                    }
                    readerSELECTmonsetup.Close();
                    //    readerEXISTSeh
                }
                string EXISTSehubconf = "DROP TABLE IF EXISTS csi_dashboard.tbl_ehubconf_data;";
                MySqlCommand cmdEXISTSehubconf = new MySqlCommand(EXISTSehubconf, mysqlcon);
                cmdEXISTSehubconf.ExecuteNonQuery();
                string EXISTSshifttbl = "DROP TABLE IF EXISTS csi_dashboard.tbl_shift_data;";
                MySqlCommand cmdEXISTSshifttbl = new MySqlCommand(EXISTSshifttbl, mysqlcon);
                cmdEXISTSshifttbl.ExecuteNonQuery();
                string EXISTSfileStatustbl = "DROP TABLE IF EXISTS csi_dashboard.tbl_file_status;";
                MySqlCommand cmdEXISTSfileStatustbl = new MySqlCommand(EXISTSfileStatustbl, mysqlcon);
                cmdEXISTSfileStatustbl.ExecuteNonQuery();
                string EXISTSMonitorData = "show tables from csi_dashboard LIKE '%tbl_monitordata%';";
                MySqlCommand cmdEXISTSMonitorData = new MySqlCommand(EXISTSMonitorData, mysqlcon);
                MySqlDataReader readerEXISTSMonitorData = cmdEXISTSMonitorData.ExecuteReader();
                DataTable dtMonitorData = new DataTable();
                dtMonitorData.Load(readerEXISTSMonitorData);
                if (dtMonitorData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtMonitorData.Rows)
                    {
                        string filename = row[0].ToString();   // 0 is the column value 
                        string DROPMondata = "DROP TABLE IF EXISTS csi_dashboard." + filename + ";";
                        MySqlCommand cmdDROPMondata = new MySqlCommand(DROPMondata, mysqlcon);
                        cmdDROPMondata.ExecuteNonQuery();
                    }
                }
                readerEXISTSMonitorData.Close();
                //InitTimer();
                if (mysqlcon != null && mysqlcon.State == ConnectionState.Open)
                { mysqlcon.Close(); }
            }
            catch (Exception ex)
            {
                csi_lib.LogServiceError("Error while checking the database : " + ex.Message, Convert.ToBoolean(1));
                Utility.WriteToFile(ex.ToString());
                //MessageBox.Show(ex.ToString());
            }
            finally
            {
                mysqlcon.Close();
            }

        }
        public void Timer2_InIt()
        {
            //Timer timer2 = new Timer();
            //timer2.Elapsed += DisplayTimeEvent;
            ////timer2.Tick += new EventHandler(DisplayTimeEvent);
            //timer2.Interval = 2000; // 1 second
            //timer2.Start();
            System.Threading.Timer timer = null;

            timer = new System.Threading.Timer((g) =>
            {
                //Console.WriteLine(1); //do whatever
                DisplayTimeEvent(null, null);
                timer.Change(1000, System.Threading.Timeout.Infinite);
            }, null, 0, System.Threading.Timeout.Infinite);
        }

        public void InitTimer()
        {
            //Timer timer1 = new Timer();
            //timer1.Elapsed += DoSetup;
            ////timer1.Tick+= new evennt
            //timer1.Interval = 2000; // Miliseconds (0.5 Second)
            //timer1.Start();

            System.Threading.Timer timer = null;

            timer = new System.Threading.Timer((g) =>
            {
                //Console.WriteLine(1); //do whatever
                DoSetup(null, null);
                timer.Change(1000, System.Threading.Timeout.Infinite);
            }, null, 0, System.Threading.Timeout.Infinite);
        }
        public void DoSetup(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                ShiftData _shiftData = new ShiftData();
                MonSetup _monSetup = new MonSetup();
                EHubConf _ehubConf = new EHubConf();
                bool isMonSetupListFilesUpdated = false;
                string fileName = serverProgramData + "temp.txt";
                string file_Shift = serverProgramData + "temp_shift.txt"; //Write Shift Setup Data to this file
                Utility.WriteToFile("---DoSetup Starts----");
                _dataLayer.createDatabase();
                //MySqlCommand cmdCREATION = new MySqlCommand("CREATE TABLE if not exists csi_dashboard.tbl_ehubconf_data(num_id_ integer AUTO_INCREMENT,monitoring_id_ varchar(255), machine_name_ varchar(255), machine_tmp_filename_ varchar(255),file_size integer, PRIMARY KEY(monitoring_id_),UNIQUE KEY (num_id_));", mysqlcon);
                //cmdCREATION.ExecuteNonQuery();
                //MySqlCommand cmdCREATION1 = new MySqlCommand("CREATE TABLE if not exists csi_dashboard.tbl_monsetup_data(num_id_ integer AUTO_INCREMENT, monitoring_id_ varchar(255),  machine_name_ varchar(255),machine_tmp_filename_ varchar(255),monitoring_ integer, mon_state_ integer, part_count_ integer, part_multiplier_ integer, cycle_identifier_ varchar(255), min_ideal_ integer, max_perc_ integer ,current_status_ varchar(255),last_cycle_time integer,elapsed_time integer,machine_part_no_ varchar(255),correction_date_ DATETIME,department_name_ varchar(255),reset_counter_ integer,day_name_ varchar(255),shift_name_ varchar(255),shift_start_ integer, shift_end_ integer,old_last_cycle_time integer,old_shift_name_ varchar(255),old_part_count_ integer, PRIMARY KEY(monitoring_id_),UNIQUE KEY (num_id_));", mysqlcon); ;
                //cmdCREATION1.ExecuteNonQuery();
                //string createShiftTable = "CREATE TABLE if not exists csi_dashboard.tbl_shift_data(num_id_ integer AUTO_INCREMENT, department_name_ varchar(255),  day_name_ varchar(255),shift_name_ varchar(255),shift_start_ integer, shift_end_ integer,break1_start_ integer, break1_end_ integer,break2_start_ integer, break2_end_ integer,break3_start_ integer, break3_end_ integer, UNIQUE KEY (num_id_));";
                //MySqlCommand cmdCREATION2 = new MySqlCommand(createShiftTable, mysqlcon);
                //cmdCREATION2.ExecuteNonQuery();
                /* 
                  * Code to get all .SYS_ files name from the 
                  * _eNETDNC\_TMP\ folder and then generate tables for each 
                  * Temporary file to observe the data related to machines 
                  * whose monitoring is ON 
                  * NOTE :::::  _eNETDNC\_TMP\ folder only contains the machines which are currrently monitored 
                  */
                //string fileName = CSI_Library.CSI_Library.serverRootPath + "\\sys\\temp.txt";     /*"C:\\Users\\BDesai\\Desktop\\test.txt";*/
                ICollection<KeyValuePair<String, String>> tmpfilelist = _readFiles.getTempFileObject();

                ////// Code For Get details for shift in the company
                /// If shift setup file change
                var isShiftUpdated = _readFiles.isShiftSetupUpdated();
                Utility.WriteToFile("isShiftUpdated:: " + isShiftUpdated);
                if (isShiftUpdated)
                {
                    List<string> tempdetails1 = _readFiles.getShiftSetup();
                    File.WriteAllLines(file_Shift, tempdetails1);
                    string[] filelength = File.ReadAllLines(file_Shift);
                    int count = filelength.Length;
                    
                    _shiftData.transactShiftData(filelength);
                }

                var isEHubUpdated = _readFiles.isEHubUpdated();
                Utility.WriteToFile("isEHubUpdated:: " + isEHubUpdated);
                if (isEHubUpdated)
                {
                    var lines = _readFiles.getEHubConf();
                    //Store all lines containing machine names into string array                    
                    _ehubConf.setEhubConf(tmpfilelist);
                    _ehubConf.updateEhubConf(lines);
                }

                string[] filenames = _readFiles.getAllFilesFromDirectory();

                int fileslength = filenames.Length - 1;
                int filein = 0;
                while (filein < fileslength)
                {
                    //Utility.WriteToFile("filenames[filein]: " + filenames[filein]);
                    DataTable dttable7 = _ehubConf.getEhubDataByMachineName(filenames[filein]);
                    if (dttable7.Rows.Count > 0) // This means Machine exists in the table 
                    {
                        int filelength1 = _readFiles.getFileSize(filenames[filein]);
                        string AllTempFileLoc = serverENETPath + "_TMP";
                        //var local_file_hashkey = Utility.GetFileHash(AllTempFileLoc + "/" + filenames[filein] + ".SYS_");
                        //byte[] db_filehash = null;
                        //if (dttable7.Rows[0]["file_hash"] != DBNull.Value)
                        //    db_filehash = (byte[])dttable7.Rows[0]["file_hash"];//Encoding.ASCII.GetBytes(dttable7.Rows[0]["file_hash"].ToString());
                        int sizeoffile = Convert.ToInt32(dttable7.Rows[0]["file_size"]);
                        //bool isFileUpdated = Utility.compareFileHash(db_filehash, local_file_hashkey);

                        //if (!isFileUpdated)
                        if (sizeoffile != filelength1)
                        {

                            _ehubConf.updateAndCreateEhubTbl(filelength1, filenames[filein], null);

                            List<string> tempdetails = _readFiles.getENETFilesWithDPrint(filenames[filein]);
                            File.WriteAllLines(fileName, tempdetails);

                            List<string> tempdetails12 = _readFiles.getENETFilesData(AllTempFileLoc + "/" + filenames[filein] + ".SYS_", fileName);


                            string[] l1_final = File.ReadLines(fileName).ToArray();
                            int sizel1 = l1_final.Length;

                            int q1 = 1, r1 = 2;
                            int p1 = 0;
                            //Utility.WriteToFile(filenames[filein] + " File Array Size: " + sizel1);
                            while (p1 < sizel1)
                            {
                                // This Code Upadtes the Machine details when it has PARTNO in it
                                if (l1_final[r1].Contains("_PARTNO") || l1_final[r1].Contains("_OPERATOR"))
                                {
                                    _ehubConf.updatePartNo(l1_final, filenames[filein], r1, p1, q1);

                                }
                                else
                                {
                                    //Utility.WriteToFile("l1_final[r1]: " + l1_final[r1]);
                                    _ehubConf.updateIfNoPartNo(l1_final, filenames[filein], r1, p1, q1);
                                    //datareader3.Close();
                                }
                                p1 += 3;
                                q1 += 3;
                                r1 += 3;
                                isMonSetupListFilesUpdated = true;
                            }
                        }
                        else //if(sizeoffile==filelength1)
                        {
                            // Code Here if MonitorData files length is equal to the filesize in ehubconfig table
                            //DateTime datenow3 = System.DateTime.Now;
                            //var DifferenceInSec3 = (datenow3 - date).TotalSeconds;
                            ////int difference3 = Convert.ToInt32(DifferenceInSec3);
                            //string query24 = "UPDATE IGNORE csi_dashboard.tbl_" + filenames[filein] + " SET elapsed_time ='""' WHERE current_status_<>'' ORDER BY correction_date_ DESC LIMIT 1;";
                            //MySqlCommand cmdUPDATE1 = new MySqlCommand(query24, mysqlcon);
                            //cmdUPDATE1.ExecuteNonQuery();
                        }
                    }
                    filein++;
                    //reader12.Close();
                }
                //// Code to get Data from monsetup file
                var isMonSetupFileUpdated = _readFiles.isMonSetupUpdated();
                Utility.WriteToFile("isMonSetupUpdated:: " + isMonSetupFileUpdated + " | isMonSetupListFilesUpdated:: " + isMonSetupListFilesUpdated);
                if (isMonSetupFileUpdated || isMonSetupListFilesUpdated)
                {
                    List<string> results2 = _readFiles.getMonSetupData();
                    File.WriteAllLines(fileName, results2);
                    _monSetup.updateMonSetup(fileName);
                    string searchKeyword3 = "MU:"; // MU---> represents cycle count
                    List<string> ccresult = new List<string>();
                    string[] textLines2 = File.ReadAllLines(serverENETPath + @"_SETUP\MonSetup.sys"); /*C:\_eNETDNC\_SETUP\MonSetup.sys*/

                    foreach (string line3 in textLines2)
                    {
                        if (line3.Contains(searchKeyword3))
                        {
                            ccresult.Add(line3.Replace("MU:", ""));
                        }

                    }
                    File.WriteAllLines(fileName, ccresult);
                    string[] textLines3 = File.ReadAllLines(fileName); /*@"C:\Users\BDesai\Desktop\test.txt"*/
                    string searchKeyword4 = ",1";
                    List<string> ccresult_final = new List<string>();
                    foreach (string line4 in textLines3)
                    {
                        if (line4.Contains(searchKeyword4))
                        {
                            ccresult_final.Add(line4.Replace(",1", ""));
                        }

                    }
                    File.WriteAllLines(fileName, ccresult_final);
                    _monSetup.updteMultiplier(fileName, textLines2);

                    _monSetup.updateCounter(fileName);

                    _monSetup.updateMonsetupFiles(filenames, watch);
                }
                //InitTimer(); //Update Elapsed Time every seconds
                //Timer2_InIt();
                //if (mysqlcon != null && mysqlcon.State == ConnectionState.Open)
                //{ mysqlcon.Close(); }

            }
            catch (Exception ex)
            {
                csi_lib.LogServiceError("Error while checking the database : " + ex.Message, Convert.ToBoolean(1));


                string lineNumber = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);


                Utility.WriteToFile(ex.Message + "|" + ex.Source + "|" + ex.StackTrace + "|" + lineNumber);
                //throw;
                //InitTimer();
                //MessageBox.Show(ex.ToString());
            }
            finally
            {
                mysqlcon.Close();
            }
            Utility.WriteToFile("---DoSetup Ends----");
        }


        protected bool CheckDate(String date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void DisplayTimeEvent(object sender, EventArgs e)
        {

            try
            {
                // This function used for monitoring shift at every second, update name of the day in each iteration 
                if (mysqlcon != null && mysqlcon.State == ConnectionState.Closed)
                { mysqlcon.Open(); }
                string querySELECT14 = "SELECT department_name_,reset_counter_,last_cycle_time,shift_name_,old_shift_name_,old_part_count_,old_last_cycle_time,monitoring_id_,part_count_ FROM csi_dashboard.tbl_monsetup_data";
                MySqlCommand cmdSELECT14 = new MySqlCommand(querySELECT14, mysqlcon);
                MySqlDataReader readerSELECT14 = cmdSELECT14.ExecuteReader();
                DataTable dtSELECT14 = new DataTable();
                dtSELECT14.Load(readerSELECT14);
                if (dtSELECT14.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSELECT14.Rows)
                    {
                        string dept_name_ = dr[0].ToString();
                        int reset_counter_ = Convert.ToInt32(dr[1].ToString());
                        int last_cycle_time = Convert.ToInt32(dr[2].ToString());
                        string shift_name_mon = dr[3].ToString();
                        string old_shift_name_ = dr[4].ToString();
                        int old_part_count_ = Convert.ToInt32(dr[5].ToString());
                        int old_last_cycle_time = Convert.ToInt32(dr[6].ToString());
                        int part_count_ = Convert.ToInt32(dr[8].ToString());
                        //string machine_tmp_filename_ = (dr[7].ToString().Replace(".SYS_", " "));
                        string monitoring_id_ = dr[7].ToString();
                        string queryCHECKShift = "SELECT *,time_to_sec(now()) as TimeNow FROM csi_dashboard.tbl_shift_data WHERE department_name_='" + dept_name_ + "' AND day_name_=dayname(now()) AND shift_start_<time_to_sec(now()) AND shift_end_>time_to_sec(now());";
                        MySqlCommand cmdqueryCHECKShift = new MySqlCommand(queryCHECKShift, mysqlcon);
                        MySqlDataReader readerqueryCHECKShift = cmdqueryCHECKShift.ExecuteReader();
                        DataTable dtqueryCHECKShift = new DataTable();
                        dtqueryCHECKShift.Load(readerqueryCHECKShift);
                        if (dtqueryCHECKShift.Rows.Count > 0)
                        {
                            Utility.WriteToFile("DisplayTimeEvent: " + dtqueryCHECKShift.Rows.Count);
                            string shift_name_ = dtqueryCHECKShift.Rows[0]["shift_name_"].ToString();
                            int shift_start_ = Convert.ToInt32(dtqueryCHECKShift.Rows[0]["shift_start_"]);
                            int shift_end_ = Convert.ToInt32(dtqueryCHECKShift.Rows[0]["shift_end_"]);
                            //int timenow = dtqueryCHECKShift.Rows[0].Field<int>(12);
                            //TimeSpan ts = TimeSpan.Parse((DateTime.Now.TimeOfDay).ToString());
                            int timenow = Convert.ToInt32(dtqueryCHECKShift.Rows[0]["TimeNow"]);//Convert.ToInt32(ts.TotalSeconds);
                            string dayname = System.DateTime.Now.DayOfWeek.ToString();
                            //string machinename = machine_tmp_filename_ + ".SYS_";
                            int shift_change_start = (shift_end_ - 360);
                            int shift_change_end = (shift_end_ - 10);
                            // int shift_change_onstart_end = (shift_start_ + 180);
                                if (old_shift_name_ == shift_name_mon || old_shift_name_ == " " || shift_name_mon == " ")
                            {
                                Utility.WriteToFile("DisplayTimeEvent : old_shift_name_ == shift_name_mon ");
                                //if (timenow >= (shift_start_ + 1) && timenow <= shift_change_onstart_end)
                                //{
                                //    string UPDATEOldShifts_AfterShiftStart = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET old_shift_name_='" + shift_name_ + "',shift_name_='" + shift_name_ + "',shift_start_='" + shift_start_ + "',shift_end_='" + shift_end_ + "',day_name_='" + dayname + "',old_part_count_='" + part_count_ + "' WHERE monitoring_id_='" + monitoring_id_ + "';";
                                //    MySqlCommand cmdUPDATEOldShifts_AfterShiftStart = new MySqlCommand(UPDATEOldShifts_AfterShiftStart, mysqlcon);
                                //    cmdUPDATEOldShifts_AfterShiftStart.ExecuteNonQuery();
                                //}
                                Utility.WriteToFile("timenow: " + timenow + " | shift_change_start: " + shift_change_start + " | shift_change_end: " + shift_change_end);
                                if (timenow >= shift_change_start && timenow <= shift_change_end)
                                {
                                    // Update Old_sHift Name Here before Shift_End
                                    Utility.WriteToFile("10 to 360 Seconds Before Shift End is HERE ::::");
                                    string UPDATEOldShifts_BeforeShiftEnd = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET old_shift_name_='" + shift_name_ + "',shift_name_='" + shift_name_ + "',shift_start_='" + shift_start_ + "',shift_end_='" + shift_end_ + "',day_name_='" + dayname + "',old_part_count_='" + part_count_ + "' WHERE monitoring_id_='" + monitoring_id_ + "';";
                                    MySqlCommand cmdUPDATEOldShifts_BeforeShiftEnd = new MySqlCommand(UPDATEOldShifts_BeforeShiftEnd, mysqlcon);
                                    cmdUPDATEOldShifts_BeforeShiftEnd.ExecuteNonQuery();
                                }
                                else
                                {
                                    //This executes normally everytime 
                                    string UPDATEOnlyShifts = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET shift_name_='" + shift_name_ + "',shift_start_='" + shift_start_ + "',shift_end_='" + shift_end_ + "',day_name_='" + dayname + "',old_part_count_='" + part_count_ + "' WHERE monitoring_id_='" + monitoring_id_ + "';";
                                    MySqlCommand cmdUPDATEOnlyShifts = new MySqlCommand(UPDATEOnlyShifts, mysqlcon);
                                    cmdUPDATEOnlyShifts.ExecuteNonQuery();
                                }
                            }
                            else if (old_shift_name_ != shift_name_mon)
                            {
                                Utility.WriteToFile("DisplayTimeEvent : old_shift_name_!= shift_name_mon ");
                                //Reset Elapsed Time and Part Count, Drop Tables(Calling LoadFiles_Load) values when Shift changes (Write code here for Every ROW in Monsetup)
                                if (reset_counter_ == 3)
                                {
                                    string UPDATEPartCount = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET old_shift_name_='" + shift_name_ + "',part_count_='0',elapsed_time='0',last_cycle_time='" + old_last_cycle_time + "',day_name_='" + dayname + "' WHERE monitoring_id_='" + monitoring_id_ + "';";
                                    MySqlCommand cmdUPDATEPartCount = new MySqlCommand(UPDATEPartCount, mysqlcon);
                                    cmdUPDATEPartCount.ExecuteNonQuery();
                                }
                                else if (reset_counter_ == 1)
                                {
                                    string NoUPDATEPartCount = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET old_shift_name_='" + shift_name_ + "',day_name_='" + dayname + "',part_count_='" + old_part_count_ + "',elapsed_time='0',last_cycle_time='" + old_last_cycle_time + "' WHERE monitoring_id_='" + monitoring_id_ + "';";
                                    MySqlCommand cmdNoUPDATEPartCount = new MySqlCommand(NoUPDATEPartCount, mysqlcon);
                                    cmdNoUPDATEPartCount.ExecuteNonQuery();
                                }
                                // This will drop all tables expext Monsetup_data and Create new tables to get new Data from all the files
                                string EXISTSmonsetup = "show tables from csi_dashboard LIKE 'tbl_monsetup_data';";
                                MySqlCommand cmdEXISTSmonsetup = new MySqlCommand(EXISTSmonsetup, mysqlcon);
                                MySqlDataReader readerEXISTSmonsetup = cmdEXISTSmonsetup.ExecuteReader();
                                if (readerEXISTSmonsetup.HasRows)
                                {
                                    readerEXISTSmonsetup.Close();
                                    string SELECTmonsetup = "SELECT * FROM csi_dashboard.tbl_monsetup_data";
                                    MySqlCommand cmdSELECTmonsetup = new MySqlCommand(SELECTmonsetup, mysqlcon);
                                    //readerEXISTS.Close();

                                    MySqlDataReader readerSELECTmonsetup = cmdSELECTmonsetup.ExecuteReader();
                                    DataTable dtSELECTmonsetup = new DataTable();
                                    dtSELECTmonsetup.Load(readerSELECTmonsetup);
                                    if (dtSELECTmonsetup.Rows.Count > 0)
                                    {
                                        foreach (DataRow dr1 in dtSELECTmonsetup.Rows)
                                        {
                                            // dr["file_size"] = 0; //Column No : 4
                                            string UPDATEoldshift = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET old_shift_name_=' '";
                                            MySqlCommand cmdUPDATEoldshift = new MySqlCommand(UPDATEoldshift, mysqlcon);
                                            cmdUPDATEoldshift.ExecuteNonQuery();
                                        }
                                    }
                                    readerSELECTmonsetup.Close();
                                    //    readerEXISTSeh
                                }
                                string EXISTSehubconf = "DROP TABLE IF EXISTS csi_dashboard.tbl_ehubconf_data;";
                                MySqlCommand cmdEXISTSehubconf = new MySqlCommand(EXISTSehubconf, mysqlcon);
                                cmdEXISTSehubconf.ExecuteNonQuery();
                                //string EXISTSshifttbl = "DROP TABLE IF EXISTS csi_dashboard.tbl_shift_data;";
                                //MySqlCommand cmdEXISTSshifttbl = new MySqlCommand(EXISTSshifttbl, mysqlcon);
                                //cmdEXISTSshifttbl.ExecuteNonQuery();
                                string EXISTSfileStatustbl = "DROP TABLE IF EXISTS csi_dashboard.tbl_file_status;";
                                MySqlCommand cmdEXISTSfileStatustbl = new MySqlCommand(EXISTSfileStatustbl, mysqlcon);
                                cmdEXISTSfileStatustbl.ExecuteNonQuery();
                                string EXISTSMonitorData = "show tables from csi_dashboard LIKE '%tbl_monitordata%';";
                                MySqlCommand cmdEXISTSMonitorData = new MySqlCommand(EXISTSMonitorData, mysqlcon);
                                MySqlDataReader readerEXISTSMonitorData = cmdEXISTSMonitorData.ExecuteReader();
                                DataTable dtMonitorData = new DataTable();
                                dtMonitorData.Load(readerEXISTSMonitorData);
                                if (dtMonitorData.Rows.Count > 0)
                                {
                                    foreach (DataRow row in dtMonitorData.Rows)
                                    {
                                        string filename = row[0].ToString();   // 0 is the column value 
                                        string DROPMondata = "DROP TABLE IF EXISTS csi_dashboard." + filename + ";";
                                        MySqlCommand cmdDROPMondata = new MySqlCommand(DROPMondata, mysqlcon);
                                        cmdDROPMondata.ExecuteNonQuery();
                                    }
                                }
                                readerEXISTSMonitorData.Close();

                            }
                        }
                        readerqueryCHECKShift.Close();
                    }
                    readerSELECT14.Close();
                    if (mysqlcon != null && mysqlcon.State == ConnectionState.Open)
                    { mysqlcon.Close(); }
                }
            }
            catch (Exception ex)
            {
                csi_lib.LogServiceError("Error while checking the database : " + ex.Message, Convert.ToBoolean(1));
                Utility.WriteToFile(ex.ToString());
                //InitTimer();
                //MessageBox.Show(ex.ToString());
            }
            finally
            {
                mysqlcon.Close();
            }


        }
    }
}
