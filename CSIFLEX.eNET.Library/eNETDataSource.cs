using CSIFLEX.eNET.Library.Data;
using CSIFLEX.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CSIFLEX.eNET.Library
{
    public static class eNETDataSource
    {
        public static List<CsvMachineModel> GetEnetHistory(int year = 0)
        {
            List<CsvMachineModel> lstStatus = new List<CsvMachineModel>();

            string file = "";
            int qtt = 0;
            int lineIdx = 0;
            try
            {
                if (year == 0) year = DateTime.Today.Year;


                file = Path.Combine(eNETServer.EnetReportsFolder, $"_MACHINE_{year}.CSV");

                if (!File.Exists(file))
                    return lstStatus;

                var lines = FileUtils.AllLinesFromTxtFile(file);

                if (lines[0].StartsWith("MACHINE NAME,"))
                {
                    lines = lines.Skip(1).ToList();
                }

                qtt = lines.Count;

                foreach (string line in lines)
                {
                    lineIdx++;

                    var fields = line.Split(',');

                    try
                    {
                        lstStatus.Add(new CsvMachineModel()
                        {
                            MachineName = fields[0],
                            Date = Convert.ToDateTime(fields[1]),
                            Shift = int.Parse(fields[2]),
                            StartTime = Convert.ToDateTime(fields[1]) + TimeSpan.Parse(fields[3]),
                            EndTime = Convert.ToDateTime(fields[1]) + TimeSpan.Parse(fields[4]),
                            ElapsedTime = int.Parse(fields[5]),
                            MachineStatus = fields[6],
                            HeadPallet = fields[7],
                            Comment = fields[8]
                        });
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"Error reading line {lineIdx}", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }

            Log.Info($" ====>  File {file} loaded. # lines: {lineIdx}/{qtt} =========================================> ");

            return lstStatus;
        }

        public static CsvMachineModel GetCurrentMachineStatus(string machineName)
        {
            var machine = eNETServer
                 .Machines
                 .Where(m => m.MachineName.Equals(machineName))
                 .FirstOrDefault();

            if (machine == null)
            {
                throw new InvalidOperationException("Given machine name has returned no value in the eNet server");
            }

            return machine.ToCsvMachineModel();
        }

        public static List<CsvMachineModel> GetCurrentMachinesStatus()
        {
            List<CsvMachineModel> lstStatus = new List<CsvMachineModel>();

            List<eNETMachine> machines = eNETServer.MonitoredMachines;

            string file = "";
            string pos = "";

            foreach (eNETMachine machine in machines)
            {
                try
                {

                    lstStatus.Add(machine.ToCsvMachineModel());
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }
            return lstStatus;
        }

        public static List<CsvMachineModel> GetTempStatusChanges()
        {
            List<CsvMachineModel> lstStatus = new List<CsvMachineModel>();

            List<eNETMachine> machines = eNETServer.MonitoredMachines;

            string file = "";
            string pos = "";

            foreach (eNETMachine machine in machines)
            {
                try
                {
                    string[] enetPos = machine.EnetPos.Split(',');
                    pos = $"{ int.Parse(enetPos[0]) - 1 }{ int.Parse(enetPos[1]) - 1 }";
                    file = Path.Combine(eNETServer.EnetTempFolder, $"MonitorData{pos}.SYS");

                    string[] lines = File.ReadAllLines(file);

                    foreach (string line in lines)
                    {
                        string[] fields = line.Split(',');
                        DateTime date = DateTime.ParseExact($"{ fields[0] } { fields[1] }", "MM/dd/yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        TimeSpan diff = DateTime.Now - date;

                        lstStatus.Add(new CsvMachineModel()
                        {
                            MachineName = machine.MachineName,
                            MachineStatus = fields[2],
                            Date = date,
                            StartTime = date,
                            ElapsedTime = diff.Seconds
                        });
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }
            return lstStatus;
        }

        public static List<EnetTempFileModel> GetTempFiles()
        {
            List<EnetTempFileModel> fullLstStatus = new List<EnetTempFileModel>();
            List<EnetTempFileModel> tmpLstStatus;
            List<EnetTempFileModel> mchLstStatus;

            List<eNETMachine> machines = eNETServer.MonitoredMachines;

            string tmpFile = "";
            string pos = "";
            string errorPoint = "";

            string[] momFiles = Directory.GetFiles(Path.Combine(eNETServer.EnetMonitoringFolder, DateTime.Today.ToString("yyyy-MM")));

            foreach (eNETMachine machine in machines)
            {
                try
                {
                    DateTime searchDate = DateTime.Today;
                    mchLstStatus = new List<EnetTempFileModel>();
                    tmpLstStatus = new List<EnetTempFileModel>();

                    int shift = 0;

                    string[] enetPos = machine.EnetPos.Split(',');
                    pos = $"{ (int.Parse(enetPos[0]) - 1).ToString("x") }{ int.Parse(enetPos[1]) - 1 }";

                    errorPoint = $"pos {pos}";

                    tmpFile = Path.Combine(eNETServer.EnetTempFolder, $"MonitorData{pos}.SYS_");
                    if (!File.Exists(tmpFile))
                        tmpFile = Path.Combine(eNETServer.EnetTempFolder, $"MonitorData{pos}.SYS");

                    Log.Debug($"Machine: {machine.MachineName}, Pos: {pos}, Shift: {shift}, Temp file: {tmpFile}");

                    List<EnetTempFileModel> tempFile = GetTempFile(machine.MachineName, shift, tmpFile);

                    Log.Debug($"Machine: {machine.MachineName}, Temp file size: {tempFile.Count}");

                    tmpLstStatus.AddRange(tempFile);

                    if (tmpLstStatus.Count > 0)
                    {
                        if (tmpLstStatus[0].DateTime.Date < DateTime.Today.Date)
                            searchDate = searchDate.AddDays(-1);

                        string[] machineMomFiles = momFiles
                            .Where(f => {
                                FileInfo fi = new FileInfo(f);
                                return fi.Name.StartsWith(searchDate.ToString("MMMdd")) && fi.Name.Contains($"_{machine.MachineName}_");
                            } )
                            .OrderBy(f => f).ToArray();

                        Log.Debug($"Machine: {machine.MachineName} - Mom files: {machineMomFiles.Length}");

                        foreach (string momFile in machineMomFiles)
                        {
                            shift = int.Parse(momFile.Substring(momFile.IndexOf("SHIFT") + 5, 1));

                            mchLstStatus.AddRange(GetTempFile(machine.MachineName, shift, momFile));
                        }
                        shift++;

                        mchLstStatus.AddRange(tmpLstStatus);

                    //Calculate the CycleTime for each record
                    int idx = 0;
                    foreach(EnetTempFileModel mchStatus in mchLstStatus)
                    {
                        if (idx > 0)
                        {
                            mchLstStatus[idx - 1].CycleTime = (int)mchLstStatus[idx].DateTime.TimeOfDay.TotalSeconds - (int)mchLstStatus[idx - 1].DateTime.TimeOfDay.TotalSeconds;

                            if (mchLstStatus[idx - 1].CycleTime < 0)
                                mchLstStatus[idx - 1].CycleTime += 86400;
                        }

                        if (mchStatus.Shift == 0)
                            mchStatus.Shift = shift;

                        idx++;
                    }
                    mchLstStatus[idx - 1].CycleTime = (int)DateTime.Now.TimeOfDay.TotalSeconds - (int)mchLstStatus[idx - 1].DateTime.TimeOfDay.TotalSeconds;
                    if (mchLstStatus[idx - 1].CycleTime < 0)
                        mchLstStatus[idx - 1].CycleTime += 86400;

                    fullLstStatus.AddRange(mchLstStatus);

                    }
                }
                catch (Exception ex)
                {
                    Log.Error(errorPoint, ex);
                }
            }
            return fullLstStatus;
        }

        public static List<EnetTempFileModel> GetTempFile(string machine, int shift, string file)
        {
            List<EnetTempFileModel> lstStatus = new List<EnetTempFileModel>();

            try
            {
                var lines = FileUtils.AllLinesFromTxtFile(file).ToList();

                int idx = 0;

                int recordSecs = 0;
                int adjustedSecs = 0;
                int lastRecordSecs = 0;

                string status = "";
                string comments = "";
                string lastStatus = "";

                foreach (string line in lines)
                {
                    bool inTimeline = true;
                    string[] fields = line.Split(',');
                    DateTime date = DateTime.ParseExact($"{ fields[0] } { fields[1] }", "MM/dd/yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

                    recordSecs = (int)date.TimeOfDay.TotalSeconds;

                    if (recordSecs >= adjustedSecs)
                    {
                        adjustedSecs = recordSecs;
                    }
                    else
                    {
                        adjustedSecs = recordSecs + 86400;
                    }

                    comments = "";
                    if (fields.Count() > 3) comments = fields[3];
                    if (fields.Count() > 4) comments += $",{fields[4]}";
                    if (fields.Count() > 5) comments += $",{fields[5]}";

                    status = fields[2];
                    if (status.Contains(':'))
                    {
                        comments = $"{status.Split(':')[1]},{comments}";
                        status = status.Split(':')[0];
                    }

                    if (status != lastStatus)
                    {
                        if (comments.Contains("- UNLOCKED"))
                        {
                            lstStatus[idx - 1].Status = status;

                            if (idx >= 2 && lstStatus[idx - 2].Status == "COFF")
                            {
                                lstStatus[idx - 2].Status = status;
                                lstStatus.Remove(lstStatus[idx - 1]);
                            }
                        }
                        else if (!status.StartsWith("_PARTNO") && !status.StartsWith("_DPRINT_") && !status.StartsWith("_OPERATOR") && !status.StartsWith("_SH_") && !status.StartsWith("_JOBNUMBER"))
                        {
                            lstStatus.Add(new EnetTempFileModel()
                            {
                                MachineName = machine,
                                TimeId = recordSecs,
                                Status = status,
                                DateTime = date,
                                CycleTime = 0,
                                Shift = shift,
                                Comment = comments,
                                InTimeline = inTimeline,
                                Line = line
                            });
                            lastStatus = status;
                            lastRecordSecs = adjustedSecs;
                            idx++;
                        }
                    }
                }
                return lstStatus;
            }
            catch (Exception ex) {
                Log.Error(ex);
                throw new Exception($"eNETDataSource.GetTempFile:: Error while try to read the file {file}", ex);
            }
        }
    }


    public class EnetTempFileModel
    {
        public string MachineName { get; set; }

        public DateTime DateTime { get; set; }

        public int TimeId { get; set; }

        public string Status { get; set; }

        public int CycleTime { get; set; }

        public int Shift { get; set; }

        public string Comment { get; set; }

        public bool InTimeline { get; set; }

        public string Line { get; set; }
    }


    public class CsvMachineModel
    {
        public string MachineName { get; set; }

        public DateTime Date { get; set; }

        public int Shift { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int ElapsedTime { get; set; }

        public string MachineStatus { get; set; }

        public string HeadPallet { get; set; }

        public string Comment { get; set; }
    }
}
