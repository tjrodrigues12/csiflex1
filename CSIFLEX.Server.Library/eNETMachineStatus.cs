using CSIFLEX.Database.Access;
using CSIFLEX.eNetLibrary;
using CSIFLEX.eNetLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFLEX.Server.Library
{
    public static class eNETMachineStatus
    {
        public static void LoadMachinesStatus()
        {
            int machineId;

            foreach (eNetMachineConfig mach in eNetServer.Machines)
            {
                machineId = int.Parse(MySqlAccess.ExecuteScalar($"SELECT Id FROM csi_auth.tbl_ehub_conf WHERE EnetMachineName = '{ mach.MachineName }'").ToString());

                foreach(KeyValuePair<string,string> pair in mach.CommandsAvailable)
                {
                    MySqlAccess.ExecuteNonQuery($"INSERT IGNORE INTO csi_auth.tbl_mach_status (MachineId, Status) VALUES ({ machineId }, '{ pair.Key }');");
                }
            }
        }

    }
}
