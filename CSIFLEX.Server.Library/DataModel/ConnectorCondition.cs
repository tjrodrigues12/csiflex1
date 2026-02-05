using CSIFLEX.Database.Access;
using System.Text;

namespace CSIFLEX.Server.Library.DataModel
{
    public class ConnectorCondition
    {
        public bool IsNew { get; set; }

        public int ConnectorId { get; set; }

        public string Machine_Name { get; set; }

        public string IP_Address { get; set; }

        public string Status { get; set; }

        public string Condition { get; set; }

        public string Machine_Type { get; set; }

        public string Delay { get; set; }

        public bool CsdOnSetup { get; set; }

        public ConnectorCondition()
        {
            IsNew = false;
        }

        public void SaveConnector()
        {
            StringBuilder sqlCmd = new StringBuilder();

            if (IsNew)
            {
                sqlCmd.Append($"INSERT IGNORE INTO               ");
                sqlCmd.Append($" csi_auth.tbl_mtcfocasconditions ");
                sqlCmd.Append($" (                               ");
                sqlCmd.Append($"   ConnectorId  ,                ");
                sqlCmd.Append($"   Machine_Name ,                ");
                sqlCmd.Append($"   Ip_Address   ,                ");
                sqlCmd.Append($"   `Status`     ,                ");
                sqlCmd.Append($"   `Condition`  ,                ");
                sqlCmd.Append($"   Machine_Type ,                ");
                sqlCmd.Append($"   `Delay`      ,                ");
                sqlCmd.Append($"   `CsdOnSetup`                  ");
                sqlCmd.Append($" )                               ");
                sqlCmd.Append($" VALUES                          ");
                sqlCmd.Append($" (                               ");
                sqlCmd.Append($"    { ConnectorId }   ,          ");
                sqlCmd.Append($"   '{ Machine_Name }' ,          ");
                sqlCmd.Append($"   '{ IP_Address }'   ,          ");
                sqlCmd.Append($"   '{ Status }'       ,          ");
                sqlCmd.Append($"   '{ Condition }'    ,          ");
                sqlCmd.Append($"   '{ Machine_Type }' ,          ");
                sqlCmd.Append($"   '{ Delay }'        ,          ");
                sqlCmd.Append($"    { CsdOnSetup }               ");
                sqlCmd.Append($" )                             ; ");
            }
            else
            {
                sqlCmd.Append($"UPDATE IGNORE `csi_auth`.`tbl_mtcfocasconditions`  ");
                sqlCmd.Append($"   SET                                             ");
                sqlCmd.Append($"       `Machine_Name`     = '{ Machine_Name }' ,   ");
                sqlCmd.Append($"       `Condition`        = '{ Condition }'    ,   ");
                sqlCmd.Append($"       `Delay`            = '{ Delay }'        ,   ");
                sqlCmd.Append($"       `CsdOnSetup`       =  { CsdOnSetup }        ");
                sqlCmd.Append($"   WHERE                                           ");
                sqlCmd.Append($"       `ConnectorId`   =  { ConnectorId } ;        ");
            }

            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString());

        }
    }
}
