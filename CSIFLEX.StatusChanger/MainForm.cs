using CSIFLEX.eNetLibrary;
using CSIFLEX.eNetLibrary.Data;
using CSIFLEX.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSIFLEX.StatusChanger
{
    public partial class MainForm : Form
    {
        string localIp;
        uint intAddress;
        bool runThread = false;
        string folder = "";
        string pwd = "";

        UdpClient listener = null;

        Thread thdUDPServer = null;

        Task udpTask = null;

        List<EnetCommand> enetCommands;

        public MainForm()
        {
            InitializeComponent();

            Log.Instance.Init("CSIFLEXStatusChanger");
        }

        ~MainForm()
        {
            runThread = false;
            thdUDPServer.Abort();
        }

        eNetMachineConfig machine;
        string machineName;

        private void MainForm_Load(object sender, EventArgs e)
        {
            cmbMachines.Items.Clear();
            cmbCommand.Items.Clear();

            string folder = @"C:\_eNETDNC";

            if (Directory.Exists(folder))
                txtFolder.Text = folder;

            var myIpAddresses = LocalMachineInformation.MyIpAddresses();

            cmbIpAddress.DataSource = myIpAddresses;
            cmbIpAddress.SelectedIndex = 0;
        }

        private void btnCheckFolder_Click(object sender, EventArgs e)
        {
            folder = txtFolder.Text;

            if (String.IsNullOrEmpty(folder))
            {
                return;
            }

            if (!Directory.Exists(folder))
            {
                folder = "";
                MessageBox.Show("Folder not found!");
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            try
            {
                eNetServer.Instance.Init(folder);
                pwd = eNetServer.Connections.FtpPassword;

                cmbIpAddress_SelectedIndexChanged(this, new EventArgs());

                //UDPConnection udp = new UDPConnection(eNetServer.Connections.UdpIp, eNetServer.Connections.UdpPort);
                //udp.Open();
                //udp.Send("REMOTE_VIEW," + intAddress.ToString());
                //udp.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            localIp = cmbIpAddress.Text;
            intAddress = Utilities.Utilities.IpAddressToUInt(localIp);

            //lblEnetIpAddress.Text = eNetServer.Connections.FtpIp;

            //cmbMachines.DataSource = eNetServer.MonitoredMachines;
            cmbMachines.DataSource = EnetAPI.LoadAllMachinesList(folder);

            cmbMachines.DisplayMember = "MachineName";
            cmbMachines.Enabled = true;

            this.Cursor = Cursors.Default;
        }

        private void cmbMachines_SelectedIndexChanged(object sender, EventArgs e)
        {
            machineName = cmbMachines.Text;

            if (String.IsNullOrEmpty(machineName))
                return;

            machine = eNetServer.Machines.FirstOrDefault(m => m.MachineName == machineName);

            if (machine == null)
                return;

            PrintCurrentStatus();

            enetCommands = new List<EnetCommand>();
            enetCommands.Add(new EnetCommand() { Display = $"udp-CYCLE COUNT ({machine.Cmd_UDPCYCNT})", Command = machine.Cmd_UDPCYCNT });
            enetCommands.Add(new EnetCommand() { Display = $"udp-OPERATOR ({machine.Cmd_UDPOPER})", Command = machine.Cmd_UDPOPER });
            enetCommands.Add(new EnetCommand() { Display = $"udp-PART NUMBER ({machine.Cmd_UDPPARTNO})", Command = machine.Cmd_UDPPARTNO });
            
            if (machine.IsCycleOnAllowed(lblMachineStatus.Text) && lblMachineStatus.Text != "CYCLE ON")
                enetCommands.Add(new EnetCommand() { Display = $"udp-CYCLE ON ({machine.Cmd_UDPCON})", Command = machine.Cmd_UDPCON });

            enetCommands.Add(new EnetCommand() { Display = $"ftp-PART NUMBER ({machine.Cmd_PARTNO})", Command = machine.Cmd_PARTNO });

            if (machine.IsCycleOnAllowed(lblMachineStatus.Text) && lblMachineStatus.Text != "CYCLE ON")
                enetCommands.Add(new EnetCommand() { Display = $"ftp-CYCLE ON ({machine.Cmd_CON})", Command = machine.Cmd_CON });

            enetCommands.Add(new EnetCommand() { Display = $"ftp-CYCLE OFF ({machine.Cmd_COFF})", Command = machine.Cmd_COFF });
            //enetCommands.Add(new EnetCommand() { Display = "PRODUCTION", Command = machine.Cmd_PROD });
            //enetCommands.Add(new EnetCommand() { Display = "SETUP", Command = machine.Cmd_SETUP });

            foreach (var item in machine.GetAllowedCommands(lblMachineStatus.Text))
            {
                enetCommands.Add(new EnetCommand() { Display = $"{item.Key} ({item.Value})", Command = item.Value });
            }

            cmbCommand.DataSource = enetCommands;
            cmbCommand.DisplayMember = "Display";
            cmbCommand.ValueMember = "Command";
            cmbCommand.Enabled = true;
            cmbCommand.SelectedIndex = -1;

            rbFTP.Enabled = true;
            rbUDP.Enabled = true;
            btnSendCommand.Enabled = true;
            btnRefreshStatus.Enabled = true;
            txtCommand.Enabled = true;
            lblEnetPos.Text = machine.EnetPos;

            btnProduction.Enabled = true;
            btnCycleOn.Enabled = true;
            btnCycleOff.Enabled = true;
            grpSetup.Enabled = true;

            cmbCommand.SelectedIndexChanged += CmbCommand_SelectedIndexChanged; 
        }

        private void CmbCommand_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCommand.Text = "";
            txtComment.Text = "";

            string command = cmbCommand.Text;

            if (String.IsNullOrEmpty(command))
                return;

            txtCommand.Text = cmbCommand.SelectedValue.ToString();

            if (command.StartsWith("udp"))
            {
                rbUDP.Checked = true;
                rbFTP.Checked = false;
            }
            else if (command.StartsWith("ftp"))
            {
                rbUDP.Checked = false;
                rbFTP.Checked = true;
            }
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cmbMachines.Text) || String.IsNullOrEmpty(txtCommand.Text))
                return;

            eNetMachineConfig machine = eNetServer.Machines.FirstOrDefault(m => m.MachineName == machineName);

            if (machine == null)
                return;

            string msgLog = "";
            string curStatus = "";

            string command = txtCommand.Text;
            if (!string.IsNullOrEmpty(txtComment.Text))
                command += $",{txtComment.Text}";

            if (rbUDP.Checked)
            {
                string machPos = machine.EnetPos.Split(',').Aggregate((x, y) => $"{int.Parse(x) - 1},{int.Parse(y) - 1}");

                try
                {
                    if (cmbCommand.Text.Contains("PART"))
                    {
                        string[] parts = txtComment.Text.Split(',');

                        string partValue = parts.Length > 0 ? parts[0] : "";
                        int cycleTime = parts.Length > 1 ? int.Parse(parts[1]) : 0;
                        int cycleMult = parts.Length > 2 ? int.Parse(parts[2]) : 0;

                        EnetAPI.SendPartNumToEnetOverUdp(localIp, eNetServer.Connections.UdpIp, eNetServer.Connections.UdpPort, machine.EnetPos, txtCommand.Text, partValue, cycleTime, cycleMult);
                    }
                    else
                    {
                        Log.Info($"SendStatusToEnetOverUdp - Local IP: {localIp}, UDP IP: {eNetServer.Connections.UdpIp}:{eNetServer.Connections.UdpPort}, Machine Pos: {machine.EnetPos}, Command: {command}.");

                        msgLog += $"UDP - Local IP: {localIp}, UDP IP: {eNetServer.Connections.UdpIp}:{eNetServer.Connections.UdpPort}, Machine Pos: {machine.EnetPos}, Command: {command}.{Environment.NewLine}";

                        EnetAPI.SendStatusToEnetOverUdp(localIp, eNetServer.Connections.UdpIp, eNetServer.Connections.UdpPort, machine.EnetPos, command);
                    }

                    msgLog += $"UDP - {machineName} - {cmbCommand.Text} - {command} - {localIp}{Environment.NewLine}";
                }
                catch (Exception ex)
                {
                    txtCommandsSend.AppendText($"ERROR-{ex.Message}{Environment.NewLine}");
                    Log.Error(ex);
                }
            }
            else if (rbFTP.Checked)
            {
                try
                { 
                    EnetAPI.SendCMDEnetFTP(eNetServer.Connections.FtpIp, eNetServer.Connections.FtpPassword, machine.FTPFileName, machineName, command, "_StatusChanger_");

                    msgLog = $"FTP - {machineName} - {pwd} - {cmbCommand.Text} - {command}{Environment.NewLine}";
                }
                catch (Exception ex)
                {
                    txtCommandsSend.AppendText($"ERROR-{ex.Message}{Environment.NewLine}");
                    Log.Error(ex);
                }
            }
            else
            {
                MachineEventChange eventChange = new MachineEventChange(machineName, cmbCommand.Text);

                IHttpEnetAPIClient httpClient = new HttpEnetAPIClient($"http://{cmbIpAddress.Text}:80/api/");
                httpClient.PutItem<MachineEventChange>("machine/event", eventChange);
            }

            Thread.Sleep(1000);
            curStatus = eNetServer.Instance.LastChange(machineName);

            txtCommandsSend.AppendText($"{new String('-', 170)}{Environment.NewLine}");
            txtCommandsSend.AppendText(msgLog);
            txtCommandsSend.AppendText($" = STATUS:  [ {curStatus} ]{Environment.NewLine}");
            txtCommandsSend.SelectionStart = txtCommandsSend.Text.Length;
            txtCommandsSend.ScrollToCaret();

            //lblMachineStatus.Text = eNetServer.Instance.CurrentStatus(machine.MachineName);
        }

        private void btnRefreshStatus_Click(object sender, EventArgs e)
        {
            PrintCurrentStatus();
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            txtCommandsSend.Clear();
        }

        private void PrintCurrentStatus()
        {
            lblMachineStatus.Text = eNetServer.Instance.CurrentStatus(machineName);
            txtCommandsSend.AppendText($"====================================================================================={Environment.NewLine}");
            txtCommandsSend.AppendText($"{machineName} - [ { eNetServer.Instance.LastChange(machineName) } ]{Environment.NewLine}");
            txtCommandsSend.SelectionStart = txtCommandsSend.Text.Length;
            txtCommandsSend.ScrollToCaret();
        }

        private async void ReadUdp()
        {
            string updIpAddress = eNetServer.Connections.UdpIp;
            int port = eNetServer.Connections.UdpPort;

            try
            {
                listener = new UdpClient(port);
                IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, port);

                string received_data;
                byte[] receive_byte_array;

                while (runThread)
                {

                    receive_byte_array = listener.Receive(ref groupEP);

                    received_data = Encoding.ASCII.GetString(receive_byte_array, 0, receive_byte_array.Length);

                    if (!received_data.StartsWith("_INI"))
                    {
                        string[] itens = received_data.Split(',');

                        DateTime dt = new DateTime(1970, 1, 1);

                        int seconds = int.Parse(itens[10]);

                        dt = dt.AddSeconds(seconds).ToLocalTime();
                        string msg = $"Machine: { itens[19] } - Status: {itens[18]} - Date: { dt.ToString("MM/dd/yy HH:mm:ss") }";

                        Invoke(new Action(() =>
                        {
                            txtUdpReturn.AppendText(msg + Environment.NewLine);
                            txtUdpReturn.SelectionStart = txtUdpReturn.Text.Length;
                            txtUdpReturn.ScrollToCaret();
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                runThread = false;
                listener.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void txtFolder_Leave(object sender, EventArgs e)
        {
            if (txtFolder.Text.ToUpper().StartsWith("DR"))
            {
                txtFolder.Text = @"\\10.0.10.189\C\_eNETDNC";
            }
        }

        private void btnStartUdpFeedback_Click(object sender, EventArgs e)
        {
            if (btnStartUdpFeedback.Text == "Stop")
            {
                try
                {
                    runThread = false;
                    listener.Close();
                    btnStartUdpFeedback.Text = "Start UDP Feedback";
                }
                catch(Exception ex) { }
            }
            else
            {
                runThread = true;

                udpTask = new Task(ReadUdp);
                udpTask.Start();
                btnStartUdpFeedback.Text = "Stop";
            }
        }

        private void cmbIpAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIpAddress.SelectedIndex < 0 || string.IsNullOrEmpty(folder))
                return;
        }

        private void btnLoadIP_Click(object sender, EventArgs e)
        {
            try
            {
                localIp = cmbIpAddress.Text;
                intAddress = Utilities.Utilities.IpAddressToUInt(localIp);

                UDPConnection udp = new UDPConnection(eNetServer.Connections.UdpIp, eNetServer.Connections.UdpPort);
                udp.Open();
                udp.Send("REMOTE_VIEW," + intAddress.ToString());
                udp.Close();

                txtCommandsSend.AppendText($"REMOTE_VIEW,{intAddress.ToString()} - {localIp}-{intAddress}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnProduction_Click(object sender, EventArgs e)
        {
            if (machine == null)
                return;

            SendCommand(machine.Cmd_PROD);
        }

        private void btnCycleOn_Click(object sender, EventArgs e)
        {
            if (machine == null)
                return;

            SendCommand(machine.Cmd_CON);
        }

        private void btnCycleOff_Click(object sender, EventArgs e)
        {
            if (machine == null)
                return;

            SendCommand(machine.Cmd_COFF);
        }

        private void btnSetup_Click(object sender, EventArgs e)
        {
            if (machine == null)
                return;

            SendCommand(machine.Cmd_SETUP);
        }

        private void btnPartNumber_Click(object sender, EventArgs e)
        {
            string partValue = txtPartNumber.Text;
            string command = machine.Cmd_UDPPARTNO;
            int cycleTime = 0;
            int cycleMult = 1;

            if (string.IsNullOrEmpty(partValue))
                return;

            int.TryParse(txtCycleTime.Text, out cycleTime);
            int.TryParse(txtCycleMultiplier.Text, out cycleMult);

            EnetAPI.SendPartNumToEnetOverUdp(localIp, 
                                             eNetServer.Connections.UdpIp, 
                                             eNetServer.Connections.UdpPort, 
                                             machine.EnetPos, 
                                             command, 
                                             partValue, 
                                             cycleTime, 
                                             cycleMult);

            Thread.Sleep(1000);
            string curStatus = eNetServer.Instance.CurrentStatus(machineName);

            txtCommandsSend.AppendText($"{new String('-', 170)}{Environment.NewLine}");
            txtCommandsSend.AppendText($"MACHINE - {machineName}{Environment.NewLine}");
            txtCommandsSend.AppendText($"FTP CMD - {command}{Environment.NewLine}");
            txtCommandsSend.AppendText($"CHANGE  - { eNetServer.Instance.LastChange(machineName) }{Environment.NewLine}");
            txtCommandsSend.AppendText($"STATUS  - {curStatus}{Environment.NewLine}");
            txtCommandsSend.SelectionStart = txtCommandsSend.Text.Length;
            txtCommandsSend.ScrollToCaret();

            lblMachineStatus.Text = curStatus;
        }

        private void SendCommand(string command)
        {
            try
            {
                EnetAPI.SendCMDEnetFTP(eNetServer.Connections.FtpIp, eNetServer.Connections.FtpPassword, machine.FTPFileName, machine.MachineName, command, "_StatusChanger_");

                Thread.Sleep(1000);
                string curStatus = eNetServer.Instance.CurrentStatus(machine.MachineName);

                txtCommandsSend.AppendText($"{new String('-', 170)}{Environment.NewLine}");
                txtCommandsSend.AppendText($"MACHINE - {machineName}{Environment.NewLine}");
                txtCommandsSend.AppendText($"FTP CMD - {command}{Environment.NewLine}");
                txtCommandsSend.AppendText($"CHANGE  - { eNetServer.Instance.LastChange(machineName) }{Environment.NewLine}");
                txtCommandsSend.AppendText($"STATUS  - {curStatus}{Environment.NewLine}");
                txtCommandsSend.SelectionStart = txtCommandsSend.Text.Length;
                txtCommandsSend.ScrollToCaret();

                lblMachineStatus.Text = curStatus;
            }
            catch (Exception ex)
            {
                txtCommandsSend.AppendText($"ERROR-{ex.Message}{Environment.NewLine}");
                Log.Error(ex);
            }
        }
    }

    public class EnetCommand
    {
        public string Command { get; set; }

        public string Display { get; set; }
    }

    public class UdpInfo
    {
        public string IpAddress { get; set; }

        public int Port { get; set; }

        public string Command { get; set; }
    }
}
