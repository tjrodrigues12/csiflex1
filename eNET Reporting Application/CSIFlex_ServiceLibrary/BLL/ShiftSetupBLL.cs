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
    public class ShiftSetupBLL
    {
        #region "General Methods"

        public static ShiftSetupModel GetShiftSetupById(int ShiftSetupId)
        {
            ShiftSetupModel objShiftSetup = new ShiftSetupModel();
            string query = "";
            MySqlParameter[] param = new MySqlParameter[] {
                 new MySqlParameter("@in_id", ShiftSetupId)

            };
            //Execute the query against the database ----------------------- "select * from category where NeedPublish=1 and IsActive=1 and CatId= "
            using (MySqlDataReader rdr = MySqlHelper.ExecuteReader(StringConstants.CONN_STRING, "csi_enetdata.usp_GetShiftSetupById", param))
            {
                // Scroll through the results 
                if (rdr.Read())
                {
                    //objShiftSetup = GetShiftSetupObjectComplete(rdr);
                    if (!rdr.IsDBNull((rdr.GetOrdinal("department_name"))))
                    {
                        objShiftSetup.DepartmentName = Convert.ToString(rdr["department_name"]);
                    }
                    if (!rdr.IsDBNull((rdr.GetOrdinal("shift_name"))))
                    {
                        objShiftSetup.ShiftName = Convert.ToString(rdr["shift_name"]);
                    }
                }
                else
                {
                    objShiftSetup = null;
                }
                rdr.Close();
            }
            return objShiftSetup;
        }

        public static List<ShiftSetupModel> getCurrentShiftByDepartment()
        {
           List< ShiftSetupModel> objShiftSetup = new List<ShiftSetupModel>();
            ShiftSetupModel shiftObject = null;
            string query = "";
            //MySqlParameter[] param = new MySqlParameter[] {
            //     new MySqlParameter("@in_id", ShiftSetupId)

            //};
            //Execute the query against the database ----------------------- "select * from category where NeedPublish=1 and IsActive=1 and CatId= "
            using (MySqlDataReader rdr = MySqlHelper.ExecuteReader(StringConstants.CONN_STRING, "call csi_enetdata.get_current_shift_by_department()"))
            {
                // Scroll through the results 
                while (rdr.Read())
                {
                    shiftObject = new ShiftSetupModel();
                    if (!rdr.IsDBNull((rdr.GetOrdinal("department_name"))))
                    {
                        shiftObject.DepartmentName = Convert.ToString(rdr["department_name"]);
                    }
                    if (!rdr.IsDBNull((rdr.GetOrdinal("shift_name"))))
                    {
                        shiftObject.ShiftName = Convert.ToString(rdr["shift_name"]);
                    }
                    objShiftSetup.Add(shiftObject);


                }                
                rdr.Close();
            }
            return objShiftSetup;
        }
        
        public static IList<ShiftSetupModel> GetAllShiftSetupList()
        {
            IList<ShiftSetupModel> objCategoryList = new List<ShiftSetupModel>();
            //Execute the query against the database 
            using (MySqlDataReader rdr = MySqlHelper.ExecuteReader(StringConstants.CONN_STRING, "csi_enetdata.usp_GetAllShiftSetupData"))
            {

                while (rdr.Read())
                {
                    objCategoryList.Add(GetShiftSetupObjectComplete(rdr));
                }

                rdr.Close();
            };
            return objCategoryList;
        }

        public static void CompareShiftData(ShiftSetupModel oldShift, ShiftSetupModel newShift)
        {
            if(oldShift == null)
            {
                return;
            }
            if (oldShift.ShiftName != newShift.ShiftName)
            {
                if (Convert.ToInt32(oldShift.ShiftName) < Convert.ToInt32(newShift.ShiftName))
                {
                    //Update Stored procedure according to Department name
                    MonSetupBLL.UpdateMonSetupOnShiftchange(new MonSetupModel() { DepartmentName = newShift.DepartmentName });
                }
            }
        }

        #endregion

        #region "CRUD"
        public static int InsertShiftSetup(ShiftSetupModel objShiftSetup)
        {
            MySqlParameter[] paramValues = new MySqlParameter[] {
                new MySqlParameter("@in_operation", "create"),
                new MySqlParameter("@in_id", 0),
                new MySqlParameter("@in_department_name", objShiftSetup.DepartmentName),
                new MySqlParameter("@in_day_name", objShiftSetup.DayName),
                new MySqlParameter("@in_shift_name", objShiftSetup.ShiftName),
                new MySqlParameter("@in_shift_start", objShiftSetup.ShiftStart),
                new MySqlParameter("@in_shift_end", objShiftSetup.ShiftEnd),
                new MySqlParameter("@in_break1_start", objShiftSetup.Break1Start),
                new MySqlParameter("@in_break1_end", objShiftSetup.Break1End),
                new MySqlParameter("@in_break2_start", objShiftSetup.Break2Start),
                new MySqlParameter("@in_break2_end", objShiftSetup.Break2End),
                new MySqlParameter("@in_break3_start", objShiftSetup.Break3Start),
                new MySqlParameter("@in_break3_end", objShiftSetup.Break3End),
                };

            return Convert.ToInt32(MySqlHelper.ExecuteScalar(StringConstants.CONN_STRING, "call csi_enetdata.usp_ShiftDataCU(@in_operation,@in_id,@in_department_name,@in_day_name,@in_shift_name,@in_shift_start,@in_shift_end,@in_break1_start,@in_break1_end,@in_break2_start,@in_break2_end,@in_break3_start,@in_break3_end)", paramValues));
        }
        public static int UpdateShiftSetup(ShiftSetupModel objShiftSetup)
        {
            MySqlParameter[] paramValues = new MySqlParameter[] {
                new MySqlParameter("@in_operation", "update"),
                new MySqlParameter("@in_id", objShiftSetup.Id),
                 new MySqlParameter("@in_department_name", objShiftSetup.DepartmentName),
                new MySqlParameter("@in_day_name", objShiftSetup.DayName),
                new MySqlParameter("@in_shift_name", objShiftSetup.ShiftName),
                new MySqlParameter("@in_shift_start", objShiftSetup.ShiftStart),
                new MySqlParameter("@in_shift_end", objShiftSetup.ShiftEnd),
                new MySqlParameter("@in_break1_start", objShiftSetup.Break1Start),
                new MySqlParameter("@in_break1_end", objShiftSetup.Break1End),
                new MySqlParameter("@in_break2_start", objShiftSetup.Break2Start),
                new MySqlParameter("@in_break2_end", objShiftSetup.Break2End),
                new MySqlParameter("@in_break3_start", objShiftSetup.Break3Start),
                new MySqlParameter("@in_break3_end", objShiftSetup.Break3End),
                };
            return MySqlHelper.ExecuteNonQuery(StringConstants.CONN_STRING, "call csi_enetdata.usp_ShiftDataCU(@in_operation,@in_id,@in_department_name,@in_day_name,@in_shift_name,@in_shift_start,@in_shift_end,@in_break1_start,@in_break1_end,@in_break2_start,@in_break2_end,@in_break3_start,@in_break3_end)", paramValues);
        }
        //public static int DeleteCategory(int CategoryId)
        //{
        //    return SQLHelper.ExecuteNonQuery(SQLHelper.CONN_STRING, "usp_DeleteCategory", CategoryId);
        //}
        #endregion

        #region "Private Methods"

        private static ShiftSetupModel GetShiftSetupObjectComplete(MySqlDataReader rdr)
        {
            ShiftSetupModel objShiftSetup = new ShiftSetupModel();
            if (!rdr.IsDBNull((rdr.GetOrdinal("id"))))
            {
                objShiftSetup.Id = Convert.ToInt32(rdr["id"]);
            }
            if (!rdr.IsDBNull((rdr.GetOrdinal("department_name"))))
            {
                objShiftSetup.DepartmentName = Convert.ToString(rdr["department_name"]);
            }
            if (!rdr.IsDBNull((rdr.GetOrdinal("day_name"))))
            {
                objShiftSetup.DayName = Convert.ToString(rdr["day_name"]);
            }
            if (!rdr.IsDBNull((rdr.GetOrdinal("shift_name"))))
            {
                objShiftSetup.ShiftName = Convert.ToString(rdr["shift_name"]);
            }
            if (!rdr.IsDBNull((rdr.GetOrdinal("shift_start"))))
            {
                objShiftSetup.ShiftStart = Convert.ToInt32(rdr["shift_start"]);
            }
            if (!rdr.IsDBNull((rdr.GetOrdinal("shift_end"))))
            {
                objShiftSetup.ShiftEnd = Convert.ToInt32(rdr["shift_end"]);
            }
            if (!rdr.IsDBNull((rdr.GetOrdinal("break1_start"))))
            {
                objShiftSetup.Break1Start = Convert.ToInt32(rdr["break1_start"]);
            }
            if (!rdr.IsDBNull((rdr.GetOrdinal("break1_end"))))
            {
                objShiftSetup.Break1End = Convert.ToInt32(rdr["break1_end"]);
            }
            if (!rdr.IsDBNull((rdr.GetOrdinal("break2_start"))))
            {
                objShiftSetup.Break2Start = Convert.ToInt32(rdr["break2_start"]);
            }

            if (!rdr.IsDBNull((rdr.GetOrdinal("break2_end"))))
            {
                objShiftSetup.Break2End = Convert.ToInt32(rdr["break2_end"]);
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("break3_start")))
            {
                objShiftSetup.Break3Start = Convert.ToInt32(rdr["break3_start"]);
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("break3_end")))
            {
                objShiftSetup.Break3End = Convert.ToInt32(rdr["break3_end"]);
            }
            return objShiftSetup;
        }
        #endregion
    }
}
