using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Diagnostics;
using System.Threading;
using MySql.Data.MySqlClient;
using System.Timers;

namespace LiveStatusSimulator
{
    public partial class Main : Form
    {
        private const string MySqlConnectionString = "server=localhost;user=root;port=3306;";
        private int nbofmachines;
        private volatile bool requestedStop;
        //private System.Windows.Forms.Timer updatestatus_timer;

        public Main()
        {
            InitializeComponent();
            //MySqlConnectionString = "server=localhost;user=root;port=3306;";
            nbofmachines = 100;
            requestedStop = false;
            //updatestatus_timer.Interval = 1000;
            //updatestatus_timer.Tick += updatestatus_timer_Tick;
        }

        //void updatestatus_timer_Tick(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        private void Form1_Load(object sender, EventArgs e)
        {
            StartWamp();
            CleanTables();
            GenerateMachineList();
            StartLiveStatusSimulation();
        }

        private void StartWamp()
        {
            if ((File.Exists("C:\\wamp\\wampmanager.exe")))
            {
                Process.Start("C:\\wamp\\wampmanager.exe");
            }
            else
            {
                MessageBox.Show("Please Install Wamp Server before launching CSI Flex Server");
                Environment.Exit(0);
            }

            Thread.Sleep(2000);
        }

        private void CleanTables()
        {
            using (MySqlConnection conn = new MySqlConnection(MySqlConnectionString))
            {
                try
                {
                    conn.Open();
                    string mysql3 = "delete from CSI_database.tbl_livestatut";
                    MySqlCommand cmdCreateDeviceTable3 = new MySqlCommand(mysql3, conn);
                    cmdCreateDeviceTable3.ExecuteNonQuery();
                    //MySqlDataReader mysqlReader3 = cmdCreateDeviceTable3.ExecuteReader;
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to delete tables");
                    conn.Close();
                }
            }
        }

        private void GenerateMachineList()
        {
            using (MySqlConnection conn = new MySqlConnection(MySqlConnectionString))
            {
                try
                {
                    conn.Open();
                    string machinelist = "";
                    for (int i = 0; i < nbofmachines; ++i)
                    {
                        machinelist += "MC" + i.ToString() + ", ";

                        string mysql3 = "insert into CSI_database.tbl_livestatut (machinename_,statut_) values('MC" + i.ToString() + "','" + GenerateStatus() + "')";
                        MySqlCommand cmdCreateDeviceTable3 = new MySqlCommand(mysql3, conn);
                        cmdCreateDeviceTable3.ExecuteNonQuery();
                    }
                    machinelist = machinelist.Substring(0, machinelist.Length - 2);

                    
                    string mysql4 = "update CSI_database.tbl_devices set machines='" + machinelist + "'";
                    MySqlCommand cmdCreateDeviceTable4 = new MySqlCommand(mysql4, conn);
                    cmdCreateDeviceTable4.ExecuteNonQuery();
                    //MySqlDataReader mysqlReader3 = cmdCreateDeviceTable3.ExecuteReader;
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to update tables");
                    conn.Close();
                }
            }

        }
        private void StartLiveStatusSimulation()
        {
            try
            {
                for (int i = 0; i < nbofmachines; ++i)
                {
                    StartUpdateThread("MC" + i.ToString());
                }
            }
            catch (Exception)
            {
                System.Console.WriteLine("Unable to start threads");
            }
        }


        private void StartUpdateThread(string machinename)
        {
            try
            {
                Thread t = new Thread(UpdateLiveStatus);
                t.Name = machinename;
                t.Start();
            }
            catch (Exception)
            {
                System.Console.WriteLine("Unable to start thread for machine:" + machinename);
            }
        }

        private void UpdateLiveStatus()
        {
            using (MySqlConnection conn = new MySqlConnection(MySqlConnectionString))
            {
                conn.Open();
                while (!requestedStop)
                {
                    try
                    {
                        string mysql3 = "update CSI_Database.tbl_livestatut set statut_='" + GenerateStatus() + "' where machinename_='" + Thread.CurrentThread.Name + "'";
                        MySqlCommand cmdCreateDeviceTable3 = new MySqlCommand(mysql3, conn);
                        cmdCreateDeviceTable3.ExecuteNonQuery();
                        //MySqlDataReader mysqlReader3 = cmdCreateDeviceTable3.ExecuteReader;                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Unable to update Livestatus for machine:" + Thread.CurrentThread.Name);                        
                    }
                    Thread.Sleep(GenerateSleep());
                }
                conn.Close();
            }
            Console.WriteLine("worker thread: terminating gracefully.");
        }

        private string GenerateStatus()
        {
            string status = "NOT CONNECTED";

            Random rnd = new Random();
            int statusint = rnd.Next(1, 4); // creates a number between 1 and 3


            switch (statusint)
            {
                case 1:
                    status = "CYCLE ON";
                    break;
                case 2:
                    status = "CYCLE OFF";
                    break;
                case 3:
                    status = "SETUP";
                    break;
                default:
                    status = "NOT CONNECTED";
                    break;
            }

            return status;
        }

        private int GenerateSleep()
        {
            Random rnd = new Random();
            return rnd.Next(1, 2) * 200;
        }

        private void BTN_StopThreads_Click(object sender, EventArgs e)
        {
            requestedStop = true;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            requestedStop = true;
        }
    }
}
