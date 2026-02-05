using CSIFLEX.eNetLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFLEX.eNET.Library.Data
{
    public class eNETMachine
    {
        private string _sendfolder;
        private string _receivefolder;

        public eNETMachine(string machname, string machpos, bool two_heads, string sendfolder, string receivefolder)
        {
            MachineName = machname;
            EnetPos = machpos;
            TwoHeads = two_heads;
            _sendfolder = sendfolder;
            _receivefolder = receivefolder;
            Cmd_Others = new Dictionary<string, string>();
        }

        public eNETMachine()
        {
            Cmd_Others = new Dictionary<string, string>();
        }

        public bool IsSerial { get { return Protocol == "eHUB" || Protocol == "xlConnect" || Protocol == "xlHUB"; } }

        public bool IsFTP { get { return Protocol == "FTP" || Protocol == "CAPS"; } }

        public string MachineName { get; private set; }

        public string EnetPos { get; private set; }

        public int SequencePos {
            get
            {
                int pos = 0;
                string[] enetPos = EnetPos.Split(',');

                int grp1 = int.Parse(enetPos[0]);
                int grp2 = int.Parse(enetPos[1]);

                pos = (grp1 - 1) * 8 + grp2;

                return pos;
            }
        }

        public string MachineDbTable { get; set; }

        public string StatusTempFile { get; set; }

        public string Protocol { get; set; }

        public bool TwoHeads { get; set; }

        public bool TwoPallets { get; set; }

        public bool IsMonitored { get; set; }

        public bool IsInMonList { get; set; }

        public bool UseDPrint { get; set; }

        public bool UseMonitoringBoard { get; set; }

        public string FTPFileName { get; set; }

        public string RedirectDPrint { get; set; }

        public string Department { get; set; }

        public string Cmd_CON { get; set; }

        public string Cmd_COFF { get; set; }

        public string Cmd_CON2 { get; set; }

        public string Cmd_COFF2 { get; set; }

        public string Cmd_PROD { get; set; }

        public string Cmd_SETUP { get; set; }

        public string Cmd_PARTNO { get; set; }

        public string Cmd_PARTNO2 { get; set; }

        public string Cmd_UDPCON { get; set; }

        public string Cmd_UDPPARTNO { get; set; }

        public string Cmd_UDPOPER { get; set; }

        public string Cmd_UDPCYCNT { get; set; }

        public string StatusCommand { get; set; }

        public Dictionary<string, string> Cmd_Others { get; set; }

        public Dictionary<string, eNetMachineTermination> Cmd_OthersTermination { get; set; }

        public bool SECanTerminateTE { get; set; }

        public bool IsCycleOnAllowed(string currentStatus)
        {
            string termination = Cmd_OthersTermination.ContainsKey(currentStatus) ? Cmd_OthersTermination[currentStatus].Termination : "";

            return !termination.Contains("&");
        }

        public Dictionary<string, string> GetAllowedCommands(string currentStatus = "")
        {
            Dictionary<string, string> commands = new Dictionary<string, string>();

            string termination = Cmd_OthersTermination.ContainsKey(currentStatus) ? Cmd_OthersTermination[currentStatus].Termination : "";

            foreach(KeyValuePair<string, eNetMachineTermination> command in Cmd_OthersTermination)
            {
                int id = command.Value.Id;

                if ((termination == "" || id == 10 || termination.Contains(id.ToString())) && currentStatus != command.Key)
                    commands.Add(command.Key, command.Value.Command);
            }

            return commands;
        }

        public string GetCommand(string newStatus, string currentStatus = "")
        {
            var termination = !String.IsNullOrEmpty(currentStatus) && Cmd_OthersTermination.ContainsKey(currentStatus) ? Cmd_OthersTermination[currentStatus].Termination : "";

            if (newStatus == "CYCLE ON" && termination.Contains("&"))
                return "";

            var statusId = Cmd_OthersTermination.ContainsKey(newStatus) ? Cmd_OthersTermination[newStatus].Id : 10;

            if (statusId == 10 || termination == "" || termination.Contains(statusId.ToString()))
            {
                switch (newStatus)
                {
                    case "CYCLE ON":
                        return Cmd_CON;
                    case "CYCLE OFF":
                        return Cmd_COFF;
                    case "SETUP":
                        return Cmd_SETUP;
                    case "PRODUCTION":
                        return Cmd_PROD;
                    default:
                        string cmd = "";

                        foreach (var item in Cmd_Others)
                        {
                            if (item.Key == newStatus)
                                cmd = item.Value;
                        }

                        return cmd;
                }
            }

            return "";
        }
    }
}
