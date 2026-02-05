using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSIFLEX.Check.Connections
{
    public partial class Connections : Form
    {
        public Connections()
        {
            InitializeComponent();
        }

        private void Connections_Load(object sender, EventArgs e)
        {
            //cmbIpAddress.DataSource = MyIpAddresses();
        }


        private void btnCheck_Click(object sender, EventArgs e)
        {
            string ip = txtIpAddress.Text.ToString();
            string folder = @"C:\_eNETDNC";

            lblEnetFolder.Text = folder;
            if (checkeNetFolder(folder))
            {
                lblEnetFolderCheck.Text = "Passed";
                lblEnetFolderCheck.ForeColor = Color.Green;
            }
            else
            {
                lblEnetFolderCheck.Text = "Unavailable";
                lblEnetFolderCheck.ForeColor = Color.Red;
            }

            if ( IsPortOpen(ip, 80, new TimeSpan(100000)) )
            {
                lblEnetHttp.Text = "Passed";
                lblEnetHttp.ForeColor = Color.Green;
            }
            else
            {
                lblEnetHttp.Text = "Unavailable";
                lblEnetHttp.ForeColor = Color.Red;
            }

            if (hasFtpConnection($"ftp://{ip}", "Drausio", "mycsipwd01"))
            {
                lblEnetFtp.Text = "Passed";
                lblEnetFtp.ForeColor = Color.Green;
            }
            else
            {
                lblEnetFtp.Text = "Unavailable";
                lblEnetFtp.ForeColor = Color.Red;
            }

            if (IsPortOpen(ip, 8008, new TimeSpan(100000)))
            {
                lblDashboard.Text = "Passed";
                lblDashboard.ForeColor = Color.Green;
            }
            else
            {
                lblDashboard.Text = "Unavailable";
                lblDashboard.ForeColor = Color.Red;
            }
        }

        private static bool checkeNetFolder(string folder)
        {
            if (!Directory.Exists(folder))
            {
                return false;
            }

            return true;
        }


        private static string[] MyIpAddresses()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            var ipAddress = host.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork).Select(ip => ip.ToString()).ToArray();

            return ipAddress;
        }


        private bool IsPortOpen(string host, int port, TimeSpan timeout)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    var result = client.BeginConnect(host, port, null, null);
                    var success = result.AsyncWaitHandle.WaitOne(timeout);
                    if (!success)
                    {
                        return false;
                    }

                    client.EndConnect(result);
                }

            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool hasFtpConnection(string url, string user, string password)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(user, password);
                request.GetResponse();
            }
            catch (WebException ex)
            {
                return false;
            }
            return true;
        }
    }
}
