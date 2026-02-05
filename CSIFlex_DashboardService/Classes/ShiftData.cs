using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFlex_DashboardService.Classes
{
    public class ShiftData
    {       

        public void transactShiftData(string[] filelength)
        {
            DataLayer _dataLayer = new DataLayer();
            int count = filelength.Length;
            string[] weekdays = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            int loc = 2, limit = 44, filenameinit = 1;
            while (loc < limit && loc < count)
            {
                for (int days = 0; days < 7; days++)
                {
                    for (int ct = 1; ct < 4; ct++)
                    {
                        string sh_start = filelength[loc];
                        string sh_end = filelength[loc + 1];
                        string br1_start = filelength[loc + 42];
                        string br1_end = filelength[loc + 43];
                        string br2_start = filelength[loc + 84];
                        string br2_end = filelength[loc + 85];
                        string br3_start = filelength[loc + 126];
                        string br3_end = filelength[loc + 127];
                        loc = loc + 2;
                        string querycheck = "SELECT * FROM csi_dashboard.tbl_shift_data WHERE department_name_='" + filelength[filenameinit] + "' AND day_name_='" + weekdays[days] + "' AND shift_name_='Shift " + ct.ToString() + "'";
                                                
                        var dtreaderquerycheck = _dataLayer.executeQuery(querycheck);
                        
                        if (dtreaderquerycheck.Rows.Count == 0)
                        {  // This code for first time INSERT
                            string query1_shifttbl = "INSERT INTO csi_dashboard.tbl_shift_data (department_name_,day_name_,shift_name_,shift_start_,shift_end_,break1_start_,break1_end_,break2_start_,break2_end_,break3_start_,break3_end_) VALUES ('" + filelength[filenameinit] + "','" + weekdays[days] + "','Shift " + ct.ToString() + "','" + Convert.ToInt32(sh_start) + "','" + Convert.ToInt32(sh_end) + "','" + Convert.ToInt32(br1_start) + "','" + Convert.ToInt32(br1_end) + "','" + Convert.ToInt32(br2_start) + "','" + Convert.ToInt32(br2_end) + "','" + Convert.ToInt32(br3_start) + "','" + Convert.ToInt32(br3_end) + "') ";
                            _dataLayer.executeNonQuery(query1_shifttbl);
                        }
                        else
                        {
                            string UPDATE_shifttbl = "UPDATE IGNORE csi_dashboard.tbl_shift_data SET shift_start_='" + Convert.ToInt32(sh_start) + "',shift_end_='" + Convert.ToInt32(sh_end) + "',break1_start_='" + Convert.ToInt32(br1_start) + "',break1_end_='" + Convert.ToInt32(br1_end) + "',break2_start_='" + Convert.ToInt32(br2_start) + "',break2_end_='" + Convert.ToInt32(br2_end) + "',break3_start_='" + Convert.ToInt32(br3_start) + "',break3_end_='" + Convert.ToInt32(br3_end) + "' WHERE department_name_='" + filelength[filenameinit] + "'AND day_name_='" + weekdays[days] + "'AND shift_name_='Shift " + ct.ToString() + "'";
                            _dataLayer.executeNonQuery(UPDATE_shifttbl);
                        }                       
                    }
                }
                loc = loc + 127;
                limit = loc + 42;
                filenameinit = filenameinit + 169;
            }
        }
    }
}
