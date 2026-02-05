
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConfigBackup
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void BTN_Backup_Click(object sender, EventArgs e)
        {
            LBL_Result.Text = "Result:";
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = @"C:\wamp\bin\mysql\mysql5.6.17\bin\mysqldump.exe";
                startInfo.Arguments = "-u root --password=CSIF1337 csi_auth --result-file=csi_auth.sql";
                process.StartInfo = startInfo;
                process.Start();

                process = new System.Diagnostics.Process();
                startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = @"C:\wamp\bin\mysql\mysql5.6.17\bin\mysqldump.exe";         
                startInfo.Arguments = "-u root --password=CSIF1337 csi_database --tables tbl_deviceconfig tbl_devices tbl_externalsource tbl_groups tbl_messages --result-file=csi_database.sql";
                process.StartInfo = startInfo;
                process.Start();

                LBL_Result.Text = "Result: Backup completed";
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
                LBL_Result.Text = "Error: "+ex.Message;
            }
        }

        private void BTN_Import_Click(object sender, EventArgs e)
        {
            LBL_ImpRes.Text = "Result:";
            try
            {
                //string path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)+ @"\CSI Flex Server\mysql\mysql-5.7.14-win32\bin\mysql.exe";
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\CSI Flex Server\mysql\mysql-5.7.21-win32\bin\mysql.exe";
                //Check if x86 exist
                if (!File.Exists(path))
                {                    
                    path = Environment.ExpandEnvironmentVariables("%ProgramW6432%") + @"\CSI Flex Server\mysql\mysql-5.7.21-win32\bin\mysql.exe";
                }

                if (File.Exists(path))
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    //startInfo.FileName = @"C:\Users\rbcou\Source\Repos\CSIFlex1\mysql\mysql-5.7.14-win32\bin\mysql.exe";
                    startInfo.FileName = path;
                    startInfo.Arguments = "-u root --password=CSIF1337 csi_auth -e \"source csi_auth.sql\"";
                    process.StartInfo = startInfo;
                    process.Start();

                    process = new System.Diagnostics.Process();
                    startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.FileName = path;
                    startInfo.Arguments = "-u root --password=CSIF1337 csi_database -e \"source csi_database.sql\"";
                    process.StartInfo = startInfo;
                    process.Start();


                    LBL_ImpRes.Text = "Result: Import succesful";
                }
                else
                {
                    LBL_ImpRes.Text = "Error: Unable to find mysql.exe at " + path;
                }
          
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
                LBL_ImpRes.Text = "Error: " + ex.Message;
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        //private void insert_into_deviceConfig2()
        //{
        //    using (MySqlConnection conn = new MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString))
        //    {
        //        MySqlCommand cmd = new MySqlCommand("SELECT name, IP, FROM CSI_database.tbl_deviceconfig", conn);
        //        conn.Open();
        //        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
        //        DataTable results = new DataTable();
        //        adapter.Fill(results);


        //        if (results.Rows.Count != 0)
        //        {
        //            foreach (DataRow r in results.Rows)
        //            {
        //                cmd = new MySqlCommand("insert ignore into CSI_Database.tbl_deviceConfig2 (name, IP_adress,timeline,trends,trendspercent,trendcompare,dateformat,devicetype,scale) VALUES('" + r(0) + "','" + r(1) + "',true,true,20,'shift','dd-MM-yyyy HH:mm:ss','" + "Computer" + "',100);", conn);
        //                cmd.ExecuteNonQuery();
        //            }
        //        }



        //        conn.Close();
        //    }
        //}
    }
}
