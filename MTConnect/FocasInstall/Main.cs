using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//run exe
using System.Diagnostics;

//file copy
using System.IO;

namespace FocasInstall
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void BTN_InstallAsService_Click(object sender, EventArgs e)
        {
            if (CB_Controllers.SelectedIndex >= 0 && TB_Path.Text.Length>0)
            {
                string adaptername = "";
                switch (CB_Controllers.Items[CB_Controllers.SelectedIndex].ToString())
                {
                    case "0i":
                        adaptername = "fanuc_0i.exe";
                        break;
                    case "15i":
                        adaptername = "fanuc_15i.exe";
                        break;
                    case "30i":
                        adaptername = "fanuc_30i.exe";
                        break;

                }

                InstallAdapter(adaptername);
            }
        }

        private void InstallAdapter(string adaptername)
        {
            // For the example
            //const string ex1 = "C:\\";
            //const string ex2 = "C:\\Dir";

            string source_folder = @"adapters\";
            string dest_folder = TB_Path.Text + @"\adapters\";
            //copy the file
            if (!Directory.Exists(dest_folder))
            {
                Directory.CreateDirectory(dest_folder);
            }
            File.Copy(source_folder+adaptername, dest_folder+adaptername,true);
            File.Copy(source_folder + "adapter.ini", dest_folder + "adapter.ini", true);
            

            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = dest_folder + adaptername;//"dcm2jpg.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Normal;//Hidden
            //use help, install, remove, debug or run
            startInfo.Arguments = "install";//"debug";//"-f j -o \"" + ex1 + "\" -z 1.0 -s y " + ex2;

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                // Log error.
                System.Console.WriteLine("ERROR:" + ex.Message);
            }
        }

        private void BTN_Browse_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Select the root folder of your installation, a subfolder will be created automatically.", "Installation path", MessageBoxButtons.OK, MessageBoxIcon.None);

            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.Description = "Select FOCAS adapter installation folder";
            folderBrowser.RootFolder = Environment.SpecialFolder.MyComputer;
            folderBrowser.ShowNewFolderButton = true;

            DialogResult dialog = folderBrowser.ShowDialog();

            if(dialog==DialogResult.OK)
            {
                TB_Path.Text = folderBrowser.SelectedPath;
            }
        }

        private void BTN_RemoveService_Click(object sender, EventArgs e)
        {
            if (CB_Controllers.SelectedIndex >= 0 && TB_Path.Text.Length > 0)
            {
                string adaptername = "";
                switch (CB_Controllers.Items[CB_Controllers.SelectedIndex].ToString())
                {
                    case "0i":
                        adaptername = "fanuc_0i.exe";
                        break;
                    case "15i":
                        adaptername = "fanuc_15i.exe";
                        break;
                    case "30i":
                        adaptername = "fanuc_30i.exe";
                        break;

                }

                RemoveAdapter(adaptername);
            }
        }

        private void RemoveAdapter(string adaptername)
        {
            string dest_folder = TB_Path.Text + @"\adapters\";
            //copy the file
            if (File.Exists(dest_folder + adaptername))
            {
                Directory.CreateDirectory(dest_folder);
            
                // Use ProcessStartInfo class
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
                startInfo.FileName = dest_folder + adaptername;//"dcm2jpg.exe";
                startInfo.WindowStyle = ProcessWindowStyle.Normal;//Hidden
                startInfo.Arguments = "remove";//"debug";//"-f j -o \"" + ex1 + "\" -z 1.0 -s y " + ex2;

                try
                {
                    // Start the process with the info we specified.
                    // Call WaitForExit and then the using statement will close.
                    using (Process exeProcess = Process.Start(startInfo))
                    {                        
                        exeProcess.WaitForExit();
                    }
                }
                catch (Exception ex)
                {
                    // Log error.
                    System.Console.WriteLine("ERROR:" + ex.Message);
                }
            }
        }


        private void TestAdapter(string adaptername)
        {
            // For the example
            //const string ex1 = "C:\\";
            //const string ex2 = "C:\\Dir";

            string source_folder = @"adapters\";
            string dest_folder = TB_Path.Text + @"\adapters\";
            //copy the file
            if (!Directory.Exists(dest_folder))
            {
                Directory.CreateDirectory(dest_folder);
            }
            File.Copy(source_folder + adaptername, dest_folder + adaptername, true);
            File.Copy(source_folder + "adapter.ini", dest_folder + "adapter.ini", true);


            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = dest_folder + adaptername;//"dcm2jpg.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Normal;//Hidden
            //use help, install, remove, debug or run
            startInfo.Arguments = "debug";//"debug";//"-f j -o \"" + ex1 + "\" -z 1.0 -s y " + ex2;

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                // Log error.
                System.Console.WriteLine("ERROR:" + ex.Message);
            }
        }

        private void BTN_TestAdapter_Click(object sender, EventArgs e)
        {
            if (CB_Controllers.SelectedIndex >= 0 && TB_Path.Text.Length > 0)
            {
                string adaptername = "";
                switch (CB_Controllers.Items[CB_Controllers.SelectedIndex].ToString())
                {
                    case "0i":
                        adaptername = "fanuc_0i.exe";
                        break;
                    case "15i":
                        adaptername = "fanuc_15i.exe";
                        break;
                    case "30i":
                        adaptername = "fanuc_30i.exe";
                        break;

                }

                TestAdapter(adaptername);
            }
            
        }
    }
}


