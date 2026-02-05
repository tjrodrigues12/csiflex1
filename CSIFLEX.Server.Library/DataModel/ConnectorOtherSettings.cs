using CSIFLEX.Database.Access;
using CSIFLEX.Utilities;
using System.Data;
using System.Text;

namespace CSIFLEX.Server.Library.DataModel
{
    public class ConnectorOtherSettings
    {
        public int ConnectorId { get; set; }

        public bool IsNew { get; set; }

        public string Machine_Name { get; set; }

        public string IP_Address { get; set; }

        public string Part_No_Var { get; set; }

        public string Part_No_Value { get; set; }

        public string Prog_No_Var { get; set; }

        public string Prog_No_Value { get; set; }

        public string Part_Req_Var { get; set; }

        public int Part_Req_Value { get; set; }

        public string Part_Count_Var { get; set; }

        public int Part_Count_Value { get; set; }

        public string FeedRateOver_Var { get; set; }

        public string FeedRateOver_Value { get; set; }

        public string SpindleOver_Var { get; set; }

        public string SpindleOver_Value { get; set; }

        public string RapidOver_Var { get; set; }

        public string RapidOver_Value { get; set; }

        public string Part_No_StartWith { get; set; }

        public string Part_No_EndWith { get; set; }

        public string Prog_No_StartWith { get; set; }

        public string Prog_No_EndWith { get; set; }

        public string Feed_MIN { get; set; }

        public string Feed_MAX { get; set; }

        public string Spindle_MIN { get; set; }

        public string Spindle_MAX { get; set; }

        public string Rapid_MIN { get; set; }

        public string Rapid_MAX { get; set; }


        public ConnectorOtherSettings(int connectorId = 0)
        {
            this.InitializeVariables<ConnectorOtherSettings>();

            ConnectorId = connectorId;

            IsNew = true;

            if (connectorId > 0)
            {
                DataTable tblConnector = MySqlAccess.GetDataTable($"SELECT * FROM csi_auth.view_csiothersettings WHERE ConnectorId = {connectorId}");

                if (tblConnector.Rows.Count == 0)
                    return;

                IsNew = false;

                DataRow connector = tblConnector.Rows[0];

                try
                {
                    Machine_Name = connector["Machine_Name"].ToString();
                    IP_Address = connector["IP_Address"].ToString();
                    Part_No_Var = connector["PartNumber_Variable"].ToString();
                    Part_No_Value = connector["PartNumber_Value"].ToString();
                    Prog_No_Var = connector["ProgramNumber_Variable"].ToString();
                    Prog_No_Value = connector["ProgramNumber_Value"].ToString();
                    Part_Req_Var = connector["PartRequired_Variable"].ToString();
                    Part_Req_Value = int.Parse(connector["PartRequired_Value"].ToString());
                    Part_Count_Var = connector["PartCount_Variable"].ToString();
                    Part_Count_Value = int.Parse(connector["PartCount_Value"].ToString());
                    FeedRateOver_Var = connector["FeedRate_Variable"].ToString();
                    FeedRateOver_Value = connector["FeedRate_Value"].ToString();
                    SpindleOver_Var = connector["Spindle_Variable"].ToString();
                    SpindleOver_Value = connector["Spindle_Value"].ToString();
                    RapidOver_Var = connector["Rapid_Variable"].ToString();
                    RapidOver_Value = connector["Rapid_Value"].ToString();
                    Part_No_StartWith = connector["PartNumber_Filter1Start"].ToString();
                    Part_No_EndWith = connector["PartNumber_Filter1End"].ToString();
                    Prog_No_StartWith = connector["ProgramNumber_FilterStart"].ToString();
                    Prog_No_EndWith = connector["ProgramNumber_FilterEnd"].ToString();
                    Feed_MIN = connector["Feedrate_MIN"].ToString();
                    Feed_MAX = connector["Feedrate_MAX"].ToString();
                    Spindle_MIN = connector["Spindle_MIN"].ToString();
                    Spindle_MAX = connector["Spindle_MAX"].ToString();
                    Rapid_MIN = connector["Rapid_MIN"].ToString();
                    Rapid_MAX = connector["Rapid_MAX"].ToString();
                }
                catch (System.Exception ex)
                {
                    Log.Error(ex);
                }
            }
            else
            {
                Feed_MIN = "0";
                Feed_MAX = "100";
                Spindle_MIN = "5";
                Spindle_MAX = "150";
                Rapid_MIN = "5";
                Rapid_MAX = "150";
            }
        }

        public void SaveConnector()
        {
            StringBuilder sqlCmd = new StringBuilder();

            if (IsNew)
            {
                sqlCmd.Append($"INSERT IGNORE INTO                      ");
                sqlCmd.Append($"    csi_auth.tbl_csiothersettings       ");
                sqlCmd.Append($"    (                                   ");
                sqlCmd.Append($"        ConnectorId             ,       ");
                sqlCmd.Append($"        Machine_Name            ,       ");
                sqlCmd.Append($"        IP_Address              ,       ");
                sqlCmd.Append($"        PartNumber_Variable     ,       ");
                sqlCmd.Append($"        PartRequired_Variable   ,       ");
                sqlCmd.Append($"        PartCount_Variable      ,       ");
                sqlCmd.Append($"        FeedRate_Variable       ,       ");
                sqlCmd.Append($"        Spindle_Variable        ,       ");
                sqlCmd.Append($"        Rapid_Variable          ,       ");
                sqlCmd.Append($"        FeedRate_MIN            ,       ");
                sqlCmd.Append($"        FeedRate_MAX            ,       ");
                sqlCmd.Append($"        Spindle_MIN             ,       ");
                sqlCmd.Append($"        Spindle_MAX             ,       ");
                sqlCmd.Append($"        Rapid_MIN               ,       ");
                sqlCmd.Append($"        Rapid_MAX                       ");
                sqlCmd.Append($"    )                                   ");
                sqlCmd.Append($"    VALUES                              ");
                sqlCmd.Append($"    (                                   ");
                sqlCmd.Append($"         { ConnectorId }        ,       ");
                sqlCmd.Append($"        '{ Machine_Name }'      ,       ");
                sqlCmd.Append($"        '{ IP_Address }'        ,       ");
                sqlCmd.Append($"        'programcomment'        ,       ");
                sqlCmd.Append($"        'RequiredPart'          ,       ");
                sqlCmd.Append($"        'partcount'             ,       ");
                sqlCmd.Append($"        'feedovr'               ,       ");
                sqlCmd.Append($"        'cspeedovr'             ,       ");
                sqlCmd.Append($"        'Frapidovr'             ,       ");
                sqlCmd.Append($"        '{Feed_MIN}'            ,       ");
                sqlCmd.Append($"        '{Feed_MAX}'            ,       ");
                sqlCmd.Append($"        '{Spindle_MIN}'         ,       ");
                sqlCmd.Append($"        '{Spindle_MAX}'         ,       ");
                sqlCmd.Append($"        '{Rapid_MIN}'           ,       ");
                sqlCmd.Append($"        '{Rapid_MAX}'                   ");
                sqlCmd.Append($"    )                           ;       ");
                sqlCmd.Append($"INSERT IGNORE INTO                      ");
                sqlCmd.Append($"    csi_auth.tbl_csiothersettingsvalues ");
                sqlCmd.Append($"    (                                   ");
                sqlCmd.Append($"        ConnectorId             ,       ");
                sqlCmd.Append($"        Machine_Name                    ");
                sqlCmd.Append($"    )                                   ");
                sqlCmd.Append($"    VALUES                              ");
                sqlCmd.Append($"    (                                   ");
                sqlCmd.Append($"         { ConnectorId }        ,       ");
                sqlCmd.Append($"        '{ Machine_Name }'              ");
                sqlCmd.Append($"    )                           ;       ");
            }
            else
            {
                sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettings                ");
                sqlCmd.Append($"   SET                                                     ");
                sqlCmd.Append($"      Rapid_Variable            = '{ RapidOver_Var }'    , ");
                sqlCmd.Append($"      Spindle_Variable          = '{ SpindleOver_Var }'  , ");
                sqlCmd.Append($"      FeedRate_Variable         = '{ FeedRateOver_Var }' , ");
                sqlCmd.Append($"      PartRequired_Variable     = '{ Part_Req_Var }'     , ");
                sqlCmd.Append($"      PartCount_Variable        = '{ Part_Count_Var }'   , ");
                sqlCmd.Append($"      ProgramNumber_Variable    = '{ Prog_No_Var }'      , ");
                sqlCmd.Append($"      ProgramNumber_FilterStart = '{ Prog_No_StartWith }', ");
                sqlCmd.Append($"      ProgramNumber_FilterEnd   = '{ Prog_No_EndWith }'  , ");
                sqlCmd.Append($"      PartNumber_Variable       = '{ Part_No_Var }'      , ");
                sqlCmd.Append($"      PartNumber_Filter1Start   = '{ Part_No_StartWith }', ");
                sqlCmd.Append($"      PartNumber_Filter1End     = '{ Part_No_EndWith }'  , ");
                sqlCmd.Append($"      FeedRate_MIN              = '{ Feed_MIN }'         , ");
                sqlCmd.Append($"      FeedRate_MAX              = '{ Feed_MAX }'         , ");
                sqlCmd.Append($"      Spindle_MIN               = '{ Spindle_MIN }'      , ");
                sqlCmd.Append($"      Spindle_MAX               = '{ Spindle_MAX }'      , ");
                sqlCmd.Append($"      Rapid_MIN                 = '{ Rapid_MIN }'        , ");
                sqlCmd.Append($"      Rapid_MAX                 = '{ Rapid_MAX }'          ");
                sqlCmd.Append($"   WHERE                                                   ");
                sqlCmd.Append($"      ConnectorId    =  { ConnectorId }                  ; ");
                sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettingsvalues          ");
                sqlCmd.Append($"   SET                                                     ");
                sqlCmd.Append($"      Rapid_Value         = '{ RapidOver_Value }'    ,     ");
                sqlCmd.Append($"      Spindle_Value       = '{ SpindleOver_Value }'  ,     ");
                sqlCmd.Append($"      FeedRate_Value      = '{ FeedRateOver_Value }' ,     ");
                sqlCmd.Append($"      PartRequired_Value  = '{ Part_Req_Value }'     ,     ");
                sqlCmd.Append($"      PartCount_Value     = '{ Part_Count_Value }'   ,     ");
                sqlCmd.Append($"      ProgramNumber_Value = '{ Prog_No_Value }'      ,     ");
                sqlCmd.Append($"      PartNumber_Value    = '{ Part_No_Value }'            ");
                sqlCmd.Append($"   WHERE                                                   ");
                sqlCmd.Append($"      ConnectorId    =  { ConnectorId }                    ");
            }
            try
            {
            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString());
            }
            catch (System.Exception ex)
            {
                Log.Error(ex);
            }
        }
    }
}
