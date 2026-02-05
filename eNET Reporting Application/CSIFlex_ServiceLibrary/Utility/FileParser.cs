using CSIFlex_ServiceLibrary.BLL;
using CSIFlex_ServiceLibrary.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFlex_ServiceLibrary.Utility
{
    public class FileParser
    {
        private static string _serverName = ConfigurationManager.AppSettings["SERVER_NAME"];

        public static List<MonSetupModel> getMonSetupData()
        {
            string monSetupFilePath = _serverName + @"c\_eNETDNC\_SETUP\MonSetup.sys";
            bool isFileUpdated = FileStatusBLL.isFileUpdated(monSetupFilePath);
            if (!isFileUpdated)
            {
                return null;
            }
            try
            {
                List<MonSetupModel> retList = new List<MonSetupModel>();
                string[] fileData = File.ReadAllLines(monSetupFilePath);//StringConstants.MON_SETUP_FILE_NAME);
                                                                        //int fileIndex = 165;
                int totalLengthMonSetup = fileData.Length;// fileIndex;
                int indexMonitoringId = 1;
                for (int i = 0; i < totalLengthMonSetup - 1; i = i + 165)
                {
                    try
                    {
                        string machine_filename = "";
                        string monitoring_id = fileData[i + indexMonitoringId].Replace(":", "");
                        string monitoring_on = fileData[i + (indexMonitoringId + 1)].Replace("ON:", "");
                        string[] monitoringIdList = monitoring_id.Split(',');
                        // Recomended by ERIK

                        machine_filename = "MonitorData" + (Convert.ToInt32(monitoringIdList[0]) - 1).ToString("x") + (Convert.ToInt32(monitoringIdList[1]) - 1) + ".SYS_";
                        //machine_filename = "MonitorData" + (Convert.ToInt32(monitoringIdList[0]) - 1) + (Convert.ToInt32(monitoringIdList[1]) - 1) + ".SYS_";
                        //Utility.WriteToFile(machine_filename);

                        string[] part_multiplier_mu = fileData[i + (indexMonitoringId + 44)].Replace("MU:", "").Split(',');
                        string cycle_identifier_ic = fileData[i + (indexMonitoringId + 42)].Replace("IC:", "");
                        string department_name_da = fileData[i + (indexMonitoringId + 63)].Replace("DA:", "");
                        string reset_counter_nm = fileData[i + (indexMonitoringId + 4)].Replace("NM:", "");
                        int ci_value = Convert.ToInt32(fileData[i + (indexMonitoringId + 45)].Replace("CI:", ""));
                        int ca_value = Convert.ToInt32(fileData[i + (indexMonitoringId + 46)].Replace("CA:", ""));
                        int mi_value = Convert.ToInt32(fileData[i + (indexMonitoringId + 43)].Replace("MI:", ""));
                        int min_value = ci_value;
                        int max_value = ca_value;
                        if (cycle_identifier_ic.Equals("1"))
                        {
                            int percentage = (ci_value * ca_value) / 100;
                            min_value = ci_value - percentage;
                            if (min_value < 0)
                                min_value = 0;
                            max_value = ci_value + percentage;
                        }

                        retList.Add(new MonSetupModel()
                        {
                            MonitoringId = monitoring_id,
                            MonStateOn = Convert.ToInt32(monitoring_on),
                            MachineTempFilename = machine_filename,
                            PartMultiplierMN = Convert.ToInt32(part_multiplier_mu[0]),
                            CyclIdentifierIC = Convert.ToInt32(cycle_identifier_ic),
                            DepartmentName = department_name_da,
                            MinIdealCI = min_value,
                            MaxPercCa = max_value,
                            ResetCounterNM = Convert.ToInt32(reset_counter_nm),

                        });
                    }
                    catch (Exception e)
                    {
                        string message = e.Message;
                        Utility.WriteToFile("Index: " + i + " | Message: " + message);
                    }
                }
                return retList;

            }
            catch (Exception e)
            {
                Utility.WriteToFile("StackTrace: " + e.Message);

            }
            return null;
        }

        public static List<ShiftSetupModel> getShiftSetup()
        {
            string shiftSetupFilePath = _serverName + StringConstants.SHIFT_SETUP_FILE_NAME;
            bool isFileUpdated = FileStatusBLL.isFileUpdated(shiftSetupFilePath);
            if (!isFileUpdated)
            {
                return null;
            }

            List<ShiftSetupModel> retList = new List<ShiftSetupModel>();
            string[] fileData = File.ReadAllLines(shiftSetupFilePath);
            List<string> splitInput = new List<string>();
            foreach (string line in fileData)
            {
                string[] parts = line.Split(',');
                foreach (string a1 in parts)
                {
                    if (!string.IsNullOrWhiteSpace(a1))
                    { splitInput.Add(a1); }

                }
            }
            int fileLength = splitInput.Count;
            string[] weekdays = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            int loc = 2, limit = 44, filenameinit = 1;
            ShiftSetupModel objShiftModel = null;
            while (loc < limit && loc < fileLength)
            {
                for (int days = 0; days < 7; days++) // 7 days a week
                {
                    for (int shifts = 1; shifts < 4; shifts++) // 3 shifts per day
                    {
                        objShiftModel = new ShiftSetupModel();
                        objShiftModel.DepartmentName = splitInput[filenameinit]; // Change as per logic
                        objShiftModel.DayName = weekdays[days];
                        objShiftModel.ShiftName = Convert.ToString(shifts);
                        objShiftModel.ShiftStart = Convert.ToInt32(splitInput[loc]);
                        objShiftModel.ShiftEnd = Convert.ToInt32(splitInput[loc + 1]);
                        objShiftModel.Break1Start = Convert.ToInt32(splitInput[loc + 42]);
                        objShiftModel.Break1End = Convert.ToInt32(splitInput[loc + 43]);
                        objShiftModel.Break2Start = Convert.ToInt32(splitInput[loc + 84]);
                        objShiftModel.Break2End = Convert.ToInt32(splitInput[loc + 85]);
                        objShiftModel.Break3Start = Convert.ToInt32(splitInput[loc + 126]);
                        objShiftModel.Break3End = Convert.ToInt32(splitInput[loc + 127]);
                        retList.Add(objShiftModel);
                        loc = loc + 2;
                    }
                }
                loc = loc + 127;
                limit = loc + 42;
                filenameinit = filenameinit + 169;
            }
            return retList;
        }

        public static List<EhubConfModel> getEHubConf()
        {
            string ehubConfFilePath = _serverName + StringConstants.EHUB_CONF_FILE_NAME;
            bool isFileUpdated = FileStatusBLL.isFileUpdated(ehubConfFilePath);
            if (!isFileUpdated)
            {
                return null;
            }

            List<EhubConfModel> retList = new List<EhubConfModel>();
            string[] fileData = File.ReadAllLines(ehubConfFilePath);
            string searchKeyword = "NM:";
            List<string> splitInput = new List<string>();
            foreach (string line in fileData)
            {
                if (line.Contains(searchKeyword))
                {
                    splitInput.Add(line.Replace("NM:", "")); // Name of the ENET Machine
                }
                else if (line.Contains("FI:"))
                {
                    splitInput.Add(line.Replace("FI:", "")); // Connection Type_ This value doesn't define all parameters but when it's 1 then it indicates FTP Connection
                }
            }
            int fileLength = splitInput.Count;
            int k = 1, l = 1, p = 0, q = 1;
            EhubConfModel objEhubConf = null;
            while (q < fileLength)
            {
                if (k <= 16 && l <= 8)
                {
                    objEhubConf = new EhubConfModel();
                    objEhubConf.MonitoringId = Convert.ToString(k) + "," + Convert.ToString(l);
                    objEhubConf.MonSetupId = 0;
                    objEhubConf.MachineName = splitInput[p];
                    if (Convert.ToInt32(splitInput[q]) == 1)
                    {
                        objEhubConf.Con_type = 5;
                    }
                    else
                    {
                        objEhubConf.Con_type = 0;
                    }
                    retList.Add(objEhubConf);
                    l++;
                    p += 2;
                    q += 2;
                    if (l > 8)
                    {
                        l = 1;
                        k++;
                    }
                }
            }
            return retList;
        }

        public static List<MonSetupModel> getMonitorDataFiles()
        {
            try
            {
                List<MonSetupModel> retList = new List<MonSetupModel>();
                //string[] fileData = File.ReadAllLines(_serverName + @"\eNETDNC\_TMP\" + monitorData);//StringConstants.MON_SETUP_FILE_NAME);
                string tempFileLoc = _serverName + @"c\_eNETDNC\_TMP\";

                var monitorDataFilesList = getMonitorDataFileList();
                MonSetupModel objMonSetup = null;
                foreach (var monitorItem in monitorDataFilesList)
                {
                    string monitorFilePath = tempFileLoc + monitorItem;
                    bool isFileUpdated = FileStatusBLL.isFileUpdated(monitorFilePath);
                    if (isFileUpdated)
                    {
                        objMonSetup = new MonSetupModel();
                        objMonSetup.MachineTempFilename = monitorItem;
                        string[] fileData = File.ReadAllLines(monitorFilePath);
                        objMonSetup.MachinePartNo = getPartNo(fileData);
                        foreach (var lineItem in fileData)
                        {
                            if (lineItem.Contains("_OPERATOR"))
                            {
                                string[] splitLineItem = lineItem.Split(',');
                                string _operator = splitLineItem[2].Replace("_OPERATOR:", "");
                                objMonSetup.MachinePartNo = objMonSetup.MachinePartNo + "<li>" + _operator;
                            }

                        }

                        string[] lastLineItem = fileData[fileData.Length - 1].Split(',');
                        //string currentStatus = lastLineItem[2];
                        objMonSetup.CurrentStatus = getCurrentStatus(fileData);
                        string last_updated_date = lastLineItem[0] + " " + lastLineItem[1];
                        //Utility.WriteToFile(last_updated_date);
                        DateTime date_last_updated = DateTime.ParseExact(last_updated_date, "MM/dd/yy HH:mm:ss", CultureInfo.InvariantCulture);//DateTime.Parse(datetime);


                        //if (Utility.CheckDate(last_updated_date))
                        //{
                        objMonSetup.LastUpdatedDate = date_last_updated;//Convert.ToDateTime(last_updated_date);
                        //}
                        retList.Add(objMonSetup);

                    }
                }

                return retList;
            }
            catch (Exception e)
            {
                Utility.WriteToFile(e.Message);
            }
            return null;
        }

        private static string getCurrentStatus(string[] data)
        {
            string result = "";
            for (int i = data.Length - 1; i >= 0; i--)
            {
                string item = data[i];
                if (!item.Contains("_PARTNO") && !item.Contains("_OPERATOR"))
                {
                    string status = item.Split(',')[2];
                    if (status.Contains("_CON:"))
                    {
                        return status.Split(':')[0];
                    }
                    return status;
                }
            }
            return result;
        }

        private static string getPartNo(string[] data)
        {
            string result = "";
            for (int i = data.Length - 1; i >= 0; i--)
            {
                string item = data[i];
                if (item.Contains("_PARTNO"))
                {
                    string[] splitLineItem = item.Split(',');
                    string partNo = splitLineItem[2].Replace("_PARTNO:", "");
                    return partNo;
                }
            }
            return result;
        }
        private static string[] getMonitorDataFileList()
        {
            string AllTempFileLoc = _serverName + @"c\_eNETDNC\_TMP\";
            DirectoryInfo d = new DirectoryInfo(AllTempFileLoc);     /*C:\_eNETDNC\_TMP*/
            FileInfo[] Files = d.GetFiles("*.SYS_"); //Getting SYS_ files
            string str = "";
            int count1 = 0;
            List<string> filename = new List<string>();
            string[] fileList = new string[Files.Length];
            foreach (FileInfo file in Files)
            {
                fileList[count1] = file.Name;
                count1++;
            }
            return fileList;
        }
    }
}
