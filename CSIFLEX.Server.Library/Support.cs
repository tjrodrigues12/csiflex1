using CSIFLEX.Database.Access;
using CSIFLEX.Server.Library.DataModel;
using System;
using System.Collections.Generic;
using System.Data;

namespace CSIFLEX.Server.Library
{
    public static class Support
    {
        public static List<MachineDBTable> GetMachineTables(string strConnection = "")
        {
            List<MachineDBTable> machines = new List<MachineDBTable>();

            DataTable dt;

            try
            {

                if (strConnection == "")
                    dt = MySqlAccess.GetDataTable("SELECT * FROM csi_auth.tbl_ehub_conf WHERE Monstate = 1 ORDER BY Machine_Name;");
                else
                    dt = MySqlAccess.GetDataTable("SELECT * FROM csi_auth.tbl_ehub_conf WHERE Monstate = 1 ORDER BY Machine_Name;", strConnection);

                foreach (DataRow row in dt.Rows)
                {
                    int machineId = int.Parse(row["Id"].ToString());
                    string machine = row["machine_name"].ToString();
                    string enetMach = row["EnetMachineName"].ToString();
                    string table = GetMachineTableName(row["EnetMachineName"].ToString());

                    machines.Add(new MachineDBTable()
                    {
                        MachineId = machineId,
                        MachineName = machine,
                        EnetMachineName = enetMach,
                        TableName = table
                    });
                }
                return machines;

            } catch(Exception ex)
            {
                throw new Exception($"GetMachineTables: {strConnection}", ex);
            }
        }


        public static string GetMachineTableName(string machineName)
        {
            string machine = $"tbl_{machineName}";
            string ch = "";

            for (int i = 32; i <= 47; i++){
                ch = ((char)i).ToString();
                machine = machine.Replace(ch, "_c" + i + "_");
            }

            for (int i = 58; i <= 64; i++){
                ch = ((char)i).ToString();
                machine = machine.Replace(ch, "_c" + i + "_");
            }

            for (int i = 91; i <= 96; i++){
                if (i != 95)
                {
                    ch = ((char)i).ToString();
                    machine = machine.Replace(ch, "_c" + i + "_");
                }
            }

            for (int i = 123; i <= 126; i++){
                ch = ((char)i).ToString();
                machine = machine.Replace(ch, "_c" + i + "_");
            }

            return machine;
        }

    }
}
