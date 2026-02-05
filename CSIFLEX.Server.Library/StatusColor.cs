using CSIFLEX.Database.Access;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFLEX.Server.Library
{
    public class StatusColor
    {
        #region singleton 
        private StatusColor() { }

        private static StatusColor _instance = null;

        public static StatusColor Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new StatusColor();
                return _instance;
            }
        }
        #endregion

        public static Dictionary<string, string> Colors { get; set; }

        public void Init()
        {
            Colors = new Dictionary<string, string>();

            DataTable tblColors = MySqlAccess.GetDataTable("SELECT * FROM csi_database.tbl_colors");

            foreach(DataRow row in tblColors.Rows)
            {
                Colors.Add(row["Statut"].ToString().ToUpper(), row["color"].ToString());
            }
        }

        public static string GetColor(string status)
        {
            string color = "#b4b4b4";

            if (Colors.ContainsKey(status))
                color = Colors[status];

            return color;
        }
    }
}
