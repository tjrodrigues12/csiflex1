using CSIFLEX.Database.Access;
using CSIFLEX.eNetLibrary;
using CSIFLEX.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFLEX.Server.Library
{
    public static class BackupSettings
    {
        static string backupFolder = @"C:\CSIFLEX\BackupSettings";
        static List<string> AuthTables = new List<string>();
        static List<string> DatabaseTables = new List<string>();
        static List<string> MonitoringTables = new List<string>();

        static List<string> lines;

        public static void Backup(string mySqlPath = @"C:\Program Files (x86)\CSI Flex Server\mysql\mysql-5.7.21-win32\bin")
        {
            string servicePath = "";

            try
            {
                WindowsServices windowsServices = new WindowsServices();

                if (windowsServices.IsServiceInstalled("MYSQL"))
                    servicePath = windowsServices.GetServicePath("MYSQL");
                else if (windowsServices.IsServiceInstalled("MYSQLSERVICE"))
                    servicePath = windowsServices.GetServicePath("MYSQLSERVICE");
                else
                {
                    Log.Error("MySQL service doesn't found.");
                    return;
                }

                Log.Info($"MySQL Service Path: {servicePath}");

                if (servicePath.EndsWith(" MySQL"))
                    servicePath = servicePath.Substring(0, servicePath.Length - 6);

                Log.Info($"MySQL Service Path: {servicePath}");

                FileInfo fileInfo = new FileInfo(servicePath);
                mySqlPath = fileInfo.DirectoryName;

                Log.Info($"MySQL Path: {mySqlPath}");
            }
            catch (Exception ex)
            {
                Log.Error($"service Path: {servicePath}",ex);
            }

            if (!Directory.Exists(backupFolder))
                Directory.CreateDirectory(backupFolder);

            var task = Task.Run(() => RunBackup(mySqlPath));
        }

        private static async void RunBackup(string mySqlPath)
        {
            lines = new List<string>();

            try
            {
                var bkpAuthFile = BackupDatabase(mySqlPath, "csi_auth", MySqlAccess.AuthTablesBckp);

                var bkpDatabaseFile = BackupDatabase(mySqlPath, "csi_database", MySqlAccess.DatabaseTablesBckp);

                var directory = new DirectoryInfo(backupFolder);
                var lastAuthFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).FirstOrDefault(f => f.Name.StartsWith("csi_auth-") && f.Name != bkpAuthFile.Name);
                var lastDatabaseFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).FirstOrDefault(f => f.Name.StartsWith("csi_database-") && f.Name != bkpDatabaseFile.Name);

                string lastAuthHash = lastAuthFile.GetContentHash();
                string bkpAuthHash = bkpAuthFile.GetContentHash();

                if (lastAuthHash == bkpAuthHash)
                    File.Delete(bkpAuthFile.FullName);

                if (lastDatabaseFile.GetContentHash() == bkpDatabaseFile.GetContentHash())
                    File.Delete(bkpDatabaseFile.FullName);


                BackupEHubConf(directory);

                BackupShiftSetup(directory);

                BackupMonList(directory);

                BackupMonSetup(directory);

            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        public static FileInfo BackupDatabase(string mysqlPath, string database, string tables = "")
        {
            string execFile = Path.Combine(mysqlPath, "mysqldump.exe");
            string bkpFile = $"C:\\CSIFLEX\\BackupSettings\\{database}-{DateTime.Now.ToString("yyyy_MM_dd_HHmm")}.sql";
            try
            {
                ProcessStartInfo procInfo = new ProcessStartInfo();

                string arguments = $"-uroot -pCSIF1337 { database } {tables}";

                procInfo.FileName = execFile;
                procInfo.RedirectStandardInput = false;
                procInfo.RedirectStandardOutput = true;
                procInfo.Arguments = arguments;
                procInfo.UseShellExecute = false;
                procInfo.CreateNoWindow = true;
                procInfo.WindowStyle = ProcessWindowStyle.Hidden;
               
                Process proc = Process.Start(procInfo);

                List<string> procLines = proc.StandardOutput.ReadAllLines();
                string dumpEndLine = procLines.Find(l => l.StartsWith("-- Dump completed on"));
                procLines.Remove(dumpEndLine);

                File.AppendAllLines(bkpFile, procLines.Select(l => 
                {
                    if (l.StartsWith(") ENGINE=InnoDB AUTO_INCREMENT="))
                        return ") ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;";
                    else
                        return l;
                }) );

                proc.WaitForExit();

                return new FileInfo(bkpFile);
            }
            catch (Exception ex)
            {
                Log.Error($"ExecFile: {execFile}, bkpFile: {bkpFile}",ex);
            }

            return null;
        }

        public static void BackupTable(string mysqlPath, string database, string table)
        {
            string execFile = Path.Combine(mysqlPath, "mysqldump.exe");

            try
            {
                ProcessStartInfo procInfo = new ProcessStartInfo();

                //string arguments = $"-uroot -pCSIF1337 -hlocalhost { database } -t {table}";
                string arguments = $"-uroot -pCSIF1337 -hlocalhost { database } {table}";

                procInfo.FileName = execFile;
                procInfo.RedirectStandardInput = false;
                procInfo.RedirectStandardOutput = true;
                procInfo.Arguments = arguments;
                procInfo.UseShellExecute = false;
                procInfo.CreateNoWindow = true;
                procInfo.WindowStyle = ProcessWindowStyle.Hidden;

                Process proc = Process.Start(procInfo);

                List<string> procLines = proc.StandardOutput.ReadAllLines();

                foreach(string line in procLines)
                {
                    if (!line.Contains("Dump completed on"))
                        lines.Add(line.Replace("INSERT INTO ", $"REPLACE INTO `{database}`.").Replace("LOCK TABLES ", $"LOCK TABLES `{database}`."));
                }
                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }


        public static bool RestoreFile(string mysqlPath, string fileFullPath, string database)
        {
            if (!File.Exists(fileFullPath))
            {
                Log.Error($"Backup file { fileFullPath } not found.");
                return false;
            }

            try
            {
                ExecuteProcess(mysqlPath, $"cmd /c {mysqlPath}\\mysql.exe -uroot -pCSIF1337 { database } < \"{ fileFullPath }\"");
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            return true;
        }

        private static void ExecuteProcess(string path, string command)
        {
            try
            {
                ProcessStartInfo procInfo = new ProcessStartInfo();
                procInfo.FileName = "cmd";
                procInfo.Arguments = command;
                procInfo.WorkingDirectory = path;
                procInfo.RedirectStandardInput = false;
                procInfo.RedirectStandardOutput = true;
                procInfo.UseShellExecute = false;
                procInfo.CreateNoWindow = true;
                procInfo.WindowStyle = ProcessWindowStyle.Hidden;

                Process proc = Process.Start(procInfo);

                string res = proc.StandardOutput.ReadToEnd();

                using (StreamWriter file = new StreamWriter("ProcessLog.log", true))
                {
                    file.WriteLine(res);
                }
                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                Log.Error($"Error executing process { command }.", ex);
                throw ex;
            }
        }

        private static void BackupEHubConf(DirectoryInfo directory)
        {
            var ehubFile = new FileInfo(Path.Combine(eNetServer.EnetSetupFolder, "eHUBConf.sys"));
            var lastEhubFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).FirstOrDefault(f => f.Name.StartsWith("eNETeHUBConf-"));

            string hash1 = ehubFile.GetContentHash();
            string hash2 = lastEhubFile.GetContentHash();

            if (ehubFile.GetContentHash() != lastEhubFile.GetContentHash())
            {
                File.Copy(ehubFile.FullName, Path.Combine(backupFolder, $"eNETeHUBConf-{DateTime.Now.ToString("yyyy_MM_dd_HHmm")}.sys"));
            }
        }

        private static void BackupShiftSetup(DirectoryInfo directory)
        {
            var shiftFile = new FileInfo(Path.Combine(eNetServer.EnetSetupFolder, "ShiftSetup2.sys"));
            var lastShiftFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).FirstOrDefault(f => f.Name.StartsWith("ShiftSetup2-"));

            if (shiftFile.GetContentHash() != lastShiftFile.GetContentHash())
            {
                File.Copy(shiftFile.FullName, Path.Combine(backupFolder, $"ShiftSetup2-{DateTime.Now.ToString("yyyy_MM_dd_HHmm")}.sys"));
            }
        }

        private static void BackupMonList(DirectoryInfo directory)
        {
            var monListFile = new FileInfo(Path.Combine(eNetServer.EnetSetupFolder, "MonList.sys"));
            var lastMonListFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).FirstOrDefault(f => f.Name.StartsWith("MonList-"));

            if (monListFile.GetContentHash() != lastMonListFile.GetContentHash())
            {
                File.Copy(monListFile.FullName, Path.Combine(backupFolder, $"MonList-{DateTime.Now.ToString("yyyy_MM_dd_HHmm")}.sys"));
            }
        }

        private static void BackupMonSetup(DirectoryInfo directory)
        {
            var monSetupFile = new FileInfo(Path.Combine(eNetServer.EnetSetupFolder, "MonSetup.sys"));
            var lastMonSetupFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).FirstOrDefault(f => f.Name.StartsWith("eNETMonSetup-"));

            List<string> lines = new List<string>();

            using (StreamReader sr = new StreamReader(monSetupFile.FullName))
            {
                while (sr.Peek() > 0)
                {
                    string line = sr.ReadLine();

                    if (line.StartsWith("XX:"))
                        lines.Add("XX:");
                    else
                        lines.Add(line);
                }
            }

            string newFile = Path.Combine(backupFolder, $"eNETMonSetup-{DateTime.Now.ToString("yyyy_MM_dd_HHmm")}.sys");
            File.WriteAllLines(newFile, lines.ToArray());

            var newFileInfo = new FileInfo(newFile);

            if (newFileInfo.GetContentHash() == lastMonSetupFile.GetContentHash())
                File.Delete(newFile);
        }
    }
}
