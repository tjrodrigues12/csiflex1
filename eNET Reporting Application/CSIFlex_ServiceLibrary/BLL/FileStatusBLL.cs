using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSIFlex_ServiceLibrary.Utility;

namespace CSIFlex_ServiceLibrary.BLL
{
    public class FileStatusBLL
    {
        public static void updateFileStatus(string fileName, byte[] file_hash)
        {
            string query26 = "call csi_enetdata.update_or_insert_file(@file_name, @file_hash);";
            //WHERE file_name = @file_name
            MySqlParameter[] param = new MySqlParameter[] {
                 new MySqlParameter("@file_hash", file_hash),
                 new MySqlParameter("@file_name", fileName)

            };
            MySqlHelper.ExecuteNonQuery(StringConstants.CONN_STRING, query26, param);
        }

        public static bool isFileUpdated(string fileName)
        {
            bool isFileUpdated = true;
            var local_file_hashkey = Utility.Utility.GetFileHash(fileName);
            byte[] db_filehash = null;
            string query = "SELECT * FROM csi_enetdata.tbl_file_status WHERE file_name='" + fileName.Replace("\\", "\\\\") + "';";
            var dtFile = MySqlHelper.ExecuteDataset(StringConstants.CONN_STRING, query).Tables[0];
            if (dtFile.Rows.Count < 1)
            {
                updateFileStatus(fileName, local_file_hashkey);
                return true;
            }
            if (dtFile.Rows[0]["file_hash"] != DBNull.Value)
                db_filehash = (byte[])dtFile.Rows[0]["file_hash"];//Encoding.ASCII.GetBytes(dttable7.Rows[0]["file_hash"].ToString());
            isFileUpdated = Utility.Utility.compareFileHash(db_filehash, local_file_hashkey);
            if (!isFileUpdated)
            {
                updateFileStatus(fileName, local_file_hashkey);
            }
            return !isFileUpdated;
        }

    }
}
