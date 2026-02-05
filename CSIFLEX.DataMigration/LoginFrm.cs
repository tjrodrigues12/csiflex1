using CSIFLEX.Database.Access;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace CSIFLEX.DataMigration
{
    public partial class LoginFrm : Form
    {
        string configPath = Path.Combine(Directory.GetCurrentDirectory(), "config.json");

        Configuration configuration;

        public LoginFrm()
        {
            InitializeComponent();
        }

        private void LoginFrm_Load(object sender, EventArgs e)
        {
            if (File.Exists(configPath))
            {
                using(StreamReader reader = new StreamReader(configPath))
                {
                    var json = reader.ReadToEnd();
                    configuration = JsonConvert.DeserializeObject<Configuration>(json);
                    Global.Database = configuration.Database;

                    txtDatabaseAddress.Text = configuration.Database;
                    txtUser.Text = configuration.User;
                }
            }

            if (String.IsNullOrEmpty(txtDatabaseAddress.Text))
            {
                txtDatabaseAddress.Focus();
            } else if (String.IsNullOrEmpty(txtUser.Text))
            {
                txtUser.Focus();
            } else
            {
                txtPassword.Focus();
            }

            btnCheck_Click(this, new EventArgs());
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtDatabaseAddress.Text.Trim()))
            {
                MessageBox.Show("You must enter the database");
                txtDatabaseAddress.Focus();
                return;
            }

            try
            {
                Global.Database = txtDatabaseAddress.Text;
                if (MySqlAccess.hasConnection(Global.ConnectionString))
                {
                    lblStatus.Text = "Connection Okay";
                    lblStatus.ForeColor = Color.Green;
                    txtPassword.Focus();
                }
                else
                {
                    lblStatus.Text = "Connection Error";
                    lblStatus.ForeColor = Color.Red;
                    txtDatabaseAddress.Focus();
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Connection Error";
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtDatabaseAddress.Text.Trim()))
            {
                MessageBox.Show("You must enter the database");
                return;
            }
            if (String.IsNullOrEmpty(txtUser.Text.Trim()))
            {
                MessageBox.Show("You must enter the user name");
                return;
            }
            if (String.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                MessageBox.Show("You must enter the password");
                return;
            }


            if (configuration == null)
                configuration = new Configuration();

            configuration.Database = txtDatabaseAddress.Text;
            configuration.User = txtUser.Text;

            Global.Database = txtDatabaseAddress.Text;

            DataTable dt = MySqlAccess.GetUser(txtUser.Text, Global.ConnectionString);

            //if(dt.Rows.Count > 0)
            //{
            //    string user = dt.Rows[0]["username_"].ToString();
            //    string pswd = dt.Rows[0]["password_"].ToString();
            //    string salt = dt.Rows[0]["salt_"].ToString();

            //    var hash_64 = Convert.ToBase64String(ComputePBKDF2Hash(txtPassword.Text, Convert.FromBase64String(salt)));

            //    if (hash_64 != pswd)
            //    {
            //        MessageBox.Show("Invalid password. Please try again!");
            //        return;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("This user does not exist.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            string json = JsonConvert.SerializeObject(configuration);
            File.WriteAllText(configPath, json);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }



        private const int SALT_SIZE = 32; // size in bytes
        private const int HASH_SIZE = 32; // size in bytes
        private const int ITERATIONS = 10000; // number of pbkdf2 iterations

        private static byte[] ComputePBKDF2Hash(string input, byte[] salt, int iterations = ITERATIONS, int hashSize = HASH_SIZE)
        {
            // Generate the hash
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(input, salt, iterations);
            return pbkdf2.GetBytes(hashSize);
        }

        private void LoginFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(this.DialogResult != DialogResult.OK)
                this.DialogResult = DialogResult.Cancel;
        }
    }
}
