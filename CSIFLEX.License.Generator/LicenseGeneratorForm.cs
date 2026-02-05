using CSIFLEX.License.Data;
using CSIFLEX.Utilities;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSIFLEX.License.Generator
{
    public partial class LicenseGeneratorForm : Form
    {
        string activationFile = "CSIFLEXLicenseActivation.lic";

        LicenseRequest licenseRequest;

        public LicenseGeneratorForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbExpiry.SelectedIndex = 0;
            cmbTypeLicense.SelectedIndex = 0;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "BIN Files (*.bin*) | *.bin";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                CleanUp();

                string file = openFile.FileName;

                string content = File.ReadAllText(file, Encoding.UTF8);

                licenseRequest = JsonConvert.DeserializeObject<LicenseRequest>(content);

                txtCompanyName.Text = licenseRequest.CompanyName;
                txtContactPerson.Text = licenseRequest.ContactName;
                txtContactEmail.Text = licenseRequest.ContactEmail;
                txtContactPhone.Text = licenseRequest.PhoneNumber;
                lblComputerId.Text = licenseRequest.ComputerId;
                lblComputerName.Text = licenseRequest.ComputerName;

                var server = licenseRequest.Products.FirstOrDefault(p => p.ProductName == LicenseProducts.CSIFLEXServer);
                if (server != null)
                {
                    grpServer.Enabled = true;
                    chkServer.Checked = true;
                    if (server.ExpiryDate > DateTime.Parse("2000-01-01")) lblExpiryServer.Text = server.ExpiryDate.ToString("MMM-dd-yyyy");
                }

                var focas = licenseRequest.Products.FirstOrDefault(p => p.ProductName == LicenseProducts.CSIFLEXFocasMtc);
                if (focas != null)
                {
                    grpFocas.Enabled = true;
                    chkFocas.Checked = true;
                    txtNumberFocas.Text = focas.NumberLicenses.ToString();
                    if (focas.ExpiryDate > DateTime.Parse("2000-01-01")) lblExpiryFocas.Text = focas.ExpiryDate.ToString("MMM-dd-yyyy");
                }

                var mobile = licenseRequest.Products.FirstOrDefault(p => p.ProductName == LicenseProducts.CSIFLEXWebApp);
                if (mobile != null)
                {
                    grpMobile.Enabled = true;
                    chkMobile.Checked = true;
                    txtNumberDevices.Text = "1";
                    if (mobile.ExpiryDate > DateTime.Parse("2000-01-01")) lblExpiryMobile.Text = mobile.ExpiryDate.ToString("MMM-dd-yyyy");
                }

                var conex = licenseRequest.Products.FirstOrDefault(p => p.ProductName == LicenseProducts.CSIFLEXeNETCONEX);
                if (conex != null)
                {
                    grpEnetConex.Enabled = true;
                    chkEnetConex.Checked = true;
                    txtNumberMachines.Text = conex.NumberLicenses.ToString();
                    if (conex.ExpiryDate > DateTime.Parse("2000-01-01")) lblExpiryConex.Text = conex.ExpiryDate.ToString("MMM-dd-yyyy");
                }

                var client = licenseRequest.Products.FirstOrDefault(p => p.ProductName == LicenseProducts.CSIFLEXClient_Server);
                if (client != null)
                {
                    grpClient.Enabled = true;
                    chkClient.Checked = true;
                    if (client.ExpiryDate > DateTime.Parse("2000-01-01")) lblExpiryClient.Text = client.ExpiryDate.ToString("MMM-dd-yyyy");
                }
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (licenseRequest == null)
                return;

            licenseRequest.LicenseType = cmbTypeLicense.SelectedItem.ToString();

            LicenseLib licenseLib = new LicenseLib();

            LicenseBase license = new LicenseBase();
            licenseRequest.CopyPropertiesTo<LicenseRequest, LicenseBase>(license);
            license.ExpiryDate = dateExpiry.Value;

            #region Product CSIFLEX Server ===================================================================================================

            license.ProductName = LicenseProducts.CSIFLEXServer;
            license.LicensesQuantity = 1;

            var server = licenseRequest.Products.FirstOrDefault(p => p.ProductName == LicenseProducts.CSIFLEXServer);
            if (chkServer.Checked)
            {
                if (server != null)
                {
                    server.HashCode = licenseLib.GenerateHash(license);
                    server.ExpiryDate = license.ExpiryDate;
                }
                else
                {
                    LicenseProduct prod = new LicenseProduct();
                    prod.ProductName = LicenseProducts.CSIFLEXServer;
                    prod.NumberLicenses = 1;
                    prod.HashCode = licenseLib.GenerateHash(license);
                    prod.ExpiryDate = license.ExpiryDate;
                    licenseRequest.Products.Add(prod);
                }
            }
            else
            {
                licenseRequest.Products.Remove(server);
            }

            #endregion


            license.ProductName = LicenseProducts.CSIFLEXWebApp;
            license.LicensesQuantity = int.Parse(txtNumberDevices.Text);

            LicenseProduct webAppProd = licenseRequest.Products.FirstOrDefault(p => p.ProductName == LicenseProducts.CSIFLEXWebApp);
            if (chkMobile.Checked)
            {
                if (webAppProd == null)
                {
                    webAppProd = new LicenseProduct();
                    webAppProd.ProductName = LicenseProducts.CSIFLEXWebApp;
                    webAppProd.HashCode = licenseLib.GenerateHash(license);
                    webAppProd.NumberLicenses = license.LicensesQuantity;
                    webAppProd.ExpiryDate = license.ExpiryDate;
                    licenseRequest.Products.Add(webAppProd);
                }
                else
                {
                    webAppProd.HashCode = licenseLib.GenerateHash(license);
                    webAppProd.NumberLicenses = license.LicensesQuantity;
                    webAppProd.ExpiryDate = license.ExpiryDate;
                }
            }
            else
            {
                if (webAppProd != null)
                    licenseRequest.Products.Remove(webAppProd);
            }

            LicenseProduct focasProd = licenseRequest.Products.FirstOrDefault(p => p.ProductName == LicenseProducts.CSIFLEXFocasMtc);
            if (chkFocas.Checked)
            {
                int numLicenses = 5;
                int.TryParse(txtNumberFocas.Text, out numLicenses);

                license.ProductName = LicenseProducts.CSIFLEXFocasMtc;
                license.LicensesQuantity = numLicenses;

                if (focasProd == null)
                {
                    focasProd = new LicenseProduct();
                    focasProd.ProductName = LicenseProducts.CSIFLEXFocasMtc;
                    focasProd.NumberLicenses = numLicenses;
                    focasProd.ExpiryDate = license.ExpiryDate;
                    focasProd.HashCode = licenseLib.GenerateHash(license);
                    licenseRequest.Products.Add(focasProd);
                }
                else
                {
                    focasProd.NumberLicenses = numLicenses;
                    focasProd.ExpiryDate = license.ExpiryDate;
                    focasProd.HashCode = licenseLib.GenerateHash(license);
                }
            }
            else
            {
                if (focasProd != null)
                    licenseRequest.Products.Remove(focasProd);
            }

            LicenseProduct conexProd = licenseRequest.Products.FirstOrDefault(p => p.ProductName == LicenseProducts.CSIFLEXeNETCONEX);
            if (chkEnetConex.Checked)
            {
                int numLicenses = 5;
                int.TryParse(txtNumberMachines.Text, out numLicenses);

                license.ProductName = LicenseProducts.CSIFLEXeNETCONEX;
                license.LicensesQuantity = numLicenses;

                if (conexProd == null)
                {
                    conexProd = new LicenseProduct();
                    conexProd.ProductName = LicenseProducts.CSIFLEXeNETCONEX;
                    conexProd.NumberLicenses = numLicenses;
                    conexProd.ExpiryDate = license.ExpiryDate;
                    conexProd.HashCode = licenseLib.GenerateHash(license);
                    licenseRequest.Products.Add(conexProd);
                }
                else
                {
                    conexProd.NumberLicenses = numLicenses;
                    conexProd.ExpiryDate = license.ExpiryDate;
                    conexProd.HashCode = licenseLib.GenerateHash(license);
                }
            }
            else
            {
                if (conexProd != null)
                    licenseRequest.Products.Remove(conexProd);
            }

            LicenseProduct clientProd = licenseRequest.Products.FirstOrDefault(p => p.ProductName == LicenseProducts.CSIFLEXClient_Server);
            if (chkClient.Checked)
            {
                license.ProductName = LicenseProducts.CSIFLEXClient_Server;

                if (clientProd == null)
                {
                    clientProd = new LicenseProduct();
                    clientProd.ProductName = LicenseProducts.CSIFLEXeNETCONEX;
                    clientProd.HashCode = licenseLib.GenerateHash(license);
                    clientProd.NumberLicenses = license.LicensesQuantity;
                    clientProd.ExpiryDate = license.ExpiryDate;
                    licenseRequest.Products.Add(clientProd);
                }
                else
                {
                    clientProd.HashCode = licenseLib.GenerateHash(license);
                    clientProd.NumberLicenses = license.LicensesQuantity;
                    clientProd.ExpiryDate = license.ExpiryDate;
                }
            }
            else
            {
                if (clientProd != null)
                    licenseRequest.Products.Remove(clientProd);
            }

            var content = JsonConvert.SerializeObject(licenseRequest);

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = activationFile;
            saveFile.Filter = "LIC Files (*.lic*) | *.lic";
            saveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                string path = saveFile.FileName;

                try
                {
                    using (TextWriter tw = new StreamWriter(path, false))
                    {
                        tw.Write(content);
                    }

                    MessageBox.Show("Ativation generated with success.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            CleanUp();
        }

        private void cmbExpiry_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbExpiry.SelectedIndex)
            {
                case 0:
                    dateExpiry.Value = new DateTime(2030, 1, 1);
                    break;
                case 1:
                    dateExpiry.Value = DateTime.Today;
                    break;
                case 2:
                    dateExpiry.Value = DateTime.Today.AddMonths(1);
                    break;
                case 3:
                    dateExpiry.Value = DateTime.Today.AddMonths(3);
                    break;
                case 4:
                    dateExpiry.Value = DateTime.Today.AddMonths(6);
                    break;
                case 5:
                    dateExpiry.Value = DateTime.Today.AddYears(1);
                    break;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CleanUp()
        {
            licenseRequest = null;

            txtCompanyName.Clear();
            txtContactEmail.Clear();
            txtContactPerson.Clear();
            txtContactPhone.Clear();

            lblComputerId.Text = "";
            lblComputerName.Text = "";

            txtNumberDevices.Text = "0";
            txtNumberFocas.Text = "0";
            txtNumberMachines.Text = "0";

            chkEnetConex.Checked = false;
            chkFocas.Checked = false;
            chkMobile.Checked = false;
            chkServer.Checked = false;

            lblExpiryConex.Text = "";
            lblExpiryFocas.Text = "";
            lblExpiryMobile.Text = "";
            lblExpiryServer.Text = "";

            //grpEnetConex.Enabled = false;
            //grpFocas.Enabled = false;
            //grpMobile.Enabled = false;
            //grpServer.Enabled = false;
        }
    }
}
