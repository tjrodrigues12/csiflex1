namespace CSIFLEX.StatusChanger
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.btnCheckFolder = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbMachines = new System.Windows.Forms.ComboBox();
            this.cmbCommand = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSendCommand = new System.Windows.Forms.Button();
            this.txtCommandsSend = new System.Windows.Forms.TextBox();
            this.txtUdpReturn = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.btnStartUdpFeedback = new System.Windows.Forms.Button();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.btnRefreshStatus = new System.Windows.Forms.Button();
            this.rbUDP = new System.Windows.Forms.RadioButton();
            this.rbFTP = new System.Windows.Forms.RadioButton();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.lblEnetPos = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnProduction = new System.Windows.Forms.Button();
            this.btnCycleOn = new System.Windows.Forms.Button();
            this.btnCycleOff = new System.Windows.Forms.Button();
            this.btnPartNumber = new System.Windows.Forms.Button();
            this.grpSetup = new System.Windows.Forms.GroupBox();
            this.txtCycleMultiplier = new System.Windows.Forms.TextBox();
            this.txtCycleTime = new System.Windows.Forms.TextBox();
            this.btnSetup = new System.Windows.Forms.Button();
            this.txtPartNumber = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbAPI = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbIpAddress = new System.Windows.Forms.ComboBox();
            this.lblMachineStatus = new System.Windows.Forms.Label();
            this.lblMachineName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtApiAddress = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grpSetup.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "eNETDNC Folder";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(7, 50);
            this.txtFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(250, 23);
            this.txtFolder.TabIndex = 0;
            this.txtFolder.Leave += new System.EventHandler(this.txtFolder_Leave);
            // 
            // btnCheckFolder
            // 
            this.btnCheckFolder.Location = new System.Drawing.Point(264, 50);
            this.btnCheckFolder.Name = "btnCheckFolder";
            this.btnCheckFolder.Size = new System.Drawing.Size(110, 23);
            this.btnCheckFolder.TabIndex = 1;
            this.btnCheckFolder.Text = "Load Settings";
            this.btnCheckFolder.UseVisualStyleBackColor = true;
            this.btnCheckFolder.Click += new System.EventHandler(this.btnCheckFolder_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(407, 19);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(203, 27);
            this.label2.TabIndex = 3;
            this.label2.Text = "Machines";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // cmbMachines
            // 
            this.cmbMachines.Enabled = false;
            this.cmbMachines.FormattingEnabled = true;
            this.cmbMachines.Location = new System.Drawing.Point(410, 51);
            this.cmbMachines.Name = "cmbMachines";
            this.cmbMachines.Size = new System.Drawing.Size(250, 23);
            this.cmbMachines.Sorted = true;
            this.cmbMachines.TabIndex = 2;
            this.cmbMachines.SelectedIndexChanged += new System.EventHandler(this.cmbMachines_SelectedIndexChanged);
            // 
            // cmbCommand
            // 
            this.cmbCommand.Enabled = false;
            this.cmbCommand.FormattingEnabled = true;
            this.cmbCommand.Location = new System.Drawing.Point(6, 42);
            this.cmbCommand.Name = "cmbCommand";
            this.cmbCommand.Size = new System.Drawing.Size(240, 23);
            this.cmbCommand.TabIndex = 3;
            this.cmbCommand.SelectedIndexChanged += new System.EventHandler(this.CmbCommand_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 12);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 27);
            this.label4.TabIndex = 7;
            this.label4.Text = "Command";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 138);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 27);
            this.label5.TabIndex = 10;
            this.label5.Text = "Protocol :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // btnSendCommand
            // 
            this.btnSendCommand.Enabled = false;
            this.btnSendCommand.Location = new System.Drawing.Point(6, 177);
            this.btnSendCommand.Name = "btnSendCommand";
            this.btnSendCommand.Size = new System.Drawing.Size(240, 42);
            this.btnSendCommand.TabIndex = 8;
            this.btnSendCommand.Text = "Send Command";
            this.btnSendCommand.UseVisualStyleBackColor = true;
            this.btnSendCommand.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // txtCommandsSend
            // 
            this.txtCommandsSend.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommandsSend.Location = new System.Drawing.Point(10, 3);
            this.txtCommandsSend.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtCommandsSend.Multiline = true;
            this.txtCommandsSend.Name = "txtCommandsSend";
            this.txtCommandsSend.ReadOnly = true;
            this.txtCommandsSend.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCommandsSend.Size = new System.Drawing.Size(908, 367);
            this.txtCommandsSend.TabIndex = 13;
            // 
            // txtUdpReturn
            // 
            this.txtUdpReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUdpReturn.Location = new System.Drawing.Point(10, 3);
            this.txtUdpReturn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtUdpReturn.Multiline = true;
            this.txtUdpReturn.Name = "txtUdpReturn";
            this.txtUdpReturn.ReadOnly = true;
            this.txtUdpReturn.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtUdpReturn.Size = new System.Drawing.Size(908, 173);
            this.txtUdpReturn.TabIndex = 14;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(2, 428);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnClearLog);
            this.splitContainer1.Panel1.Controls.Add(this.txtCommandsSend);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnStartUdpFeedback);
            this.splitContainer1.Panel2.Controls.Add(this.txtUdpReturn);
            this.splitContainer1.Size = new System.Drawing.Size(931, 616);
            this.splitContainer1.SplitterDistance = 401;
            this.splitContainer1.TabIndex = 16;
            // 
            // btnClearLog
            // 
            this.btnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearLog.Location = new System.Drawing.Point(808, 375);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(110, 23);
            this.btnClearLog.TabIndex = 10;
            this.btnClearLog.Text = "Clear";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // btnStartUdpFeedback
            // 
            this.btnStartUdpFeedback.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStartUdpFeedback.Location = new System.Drawing.Point(10, 182);
            this.btnStartUdpFeedback.Name = "btnStartUdpFeedback";
            this.btnStartUdpFeedback.Size = new System.Drawing.Size(158, 23);
            this.btnStartUdpFeedback.TabIndex = 11;
            this.btnStartUdpFeedback.Text = "Start UDP Feedback";
            this.btnStartUdpFeedback.UseVisualStyleBackColor = true;
            this.btnStartUdpFeedback.Click += new System.EventHandler(this.btnStartUdpFeedback_Click);
            // 
            // txtCommand
            // 
            this.txtCommand.Enabled = false;
            this.txtCommand.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtCommand.Location = new System.Drawing.Point(6, 69);
            this.txtCommand.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(240, 23);
            this.txtCommand.TabIndex = 4;
            // 
            // btnRefreshStatus
            // 
            this.btnRefreshStatus.Enabled = false;
            this.btnRefreshStatus.Location = new System.Drawing.Point(666, 93);
            this.btnRefreshStatus.Name = "btnRefreshStatus";
            this.btnRefreshStatus.Size = new System.Drawing.Size(85, 23);
            this.btnRefreshStatus.TabIndex = 9;
            this.btnRefreshStatus.Text = "Refresh Status";
            this.btnRefreshStatus.UseVisualStyleBackColor = true;
            this.btnRefreshStatus.Click += new System.EventHandler(this.btnRefreshStatus_Click);
            // 
            // rbUDP
            // 
            this.rbUDP.AutoSize = true;
            this.rbUDP.Checked = true;
            this.rbUDP.Enabled = false;
            this.rbUDP.Location = new System.Drawing.Point(93, 147);
            this.rbUDP.Name = "rbUDP";
            this.rbUDP.Size = new System.Drawing.Size(51, 19);
            this.rbUDP.TabIndex = 6;
            this.rbUDP.TabStop = true;
            this.rbUDP.Text = "UDP";
            this.rbUDP.UseVisualStyleBackColor = true;
            // 
            // rbFTP
            // 
            this.rbFTP.AutoSize = true;
            this.rbFTP.Enabled = false;
            this.rbFTP.Location = new System.Drawing.Point(150, 147);
            this.rbFTP.Name = "rbFTP";
            this.rbFTP.Size = new System.Drawing.Size(46, 19);
            this.rbFTP.TabIndex = 7;
            this.rbFTP.Text = "FTP";
            this.rbFTP.UseVisualStyleBackColor = true;
            // 
            // txtComment
            // 
            this.txtComment.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtComment.Location = new System.Drawing.Point(6, 118);
            this.txtComment.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(240, 23);
            this.txtComment.TabIndex = 5;
            // 
            // lblEnetPos
            // 
            this.lblEnetPos.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnetPos.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblEnetPos.Location = new System.Drawing.Point(59, 510);
            this.lblEnetPos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnetPos.Name = "lblEnetPos";
            this.lblEnetPos.Size = new System.Drawing.Size(65, 19);
            this.lblEnetPos.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 90);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 26);
            this.label3.TabIndex = 23;
            this.label3.Text = "Comment";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // btnProduction
            // 
            this.btnProduction.Enabled = false;
            this.btnProduction.Location = new System.Drawing.Point(15, 13);
            this.btnProduction.Name = "btnProduction";
            this.btnProduction.Size = new System.Drawing.Size(120, 30);
            this.btnProduction.TabIndex = 28;
            this.btnProduction.Text = "PRODUCTION";
            this.btnProduction.UseVisualStyleBackColor = true;
            this.btnProduction.Click += new System.EventHandler(this.btnProduction_Click);
            // 
            // btnCycleOn
            // 
            this.btnCycleOn.Enabled = false;
            this.btnCycleOn.Location = new System.Drawing.Point(141, 13);
            this.btnCycleOn.Name = "btnCycleOn";
            this.btnCycleOn.Size = new System.Drawing.Size(120, 30);
            this.btnCycleOn.TabIndex = 29;
            this.btnCycleOn.Text = "CYCLE ON";
            this.btnCycleOn.UseVisualStyleBackColor = true;
            this.btnCycleOn.Click += new System.EventHandler(this.btnCycleOn_Click);
            // 
            // btnCycleOff
            // 
            this.btnCycleOff.Enabled = false;
            this.btnCycleOff.Location = new System.Drawing.Point(141, 49);
            this.btnCycleOff.Name = "btnCycleOff";
            this.btnCycleOff.Size = new System.Drawing.Size(120, 30);
            this.btnCycleOff.TabIndex = 30;
            this.btnCycleOff.Text = "CYCLE OFF";
            this.btnCycleOff.UseVisualStyleBackColor = true;
            this.btnCycleOff.Click += new System.EventHandler(this.btnCycleOff_Click);
            // 
            // btnPartNumber
            // 
            this.btnPartNumber.Location = new System.Drawing.Point(78, 177);
            this.btnPartNumber.Name = "btnPartNumber";
            this.btnPartNumber.Size = new System.Drawing.Size(120, 42);
            this.btnPartNumber.TabIndex = 31;
            this.btnPartNumber.Text = "PART NUMBER";
            this.btnPartNumber.UseVisualStyleBackColor = true;
            this.btnPartNumber.Click += new System.EventHandler(this.btnPartNumber_Click);
            // 
            // grpSetup
            // 
            this.grpSetup.Controls.Add(this.txtCycleMultiplier);
            this.grpSetup.Controls.Add(this.txtCycleTime);
            this.grpSetup.Controls.Add(this.btnSetup);
            this.grpSetup.Controls.Add(this.txtPartNumber);
            this.grpSetup.Controls.Add(this.btnCycleOff);
            this.grpSetup.Controls.Add(this.btnCycleOn);
            this.grpSetup.Controls.Add(this.label10);
            this.grpSetup.Controls.Add(this.btnProduction);
            this.grpSetup.Controls.Add(this.label9);
            this.grpSetup.Controls.Add(this.label8);
            this.grpSetup.Controls.Add(this.btnPartNumber);
            this.grpSetup.Enabled = false;
            this.grpSetup.Location = new System.Drawing.Point(274, 132);
            this.grpSetup.Name = "grpSetup";
            this.grpSetup.Size = new System.Drawing.Size(267, 225);
            this.grpSetup.TabIndex = 32;
            this.grpSetup.TabStop = false;
            // 
            // txtCycleMultiplier
            // 
            this.txtCycleMultiplier.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtCycleMultiplier.Location = new System.Drawing.Point(98, 146);
            this.txtCycleMultiplier.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtCycleMultiplier.Name = "txtCycleMultiplier";
            this.txtCycleMultiplier.Size = new System.Drawing.Size(150, 23);
            this.txtCycleMultiplier.TabIndex = 36;
            // 
            // txtCycleTime
            // 
            this.txtCycleTime.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtCycleTime.Location = new System.Drawing.Point(98, 120);
            this.txtCycleTime.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtCycleTime.Name = "txtCycleTime";
            this.txtCycleTime.Size = new System.Drawing.Size(150, 23);
            this.txtCycleTime.TabIndex = 35;
            // 
            // btnSetup
            // 
            this.btnSetup.Location = new System.Drawing.Point(15, 49);
            this.btnSetup.Name = "btnSetup";
            this.btnSetup.Size = new System.Drawing.Size(120, 30);
            this.btnSetup.TabIndex = 33;
            this.btnSetup.Text = "SETUP";
            this.btnSetup.UseVisualStyleBackColor = true;
            this.btnSetup.Click += new System.EventHandler(this.btnSetup_Click);
            // 
            // txtPartNumber
            // 
            this.txtPartNumber.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtPartNumber.Location = new System.Drawing.Point(98, 94);
            this.txtPartNumber.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtPartNumber.Name = "txtPartNumber";
            this.txtPartNumber.Size = new System.Drawing.Size(150, 23);
            this.txtPartNumber.TabIndex = 33;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(16, 147);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 18);
            this.label10.TabIndex = 34;
            this.label10.Text = "Cycle mltp:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(16, 121);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 18);
            this.label9.TabIndex = 33;
            this.label9.Text = "Cycle time:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(16, 95);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 18);
            this.label8.TabIndex = 32;
            this.label8.Text = "Part Nbr:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.grpSetup);
            this.panel1.Location = new System.Drawing.Point(2, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(931, 372);
            this.panel1.TabIndex = 34;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtApiAddress);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(547, 132);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(371, 225);
            this.groupBox3.TabIndex = 33;
            this.groupBox3.TabStop = false;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Lucida Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 11);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 27);
            this.label7.TabIndex = 5;
            this.label7.Text = "API";
            this.label7.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbAPI);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cmbCommand);
            this.groupBox2.Controls.Add(this.txtCommand);
            this.groupBox2.Controls.Add(this.txtComment);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.rbUDP);
            this.groupBox2.Controls.Add(this.rbFTP);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnSendCommand);
            this.groupBox2.Location = new System.Drawing.Point(10, 132);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(257, 225);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // rbAPI
            // 
            this.rbAPI.AutoSize = true;
            this.rbAPI.Enabled = false;
            this.rbAPI.Location = new System.Drawing.Point(202, 147);
            this.rbAPI.Name = "rbAPI";
            this.rbAPI.Size = new System.Drawing.Size(44, 19);
            this.rbAPI.TabIndex = 24;
            this.rbAPI.Text = "API";
            this.rbAPI.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbIpAddress);
            this.groupBox1.Controls.Add(this.lblMachineStatus);
            this.groupBox1.Controls.Add(this.lblMachineName);
            this.groupBox1.Controls.Add(this.cmbMachines);
            this.groupBox1.Controls.Add(this.txtFolder);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnCheckFolder);
            this.groupBox1.Controls.Add(this.btnRefreshStatus);
            this.groupBox1.Location = new System.Drawing.Point(10, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(908, 123);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // cmbIpAddress
            // 
            this.cmbIpAddress.FormattingEnabled = true;
            this.cmbIpAddress.Location = new System.Drawing.Point(666, 50);
            this.cmbIpAddress.Name = "cmbIpAddress";
            this.cmbIpAddress.Size = new System.Drawing.Size(236, 23);
            this.cmbIpAddress.TabIndex = 10;
            // 
            // lblMachineStatus
            // 
            this.lblMachineStatus.Font = new System.Drawing.Font("Lucida Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMachineStatus.Location = new System.Drawing.Point(406, 89);
            this.lblMachineStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMachineStatus.Name = "lblMachineStatus";
            this.lblMachineStatus.Size = new System.Drawing.Size(253, 27);
            this.lblMachineStatus.TabIndex = 5;
            this.lblMachineStatus.Text = "eNETDNC Folder";
            this.lblMachineStatus.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblMachineName
            // 
            this.lblMachineName.Font = new System.Drawing.Font("Lucida Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMachineName.Location = new System.Drawing.Point(4, 89);
            this.lblMachineName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMachineName.Name = "lblMachineName";
            this.lblMachineName.Size = new System.Drawing.Size(253, 27);
            this.lblMachineName.TabIndex = 4;
            this.lblMachineName.Text = "eNETDNC Folder";
            this.lblMachineName.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Lucida Sans", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(7, 9);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(396, 38);
            this.label6.TabIndex = 1;
            this.label6.Text = "eNETDNC - Status Change";
            this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtApiAddress
            // 
            this.txtApiAddress.Enabled = false;
            this.txtApiAddress.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtApiAddress.Location = new System.Drawing.Point(11, 69);
            this.txtApiAddress.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtApiAddress.Name = "txtApiAddress";
            this.txtApiAddress.Size = new System.Drawing.Size(288, 23);
            this.txtApiAddress.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(8, 38);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(168, 27);
            this.label11.TabIndex = 8;
            this.label11.Text = "eNETDNC API Address";
            this.label11.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(306, 69);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(59, 23);
            this.button1.TabIndex = 31;
            this.button1.Text = "Check";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 1056);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblEnetPos);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Lucida Sans", 9.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(825, 540);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CSIFLEX Status Changer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.grpSetup.ResumeLayout(false);
            this.grpSetup.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Button btnCheckFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbMachines;
        private System.Windows.Forms.ComboBox cmbCommand;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSendCommand;
        private System.Windows.Forms.TextBox txtCommandsSend;
        private System.Windows.Forms.TextBox txtUdpReturn;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.Button btnRefreshStatus;
        private System.Windows.Forms.RadioButton rbUDP;
        private System.Windows.Forms.RadioButton rbFTP;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Button btnStartUdpFeedback;
        private System.Windows.Forms.Label lblEnetPos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnProduction;
        private System.Windows.Forms.Button btnCycleOn;
        private System.Windows.Forms.Button btnCycleOff;
        private System.Windows.Forms.Button btnPartNumber;
        private System.Windows.Forms.GroupBox grpSetup;
        private System.Windows.Forms.TextBox txtCycleMultiplier;
        private System.Windows.Forms.TextBox txtCycleTime;
        private System.Windows.Forms.TextBox txtPartNumber;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSetup;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMachineStatus;
        private System.Windows.Forms.Label lblMachineName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbIpAddress;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbAPI;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtApiAddress;
    }
}

