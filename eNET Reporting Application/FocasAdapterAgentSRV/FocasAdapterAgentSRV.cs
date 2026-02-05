using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace FocasAdapterAgentSRV
{
    public partial class FocasAdapterAgentSRV : ServiceBase
    {
        public FocasAdapterAgentSRV()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // Once CSIFlex_Server Service is started successfully start a thread to check if This service running or not 
            // Install Different Adapter Settings and Agent Settings 
            // Start Both the Services 
        }

        protected override void OnStop()
        {
        }
    }
}
