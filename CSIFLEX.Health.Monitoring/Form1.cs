using CSIFLEX.Database.Access;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Exchange.WebServices.Data;
using System.Threading;

namespace CSIFLEX.Health.Monitoring
{
    public partial class frmMain : Form
    {
        const string CONNECTION_STRING = "server=localhost;user=root;password=CSIF1337;port=3306;Convert Zero Datetime=True;Allow User Variables=True;";

        int customerId = 0;
        string[] lines;

        Dictionary<string, int> dicMachines;
        Dictionary<string, Dictionary<string, Dictionary<string, long>>> dicCsvMachines;

        List<string> result;

        public frmMain()
        {
            InitializeComponent();

            LoadCustomers(chkListCompanies);
            LoadRegions();
            datePickerStart.Value = DateTime.Today.AddDays(-1);
            datePickerEnd.Value = DateTime.Today.AddDays(-1);
            dateTimeEmail.Value = DateTime.Today;
        }

        private void btnImportEmail_Click(object sender, EventArgs e)
        {
            ExchangeService exchange = new ExchangeService();
            exchange.Credentials = new WebCredentials("sh@csiflex.com", @"New.Csi.Pwd#1357");
            exchange.Url = new Uri("https://outlook.office365.com/EWS/Exchange.asmx");

            if(exchange != null)
            {
                this.Cursor = Cursors.WaitCursor;
                var itens = exchange.FindItems(WellKnownFolderName.Inbox, new ItemView(100));

                foreach (Item email in itens.Where(i => i.DateTimeReceived >= dateTimeEmail.Value.Date))
                {
                    if (email.HasAttachments)
                    {
                        string file = GetAttachmentsFromEmail(exchange, email.Id);

                        if (!string.IsNullOrEmpty(file))
                        {
                            txtDatFilePath.Text = file;

                            LoadDatFile();

                            btnImportDatFile_Click(this, new EventArgs());
                            Thread.Sleep(500);
                        }
                    }
                }
                this.Cursor = Cursors.Default;
                MessageBox.Show("Done");
            }
        }

        public static string GetAttachmentsFromEmail(ExchangeService service, ItemId itemId)
        {
            // Bind to an existing message item and retrieve the attachments collection.
            // This method results in an GetItem call to EWS.
            EmailMessage message = EmailMessage.Bind(service, itemId, new PropertySet(ItemSchema.Attachments));

            string fileName = "C:\\temp\\cfh.dat";

            // Iterate through the attachments collection and load each attachment.
            foreach (Attachment attachment in message.Attachments)
            {
                if (attachment is FileAttachment && attachment.Name == "cfh.dat")
                {
                    FileAttachment fileAttachment = attachment as FileAttachment;

                    // Load the attachment into a file.
                    // This call results in a GetAttachment call to EWS.
                    fileAttachment.Load(fileName);

                    return fileName;
                }
            }

            return "";
        }


        private void btnBrowseDatFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            txtDatFilePath.Text = openFileDialog.FileName;

            LoadDatFile();
        }

        private void btnSaveCustomerName_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtCustomerName.Text))
            {
                MessageBox.Show("Enter the customer's name");
                txtCustomerName.Focus();
                return;
            }

            if (cmbRegion.SelectedIndex < 0)
            {
                MessageBox.Show("Enter the region");
                cmbRegion.Focus();
                return;
            }

            int regionId = int.Parse(cmbRegion.SelectedValue.ToString());

            StringBuilder cmd = new StringBuilder();
            cmd.Append($"INSERT INTO csiflex_health.customers        ");
            cmd.Append($" (                                          ");
            cmd.Append($"     UniqueId    ,                          ");
            cmd.Append($"     RegionId    ,                          ");
            cmd.Append($"     CustomerName                           ");
            cmd.Append($" )                                          ");
            cmd.Append($" VALUES                                     ");
            cmd.Append($" (                                          ");
            cmd.Append($"    '{ lblCustomerGuid.Text }',             ");
            cmd.Append($"     { regionId }             ,             ");
            cmd.Append($"    '{ txtCustomerName.Text }'              ");
            cmd.Append($" )                                          ");
            cmd.Append($" ON DUPLICATE KEY                           ");
            cmd.Append($" UPDATE                                     ");
            cmd.Append($"   CustomerName = '{txtCustomerName.Text}', ");
            cmd.Append($"   RegionId     =  {regionId}             ; ");

            cmd.Append($"SELECT * FROM csiflex_health.customers WHERE UniqueId = '{lblCustomerGuid.Text}'");

            DataTable dtCustomer = MySqlAccess.GetDataTable(cmd.ToString(), CONNECTION_STRING);
            customerId = int.Parse(dtCustomer.Rows[0]["Id"].ToString());

            lblCustomerId.Text = customerId.ToString();

            btnImportDatFile.Enabled = true;
        }

        private void btnImportDatFile_Click(object sender, EventArgs e)
        {
            LoadDatFile();

            LoadMachines();

            string[] lines = this.lines.Skip(1).ToArray();

            List<CycleStatus> cycleTimes = new List<CycleStatus>();

            foreach (string line in lines)
            {
                string[] fields = line.Split(',');

                if (!dicMachines.ContainsKey(fields[1].ToUpper()))
                {
                    int id = int.Parse(MySqlAccess.ExecuteScalar($"INSERT INTO csiflex_health.machines (CustomerId, MachineName) VALUES ({customerId}, '{fields[1]}'); SELECT LAST_INSERT_ID();", CONNECTION_STRING).ToString());
                    dicMachines.Add(fields[1].ToUpper(), id);
                }

                CycleStatus cycleTime = new CycleStatus()
                {
                    CustomerId = int.Parse(lblCustomerId.Text),
                    SummaryDate = DateTime.ParseExact(fields[0], "yyyy-MM-dd",System.Globalization.CultureInfo.InvariantCulture),
                    MachineId = dicMachines[fields[1].ToUpper()],
                    MachineName = fields[1],
                    Status = fields[2],
                    Cycletime = (long)(Decimal.Parse(fields[3]) * 60)
                };

                SaveCycleTime(cycleTime);
                cycleTimes.Add(cycleTime);
            }

            dataGridDatImport.DataSource = cycleTimes;
        }


        private void btnBrowseCsvFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files|*.csv";

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            txtCsvFilePath.Text = openFileDialog.FileName;

            LoadCustomers(cmbCsvCustomers);
            cmbCsvCustomers.SelectedIndex = -1;
        }

        private void cmbCsvCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCsvCustomers.SelectedIndex < 0)
            {
                btnImportCsvFile.Enabled = false;
                lblCsvCustomerId.Text = "0";
            }
            else
            {
                btnImportCsvFile.Enabled = true;
                lblCsvCustomerId.Text = (cmbCsvCustomers.SelectedItem as dynamic).Value ;
            }
            int.TryParse(lblCsvCustomerId.Text, out customerId);
        }

        private void btnImportCsvFile_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            LoadMachines();

            string[] lines = File.ReadAllLines(txtCsvFilePath.Text).Skip(1).ToArray();

            dicCsvMachines = new Dictionary<string, Dictionary<string, Dictionary<string, long>>>();

            foreach(string line in lines)
            {
                string[] fields = line.Split(',');
                string machine = fields[0];
                string date = fields[1];
                string status = fields[6];
                long cycleTime = long.Parse(fields[5]);
                
                if (cycleTime > 0)
                {
                    if (dicCsvMachines.ContainsKey(machine))
                    {
                        if (dicCsvMachines[machine].ContainsKey(date))
                        {
                            if (dicCsvMachines[machine][date].ContainsKey(status))
                            {
                                dicCsvMachines[machine][date][status] += cycleTime;
                            }
                            else
                            {
                                dicCsvMachines[machine][date].Add(status, cycleTime);
                            }
                        }
                        else
                        {
                            dicCsvMachines[machine].Add(date, new Dictionary<string, long> { [status] = cycleTime });
                        }
                    }
                    else
                    {
                        dicCsvMachines.Add(machine, new Dictionary<string, Dictionary<string, long>> { [date] = new Dictionary<string, long> { [status] = cycleTime } });
                    }
                }
            }

            List<CycleStatus> cycleTimes = new List<CycleStatus>();

            CycleStatus cycleStatus;

            foreach (var machine in dicCsvMachines)
            {
                if (!dicMachines.ContainsKey(machine.Key.ToUpper()))
                {
                    int id = int.Parse(MySqlAccess.ExecuteScalar($"INSERT IGNORE INTO csiflex_health.machines (CustomerId, MachineName) VALUES ({customerId}, '{machine.Key}'); SELECT LAST_INSERT_ID();", CONNECTION_STRING).ToString());
                    dicMachines.Add(machine.Key.ToUpper(), id);
                }

                foreach (var date in machine.Value)
                {
                    foreach(var status in date.Value)
                    {
                        cycleStatus = new CycleStatus()
                        {
                            CustomerId = int.Parse(lblCsvCustomerId.Text),
                            MachineId = dicMachines[machine.Key.ToUpper()],
                            MachineName = machine.Key,
                            SummaryDate = DateTime.ParseExact(date.Key, "M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                            Status = status.Key,
                            Cycletime = status.Value
                        };

                        cycleTimes.Add(cycleStatus);
                        SaveCycleTime(cycleStatus);
                    }
                }
            }

            dataGridCsvImport.DataSource = cycleTimes;

            this.Cursor = Cursors.Default;
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCustomers(chkListCompanies);
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            string customersId = "";

            foreach (object item in chkListCompanies.CheckedItems)
            {
                string customerId = item.GetType().GetProperty("Value").GetValue(item).ToString().Split(',')[0];

                customersId += $",{customerId}";
            }
            customersId = customersId.Substring(1);


            dataGridResult.Rows.Clear();

            StringBuilder cmd = new StringBuilder();
            cmd.Append($"SELECT                                ");
            cmd.Append($"    MachineId   ,                     ");
            cmd.Append($"    SummaryDate ,                     ");
            cmd.Append($"    CycleTime                         ");
            cmd.Append($" FROM                                 ");
            cmd.Append($"    csiflex_health.summary_cycletimes ");
            cmd.Append($" WHERE                                ");
            cmd.Append($"    CustomerId IN ('{ customersId }') ");
            cmd.Append($"    AND SummaryDate >= '{datePickerStart.Value.ToString("yyyy-MM-dd")}' ");
            cmd.Append($"    AND SummaryDate <= '{datePickerEnd.Value.ToString("yyyy-MM-dd")}'   ");
            cmd.Append($"    AND Status  = 'CYCLE ON'          ");
            cmd.Append($" GROUP BY                             ");
            cmd.Append($"    CustomerId,                       ");
            cmd.Append($"    SummaryDate,                      ");
            cmd.Append($"    MachineId	                       ");

            DataTable dtCycleOn = MySqlAccess.GetDataTable(cmd.ToString(), CONNECTION_STRING);

            cmd.Clear();
            cmd.Append($"SELECT                                ");
            cmd.Append($"    CustomerId,                       ");
            cmd.Append($"    MachineId ,                       ");
            cmd.Append($"    SummaryDate ,                     ");
            cmd.Append($"    Sum(CycleTime) AS TotalOther      ");
            cmd.Append($" FROM                                 ");
            cmd.Append($"    csiflex_health.summary_cycletimes ");
            cmd.Append($" WHERE                                ");
            cmd.Append($"    CustomerId IN ('{ customersId }') ");
            cmd.Append($"    AND SummaryDate >= '{datePickerStart.Value.ToString("yyyy-MM-dd")}' ");
            cmd.Append($"    AND SummaryDate <= '{datePickerEnd.Value.ToString("yyyy-MM-dd")}'   ");
            cmd.Append($"    AND Status <> 'CYCLE ON'          ");
            cmd.Append($" GROUP BY                             ");
            cmd.Append($"    CustomerId,                       ");
            cmd.Append($"    SummaryDate,                      ");
            cmd.Append($"    MachineId	                       ");

            DataTable dtOthers = MySqlAccess.GetDataTable(cmd.ToString(), CONNECTION_STRING);

            var dates = dtOthers.AsEnumerable().Select(o => o.Field<DateTime>("SummaryDate").ToString()).Distinct();

            result = new List<string>();

            foreach(string date in dates)
            {
                int qttMachines = dtOthers.AsEnumerable().Where(o => o.Field<DateTime>("SummaryDate").ToString() == date).Select(o => o.Field<int>("MachineId")).Distinct().Count();

                decimal ttlOthers = dtOthers.AsEnumerable().Where(o => o.Field<DateTime>("SummaryDate").ToString() == date).Sum(m => m.Field<decimal>("TotalOther"));

                decimal ttlCycleOn = dtCycleOn.AsEnumerable().Where(o => o.Field<DateTime>("SummaryDate").ToString() == date).Sum(m => m.Field<int>("CycleTime"));

                decimal ttl = ttlCycleOn + ttlOthers;

                if (ttl > 0)
                {
                    string percCycleOn = string.Format("{0:0.00}", ((ttlCycleOn / ttl) * 100));
                    string percOthers = string.Format("{0:0.00}", ((ttlOthers / ttl) * 100));

                    string summary = $"Date:{date.Substring(0,10)}; NumberMachines:{qttMachines}; TotalSeconds:{ttl}; CycleOn:{ttlCycleOn}:{percCycleOn}; Others:{ttlOthers}:{percOthers}";

                    result.Add(summary);

                    dataGridResult.Rows.Add(summary);
                }
            }

            int ttlCompanies = dtOthers.AsEnumerable().Select(c => c.Field<int>("CustomerId")).Distinct().Count();

            lblNumberOfCompanies.Text = chkListCompanies.CheckedItems.Count.ToString();
        }

        Dictionary<string, Tuple<long, long, int, Dictionary<string, long>>> dicCyclesTimeDates;

        private void btnProcess2_Click(object sender, EventArgs e)
        {
            string customersId = "";

            foreach (object item in chkListCompanies.CheckedItems)
            {
                string customerId = item.GetType().GetProperty("Value").GetValue(item).ToString().Split(',')[0];

                customersId += $",{customerId}";
            }

            if (string.IsNullOrEmpty(customersId))
            {
                MessageBox.Show("Select the curstomer(s)");
                return;
            }

            customersId = customersId.Substring(1);

            StringBuilder cmd = new StringBuilder();
            cmd.Append($"SELECT                                ");
            cmd.Append($"    CustomerId  ,                     ");
            cmd.Append($"    MachineId   ,                     ");
            cmd.Append($"    Status      ,                     ");
            cmd.Append($"    SummaryDate ,                     ");
            cmd.Append($"    CycleTime                         ");
            cmd.Append($" FROM                                 ");
            cmd.Append($"    csiflex_health.summary_cycletimes ");
            cmd.Append($" WHERE                                ");
            cmd.Append($"    CustomerId IN ({ customersId })   ");
            cmd.Append($"    AND CycleTime > 0                 ");
            cmd.Append($"    AND SummaryDate >= '{datePickerStart.Value.ToString("yyyy-MM-dd")}' ");
            cmd.Append($"    AND SummaryDate <= '{datePickerEnd.Value.ToString("yyyy-MM-dd")}'   ");
            cmd.Append($" ORDER BY                             ");
            cmd.Append($"    SummaryDate                       ");

            DataTable dtCyclesTime = MySqlAccess.GetDataTable(cmd.ToString(), CONNECTION_STRING);

            var listCias = from c in dtCyclesTime.AsEnumerable()
                           group c by c.Field<string>("CustomerId") into g
                           select new
                           {
                               CompanyId = g.Key,
                               Count = g.Count()
                           };

            dataGridResult.DataSource = listCias;

            var listStatus = dtCyclesTime.AsEnumerable().Select(o => o.Field<string>("Status").ToString()).Distinct();

            Dictionary<string, long> dicCycleStatus = new Dictionary<string, long>();
            foreach (string item in listStatus)
            {
                dicCycleStatus.Add(item, 0);
            }

            var dates = dtCyclesTime.AsEnumerable().Select(o => o.Field<DateTime>("SummaryDate").ToString()).Distinct();

            dicCyclesTimeDates = new Dictionary<string, Tuple<long, long, int, Dictionary<string, long>>>();

            foreach (string date in dates)
            {
                long ttlOthers = 0;
                long ttlDate = 0;
                EnumerableRowCollection<DataRow> cycles = dtCyclesTime.AsEnumerable().Where(item => item.Field<DateTime>("SummaryDate").ToString() == date);

                int qttMachines = cycles.AsEnumerable().Where(o => o.Field<DateTime>("SummaryDate").ToString() == date).Select(o => o.Field<int>("MachineId")).Distinct().Count();

                Dictionary<string, long> cloneDicCycleStatus = new Dictionary<string, long>(dicCycleStatus);

                foreach(DataRow row in cycles)
                {
                    string status = row["Status"].ToString();
                    long cycletime = long.Parse(row["CycleTime"].ToString());

                    ttlDate += cycletime;
                    if (status != "CYCLE ON")
                        ttlOthers += cycletime;

                    cloneDicCycleStatus[status] += cycletime;
                }
                dicCyclesTimeDates.Add(date, Tuple.Create(ttlDate, ttlOthers, qttMachines, cloneDicCycleStatus));
            }

            List<string> lines = new List<string>();
            string line = "DATE,NUMBER OF MACHINES,TOTAL TIME,CYCLE ON,CYCLE ON %,OTHERS,OTHERS %";

            foreach(string item in listStatus)
            {
                if (item != "CYCLE ON")
                    line += $",{item.ToUpper()},{item.ToUpper()} %";
            }
            lines.Add(line);

            foreach(var item in dicCyclesTimeDates)
            {
                string date = item.Key;
                decimal ttlDate = (decimal)item.Value.Item1;
                decimal ttlOther = (decimal)item.Value.Item2;
                int ttlMachines = item.Value.Item3;

                decimal ttlStatus = item.Value.Item4.ContainsKey("CYCLE ON") ? item.Value.Item4["CYCLE ON"] : 0;
                string percStatus = string.Format("{0:0.00000}", ttlStatus / ttlDate);
                string percOthers = string.Format("{0:0.00000}", ttlOther / ttlDate);

                line = $"{date},{ttlMachines},{ttlDate},{ttlStatus},{percStatus},{ttlOther},{percOthers}";

                foreach(var itemDic in item.Value.Item4)
                {
                    ttlStatus = (decimal)itemDic.Value;
                    percStatus = string.Format("{0:0.00000}", ttlStatus / ttlDate);

                    if (itemDic.Key != "CYCLE ON")
                        line += $",{ttlStatus},{percStatus}";
                }
                lines.Add(line);
            }

            txtSummaryList.Lines = lines.ToArray();
        }

        private void dataGridResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSummary.Text = dataGridResult.Rows[e.RowIndex].Cells[0].Value.ToString();

            string[] fields = txtSummary.Text.Split(';');

            if (fields.Length == 0)
                return;

            lblDate.Text = fields[0].Split(':')[1];
            lblNumberOfMachines.Text = fields[1].Split(':')[1];

            lblTotal.Text = long.Parse(fields[2].Split(':')[1]).ToString("#,0");
            lblCycleOn.Text = long.Parse(fields[3].Split(':')[1]).ToString("#,0");
            lblOthers.Text = long.Parse(fields[4].Split(':')[1]).ToString("#,0");

            lblPercCycleOn.Text = fields[3].Split(':')[2];
            lblPercOthers.Text = fields[4].Split(':')[2];
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "CSIFLEX Monitoring Health.csv";
            
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            File.WriteAllLines(saveFileDialog.FileName, txtSummaryList.Lines);

            return;


            List<string> lines = new List<string>();
            lines.Add("DATE,NUMBER OF MACHINES,TOTAL TIME,CYCLE ON TIME,CYCLE ON PERCENTUAL,OTHERS TIME,OTHERS PERCENTUAL");
            foreach(string line in result)
            {
                string[] fields = line.Split(';');

                string lblDate = fields[0].Split(':')[1];
                string lblNumberOfMachines = fields[1].Split(':')[1];

                string lblTotal = fields[2].Split(':')[1];
                string lblCycleOn = fields[3].Split(':')[1];
                string lblOthers = fields[4].Split(':')[1];

                string lblPercCycleOn = fields[3].Split(':')[2];
                string lblPercOthers = fields[4].Split(':')[2];

                lines.Add($"{lblDate},{lblNumberOfMachines},{lblTotal},{lblCycleOn},{lblPercCycleOn},{lblOthers},{lblPercOthers}");
            }

            File.WriteAllLines(saveFileDialog.FileName, lines.ToArray());
        }


        private void LoadDatFile()
        {
            try
            {
                lines = File.ReadAllLines(txtDatFilePath.Text);

                Guid customerGuid;

                if (!Guid.TryParse(lines[0], out customerGuid))
                {
                    MessageBox.Show("Invalid file");
                    return;
                }

                lblCustomerGuid.Text = customerGuid.ToString();

                DataTable dtCustomer = MySqlAccess.GetDataTable($"SELECT * FROM csiflex_health.customers WHERE UniqueId = '{customerGuid.ToString()}'", CONNECTION_STRING);

                customerId = 0;
                txtCustomerName.Text = string.Empty;
                cmbRegion.SelectedIndex = -1;

                btnImportDatFile.Enabled = false;
                btnSaveCustomerName.Enabled = true;

                if (dtCustomer.Rows.Count > 0)
                {
                    customerId = int.Parse(dtCustomer.Rows[0]["Id"].ToString());
                    txtCustomerName.Text = dtCustomer.Rows[0]["CustomerName"].ToString();

                    cmbRegion.SelectedValue = dtCustomer.Rows[0]["RegionId"].ToString();

                    btnImportDatFile.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Enter the customer's name");
                    txtCustomerName.Focus();
                }

                lblCustomerId.Text = customerId.ToString();
            }
            catch (Exception ex) { }
        }

        private void LoadMachines()
        {
            dicMachines = new Dictionary<string, int>();

            DataTable dtMachines = MySqlAccess.GetDataTable($"SELECT * FROM csiflex_health.machines WHERE CustomerId = { customerId }", CONNECTION_STRING);

            foreach(DataRow row in dtMachines.Rows)
            {
                dicMachines.Add(row["MachineName"].ToString().ToUpper(), int.Parse(row["Id"].ToString()));
            }
        }

        private void SaveCycleTime(CycleStatus cycleStatus)
        {
            StringBuilder cmd = new StringBuilder();

            cmd.Clear();
            cmd.Append($"INSERT INTO                          ");
            cmd.Append($"   csiflex_health.summary_cycletimes ");
            cmd.Append($" (                                   ");
            cmd.Append($"     CustomerId ,                    ");
            cmd.Append($"     MachineId  ,                    ");
            cmd.Append($"     SummaryDate,                    ");
            cmd.Append($"     Status     ,                    ");
            cmd.Append($"     CycleTime                       ");
            cmd.Append($" )                                   ");
            cmd.Append($" VALUES                              ");
            cmd.Append($" (                                   ");
            cmd.Append($"      { cycleStatus.CustomerId } ,   ");
            cmd.Append($"      { cycleStatus.MachineId }  ,   ");
            cmd.Append($"     '{ cycleStatus.SummaryDate.ToString("yyyy-MM-dd")}', ");
            cmd.Append($"     '{ cycleStatus.Status }'    ,   ");
            cmd.Append($"      { cycleStatus.Cycletime }      ");
            cmd.Append($" )                                   ");
            cmd.Append($" ON DUPLICATE KEY                    ");
            cmd.Append($" UPDATE                              ");
            cmd.Append($"   CycleTime = { cycleStatus.Cycletime }; ");

            MySqlAccess.ExecuteNonQuery(cmd.ToString(), CONNECTION_STRING);
        }

        private void LoadCustomers(ComboBox comboBox)
        {
            DataTable dtCustomers = MySqlAccess.GetDataTable($"SELECT Id, CustomerName FROM csiflex_health.customers ORDER BY CustomerName", CONNECTION_STRING);

            comboBox.Items.Clear();
            comboBox.DisplayMember = "Text";
            comboBox.ValueMember = "Value";

            foreach(DataRow row in dtCustomers.Rows)
            {
                comboBox.Items.Add(new { Text = row["CustomerName"].ToString(), Value = row["Id"].ToString() });
            }

            comboBox.SelectedIndex = -1;

            if (comboBox.Name == "cmbCompanies")
            {
                comboBox.Items.Insert(0, new { Text = "All Companies", Value = "0" });
                comboBox.SelectedIndex = 0;
            }
        }

        private void LoadCustomers(CheckedListBox listBox)
        {
            DataTable dtCustomers = MySqlAccess.GetDataTable($"SELECT Id, RegionId, CustomerName FROM csiflex_health.customers ORDER BY CustomerName", CONNECTION_STRING);

            listBox.Items.Clear();
            listBox.DisplayMember = "Text";
            listBox.ValueMember = "Value";

            foreach (DataRow row in dtCustomers.Rows)
            {
                listBox.Items.Add(new { Text = row["CustomerName"].ToString(), Value = $"{ row["Id"].ToString() },{ row["RegionId"].ToString() }" });
            }
        }


        private void LoadRegions()
        {
            DataTable dtCustomers = MySqlAccess.GetDataTable($"SELECT Id, Name FROM csiflex_health.regions", CONNECTION_STRING);

            cmbRegion.Items.Clear();
            cmbRegion.DataSource = dtCustomers;
            cmbRegion.DisplayMember = "Name";
            cmbRegion.ValueMember = "Id";
            cmbRegion.SelectedIndex = -1;
        }

        private void cmbCompanies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCompanies.SelectedIndex < 0)
                return;

            int selectedIndex = cmbCompanies.SelectedIndex;

            for (int idx = 0; idx < chkListCompanies.Items.Count; idx++)
            {
                //string[] values = chkListCompanies.Items[idx].ToString().Split(',')[1].Trim().Split('=');

                string[] values = chkListCompanies.Items[idx].GetType().GetProperty("Value").GetValue(chkListCompanies.Items[idx]).ToString().Split(',');

                switch (selectedIndex)
                {
                    case 0:         //All
                        chkListCompanies.SetItemCheckState(idx, CheckState.Checked);
                        break;
                    case 1:         //Canada
                        chkListCompanies.SetItemCheckState(idx, values[1].Trim() != "3" ? CheckState.Checked : CheckState.Unchecked);
                        break;
                    case 2:         //Ontario
                        chkListCompanies.SetItemCheckState(idx, values[1].Trim() == "2" ? CheckState.Checked : CheckState.Unchecked);
                        break;
                    case 3:         //Quebec
                        chkListCompanies.SetItemCheckState(idx, values[1].Trim() == "1" ? CheckState.Checked : CheckState.Unchecked);
                        break;
                    case 4:         //USA
                        chkListCompanies.SetItemCheckState(idx, values[1].Trim() == "3" ? CheckState.Checked : CheckState.Unchecked);
                        break;
                    case 5:         //None
                        chkListCompanies.SetItemCheckState(idx, CheckState.Unchecked);
                        break;
                }
                if (values[0].Trim() == "14") chkListCompanies.SetItemCheckState(idx, CheckState.Unchecked);
            }
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }
    }
}
