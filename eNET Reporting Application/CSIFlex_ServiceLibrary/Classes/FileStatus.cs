using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFlex_ServiceLibrary.Classes
{
    public class FileStatus
    {
        DataLayer _data = null;

        public FileStatus()
        {
            _data = new DataLayer();
        }

        public void updateFileStatus(string fileName, byte[] file_hash)
        {
            string query26 = "call csi_dashboard.update_or_insert_file(@file_name, @file_hash);";
            //WHERE file_name = @file_name
            MySqlParameter[] param = new MySqlParameter[] {
                 new MySqlParameter("@file_hash", file_hash),
                 new MySqlParameter("@file_name", fileName)

            };
            _data.executeQueryWithParameter(query26, param);
        }

        public bool isFileUpdated(string fileName)
        {
            bool isFileUpdated = true;
            var local_file_hashkey = Utility.GetFileHash(fileName);
            byte[] db_filehash = null;
            string query = "SELECT * FROM csi_dashboard.tbl_file_status WHERE file_name='" + fileName.Replace("\\", "\\\\") + "';";
            var dtFile = _data.executeQuery(query);
            //Utility.WriteToFile("dtFile.Rows.Count::" + dtFile.Rows.Count + "|" + query);
            if (dtFile.Rows.Count < 1)
                return true;
            if (dtFile.Rows[0]["file_hash"] != DBNull.Value)
                db_filehash = (byte[])dtFile.Rows[0]["file_hash"];//Encoding.ASCII.GetBytes(dttable7.Rows[0]["file_hash"].ToString());
            isFileUpdated = Utility.compareFileHash(db_filehash, local_file_hashkey);
            return !isFileUpdated;
        }

    }
}
