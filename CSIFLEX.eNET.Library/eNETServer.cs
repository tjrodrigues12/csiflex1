using CSIFLEX.Database.Access;
using CSIFLEX.eNET.Library.Data;
using CSIFLEX.eNetLibrary;
using CSIFLEX.eNetLibrary.Data;
using CSIFLEX.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace CSIFLEX.eNET.Library
{
    public class eNETServer
    {

        #region singleton 
        private eNETServer() { }

        private static eNETServer _instance = null;

        public static eNETServer Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new eNETServer();
                return _instance;
            }
        }
        #endregion

        #region properties

        private static bool eNetLoaded_ = false;
        public static bool eNetLoaded
        {
            get
            {
                return eNetLoaded_;
            }
        }

        public static eNETConnection Connections { get; set; }

        public static List<DepartmentShift> Departments { get; set; }

        public static List<eNETMachine> Machines { get; set; }

        public static List<eNETMachine> MonitoredMachines
        {
            get
            {
                //return Machines.FindAll(m => MonList.FindIndex(ml => (ml == m.MachineName)) > -1);

                List<eNETMachine> machines = Machines.FindAll(m => MonList.FindIndex(ml => (ml == m.MachineName)) > -1);

                return machines.FindAll(m => m.IsMonitored && m.RedirectDPrint == "0,0");
            }
        }

        public List<eNetDashboardMachine> DashboardMachines { get; set; }

        public static List<string> MonList { get; set; }

        public static string EnetFolder { get; set; }

        public static string EnetMonitoringFolder { get; set; }

        public static string EnetReportsFolder { get; set; }

        public static string EnetSetupFolder { get; set; }

        public static string EnetTempFolder { get; set; }

        public static string Error { get; set; }

        #endregion


        public void Init(string _eNetFolderPath)
        {
            try
            {
                if (string.IsNullOrEmpty(_eNetFolderPath))
                    return;

                if (!Directory.Exists(_eNetFolderPath))
                {
                    Error = $"eNET folder ( {_eNetFolderPath} ) not found.";
                }

                eNetLoaded_ = true;
                EnetFolder = _eNetFolderPath;
                EnetMonitoringFolder = $"{EnetFolder}\\_MONITORING\\";
                EnetReportsFolder = $"{EnetFolder}\\_REPORTS\\";
                EnetSetupFolder = $"{EnetFolder}\\_SETUP\\";
                EnetTempFolder = $"{EnetFolder}\\_TMP\\";

                LoadServerProperties();
                LoadDepartments();
                LoadMachines();
                LoadMonList();

                DashboardMachines = new List<eNetDashboardMachine>();

                FileSystemWatcher watcher = new FileSystemWatcher()
                {
                    Path = EnetSetupFolder,
                    NotifyFilter = NotifyFilters.LastWrite,
                    Filter = "*.sys"
                };
                watcher.Changed -= OnSetupChanged;
                watcher.Changed += OnSetupChanged;
                watcher.EnableRaisingEvents = true;

            }
            catch (Exception ex)
            {
                Log.Fatal(ex);
            }
        }

        public void ReloadMachines()
        {
            LoadMachines();
        }

        public string CurrentStatus(string machineName)
        {
            string currentStatus = "";

            eNETMachine machine = Machines.FirstOrDefault(m => m.MachineName == machineName);

            if (machine == null)
                return currentStatus;

            try
            {
                IEnumerable<string> lines = File.ReadAllLines(machine.StatusTempFile).Reverse();

                foreach (string line in lines)
                {
                    if (!line.Contains("_PARTNO") && !line.Contains("_OPERATOR"))
                    {
                        string[] parts = line.Split(',');
                        currentStatus = parts[2];
                        break;
                    }
                }

                currentStatus = currentStatus.Replace("_CON", "CYCLE ON").Replace("_COFF", "CYCLE OFF").Replace("_SETUP", "SETUP");
            }
            catch (Exception e) { }

            return currentStatus;
        }

        public string LastChange(string machineName)
        {
            string lastChange = "";
            string lastOperator = "";
            string lastPartNumb = "";

            eNETMachine machine = Machines.FirstOrDefault(m => m.MachineName == machineName);

            if (machine == null)
                return lastChange;

            try
            {
                IEnumerable<string> lines = File.ReadAllLines(machine.StatusTempFile).Reverse();

                foreach (string line in lines)
                {
                    if (line.Contains("PARTNO") && string.IsNullOrEmpty(lastPartNumb))
                    {
                        lastPartNumb = line.Split(',')[2];
                    }
                    else if (line.Contains("_OPERATOR") && string.IsNullOrEmpty(lastOperator))
                    {
                        lastOperator = line.Split(',')[2];
                    }
                    else if (string.IsNullOrEmpty(lastChange))
                    {
                        lastChange = line;
                    }

                    if (!string.IsNullOrEmpty(lastOperator) && !string.IsNullOrEmpty(lastPartNumb) && !string.IsNullOrEmpty(lastChange))
                    {
                        break;
                    }
                }
            }
            catch (Exception e) { }

            if (!string.IsNullOrEmpty(lastPartNumb)) lastChange += $",{lastPartNumb}";
            if (!string.IsNullOrEmpty(lastOperator)) lastChange += $",{lastOperator}";

            return lastChange;
        }

        public string LastLine(string machineName)
        {
            string lastLine = "";

            eNETMachine machine = Machines.FirstOrDefault(m => m.MachineName == machineName);

            if (machine == null)
                return lastLine;

            try
            {
                lastLine = File.ReadAllLines(machine.StatusTempFile).Reverse().First();
            }
            catch (Exception e) { }

            return lastLine;
        }

        public void AddStatusMachine(string position, string status, string command)
        {
            string eNetMonListFile = Path.Combine(EnetSetupFolder, "MonSetup.sys");

            if (!File.Exists(eNetMonListFile))
            {
                Error = $"eNET Departments file ( {eNetMonListFile} ) not found.";
                eNetLoaded_ = false;
                return;
            }

            List<string> allLines = FileUtils.AllLinesFromTxtFile(eNetMonListFile);

            int idxBase = allLines.FindIndex(l => l == $"{position}:");

            string[] lines = allLines.ToArray();

            for (int idx = idxBase + 161; idx > idxBase + 116; idx -= 3)
            {
                if (lines[idx].Length == 5)
                {
                    lines[idx] = lines[idx].Substring(0, 5) + status;
                    lines[idx + 1] = lines[idx + 1].Substring(0, 5) + command;
                    lines[idx + 2] = lines[idx + 2].Substring(0, 5) + "RECEIVED^J";
                    break;
                }
            }

            File.WriteAllLines(eNetMonListFile, lines);
        }

        public List<eNetDashboardMachine> GetMachinesStatus()
        {
            return DashboardMachines;
        }

        public void LoadMachinesStatus()
        {
            foreach (eNETMachine machine in MonitoredMachines)
            {
                eNetDashboardMachine dashMachine = DashboardMachines.FirstOrDefault(m => m.MachineName == machine.MachineName);

                if (dashMachine == null)
                {
                    dashMachine = new eNetDashboardMachine()
                    {
                        MachineName = machine.MachineName,
                        Status = "",
                        Shift = -1,
                        OperatorCod = "",
                        PartNumber = "",
                        LastWriteTime = DateTime.Now
                    };

                    //Log.Debug($"Machine: {dashMachine.MachineName}, Status: {dashMachine.Status}, Shift: {dashMachine.Shift}, Last cycle: {dashMachine.LastCycle}, Elapsed time: {dashMachine.ElapsedTime}");

                    LoadDashboardMachine(ref dashMachine, machine);

                    DashboardMachines.Add(dashMachine);
                }
                else
                {
                    //Log.Debug($"Machine: {dashMachine.MachineName}, Status: {dashMachine.Status}, Shift: {dashMachine.Shift}, Last cycle: {dashMachine.LastCycle}, Elapsed time: {dashMachine.ElapsedTime}");

                    LoadDashboardMachine(ref dashMachine, machine);
                }

                //Log.Debug($"Machine: {dashMachine.MachineName}, Status: {dashMachine.Status}, Shift: {dashMachine.Shift}, Last cycle: {dashMachine.LastCycle}, Elapsed time: {dashMachine.ElapsedTime}");
            }
        }

        public string GetHtmlDashboard()
        {
            StringBuilder html = new StringBuilder();

            html.Append($"<html><head>");
            html.Append($"<style type=\"text / css\"></style>");
            html.Append($"<body>");
            html.Append($"<table><tr><th height='50' colspan='7' bgcolor=\"#6699CC\" class='Large' scope='col'>- <span class='red'>eNet</span>DNC Machine Monitoring -</th></tr></table>");
            html.Append($"<table></table>");
            html.Append($"<table>");
            html.Append($"<tr>");
            html.Append($"<th>Machine Name / Shift</th>");
            html.Append($"<th>Status</th>");
            html.Append($"<th>Part Number / Operator</th>");
            html.Append($"<th>Cycle Count</th>");
            html.Append($"<th>Last Cycle</th>");
            html.Append($"<th>Current Cycle</th>");
            html.Append($"<th>Elapsed Time</th>");
            html.Append($"<th>Feed Override</th>");
            html.Append($"<th>Spindle Override</th>");
            //html.Append($"<th>Status Time</th>");
            //html.Append($"<th>Record</th>");
            html.Append($"</tr>{Environment.NewLine}");

            foreach (eNetDashboardMachine machine in DashboardMachines)
            {
                string currentCycle = machine.Status.ToUpper() == "CYCLE ON" ? TimeSpan.FromSeconds(machine.CurrentCycle).ToString("c") : "00:00:00";

                html.Append($"<tr align=\"center\">{ Environment.NewLine }");
                html.Append($"<td bgcolor=\"#0033FF\"><font color='#FFFFFF'>{ machine.MachineName }<LI>{ machine.Shift }</font></td>{ Environment.NewLine }");
                html.Append($"<td bgcolor=\"#0033FF\"><font color='#FFFFFF'>{ machine.Status }</font></td>");
                html.Append($"<td bgcolor=\"#0033FF\"><font color='#FFFFFF'>{ machine.PartNumber }<LI>{ machine.OperatorCod }</font></td>");
                html.Append($"<td bgcolor=\"#0033FF\"><font color='#FFFFFF'>0</font></td>");
                html.Append($"<td bgcolor=\"#0033FF\"><font color='#FFFFFF'>{ TimeSpan.FromSeconds(machine.LastCycle).ToString("c") }</font></td>");
                html.Append($"<td bgcolor=\"#0033FF\"><font color='#FFFFFF'>{ currentCycle }</font></td>");
                html.Append($"<td bgcolor=\"#0033FF\"><font color='#FFFFFF'>{ TimeSpan.FromSeconds(machine.ElapsedTime).ToString("c") }</font></td>");
                html.Append($"<td bgcolor=\"#0033FF\"><font color='#FFFFFF'>{ machine.FeedOverride } %</font></td>");
                html.Append($"<td bgcolor=\"#0033FF\"><font color='#FFFFFF'>{ machine.SpindleOverride } %</font></td>");
                //html.Append($"<td bgcolor=\"#0033FF\"><font color='#FFFFFF'>{ TimeSpan.FromSeconds(machine.StatusTime).ToString("c") }</font></td>");
                //html.Append($"<td bgcolor=\"#0033FF\"><font color='#FFFFFF'>{ machine.TempRecord }</font></td>");
                html.Append($"</tr>{Environment.NewLine}");
            }

            html.Append($"</table>");
            html.Append($"</body>");
            html.Append($"</html>");

            return html.ToString();
        }

        public int GetCurrentShift(string deptName, DateTime date = default(DateTime))
        {
            int shift = 0;

            try
            {
                date = date == default(DateTime) ? DateTime.Now : date;

                DayOfWeek dayOfWeek = date.DayOfWeek;

                long time = (long)date.TimeOfDay.TotalSeconds;

                DepartmentShift department = Departments.FirstOrDefault(d => d.Name == deptName);

                List<ShiftItem> dayOfWeekShifts = department.WeekShifts[dayOfWeek];

                foreach (ShiftItem item in dayOfWeekShifts)
                {
                    if (item.Start > 0 || item.End > 0)
                    {
                        if (item.Start == item.End)
                        {
                            return item.Number;
                        }
                        else if (item.End > item.Start)
                        {
                            if (time >= item.Start && time < item.End)
                                return item.Number;
                        }
                        else
                        {
                            if (time >= item.Start ^ time < item.End)
                                return item.Number;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            return shift;
        }

        public static void SendCMDEnetFTP(string ip, string pwd, string remoteFile, string machinename, string command)
        {
            EnetAPI.SendCMDEnetFTP(ip, pwd, remoteFile, machinename, command);
        }

        public static void SendStatusToEnetOverUdp(string localIp, string eNetIp, int eNetPort, string machinePosition, string status)
        {
            EnetAPI.SendStatusToEnetOverUdp(localIp, eNetIp, eNetPort, machinePosition, status);
        }

        public static void SendPartNumToEnetOverUdp(string localIp, string eNetIp, int eNetPort, string machinePosition, string partNumCmd, string partNumValue, int cycleTime = 0, int cycleMultiplier = 0)
        {
            EnetAPI.SendPartNumToEnetOverUdp(localIp, eNetIp, eNetPort, machinePosition, partNumCmd, partNumValue, cycleTime, cycleMultiplier);
        }

        private void LoadDashboardMachine(ref eNetDashboardMachine dashMachine, eNETMachine enetMachine)
        {
            DateTime statusDatetime = dashMachine.StatusDateTime;
            string status = dashMachine.Status;
            string partNo = dashMachine.PartNumber;
            string operat = dashMachine.OperatorCod;
            string tempRg = dashMachine.TempRecord;
            int shift = dashMachine.Shift;
            int feed = dashMachine.FeedOverride;
            int spindle = dashMachine.SpindleOverride;

            long lastCycle = dashMachine.LastCycle;
            long lastCycleEnd = 0;
            long statusTime = dashMachine.StatusTime;

            if (File.Exists(enetMachine.StatusTempFile))
            {
                FileInfo fileInfo = new FileInfo(enetMachine.StatusTempFile);

                DateTime lastWriteTime = fileInfo.LastWriteTime;

                //Log.Debug($"Check file changed. Machine: {dashMachine.MachineName}, Last Write time: {dashMachine.LastWriteTime} / {lastWriteTime}");
                //Log.Debug($"Last status: {dashMachine.Status}, last shift: {dashMachine.Shift}");

                if (dashMachine.LastWriteTime != lastWriteTime || dashMachine.Shift < 0)
                {
                    //Log.Debug($"File changed. Machine: {dashMachine.MachineName}, Last Write time: {lastWriteTime}");

                    lastCycle = 0;
                    statusTime = 0;
                    status = "";
                    partNo = "";
                    operat = "";
                    try
                    {
                        IEnumerable<string> lines = File.ReadAllLines(enetMachine.StatusTempFile).Reverse();

                        foreach (string line in lines)
                        {
                            string[] fields = line.Split(',');
                            string[] newStatus = fields[2].Replace("_CON", "CYCLE ON").Replace("_COFF", "CYCLE OFF").Replace("_SETUP", "SETUP").Split(':');

                            if (line.Contains("_PARTNO") && string.IsNullOrEmpty(partNo))
                            {
                                partNo = newStatus[1];
                                if (string.IsNullOrEmpty(partNo)) partNo = "_";
                            }
                            else if (line.Contains("_OPERATOR") && string.IsNullOrEmpty(operat))
                            {
                                operat = newStatus[1];
                                if (string.IsNullOrEmpty(operat)) operat = "_";
                            }
                            else if (status == "" && !newStatus[0].StartsWith("_"))
                            {
                                status = newStatus[0];

                                if (line.Contains("- LOCKED"))
                                    status = "LOCKED";

                                if (status == "CYCLE ON" && newStatus.Length > 1)
                                {
                                    int.TryParse(newStatus[1].Substring(1), out feed);
                                    int.TryParse(fields[3], out spindle);
                                }

                                statusDatetime = DateTime.ParseExact($"{ fields[0] } { fields[1] }", "MM/dd/yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                                statusTime = (long)DateTime.ParseExact($"{ fields[0] } { fields[1] }", "MM/dd/yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).TimeOfDay.TotalSeconds;
                                tempRg = line;

                                shift = GetCurrentShift(enetMachine.Department, statusDatetime);
                            }

                            if (lastCycle == 0)
                            {
                                if (newStatus[0] != "CYCLE ON" && !newStatus[0].StartsWith("_"))
                                {
                                    lastCycleEnd = (long)DateTime.ParseExact($"{ fields[0] } { fields[1] }", "MM/dd/yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).TimeOfDay.TotalSeconds;
                                }
                                else if (lastCycleEnd > 0 && newStatus[0] == "CYCLE ON")
                                {
                                    lastCycle = lastCycleEnd - (long)DateTime.ParseExact($"{ fields[0] } { fields[1] }", "MM/dd/yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).TimeOfDay.TotalSeconds;

                                    if (status != "CYCLE ON" && newStatus.Length > 1)
                                    {
                                        int.TryParse(newStatus[1].Substring(1), out feed);
                                        int.TryParse(fields[3], out spindle);
                                    }
                                }
                            }

                            Log.Debug($"Machine: {dashMachine.MachineName}, Status: {status}, Last cycle: {dashMachine.LastCycle}, Elapsed time: {dashMachine.ElapsedTime}, Line: {line}");

                            if (!string.IsNullOrEmpty(status) && !string.IsNullOrEmpty(partNo) && !string.IsNullOrEmpty(operat) && lastCycle != 0)
                            {
                                break;
                            }
                        }

                        //DataTable tbStatus = MySqlAccess.GetDataTable($"SELECT * FROM csi_machineperf.{enetMachine.MachineDbTable} ORDER BY time DESC LIMIT 1;");

                        //if (tbStatus.Rows.Count > 0)
                        //{
                        //    shift = int.Parse(tbStatus.Rows[0]["shift"].ToString());
                        //}

                        dashMachine.LastWriteTime = lastWriteTime;

                        Log.Debug($"New status: {status}, shift: {shift}, {lastWriteTime}");
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"Machine: {enetMachine.MachineName}", ex);
                    }
                }
            }

            dashMachine.Status = status;
            dashMachine.StatusDateTime = statusDatetime;
            dashMachine.Shift = shift;
            dashMachine.PartNumber = partNo;
            dashMachine.OperatorCod = operat;
            dashMachine.LastCycle = lastCycle;
            dashMachine.StatusTime = statusTime;
            dashMachine.ElapsedTime = (long)DateTime.Now.TimeOfDay.TotalSeconds - dashMachine.StatusTime;
            dashMachine.TempRecord = tempRg;
            dashMachine.CurrentCycle = dashMachine.Status == "CYCLE ON" ? dashMachine.ElapsedTime : 0;
            dashMachine.FeedOverride = feed;
            dashMachine.SpindleOverride = spindle;

            if (dashMachine.ElapsedTime < 0) dashMachine.ElapsedTime += 86400;

            Log.Debug($"Machine: {dashMachine.MachineName}, Status: {dashMachine.Status}, Shift: {dashMachine.Shift}, Last cycle: {dashMachine.LastCycle}, Elapsed time: {dashMachine.ElapsedTime}");

        }

        private void OnSetupChanged(object source, FileSystemEventArgs e)
        {
            try
            {
                if (e.Name == "Options.sys")
                    LoadServerProperties();
                else if (e.Name == "ShiftSetup2.sys")
                    LoadDepartments();
                else if (e.Name == "MonList.sys")
                {
                    LoadMachines();
                    LoadMonList();
                }
                else if (e.Name == "eHUBConf.sys")
                    LoadMachines();

            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        private static void LoadServerProperties()
        {
            string eNetOptionsFile = Path.Combine(EnetSetupFolder, "Options.sys");

            if (!File.Exists(eNetOptionsFile))
            {
                Error = $"Options.sys file ( {eNetOptionsFile} ) not found.";
                eNetLoaded_ = false;
                return;
            }

            try
            {
                Connections = new eNETConnection();

                string line = "";
                int nLine = 0;
                using (StreamReader fs = new StreamReader(File.OpenRead(eNetOptionsFile)))
                {
                    while (!(fs.EndOfStream))
                    {
                        nLine++;
                        line = fs.ReadLine();

                        if (nLine == 9)
                        {
                            Connections.UdpIp = Utilities.Utilities.ConvertHexToIpAddress(line);
                            Connections.UdpPort = 17;
                        }

                        if (nLine == 21)
                        {
                            string httpIp = Utilities.Utilities.ConvertHexToIpAddress(line);
                            if (httpIp == "0.0.0.0") httpIp = Utilities.Utilities.GetLocalIPAddress();

                            Connections.HttpIp = httpIp;
                        }

                        if (nLine == 25)
                        {
                            string ftpIp = Utilities.Utilities.ConvertHexToIpAddress(line);
                            if (ftpIp == "0.0.0.0") ftpIp = Utilities.Utilities.GetLocalIPAddress();

                            Connections.FtpIp = ftpIp;
                        }

                        if (nLine == 26)
                            Connections.FtpPassword = line;

                        if (nLine == 39)
                        {
                            int port = int.Parse(line);
                            if (port == 0) port = 21;

                            Connections.FtpPort = port;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        private void LoadDepartments()
        {

            Departments = EnetAPI.GetDepartmentsAndShifts(EnetFolder);

            return;
        }

        private static Dictionary<DayOfWeek, List<ShiftItem>> GetShiftItem(string[] line)
        {
            Dictionary<DayOfWeek, List<ShiftItem>> retVal = new Dictionary<DayOfWeek, List<ShiftItem>>();
            if (line != null && line.Length == 42)
            {
                for (int i = 0; i < 7; i++)
                {
                    var items = new List<ShiftItem>();
                    for (int j = 0; j < 6; j += 2)
                    {
                        if (line[i * 6 + j] != "0")
                        {
                            int startSeconds = 0;
                            int endSeconds = 0;
                            if (int.TryParse(line[i * 6 + j], out startSeconds) && int.TryParse(line[i * 6 + j + 1], out endSeconds))
                            {
                                ShiftItem item = new ShiftItem
                                {
                                    Number = j / 2 + 1,
                                    Start = startSeconds,
                                    End = endSeconds
                                };
                                items.Add(item);
                            }
                        }
                    }
                    if (items.Any())
                        retVal.Add((DayOfWeek)i, items);
                }
            }
            return retVal;
        }

        private void LoadMonList()
        {
            MonList = new List<string>();

            string eNetMonListFile = Path.Combine(EnetSetupFolder, "MonList.sys");

            if (!File.Exists(eNetMonListFile))
            {
                Error = $"eNET Departments file ( {eNetMonListFile} ) not found.";
                eNetLoaded_ = false;
                return;
            }

            var allLines = FileUtils.AllLinesFromTxtFile(eNetMonListFile);

            foreach (string line in allLines)
            {
                eNETMachine mach = Machines.FirstOrDefault(m => m.MachineName == line);
                if (mach != null)
                    mach.IsInMonList = true;
            }

            MonList.AddRange(allLines);
        }

        private void LoadMachines()
        {
            List<MachineConfig> machines = EnetAPI.LoadAllMachinesList(EnetFolder);

            Machines = new List<eNETMachine>();

            foreach (MachineConfig machCfg in machines)
            {
                eNETMachine eNetMach = new eNETMachine();

                machCfg.CopyPropertiesTo<MachineConfig, eNETMachine>(eNetMach);

                Machines.Add(eNetMach);
            }
        }

        private static string GetMachineProtocol(int eh, int fi, int eo)
        {
            string protocol = "";
            if (eo > 0)
            {
                if (eo == 1)
                    protocol = "COM";
            }
            else if (fi > 0)
            {
                if (fi == 1)
                    protocol = "FTP";
                else if (fi == 2)
                    protocol = "CAPS";
            }
            else if (eh == 0)
                protocol = "eHUB";
            else if (eh > 0)
            {
                if (eh == 1)
                    protocol = "xlConnect";
                else if (eh == 4)
                    protocol = "USB";
                else if (eh == 6)
                    protocol = "xlHUB";
            }
            return protocol;
        }

        private static string MachineDbTableName(string machineName)
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

        private static string MachineTempFileName(string position)
        {
            string fileName = "";

            if (String.IsNullOrEmpty(position))
                return fileName;

            var reg = new Regex("^([1-9]|1[0-6]),[1-8]$");
            if (!reg.Match(position).Success)
                return fileName;

            string[] parts = position.Split(',');
            int part1 = int.Parse(parts[0]) - 1;
            int part2 = int.Parse(parts[1]) - 1;

            fileName = Path.Combine(EnetTempFolder, $"MonitorData{part1}{part2}.SYS_");

            return fileName;
        }
    }

    public class eNETConnection
    {
        public string HttpIp { get; set; }

        public string UdpIp { get; set; }

        public int UdpPort { get; set; }

        public string FtpIp { get; set; }

        public int FtpPort { get; set; }

        public string FtpPassword { get; set; }
    }

    //public class eNETDepartment
    //{
    //    public string Name { get; set; }

    //    public ShiftDateMode Mode { get; set; }

    //    public Dictionary<DayOfWeek, List<ShiftItem>> WeekShifts;
    //}

    //public class eNETSetupProperty
    //{
    //    public string Name { get; set; }

    //    public string Prefix { get; set; }

    //    public int LinePosition { get; set; }
    //}

    //public class ShiftItem
    //{
    //    public int Number { get; set; }

    //    public int Start { get; set; }

    //    public int End { get; set; }

    //    public List<ShiftItem> Breaks { get; set; }
    //}

    public enum ShiftDateMode
    {
        ShiftEnd = 0,
        ShiftStart = 1
    }

    public static class eNETMachineProperty
    {
        public const string UDPCycleOn = "";
    }
}
