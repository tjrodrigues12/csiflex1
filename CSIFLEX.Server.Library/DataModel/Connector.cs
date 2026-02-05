using CSIFLEX.Database.Access;
using CSIFLEX.Utilities;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CSIFLEX.Server.Library.DataModel
{
    public class Connector
    {
        public int Id { get; set; }

        public int MachineId { get; set; }

        private string machineName;
        public string MachineName
        {
            get
            {
                return machineName;
            }

            set
            {
                machineName = value;
                ConnectorOtherSettings.Machine_Name = machineName;
                foreach (ConnectorCondition condition in Conditions)
                    condition.Machine_Name = machineName;
            }
        }

        private string machineIp;
        public string MachineIP
        {
            get
            {
                return machineIp;
            }
            set
            {
                machineIp = value;
                ConnectorOtherSettings.IP_Address = machineIp;
                foreach (ConnectorCondition condition in Conditions)
                    condition.IP_Address = machineIp;
            }
        }

        public string MTCMachine { get; set; }

        private string connectorType;
        public string ConnectorType
        {
            get
            {
                return connectorType;
            }
            set
            {
                connectorType = value;
                foreach (ConnectorCondition condition in Conditions)
                    condition.Machine_Type = connectorType;
            }
        }

        public string CurrentStatus { get; set; }

        public string ConditionStr { get; set; }

        public string PartnoID { get; set; }

        public string ProgramID { get; set; }

        public string FeedOverrideID { get; set; }

        public string SpindleOverrideID { get; set; }

        public string eNETMachineName { get; set; }

        public string FocasPort { get; set; }

        public string ControllerType { get; set; }

        public string AgentIP { get; set; }

        public string AgentPort { get; set; }

        public string Manufacturer { get; set; }

        private string agentServiceName;
        public string AgentServiceName
        {
            get
            {
                if (connectorType == "Focas")
                    return string.IsNullOrEmpty(agentServiceName) ? $"CSIFLEX.FocasAgent.Machine-{MachineId.ToString("000")}" : agentServiceName ;
                else
                    return "";
            }
            set
            {
                agentServiceName = value;
            }
        }

        public string AgentExeLocation { get; set; }

        private string adapterServiceName;
        public string AdapterServiceName
        {
            get
            {
                if (connectorType == "Focas")
                    return string.IsNullOrEmpty(adapterServiceName) ? $"CSIFLEX.FocasAdapter.Machine-{MachineId.ToString("000")}" : adapterServiceName;
                else
                    return "";
            }
            set
            {
                adapterServiceName = value;
            }
        }

        public string AdapterExeLocation { get; set; }

        public string AdapterPort { get; set; }

        public int MonitoringUnitId { get; set; }

        public int OldMUId { get; set; }

        public ConnectorOtherSettings ConnectorOtherSettings { get; set; }

        public List<ConnectorCondition> Conditions { get; set; }


        public Connector(int connectorId = 0)
        {
            ConnectorOtherSettings = new ConnectorOtherSettings(connectorId);
            Conditions = new List<ConnectorCondition>();

            this.InitializeVariables<Connector>();

            if (connectorId > 0)
            {
                DataTable tblConnector = MySqlAccess.GetDataTable($"SELECT * FROM csi_auth.tbl_csiconnector WHERE Id = {connectorId}");

                if (tblConnector.Rows.Count == 0)
                    return;

                DataRow connector = tblConnector.Rows[0];

                Id = connectorId;
                MachineId = int.Parse(connector["MachineId"].ToString());
                MachineName = connector["MachineName"].ToString();
                MachineIP = connector["MachineIP"].ToString();
                MTCMachine = connector["MTCMachine"].ToString();
                ConnectorType = connector["ConnectorType"].ToString();
                CurrentStatus = connector["CurrentStatus"].ToString();
                ConditionStr = connector["ConditionStr"].ToString();
                PartnoID = connector["PartnoID"].ToString();
                ProgramID = connector["ProgramID"].ToString();
                FeedOverrideID = connector["FeedOverrideID"].ToString();
                SpindleOverrideID = connector["SpindleOverrideID"].ToString();
                eNETMachineName = connector["eNETMachineName"].ToString();
                FocasPort = connector["FocasPort"].ToString();
                ControllerType = connector["ControllerType"].ToString();
                AgentIP = connector["AgentIP"].ToString();
                AgentPort = connector["AgentPort"].ToString();
                Manufacturer = connector["Manufacturer"].ToString();
                AgentServiceName = connector["AgentServiceName"].ToString();
                AgentExeLocation = connector["AgentExeLocation"].ToString();
                AdapterServiceName = connector["AdapterServiceName"].ToString();
                AdapterExeLocation = connector["AdapterExeLocation"].ToString();
                AdapterPort = connector["AdapterPort"].ToString();
                MonitoringUnitId = int.Parse( connector["MonitoringUnitId"].ToString());
                OldMUId = int.Parse( connector["MonitoringUnitId"].ToString());

                DataTable tblConditions = MySqlAccess.GetDataTable($"SELECT * FROM csi_auth.tbl_mtcfocasconditions WHERE ConnectorId = {connectorId};");

                foreach (DataRow condition in tblConditions.Rows)
                {
                    Conditions.Add(new ConnectorCondition()
                    {
                        IsNew = false,
                        ConnectorId = connectorId,
                        Machine_Name = MachineName,
                        IP_Address = MachineIP,
                        Machine_Type = ConnectorType,
                        Status = condition["Status"].ToString(),
                        Condition = condition["Condition"].ToString(),
                        Delay = condition["Delay"].ToString(),
                        CsdOnSetup = condition["CsdOnSetup"].ToString() == "1"
                    });
                }
            }

            ConnectorOtherSettings = new ConnectorOtherSettings(connectorId);
        }

        public void SaveConnector()
        {
            StringBuilder cmd = new StringBuilder();

            if (MachineId == 0)
                return;

            if (Id == 0)
            {
                cmd.Append($"INSERT INTO CSI_auth.tbl_CSIConnector  ");
                cmd.Append($" (                               ");
                cmd.Append($"   MachineId                   , ");
                cmd.Append($"   MachineName                 , ");
                cmd.Append($"   MachineIP                   , ");
                cmd.Append($"   MTCMachine                  , ");
                cmd.Append($"   ConnectorType               , ");
                cmd.Append($"   CurrentStatus               , ");
                cmd.Append($"   ConditionStr                , ");
                cmd.Append($"   PartnoID                    , ");
                cmd.Append($"   ProgramID                   , ");
                cmd.Append($"   FeedOverrideId              , ");
                cmd.Append($"   SpindleOverrideID           , ");
                cmd.Append($"   eNETMachineName             , ");
                cmd.Append($"   FocasPort                   , ");
                cmd.Append($"   ControllerType              , ");
                cmd.Append($"   AgentIP                     , ");
                cmd.Append($"   AgentPort                   , ");
                cmd.Append($"   Manufacturer                , ");
                cmd.Append($"   AgentServiceName            , ");
                cmd.Append($"   AgentExeLocation            , ");
                cmd.Append($"   AdapterServiceName          , ");
                cmd.Append($"   AdapterExeLocation          , ");
                cmd.Append($"   AdapterPort                 , ");
                cmd.Append($"   MonitoringUnitId              ");
                cmd.Append($" )                               ");
                cmd.Append($" VALUES                          ");
                cmd.Append($" (                               ");
                cmd.Append($"    { MachineId }              , ");
                cmd.Append($"   '{ MachineName }'           , ");
                cmd.Append($"   '{ MachineIP }'             , ");
                cmd.Append($"   '{ MTCMachine }'            , ");
                cmd.Append($"   '{ ConnectorType }'         , ");
                cmd.Append($"   'CYCLE ON'                  , ");
                cmd.Append($"   ''                          , ");
                cmd.Append($"   'msg'                       , ");
                cmd.Append($"   'cn5'                       , ");
                cmd.Append($"   'Fovr'                      , ");
                cmd.Append($"   'c3'                        , ");
                cmd.Append($"   '{ eNETMachineName }'       , ");
                cmd.Append($"   '{ FocasPort }'             , ");
                cmd.Append($"   '{ ControllerType }'        , ");
                cmd.Append($"   '{ AgentIP }'               , ");
                cmd.Append($"   '{ AgentPort }'             , ");
                cmd.Append($"   '{ Manufacturer }'          , ");
                cmd.Append($"   '{ AgentServiceName }'      , ");
                cmd.Append($"   '{ AgentExeLocation }'      , ");
                cmd.Append($"   '{ AdapterServiceName }'    , ");
                cmd.Append($"   '{ AdapterExeLocation }'    , ");
                cmd.Append($"   '{ AdapterPort }'           , ");
                cmd.Append($"    { MonitoringUnitId }         ");
                cmd.Append($" )                             ; ");
                cmd.Append($"  SELECT LAST_INSERT_ID()      ; ");

                Id = int.Parse(MySqlAccess.ExecuteScalar(cmd.ToString()).ToString());

                ConnectorOtherSettings.ConnectorId = Id;
            }
            else
            {
                cmd.Append($"UPDATE IGNORE `csi_auth`.`tbl_csiconnector`             ");
                cmd.Append($"   SET                                                  ");
                cmd.Append($"       `MachineName`        = '{ MachineName }'       , ");
                cmd.Append($"       `MTCMachine`         = '{ MTCMachine }'        , ");
                cmd.Append($"       `eNETMachineName`    = '{ eNETMachineName }'   , ");
                cmd.Append($"       `AgentServiceName`   = '{ AgentServiceName }'  , ");
                cmd.Append($"       `AdapterServiceName` = '{ AdapterServiceName }', ");
                cmd.Append($"       `FocasPort`          = '{ FocasPort }'         , ");
                cmd.Append($"       `ControllerType`     = '{ ControllerType }'    , ");
                cmd.Append($"       `Manufacturer`       = '{ Manufacturer }'      , ");
                cmd.Append($"       `MonitoringUnitId`   =  { MonitoringUnitId }     ");
                cmd.Append($"   WHERE                                                ");
                cmd.Append($"       `Id`                 =  { Id } ;                 ");

                MySqlAccess.ExecuteNonQuery(cmd.ToString());
            }

            MonitoringBoardsService.SetTarget(MonitoringUnitId, MachineId);

            if (OldMUId > 0 && OldMUId != MonitoringUnitId)
                MonitoringBoardsService.SetTarget(OldMUId, 0);

            OldMUId = MonitoringUnitId;

            ConnectorOtherSettings.SaveConnector();

            foreach(ConnectorCondition condition in Conditions)
            {
                condition.ConnectorId = Id;
                condition.SaveConnector();
            }

            if (!Conditions.Any(c => c.Status == "CYCLE ON"))
            {
                ConnectorCondition condition = new ConnectorCondition()
                {
                    IsNew = true,
                    ConnectorId = Id,
                    Machine_Name = MachineName,
                    IP_Address = MachineIP,
                    Machine_Type = ConnectorType,
                    Status = "CYCLE ON",
                    Condition = ConnectorType == "Focas" ? "(execution = ''ACTIVE'') AND (mode = ''AUTOMATIC'')" : "(exec = ''ACTIVE'') AND (mode = ''AUTOMATIC'')",
                    Delay = "0",
                    CsdOnSetup = false
                };
                condition.SaveConnector();
                Conditions.Add(condition);
            }

            if (MonitoringUnitId > 0 && !Conditions.Any(c => c.Status == "CYCLE START DISABLE"))
            {
                ConnectorCondition condition = new ConnectorCondition()
                {
                    IsNew = true,
                    ConnectorId = Id,
                    Machine_Name = MachineName,
                    IP_Address = MachineIP,
                    Machine_Type = ConnectorType,
                    Status = "CYCLE START DISABLE",
                    Condition = ConnectorType == "Focas" ? "(execution != ''ACTIVE'') OR (mode != ''AUTOMATIC'')" : "(exec != ''ACTIVE'') OR (mode != ''AUTOMATIC'')",
                    Delay = "0",
                    CsdOnSetup = true
                };
                condition.SaveConnector();
                Conditions.Add(condition);
            }

            if (!Conditions.Any(c => c.Status == "NO eMONITORING"))
            {
                ConnectorCondition condition = new ConnectorCondition()
                {
                    IsNew = true,
                    ConnectorId = Id,
                    Machine_Name = MachineName,
                    IP_Address = MachineIP,
                    Machine_Type = ConnectorType,
                    Status = "NO eMONITORING",
                    Condition = "mode = ''UNAVAILABLE''",
                    Delay = "0",
                    CsdOnSetup = false
                };
                condition.SaveConnector();
                Conditions.Add(condition);
            }
        }

        public void DeleteConnector()
        {
            StringBuilder cmd = new StringBuilder();

            cmd.Append($"DELETE FROM CSI_auth.tbl_mtcfocasconditions     WHERE ConnectorId = { Id } ; ");
            cmd.Append($"DELETE FROM CSI_auth.tbl_csiothersettings       WHERE ConnectorId = { Id } ; ");
            cmd.Append($"DELETE FROM CSI_auth.tbl_csiothersettingsvalues WHERE ConnectorId = { Id } ; ");
            cmd.Append($"DELETE FROM CSI_auth.tbl_CSIConnector           WHERE Id          = { Id } ; ");
            cmd.Append($"UPDATE IGNORE csi_auth.tbl_ehub_conf SET MTC_Machine_name = '' WHERE Id = { MachineId };");

            MySqlAccess.ExecuteNonQuery(cmd.ToString());

            if (MonitoringUnitId > 0)
                MonitoringBoardsService.SetTarget(MonitoringUnitId, 0);

            Id = 0;
        }

        public void CreateCSDCondition()
        {
            if (!Conditions.Any(c => c.Status == "CYCLE START DISABLE"))
            {
                ConnectorCondition condition = new ConnectorCondition()
                {
                    IsNew = true,
                    ConnectorId = Id,
                    Machine_Name = MachineName,
                    IP_Address = MachineIP,
                    Machine_Type = ConnectorType,
                    Status = "CYCLE START DISABLE",
                    Condition = ConnectorType == "Focas" ? "(execution != ''ACTIVE'') OR (mode != ''AUTOMATIC'')" : "(exec != ''ACTIVE'') OR (mode != ''AUTOMATIC'')",
                    Delay = "0",
                    CsdOnSetup = true
                };
                condition.SaveConnector();
                Conditions.Add(condition);
            }
        }
    }
}
