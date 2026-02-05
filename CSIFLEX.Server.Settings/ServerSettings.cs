using CSIFLEX.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSIFLEX.Server.Settings
{
    public class ServerSettings
    {
        #region singleton 
        private ServerSettings() { }

        private static ServerSettings _instance = null;

        public static ServerSettings Instance
        {
            get {
                if (_instance == null)
                    _instance = new ServerSettings();
                return _instance;
            }
        }
        #endregion


        private static string[] firstDayName = { "Sunday", "Monday" };


        private static bool settingsLoaded_ = false;
        public static bool SettingsLoaded
        {
            get {
                return settingsLoaded_;
            }
        }

        public static string SettingsPath { get; set; }

        public static string CompanyName { get; set; }

        private static string serverIPAddress_;
        public static string ServerIPAddress
        {
            get
            {
                return serverIPAddress_;
            }
            set
            {
                serverIPAddress_ = value;
                SetFileContent("srv_ip_.csys", serverIPAddress_);
            }
        }

        private static string enetFolder_;
        public static string EnetFolder
        {
            get
            {
                return enetFolder_;
            }
            set
            {
                enetFolder_ = value;
                SetFileContent("setup_.csys", enetFolder_);
            }
        }

        private static string enetIPAddress_;
        public static string EnetIPAddress
        {
            get
            {
                return enetIPAddress_;
            }
            set
            {
                enetIPAddress_ = value;
                SetFileContent("Networkenet_.csys", enetIPAddress_);
            }
        }

        private static string rmPort_;
        public static string RMPort
        {
            get
            {
                return rmPort_;
            }
            set
            {
                rmPort_ = value;
                SetFileContent("RM_port_.csys", rmPort_);
            }
        }

        private static int firstDayOfWeek_;
        public static int FirstDayOfWeek
        {
            get {
                return firstDayOfWeek_;
            }
            set {
                firstDayOfWeek_ = value;
                SetFileContent("firstDayOfWeek.csys", $"{firstDayOfWeek_};{firstDayName[firstDayOfWeek_]}");
            }
        }

        private static List<string> loginInfo_;
        public static string[] LoginInfo
        {
            get {
                return loginInfo_.ToArray();
            }
        }

        //public static string ClientConnString
        //{
        //    get
        //    {
        //        return $"SERVER={ db_authPath };{ MySqlServerBaseString }";
        //    }
        //}


        public void Init(string settingsPath)
        {
            SettingsPath = settingsPath;

            serverIPAddress_ = GetFileContent("srv_ip_.csys");

            enetFolder_ = GetFileContent("setup_.csys");

            enetIPAddress_ = GetFileContent("Networkenet_.csys");

            rmPort_ = GetFileContent("RM_port_.csys");

            if (string.IsNullOrEmpty(rmPort_)) rmPort_ = "8008";

            LoadFirstDayOfWeek();

            LoadLoginInfo();

            settingsLoaded_ = true;
        }


        private void LoadFirstDayOfWeek()
        {
            string filePath = Path.Combine(SettingsPath, "firstDayOfWeek.csys");

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string[] line = reader.ReadLine().Split(';');
                    firstDayOfWeek_ = int.Parse(line[0]);
                }
            }
            else
            {
                firstDayOfWeek_ = 0;
                SetFileContent("firstDayOfWeek.csys", $"{firstDayOfWeek_};{firstDayName[firstDayOfWeek_]}");
            }
        }


        private void LoadLoginInfo()
        {
            string filePath = Path.Combine(SettingsPath, "logininfo.csys");

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    loginInfo_ = reader.ReadAllLines();
                }
            }
        }


        private static string GetFileContent(string fileName)
        {
            fileName = Path.Combine(SettingsPath, fileName);

            if (File.Exists(fileName))
                return File.ReadLines(fileName).FirstOrDefault();
            else
                return "";
        }


        private static void SetFileContent(string fileName, string content)
        {
            string filePath = Path.Combine(SettingsPath, fileName);

            File.WriteAllText(filePath, content);
        }


        public static void AddLoginInfo(string loginInfo)
        {
            string filePath = Path.Combine(SettingsPath, "logininfo.csys");

            string[] info = loginInfo.Split(',');
            string line = loginInfo_.FirstOrDefault(l => l.StartsWith(info[0]));

            if (line != null)
                line = loginInfo;
            else
                loginInfo_.Add(loginInfo);

            File.WriteAllLines(filePath, loginInfo_.ToArray());
        }
    }
}
