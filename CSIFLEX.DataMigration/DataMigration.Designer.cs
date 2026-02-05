namespace CSIFLEX.DataMigration
{
    partial class DataMigration
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataMigration));
            this.label2 = new System.Windows.Forms.Label();
            this.grpTables = new System.Windows.Forms.GroupBox();
            this.checkAllTables = new System.Windows.Forms.CheckBox();
            this.dataGridExport = new System.Windows.Forms.DataGridView();
            this.checkColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tableNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExport = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabExport = new System.Windows.Forms.TabPage();
            this.btnFolder = new System.Windows.Forms.Button();
            this.lblExport = new System.Windows.Forms.Label();
            this.tabImport = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblTable = new System.Windows.Forms.Label();
            this.dataGridImport = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fullPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkAllFiles = new System.Windows.Forms.CheckBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.txtImportFolder = new System.Windows.Forms.TextBox();
            this.btnImportFolder = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDatabaseAddress = new System.Windows.Forms.TextBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtMySQLPath = new System.Windows.Forms.TextBox();
            this.btnPathDialog = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.chkFullDatabase = new System.Windows.Forms.RadioButton();
            this.chkSelectedTables = new System.Windows.Forms.RadioButton();
            this.cmbDatabases = new System.Windows.Forms.ComboBox();
            this.grpTables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridExport)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabExport.SuspendLayout();
            this.tabImport.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridImport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(275, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(302, 85);
            this.label2.TabIndex = 8;
            this.label2.Text = "Data Migration";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpTables
            // 
            this.grpTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTables.Controls.Add(this.checkAllTables);
            this.grpTables.Controls.Add(this.dataGridExport);
            this.grpTables.Enabled = false;
            this.grpTables.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTables.Location = new System.Drawing.Point(6, 141);
            this.grpTables.Name = "grpTables";
            this.grpTables.Size = new System.Drawing.Size(643, 457);
            this.grpTables.TabIndex = 9;
            this.grpTables.TabStop = false;
            this.grpTables.Text = "  Tables  ";
            // 
            // checkAllTables
            // 
            this.checkAllTables.AutoSize = true;
            this.checkAllTables.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkAllTables.Location = new System.Drawing.Point(17, 27);
            this.checkAllTables.Name = "checkAllTables";
            this.checkAllTables.Size = new System.Drawing.Size(122, 20);
            this.checkAllTables.TabIndex = 1;
            this.checkAllTables.Text = "Select all tables";
            this.checkAllTables.UseVisualStyleBackColor = true;
            this.checkAllTables.CheckedChanged += new System.EventHandler(this.checkAllTables_CheckedChanged);
            // 
            // dataGridExport
            // 
            this.dataGridExport.AllowUserToAddRows = false;
            this.dataGridExport.AllowUserToDeleteRows = false;
            this.dataGridExport.AllowUserToResizeColumns = false;
            this.dataGridExport.AllowUserToResizeRows = false;
            this.dataGridExport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridExport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridExport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkColumn,
            this.tableNameColumn});
            this.dataGridExport.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dataGridExport.Location = new System.Drawing.Point(6, 50);
            this.dataGridExport.Name = "dataGridExport";
            this.dataGridExport.RowHeadersVisible = false;
            this.dataGridExport.Size = new System.Drawing.Size(631, 401);
            this.dataGridExport.TabIndex = 0;
            // 
            // checkColumn
            // 
            this.checkColumn.HeaderText = "";
            this.checkColumn.Name = "checkColumn";
            this.checkColumn.Width = 30;
            // 
            // tableNameColumn
            // 
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableNameColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.tableNameColumn.HeaderText = "Table Name";
            this.tableNameColumn.Name = "tableNameColumn";
            this.tableNameColumn.ReadOnly = true;
            this.tableNameColumn.Width = 450;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExport.Location = new System.Drawing.Point(6, 604);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(215, 43);
            this.btnExport.TabIndex = 10;
            this.btnExport.Text = "Export Tables";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabExport);
            this.tabControl.Controls.Add(this.tabImport);
            this.tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.Location = new System.Drawing.Point(12, 150);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(663, 686);
            this.tabControl.TabIndex = 11;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabExport
            // 
            this.tabExport.Controls.Add(this.cmbDatabases);
            this.tabExport.Controls.Add(this.chkSelectedTables);
            this.tabExport.Controls.Add(this.chkFullDatabase);
            this.tabExport.Controls.Add(this.btnFolder);
            this.tabExport.Controls.Add(this.lblExport);
            this.tabExport.Controls.Add(this.grpTables);
            this.tabExport.Controls.Add(this.btnExport);
            this.tabExport.Location = new System.Drawing.Point(4, 29);
            this.tabExport.Name = "tabExport";
            this.tabExport.Padding = new System.Windows.Forms.Padding(3);
            this.tabExport.Size = new System.Drawing.Size(655, 653);
            this.tabExport.TabIndex = 0;
            this.tabExport.Text = "  Export  ";
            this.tabExport.UseVisualStyleBackColor = true;
            // 
            // btnFolder
            // 
            this.btnFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFolder.Image = global::CSIFLEX.DataMigration.Properties.Resources.open_folder;
            this.btnFolder.Location = new System.Drawing.Point(600, 604);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(43, 43);
            this.btnFolder.TabIndex = 12;
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // lblExport
            // 
            this.lblExport.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExport.Location = new System.Drawing.Point(6, 10);
            this.lblExport.Name = "lblExport";
            this.lblExport.Size = new System.Drawing.Size(643, 48);
            this.lblExport.TabIndex = 11;
            this.lblExport.Text = "Data Migration";
            this.lblExport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabImport
            // 
            this.tabImport.Controls.Add(this.groupBox2);
            this.tabImport.Controls.Add(this.btnImport);
            this.tabImport.Controls.Add(this.txtImportFolder);
            this.tabImport.Controls.Add(this.btnImportFolder);
            this.tabImport.Controls.Add(this.label3);
            this.tabImport.Controls.Add(this.txtDatabaseAddress);
            this.tabImport.Controls.Add(this.btnCheck);
            this.tabImport.Controls.Add(this.label1);
            this.tabImport.Location = new System.Drawing.Point(4, 29);
            this.tabImport.Name = "tabImport";
            this.tabImport.Padding = new System.Windows.Forms.Padding(3);
            this.tabImport.Size = new System.Drawing.Size(655, 653);
            this.tabImport.TabIndex = 1;
            this.tabImport.Text = "  Import  ";
            this.tabImport.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTable);
            this.groupBox2.Controls.Add(this.dataGridImport);
            this.groupBox2.Controls.Add(this.checkAllFiles);
            this.groupBox2.Location = new System.Drawing.Point(6, 108);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(643, 490);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "  Files  ";
            // 
            // lblTable
            // 
            this.lblTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTable.Location = new System.Drawing.Point(6, 462);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(631, 25);
            this.lblTable.TabIndex = 19;
            // 
            // dataGridImport
            // 
            this.dataGridImport.AllowUserToAddRows = false;
            this.dataGridImport.AllowUserToDeleteRows = false;
            this.dataGridImport.AllowUserToResizeColumns = false;
            this.dataGridImport.AllowUserToResizeRows = false;
            this.dataGridImport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridImport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridImport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn1,
            this.fullPath});
            this.dataGridImport.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dataGridImport.Location = new System.Drawing.Point(6, 63);
            this.dataGridImport.Name = "dataGridImport";
            this.dataGridImport.RowHeadersVisible = false;
            this.dataGridImport.Size = new System.Drawing.Size(631, 392);
            this.dataGridImport.TabIndex = 17;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 30;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn1.HeaderText = "Files Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 450;
            // 
            // fullPath
            // 
            this.fullPath.HeaderText = "";
            this.fullPath.Name = "fullPath";
            this.fullPath.Visible = false;
            // 
            // checkAllFiles
            // 
            this.checkAllFiles.AutoSize = true;
            this.checkAllFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkAllFiles.Location = new System.Drawing.Point(17, 37);
            this.checkAllFiles.Name = "checkAllFiles";
            this.checkAllFiles.Size = new System.Drawing.Size(109, 20);
            this.checkAllFiles.TabIndex = 18;
            this.checkAllFiles.Text = "Select all files";
            this.checkAllFiles.UseVisualStyleBackColor = true;
            this.checkAllFiles.CheckedChanged += new System.EventHandler(this.checkAllFiles_CheckedChanged);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnImport.Location = new System.Drawing.Point(10, 604);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(215, 43);
            this.btnImport.TabIndex = 19;
            this.btnImport.Text = "Import Files";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // txtImportFolder
            // 
            this.txtImportFolder.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImportFolder.Location = new System.Drawing.Point(188, 60);
            this.txtImportFolder.Name = "txtImportFolder";
            this.txtImportFolder.ReadOnly = true;
            this.txtImportFolder.Size = new System.Drawing.Size(402, 26);
            this.txtImportFolder.TabIndex = 16;
            // 
            // btnImportFolder
            // 
            this.btnImportFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportFolder.Location = new System.Drawing.Point(596, 60);
            this.btnImportFolder.Name = "btnImportFolder";
            this.btnImportFolder.Size = new System.Drawing.Size(53, 26);
            this.btnImportFolder.TabIndex = 15;
            this.btnImportFolder.Text = "...";
            this.btnImportFolder.UseVisualStyleBackColor = true;
            this.btnImportFolder.Click += new System.EventHandler(this.btnImportFolder_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 29);
            this.label3.TabIndex = 14;
            this.label3.Text = "Folder to import:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDatabaseAddress
            // 
            this.txtDatabaseAddress.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDatabaseAddress.Location = new System.Drawing.Point(188, 12);
            this.txtDatabaseAddress.Name = "txtDatabaseAddress";
            this.txtDatabaseAddress.Size = new System.Drawing.Size(402, 32);
            this.txtDatabaseAddress.TabIndex = 13;
            this.txtDatabaseAddress.TextChanged += new System.EventHandler(this.txtDatabaseAddress_TextChanged);
            // 
            // btnCheck
            // 
            this.btnCheck.BackColor = System.Drawing.Color.Transparent;
            this.btnCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheck.Location = new System.Drawing.Point(596, 11);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(53, 34);
            this.btnCheck.TabIndex = 12;
            this.btnCheck.Text = "Check";
            this.btnCheck.UseVisualStyleBackColor = false;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Database (IP address):";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(257, 85);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // txtMySQLPath
            // 
            this.txtMySQLPath.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMySQLPath.Location = new System.Drawing.Point(167, 108);
            this.txtMySQLPath.Name = "txtMySQLPath";
            this.txtMySQLPath.Size = new System.Drawing.Size(455, 27);
            this.txtMySQLPath.TabIndex = 16;
            this.txtMySQLPath.TextChanged += new System.EventHandler(this.txtMySQLPath_TextChanged);
            // 
            // btnPathDialog
            // 
            this.btnPathDialog.BackColor = System.Drawing.Color.Transparent;
            this.btnPathDialog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPathDialog.Location = new System.Drawing.Point(628, 108);
            this.btnPathDialog.Name = "btnPathDialog";
            this.btnPathDialog.Size = new System.Drawing.Size(31, 27);
            this.btnPathDialog.TabIndex = 15;
            this.btnPathDialog.Text = "...";
            this.btnPathDialog.UseVisualStyleBackColor = false;
            this.btnPathDialog.Click += new System.EventHandler(this.btnPathDialog_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(24, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 29);
            this.label4.TabIndex = 14;
            this.label4.Text = "MySQL Path:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkFullDatabase
            // 
            this.chkFullDatabase.AutoSize = true;
            this.chkFullDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFullDatabase.Location = new System.Drawing.Point(6, 61);
            this.chkFullDatabase.Name = "chkFullDatabase";
            this.chkFullDatabase.Size = new System.Drawing.Size(123, 24);
            this.chkFullDatabase.TabIndex = 17;
            this.chkFullDatabase.TabStop = true;
            this.chkFullDatabase.Text = "Full database";
            this.chkFullDatabase.UseVisualStyleBackColor = true;
            this.chkFullDatabase.CheckedChanged += new System.EventHandler(this.chkFullDatabase_CheckedChanged);
            // 
            // chkSelectedTables
            // 
            this.chkSelectedTables.AutoSize = true;
            this.chkSelectedTables.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSelectedTables.Location = new System.Drawing.Point(6, 99);
            this.chkSelectedTables.Name = "chkSelectedTables";
            this.chkSelectedTables.Size = new System.Drawing.Size(137, 24);
            this.chkSelectedTables.TabIndex = 18;
            this.chkSelectedTables.TabStop = true;
            this.chkSelectedTables.Text = "Selected tables";
            this.chkSelectedTables.UseVisualStyleBackColor = true;
            this.chkSelectedTables.CheckedChanged += new System.EventHandler(this.chkSelectedTables_CheckedChanged);
            // 
            // cmbDatabases
            // 
            this.cmbDatabases.Enabled = false;
            this.cmbDatabases.FormattingEnabled = true;
            this.cmbDatabases.Items.AddRange(new object[] {
            "CSI_AUTH",
            "CSI_DATABASE"});
            this.cmbDatabases.Location = new System.Drawing.Point(151, 60);
            this.cmbDatabases.Name = "cmbDatabases";
            this.cmbDatabases.Size = new System.Drawing.Size(455, 28);
            this.cmbDatabases.TabIndex = 19;
            // 
            // DataMigration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 864);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtMySQLPath);
            this.Controls.Add(this.btnPathDialog);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "DataMigration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DataMigration";
            this.Load += new System.EventHandler(this.DataMigration_Load);
            this.grpTables.ResumeLayout(false);
            this.grpTables.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridExport)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabExport.ResumeLayout(false);
            this.tabExport.PerformLayout();
            this.tabImport.ResumeLayout(false);
            this.tabImport.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridImport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox grpTables;
        private System.Windows.Forms.CheckBox checkAllTables;
        private System.Windows.Forms.DataGridView dataGridExport;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tableNameColumn;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabExport;
        private System.Windows.Forms.Label lblExport;
        private System.Windows.Forms.TabPage tabImport;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.TextBox txtImportFolder;
        private System.Windows.Forms.Button btnImportFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDatabaseAddress;
        private System.Windows.Forms.DataGridView dataGridImport;
        private System.Windows.Forms.CheckBox checkAllFiles;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullPath;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.TextBox txtMySQLPath;
        private System.Windows.Forms.Button btnPathDialog;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbDatabases;
        private System.Windows.Forms.RadioButton chkSelectedTables;
        private System.Windows.Forms.RadioButton chkFullDatabase;
    }
}