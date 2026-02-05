using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.Text;

namespace TreeViewSerialization
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Button btnLoad;
		private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnViewXml;
        private System.ComponentModel.IContainer components;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnViewXml = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = this.treeView1.ImageIndex;
            this.treeView1.Size = new System.Drawing.Size(440, 440);
            this.treeView1.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            this.imageList1.Images.SetKeyName(7, "");
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(355, 451);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(274, 451);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnViewXml
            // 
            this.btnViewXml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnViewXml.Location = new System.Drawing.Point(11, 451);
            this.btnViewXml.Name = "btnViewXml";
            this.btnViewXml.Size = new System.Drawing.Size(93, 23);
            this.btnViewXml.TabIndex = 3;
            this.btnViewXml.Text = "View Xml File";
            this.btnViewXml.Click += new System.EventHandler(this.btnViewXml_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(440, 486);
            this.Controls.Add(this.btnViewXml);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.treeView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "TreeView Serialization";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
            //this.LoadSampleData();
		}

        private void btnSave_Click(object sender, System.EventArgs e)
		{
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = Application.StartupPath + "\\..\\..\\MyTreeView.xml";
            if(saveFile.ShowDialog() != DialogResult.OK) return;

			TreeViewSerializer serializer = new TreeViewSerializer();
			serializer.SerializeTreeView(this.treeView1, saveFile.FileName);
		}

		private void btnLoad_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog openFile = new OpenFileDialog();
            openFile.FileName = Application.StartupPath + "\\..\\..\\MyTreeView.xml";
			if (openFile.ShowDialog() != DialogResult.OK) return;
            this.treeView1.Nodes.Clear();
			TreeViewSerializer serializer = new TreeViewSerializer();
			serializer.DeserializeTreeView(this.treeView1, openFile.FileName);

			
		}

        private void LoadSampleData()
        {
            TreeNode n;
            this.treeView1.Nodes.Add("Asia").ImageIndex = 0;
            this.treeView1.Nodes.Add("Europe").ImageIndex = 1;
            this.treeView1.Nodes.Add("America").ImageIndex = 2;
            this.treeView1.Nodes.Add("Africa").ImageIndex = 3;
            n = this.treeView1.Nodes[0].Nodes.Add("China");
            n.Tag = "Largest Population";
            n.Nodes.Add("Beijing");
            this.treeView1.Nodes[0].Nodes.Add("Pakistan").ImageIndex = 4;
            this.treeView1.Nodes[0].Nodes.Add("India").ImageIndex = 5;
            this.treeView1.Nodes[0].Nodes.Add("Srilanka").ImageIndex = 6;
            this.treeView1.Nodes[1].Nodes.Add("Germany").ImageIndex = 6;
        }

        private void btnViewXml_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.FileName = Application.StartupPath + "\\..\\..\\MyTreeView.xml";
            if(openFile.ShowDialog() != DialogResult.OK) return;

            this.treeView1.Nodes.Clear();
            TreeViewSerializer serializer = new TreeViewSerializer();
            serializer.LoadXmlFileInTreeView(this.treeView1, openFile.FileName);        
        }
	}
}
