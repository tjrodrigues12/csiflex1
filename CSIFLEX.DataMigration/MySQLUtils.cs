using CSIFLEX.Utilities;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CSIFLEX.DataMigration
{
    public static class MySQLUtils
    {
        public static void InstallMySQLFromZip()
        {
            //try
            //{
            //    string sourceFolder = Path.Combine(Environment.CurrentDirectory, "MySQLInstaller");
            //    string destination = Path.Combine(CSIFLEXConfig.AppFolder, "MySQL");

            //    FileInfo zipFile = GetLastFile(sourceFolder);

            //    ZipFileUtils.ExtractToDirectory(zipFile.FullName, destination);

            //    string binPath = Path.Combine(destination, "mysql-8.0.18-winx64", "bin");

            //    ExecuteProcess(binPath, Path.Combine(binPath, "mysqlstart.bat"), "");
            //}
            //catch (Exception ex)
            //{
            //    Log.Error(ex);
            //}
        }


        public static void GrantRootUser()
        {
            //string binPath = Path.Combine(CSIFLEXConfig.AppFolder, "MySQL", "mysql-8.0.18-winx64", "bin");

            //string grandCmdFile = Path.Combine(Path.GetTempPath(), "grandCmdFile.sql");
            //using (StreamWriter writer = new StreamWriter(grandCmdFile, false))
            //{
            //    writer.WriteLine($"ALTER  USER 'root'@'localhost' IDENTIFIED BY 'CSIF1337';");
            //    writer.WriteLine($"CREATE USER 'root'@'%'         IDENTIFIED BY 'CSIF1337';");
            //    writer.WriteLine($"GRANT ALL ON `db_csiflex`.*    TO 'root'@'%';");
            //    writer.Close();
            //}

            //try
            //{
            //    ExecuteProcess(binPath, $"cmd /c {binPath}\\mysql.exe -uroot db_csiflex < \"{ grandCmdFile }\"");
            //    File.Delete(grandCmdFile);
            //}
            //catch (Exception ex)
            //{
            //    Log.Error(ex);
            //}
        }


        public static void RemoveOldVersion(string path)
        {
            WindowsServices windowsServices = new WindowsServices();
            windowsServices.DeleteService("MYSQLSERVICE");
        }


        public static bool BackupDatabase(string mysqlPath, string database)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "DatabaseBackup");
            string execFile = Path.Combine(mysqlPath, "mysqldump.exe");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fileName = Path.Combine(path, $"{DateTime.Now.ToString("ddMMMyyyy_HHmm")}-{database}.sql");

            try
            {
                ProcessStartInfo procInfo = new ProcessStartInfo();

                string arguments = $"-uroot -pCSIF1337 -hlocalhost { database }";

                procInfo.FileName = execFile;
                procInfo.RedirectStandardInput = false;
                procInfo.RedirectStandardOutput = true;
                procInfo.Arguments = arguments;
                procInfo.UseShellExecute = false;

                Process proc = Process.Start(procInfo);

                string res = proc.StandardOutput.ReadToEnd().Replace("INSERT INTO", "INSERT IGNORE INTO");

                using (StreamWriter file = new StreamWriter(fileName))
                {
                    file.WriteLine(res);
                }
                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }

            return true;
        }


        public static bool BackupTable(string mysqlPath, string database, string table)
        {

            string path = Path.Combine(Directory.GetCurrentDirectory(), "DatabaseBackup");
            string execFile = Path.Combine(mysqlPath, "mysqldump.exe");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fileName = Path.Combine(path, $"{DateTime.Now.ToString("ddMMMyyyy_HHmm")}-{database}-{table}.sql");

            try
            {
                ProcessStartInfo procInfo = new ProcessStartInfo();

                string arguments = $"-uroot -pCSIF1337 -hlocalhost { database } -t {table}";

                procInfo.FileName = execFile;
                procInfo.RedirectStandardInput = false;
                procInfo.RedirectStandardOutput = true;
                procInfo.Arguments = arguments;
                procInfo.UseShellExecute = false;

                Process proc = Process.Start(procInfo);

                string res = proc.StandardOutput.ReadToEnd();

                if (!string.IsNullOrEmpty(res))
                {
                    using (StreamWriter file = new StreamWriter(fileName))
                    {
                        file.WriteLine(res.Replace("INSERT INTO", "INSERT IGNORE INTO"));
                    }
                    proc.WaitForExit();
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }

            return true;
        }


        public static void RestoreFromOldDatabase(string mysqlPath)
        {
            //RestoreDatabase(mysqlPath, Path.Combine(CSIFLEXConfig.AppFolder, "DatabaseBackup"), "csi_auth");
            //RestoreDatabase(mysqlPath, Path.Combine(CSIFLEXConfig.AppFolder, "DatabaseBackup"), "csi_database");

            //string grandCmdFile = Path.Combine(Path.GetTempPath(), "grandCmdFile.sql");
            //using (StreamWriter writer = new StreamWriter(grandCmdFile, false))
            //{
            //    writer.WriteLine($"GRANT ALL ON `csi_auth`.*     TO 'root'@'%';");
            //    writer.WriteLine($"GRANT ALL ON `csi_database`.* TO 'root'@'%';");
            //    writer.Close();
            //}

            //try
            //{
            //    ExecuteProcess(mysqlPath, $"cmd /c {mysqlPath}\\mysql.exe -uroot csi_auth < \"{ grandCmdFile }\"");
            //    ExecuteProcess(mysqlPath, $"cmd /c {mysqlPath}\\mysql.exe -uroot csi_database < \"{ grandCmdFile }\"");

            //    File.Delete(grandCmdFile);
            //}
            //catch (Exception ex)
            //{
            //    Log.Error(ex);
            //}
        }


        public static bool RestoreDatabase(string mysqlPath, string backupPath, string database)
        {
            string backFile = "";

            if (!Directory.Exists(backupPath))
            {
                Log.Error($"Backups folder { backupPath } not found.");
                return false;
            }

            backFile = Directory.GetFiles(backupPath).ToList<string>().Where(f => f.Contains(database)).OrderByDescending(f => f).FirstOrDefault();

            if (string.IsNullOrEmpty(backFile))
            {
                Log.Error($"Backup file not found. Database: {database}");
                return false;
            }

            try
            {
                ExecuteProcess(mysqlPath, Path.Combine(mysqlPath, "mysqladmin.exe"), $"-uroot create { database }");
                ExecuteProcess(mysqlPath, $"cmd /c {mysqlPath}\\mysql.exe -uroot { database } < \"{ backFile }\"");
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
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
                return false;
            }
            return true;
        }


        private static FileInfo GetLastFile(string path)
        {
            FileInfo lastFile = null;

            foreach (string file in Directory.GetFiles(path))
            {
                if (lastFile == null)
                {
                    lastFile = new FileInfo(file);
                }
                else
                {
                    FileInfo fileInfo = new FileInfo(file);
                    if (fileInfo.CreationTime > lastFile.CreationTime)
                    {
                        lastFile = fileInfo;
                    }
                }
            }
            return lastFile;
        }


        private static void ExecuteProcess(string path, string execName, string arguments)
        {
            string fileName = Path.Combine(path, execName);
            try
            {
                ProcessStartInfo procInfo = new ProcessStartInfo();
                procInfo.FileName = execName;
                procInfo.Arguments = arguments;
                procInfo.WorkingDirectory = path;
                procInfo.RedirectStandardInput = false;
                procInfo.RedirectStandardOutput = true;
                procInfo.UseShellExecute = false;

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
                Log.Error($"Error executing process { fileName }, arguments: { arguments }", ex);
                throw ex;
            }
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
    }
}
