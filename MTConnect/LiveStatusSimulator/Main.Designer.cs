namespace LiveStatusSimulator
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
            this.BTN_StopThreads = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BTN_StopThreads
            // 
            this.BTN_StopThreads.Location = new System.Drawing.Point(79, 144);
            this.BTN_StopThreads.Name = "BTN_StopThreads";
            this.BTN_StopThreads.Size = new System.Drawing.Size(97, 39);
            this.BTN_StopThreads.TabIndex = 0;
            this.BTN_StopThreads.Text = "Stop threads";
            this.BTN_StopThreads.UseVisualStyleBackColor = true;
            this.BTN_StopThreads.Click += new System.EventHandler(this.BTN_StopThreads_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.BTN_StopThreads);
            this.Name = "Main";
            this.Text = "Live Status Simulator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BTN_StopThreads;
    }
}

