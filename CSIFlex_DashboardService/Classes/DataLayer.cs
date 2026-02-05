using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFlex_DashboardService.Classes
{
    public class DataLayer
    {
        private MySqlConnection mysqlcon = null;
        private CSI_Library.CSI_Library csi_lib = null;
        private string serverName;
        private string serverProgramData;
        private string serverENETPath;

        public DataLayer()
        {
            mysqlcon = new MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString);
            csi_lib = new CSI_Library.CSI_Library();
            
            ////Creating database if not exist
            //createDatabase();
                     
        }

        public void createDatabase()
        {
            if (mysqlcon != null && mysqlcon.State == ConnectionState.Closed)
            { mysqlcon.Open(); }

            StringBuilder command = new StringBuilder("CREATE DATABASE IF NOT EXISTS csi_dashboard;");
            command.Append("CREATE TABLE if not exists csi_dashboard.tbl_ehubconf_data(num_id_ integer AUTO_INCREMENT,monitoring_id_ varchar(255), machine_name_ varchar(255), machine_tmp_filename_ varchar(255),file_size integer, PRIMARY KEY(monitoring_id_),UNIQUE KEY (num_id_), file_hash varbinary(1000));");
            command.Append("CREATE TABLE if not exists csi_dashboard.tbl_monsetup_data(num_id_ integer AUTO_INCREMENT, monitoring_id_ varchar(255),  machine_name_ varchar(255),machine_tmp_filename_ varchar(255),monitoring_ integer, mon_state_ integer, part_count_ integer, part_multiplier_ integer, cycle_identifier_ varchar(255), min_ideal_ integer, max_perc_ integer ,current_status_ varchar(255),last_cycle_time integer,elapsed_time integer,machine_part_no_ varchar(255),correction_date_ DATETIME,department_name_ varchar(255),reset_counter_ integer,day_name_ varchar(255),shift_name_ varchar(255),shift_start_ integer, shift_end_ integer,old_last_cycle_time integer,old_shift_name_ varchar(255),old_part_count_ integer, PRIMARY KEY(monitoring_id_),UNIQUE KEY (num_id_));");
            command.Append("CREATE TABLE if not exists csi_dashboard.tbl_shift_data(num_id_ integer AUTO_INCREMENT, department_name_ varchar(255),  day_name_ varchar(255),shift_name_ varchar(255),shift_start_ integer, shift_end_ integer,break1_start_ integer, break1_end_ integer,break2_start_ integer, break2_end_ integer,break3_start_ integer, break3_end_ integer, UNIQUE KEY (num_id_));");
            command.Append("CREATE TABLE if not exists csi_dashboard.tbl_file_status(num_id_ integer AUTO_INCREMENT,log_date datetime ,file_name varchar(255), PRIMARY KEY(num_id_),UNIQUE KEY (num_id_), file_hash varbinary(1000));");
            MySqlCommand mysqlcmd = new MySqlCommand(command.ToString(), mysqlcon);
            //mysqlcmd.ExecuteNonQuery();
            using (MySqlDataReader dr = mysqlcmd.ExecuteReader())
            {
                dr.Close();
                mysqlcon.Close();
            }
        }


        public DataTable executeQuery(string querycheck)
        {
            if (mysqlcon != null && mysqlcon.State == ConnectionState.Closed)
            { mysqlcon.Open(); }
            MySqlCommand cmdSELECTquerycheck = new MySqlCommand(querycheck, mysqlcon);
            using (MySqlDataReader readerquerycheck = cmdSELECTquerycheck.ExecuteReader())
            {
                DataTable dtreaderquerycheck = new DataTable();
                dtreaderquerycheck.Load(readerquerycheck);
                readerquerycheck.Close();
                mysqlcon.Close();
                return dtreaderquerycheck;
            }


        }

        public void executeNonQuery(string querycheck)
        {
            if (mysqlcon != null && mysqlcon.State == ConnectionState.Closed)
            { mysqlcon.Open(); }
            //Utility.WriteToFile(querycheck);
            MySqlCommand cmdINSERT_query1_shifttbl = new MySqlCommand(querycheck, mysqlcon);
            cmdINSERT_query1_shifttbl.ExecuteNonQuery();
            mysqlcon.Close();
        }

        public void executeQueryWithParameter(string query, MySqlParameter[] myParamArray)
        {
            if (mysqlcon != null && mysqlcon.State == ConnectionState.Closed)
            { mysqlcon.Open(); }
            MySqlCommand cmd = new MySqlCommand(query, mysqlcon);
            for (int i = 0; i < myParamArray.Length; i++)
            {
                cmd.Parameters.Add(myParamArray[i]);               
            }

            cmd.ExecuteNonQuery();
            mysqlcon.Close();
        }

    }
}
