using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFlex_ServiceLibrary
{
    public class StringConstants
    {
        //public static MySqlConnection DB_CONNECTION = new MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString);
        //public static string CONN_STRING = "server=localhost;user=root;password=CSIF1337;port=3306;Convert Zero Datetime=True;";
        public static string CONN_STRING = "server=10.0.10.189;user=root;password=CSIF1337;port=3306;Convert Zero Datetime=True;";

        public static string MON_SETUP_FILE_NAME = ConfigurationManager.AppSettings["SERVER_ENET_PATH"] + ConfigurationManager.AppSettings["MON_SETUP"];
        public static string SHIFT_SETUP_FILE_NAME = @"c\_eNETDNC\_SETUP\ShiftSetup2.sys";//ConfigurationManager.AppSettings["SERVER_ENET_PATH"] + ConfigurationManager.AppSettings["SHIFT_SETUP"];
        public static string EHUB_CONF_FILE_NAME = @"c\_eNETDNC\_SETUP\eHUBConf.sys";//ConfigurationManager.AppSettings["SERVER_ENET_PATH"] + ConfigurationManager.AppSettings["EHUB_CONF"];


        //CSI_Library.CSI_Library.MySqlConnectionString.Replace("10.0.10.189", "localhost");
    }
}
