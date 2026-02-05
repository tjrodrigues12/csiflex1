using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSIFLEX.StatusChanger
{
    public partial class BootControl : UserControl
    {
        public BootControl()
        {
            InitializeComponent();
        }
    }

    public class Machine
    {
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public int CycleOnTime { get; set; }
        public int CycleOffTime { get; set; }
        public string MachineStatus { get; set; }
        public string BootStatus { get; set; }

        int cycleOnTimeMin = 0;
        int cycleOnTimeMax = 0;
        int cycleOffTimeMin = 0;
        int cycleOffTimeMax = 0;

        bool inPause = false;

        public void Start()
        {
            if (CycleOnTime == 0) CycleOnTime = 60;
            if (CycleOffTime == 0) CycleOffTime = 30;

            cycleOnTimeMin = (int)(CycleOnTime * 0.9);
            cycleOnTimeMax = (int)(CycleOnTime * 1.1);
            cycleOffTimeMin = (int)(CycleOffTime * 0.9);
            cycleOffTimeMax = (int)(CycleOffTime * 1.1);

        }

        public void Pause()
        {

        }

        public void Stop()
        {

        }
    }
}
