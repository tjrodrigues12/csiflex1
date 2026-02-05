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
    public class EHubConfBLL
    {
        #region "General Methods"

        public static EhubConfModel GetEhubConfById(int EhubConfId)
        {
            EhubConfModel objEhubConf = new EhubConfModel();
            string query = "";
            MySqlParameter[] param = new MySqlParameter[] {
                 new MySqlParameter("@in_id", EhubConfId)

            };
            //Execute the query against the database ----------------------- "select * from category where NeedPublish=1 and IsActive=1 and CatId= "
            using (MySqlDataReader rdr = MySqlHelper.ExecuteReader(StringConstants.CONN_STRING, "csi_enetdata.usp_GetEhubConfById", param))
            {
                // Scroll through the results 
                if (rdr.Read())
                {
                    objEhubConf = GetEhubConfObjectComplete(rdr);


                }
                else
                {
                    objEhubConf = null;
                }
                rdr.Close();
            }
            return objEhubConf;
        }
        public static IList<EhubConfModel> GetAllEhubConfList()
        {


            IList<EhubConfModel> objCategoryList = new List<EhubConfModel>();
            //Execute the query against the database 
            using (MySqlDataReader rdr = MySqlHelper.ExecuteReader(StringConstants.CONN_STRING, "csi_enetdata.usp_GetAllEhubConf"))
            {

                while (rdr.Read())
                {
                    objCategoryList.Add(GetEhubConfObjectComplete(rdr));
                }

                rdr.Close();
            };
            return objCategoryList;
        }

        #endregion

        #region "CRUD"
        public static int InsertEhubConf(EhubConfModel objEhubConf)
        {

            MySqlParameter[] paramValues = new MySqlParameter[] {
                new MySqlParameter("@in_operation", "create"),
                new MySqlParameter("@in_id", 0),
                new MySqlParameter("@in_monitoring_id", objEhubConf.MonitoringId),
                new MySqlParameter("@in_machine_name", objEhubConf.MachineName),
                new MySqlParameter("@in_mon_setup_id", objEhubConf.MonSetupId),
                new MySqlParameter("@in_Con_type", objEhubConf.Con_type),
                };

            return Convert.ToInt32(MySqlHelper.ExecuteScalar(StringConstants.CONN_STRING, "call csi_enetdata.usp_EhubConfCU(@in_operation,@in_id,@in_monitoring_id,@in_machine_name,@in_mon_setup_id)", paramValues));
        }
        public static int UpdateEhubConf(EhubConfModel objEhubConf)
        {
            MySqlParameter[] paramValues = new MySqlParameter[] {
                new MySqlParameter("@in_operation", "update"),
                new MySqlParameter("@in_id", objEhubConf.Id),
                new MySqlParameter("@in_monitoring_id", objEhubConf.MonitoringId),
                new MySqlParameter("@in_machine_name", objEhubConf.MachineName),
                new MySqlParameter("@in_mon_setup_id", objEhubConf.MonSetupId),
                new MySqlParameter("@in_Con_type", objEhubConf.Con_type),
                };
            return MySqlHelper.ExecuteNonQuery(StringConstants.CONN_STRING, "call csi_enetdata.usp_EhubConfCU(@in_operation,@in_id,@in_monitoring_id,@in_machine_name,@in_mon_setup_id)", paramValues);
        }
        //public static int DeleteCategory(int CategoryId)
        //{
        //    return SQLHelper.ExecuteNonQuery(SQLHelper.CONN_STRING, "csi_enetdata.usp_DeleteCategory", CategoryId);
        //}
        #endregion

        #region "Private Methods"

        private static EhubConfModel GetEhubConfObjectComplete(MySqlDataReader rdr)
        {
            EhubConfModel objEhubConf = new EhubConfModel();
            if (!rdr.IsDBNull((rdr.GetOrdinal("id"))))
            {
                objEhubConf.Id = Convert.ToInt32(rdr["id"]);
            }
            if (!rdr.IsDBNull((rdr.GetOrdinal("monitoring_id"))))
            {
                objEhubConf.MonitoringId = Convert.ToString(rdr["monitoring_id"]);
            }
            if (!rdr.IsDBNull((rdr.GetOrdinal("machine_name"))))
            {
                objEhubConf.MachineName = Convert.ToString(rdr["machine_name"]);
            }
            if (!rdr.IsDBNull((rdr.GetOrdinal("mon_setup_id"))))
            {
                objEhubConf.MonSetupId = Convert.ToInt32(rdr["mon_setup_id"]);
            }
            if (!rdr.IsDBNull((rdr.GetOrdinal("Con_type"))))
            {
                objEhubConf.Con_type = Convert.ToInt32(rdr["Con_type"]);
            }
            return objEhubConf;
        }
        #endregion
    }
}
