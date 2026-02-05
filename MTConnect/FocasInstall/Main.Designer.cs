namespace FocasInstall
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.CB_Controllers = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BTN_InstallAsService = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_Path = new System.Windows.Forms.TextBox();
            this.BTN_Browse = new System.Windows.Forms.Button();
            this.BTN_RemoveService = new System.Windows.Forms.Button();
            this.BTN_TestAdapter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CB_Controllers
            // 
            this.CB_Controllers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Controllers.FormattingEnabled = true;
            this.CB_Controllers.Items.AddRange(new object[] {
            "0i",
            "15i",
            "30i"});
            this.CB_Controllers.Location = new System.Drawing.Point(34, 52);
            this.CB_Controllers.Name = "CB_Controllers";
            this.CB_Controllers.Size = new System.Drawing.Size(136, 21);
            this.CB_Controllers.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select your Fanuc controller";
            // 
            // BTN_InstallAsService
            // 
            this.BTN_InstallAsService.Location = new System.Drawing.Point(34, 169);
            this.BTN_InstallAsService.Name = "BTN_InstallAsService";
            this.BTN_InstallAsService.Size = new System.Drawing.Size(136, 23);
            this.BTN_InstallAsService.TabIndex = 2;
            this.BTN_InstallAsService.Text = "Install adapter as service";
            this.BTN_InstallAsService.UseVisualStyleBackColor = true;
            this.BTN_InstallAsService.Click += new System.EventHandler(this.BTN_InstallAsService_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Where do you want to install?";
            // 
            // TB_Path
            // 
            this.TB_Path.Location = new System.Drawing.Point(34, 104);
            this.TB_Path.Name = "TB_Path";
            this.TB_Path.ReadOnly = true;
            this.TB_Path.Size = new System.Drawing.Size(185, 20);
            this.TB_Path.TabIndex = 5;
            // 
            // BTN_Browse
            // 
            this.BTN_Browse.Location = new System.Drawing.Point(225, 102);
            this.BTN_Browse.Name = "BTN_Browse";
            this.BTN_Browse.Size = new System.Drawing.Size(87, 23);
            this.BTN_Browse.TabIndex = 6;
            this.BTN_Browse.Text = "Browse";
            this.BTN_Browse.UseVisualStyleBackColor = true;
            this.BTN_Browse.Click += new System.EventHandler(this.BTN_Browse_Click);
            // 
            // BTN_RemoveService
            // 
            this.BTN_RemoveService.Location = new System.Drawing.Point(176, 169);
            this.BTN_RemoveService.Name = "BTN_RemoveService";
            this.BTN_RemoveService.Size = new System.Drawing.Size(136, 23);
            this.BTN_RemoveService.TabIndex = 7;
            this.BTN_RemoveService.Text = "Remove adapter service";
            this.BTN_RemoveService.UseVisualStyleBackColor = true;
            this.BTN_RemoveService.Click += new System.EventHandler(this.BTN_RemoveService_Click);
            // 
            // BTN_TestAdapter
            // 
            this.BTN_TestAdapter.Location = new System.Drawing.Point(34, 140);
            this.BTN_TestAdapter.Name = "BTN_TestAdapter";
            this.BTN_TestAdapter.Size = new System.Drawing.Size(136, 23);
            this.BTN_TestAdapter.TabIndex = 8;
            this.BTN_TestAdapter.Text = "Test adapter";
            this.BTN_TestAdapter.UseVisualStyleBackColor = true;
            this.BTN_TestAdapter.Click += new System.EventHandler(this.BTN_TestAdapter_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 221);
            this.Controls.Add(this.BTN_TestAdapter);
            this.Controls.Add(this.BTN_RemoveService);
            this.Controls.Add(this.BTN_Browse);
            this.Controls.Add(this.TB_Path);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BTN_InstallAsService);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CB_Controllers);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "Focas Installer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CB_Controllers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BTN_InstallAsService;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TB_Path;
        private System.Windows.Forms.Button BTN_Browse;
        private System.Windows.Forms.Button BTN_RemoveService;
        private System.Windows.Forms.Button BTN_TestAdapter;
    }
}

