using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using CSI_Library;
using System.Configuration;
using System.Text;
using CSIFlex_ServiceLibrary;
using System.Globalization;

namespace CSIFlex_ServiceLibrary.Utility
{
    public class CreateDatabase
    {
        public static MySqlConnection mysqlcon = new MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString);
        public void CreateDatabaseForFirsttime()
        {
            try
            {
                if (mysqlcon != null && mysqlcon.State == ConnectionState.Closed)
                {
                    mysqlcon.Open();
                }
                StringBuilder command = new StringBuilder("CREATE DATABASE IF NOT EXISTS csi_enetdata;");
                command.Append("CREATE TABLE if not exists csi_enetdata.tbl_ehub_conf(id int(11) NOT NULL AUTO_INCREMENT,monitoring_id varchar(20) DEFAULT NULL,machine_name varchar(200) DEFAULT NULL,mon_setup_id int(11) DEFAULT NULL,log_date datetime DEFAULT NULL,Con_type int(2) DEFAULT NULL,PRIMARY KEY (id),UNIQUE KEY (id));");
                //command.Append("CREATE TABLE if not exists `tbl_ehubconf_data` (`num_id_` int(11) NOT NULL AUTO_INCREMENT,`monitoring_id_` varchar(255) NOT NULL,`machine_name_` varchar(255) DEFAULT NULL,`machine_tmp_filename_` varchar(255) DEFAULT NULL,`Con_type` int(2) DEFAULT NULL,PRIMARY KEY(`monitoring_id_`),UNIQUE KEY `num_id_` (`num_id_`))"); 
                command.Append("CREATE TABLE if not exists csi_enetdata.tbl_file_status(num_id_ int(11) NOT NULL AUTO_INCREMENT,log_date datetime DEFAULT NULL,file_name varchar(255) DEFAULT NULL,file_hash varbinary(1000) DEFAULT NULL,PRIMARY KEY (num_id_),UNIQUE KEY (num_id_));");
                command.Append("CREATE TABLE if not exists csi_enetdata.tbl_monsetup (id int(11) NOT NULL AUTO_INCREMENT,monitoring_id varchar(20) DEFAULT NULL,machine_name int(11) DEFAULT NULL,monitoring_on_track int(11) DEFAULT NULL,machine_temp_filename varchar(200) DEFAULT NULL,mon_state_on int(11) DEFAULT NULL,part_count int(11) DEFAULT NULL,part_multiplier_mn int(11) DEFAULT NULL,cycle_identifier_ic int(11) DEFAULT NULL,min_ideal_ci int(11) DEFAULT NULL,max_perc_ca int(11) DEFAULT NULL,current_status varchar(50) DEFAULT NULL,last_cycle_time int(11) DEFAULT NULL,elapsed_time int(11) DEFAULT NULL,last_updated_date datetime DEFAULT NULL,current_part_no varchar(100) DEFAULT NULL,department_name varchar(100) DEFAULT NULL,reset_counter_nm int(11) DEFAULT NULL,log_date datetime DEFAULT NULL,PRIMARY KEY (id),UNIQUE KEY (id));");
                command.Append("CREATE TABLE if not exists csi_enetdata.tbl_shift_data (id int(11) NOT NULL AUTO_INCREMENT,department_name varchar(100) DEFAULT NULL,day_name varchar(40) DEFAULT NULL,shift_name varchar(100) DEFAULT NULL,shift_start int(11) DEFAULT NULL,shift_end int(11) DEFAULT NULL,break1_start int(11) DEFAULT NULL,break1_end int(11) DEFAULT NULL,break2_start int(11) DEFAULT NULL,break2_end int(11) DEFAULT NULL,break3_start int(11) DEFAULT NULL,break3_end int(11) DEFAULT NULL,log_date datetime DEFAULT NULL,is_first_time_run int(11) DEFAULT NULL,PRIMARY KEY(id),UNIQUE KEY(id));");
                MySqlCommand mysqlcmd = new MySqlCommand(command.ToString(), mysqlcon);
                mysqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Utility.WriteToFile("CreateDatabaseForFirsttime Error :" + ex);
            }
            finally
            {
                mysqlcon.Close();
            }

        }
    }
}
