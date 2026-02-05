using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CSI_Library;
using System.Configuration;
using System.Text;
using CSIFlex_ServiceLibrary;
using System.Globalization;
/*Use ExecuteNonQueery Method for INSERT,UPDATE and Delete queries
* But Use MySqlDataReader for SELECT queries to excute the select query first and store it's result into 
* Datatable 
*/

namespace Demo_CSIFlex
{
    public partial class LoadFiles : Form
    {
        public static MySqlConnection mysqlcon = new MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString);
        public static CSI_Library.CSI_Library csi_lib = new CSI_Library.CSI_Library();
        private string serverName;
        private string serverProgramData;
        private string serverENETPath;
       
        public LoadFiles()
        {
            InitializeComponent();
            serverName = ConfigurationManager.AppSettings["SERVER_NAME"];
            serverProgramData = serverName + ConfigurationManager.AppSettings["SERVER_PROGRAM_DATA"];
            serverENETPath = serverName + ConfigurationManager.AppSettings["SERVER_ENET_PATH"];
        }

        private void LoadFiles_Load(object sender, EventArgs e)
        { }

        //private void LoadFiles_Load(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (mysqlcon != null && mysqlcon.State == ConnectionState.Closed)
        //        {
        //            mysqlcon.Open();
        //        }
        //        string EXISTSmonsetup = "show tables from csi_dashboard LIKE 'tbl_monsetup_data';";
        //        MySqlCommand cmdEXISTSmonsetup = new MySqlCommand(EXISTSmonsetup, mysqlcon);
        //        MySqlDataReader readerEXISTSmonsetup = cmdEXISTSmonsetup.ExecuteReader();
        //        if (readerEXISTSmonsetup.HasRows)
        //        {
        //            string SELECTmonsetup = "SELECT * FROM csi_dashboard.tbl_monsetup_data";
        //            MySqlCommand cmdSELECTmonsetup = new MySqlCommand(SELECTmonsetup, mysqlcon);
        //            //readerEXISTS.Close();
        //            readerEXISTSmonsetup.Close();
        //            MySqlDataReader readerSELECTmonsetup = cmdSELECTmonsetup.ExecuteReader();
        //            DataTable dtSELECTmonsetup = new DataTable();
        //            dtSELECTmonsetup.Load(readerSELECTmonsetup);
        //            if (dtSELECTmonsetup.Rows.Count > 0)
        //            {
        //                foreach (DataRow dr in dtSELECTmonsetup.Rows)
        //                {
        //                    // dr["file_size"] = 0; //Column No : 4
        //                    string UPDATEoldshift = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET old_shift_name_=' '";
        //                    MySqlCommand cmdUPDATEoldshift = new MySqlCommand(UPDATEoldshift, mysqlcon);
        //                    cmdUPDATEoldshift.ExecuteNonQuery();
        //                }
        //            }
        //            readerSELECTmonsetup.Close();
        //            //    readerEXISTSeh
        //        }
        //        readerEXISTSmonsetup.Close();
        //        string EXISTSehubconf = "DROP TABLE IF EXISTS csi_dashboard.tbl_ehubconf_data;";
        //        MySqlCommand cmdEXISTSehubconf = new MySqlCommand(EXISTSehubconf, mysqlcon);
        //        cmdEXISTSehubconf.ExecuteNonQuery();
        //        string EXISTSshifttbl = "DROP TABLE IF EXISTS csi_dashboard.tbl_shift_data;";
        //        MySqlCommand cmdEXISTSshifttbl = new MySqlCommand(EXISTSshifttbl, mysqlcon);
        //        cmdEXISTSshifttbl.ExecuteNonQuery();
        //        //readerEXISTSehubconf.Close();
        //        string EXISTSMonitorData = "show tables from csi_dashboard LIKE '%tbl_monitordata%';";
        //        MySqlCommand cmdEXISTSMonitorData = new MySqlCommand(EXISTSMonitorData, mysqlcon);
        //        MySqlDataReader readerEXISTSMonitorData = cmdEXISTSMonitorData.ExecuteReader();
        //        DataTable dtMonitorData = new DataTable();
        //        dtMonitorData.Load(readerEXISTSMonitorData);
        //        if (dtMonitorData.Rows.Count > 0)
        //        {
        //            foreach (DataRow row in dtMonitorData.Rows)
        //            {
        //                string filename = row[0].ToString();   // 0 is the column value 
        //                string DROPMondata = "DROP TABLE IF EXISTS csi_dashboard." + filename + ";";
        //                MySqlCommand cmdDROPMondata = new MySqlCommand(DROPMondata, mysqlcon);
        //                cmdDROPMondata.ExecuteNonQuery();
        //            }
        //        }
        //        readerEXISTSMonitorData.Close();
        //        DoSetup(null, null);
        //        // InitTimer();
        //        if (mysqlcon != null && mysqlcon.State == ConnectionState.Open)
        //        { mysqlcon.Close(); }
        //    }

        //    catch (Exception ex)
        //    {
        //        csi_lib.LogServiceError("Error while checking the database : " + ex.Message, Convert.ToBoolean(1));
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        mysqlcon.Close();
        //    }

        //}
        //public void Timer2_InIt()
        //{
        //    Timer timer2 = new Timer();
        //    timer2.Tick += new EventHandler(DisplayTimeEvent);
        //    timer2.Interval = 1000; // 1 second
        //    timer2.Start();
        //}
        //public void InitTimer()
        //{
        //    Timer timer1 = new Timer();
        //    timer1.Tick += new EventHandler(DoSetup);
        //    //timer1.Tick+= new evennt
        //    timer1.Interval = 1000; // Miliseconds (0.5 Second)
        //    timer1.Start();
        //}
        //public void DoSetup(object sender, EventArgs e)
        //{
        //    var watch = System.Diagnostics.Stopwatch.StartNew();
        //    try
        //    {
        //        Utility.WriteToFile("---DoSetup Starts----");
        //        if (mysqlcon != null && mysqlcon.State == ConnectionState.Closed)
        //        { mysqlcon.Open(); }

        //        StringBuilder command = new StringBuilder("CREATE DATABASE IF NOT EXISTS csi_dashboard;");
        //        command.Append("CREATE TABLE if not exists csi_dashboard.tbl_ehubconf_data(num_id_ integer AUTO_INCREMENT,monitoring_id_ varchar(255), machine_name_ varchar(255), machine_tmp_filename_ varchar(255),file_size integer, PRIMARY KEY(monitoring_id_),UNIQUE KEY (num_id_));");
        //        command.Append("CREATE TABLE if not exists csi_dashboard.tbl_monsetup_data(num_id_ integer AUTO_INCREMENT, monitoring_id_ varchar(255),  machine_name_ varchar(255),machine_tmp_filename_ varchar(255),monitoring_ integer, mon_state_ integer, part_count_ integer, part_multiplier_ integer, cycle_identifier_ varchar(255), min_ideal_ integer, max_perc_ integer ,current_status_ varchar(255),last_cycle_time integer,elapsed_time integer,machine_part_no_ varchar(255),correction_date_ DATETIME,department_name_ varchar(255),reset_counter_ integer,day_name_ varchar(255),shift_name_ varchar(255),shift_start_ integer, shift_end_ integer,old_last_cycle_time integer,old_shift_name_ varchar(255),old_part_count_ integer, PRIMARY KEY(monitoring_id_),UNIQUE KEY (num_id_));");
        //        command.Append("CREATE TABLE if not exists csi_dashboard.tbl_shift_data(num_id_ integer AUTO_INCREMENT, department_name_ varchar(255),  day_name_ varchar(255),shift_name_ varchar(255),shift_start_ integer, shift_end_ integer,break1_start_ integer, break1_end_ integer,break2_start_ integer, break2_end_ integer,break3_start_ integer, break3_end_ integer, UNIQUE KEY (num_id_));");
        //        MySqlCommand mysqlcmd = new MySqlCommand(command.ToString(), mysqlcon);
        //        //mysqlcmd.ExecuteNonQuery();
        //        using (MySqlDataReader dr = mysqlcmd.ExecuteReader())
        //        {
        //            dr.Close();
        //        }
        //        //MySqlCommand cmdCREATION = new MySqlCommand("CREATE TABLE if not exists csi_dashboard.tbl_ehubconf_data(num_id_ integer AUTO_INCREMENT,monitoring_id_ varchar(255), machine_name_ varchar(255), machine_tmp_filename_ varchar(255),file_size integer, PRIMARY KEY(monitoring_id_),UNIQUE KEY (num_id_));", mysqlcon);
        //        //cmdCREATION.ExecuteNonQuery();
        //        //MySqlCommand cmdCREATION1 = new MySqlCommand("CREATE TABLE if not exists csi_dashboard.tbl_monsetup_data(num_id_ integer AUTO_INCREMENT, monitoring_id_ varchar(255),  machine_name_ varchar(255),machine_tmp_filename_ varchar(255),monitoring_ integer, mon_state_ integer, part_count_ integer, part_multiplier_ integer, cycle_identifier_ varchar(255), min_ideal_ integer, max_perc_ integer ,current_status_ varchar(255),last_cycle_time integer,elapsed_time integer,machine_part_no_ varchar(255),correction_date_ DATETIME,department_name_ varchar(255),reset_counter_ integer,day_name_ varchar(255),shift_name_ varchar(255),shift_start_ integer, shift_end_ integer,old_last_cycle_time integer,old_shift_name_ varchar(255),old_part_count_ integer, PRIMARY KEY(monitoring_id_),UNIQUE KEY (num_id_));", mysqlcon); ;
        //        //cmdCREATION1.ExecuteNonQuery();
        //        //string createShiftTable = "CREATE TABLE if not exists csi_dashboard.tbl_shift_data(num_id_ integer AUTO_INCREMENT, department_name_ varchar(255),  day_name_ varchar(255),shift_name_ varchar(255),shift_start_ integer, shift_end_ integer,break1_start_ integer, break1_end_ integer,break2_start_ integer, break2_end_ integer,break3_start_ integer, break3_end_ integer, UNIQUE KEY (num_id_));";
        //        //MySqlCommand cmdCREATION2 = new MySqlCommand(createShiftTable, mysqlcon);
        //        //cmdCREATION2.ExecuteNonQuery();
        //        /* 
        //          * Code to get all .SYS_ files name from the 
        //          * _eNETDNC\_TMP\ folder and then generate tables for each 
        //          * Temporary file to observe the data related to machines 
        //          * whose monitoring is ON 
        //          * NOTE :::::  _eNETDNC\_TMP\ folder only contains the machines which are currrently monitored 
        //          */
        //        //string fileName = CSI_Library.CSI_Library.serverRootPath + "\\sys\\temp.txt";     /*"C:\\Users\\BDesai\\Desktop\\test.txt";*/
        //        string fileName = serverProgramData + "temp.txt";
        //        ICollection<KeyValuePair<String, String>> tmpfilelist = new Dictionary<String, String>();
        //        for (int i = 1; i <= 16; i++)
        //        {
        //            for (int j1 = 1; j1 <= 8; j1++)
        //            {
        //                tmpfilelist.Add(new KeyValuePair<string, string>((i + "," + j1), "MonitorData" + (i - 1) + "" + (j1 - 1) + ".SYS_"));
        //            }
        //        }
        //        ////// Code For Get details for shift in the company
        //        string file_Shift = serverProgramData + "temp_shift.txt"; //Write Shift Setup Data to this file
        //        string file_Main = serverENETPath + @"_SETUP\ShiftSetup2.sys";
        //        string[] textLines5 = File.ReadAllLines(file_Main);
        //        List<string> tempdetails1 = new List<string>();
        //        foreach (string line7 in textLines5)
        //        {
        //            string[] parts = line7.Split(',');
        //            foreach (string a1 in parts)
        //            {
        //                if (!string.IsNullOrWhiteSpace(a1))
        //                { tempdetails1.Add(a1); }

        //            }
        //        }
        //        File.WriteAllLines(file_Shift, tempdetails1);
        //        string[] filelength = File.ReadAllLines(file_Shift);
        //        int count = filelength.Length;
        //        Utility.WriteToFile(Convert.ToString(count));
        //        //MessageBox.Show(Convert.ToString(count));  170
        //        string[] weekdays = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        //        int loc = 2, limit = 44, filenameinit = 1;
        //        while (loc < limit && loc < count)
        //        {
        //            for (int days = 0; days < 7; days++)
        //            {
        //                for (int ct = 1; ct < 4; ct++)
        //                {
        //                    string sh_start = filelength[loc];
        //                    string sh_end = filelength[loc + 1];
        //                    string br1_start = filelength[loc + 42];
        //                    string br1_end = filelength[loc + 43];
        //                    string br2_start = filelength[loc + 84];
        //                    string br2_end = filelength[loc + 85];
        //                    string br3_start = filelength[loc + 126];
        //                    string br3_end = filelength[loc + 127];
        //                    loc = loc + 2;
        //                    string querycheck = "SELECT * FROM csi_dashboard.tbl_shift_data WHERE department_name_='" + filelength[filenameinit] + "' AND day_name_='" + weekdays[days] + "' AND shift_name_='Shift " + ct.ToString() + "'";
        //                    MySqlCommand cmdSELECTquerycheck = new MySqlCommand(querycheck, mysqlcon);
        //                    MySqlDataReader readerquerycheck = cmdSELECTquerycheck.ExecuteReader();
        //                    DataTable dtreaderquerycheck = new DataTable();
        //                    dtreaderquerycheck.Load(readerquerycheck);
        //                    if (dtreaderquerycheck.Rows.Count == 0)
        //                    {  // This code for first time INSERT
        //                        string query1_shifttbl = "INSERT INTO csi_dashboard.tbl_shift_data (department_name_,day_name_,shift_name_,shift_start_,shift_end_,break1_start_,break1_end_,break2_start_,break2_end_,break3_start_,break3_end_) VALUES ('" + filelength[filenameinit] + "','" + weekdays[days] + "','Shift " + ct.ToString() + "','" + Convert.ToInt32(sh_start) + "','" + Convert.ToInt32(sh_end) + "','" + Convert.ToInt32(br1_start) + "','" + Convert.ToInt32(br1_end) + "','" + Convert.ToInt32(br2_start) + "','" + Convert.ToInt32(br2_end) + "','" + Convert.ToInt32(br3_start) + "','" + Convert.ToInt32(br3_end) + "') ";
        //                        //MessageBox.Show(filelength[filenameinit] + "\n" + weekdays[days] + "\n" + sh_start + "\n" + sh_end + "\n" + br1_start + "\n" + br1_end + "\n" + br2_start + "\n" + br2_end + "\n" + br3_start + "\n" + br3_end);
        //                        MySqlCommand cmdINSERT_query1_shifttbl = new MySqlCommand(query1_shifttbl, mysqlcon);
        //                        cmdINSERT_query1_shifttbl.ExecuteNonQuery();
        //                    }
        //                    else
        //                    {
        //                        string UPDATE_shifttbl = "UPDATE IGNORE csi_dashboard.tbl_shift_data SET shift_start_='" + Convert.ToInt32(sh_start) + "',shift_end_='" + Convert.ToInt32(sh_end) + "',break1_start_='" + Convert.ToInt32(br1_start) + "',break1_end_='" + Convert.ToInt32(br1_end) + "',break2_start_='" + Convert.ToInt32(br2_start) + "',break2_end_='" + Convert.ToInt32(br2_end) + "',break3_start_='" + Convert.ToInt32(br3_start) + "',break3_end_='" + Convert.ToInt32(br3_end) + "' WHERE department_name_='" + filelength[filenameinit] + "'AND day_name_='" + weekdays[days] + "'AND shift_name_='Shift " + ct.ToString() + "'";
        //                        MySqlCommand cmdUPDATE_shifttbl = new MySqlCommand(UPDATE_shifttbl, mysqlcon);
        //                        cmdUPDATE_shifttbl.ExecuteNonQuery();
        //                    }
        //                    readerquerycheck.Close();
        //                }
        //            }
        //            loc = loc + 127;
        //            limit = loc + 42;
        //            filenameinit = filenameinit + 169;
        //        }
        //        string searchKeyword = "NM:";//NM ----> represent Name of the machine
        //                                     // string fileName = "C:\\Users\\BDesai\\Desktop\\test.txt";
        //        string eHubFileLocation = serverENETPath + @"_SETUP\eHUBConf.sys";
        //        string[] textLines = File.ReadAllLines(eHubFileLocation);  /*@"C:\_eNETDNC\_SETUP\eHUBConf.sys"*/
        //        List<string> results = new List<string>();

        //        foreach (string line in textLines)
        //        {
        //            if (line.Contains(searchKeyword))
        //            {
        //                results.Add(line.Replace("NM:", ""));
        //            }
        //        }

        //        File.WriteAllLines(fileName, results);
        //        string[] lines = File.ReadLines(fileName).ToArray(); //Store all lines containing machine names into string array
        //        string today = System.DateTime.Now.DayOfWeek.ToString();
        //        foreach (KeyValuePair<string, string> element in tmpfilelist)
        //        {
        //            MySqlCommand cmdINSERT = new MySqlCommand("INSERT IGNORE INTO csi_dashboard.tbl_ehubconf_data (monitoring_id_,machine_name_,machine_tmp_filename_,file_size) VALUES ('" + element.Key + "','','" + element.Value + "','0')", mysqlcon);
        //            cmdINSERT.ExecuteNonQuery();
        //            MySqlCommand cmdINSERT2 = new MySqlCommand("INSERT IGNORE INTO csi_dashboard.tbl_monsetup_data(monitoring_id_,machine_name_,machine_tmp_filename_,monitoring_,mon_state_,part_count_,part_multiplier_,cycle_identifier_, min_ideal_, max_perc_,current_status_,last_cycle_time,elapsed_time,machine_part_no_, correction_date_,department_name_,reset_counter_,day_name_,shift_name_ ,shift_start_, shift_end_,old_last_cycle_time,old_shift_name_,old_part_count_) VALUES ('" + element.Key + "','','" + element.Value + "','0','0','0','0','MIN/MAX','','','','0','0','','','','','" + today + "','','','','0','','0')", mysqlcon);
        //            cmdINSERT2.ExecuteNonQuery();

        //        }
        //        int k = 1, l = 1, p = 0;
        //        int m = lines.Length;
        //        while (p < m)
        //        {
        //            if (k <= 16 && l <= 8)
        //            {
        //                string query = "UPDATE IGNORE csi_dashboard.tbl_ehubconf_data SET machine_name_ = '" + lines[p].ToString() + "' WHERE monitoring_id_ ='" + Convert.ToString(k) + "," + Convert.ToString(l) + "'";
        //                MySqlCommand cmdUPDATE = new MySqlCommand(query, mysqlcon);
        //                cmdUPDATE.ExecuteNonQuery();
        //                string query2 = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET machine_name_ = '" + lines[p].ToString() + "',day_name_='" + today + "' WHERE monitoring_id_ ='" + Convert.ToString(k) + "," + Convert.ToString(l) + "'";
        //                MySqlCommand cmdUPDATE2 = new MySqlCommand(query2, mysqlcon);
        //                cmdUPDATE2.ExecuteNonQuery();
        //                k = Convert.ToInt32(k);
        //                l = Convert.ToInt32(l);
        //                l++;
        //                p++;
        //                if (l > 8)
        //                {
        //                    l = 1;
        //                    k++;
        //                }
        //            }
        //        }
        //        string AllTempFileLoc = serverENETPath + "_TMP";
        //        DirectoryInfo d = new DirectoryInfo(AllTempFileLoc);     /*C:\_eNETDNC\_TMP*/
        //        //string fileName1 = CSI_Library.CSI_Library.serverRootPath + "\\sys\\templist.txt";        /*"C:\\Users\\BDesai\\Desktop\\templist.txt";*/
        //        string fileName1 = serverProgramData + "templist.txt";
        //        FileInfo[] Files = d.GetFiles("*.SYS_"); //Getting SYS_ files
        //        string str = "";
        //        int count1 = 0;
        //        List<string> filename = new List<string>();
        //        foreach (FileInfo file in Files)
        //        {
        //            str = str + Path.GetFileNameWithoutExtension(file.Name) + Environment.NewLine;
        //            count1++;
        //        }
        //        filename.Add(str);
        //        File.WriteAllLines(fileName1, filename);
        //        string[] filenames = File.ReadLines(fileName1).ToArray(); // Array to store all file names in _eNETDNC\_TMP\ folder
        //        int fileslength = filenames.Length - 1;
        //        int filein = 0;
        //        while (filein < fileslength)
        //        {
        //            var filesize = new System.IO.FileInfo(serverENETPath + @"_TMP\" + filenames[filein] + ".SYS_").Length; /*C:\\_eNETDNC\\_TMP\\" + filenames[filein] + ".SYS_"*/
        //            int filelength1 = Convert.ToInt32(filesize);
        //            string query25 = "SELECT * FROM csi_dashboard.tbl_ehubconf_data WHERE machine_tmp_filename_='" + filenames[filein] + ".SYS_';";
        //            MySqlCommand cmdSELECT10 = new MySqlCommand(query25, mysqlcon);
        //            MySqlDataReader reader12 = cmdSELECT10.ExecuteReader();
        //            DataTable dttable7 = new DataTable();
        //            dttable7.Load(reader12);
        //            if (dttable7.Rows.Count > 0) // This means Machine exists in the table 
        //            {
        //                int sizeoffile = dttable7.Rows[0].Field<int>(4);
        //                if (sizeoffile != filelength1)
        //                {
        //                    string query26 = "UPDATE csi_dashboard.tbl_ehubconf_data SET file_size = '" + filelength1 + "' WHERE machine_tmp_filename_ ='" + filenames[filein] + ".SYS_'";
        //                    MySqlCommand cmdUPDATEehub = new MySqlCommand(query26, mysqlcon);
        //                    cmdUPDATEehub.ExecuteNonQuery();
        //                    string query8 = "CREATE TABLE if not exists csi_dashboard.tbl_" + filenames[filein] + "(machine_tmp_filename_ varchar(255), current_status_ varchar(255) , last_cycle_time integer, elapsed_time integer, correction_date_ DATETIME ,machine_part_no_ varchar(255) );";
        //                    MySqlCommand cmdCREATION3 = new MySqlCommand(query8, mysqlcon);
        //                    cmdCREATION3.ExecuteNonQuery();

        //                    string[] textLines4 = File.ReadAllLines(serverENETPath + @"_TMP\" + filenames[filein] + ".SYS_");/*C:\_eNETDNC\_TMP\" + filenames[filein] + ".SYS_"*/
        //                    List<string> tempdetails = new List<string>();
        //                    foreach (string line6 in textLines4)
        //                    {
        //                        if (!(line6.Contains("_DPRINT_"))) // ignore _DPRINT_  lines in all .SYS_ files 
        //                        {
        //                            tempdetails.Add(line6);
        //                        }
        //                    }
        //                    File.WriteAllLines(fileName, tempdetails);
        //                    string[] textLines55 = File.ReadAllLines(fileName);
        //                    List<string> tempdetails12 = new List<string>();
        //                    foreach (string line7 in textLines55)
        //                    {
        //                        string[] parts = line7.Split(',');
        //                        foreach (string a1 in parts)
        //                        {
        //                            if (!string.IsNullOrWhiteSpace(a1))
        //                            { tempdetails12.Add(a1); }

        //                        }
        //                    }
        //                    File.WriteAllLines(fileName, tempdetails12);
        //                    string[] tempdetails1_final = File.ReadLines(fileName).ToArray();
        //                    int length = tempdetails1_final.Length;
        //                    int p1 = 0;
        //                    List<string> l1 = new List<string>();
        //                    while (p1 < length)
        //                    {
        //                        if (tempdetails1_final[p1].Contains("_PARTNO"))
        //                        {
        //                            l1.Add(tempdetails1_final[p1]);
        //                            p1 = p1 + 4;
        //                        }
        //                        else // this contains all status and _OPERATOR information
        //                        {
        //                            l1.Add(tempdetails1_final[p1]);
        //                            p1++;
        //                        }

        //                    }
        //                    File.WriteAllLines(fileName, l1);
        //                    string[] l1_final = File.ReadLines(fileName).ToArray();
        //                    int sizel1 = l1_final.Length;

        //                    int q1 = 1, r1 = 2;
        //                    p1 = 0;

        //                    while (p1 < sizel1)
        //                    {
        //                        // This Code Upadtes the Machine details when it has PARTNO in it
        //                        if (l1_final[r1].Contains("_PARTNO") || l1_final[r1].Contains("_OPERATOR"))
        //                        {
        //                            string updatepart;
        //                            if (l1_final[r1].Contains("_OPERATOR"))
        //                            {
        //                                string operatorID = l1_final[r1].Replace("_OPERATOR", "OID");
        //                                updatepart = operatorID;
        //                            }
        //                            else
        //                            {
        //                                string newpartno = l1_final[r1].Split(':')[1];
        //                                updatepart = newpartno;
        //                            }
        //                            string dateformat = l1_final[p1];
        //                            string timeformat = l1_final[q1];
        //                            string datetime = dateformat + " " + timeformat;

        //                            //DateTime date = DateTime.Now;
        //                            //try
        //                            //{
        //                            //    Utility.WriteToFile(datetime);
        //                            //    date = DateTime.ParseExact(datetime, "dd-MM-yyyy hh:mm:ss:tt", CultureInfo.InvariantCulture);//DateTime.Parse(datetime);                                                                                                                                             
        //                            //}
        //                            //catch (Exception ex)
        //                            //{
        //                            //    Utility.WriteToFile(ex.ToString());

        //                            //}
        //                            //                                    Dim ci As New System.Globalization.CultureInfo("en-US")

        //                            //tDate = Date.Parse("04/14/08", ci) 'this date format is Month/Day/Year
        //                            DateTime date = DateTime.ParseExact(datetime, "MM/dd/yy HH:mm:ss", CultureInfo.InvariantCulture);// Convert.ToDateTime("11-27-17 15:30:00"); //datetime
        //                            string formatForMySql = date.ToString("yyyy-MM-dd HH:mm:ss");

        //                            string query13 = "SELECT * FROM csi_dashboard.tbl_" + filenames[filein] + ";";
        //                            MySqlCommand cmdCHECKEMPTY = new MySqlCommand(query13, mysqlcon);
        //                            MySqlDataReader datareader1 = cmdCHECKEMPTY.ExecuteReader();
        //                            DataTable dttable = new DataTable();
        //                            dttable.Load(datareader1);
        //                            if (dttable.Rows.Count == 0) // When the table has zero records (the first time data will be added here)
        //                            {
        //                                //Code for first time INSERT Values

        //                                string query40 = "INSERT INTO csi_dashboard.tbl_" + filenames[filein] + "(machine_tmp_filename_ , current_status_ , last_cycle_time , elapsed_time , correction_date_ ,machine_part_no_) VALUES ('" + filenames[filein] + "','','0','0','" + formatForMySql + "','" + updatepart + "');";
        //                                MySqlCommand cmdINSERTquery40 = new MySqlCommand(query40, mysqlcon);
        //                                cmdINSERTquery40.ExecuteNonQuery();


        //                            }
        //                            else
        //                            {  //When table has records already saved 
        //                                string query11 = "SELECT * FROM csi_dashboard.tbl_" + filenames[filein] + " WHERE  machine_part_no_='" + updatepart + "' AND correction_date_ ='" + formatForMySql + "' AND current_status_=''";
        //                                MySqlCommand cmdSELECT = new MySqlCommand(query11, mysqlcon);
        //                                MySqlDataReader datareader2 = cmdSELECT.ExecuteReader();
        //                                DataTable dttable1 = new DataTable();
        //                                dttable1.Load(datareader2);
        //                                if (dttable1.Rows.Count == 0)
        //                                {
        //                                    string query14 = "INSERT IGNORE INTO csi_dashboard.tbl_" + filenames[filein] + "(machine_tmp_filename_ , current_status_ , last_cycle_time , elapsed_time , correction_date_ ,machine_part_no_) VALUES ('" + filenames[filein] + "','','0','0','" + formatForMySql + "','" + updatepart + "');";
        //                                    MySqlCommand cmdINSERT1 = new MySqlCommand(query14, mysqlcon);
        //                                    cmdINSERT1.ExecuteNonQuery();
        //                                    string UPDATEMonsetup = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET part_count_='0',old_part_count_='0' WHERE machine_tmp_filename_='" + filenames[filein] + ".SYS_'";
        //                                    MySqlCommand cmdUPDATEMonsetup = new MySqlCommand(UPDATEMonsetup, mysqlcon);
        //                                    cmdUPDATEMonsetup.ExecuteNonQuery();
        //                                }
        //                                else if (dttable1.Rows.Count > 0)
        //                                {

        //                                }
        //                                datareader2.Close();
        //                            }
        //                            p1 += 3;
        //                            q1 += 3;
        //                            r1 += 3;
        //                            datareader1.Close();
        //                        }
        //                        else
        //                        {   //Normal entreis that has no part number means This notes all cysles of the machines (NO CHANGES TO THE PART NUMBER) 
        //                            string dateformat = l1_final[p1];
        //                            string timeformat = l1_final[q1];
        //                            string datetime = dateformat + " " + timeformat;
        //                            Utility.WriteToFile(datetime);
        //                            DateTime date = DateTime.ParseExact(datetime, "MM/dd/yy HH:mm:ss", CultureInfo.InvariantCulture);//DateTime.Parse(datetime);
        //                            string formatForMySql = date.ToString("yyyy-MM-dd HH:mm:ss");
        //                            //DateTime date = Convert.ToDateTime(datetime);
        //                            string query15 = "SELECT * FROM csi_dashboard.tbl_" + filenames[filein] + ";";
        //                            MySqlCommand cmdCHECKEMPTY2 = new MySqlCommand(query15, mysqlcon);
        //                            MySqlDataReader datareader3 = cmdCHECKEMPTY2.ExecuteReader();
        //                            DataTable dttable2 = new DataTable();
        //                            dttable2.Load(datareader3);
        //                            if (dttable2.Rows.Count == 0)
        //                            {
        //                                //Code for first time insert for Machine Status
        //                                string getlastcyclefromMonsetup = "SELECT * FROM csi_dashboard.tbl_monsetup_data WHERE machine_tmp_filename_='" + filenames[filein] + ".SYS_'";
        //                                MySqlCommand cmdgetlastcyclefromMonsetup = new MySqlCommand(getlastcyclefromMonsetup, mysqlcon);
        //                                MySqlDataReader readergetlastcyclefromMonsetup = cmdgetlastcyclefromMonsetup.ExecuteReader();
        //                                DataTable dtgetlastcyclefromMonsetup = new DataTable();
        //                                dtgetlastcyclefromMonsetup.Load(readergetlastcyclefromMonsetup);
        //                                if (dtgetlastcyclefromMonsetup.Rows.Count > 0)
        //                                {

        //                                    int old_last_cycle_time = dtgetlastcyclefromMonsetup.Rows[0].Field<int>(22);
        //                                    DateTime datenow = System.DateTime.Now;
        //                                    var DifferenceInSec = (datenow - date).TotalSeconds;
        //                                    int difference = Convert.ToInt32(DifferenceInSec);
        //                                    string query23 = "INSERT INTO csi_dashboard.tbl_" + filenames[filein] + "(machine_tmp_filename_ , current_status_ , last_cycle_time , elapsed_time , correction_date_ ,machine_part_no_) VALUES ('" + filenames[filein] + "','" + l1_final[r1] + "','" + old_last_cycle_time + "','" + difference + "','" + formatForMySql + "','');";
        //                                    MySqlCommand cmdINSERT6 = new MySqlCommand(query23, mysqlcon);
        //                                    cmdINSERT6.ExecuteNonQuery();
        //                                }
        //                                readergetlastcyclefromMonsetup.Close();
        //                                // This down one is an old Code 
        //                                //DateTime datenow = System.DateTime.Now;
        //                                //var DifferenceInSec = (datenow - date).TotalSeconds;
        //                                //int difference = Convert.ToInt32(DifferenceInSec);
        //                                //string query16 = "INSERT INTO csi_dashboard.tbl_" + filenames[filein] + "(machine_tmp_filename_ , current_status_ , last_cycle_time , elapsed_time , correction_date_ ,machine_part_no_) VALUES ('" + filenames[filein] + "','" + l1_final[r1] + "','0','" + difference + "','" + formatForMySql + "','');";
        //                                //MySqlCommand cmdINSERT5 = new MySqlCommand(query16, mysqlcon);
        //                                //cmdINSERT5.ExecuteNonQuery();
        //                            }
        //                            else
        //                            { // If table is having records greater than zero

        //                                string query12 = "SELECT * FROM csi_dashboard.tbl_" + filenames[filein] + " WHERE current_status_='" + l1_final[r1] + "' AND correction_date_ ='" + formatForMySql + "' AND  machine_part_no_=''";
        //                                MySqlCommand cmdSELECT1 = new MySqlCommand(query12, mysqlcon);
        //                                MySqlDataReader datareader4 = cmdSELECT1.ExecuteReader();
        //                                DataTable dttable3 = new DataTable();
        //                                dttable3.Load(datareader4);
        //                                if (dttable3.Rows.Count == 0) //This means record  not exists previously
        //                                {
        //                                    // :::::::::::::::: Add here code if [r1-3] is empty so [r1-3] ni jagya e last 2 records levana 1st[0] is the latest one and 2nd[1] is the later one ::: Before ::::current_status_='" + l1_final[r1 - 3] + "'
        //                                    string query17 = "SELECT * FROM csi_dashboard.tbl_" + filenames[filein] + " WHERE current_status_<>'' ORDER BY correction_date_ DESC LIMIT 1"; //This gives last row where Current Status is CON and Order By Date gives last entery first
        //                                    MySqlCommand cmdSELECT2 = new MySqlCommand(query17, mysqlcon);
        //                                    MySqlDataReader datareader5 = cmdSELECT2.ExecuteReader();
        //                                    DataTable dttable4 = new DataTable();
        //                                    dttable4.Load(datareader5);
        //                                    if (dttable4.Rows.Count > 0) //Record Found
        //                                    {
        //                                        int prevelapse = dttable4.Rows[0].Field<int>(3);
        //                                        int prevlastcycle = dttable4.Rows[0].Field<int>(2);
        //                                        string last_status_ = dttable4.Rows[0].Field<string>(1);
        //                                        DateTime datenow1 = System.DateTime.Now;
        //                                        var DifferenceInSec1 = (datenow1 - date).TotalSeconds;
        //                                        int difference1 = Convert.ToInt32(DifferenceInSec1);
        //                                        if (last_status_ == "_CON")
        //                                        { /* HERE WE NEED TO ADD CODE FOR CYCLE COUNT AND CYCLE MULTIPLIER

        //                                     */

        //                                            if (l1_final[r1] == "_COFF")
        //                                            {// Changes from ON to OFF
        //                                             /*// Means last cycle is ON and Now it is Off then Last Cycle Set equal to elapsae time of the previous one and current cycle is set to zero 
        //                                          * Adds Also Cycle Count Logic Here
        //                                          */
        //                                             //string dateformat1 = l1_final[p1 - 3];
        //                                             //string timeformat1 = l1_final[q1 - 3];
        //                                             //string datetime1 = dateformat1 + " " + timeformat1;
        //                                             //DateTime date1 = DateTime.ParseExact(datetime1, "MM/dd/yy HH:mm:ss", CultureInfo.InvariantCulture); //DateTime.Parse(datetime1);
        //                                                DateTime last_correction_date_ = dttable4.Rows[0].Field<DateTime>(4);
        //                                                var differenceinsec = (date - last_correction_date_).TotalSeconds;
        //                                                //string formatForMySql1 = date1.ToString("yyyy-MM-dd HH:mm:ss");
        //                                                //TimeSpan ts = TimeSpan.Parse((date1.TimeOfDay).ToString());
        //                                                int newpreviouscycle = Convert.ToInt32(differenceinsec);
        //                                                string query18 = "INSERT INTO csi_dashboard.tbl_" + filenames[filein] + "(machine_tmp_filename_ , current_status_ , last_cycle_time , elapsed_time , correction_date_ ,machine_part_no_) VALUES ('" + filenames[filein] + "','" + l1_final[r1] + "','" + newpreviouscycle + "','" + difference1 + "','" + formatForMySql + "','');"; //prevelapse
        //                                                MySqlCommand cmdINSERT3 = new MySqlCommand(query18, mysqlcon);
        //                                                cmdINSERT3.ExecuteNonQuery();
        //                                                string updatemon = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET old_last_cycle_time = '" + newpreviouscycle + "' WHERE machine_tmp_filename_='" + filenames[filein] + ".SYS_'";
        //                                                MySqlCommand cmdupdatemon = new MySqlCommand(updatemon, mysqlcon);
        //                                                cmdupdatemon.ExecuteNonQuery();
        //                                            }
        //                                            else
        //                                            {

        //                                                // Means last cycle is ON and Now it is CHANGES TO ONE OF ABOVE then Last Cycle is not changed  and current cycle is set to zero elapse time always START COUNTING
        //                                                string query19 = "INSERT INTO csi_dashboard.tbl_" + filenames[filein] + "(machine_tmp_filename_ , current_status_ , last_cycle_time , elapsed_time , correction_date_ ,machine_part_no_) VALUES ('" + filenames[filein] + "','" + l1_final[r1] + "','" + prevlastcycle + "','" + difference1 + "','" + formatForMySql + "','');";
        //                                                MySqlCommand cmdINSERT5 = new MySqlCommand(query19, mysqlcon);
        //                                                cmdINSERT5.ExecuteNonQuery();
        //                                            }

        //                                        }
        //                                        else if (last_status_ == "_COFF") // If previous cycle is OFF
        //                                        {
        //                                            if (l1_final[r1] == "_CON")
        //                                            {
        //                                                // When cycle changes from OFF to On then :: last cycle no change current cycle is start from zero from Zero equal to elapsed time elapsed time starts from zero
        //                                                string query20 = "INSERT INTO csi_dashboard.tbl_" + filenames[filein] + "(machine_tmp_filename_ , current_status_ , last_cycle_time , elapsed_time , correction_date_ ,machine_part_no_) VALUES ('" + filenames[filein] + "','" + l1_final[r1] + "','" + prevlastcycle + "','" + difference1 + "','" + formatForMySql + "','');";
        //                                                MySqlCommand cmdINSERT6 = new MySqlCommand(query20, mysqlcon);
        //                                                cmdINSERT6.ExecuteNonQuery();

        //                                            }
        //                                            else
        //                                            {
        //                                                // Means last cycle is OFF and Now it is CHANGES TO ONE OF ABOVE then Last Cycle is not changed  and current cycle is set to zero elapse time always START COUNTING
        //                                                string query21 = "INSERT INTO csi_dashboard.tbl_" + filenames[filein] + "(machine_tmp_filename_ , current_status_ , last_cycle_time , elapsed_time , correction_date_ ,machine_part_no_) VALUES ('" + filenames[filein] + "','" + l1_final[r1] + "','" + prevlastcycle + "','" + difference1 + "','" + formatForMySql + "','');";
        //                                                MySqlCommand cmdINSERT7 = new MySqlCommand(query21, mysqlcon);
        //                                                cmdINSERT7.ExecuteNonQuery();

        //                                            }
        //                                        }
        //                                        else
        //                                        {
        //                                            //MessageBox.Show("States Changes in between other state not from ON or Off");
        //                                            string query22 = "INSERT INTO csi_dashboard.tbl_" + filenames[filein] + "(machine_tmp_filename_ , current_status_ , last_cycle_time , elapsed_time , correction_date_ ,machine_part_no_) VALUES ('" + filenames[filein] + "','" + l1_final[r1] + "','" + prevlastcycle + "','" + difference1 + "','" + formatForMySql + "','');";
        //                                            MySqlCommand cmdINSERT8 = new MySqlCommand(query22, mysqlcon);
        //                                            cmdINSERT8.ExecuteNonQuery();
        //                                        }
        //                                    }
        //                                    else
        //                                    { //Add code here This Means we dont have previous record where we have status Change last_cycle_time here 
        //                                        DateTime datenow2 = System.DateTime.Now;
        //                                        var DifferenceInSec2 = (datenow2 - date).TotalSeconds;
        //                                        int difference2 = Convert.ToInt32(DifferenceInSec2);
        //                                        string getlastcyclefromMonsetup = "SELECT * FROM csi_dashboard.tbl_monsetup_data WHERE machine_tmp_filename_='" + filenames[filein] + ".SYS_'";
        //                                        MySqlCommand cmdgetlastcyclefromMonsetup = new MySqlCommand(getlastcyclefromMonsetup, mysqlcon);
        //                                        MySqlDataReader readergetlastcyclefromMonsetup = cmdgetlastcyclefromMonsetup.ExecuteReader();
        //                                        DataTable dtgetlastcyclefromMonsetup = new DataTable();
        //                                        dtgetlastcyclefromMonsetup.Load(readergetlastcyclefromMonsetup);
        //                                        if (dtgetlastcyclefromMonsetup.Rows.Count > 0)
        //                                        {
        //                                            int old_last_cycle_time = dtgetlastcyclefromMonsetup.Rows[0].Field<int>(22);
        //                                            string query23 = "INSERT INTO csi_dashboard.tbl_" + filenames[filein] + "(machine_tmp_filename_ , current_status_ , last_cycle_time , elapsed_time , correction_date_ ,machine_part_no_) VALUES ('" + filenames[filein] + "','" + l1_final[r1] + "','" + old_last_cycle_time + "','" + difference2 + "','" + formatForMySql + "','');";
        //                                            MySqlCommand cmdINSERT6 = new MySqlCommand(query23, mysqlcon);
        //                                            cmdINSERT6.ExecuteNonQuery();
        //                                        }
        //                                        readergetlastcyclefromMonsetup.Close();
        //                                    }
        //                                    datareader5.Close();
        //                                }
        //                                else // This means  record already exists 
        //                                { //Update Elapsed Time Every Second
        //                                  // Code here for the last entry which has cycle not emty Updates it's elapse_time every second
        //                                    DateTime datenow3 = System.DateTime.Now;
        //                                    var DifferenceInSec3 = (datenow3 - date).TotalSeconds;
        //                                    int difference3 = Convert.ToInt32(DifferenceInSec3);
        //                                    string query24 = "UPDATE IGNORE csi_dashboard.tbl_" + filenames[filein] + " SET elapsed_time ='" + difference3 + "' WHERE current_status_<>'' ORDER BY correction_date_ DESC LIMIT 1;";
        //                                    MySqlCommand cmdUPDATE1 = new MySqlCommand(query24, mysqlcon);
        //                                    cmdUPDATE1.ExecuteNonQuery();

        //                                }
        //                                datareader4.Close();
        //                            }
        //                            p1 += 3;
        //                            q1 += 3;
        //                            r1 += 3;
        //                            datareader3.Close();
        //                        }
        //                    }
        //                }
        //                else //if(sizeoffile==filelength1)
        //                {
        //                    // Code Here if MonitorData files length is equal to the filesize in ehubconfig table
        //                    //DateTime datenow3 = System.DateTime.Now;
        //                    //var DifferenceInSec3 = (datenow3 - date).TotalSeconds;
        //                    ////int difference3 = Convert.ToInt32(DifferenceInSec3);
        //                    //string query24 = "UPDATE IGNORE csi_dashboard.tbl_" + filenames[filein] + " SET elapsed_time ='""' WHERE current_status_<>'' ORDER BY correction_date_ DESC LIMIT 1;";
        //                    //MySqlCommand cmdUPDATE1 = new MySqlCommand(query24, mysqlcon);
        //                    //cmdUPDATE1.ExecuteNonQuery();
        //                }
        //            }
        //            filein++;
        //            reader12.Close();
        //        }
        //        //// Code to get Data from monsetup file
        //        string searchKeyword2 = "ON:"; //ON----> represent machine status ON/OFF(0/1)
        //        string[] textLines2 = File.ReadAllLines(serverENETPath + @"_SETUP\MonSetup.sys"); /*C:\_eNETDNC\_SETUP\MonSetup.sys*/
        //        List<string> results2 = new List<string>();
        //        foreach (string line2 in textLines2)
        //        {
        //            if (line2.Contains(searchKeyword2))
        //            {
        //                results2.Add(line2.Replace("ON:", ""));
        //            }
        //        }
        //        File.WriteAllLines(fileName, results2);
        //        string[] lines2 = File.ReadLines(fileName).ToArray();
        //        k = 1; l = 1; p = 0;
        //        int m2 = lines2.Length;
        //        while (p < m2)
        //        {
        //            if (k <= 16 && l <= 8)
        //            {
        //                if (Convert.ToInt32(lines2[p]) == 0)
        //                {
        //                    string query3 = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET mon_state_='" + Convert.ToInt32(lines2[p]) + "' WHERE monitoring_id_ ='" + Convert.ToString(k) + "," + Convert.ToString(l) + "'";
        //                    MySqlCommand cmdUPDATE3 = new MySqlCommand(query3, mysqlcon);
        //                    cmdUPDATE3.ExecuteNonQuery();
        //                }
        //                else if (Convert.ToInt32(lines2[p]) == 1)
        //                {
        //                    string query4 = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET monitoring_='1',mon_state_='" + Convert.ToInt32(lines2[p]) + "' WHERE monitoring_id_ ='" + Convert.ToString(k) + "," + Convert.ToString(l) + "'";
        //                    MySqlCommand cmdUPDATE4 = new MySqlCommand(query4, mysqlcon);
        //                    cmdUPDATE4.ExecuteNonQuery();
        //                }

        //                k = Convert.ToInt32(k);
        //                l = Convert.ToInt32(l);
        //                l++;
        //                p++;
        //                if (l > 8)
        //                {
        //                    l = 1;
        //                    k++;
        //                }
        //            }
        //        }
        //        string searchKeyword3 = "MU:"; // MU---> represents cycle count
        //        List<string> ccresult = new List<string>();
        //        foreach (string line3 in textLines2)
        //        {
        //            if (line3.Contains(searchKeyword3))
        //            {
        //                ccresult.Add(line3.Replace("MU:", ""));
        //            }

        //        }
        //        File.WriteAllLines(fileName, ccresult);
        //        string[] textLines3 = File.ReadAllLines(fileName); /*@"C:\Users\BDesai\Desktop\test.txt"*/
        //        string searchKeyword4 = ",1";
        //        List<string> ccresult_final = new List<string>();
        //        foreach (string line4 in textLines3)
        //        {
        //            if (line4.Contains(searchKeyword4))
        //            {
        //                ccresult_final.Add(line4.Replace(",1", ""));
        //            }

        //        }
        //        File.WriteAllLines(fileName, ccresult_final);
        //        string[] cyclecountermul = File.ReadLines(fileName).ToArray(); //IT has cycle multiplier 
        //        k = 1; l = 1; p = 0;
        //        int m3 = cyclecountermul.Length;
        //        while (p < m3)
        //        {
        //            if (k <= 16 && l <= 8)
        //            {
        //                string query5 = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET part_multiplier_='" + Convert.ToInt32(cyclecountermul[p]) + "' WHERE monitoring_id_ ='" + Convert.ToString(k) + "," + Convert.ToString(l) + "'";
        //                MySqlCommand cmdUPDATE5 = new MySqlCommand(query5, mysqlcon);
        //                cmdUPDATE5.ExecuteNonQuery();

        //                k = Convert.ToInt32(k);
        //                l = Convert.ToInt32(l);
        //                l++;
        //                p++;
        //                if (l > 8)
        //                {
        //                    l = 1;
        //                    k++;
        //                }
        //            }
        //        }
        //        // Code for find cycle identifier (MIN/MAX or Ideal) ----->> IC:
        //        List<string> cycleiden = new List<string>();
        //        foreach (string line5 in textLines2)
        //        {
        //            if (line5.Contains("NM:"))
        //            {
        //                cycleiden.Add(line5.Replace("NM:", "")); //used to set cycle count zero at end of shift if equal to 3 (if 1 then dont count cycle count)
        //            }
        //            if (line5.Contains("IC:"))
        //            {
        //                cycleiden.Add(line5.Replace("IC:", ""));
        //            }
        //            if (line5.Contains("CI:"))
        //            {
        //                cycleiden.Add(line5.Replace("CI:", ""));
        //            }
        //            if (line5.Contains("CA:"))
        //            {
        //                cycleiden.Add(line5.Replace("CA:", ""));
        //            }
        //            if (line5.Contains("DA:"))
        //            {
        //                cycleiden.Add(line5.Replace("DA:", ""));
        //            }
        //        }
        //        File.WriteAllLines(fileName, cycleiden);
        //        string[] cycleiden_final = File.ReadLines(fileName).ToArray();
        //        k = 1; l = 1; p = 0;
        //        int q = 1, r = 2, s = 3, t = 4;
        //        int m4 = cycleiden_final.Length;
        //        while (p < m4)
        //        {
        //            if (k <= 16 && l <= 8)
        //            {
        //                if (Convert.ToInt32(cycleiden_final[q]) == 0) // MIN/MAX Cycle Type
        //                {
        //                    string query6 = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET cycle_identifier_='MIN/MAX',min_ideal_='" + Convert.ToInt32(cycleiden_final[r]) + "',max_perc_='" + Convert.ToInt32(cycleiden_final[s]) + "',department_name_='" + cycleiden_final[t] + "',reset_counter_='" + Convert.ToInt32(cycleiden_final[p]) + "' WHERE monitoring_id_ ='" + Convert.ToString(k) + "," + Convert.ToString(l) + "'";
        //                    MySqlCommand cmdUPDATE6 = new MySqlCommand(query6, mysqlcon);
        //                    cmdUPDATE6.ExecuteNonQuery();
        //                }
        //                else if (Convert.ToInt32(cycleiden_final[q]) == 1) // IDEAL Cycle Type In this Case if R<0 then we repalce Minimum value as 0
        //                {
        //                    double a = Convert.ToDouble(cycleiden_final[r]), b = Convert.ToDouble(cycleiden_final[s]);
        //                    double percentage = 0.00;
        //                    percentage = Math.Ceiling((a * b) / 100);
        //                    int R = Convert.ToInt32(a + percentage);
        //                    int S = Convert.ToInt32(a - percentage);
        //                    if (S < 0)
        //                    {
        //                        S = 0;
        //                    }
        //                    string query7 = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET cycle_identifier_='IDEAL',min_ideal_='" + S + "',max_perc_='" + R + "',department_name_='" + cycleiden_final[t] + "',reset_counter_='" + Convert.ToInt32(cycleiden_final[p]) + "' WHERE monitoring_id_ ='" + Convert.ToString(k) + "," + Convert.ToString(l) + "'";
        //                    MySqlCommand cmdUPDATE7 = new MySqlCommand(query7, mysqlcon);
        //                    cmdUPDATE7.ExecuteNonQuery();
        //                }
        //                k = Convert.ToInt32(k);
        //                l = Convert.ToInt32(l);
        //                l++;
        //                p += 5;
        //                q += 5;
        //                r += 5;
        //                s += 5;
        //                t += 5;
        //                if (l > 8)
        //                {
        //                    l = 1;
        //                    k++;
        //                }
        //            }
        //        }
        //        int len = filenames.Length - 1;
        //        for (filein = 0; filein < len; filein++)
        //        {
        //            string query28 = "SELECT * FROM csi_dashboard.tbl_" + filenames[filein] + " WHERE current_status_<>'' ORDER BY correction_date_ DESC LIMIT 1";
        //            MySqlCommand cmdSELECT11 = new MySqlCommand(query28, mysqlcon);
        //            MySqlDataReader reader12 = cmdSELECT11.ExecuteReader();
        //            DataTable dtreader12 = new DataTable();
        //            dtreader12.Load(reader12);
        //            if (dtreader12.Rows.Count > 0)
        //            {
        //                string status = dtreader12.Rows[0].Field<string>(1);
        //                int lastcycletime = dtreader12.Rows[0].Field<int>(2); //last cycle time ahiya thi nahi levanu jyare cycle change thai tyare last cycle time ni value carry forward karvani
        //                //int elapsedtime = dtreader12.Rows[0].Field<int>(3);
        //                DateTime datetime = dtreader12.Rows[0].Field<DateTime>(4);

        //                string formatForMySql1 = datetime.ToString("yyyy-MM-dd HH:mm:ss");
        //                //datetime.ToString();
        //                string query36 = "SELECT * FROM csi_dashboard.tbl_monsetup_data WHERE machine_tmp_filename_='" + filenames[filein] + ".SYS_';";
        //                MySqlCommand cmdSELECTq36 = new MySqlCommand(query36, mysqlcon);
        //                MySqlDataReader readerq36 = cmdSELECTq36.ExecuteReader();
        //                DataTable dtq36 = new DataTable();
        //                dtq36.Load(readerq36);
        //                if (dtq36.Rows.Count > 0)
        //                {

        //                    int oldlastcycletime = dtq36.Rows[0].Field<int>(22);
        //                    //DateTime datetimemon = (DateTime?)(dtq36.Rows[0].Field<DateTime>(15) == DBNull.Value ? null : dtq36.Rows[0].Field<DateTime>(15));
        //                    Utility.WriteToFile("Date Crash: " + dtq36.Rows[0][15].ToString());
        //                    DateTime datetimemon = DateTime.MinValue;
        //                    if (CheckDate(dtq36.Rows[0][15].ToString()))
        //                    {
        //                        datetimemon = dtq36.Rows[0].Field<DateTime>(15);//(DateTime)(dtq36.Rows[0][15] == DBNull.Value ? null : dtq36.Rows[0][15]);
        //                    }
        //                    //Convert.IsDBNull(dtq36.Rows[0].Field<DateTime>(15)) ? DateTime.MinValue : (DateTime)dtq36.Rows[0].Field<DateTime>(15);
        //                    //dtq36.Rows[0].Field<DateTime>(15);
        //                    if (datetimemon != datetime)
        //                    {
        //                        if (status == "_COFF")
        //                        {
        //                            string querry33 = "SELECT * FROM csi_dashboard.tbl_" + filenames[filein] + " WHERE current_status_<>'' ORDER BY correction_date_ desc LIMIT 2;";
        //                            MySqlCommand cmdSELECTq33 = new MySqlCommand(querry33, mysqlcon);
        //                            MySqlDataReader readerq33 = cmdSELECTq33.ExecuteReader();
        //                            DataTable dtq33 = new DataTable();
        //                            dtq33.Load(readerq33);

        //                            if (dtq33.Rows.Count > 1) // Means we have two rows
        //                            {
        //                                string oldstatus = dtq33.Rows[1].Field<string>(1);
        //                                if (oldstatus == "_CON")
        //                                {
        //                                    //DateTime last_correction_date_ = dtq33.Rows[0].Field<DateTime>(4);
        //                                    //DateTime current_correction_date_ = dtq33.Rows[1].Field<DateTime>(4);
        //                                    //TimeSpan ts1 = TimeSpan.Parse((DateTime.Now.TimeOfDay).ToString());
        //                                    //int timenow = Convert.ToInt32(ts.TotalSeconds);
        //                                    //DateTime time_difference = current_correction_date_ - last_correction_date_; 
        //                                    string query34 = "SELECT * FROM csi_dashboard.tbl_monsetup_data WHERE machine_tmp_filename_='" + filenames[filein] + ".SYS_';";
        //                                    MySqlCommand cmdSELECTq34 = new MySqlCommand(query34, mysqlcon);
        //                                    MySqlDataReader readerq34 = cmdSELECTq34.ExecuteReader();
        //                                    DataTable dtq34 = new DataTable();
        //                                    dtq34.Load(readerq34);
        //                                    if (dtq34.Rows.Count > 0)
        //                                    {
        //                                        int part_count_ = dtq34.Rows[0].Field<int>(6);
        //                                        int part_multiplier_ = dtq34.Rows[0].Field<int>(7);
        //                                        int min_ideal_ = dtq34.Rows[0].Field<int>(9);
        //                                        int max_perc_ = dtq34.Rows[0].Field<int>(10);
        //                                        int counter = 0;
        //                                        DateTime datenow = DateTime.Now;
        //                                        var diff = (datenow - datetime).TotalSeconds;
        //                                        int elapsedTimeDiff = Convert.ToInt32(diff);
        //                                        if (elapsedTimeDiff >= min_ideal_)/* && elapsedtime <= max_perc_*/
        //                                        {
        //                                            counter = part_count_ + part_multiplier_;
        //                                            string query35 = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET part_count_='" + counter + "',old_part_count_='" + counter + "',last_cycle_time='" + oldlastcycletime + "' WHERE machine_tmp_filename_ ='" + filenames[filein] + ".SYS_'; ";// was addded before hereIGNORE
        //                                            MySqlCommand cmdUPDATEq35 = new MySqlCommand(query35, mysqlcon);
        //                                            cmdUPDATEq35.ExecuteNonQuery();
        //                                        }

        //                                    }
        //                                    readerq34.Close();
        //                                }
        //                                else
        //                                { // If Previous Status is other than Cycle On Then do nothing 
        //                                }
        //                            }
        //                            else
        //                            if (dtq33.Rows.Count == 1)
        //                            {
        //                                // It has only one row which has status Do nothing Here
        //                                string query35_2 = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET last_cycle_time='" + oldlastcycletime + "' WHERE machine_tmp_filename_ ='" + filenames[filein] + ".SYS_'; ";// was addded before hereIGNORE
        //                                MySqlCommand cmdUPDATEq35_2 = new MySqlCommand(query35_2, mysqlcon);
        //                                cmdUPDATEq35_2.ExecuteNonQuery();
        //                            }
        //                            readerq33.Close();
        //                        }
        //                    }
        //                    else
        //                    {
        //                        //
        //                    }

        //                }
        //                readerq36.Close();
        //                watch.Stop();
        //                double TN = watch.Elapsed.TotalSeconds;
        //                int sec = Convert.ToInt32(TN);
        //                DateTime datenow1 = DateTime.Now;
        //                var diff1 = (datenow1 - datetime).TotalSeconds;
        //                int elapsedTimeDiff1 = Convert.ToInt32(diff1);
        //                int newelapsed = elapsedTimeDiff1;
        //                string query32 = "UPDATE IGNORE csi_dashboard.tbl_" + filenames[filein] + " SET elapsed_time='" + newelapsed + "' WHERE machine_tmp_filename_ ='" + filenames[filein] + "' AND current_status_<>'' ORDER BY correction_date_ DESC LIMIT 1;";
        //                MySqlCommand cmdUPDATE14 = new MySqlCommand(query32, mysqlcon);
        //                cmdUPDATE14.ExecuteNonQuery();

        //                /// Update this Query for last cycle time :::::::::: Old Statement :::
        //                string query29 = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET current_status_='" + status + "',last_cycle_time='" + lastcycletime + "',elapsed_time='" + newelapsed + "',correction_date_='" + formatForMySql1 + "' WHERE machine_tmp_filename_ ='" + filenames[filein] + ".SYS_';";
        //                MySqlCommand cmdUPDATE9 = new MySqlCommand(query29, mysqlcon);
        //                cmdUPDATE9.ExecuteNonQuery();
        //                Utility.WriteToFile(TN.ToString() + "Seconds");
        //                //label2.Text = TN.ToString() + "Seconds";
        //            }
        //            reader12.Close();
        //            // string query30 = "SELECT * FROM csi_dashboard.tbl_" + filenames[filein] + " WHERE machine_part_no_<>'' ORDER BY correction_date_ DESC LIMIT 1";
        //            string query30 = "(SELECT * FROM csi_dashboard.tbl_" + filenames[filein] + " WHERE machine_part_no_ LIKE '%OID:%' order by correction_date_ desc limit 1) union (SELECT * FROM csi_dashboard.tbl_" + filenames[filein] + " WHERE machine_part_no_ NOT LIKE '%OID:%' AND machine_part_no_<>''order by correction_date_ desc limit 1);";
        //            MySqlCommand cmdSELECT12 = new MySqlCommand(query30, mysqlcon);
        //            MySqlDataReader reader13 = cmdSELECT12.ExecuteReader();
        //            DataTable dtreader13 = new DataTable();
        //            dtreader13.Load(reader13);
        //            if (dtreader13.Rows.Count == 2) //Means we have two records consists of Operator id and Part Number
        //            {
        //                string operatorID = dtreader13.Rows[0].Field<string>(5);
        //                string partnum = dtreader13.Rows[1].Field<string>(5);
        //                string queryunion = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET machine_part_no_='" + partnum + " " + operatorID + "' WHERE machine_tmp_filename_ ='" + filenames[filein] + ".SYS_';";
        //                MySqlCommand cmdUPDATEunion = new MySqlCommand(queryunion, mysqlcon);
        //                cmdUPDATEunion.ExecuteNonQuery();
        //            }
        //            if (dtreader13.Rows.Count == 1) // We have output from either one of the tables(Part Number OR Operator ID)
        //            {
        //                string partnum = dtreader13.Rows[0].Field<string>(5);
        //                string query31 = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET machine_part_no_='" + partnum + "'  WHERE machine_tmp_filename_ ='" + filenames[filein] + ".SYS_';";
        //                MySqlCommand cmdUPDATE10 = new MySqlCommand(query31, mysqlcon);
        //                cmdUPDATE10.ExecuteNonQuery();
        //            }
        //            reader13.Close();
        //        }

        //        //InitTimer(); //Update Elapsed Time every seconds
        //        Timer2_InIt();
        //        if (mysqlcon != null && mysqlcon.State == ConnectionState.Open)
        //        { mysqlcon.Close(); }

        //    }
        //    catch (Exception ex)
        //    {
        //        csi_lib.LogServiceError("Error while checking the database : " + ex.Message, Convert.ToBoolean(1));


        //        string lineNumber = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);


        //        Utility.WriteToFile(ex.Message + "|" + ex.Source + "|" + ex.StackTrace + "|" + lineNumber);
        //        //throw;
        //        //InitTimer();
        //        //MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        mysqlcon.Close();
        //    }
        //    Utility.WriteToFile("---DoSetup Ends----");
        //}


        //protected bool CheckDate(String date)
        //{
        //    try
        //    {
        //        DateTime dt = DateTime.Parse(date);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        //public void DisplayTimeEvent(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        // This function used for monitoring shift at every second, update name of the day in each iteration 
        //        if (mysqlcon != null && mysqlcon.State == ConnectionState.Closed)
        //        { mysqlcon.Open(); }
        //        string querySELECT14 = "SELECT department_name_,reset_counter_,last_cycle_time,shift_name_,old_shift_name_,old_part_count_,old_last_cycle_time,monitoring_id_,part_count_ FROM csi_dashboard.tbl_monsetup_data";
        //        MySqlCommand cmdSELECT14 = new MySqlCommand(querySELECT14, mysqlcon);
        //        MySqlDataReader readerSELECT14 = cmdSELECT14.ExecuteReader();
        //        DataTable dtSELECT14 = new DataTable();
        //        dtSELECT14.Load(readerSELECT14);
        //        if (dtSELECT14.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in dtSELECT14.Rows)
        //            {
        //                string dept_name_ = dr[0].ToString();
        //                int reset_counter_ = Convert.ToInt32(dr[1].ToString());
        //                int last_cycle_time = Convert.ToInt32(dr[2].ToString());
        //                string shift_name_mon = dr[3].ToString();
        //                string old_shift_name_ = dr[4].ToString();
        //                int old_part_count_ = Convert.ToInt32(dr[5].ToString());
        //                int old_last_cycle_time = Convert.ToInt32(dr[6].ToString());
        //                int part_count_ = Convert.ToInt32(dr[8].ToString());
        //                //string machine_tmp_filename_ = (dr[7].ToString().Replace(".SYS_", " "));
        //                string monitoring_id_ = dr[7].ToString();
        //                string queryCHECKShift = "SELECT *,time_to_sec(now()) as TimeNow FROM csi_dashboard.tbl_shift_data WHERE department_name_='" + dept_name_ + "' AND day_name_=dayname(now()) AND shift_start_<time_to_sec(now()) AND shift_end_>time_to_sec(now());";
        //                MySqlCommand cmdqueryCHECKShift = new MySqlCommand(queryCHECKShift, mysqlcon);
        //                MySqlDataReader readerqueryCHECKShift = cmdqueryCHECKShift.ExecuteReader();
        //                DataTable dtqueryCHECKShift = new DataTable();
        //                dtqueryCHECKShift.Load(readerqueryCHECKShift);
        //                if (dtqueryCHECKShift.Rows.Count > 0)
        //                {
        //                    string shift_name_ = dtqueryCHECKShift.Rows[0].Field<string>(3);
        //                    int shift_start_ = dtqueryCHECKShift.Rows[0].Field<int>(4);
        //                    int shift_end_ = dtqueryCHECKShift.Rows[0].Field<int>(5);
        //                    //int timenow = dtqueryCHECKShift.Rows[0].Field<int>(12);
        //                    TimeSpan ts = TimeSpan.Parse((DateTime.Now.TimeOfDay).ToString());
        //                    int timenow = Convert.ToInt32(ts.TotalSeconds);
        //                    string dayname = System.DateTime.Now.DayOfWeek.ToString();
        //                    //string machinename = machine_tmp_filename_ + ".SYS_";
        //                    int shift_change_start = (shift_end_ - 360);
        //                    int shift_change_end = (shift_end_ - 180);
        //                    // int shift_change_onstart_end = (shift_start_ + 180);
        //                    if (old_shift_name_ == shift_name_mon || old_shift_name_ == " " || shift_name_mon == " ")
        //                    {
        //                        //if (timenow >= (shift_start_ + 1) && timenow <= shift_change_onstart_end)
        //                        //{
        //                        //    string UPDATEOldShifts_AfterShiftStart = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET old_shift_name_='" + shift_name_ + "',shift_name_='" + shift_name_ + "',shift_start_='" + shift_start_ + "',shift_end_='" + shift_end_ + "',day_name_='" + dayname + "',old_part_count_='" + part_count_ + "' WHERE monitoring_id_='" + monitoring_id_ + "';";
        //                        //    MySqlCommand cmdUPDATEOldShifts_AfterShiftStart = new MySqlCommand(UPDATEOldShifts_AfterShiftStart, mysqlcon);
        //                        //    cmdUPDATEOldShifts_AfterShiftStart.ExecuteNonQuery();
        //                        //}
        //                        if (timenow >= shift_change_start && timenow <= shift_change_end)
        //                        {
        //                            // Update Old_sHift Name Here before Shift_End
        //                            string UPDATEOldShifts_BeforeShiftEnd = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET old_shift_name_='" + shift_name_ + "',shift_name_='" + shift_name_ + "',shift_start_='" + shift_start_ + "',shift_end_='" + shift_end_ + "',day_name_='" + dayname + "',old_part_count_='" + part_count_ + "' WHERE monitoring_id_='" + monitoring_id_ + "';";
        //                            MySqlCommand cmdUPDATEOldShifts_BeforeShiftEnd = new MySqlCommand(UPDATEOldShifts_BeforeShiftEnd, mysqlcon);
        //                            cmdUPDATEOldShifts_BeforeShiftEnd.ExecuteNonQuery();
        //                        }
        //                        else
        //                        {
        //                            //This executes normally everytime 
        //                            string UPDATEOnlyShifts = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET shift_name_='" + shift_name_ + "',shift_start_='" + shift_start_ + "',shift_end_='" + shift_end_ + "',day_name_='" + dayname + "',old_part_count_='" + part_count_ + "' WHERE monitoring_id_='" + monitoring_id_ + "';";
        //                            MySqlCommand cmdUPDATEOnlyShifts = new MySqlCommand(UPDATEOnlyShifts, mysqlcon);
        //                            cmdUPDATEOnlyShifts.ExecuteNonQuery();
        //                        }
        //                    }
        //                    else if (old_shift_name_ != shift_name_mon)
        //                    {
        //                        //Reset Elapsed Time and Part Count, Drop Tables(Calling LoadFiles_Load) values when Shift changes (Write code here for Every ROW in Monsetup)
        //                        if (reset_counter_ == 3)
        //                        {
        //                            string UPDATEPartCount = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET old_shift_name_='" + shift_name_ + "',part_count_='0',elapsed_time='0',last_cycle_time='" + old_last_cycle_time + "',day_name_='" + dayname + "' WHERE monitoring_id_='" + monitoring_id_ + "';";
        //                            MySqlCommand cmdUPDATEPartCount = new MySqlCommand(UPDATEPartCount, mysqlcon);
        //                            cmdUPDATEPartCount.ExecuteNonQuery();
        //                        }
        //                        else if (reset_counter_ == 1)
        //                        {
        //                            string NoUPDATEPartCount = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET old_shift_name_='" + shift_name_ + "',day_name_='" + dayname + "',part_count_='" + old_part_count_ + "',elapsed_time='0',last_cycle_time='" + old_last_cycle_time + "' WHERE monitoring_id_='" + monitoring_id_ + "';";
        //                            MySqlCommand cmdNoUPDATEPartCount = new MySqlCommand(NoUPDATEPartCount, mysqlcon);
        //                            cmdNoUPDATEPartCount.ExecuteNonQuery();
        //                        }
        //                        // This will drop all tables expext Monsetup_data and Create new tables to get new Data from all the files
        //                        string EXISTSmonsetup = "show tables from csi_dashboard LIKE 'tbl_monsetup_data';";
        //                        MySqlCommand cmdEXISTSmonsetup = new MySqlCommand(EXISTSmonsetup, mysqlcon);
        //                        MySqlDataReader readerEXISTSmonsetup = cmdEXISTSmonsetup.ExecuteReader();
        //                        if (readerEXISTSmonsetup.HasRows)
        //                        {
        //                            readerEXISTSmonsetup.Close();
        //                            string SELECTmonsetup = "SELECT * FROM csi_dashboard.tbl_monsetup_data";
        //                            MySqlCommand cmdSELECTmonsetup = new MySqlCommand(SELECTmonsetup, mysqlcon);
        //                            //readerEXISTS.Close();

        //                            MySqlDataReader readerSELECTmonsetup = cmdSELECTmonsetup.ExecuteReader();
        //                            DataTable dtSELECTmonsetup = new DataTable();
        //                            dtSELECTmonsetup.Load(readerSELECTmonsetup);
        //                            if (dtSELECTmonsetup.Rows.Count > 0)
        //                            {
        //                                foreach (DataRow dr1 in dtSELECTmonsetup.Rows)
        //                                {
        //                                    // dr["file_size"] = 0; //Column No : 4
        //                                    string UPDATEoldshift = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET old_shift_name_=' '";
        //                                    MySqlCommand cmdUPDATEoldshift = new MySqlCommand(UPDATEoldshift, mysqlcon);
        //                                    cmdUPDATEoldshift.ExecuteNonQuery();
        //                                }
        //                            }
        //                            readerSELECTmonsetup.Close();
        //                            //    readerEXISTSeh
        //                        }
        //                        string EXISTSehubconf = "DROP TABLE IF EXISTS csi_dashboard.tbl_ehubconf_data;";
        //                        MySqlCommand cmdEXISTSehubconf = new MySqlCommand(EXISTSehubconf, mysqlcon);
        //                        cmdEXISTSehubconf.ExecuteNonQuery();
        //                        string EXISTSshifttbl = "DROP TABLE IF EXISTS csi_dashboard.tbl_shift_data;";
        //                        MySqlCommand cmdEXISTSshifttbl = new MySqlCommand(EXISTSshifttbl, mysqlcon);
        //                        cmdEXISTSshifttbl.ExecuteNonQuery();
        //                        string EXISTSMonitorData = "show tables from csi_dashboard LIKE '%tbl_monitordata%';";
        //                        MySqlCommand cmdEXISTSMonitorData = new MySqlCommand(EXISTSMonitorData, mysqlcon);
        //                        MySqlDataReader readerEXISTSMonitorData = cmdEXISTSMonitorData.ExecuteReader();
        //                        DataTable dtMonitorData = new DataTable();
        //                        dtMonitorData.Load(readerEXISTSMonitorData);
        //                        if (dtMonitorData.Rows.Count > 0)
        //                        {
        //                            foreach (DataRow row in dtMonitorData.Rows)
        //                            {
        //                                string filename = row[0].ToString();   // 0 is the column value 
        //                                string DROPMondata = "DROP TABLE IF EXISTS csi_dashboard." + filename + ";";
        //                                MySqlCommand cmdDROPMondata = new MySqlCommand(DROPMondata, mysqlcon);
        //                                cmdDROPMondata.ExecuteNonQuery();
        //                            }
        //                        }
        //                        readerEXISTSMonitorData.Close();

        //                    }
        //                }
        //                readerqueryCHECKShift.Close();
        //            }
        //            readerSELECT14.Close();
        //            if (mysqlcon != null && mysqlcon.State == ConnectionState.Open)
        //            { mysqlcon.Close(); }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        csi_lib.LogServiceError("Error while checking the database : " + ex.Message, Convert.ToBoolean(1));
        //        Utility.WriteToFile(ex.ToString());
        //        //InitTimer();
        //        //MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        mysqlcon.Close();
        //    }


        //}


    }
}