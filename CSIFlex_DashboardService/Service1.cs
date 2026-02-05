//using CSIFlex_ServiceLibrary.Classes;
using CSIFlex_ServiceLibrary.BLL;
using CSIFlex_ServiceLibrary.Model;
using CSIFlex_ServiceLibrary.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CSIFlex_ServiceLibrary
{
    public partial class CSIFlex_Service : ServiceBase
    {
        static List<ShiftSetupModel> oldShift = new List<ShiftSetupModel>();
        public CSIFlex_Service()
        {
            InitializeComponent();
        }


        protected override void OnStart(string[] args)
        {
            Debugger.Launch();


            try
            {
                // When Windows service starts, creates and starts separate thread
                //string[] fileData = File.ReadAllLines(@"\\10.0.10.189\c\_eNETDNC\_SETUP\ShiftSetup2.sys");
                //int i = 0;
                //ImportData objLoad = new ImportData();
                //objLoad.LoadFiles_Load();
                //ThreadStart tsTask = new ThreadStart(TaskLoop);
                //Thread MyTask = new Thread(tsTask);
                //MyTask.Start();
                //LoadFiles objLoad = new LoadFiles();
                //    //Thread.Sleep(1000);
                //}

                //var monList = FileParser.getMonSetupData();
                //if (monList != null)
                //{
                //    foreach (var item in monList)
                //    {
                //        MonSetupBLL.InsertMonSetup(item);
                //    }
                //}

                //var ShiftList = FileParser.getShiftSetup();
                //if (ShiftList != null)
                //{
                //    foreach (var item in ShiftList)
                //    {
                //        ShiftSetupBLL.InsertShiftSetup(item); // Insert Function for ShiftSetup Table
                //    }
                //}
                //while (true)
                //{
                //    var currentShifts = ShiftSetupBLL.getCurrentShiftByDepartment();
                //    for (int i = 0; i < oldShift.Count && i < currentShifts.Count; i++)
                //    {
                //        ShiftSetupBLL.CompareShiftData(oldShift[i], currentShifts[i]);
                //    }

                //    oldShift = currentShifts;
                //}
                //TaskLoop();
                // table generate code here
                ThreadStart tsTask = new ThreadStart(TaskLoop);
                Thread MyTask = new Thread(tsTask);
                MyTask.Start();
            }
            catch (Exception e)
            {
                string lineNumber = e.StackTrace.Substring(e.StackTrace.Length - 7, 7);
                Utility.Utility.WriteToFile(e.ToString() + "|" + lineNumber);
            }
            //while (true)
            //{
            //    objLoad.LoadFiles_Load();
            //}
        }

        static void TaskLoop()
        {
            // In this example, task will repeat in infinite loop with while(true)
            // You can add additional parameter here if you want to have an option
            // to stop and restart the task from some external control panel
            while (true)
            {
                // First, execute scheduled task
                ScheduledTask();

                // Then, wait for certain time interval, in this case 1 hour

                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));

            }
        }

        static void ScheduledTask()
        {
            try
            {
                var obj = new CSIFlex_ServiceLibrary.Utility.CreateDatabase();
                obj.CreateDatabaseForFirsttime();
                var monList = FileParser.getMonSetupData();
                if (monList != null)
                {
                    foreach (var item in monList)
                    {
                        int mon_id = MonSetupBLL.InsertMonSetup(item);
                    }
                }

                var ShiftList = FileParser.getShiftSetup();
                if (ShiftList != null)
                {
                    foreach (var item in ShiftList)
                    {
                        ShiftSetupBLL.InsertShiftSetup(item); // Insert Function for ShiftSetup Table
                    }
                }
                var EhubConfList = FileParser.getEHubConf();
                if (EhubConfList != null)
                {
                    foreach (var item in EhubConfList)
                    {
                        EHubConfBLL.InsertEhubConf(item); // Insert Function for EhubConf Table
                    }
                }
                var monitorDataList = FileParser.getMonitorDataFiles();
                if (monitorDataList != null && monitorDataList.Count > 0)
                {
                    foreach (var item in monitorDataList)
                    {
                        MonSetupBLL.UpdateMonSetupMonitorData(item);
                    }
                }

                var currentShifts = ShiftSetupBLL.getCurrentShiftByDepartment();
                for (int i = 0; i < oldShift.Count && i < currentShifts.Count; i++)
                {
                    ShiftSetupBLL.CompareShiftData(oldShift[i], currentShifts[i]);
                }

                oldShift = currentShifts;
            }
            catch (Exception e)
            {
                string lineNumber = e.StackTrace.Substring(e.StackTrace.Length - 7, 7);
                Utility.Utility.WriteToFile(e.ToString() + "|" + lineNumber);
            }
            //ImportData objLoad = new ImportData();
            //// Task code which is executed periodically
            ////Utility.WriteToFile("-----Service starts-----");
            //objLoad.DoSetup(null, null);
            //objLoad.DisplayTimeEvent(null, null);
        }

        private void runLoadFile()
        {
            //LoadFiles objLoad = new LoadFiles();
            //objLoad.LoadFiles_Load();

            //System.Threading.Timer timer = null;

            //timer = new System.Threading.Timer((g) =>
            //{
            //    //Console.WriteLine(1); //do whatever
            //    objLoad.DoSetup(null, null);
            //    objLoad.DisplayTimeEvent(null, null);
            //    timer.Change(1000, System.Threading.Timeout.Infinite);
            //    runLoadFile();
            //}, null, 0, System.Threading.Timeout.Infinite);
        }

        protected override void OnStop()
        {
            //Utility.WriteToFile("-----Service Stop-----");
        }


    }
}
