using CSIFLEX.Database.Access;
using CSIFLEX.Utilities;
using CsvHelper;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSIFLEX.DataMigration
{
    public partial class DataMigration : Form
    {
        string basePath = Path.Combine(Directory.GetCurrentDirectory(), "DatabaseBackup");
        string importPath;

        string exportDatabase = "";
        string importDatabase = "";

        bool ipIsChecked = false;

        public DataMigration()
        {
            InitializeComponent();
        }

        private void DataMigration_Load(object sender, EventArgs e)
        {
            Log.Instance.Init("CSIFLEX.DataMigration");
            Log.Info("CSIFLEX.DataMigration started!");

            LoginFrm loginFrm = new LoginFrm();

            if (loginFrm.ShowDialog() != DialogResult.OK)
            {
                this.Close();
                return;
            }

            txtMySQLPath.Text = @"C:\Program Files (x86)\CSI Flex Server\mysql\mysql-5.7.21-win32\bin";

            if (!Directory.Exists(txtMySQLPath.Text))
                txtMySQLPath.ForeColor = Color.Red;


            DataTable tablesAuth = MySqlAccess.GetDataTable($"SELECT table_name FROM information_schema.tables WHERE table_schema = 'csi_auth' AND table_type = 'BASE TABLE' ORDER BY table_name;");
            DataTable tablesDatabase = MySqlAccess.GetDataTable($"SELECT table_name FROM information_schema.tables WHERE table_schema = 'csi_database' AND table_type = 'BASE TABLE' ORDER BY table_name;");

            foreach(DataRow row in tablesAuth.Rows)
            {
                dataGridExport.Rows.Add(false, $"csi_auth.{row["table_name"]}");
            }

            foreach (DataRow row in tablesDatabase.Rows)
            {
                dataGridExport.Rows.Add(false, $"csi_database.{row["table_name"]}");
            }


            exportDatabase = Global.Database;
            importDatabase = Global.Database;
            txtDatabaseAddress.Text = importDatabase;

            lblExport.Text = $"Export configuration from {Global.Database}";

            //dataGridExport.Rows.Add(false, "csi_auth.auto_report_config");
            //dataGridExport.Rows.Add(false, "csi_auth.ftpconfig");
            ////dataGridExport.Rows.Add(false, "csi_auth.tbl_autoreport_status");
            //dataGridExport.Rows.Add(false, "csi_auth.tbl_csiconnector");
            //dataGridExport.Rows.Add(false, "csi_auth.tbl_csiothersettings");
            //dataGridExport.Rows.Add(false, "csi_auth.tbl_ehub_conf");
            //dataGridExport.Rows.Add(false, "csi_auth.tbl_emailreports");
            //dataGridExport.Rows.Add(false, "csi_auth.tbl_file_status");
            //dataGridExport.Rows.Add(false, "csi_auth.tbl_mtcfocasconditions");
            //dataGridExport.Rows.Add(false, "csi_auth.tbl_serviceconfig");
            //dataGridExport.Rows.Add(false, "csi_auth.tbl_updatestatus");
            //dataGridExport.Rows.Add(false, "csi_auth.users");
            //dataGridExport.Rows.Add(false, "csi_database.tbl_colors");
            //dataGridExport.Rows.Add(false, "csi_database.tbl_deviceconfig");
            //dataGridExport.Rows.Add(false, "csi_database.tbl_deviceconfig2");
            //dataGridExport.Rows.Add(false, "csi_database.tbl_devices");
            //dataGridExport.Rows.Add(false, "csi_database.tbl_externalsource");
            //dataGridExport.Rows.Add(false, "csi_database.tbl_groups");
            //dataGridExport.Rows.Add(false, "csi_database.tbl_livestatut");
            //dataGridExport.Rows.Add(false, "csi_database.tbl_messages");
            //dataGridExport.Rows.Add(false, "csi_database.tbl_renamemachines");
            //dataGridExport.Rows.Add(false, "csi_database.tbl_userconfig");
        }


        private void btnExport_Click(object sender, EventArgs e)
        {
            string tableName;
            string database;
            string query;
            string exportPath;

            DataTable table;
            List<dynamic> records;
            dynamic record;
            StreamWriter writer;

            this.Cursor = Cursors.WaitCursor;

            if (!Directory.Exists(txtMySQLPath.Text))
            {
                MessageBox.Show("MySQL Path doesn't exist!");
                return;
            }

            if (chkFullDatabase.Checked)
            {
                MySQLUtils.BackupDatabase(txtMySQLPath.Text, cmbDatabases.Text);
            }
            else
            {
                foreach (DataGridViewRow file in dataGridExport.Rows)
                {
                    if (Boolean.Parse(file.Cells[0].Value.ToString()))
                    {
                        string[] path = file.Cells[1].Value.ToString().Split('.');
                        database = path[0];
                        tableName = path[1];

                        //tableName = file.Cells[1].Value.ToString();


                        if (!MySQLUtils.BackupTable(txtMySQLPath.Text, database, tableName))
                        {
                            if (MessageBox.Show($"Attention! Error trying to generate the backup file for the table '{tableName}'. \n\nDo you want to continue?", "Backup error!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error) != DialogResult.Yes)
                            {
                                break;
                            }
                        }


                        //query = $"SELECT * FROM {tableName}";

                        //table = MySqlAccess.GetDataTable(query, Global.ConnectionString);

                        //if (table.Rows.Count > 0)
                        //{
                        //    exportPath = Path.Combine(basePath, $"{tableName}.csv");

                        //    if (!Directory.Exists(basePath))
                        //    {
                        //        Directory.CreateDirectory(basePath);
                        //    }

                        //    writer = new StreamWriter(exportPath);

                        //    records = new List<dynamic>();

                        //    using (var csv = new CsvWriter(writer))
                        //    {
                        //        foreach (DataRow row in table.Rows)
                        //        {
                        //            record = new ExpandoObject();

                        //            foreach (DataColumn column in table.Columns)
                        //            {
                        //                AddProperty(record, column.ColumnName, row[column].ToString());
                        //            }
                        //            records.Add(record);
                        //        }

                        //        csv.WriteRecords(records);
                        //    }
                        //}
                    }
                }
            }

            this.Cursor = Cursors.Default;

            MessageBox.Show("Exportation completed");
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            Process.Start(basePath);
        }

        private void checkAllTables_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            foreach (DataGridViewRow row in dataGridExport.Rows)
            {
                row.Cells[0].Value = check.Checked;
            }
        }



        private void btnImportFolder_Click(object sender, EventArgs e)
        {
            using (var fldrDlg = new FolderBrowserDialog())
            {
                if (fldrDlg.ShowDialog() == DialogResult.OK)
                {
                    importPath = fldrDlg.SelectedPath;
                    txtImportFolder.Text = importPath;

                    dataGridImport.Rows.Clear();

                    DirectoryInfo di = new DirectoryInfo(importPath);
                    FileInfo[] files = di.GetFiles("*.csv");

                    foreach(FileInfo file in files)
                    {
                        dataGridImport.Rows.Add(false, Path.GetFileNameWithoutExtension(file.Name), file.FullName);
                    }
                }
            }
        }

        private void txtDatabaseAddress_TextChanged(object sender, EventArgs e)
        {
            btnCheck.Text = "Check";
            btnCheck.BackColor = Color.Transparent;
            ipIsChecked = false;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtDatabaseAddress.Text.Trim()))
            {
                MessageBox.Show("You must enter the database and check the connection");
                return;
            }

            try
            {
                importDatabase = txtDatabaseAddress.Text;
                Global.Database = importDatabase;
                if (MySqlAccess.hasConnection(Global.ConnectionString))
                {
                    btnCheck.Text = "Okay";
                    btnCheck.BackColor = Color.Green;
                    ipIsChecked = true;
                }
                else
                {
                    btnCheck.Text = "Error";
                    btnCheck.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                btnCheck.Text = "Error";
                btnCheck.BackColor = Color.Red;
                MessageBox.Show(ex.Message);
            }
        }

        private void checkAllFiles_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            foreach (DataGridViewRow row in dataGridImport.Rows)
            {
                row.Cells[0].Value = check.Checked;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            string fileFullPath;
            string database;
            bool hasFileSelected = false;

            if (!ipIsChecked)
            {
                MessageBox.Show("You must check the database connection");
                return;
            }

            foreach (DataGridViewRow row in dataGridImport.Rows)
            {
                if (Boolean.Parse(row.Cells[0].Value.ToString()))
                {
                    hasFileSelected = true;

                    fileFullPath = row.Cells[2].Value.ToString();

                    FileInfo file = new FileInfo(fileFullPath);
                    var fileName = file.Name.Split('-');

                    database = fileName[1];

                    MySQLUtils.RestoreFile(txtMySQLPath.Text, fileFullPath, database);


                    //table = row.Cells[1].Value.ToString();
                    //lblTable.Text = table;
                    
                    //using (TextFieldParser csvParser = new TextFieldParser(fileFullPath))
                    //{
                    //    csvParser.CommentTokens = new string[] { "#" };
                    //    csvParser.SetDelimiters(new string[] { "," });
                    //    csvParser.HasFieldsEnclosedInQuotes = true;

                    //    string[] header = csvParser.ReadLine().Split(',');

                    //    while (!csvParser.EndOfData)
                    //    {
                    //        string[] fields = csvParser.ReadFields();

                    //        try
                    //        {
                    //            MySqlAccess.InputGenericRecord(table, header, fields, false, Global.ConnectionString);
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            MessageBox.Show(ex.Message, $"Error importing the table {table}");
                    //        }
                    //    }
                    //}
                }
            }

            if (!hasFileSelected)
                MessageBox.Show("No files selected to be imported");
            else
                MessageBox.Show("Importation completed");
        }


        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tab = (TabControl)sender;

            if(tab.SelectedIndex == 0)
            {
                Global.Database = exportDatabase;
            }
            else
            {
                Global.Database = importDatabase;

                if (String.IsNullOrEmpty(txtImportFolder.Text))
                {
                    txtImportFolder.Text = basePath;
                }

                dataGridImport.Rows.Clear();

                if (Directory.Exists(txtImportFolder.Text))
                {
                    DirectoryInfo di = new DirectoryInfo(txtImportFolder.Text);
                    FileInfo[] files = di.GetFiles("*.sql");

                    foreach (FileInfo file in files)
                    {
                        dataGridImport.Rows.Add(false, Path.GetFileNameWithoutExtension(file.Name), file.FullName);
                    }
                }

                btnCheck_Click(this, new EventArgs());
            }
        }


        private static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            // ExpandoObject supports IDictionary so we can extend it like this
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }


        private void btnPathDialog_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            txtMySQLPath.Text = dialog.SelectedPath;
        }

        private void chkFullDatabase_CheckedChanged(object sender, EventArgs e)
        {
            cmbDatabases.Enabled = true;
            cmbDatabases.SelectedIndex = 0;

            grpTables.Enabled = false;
            foreach (DataGridViewRow row in dataGridImport.Rows)
            {
                row.Cells[0].Value = false;
            }
        }

        private void chkSelectedTables_CheckedChanged(object sender, EventArgs e)
        {
            cmbDatabases.Enabled = false;
            cmbDatabases.SelectedIndex = -1;

            grpTables.Enabled = true;
        }

        private void txtMySQLPath_TextChanged(object sender, EventArgs e)
        {
            if (txtMySQLPath.Text.ToUpper().StartsWith("DR"))
            {
                txtMySQLPath.Text = @"C:\CSIFLEX\MySQL\mysql-8.0.18-winx64\bin";
            }
        }
    }
}
