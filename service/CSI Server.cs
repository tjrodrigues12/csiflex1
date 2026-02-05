using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using CSI_Library;
using System.IO ;

using System.Threading;


namespace CSIServer
{
    

    public partial class CSIServer : ServiceBase
    {

       // public new CSI_Library.ServiceNT srv;
        private Thread m_thread = null;
        public CSIServer()
        {
            InitializeComponent();
        }
        public CSI_Library.ServiceNT srv = new CSI_Library.ServiceNT();

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.

            Thread.Sleep(500);

            m_thread = new Thread((srv.start_service));
            m_thread.Name = "CSIFLEX_Server main thread";
            m_thread.Start();

        }


       

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            Thread.Sleep(500);
            srv.stop_service();
            m_thread.Abort();



        }
    }
}
