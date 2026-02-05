using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Management;

namespace OfflineLicensing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
             String infos;

             infos = "cpuid:" + GetCPUSerialNumber() + ";hdd:" + GetDriveSerialNumber("C:") + ";";

             RTB_Infos.Text = infos;

        //GenerateLicense = AES_Encrypt(infos, "license")
        }


        /// <summary>
        /// get CPU serial number
        /// </summary>
        /// <returns>drive serial</returns>
        public string GetCPUSerialNumber()
        {

            //http://msdn.microsoft.com/en-us/library/windows/desktop/aa394373(v=vs.85).aspx

            string cpuSerial = string.Empty;

            using (System.Management.ManagementObjectSearcher querySearch = new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_Processor"))
            {

                using (ManagementObjectCollection queryCollection = querySearch.Get())
                {


                    foreach (ManagementObject moItem in queryCollection)
                    {
                        cpuSerial = Convert.ToString(moItem["Manufacturer"]);
                        cpuSerial = cpuSerial + " " + Convert.ToString(moItem["ProcessorID"]);

                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
            }
            return cpuSerial;
        }


        /// <summary>
        /// get dirve serial number
        /// </summary>
        /// <param name="drive">C:</param>
        /// <returns>drive serial</returns>
        public string GetDriveSerialNumber(string drive)
        {

	        string driveSerial = string.Empty;
	        string driveFixed = System.IO.Path.GetPathRoot(drive);
	        driveFixed = driveFixed.Replace("\\", "");

	        using (System.Management.ManagementObjectSearcher querySearch = new System.Management.ManagementObjectSearcher("SELECT VolumeSerialNumber FROM Win32_LogicalDisk Where Name = '" + driveFixed + "'")) {

		        using (ManagementObjectCollection queryCollection = querySearch.Get()) {

			        //ManagementObject moItem = default(ManagementObject);


                    foreach (ManagementObject moItem in queryCollection)
                    {
				        driveSerial = Convert.ToString(moItem["VolumeSerialNumber"]);

				        break; // TODO: might not be correct. Was : Exit For
			        }
		        }
	        }
	        return driveSerial;
        }
    }
}
