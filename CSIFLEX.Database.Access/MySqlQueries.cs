using CSIFLEX.Utilities;
using System;
using System.Text;

namespace CSIFLEX.Database.Access
{
    public static class MySqlQueries
    {
        public static void InsertRenameMachine(string machineName)
        {
            try
            {
                string tableName = Util.MachineDbTableName(machineName);

                StringBuilder command = new StringBuilder();
                command.Append($"DELETE FROM CSI_Database.tbl_renameMachines WHERE table_name = '{ tableName }' OR original_name = '{ machineName }' ; ");

                command.Append($"INSERT IGNORE INTO CSI_Database.tbl_renameMachines ( table_name, original_name ) VALUES ( '{ tableName }', '{ machineName }' ); ");

                MySqlAccess.ExecuteNonQuery(command.ToString());

            } catch (Exception ex)
            {
                Log.Error(ex);
            }
        }
    }
}
