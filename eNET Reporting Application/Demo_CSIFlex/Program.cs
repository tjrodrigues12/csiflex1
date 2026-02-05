using CSIFlex_ServiceLibrary;
using CSIFlex_ServiceLibrary.Classes;
using CSIFlex_ServiceLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo_CSIFlex
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                ImportData objLoad = new ImportData();
                //LoadFiles objLoad = new LoadFiles();
                objLoad.LoadFiles_Load();
                while (true)
                {
                    Utility.WriteToFile("-----Service starts-----");
                    objLoad.DoSetup(null, null);
                    objLoad.DisplayTimeEvent(null, null);
                    //Thread.Sleep(1000);
                }
                Application.Run(new LoadFiles());
            }
            catch (Exception e)
            {
                string lineNumber = e.StackTrace.Substring(e.StackTrace.Length - 7, 7);
                Utility.WriteToFile(e.ToString() + "|" + lineNumber);
            }


        }
    }
}
