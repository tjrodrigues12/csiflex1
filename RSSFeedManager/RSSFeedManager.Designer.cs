namespace RSSFeedManager
{
    partial class RSSFeedManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RSSFeedManager));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpPeriod = new System.Windows.Forms.GroupBox();
            this.rdbMonth = new System.Windows.Forms.RadioButton();
            this.rdbWeek = new System.Windows.Forms.RadioButton();
            this.rdbDay = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.drbSystemMessage = new System.Windows.Forms.RadioButton();
            this.rdbUserMessage = new System.Windows.Forms.RadioButton();
            this.chkAddOnTop = new System.Windows.Forms.CheckBox();
            this.chkAllDashboards = new System.Windows.Forms.CheckBox();
            this.btnAddMessage = new System.Windows.Forms.Button();
            this.txtNewMessage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDeleteMessge = new System.Windows.Forms.Button();
            this.btnDownMessage = new System.Windows.Forms.Button();
            this.btnUpMessage = new System.Windows.Forms.Button();
            this.dataGridMessages = new System.Windows.Forms.DataGridView();
            this.Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lstDashboards = new System.Windows.Forms.ListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpPeriod.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMessages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnSaveChanges);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(664, 661);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(275, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(376, 85);
            this.label2.TabIndex = 6;
            this.label2.Text = "RSS Feed Manager";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveChanges.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveChanges.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSaveChanges.Location = new System.Drawing.Point(421, 608);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(209, 43);
            this.btnSaveChanges.TabIndex = 5;
            this.btnSaveChanges.Text = "Save the changes";
            this.btnSaveChanges.UseVisualStyleBackColor = true;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.grpPeriod);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.chkAddOnTop);
            this.groupBox1.Controls.Add(this.chkAllDashboards);
            this.groupBox1.Controls.Add(this.btnAddMessage);
            this.groupBox1.Controls.Add(this.txtNewMessage);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.lstDashboards);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(640, 502);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Dashboards ";
            // 
            // grpPeriod
            // 
            this.grpPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grpPeriod.Controls.Add(this.rdbMonth);
            this.grpPeriod.Controls.Add(this.rdbWeek);
            this.grpPeriod.Controls.Add(this.rdbDay);
            this.grpPeriod.Enabled = false;
            this.grpPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPeriod.Location = new System.Drawing.Point(286, 352);
            this.grpPeriod.Name = "grpPeriod";
            this.grpPeriod.Size = new System.Drawing.Size(251, 49);
            this.grpPeriod.TabIndex = 8;
            this.grpPeriod.TabStop = false;
            this.grpPeriod.Text = "Best CYCLE ON Machine ";
            // 
            // rdbMonth
            // 
            this.rdbMonth.AutoSize = true;
            this.rdbMonth.Location = new System.Drawing.Point(131, 23);
            this.rdbMonth.Name = "rdbMonth";
            this.rdbMonth.Size = new System.Drawing.Size(65, 21);
            this.rdbMonth.TabIndex = 3;
            this.rdbMonth.TabStop = true;
            this.rdbMonth.Text = "Month";
            this.rdbMonth.UseVisualStyleBackColor = true;
            this.rdbMonth.CheckedChanged += new System.EventHandler(this.rdbPeriod_CheckedChanged);
            // 
            // rdbWeek
            // 
            this.rdbWeek.AutoSize = true;
            this.rdbWeek.Location = new System.Drawing.Point(63, 23);
            this.rdbWeek.Name = "rdbWeek";
            this.rdbWeek.Size = new System.Drawing.Size(62, 21);
            this.rdbWeek.TabIndex = 2;
            this.rdbWeek.TabStop = true;
            this.rdbWeek.Text = "Week";
            this.rdbWeek.UseVisualStyleBackColor = true;
            this.rdbWeek.CheckedChanged += new System.EventHandler(this.rdbPeriod_CheckedChanged);
            // 
            // rdbDay
            // 
            this.rdbDay.AutoSize = true;
            this.rdbDay.Location = new System.Drawing.Point(6, 22);
            this.rdbDay.Name = "rdbDay";
            this.rdbDay.Size = new System.Drawing.Size(51, 21);
            this.rdbDay.TabIndex = 1;
            this.rdbDay.TabStop = true;
            this.rdbDay.Text = "Day";
            this.rdbDay.UseVisualStyleBackColor = true;
            this.rdbDay.CheckedChanged += new System.EventHandler(this.rdbPeriod_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.drbSystemMessage);
            this.groupBox3.Controls.Add(this.rdbUserMessage);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(6, 352);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(274, 49);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " Message Type ";
            // 
            // drbSystemMessage
            // 
            this.drbSystemMessage.AutoSize = true;
            this.drbSystemMessage.Location = new System.Drawing.Point(129, 22);
            this.drbSystemMessage.Name = "drbSystemMessage";
            this.drbSystemMessage.Size = new System.Drawing.Size(133, 21);
            this.drbSystemMessage.TabIndex = 1;
            this.drbSystemMessage.Text = "System Message";
            this.drbSystemMessage.UseVisualStyleBackColor = true;
            this.drbSystemMessage.CheckedChanged += new System.EventHandler(this.rdbMessageType_CheckedChanged);
            // 
            // rdbUserMessage
            // 
            this.rdbUserMessage.AutoSize = true;
            this.rdbUserMessage.Checked = true;
            this.rdbUserMessage.Location = new System.Drawing.Point(6, 23);
            this.rdbUserMessage.Name = "rdbUserMessage";
            this.rdbUserMessage.Size = new System.Drawing.Size(117, 21);
            this.rdbUserMessage.TabIndex = 0;
            this.rdbUserMessage.TabStop = true;
            this.rdbUserMessage.Text = "User Message";
            this.rdbUserMessage.UseVisualStyleBackColor = true;
            this.rdbUserMessage.CheckedChanged += new System.EventHandler(this.rdbMessageType_CheckedChanged);
            // 
            // chkAddOnTop
            // 
            this.chkAddOnTop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAddOnTop.AutoSize = true;
            this.chkAddOnTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAddOnTop.Location = new System.Drawing.Point(245, 465);
            this.chkAddOnTop.Name = "chkAddOnTop";
            this.chkAddOnTop.Size = new System.Drawing.Size(120, 21);
            this.chkAddOnTop.TabIndex = 6;
            this.chkAddOnTop.Text = "Add on the top";
            this.chkAddOnTop.UseVisualStyleBackColor = true;
            // 
            // chkAllDashboards
            // 
            this.chkAllDashboards.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAllDashboards.AutoSize = true;
            this.chkAllDashboards.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAllDashboards.Location = new System.Drawing.Point(6, 465);
            this.chkAllDashboards.Name = "chkAllDashboards";
            this.chkAllDashboards.Size = new System.Drawing.Size(164, 21);
            this.chkAllDashboards.TabIndex = 5;
            this.chkAllDashboards.Text = "Add in all dashboards";
            this.chkAllDashboards.UseVisualStyleBackColor = true;
            // 
            // btnAddMessage
            // 
            this.btnAddMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddMessage.Location = new System.Drawing.Point(479, 459);
            this.btnAddMessage.Name = "btnAddMessage";
            this.btnAddMessage.Size = new System.Drawing.Size(139, 29);
            this.btnAddMessage.TabIndex = 4;
            this.btnAddMessage.Text = "Add Message";
            this.btnAddMessage.UseVisualStyleBackColor = true;
            this.btnAddMessage.Click += new System.EventHandler(this.btnAddMessage_Click);
            // 
            // txtNewMessage
            // 
            this.txtNewMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewMessage.Location = new System.Drawing.Point(6, 427);
            this.txtNewMessage.Name = "txtNewMessage";
            this.txtNewMessage.Size = new System.Drawing.Size(612, 23);
            this.txtNewMessage.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 407);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "New message:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnDeleteMessge);
            this.groupBox2.Controls.Add(this.btnDownMessage);
            this.groupBox2.Controls.Add(this.btnUpMessage);
            this.groupBox2.Controls.Add(this.dataGridMessages);
            this.groupBox2.Location = new System.Drawing.Point(6, 149);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(625, 197);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Messages ";
            // 
            // btnDeleteMessge
            // 
            this.btnDeleteMessge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteMessge.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteMessge.Location = new System.Drawing.Point(537, 162);
            this.btnDeleteMessge.Name = "btnDeleteMessge";
            this.btnDeleteMessge.Size = new System.Drawing.Size(75, 29);
            this.btnDeleteMessge.TabIndex = 3;
            this.btnDeleteMessge.Text = "Delete";
            this.btnDeleteMessge.UseVisualStyleBackColor = true;
            this.btnDeleteMessge.Click += new System.EventHandler(this.btnDeleteMessge_Click);
            // 
            // btnDownMessage
            // 
            this.btnDownMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownMessage.Location = new System.Drawing.Point(537, 127);
            this.btnDownMessage.Name = "btnDownMessage";
            this.btnDownMessage.Size = new System.Drawing.Size(75, 29);
            this.btnDownMessage.TabIndex = 2;
            this.btnDownMessage.Text = "Down";
            this.btnDownMessage.UseVisualStyleBackColor = true;
            this.btnDownMessage.Click += new System.EventHandler(this.btnDownMessage_Click);
            // 
            // btnUpMessage
            // 
            this.btnUpMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpMessage.Location = new System.Drawing.Point(537, 92);
            this.btnUpMessage.Name = "btnUpMessage";
            this.btnUpMessage.Size = new System.Drawing.Size(75, 29);
            this.btnUpMessage.TabIndex = 1;
            this.btnUpMessage.Text = "Up";
            this.btnUpMessage.UseVisualStyleBackColor = true;
            this.btnUpMessage.Click += new System.EventHandler(this.btnUpMessage_Click);
            // 
            // dataGridMessages
            // 
            this.dataGridMessages.AllowUserToAddRows = false;
            this.dataGridMessages.AllowUserToDeleteRows = false;
            this.dataGridMessages.AllowUserToOrderColumns = true;
            this.dataGridMessages.AllowUserToResizeColumns = false;
            this.dataGridMessages.AllowUserToResizeRows = false;
            this.dataGridMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridMessages.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridMessages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridMessages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Message});
            this.dataGridMessages.Location = new System.Drawing.Point(6, 25);
            this.dataGridMessages.Name = "dataGridMessages";
            this.dataGridMessages.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridMessages.Size = new System.Drawing.Size(525, 166);
            this.dataGridMessages.TabIndex = 0;
            // 
            // Message
            // 
            this.Message.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Message.HeaderText = "Message";
            this.Message.Name = "Message";
            this.Message.ReadOnly = true;
            // 
            // lstDashboards
            // 
            this.lstDashboards.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDashboards.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDashboards.FormattingEnabled = true;
            this.lstDashboards.ItemHeight = 16;
            this.lstDashboards.Location = new System.Drawing.Point(6, 27);
            this.lstDashboards.Name = "lstDashboards";
            this.lstDashboards.Size = new System.Drawing.Size(625, 116);
            this.lstDashboards.TabIndex = 0;
            this.lstDashboards.Click += new System.EventHandler(this.lstDashboards_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(257, 85);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // RSSFeedManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 661);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(680, 700);
            this.Name = "RSSFeedManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RSS Feed Manager";
            this.Load += new System.EventHandler(this.RSSFeedManager_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpPeriod.ResumeLayout(false);
            this.grpPeriod.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMessages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstDashboards;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDownMessage;
        private System.Windows.Forms.Button btnUpMessage;
        private System.Windows.Forms.DataGridView dataGridMessages;
        private System.Windows.Forms.Button btnSaveChanges;
        private System.Windows.Forms.CheckBox chkAddOnTop;
        private System.Windows.Forms.CheckBox chkAllDashboards;
        private System.Windows.Forms.Button btnAddMessage;
        private System.Windows.Forms.TextBox txtNewMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDeleteMessge;
        private System.Windows.Forms.GroupBox grpPeriod;
        private System.Windows.Forms.RadioButton rdbMonth;
        private System.Windows.Forms.RadioButton rdbWeek;
        private System.Windows.Forms.RadioButton rdbDay;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton drbSystemMessage;
        private System.Windows.Forms.RadioButton rdbUserMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn Message;
        private System.Windows.Forms.Label label2;
    }
}