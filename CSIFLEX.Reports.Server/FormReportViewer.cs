using CSIFLEX.Reports.Server.Data;
using CSIFLEX.Server.Library;
using CSIFLEX.Server.Library.DataModel;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CSIFLEX.Reports.Server
{
    public partial class FormReportViewer : Form
    {
        List<MachineDBTable> machines;

        public FormReportViewer()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbReports.Items.Add("DowntimeReport");
            cmbReports.SelectedIndex = 0;

            cmbScale.SelectedIndex = 0;

            machines = Support.GetMachineTables();

            foreach (var machine in machines)
            {
                cklistMachines.Items.Add(machine.MachineName);
            }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            if (cmbReports.SelectedIndex == -1)
            {
                MessageBox.Show("Select a report");
                return;
            }

            List<MachineDBTable> selectedMachines = new List<MachineDBTable>();
            foreach(var selected in cklistMachines.CheckedItems)
            {
                var mach = machines.Find(m => m.MachineName == selected.ToString());
                selectedMachines.Add(mach);
            }

            if (selectedMachines.Count == 0)
            {
                MessageBox.Show("Select one or more machines");
                return;
            }

            //this.reportViewer1.LocalReport.ReportEmbeddedResource = $"CSIFLEX.Reports.Server.Reports.{cmbReports.Text}.rdlc";

            ReportDataSource rds;

            ReportParameters parameters = new ReportParameters();
            parameters.ReportName = cmbReports.Text;
            parameters.ReportPeriod = "Daily";
            parameters.Start = dateStart.Value;
            parameters.End = dateEnd.Value;
            parameters.Machines = selectedMachines;
            parameters.Scale = cmbScale.Text;
            parameters.RdlcPath = @"..\..\reports\DowntimeReport.rdlc";
            parameters.OutputPath = @"C:\Temp";
            parameters.ShortFileName = false;

            switch (cmbReports.Text) {
                case "DowntimeReport":
                    DowntimeReport downtimeReport = new DowntimeReport(parameters);
                    webBrowser.Navigate(downtimeReport.GenerateReport());
                    break;
                default:
                    //rds = new ReportDataSource("DataSetDowntime", DowntimeDataSource.GetMachines(dateStart.Value, dateEnd.Value, selectedMachines));
                    break;
            }


            //this.reportViewer1.LocalReport.DataSources.Clear();
            //this.reportViewer1.LocalReport.DataSources.Add(rds);
            //this.reportViewer1.LocalReport.Refresh();
            //this.reportViewer1.RefreshReport();
        }
    }
}
