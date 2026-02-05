namespace CSIFLEX.MonitoringUnits.UI
{
    partial class UnitsDataForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnitsDataForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblBoardLabel = new System.Windows.Forms.Label();
            this.lblMachineName = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Panel();
            this.chkShowDeleted = new System.Windows.Forms.CheckBox();
            this.lblLinkSettings = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnExportToCsv = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.cmbDataType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.clbSensors = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvUnits = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BoardLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BoardIpAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BoardMacAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BoardMachineName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BoardManufacturer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BoardModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BoardSerialNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BoardFirmware = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BoardDeleted = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chartSensors = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.SensorId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SensorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Timestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsMonitoring = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SensorOnline = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsOverride = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsAlarming = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsCSD = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Pallet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentPallet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Metric = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnits)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartSensors)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(1204, 925);
            this.splitContainer1.SplitterDistance = 591;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblBoardLabel);
            this.groupBox1.Controls.Add(this.lblMachineName);
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.chkShowDeleted);
            this.groupBox1.Controls.Add(this.lblLinkSettings);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.btnExportToCsv);
            this.groupBox1.Controls.Add(this.dgvData);
            this.groupBox1.Controls.Add(this.btnLoadData);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtpEnd);
            this.groupBox1.Controls.Add(this.dtpStart);
            this.groupBox1.Controls.Add(this.cmbDataType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.clbSensors);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dgvUnits);
            this.groupBox1.Location = new System.Drawing.Point(18, 5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(1168, 581);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lblBoardLabel
            // 
            this.lblBoardLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBoardLabel.Location = new System.Drawing.Point(7, 268);
            this.lblBoardLabel.Name = "lblBoardLabel";
            this.lblBoardLabel.Size = new System.Drawing.Size(264, 33);
            this.lblBoardLabel.TabIndex = 30;
            this.lblBoardLabel.Text = "BoardLabel";
            this.lblBoardLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblMachineName
            // 
            this.lblMachineName.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMachineName.ForeColor = System.Drawing.Color.Blue;
            this.lblMachineName.Location = new System.Drawing.Point(7, 242);
            this.lblMachineName.Name = "lblMachineName";
            this.lblMachineName.Size = new System.Drawing.Size(264, 26);
            this.lblMachineName.TabIndex = 29;
            this.lblMachineName.Text = "MachineName";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackgroundImage = global::CSIFLEX.MonitoringUnits.UI.Properties.Resources.Refresh;
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Location = new System.Drawing.Point(1134, 56);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(25, 25);
            this.btnRefresh.TabIndex = 28;
            this.btnRefresh.Paint += new System.Windows.Forms.PaintEventHandler(this.btnRefresh_Paint);
            // 
            // chkShowDeleted
            // 
            this.chkShowDeleted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowDeleted.AutoSize = true;
            this.chkShowDeleted.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkShowDeleted.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowDeleted.Location = new System.Drawing.Point(976, 60);
            this.chkShowDeleted.Name = "chkShowDeleted";
            this.chkShowDeleted.Size = new System.Drawing.Size(152, 21);
            this.chkShowDeleted.TabIndex = 27;
            this.chkShowDeleted.Text = "Show deleted boards";
            this.chkShowDeleted.UseVisualStyleBackColor = true;
            this.chkShowDeleted.CheckedChanged += new System.EventHandler(this.chkShowDeleted_CheckedChanged);
            // 
            // lblLinkSettings
            // 
            this.lblLinkSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLinkSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblLinkSettings.Font = new System.Drawing.Font("Segoe UI", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLinkSettings.ForeColor = System.Drawing.Color.Blue;
            this.lblLinkSettings.Location = new System.Drawing.Point(7, 547);
            this.lblLinkSettings.Name = "lblLinkSettings";
            this.lblLinkSettings.Size = new System.Drawing.Size(294, 23);
            this.lblLinkSettings.TabIndex = 26;
            this.lblLinkSettings.Text = "Open Unit Settings";
            this.lblLinkSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblLinkSettings.Click += new System.EventHandler(this.lblLinkSettings_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Image = global::CSIFLEX.MonitoringUnits.UI.Properties.Resources.csiflex_logo_blue1;
            this.pictureBox1.Location = new System.Drawing.Point(9, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(158, 63);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // btnExportToCsv
            // 
            this.btnExportToCsv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportToCsv.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportToCsv.Location = new System.Drawing.Point(986, 541);
            this.btnExportToCsv.Name = "btnExportToCsv";
            this.btnExportToCsv.Size = new System.Drawing.Size(173, 32);
            this.btnExportToCsv.TabIndex = 12;
            this.btnExportToCsv.Text = "Export to CSV file";
            this.btnExportToCsv.UseVisualStyleBackColor = true;
            this.btnExportToCsv.Click += new System.EventHandler(this.btnExportToCsv_Click);
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AllowUserToOrderColumns = true;
            this.dgvData.AllowUserToResizeRows = false;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SensorId,
            this.SensorName,
            this.Timestamp,
            this.CurrentTime,
            this.IsMonitoring,
            this.SensorOnline,
            this.IsOverride,
            this.IsAlarming,
            this.IsCSD,
            this.Pallet,
            this.CurrentPallet,
            this.Metric,
            this.Value});
            this.dgvData.Location = new System.Drawing.Point(9, 309);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.Size = new System.Drawing.Size(1150, 226);
            this.dgvData.TabIndex = 11;
            // 
            // btnLoadData
            // 
            this.btnLoadData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadData.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadData.Location = new System.Drawing.Point(1017, 249);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(142, 52);
            this.btnLoadData.TabIndex = 10;
            this.btnLoadData.Text = "Load Data";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(857, 218);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 26);
            this.label5.TabIndex = 9;
            this.label5.Text = "End Period";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(693, 218);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 26);
            this.label4.TabIndex = 8;
            this.label4.Text = "Start Period";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(529, 218);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 26);
            this.label3.TabIndex = 7;
            this.label3.Text = "Data Type";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(861, 249);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(150, 29);
            this.dtpEnd.TabIndex = 6;
            // 
            // dtpStart
            // 
            this.dtpStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(697, 249);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(150, 29);
            this.dtpStart.TabIndex = 5;
            this.dtpStart.ValueChanged += new System.EventHandler(this.dtpStart_ValueChanged);
            // 
            // cmbDataType
            // 
            this.cmbDataType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDataType.FormattingEnabled = true;
            this.cmbDataType.Items.AddRange(new object[] {
            "Pressure",
            "Temperature",
            "Both"});
            this.cmbDataType.Location = new System.Drawing.Point(533, 249);
            this.cmbDataType.Name = "cmbDataType";
            this.cmbDataType.Size = new System.Drawing.Size(150, 29);
            this.cmbDataType.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(274, 218);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 26);
            this.label2.TabIndex = 3;
            this.label2.Text = "Sensors";
            // 
            // clbSensors
            // 
            this.clbSensors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clbSensors.FormattingEnabled = true;
            this.clbSensors.Location = new System.Drawing.Point(278, 249);
            this.clbSensors.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clbSensors.Name = "clbSensors";
            this.clbSensors.Size = new System.Drawing.Size(240, 52);
            this.clbSensors.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(174, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(609, 42);
            this.label1.TabIndex = 1;
            this.label1.Text = "Monitoring Unit Analytics";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // dgvUnits
            // 
            this.dgvUnits.AllowUserToAddRows = false;
            this.dgvUnits.AllowUserToDeleteRows = false;
            this.dgvUnits.AllowUserToOrderColumns = true;
            this.dgvUnits.AllowUserToResizeColumns = false;
            this.dgvUnits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUnits.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUnits.ColumnHeadersHeight = 38;
            this.dgvUnits.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.BoardLabel,
            this.BoardIpAddress,
            this.BoardMacAddress,
            this.BoardMachineName,
            this.BoardManufacturer,
            this.BoardModel,
            this.BoardSerialNumber,
            this.BoardFirmware,
            this.BoardDeleted});
            this.dgvUnits.Location = new System.Drawing.Point(9, 86);
            this.dgvUnits.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvUnits.Name = "dgvUnits";
            this.dgvUnits.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUnits.Size = new System.Drawing.Size(1150, 122);
            this.dgvUnits.TabIndex = 0;
            this.dgvUnits.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvUnits_CurrentCellDirtyStateChanged);
            this.dgvUnits.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvUnits_RowPostPaint);
            this.dgvUnits.SelectionChanged += new System.EventHandler(this.dgvUnits_SelectionChanged);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // BoardLabel
            // 
            this.BoardLabel.DataPropertyName = "Label";
            this.BoardLabel.HeaderText = "Label";
            this.BoardLabel.Name = "BoardLabel";
            this.BoardLabel.ReadOnly = true;
            // 
            // BoardIpAddress
            // 
            this.BoardIpAddress.DataPropertyName = "IpAddress";
            this.BoardIpAddress.HeaderText = "IP Address";
            this.BoardIpAddress.Name = "BoardIpAddress";
            this.BoardIpAddress.ReadOnly = true;
            // 
            // BoardMacAddress
            // 
            this.BoardMacAddress.DataPropertyName = "Mac";
            this.BoardMacAddress.HeaderText = "MAC Address";
            this.BoardMacAddress.Name = "BoardMacAddress";
            this.BoardMacAddress.ReadOnly = true;
            // 
            // BoardMachineName
            // 
            this.BoardMachineName.DataPropertyName = "MachineName";
            this.BoardMachineName.HeaderText = "Machine";
            this.BoardMachineName.Name = "BoardMachineName";
            this.BoardMachineName.ReadOnly = true;
            // 
            // BoardManufacturer
            // 
            this.BoardManufacturer.DataPropertyName = "Manufacturer";
            this.BoardManufacturer.HeaderText = "Manufacturer";
            this.BoardManufacturer.Name = "BoardManufacturer";
            this.BoardManufacturer.ReadOnly = true;
            // 
            // BoardModel
            // 
            this.BoardModel.DataPropertyName = "Model";
            this.BoardModel.HeaderText = "Model";
            this.BoardModel.Name = "BoardModel";
            this.BoardModel.ReadOnly = true;
            // 
            // BoardSerialNumber
            // 
            this.BoardSerialNumber.DataPropertyName = "SerialNumber";
            this.BoardSerialNumber.HeaderText = "Serial #";
            this.BoardSerialNumber.Name = "BoardSerialNumber";
            this.BoardSerialNumber.ReadOnly = true;
            // 
            // BoardFirmware
            // 
            this.BoardFirmware.DataPropertyName = "Firmware";
            this.BoardFirmware.HeaderText = "Firmware";
            this.BoardFirmware.Name = "BoardFirmware";
            this.BoardFirmware.ReadOnly = true;
            // 
            // BoardDeleted
            // 
            this.BoardDeleted.DataPropertyName = "Deleted";
            this.BoardDeleted.HeaderText = "Deleted";
            this.BoardDeleted.Name = "BoardDeleted";
            this.BoardDeleted.ReadOnly = true;
            this.BoardDeleted.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Location = new System.Drawing.Point(18, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1168, 309);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.chartSensors);
            this.panel1.Location = new System.Drawing.Point(6, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1156, 285);
            this.panel1.TabIndex = 0;
            // 
            // chartSensors
            // 
            this.chartSensors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chartSensors.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartSensors.Legends.Add(legend1);
            this.chartSensors.Location = new System.Drawing.Point(3, 3);
            this.chartSensors.Name = "chartSensors";
            this.chartSensors.Size = new System.Drawing.Size(1150, 282);
            this.chartSensors.TabIndex = 0;
            this.chartSensors.Text = "chart1";
            this.chartSensors.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chartSensors_MouseClick);
            this.chartSensors.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chartSensors_MouseDoubleClick);
            this.chartSensors.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chartSensors_MouseMove);
            // 
            // SensorId
            // 
            this.SensorId.DataPropertyName = "Id";
            this.SensorId.HeaderText = "Id";
            this.SensorId.Name = "SensorId";
            this.SensorId.ReadOnly = true;
            this.SensorId.Visible = false;
            // 
            // SensorName
            // 
            this.SensorName.DataPropertyName = "SensorName";
            this.SensorName.HeaderText = "Sensor";
            this.SensorName.Name = "SensorName";
            this.SensorName.ReadOnly = true;
            // 
            // Timestamp
            // 
            this.Timestamp.DataPropertyName = "Timestamp";
            this.Timestamp.HeaderText = "Time";
            this.Timestamp.Name = "Timestamp";
            this.Timestamp.ReadOnly = true;
            // 
            // CurrentTime
            // 
            this.CurrentTime.DataPropertyName = "CurrentTime";
            this.CurrentTime.HeaderText = "Current Time";
            this.CurrentTime.Name = "CurrentTime";
            this.CurrentTime.ReadOnly = true;
            // 
            // IsMonitoring
            // 
            this.IsMonitoring.DataPropertyName = "IsMonitoring";
            this.IsMonitoring.HeaderText = "Monitoring";
            this.IsMonitoring.Name = "IsMonitoring";
            this.IsMonitoring.ReadOnly = true;
            // 
            // SensorOnline
            // 
            this.SensorOnline.DataPropertyName = "IsSensorAvailable";
            this.SensorOnline.HeaderText = "Sensor Online";
            this.SensorOnline.Name = "SensorOnline";
            this.SensorOnline.ReadOnly = true;
            // 
            // IsOverride
            // 
            this.IsOverride.DataPropertyName = "IsOverride";
            this.IsOverride.HeaderText = "Override";
            this.IsOverride.Name = "IsOverride";
            this.IsOverride.ReadOnly = true;
            this.IsOverride.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsOverride.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // IsAlarming
            // 
            this.IsAlarming.DataPropertyName = "IsAlarming";
            this.IsAlarming.HeaderText = "Alarming";
            this.IsAlarming.Name = "IsAlarming";
            this.IsAlarming.ReadOnly = true;
            // 
            // IsCSD
            // 
            this.IsCSD.DataPropertyName = "IsCSD";
            this.IsCSD.HeaderText = "CSD";
            this.IsCSD.Name = "IsCSD";
            this.IsCSD.ReadOnly = true;
            // 
            // Pallet
            // 
            this.Pallet.DataPropertyName = "SensorGroup";
            this.Pallet.HeaderText = "Sensor Pallet";
            this.Pallet.Name = "Pallet";
            this.Pallet.ReadOnly = true;
            // 
            // CurrentPallet
            // 
            this.CurrentPallet.DataPropertyName = "CurrentPallet";
            this.CurrentPallet.HeaderText = "Current Pallet";
            this.CurrentPallet.Name = "CurrentPallet";
            this.CurrentPallet.ReadOnly = true;
            // 
            // Metric
            // 
            this.Metric.DataPropertyName = "Metric";
            this.Metric.HeaderText = "Metric";
            this.Metric.Name = "Metric";
            this.Metric.ReadOnly = true;
            // 
            // Value
            // 
            this.Value.DataPropertyName = "Value";
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            // 
            // UnitsDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1204, 925);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "UnitsDataForm";
            this.Load += new System.EventHandler(this.UnitsDataForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnits)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartSensors)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvUnits;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.ComboBox cmbDataType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox clbSensors;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btnExportToCsv;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSensors;
        private System.Windows.Forms.PictureBox pictureBox1;
        internal System.Windows.Forms.Label lblLinkSettings;
        private System.Windows.Forms.CheckBox chkShowDeleted;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn BoardLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn BoardIpAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn BoardMacAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn BoardMachineName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BoardManufacturer;
        private System.Windows.Forms.DataGridViewTextBoxColumn BoardModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn BoardSerialNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn BoardFirmware;
        private System.Windows.Forms.DataGridViewCheckBoxColumn BoardDeleted;
        private System.Windows.Forms.Panel btnRefresh;
        private System.Windows.Forms.Label lblMachineName;
        private System.Windows.Forms.Label lblBoardLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn SensorId;
        private System.Windows.Forms.DataGridViewTextBoxColumn SensorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Timestamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentTime;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsMonitoring;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SensorOnline;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsOverride;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsAlarming;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsCSD;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pallet;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentPallet;
        private System.Windows.Forms.DataGridViewTextBoxColumn Metric;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
    }
}