using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFlex_DashboardService.Classes
{
    public class ReadFiles
    {
        private string serverName;
        private string serverProgramData;
        private string serverENETPath;
        private string TEMP = "temp.txt";
        private string EHUB_CONF = @"_SETUP\eHUBConf.sys";
        private string SHIFT_SETUP = @"_SETUP\ShiftSetup2.sys";
        private string MON_SETUP = @"_SETUP\MonSetup.sys";
        

        public ReadFiles()
        {
            serverName = ConfigurationManager.AppSettings["SERVER_NAME"];
            serverProgramData = serverName + ConfigurationManager.AppSettings["SERVER_PROGRAM_DATA"];
            serverENETPath = serverName + ConfigurationManager.AppSettings["SERVER_ENET_PATH"];
        }

        public ICollection<KeyValuePair<String, String>> getTempFileObject()
        {
            ICollection<KeyValuePair<String, String>> tmpfilelist = new Dictionary<String, String>();
            for (int i = 1; i <= 16; i++)
            {
                for (int j1 = 1; j1 <= 8; j1++)
                {
                    tmpfilelist.Add(new KeyValuePair<string, string>((i + "," + j1), "MonitorData" + (i - 1) + "" + (j1 - 1) + ".SYS_"));
                }
            }
            return tmpfilelist;
        }

        public List<string> getShiftSetup()
        {
            string file_Main = serverENETPath + SHIFT_SETUP;
            string[] textLines5 = File.ReadAllLines(file_Main);
            List<string> tempdetails1 = new List<string>();
            foreach (string line7 in textLines5)
            {
                string[] parts = line7.Split(',');
                foreach (string a1 in parts)
                {
                    if (!string.IsNullOrWhiteSpace(a1))
                    { tempdetails1.Add(a1); }

                }
            }            
            updateFileStatus(file_Main);
            return tempdetails1;
        }

        private void updateFileStatus(string file_Main)
        {
            FileStatus _fileStatus = new FileStatus();
            var file_hash = Utility.GetFileHash(file_Main);
            _fileStatus.updateFileStatus(file_Main, file_hash);
        }

        public bool isShiftSetupUpdated()
        {
            FileStatus _fileStatus = new FileStatus();
            string file_Main = serverENETPath + SHIFT_SETUP;
            Utility.WriteToFile(file_Main);
            return _fileStatus.isFileUpdated(file_Main);
        }

        public bool isEHubUpdated()
        {
            FileStatus _fileStatus = new FileStatus();
            string file_Main = serverENETPath + EHUB_CONF;            
            return _fileStatus.isFileUpdated(file_Main);
        }

        public bool isMonSetupUpdated()
        {
            FileStatus _fileStatus = new FileStatus();
            string file_Main = serverENETPath + MON_SETUP;
            return _fileStatus.isFileUpdated(file_Main);
        }

        public string[] getEHubConf()
        {
            string fileName = serverProgramData + TEMP;
            string searchKeyword = "NM:";//NM ----> represent Name of the machine
                                         // string fileName = "C:\\Users\\BDesai\\Desktop\\test.txt";
            string eHubFileLocation = serverENETPath + EHUB_CONF;
            string[] textLines = File.ReadAllLines(eHubFileLocation);  /*@"C:\_eNETDNC\_SETUP\eHUBConf.sys"*/
            List<string> results = new List<string>();

            foreach (string line in textLines)
            {
                if (line.Contains(searchKeyword))
                {
                    results.Add(line.Replace("NM:", ""));
                }
            }

            File.WriteAllLines(fileName, results);
            string[] lines = File.ReadLines(fileName).ToArray();
            updateFileStatus(eHubFileLocation);
            return lines;
        }

        public string[] getAllFilesFromDirectory()
        {
            string AllTempFileLoc = serverENETPath + "_TMP";
            DirectoryInfo d = new DirectoryInfo(AllTempFileLoc);     /*C:\_eNETDNC\_TMP*/
                                                                     //string fileName1 = CSI_Library.CSI_Library.serverRootPath + "\\sys\\templist.txt";        /*"C:\\Users\\BDesai\\Desktop\\templist.txt";*/
            string fileName1 = serverProgramData + "templist.txt";
            FileInfo[] Files = d.GetFiles("*.SYS_"); //Getting SYS_ files
            string str = "";
            int count1 = 0;
            List<string> filename = new List<string>();
            foreach (FileInfo file in Files)
            {
                str = str + Path.GetFileNameWithoutExtension(file.Name) + Environment.NewLine;
                count1++;
            }
            filename.Add(str);
            File.WriteAllLines(fileName1, filename);
            string[] filenames = File.ReadLines(fileName1).ToArray(); // Array to store all file names in _eNETDNC\_TMP\ folder
            return filenames;
        }

        public int getFileSize(string filenames)
        {
            var filesize = new System.IO.FileInfo(serverENETPath + @"_TMP\" + filenames + ".SYS_").Length; /*C:\\_eNETDNC\\_TMP\\" + filenames[filein] + ".SYS_"*/
            int filelength1 = Convert.ToInt32(filesize);
            return filelength1;
        }

        public List<string> getENETFilesWithDPrint(string filenames)
        {
            string[] textLines4 = File.ReadAllLines(serverENETPath + @"_TMP\" + filenames + ".SYS_");/*C:\_eNETDNC\_TMP\" + filenames[filein] + ".SYS_"*/
            List<string> tempdetails = new List<string>();
            foreach (string line6 in textLines4)
            {
                if (!(line6.Contains("_DPRINT_"))) // ignore _DPRINT_  lines in all .SYS_ files 
                {
                    tempdetails.Add(line6);
                }
            }
            return tempdetails;
        }

        public List<string> getENETFilesData(string fileName, string tempFile)
        {
            string[] textLines55 = File.ReadAllLines(tempFile);
            List<string> tempdetails12 = new List<string>();
            foreach (string line7 in textLines55)
            {
                string[] parts = line7.Split(',');
                foreach (string a1 in parts)
                {
                    if (!string.IsNullOrWhiteSpace(a1))
                    { tempdetails12.Add(a1); }

                }
            }
            File.WriteAllLines(tempFile, tempdetails12);
            string[] tempdetails1_final = File.ReadLines(tempFile).ToArray();
            int length = tempdetails1_final.Length;
            int p1 = 0;
            List<string> l1 = new List<string>();
            while (p1 < length)
            {
                if (tempdetails1_final[p1].Contains("_PARTNO"))
                {
                    l1.Add(tempdetails1_final[p1]);
                    p1 = p1 + 4;
                }
                else // this contains all status and _OPERATOR information
                {
                    l1.Add(tempdetails1_final[p1]);
                    p1++;
                }

            }
            File.WriteAllLines(tempFile, l1);
            return tempdetails12;
        }

        public List<string> getENETStatus(string fileName)
        {
            string[] tempdetails1_final = File.ReadLines(fileName).ToArray();
            int length = tempdetails1_final.Length;
            int p1 = 0;
            List<string> l1 = new List<string>();
            //while (p1 < length)
            for (int i = 0; i < length; i++)
            {
                if (tempdetails1_final[i].Contains("_PARTNO"))
                {
                    l1.Add(tempdetails1_final[i]);
                    p1 = p1 + 4;
                }
                else // this contains all status and _OPERATOR information
                {
                    l1.Add(tempdetails1_final[i]);
                    p1++;
                }

            }
            return l1;
        }

        public List<string> getMonSetupData()
        {
            string monSetupFilePath = serverENETPath + MON_SETUP;
            string searchKeyword2 = "ON:"; //ON----> represent machine status ON/OFF(0/1)
            string[] textLines2 = File.ReadAllLines(monSetupFilePath); /*C:\_eNETDNC\_SETUP\MonSetup.sys*/
            List<string> results2 = new List<string>();
            foreach (string line2 in textLines2)
            {
                if (line2.Contains(searchKeyword2))
                {
                    results2.Add(line2.Replace("ON:", ""));
                }
            }
            updateFileStatus(monSetupFilePath);
            return results2;
        }


    }
}
