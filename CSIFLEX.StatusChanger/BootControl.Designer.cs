namespace CSIFLEX.StatusChanger
{
    partial class BootControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.MachineId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MachineName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CycleOnTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CycleOffTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MachineStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BootStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 40;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MachineId,
            this.MachineName,
            this.CycleOnTime,
            this.CycleOffTime,
            this.MachineStatus,
            this.BootStatus});
            this.dataGridView1.Location = new System.Drawing.Point(3, 54);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(835, 296);
            this.dataGridView1.TabIndex = 0;
            // 
            // MachineId
            // 
            this.MachineId.DataPropertyName = "MachineId";
            this.MachineId.FillWeight = 45.68528F;
            this.MachineId.HeaderText = "Id";
            this.MachineId.Name = "MachineId";
            this.MachineId.ReadOnly = true;
            // 
            // MachineName
            // 
            this.MachineName.DataPropertyName = "MachineName";
            this.MachineName.FillWeight = 110.8629F;
            this.MachineName.HeaderText = "Machine";
            this.MachineName.Name = "MachineName";
            this.MachineName.ReadOnly = true;
            // 
            // CycleOnTime
            // 
            this.CycleOnTime.DataPropertyName = "CycleOnTime";
            this.CycleOnTime.FillWeight = 110.8629F;
            this.CycleOnTime.HeaderText = "Cycle On Time";
            this.CycleOnTime.Name = "CycleOnTime";
            this.CycleOnTime.ReadOnly = true;
            // 
            // CycleOffTime
            // 
            this.CycleOffTime.DataPropertyName = "CycleOffTime";
            this.CycleOffTime.FillWeight = 110.8629F;
            this.CycleOffTime.HeaderText = "Cycle Off Time";
            this.CycleOffTime.Name = "CycleOffTime";
            this.CycleOffTime.ReadOnly = true;
            // 
            // MachineStatus
            // 
            this.MachineStatus.DataPropertyName = "MachineStatus";
            this.MachineStatus.FillWeight = 110.8629F;
            this.MachineStatus.HeaderText = "Machine Status";
            this.MachineStatus.Name = "MachineStatus";
            this.MachineStatus.ReadOnly = true;
            // 
            // BootStatus
            // 
            this.BootStatus.DataPropertyName = "BootStatus";
            this.BootStatus.FillWeight = 110.8629F;
            this.BootStatus.HeaderText = "Boot Status";
            this.BootStatus.Name = "BootStatus";
            this.BootStatus.ReadOnly = true;
            // 
            // BootControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Name = "BootControl";
            this.Size = new System.Drawing.Size(841, 839);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MachineId;
        private System.Windows.Forms.DataGridViewTextBoxColumn MachineName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CycleOnTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn CycleOffTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn MachineStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn BootStatus;
    }
}
