using CSIFLEX.Utilities;
using System;
using System.Windows.Forms;

namespace CSIFLEX.MonitoringUnits.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Log.Instance.Init("CSIFLEX_MU_Analytics");

            UnitsDataForm form = new UnitsDataForm();
            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
        }
    }
}
