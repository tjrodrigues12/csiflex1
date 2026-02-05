namespace ConfigBackup
{
    partial class Main
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
            this.BTN_Backup = new System.Windows.Forms.Button();
            this.LBL_Result = new System.Windows.Forms.Label();
            this.LBL_ImpRes = new System.Windows.Forms.Label();
            this.BTN_Import = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // BTN_Backup
            // 
            this.BTN_Backup.Location = new System.Drawing.Point(132, 261);
            this.BTN_Backup.Margin = new System.Windows.Forms.Padding(4);
            this.BTN_Backup.Name = "BTN_Backup";
            this.BTN_Backup.Size = new System.Drawing.Size(356, 44);
            this.BTN_Backup.TabIndex = 0;
            this.BTN_Backup.Text = "Backup configuration";
            this.BTN_Backup.UseVisualStyleBackColor = true;
            this.BTN_Backup.Click += new System.EventHandler(this.BTN_Backup_Click);
            // 
            // LBL_Result
            // 
            this.LBL_Result.AutoEllipsis = true;
            this.LBL_Result.Location = new System.Drawing.Point(132, 309);
            this.LBL_Result.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LBL_Result.Name = "LBL_Result";
            this.LBL_Result.Size = new System.Drawing.Size(356, 49);
            this.LBL_Result.TabIndex = 1;
            this.LBL_Result.Text = "Result:";
            // 
            // LBL_ImpRes
            // 
            this.LBL_ImpRes.AutoEllipsis = true;
            this.LBL_ImpRes.Location = new System.Drawing.Point(132, 409);
            this.LBL_ImpRes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LBL_ImpRes.Name = "LBL_ImpRes";
            this.LBL_ImpRes.Size = new System.Drawing.Size(356, 48);
            this.LBL_ImpRes.TabIndex = 3;
            this.LBL_ImpRes.Text = "Result:";
            this.LBL_ImpRes.Visible = false;
            // 
            // BTN_Import
            // 
            this.BTN_Import.Location = new System.Drawing.Point(132, 361);
            this.BTN_Import.Margin = new System.Windows.Forms.Padding(4);
            this.BTN_Import.Name = "BTN_Import";
            this.BTN_Import.Size = new System.Drawing.Size(356, 44);
            this.BTN_Import.TabIndex = 2;
            this.BTN_Import.Text = "Import configuration";
            this.BTN_Import.UseVisualStyleBackColor = true;
            this.BTN_Import.Visible = false;
            this.BTN_Import.Click += new System.EventHandler(this.BTN_Import_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.Location = new System.Drawing.Point(88, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(449, 72);
            this.label1.TabIndex = 6;
            this.label1.Text = "This tool allows you to make a backup of your CSIFlex \r\nsettings, to imports them" +
    " in a new installation.\r\n\r\nYou will find your backups files in the same folder t" +
    "han this software\r\n";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ConfigBackup.Properties.Resources.snapshot_manager;
            this.pictureBox2.Location = new System.Drawing.Point(253, 355);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(118, 103);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ConfigBackup.Properties.Resources.Capture;
            this.pictureBox1.Location = new System.Drawing.Point(135, 26);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(356, 103);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(621, 466);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.LBL_ImpRes);
            this.Controls.Add(this.BTN_Import);
            this.Controls.Add(this.LBL_Result);
            this.Controls.Add(this.BTN_Backup);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.Text = "CSIFlex configuration backup";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BTN_Backup;
        private System.Windows.Forms.Label LBL_Result;
        private System.Windows.Forms.Label LBL_ImpRes;
        private System.Windows.Forms.Button BTN_Import;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
    }
}

