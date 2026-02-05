using CSIFlex_ServiceLibrary.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFlex_ServiceLibrary.BLL
{
    public class MonSetupBLL
    {
        #region "General Methods"

        public static MonSetupModel GetMonSetupById(int MonSetupId)
        {
            MonSetupModel objMonSetup = new MonSetupModel();
            string query = "";
            MySqlParameter[] param = new MySqlParameter[] {
                 new MySqlParameter("@in_id", MonSetupId)

            };
            //Execute the query against the database ----------------------- "select * from category where NeedPublish=1 and IsActive=1 and CatId= "
            using (MySqlDataReader rdr = MySqlHelper.ExecuteReader(StringConstants.CONN_STRING, "usp_GetMonSetupById", param))
            {
                // Scroll through the results 
                if (rdr.Read())
                {
                    objMonSetup = GetMonSetupObjectComplete(rdr);


                }
                else
                {
                    objMonSetup = null;
                }
                rdr.Close();
            }
            return objMonSetup;
        }
        public static IList<MonSetupModel> GetAllMonSetupList()
        {


            IList<MonSetupModel> objCategoryList = new List<MonSetupModel>();
            //Execute the query against the database 
            using (MySqlDataReader rdr = MySqlHelper.ExecuteReader(StringConstants.CONN_STRING, "usp_GetAllMonSetup"))
            {

                while (rdr.Read())
                {
                    objCategoryList.Add(GetMonSetupObjectComplete(rdr));
                }

                rdr.Close();
            };
            return objCategoryList;


        }


        #endregion

        #region "CRUD"
        public static int InsertMonSetup(MonSetupModel objMonSetup)
        {
            try
            {
                MySqlParameter[] paramValues = new MySqlParameter[] {
                new MySqlParameter("@in_operation", "create"),
                new MySqlParameter("@in_id", 0),
                new MySqlParameter("@in_monitoring_id", objMonSetup.MonitoringId),
                new MySqlParameter("@in_machine_name", objMonSetup.MachineName),
                new MySqlParameter("@in_monitoring_on_track", objMonSetup.MonitoringOnTrack),
                new MySqlParameter("@in_machine_temp_filename", objMonSetup.MachineTempFilename),
                new MySqlParameter("@in_mon_state_on", objMonSetup.MonStateOn),
                new MySqlParameter("@in_part_count", objMonSetup.PartCount),
                new MySqlParameter("@in_part_multiplier_mn", objMonSetup.PartMultiplierMN),
                new MySqlParameter("@in_cycle_identifier_ic", objMonSetup.CyclIdentifierIC),
                new MySqlParameter("@in_min_ideal_ci", objMonSetup.MinIdealCI),
                new MySqlParameter("@in_max_perc_ca", objMonSetup.MaxPercCa),
                new MySqlParameter("@in_current_status", objMonSetup.CurrentStatus),
                new MySqlParameter("@in_last_cycle_time", objMonSetup.LastCycleTime),
                new MySqlParameter("@in_elapsed_time", objMonSetup.ElapsedTime),
                new MySqlParameter("@in_last_updated_date", objMonSetup.LastUpdatedDate),
                new MySqlParameter("@in_current_part_no", objMonSetup.MachinePartNo),
                new MySqlParameter("@in_department_name", objMonSetup.DepartmentName),
                new MySqlParameter("@in_reset_counter_nm", objMonSetup.ResetCounterNM ),
                };

                return Convert.ToInt32(MySqlHelper.ExecuteScalar(StringConstants.CONN_STRING, "call csi_enetdata.usp_MonSetupCU(@in_operation, @in_id, @in_monitoring_id, @in_machine_name, @in_monitoring_on_track, @in_machine_temp_filename, @in_mon_state_on, @in_part_count, @in_part_multiplier_mn, @in_cycle_identifier_ic, @in_min_ideal_ci, @in_max_perc_ca, @in_current_status, @in_last_cycle_time, @in_elapsed_time, @in_last_updated_date, @in_current_part_no, @in_department_name, @in_reset_counter_nm)", paramValues));
            }
            catch (Exception e)
            {
                Utility.Utility.WriteToFile("Exception: " + e.Message);
            }
            return 0;
        }

        public static int UpdateMonSetupMonitorData(MonSetupModel objMonSetup)
        {

            //            IN in_machine_temp_filename varchar(200),
            //IN in_last_updated_date datetime, 
            //IN in_current_status varchar(50),
            //IN in_current_part_no varchar(100)
            MySqlParameter[] paramValues = new MySqlParameter[] {
                new MySqlParameter("@in_machine_temp_filename", objMonSetup.MachineTempFilename),
                new MySqlParameter("@in_last_updated_date", objMonSetup.LastUpdatedDate),
                new MySqlParameter("@in_current_status", objMonSetup.CurrentStatus),
                new MySqlParameter("@in_current_part_no", objMonSetup.MachinePartNo),
                };

            return Convert.ToInt32(MySqlHelper.ExecuteScalar(StringConstants.CONN_STRING, "call csi_enetdata.usp_MonSetupMonitorData(@in_machine_temp_filename, @in_last_updated_date, @in_current_status, @in_current_part_no)", paramValues));
        }

        public static int UpdateMonSetupOnShiftchange(MonSetupModel objMonSetup)
        {

            //            IN in_machine_temp_filename varchar(200),
            //IN in_last_updated_date datetime, 
            //IN in_current_status varchar(50),
            //IN in_current_part_no varchar(100)
            MySqlParameter[] paramValues = new MySqlParameter[] {
                new MySqlParameter("@in_department_name", objMonSetup.DepartmentName)
                };

            return Convert.ToInt32(MySqlHelper.ExecuteScalar(StringConstants.CONN_STRING, "call csi_enetdata.update_monsetup_on_shiftchange(@in_department_name)", paramValues));
        }
        public static int UpdateMonSetup(MonSetupModel objMonSetup)
        {
            MySqlParameter[] paramValues = new MySqlParameter[] {
                new MySqlParameter("in_operation", "update"),
                new MySqlParameter("in_id", objMonSetup.Id),
                new MySqlParameter("in_monitoring_id", objMonSetup.MonitoringId),
                new MySqlParameter("in_machine_name", objMonSetup.MachineName),
                new MySqlParameter("in_monitoring_on_track", objMonSetup.MonitoringOnTrack),
                new MySqlParameter("in_machine_temp_filename", objMonSetup.MachineTempFilename),
                new MySqlParameter("in_mon_state_on", objMonSetup.MonStateOn),
                new MySqlParameter("in_part_count", objMonSetup.PartCount),
                new MySqlParameter("in_part_multiplier_mn", objMonSetup.PartMultiplierMN),
                new MySqlParameter("in_cycle_identifier_ic", objMonSetup.CyclIdentifierIC),
                new MySqlParameter("in_min_ideal_ci", objMonSetup.MinIdealCI),
                new MySqlParameter("in_max_perc_ca", objMonSetup.MaxPercCa),
                new MySqlParameter("in_current_status", objMonSetup.CurrentStatus),
                new MySqlParameter("in_last_cycle_time", objMonSetup.LastCycleTime),
                new MySqlParameter("in_elapsed_time", objMonSetup.ElapsedTime),
                new MySqlParameter("in_last_updated_date", objMonSetup.LastUpdatedDate),
                new MySqlParameter("in_current_part_no", objMonSetup.MachinePartNo),
                new MySqlParameter("in_department_name", objMonSetup.DepartmentName),
                new MySqlParameter("in_reset_counter_nm", objMonSetup.ResetCounterNM ),
                };
            return MySqlHelper.ExecuteNonQuery(StringConstants.CONN_STRING, "usp_MonSetupCU", paramValues);
        }
        //public static int DeleteCategory(int CategoryId)
        //{
        //    return SQLHelper.ExecuteNonQuery(SQLHelper.CONN_STRING, "usp_DeleteCategory", CategoryId);
        //}
        #endregion

        #region "Private Methods"

        private static MonSetupModel GetMonSetupObjectComplete(MySqlDataReader rdr)
        {
            MonSetupModel objMonSetup = new MonSetupModel();

            if (!rdr.IsDBNull((rdr.GetOrdinal("id"))))
            {
                objMonSetup.Id = Convert.ToInt32(rdr["id"]);
            }

            if (!rdr.IsDBNull((rdr.GetOrdinal("monitoring_id"))))
            {
                objMonSetup.MonitoringId = Convert.ToString(rdr["monitoring_id"]);
            }


            if (!rdr.IsDBNull((rdr.GetOrdinal("machine_name"))))
            {
                objMonSetup.MachineName = Convert.ToString(rdr["machine_name"]);
            }

            if (!rdr.IsDBNull((rdr.GetOrdinal("monitoring_on_track"))))
            {
                objMonSetup.MonitoringOnTrack = Convert.ToInt32(rdr["monitoring_on_track"]);
            }

            if (!rdr.IsDBNull((rdr.GetOrdinal("machine_temp_filename"))))
            {
                objMonSetup.MachineTempFilename = Convert.ToString(rdr["machine_temp_filename"]);
            }

            if (!rdr.IsDBNull((rdr.GetOrdinal("mon_state_on"))))
            {
                objMonSetup.MonStateOn = Convert.ToInt32(rdr["mon_state_on"]);
            }


            if (!rdr.IsDBNull((rdr.GetOrdinal("part_count"))))
            {
                objMonSetup.PartCount = Convert.ToInt32(rdr["part_count"]);
            }
            if (!rdr.IsDBNull((rdr.GetOrdinal("part_multiplier_mn"))))
            {
                objMonSetup.PartMultiplierMN = Convert.ToInt32(rdr["part_multiplier_mn"]);
            }
            if (!rdr.IsDBNull((rdr.GetOrdinal("cycle_identifier_ic"))))
            {
                objMonSetup.CyclIdentifierIC = Convert.ToInt32(rdr["cycle_identifier_ic"]);
            }

            if (!rdr.IsDBNull((rdr.GetOrdinal("min_ideal_ci"))))
            {
                objMonSetup.MinIdealCI = Convert.ToInt32(rdr["min_ideal_ci"]);
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("max_perc_ca")))
            {
                objMonSetup.MaxPercCa = Convert.ToInt32(rdr["max_perc_ca"]);
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("current_status")))
            {
                objMonSetup.CurrentStatus = Convert.ToString(rdr["current_status"]);
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("last_cycle_time")))
            {
                objMonSetup.LastCycleTime = Convert.ToInt32(rdr["last_cycle_time"]);
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("elapsed_time")))
            {
                objMonSetup.ElapsedTime = Convert.ToInt32(rdr["elapsed_time"]);
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("last_updated_date")))
            {
                objMonSetup.LastUpdatedDate = Convert.ToDateTime(rdr["last_updated_date"]);
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("current_part_no")))
            {
                objMonSetup.MachinePartNo = Convert.ToString(rdr["current_part_no"]);
            }
            //****************************
            if (!rdr.IsDBNull((rdr.GetOrdinal("department_name"))))
            {
                objMonSetup.DepartmentName = Convert.ToString(rdr["department_name"]);
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("reset_counter_nm")))
            {
                objMonSetup.ResetCounterNM = Convert.ToInt32(rdr["reset_counter_nm"]);
            }





            return objMonSetup;

        }
        #endregion
    }
}
