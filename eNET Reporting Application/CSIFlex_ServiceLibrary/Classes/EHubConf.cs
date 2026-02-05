using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFlex_ServiceLibrary.Classes
{
    public class EHubConf
    {
        DataLayer _data = null;
        ReadFiles _readFiles = null;
        public EHubConf()
        {
            _data = new DataLayer();
            _readFiles = new ReadFiles();
        }

        public void setEhubConf(ICollection<KeyValuePair<String, String>> tmpfilelist)
        {
            var lines = _readFiles.getEHubConf();
            //Store all lines containing machine names into string array
            string today = System.DateTime.Now.DayOfWeek.ToString();
            foreach (KeyValuePair<string, string> element in tmpfilelist)
            {
                string query_ehub = "INSERT IGNORE INTO csi_dashboard.tbl_ehubconf_data (monitoring_id_,machine_name_,machine_tmp_filename_,file_size) VALUES ('" + element.Key + "','','" + element.Value + "','0')";
                _data.executeNonQuery(query_ehub);
                string query_mon_setup = "INSERT IGNORE INTO csi_dashboard.tbl_monsetup_data(monitoring_id_,machine_name_,machine_tmp_filename_,monitoring_,mon_state_,part_count_,part_multiplier_,cycle_identifier_, min_ideal_, max_perc_,current_status_,last_cycle_time,elapsed_time,machine_part_no_, correction_date_,department_name_,reset_counter_,day_name_,shift_name_ ,shift_start_, shift_end_,old_last_cycle_time,old_shift_name_,old_part_count_) VALUES ('" + element.Key + "','','" + element.Value + "','0','0','0','0','MIN/MAX','','','','0','0','','','','','" + today + "','','','','0','','0')";
                _data.executeNonQuery(query_mon_setup);
            }

        }

        public void updateEhubConf(string[] lines)
        {
            int k = 1, l = 1, p = 0;
            string today = System.DateTime.Now.DayOfWeek.ToString();
            int m = lines.Length;
            while (p < m)
            {
                if (k <= 16 && l <= 8)
                {
                    string query = "UPDATE IGNORE csi_dashboard.tbl_ehubconf_data SET machine_name_ = '" + lines[p].ToString() + "' WHERE monitoring_id_ ='" + Convert.ToString(k) + "," + Convert.ToString(l) + "'";
                    _data.executeNonQuery(query);
                    string query2 = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET machine_name_ = '" + lines[p].ToString() + "',day_name_='" + today + "' WHERE monitoring_id_ ='" + Convert.ToString(k) + "," + Convert.ToString(l) + "'";
                    _data.executeNonQuery(query2);
                    k = Convert.ToInt32(k);
                    l = Convert.ToInt32(l);
                    l++;
                    p++;
                    if (l > 8)
                    {
                        l = 1;
                        k++;
                    }
                }
            }
        }

        public DataTable getEhubDataByMachineName(string filenames)
        {
            ReadFiles _read = new ReadFiles();
            DataLayer _data = new DataLayer();
            //int filelength1 = _read.getFileSize(filenames);
            string query25 = "SELECT * FROM csi_dashboard.tbl_ehubconf_data WHERE machine_tmp_filename_='" + filenames + ".SYS_';";
            return _data.executeQuery(query25);
        }

        public void updateAndCreateEhubTbl(int filelength1, string filenames, byte[] file_hash)
        {
            string query26 = "UPDATE csi_dashboard.tbl_ehubconf_data SET file_size = @file_size, file_hash=@file_hash WHERE machine_tmp_filename_ ='" + filenames + ".SYS_'";
            MySqlParameter[] param = new MySqlParameter[] {
                 new MySqlParameter("@file_size", filelength1),
                 new MySqlParameter("@file_hash", file_hash),
            };
            //Utility.WriteToFile(query26);
            _data.executeQueryWithParameter(query26, param);
            string query8 = "CREATE TABLE if not exists csi_dashboard.tbl_" + filenames + "(machine_tmp_filename_ varchar(255), current_status_ varchar(255) , last_cycle_time integer, elapsed_time integer, correction_date_ DATETIME ,machine_part_no_ varchar(255) );";
            _data.executeNonQuery(query8);
        }

        public void updatePartNo(string[] l1_final, string filenames, int r1, int p1, int q1)
        {
            string updatepart;
            string operatorID = string.Empty;
            if (l1_final[r1].Contains("_OPERATOR"))
            {
                operatorID = l1_final[r1].Replace("_OPERATOR", "OID");
                updatepart = operatorID;
            }
            else
            {
                string newpartno = l1_final[r1].Split(':')[1];
                updatepart = newpartno;
            }
            string dateformat = l1_final[p1];
            string timeformat = l1_final[q1];
            string datetime = dateformat + " " + timeformat;

            //tDate = Date.Parse("04/14/08", ci) 'this date format is Month/Day/Year
            DateTime date = DateTime.ParseExact(datetime, "MM/dd/yy HH:mm:ss", CultureInfo.InvariantCulture);// Convert.ToDateTime("11-27-17 15:30:00"); //datetime
            string formatForMySql = date.ToString("yyyy-MM-dd HH:mm:ss");

            string query13 = "SELECT * FROM csi_dashboard.tbl_" + filenames + ";";
            DataTable dttable = _data.executeQuery(query13);


            if (dttable.Rows.Count == 0) // When the table has zero records (the first time data will be added here)
            {
                //Code for first time INSERT Values
                string query_monsetup = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET machine_part_no_='" + updatepart + "' WHERE machine_tmp_filename_ ='" + filenames + ".SYS_';";

                string query40 = "INSERT INTO csi_dashboard.tbl_" + filenames + "(machine_tmp_filename_ , current_status_ , last_cycle_time , elapsed_time , correction_date_ ,machine_part_no_) VALUES ('" + filenames + "','','0','0','" + formatForMySql + "','" + updatepart + "');" + query_monsetup;
                _data.executeNonQuery(query40);
            }
            else
            {  //When table has records already saved 
                string query11 = "SELECT * FROM csi_dashboard.tbl_" + filenames + " WHERE  machine_part_no_='" + updatepart + "' AND correction_date_ ='" + formatForMySql + "' AND current_status_=''";
                DataTable dttable1 = _data.executeQuery(query11);

                if (dttable1.Rows.Count == 0)
                {

                    string query14 = "INSERT IGNORE INTO csi_dashboard.tbl_" + filenames + "(machine_tmp_filename_ , current_status_ , last_cycle_time , elapsed_time , correction_date_ ,machine_part_no_) VALUES ('" + filenames + "','','0','0','" + formatForMySql + "','" + updatepart + "');";
                    _data.executeNonQuery(query14);
                    string UPDATEMonsetup = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET part_count_='0',old_part_count_='0',  machine_part_no_='" + updatepart + "' WHERE machine_tmp_filename_='" + filenames + ".SYS_'";
                    _data.executeNonQuery(UPDATEMonsetup);
                    Utility.WriteToFile(":::::::::::::::::::: Code For changing Part Cound and Part Numnber :::::::::::::::");
                }
                else if (dttable1.Rows.Count > 0)
                {

                }
            }
        }

        public void updateIfNoPartNo(string[] l1_final, string filenames, int r1, int p1, int q1)
        {
            //Normal entreis that has no part number means This notes all cysles of the machines (NO CHANGES TO THE PART NUMBER) 
            string dateformat = l1_final[p1];
            string timeformat = l1_final[q1];
            string datetime = dateformat + " " + timeformat;
            //Utility.WriteToFile(datetime);
            DateTime date = DateTime.ParseExact(datetime, "MM/dd/yy HH:mm:ss", CultureInfo.InvariantCulture);//DateTime.Parse(datetime);
            string formatForMySql = date.ToString("yyyy-MM-dd HH:mm:ss");
            //DateTime date = Convert.ToDateTime(datetime);
            string query15 = "SELECT * FROM csi_dashboard.tbl_" + filenames + ";";

            DataTable dttable2 = _data.executeQuery(query15);
            if (dttable2.Rows.Count == 0)
            {
                //Code for first time insert for Machine Status
                string getlastcyclefromMonsetup = "SELECT * FROM csi_dashboard.tbl_monsetup_data WHERE machine_tmp_filename_='" + filenames + ".SYS_'";

                DataTable dtgetlastcyclefromMonsetup = _data.executeQuery(getlastcyclefromMonsetup);
                if (dtgetlastcyclefromMonsetup.Rows.Count > 0)
                {

                    int old_last_cycle_time = Convert.ToInt32(dtgetlastcyclefromMonsetup.Rows[0]["old_last_cycle_time"]);
                    DateTime datenow = System.DateTime.Now;
                    var DifferenceInSec = (datenow - date).TotalSeconds;
                    //,correction_date_ = '" + formatForMySql + "'
                    int difference = Convert.ToInt32(DifferenceInSec);
                    string query_monsetup_update = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET current_status_='" + l1_final[r1] + "',last_cycle_time='" + old_last_cycle_time + "',elapsed_time='" + difference + "' WHERE machine_tmp_filename_ ='" + filenames + ".SYS_';";
                    string query23 = "INSERT INTO csi_dashboard.tbl_" + filenames + "(machine_tmp_filename_ , current_status_ , last_cycle_time , elapsed_time , correction_date_ ,machine_part_no_) VALUES ('" + filenames + "','" + l1_final[r1] + "','" + old_last_cycle_time + "','" + difference + "','" + formatForMySql + "','');" + query_monsetup_update;
                    _data.executeNonQuery(query23);
                }
                // This down one is an old Code 
                //DateTime datenow = System.DateTime.Now;
                //var DifferenceInSec = (datenow - date).TotalSeconds;
                //int difference = Convert.ToInt32(DifferenceInSec);
                //string query16 = "INSERT INTO csi_dashboard.tbl_" + filenames[filein] + "(machine_tmp_filename_ , current_status_ , last_cycle_time , elapsed_time , correction_date_ ,machine_part_no_) VALUES ('" + filenames[filein] + "','" + l1_final[r1] + "','0','" + difference + "','" + formatForMySql + "','');";
                //MySqlCommand cmdINSERT5 = new MySqlCommand(query16, mysqlcon);
                //cmdINSERT5.ExecuteNonQuery();
            }
            else
            { // If table is having records greater than zero

                string query12 = "SELECT * FROM csi_dashboard.tbl_" + filenames + " WHERE current_status_='" + l1_final[r1] + "' AND correction_date_ ='" + formatForMySql + "' AND  machine_part_no_=''";

                DataTable dttable3 = _data.executeQuery(query12);
                if (dttable3.Rows.Count == 0) //This means record  not exists previously
                {
                    // :::::::::::::::: Add here code if [r1-3] is empty so [r1-3] ni jagya e last 2 records levana 1st[0] is the latest one and 2nd[1] is the later one ::: Before ::::current_status_='" + l1_final[r1 - 3] + "'
                    string query17 = "SELECT * FROM csi_dashboard.tbl_" + filenames + " WHERE current_status_<>'' ORDER BY correction_date_ DESC LIMIT 1"; //This gives last row where Current Status is CON and Order By Date gives last entery first
                    DataTable dttable4 = _data.executeQuery(query17);
                    if (dttable4.Rows.Count > 0) //Record Found
                    {
                        int prevelapse = Convert.ToInt32(dttable4.Rows[0]["elapsed_time"]);
                        int prevlastcycle = Convert.ToInt32(dttable4.Rows[0]["last_cycle_time"]);
                        string last_status_ = Convert.ToString(dttable4.Rows[0]["current_status_"]);
                        DateTime datenow1 = System.DateTime.Now;
                        var DifferenceInSec1 = (datenow1 - date).TotalSeconds;
                        int difference1 = Convert.ToInt32(DifferenceInSec1);
                        if (last_status_ == "_CON")
                        { /* HERE WE NEED TO ADD CODE FOR CYCLE COUNT AND CYCLE MULTIPLIER

                                             */

                            if (l1_final[r1] == "_COFF")
                            {// Changes from ON to OFF
                             /*// Means last cycle is ON and Now it is Off then Last Cycle Set equal to elapsae time of the previous one and current cycle is set to zero 
                          * Adds Also Cycle Count Logic Here
                          */
                             //string dateformat1 = l1_final[p1 - 3];
                             //string timeformat1 = l1_final[q1 - 3];
                             //string datetime1 = dateformat1 + " " + timeformat1;
                             //DateTime date1 = DateTime.ParseExact(datetime1, "MM/dd/yy HH:mm:ss", CultureInfo.InvariantCulture); //DateTime.Parse(datetime1);
                                DateTime last_correction_date_ = Convert.ToDateTime(dttable4.Rows[0]["correction_date_"]);
                                var differenceinsec = (date - last_correction_date_).TotalSeconds;
                                //string formatForMySql1 = date1.ToString("yyyy-MM-dd HH:mm:ss"); 
                                //TimeSpan ts = TimeSpan.Parse((date1.TimeOfDay).ToString()); ,correction_date_='" + formatForMySql + "'
                                int newpreviouscycle = Convert.ToInt32(differenceinsec);
                                string query18 = "INSERT INTO csi_dashboard.tbl_" + filenames + "(machine_tmp_filename_ , current_status_ , last_cycle_time , elapsed_time , correction_date_ ,machine_part_no_) VALUES ('" + filenames + "','" + l1_final[r1] + "','" + newpreviouscycle + "','" + difference1 + "','" + formatForMySql + "','');"; //prevelapse
                                _data.executeNonQuery(query18);
                                string updatemon = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET old_last_cycle_time = '" + newpreviouscycle + "', current_status_='" + l1_final[r1] + "',last_cycle_time='" + prevlastcycle + "',elapsed_time='" + difference1 + "'  WHERE machine_tmp_filename_='" + filenames + ".SYS_'";
                                _data.executeNonQuery(updatemon);
                            }
                            else
                            {

                                // Means last cycle is ON and Now it is CHANGES TO ONE OF ABOVE then Last Cycle is not changed  and current cycle is set to zero elapse time always START COUNTING ,correction_date_='" + formatForMySql + "'
                                string query_monsetup_update = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET current_status_='" + l1_final[r1] + "',last_cycle_time='" + prevlastcycle + "',elapsed_time='" + difference1 + "' WHERE machine_tmp_filename_ ='" + filenames + ".SYS_';";
                                string query19 = "INSERT INTO csi_dashboard.tbl_" + filenames + "(machine_tmp_filename_ , current_status_ , last_cycle_time , elapsed_time , correction_date_ ,machine_part_no_) VALUES ('" + filenames + "','" + l1_final[r1] + "','" + prevlastcycle + "','" + difference1 + "','" + formatForMySql + "','');" + query_monsetup_update;
                                _data.executeNonQuery(query19);
                            }

                        }
                        else if (last_status_ == "_COFF") // If previous cycle is OFF ,correction_date_='" + formatForMySql + "'
                        {
                            string query_monsetup_update = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET current_status_='" + l1_final[r1] + "',last_cycle_time='" + prevlastcycle + "',elapsed_time='" + difference1 + "' WHERE machine_tmp_filename_ ='" + filenames + ".SYS_';";
                            string query20 = "INSERT INTO csi_dashboard.tbl_" + filenames + "(machine_tmp_filename_ , current_status_ , last_cycle_time , elapsed_time , correction_date_ ,machine_part_no_) VALUES ('" + filenames + "','" + l1_final[r1] + "','" + prevlastcycle + "','" + difference1 + "','" + formatForMySql + "','');" + query_monsetup_update;
                            _data.executeNonQuery(query20);
                            //if (l1_final[r1] == "_CON")
                            //{
                            //    // When cycle changes from OFF to On then :: last cycle no change current cycle is start from zero from Zero equal to elapsed time elapsed time starts from zero
                            //    string query20 = "INSERT INTO csi_dashboard.tbl_" + filenames + "(machine_tmp_filename_ , current_status_ , last_cycle_time , elapsed_time , correction_date_ ,machine_part_no_) VALUES ('" + filenames + "','" + l1_final[r1] + "','" + prevlastcycle + "','" + difference1 + "','" + formatForMySql + "','');";
                            //    _data.executeNonQuery(query20);

                            //}
                            //else
                            //{
                            //    // Means last cycle is OFF and Now it is CHANGES TO ONE OF ABOVE then Last Cycle is not changed  and current cycle is set to zero elapse time always START COUNTING
                            //    string query21 = "INSERT INTO csi_dashboard.tbl_" + filenames + "(machine_tmp_filename_ , current_status_ , last_cycle_time , elapsed_time , correction_date_ ,machine_part_no_) VALUES ('" + filenames + "','" + l1_final[r1] + "','" + prevlastcycle + "','" + difference1 + "','" + formatForMySql + "','');";
                            //    _data.executeNonQuery(query21);

                            //}
                        }
                        else
                        {
                            //MessageBox.Show("States Changes in between other state not from ON or Off"); ,correction_date_='" + formatForMySql + "'
                            string query_monsetup_update = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET current_status_='" + l1_final[r1] + "',last_cycle_time='" + prevlastcycle + "',elapsed_time='" + difference1 + "' WHERE machine_tmp_filename_ ='" + filenames + ".SYS_';";
                            string query22 = "INSERT INTO csi_dashboard.tbl_" + filenames + "(machine_tmp_filename_ , current_status_ , last_cycle_time , elapsed_time , correction_date_ ,machine_part_no_) VALUES ('" + filenames + "','" + l1_final[r1] + "','" + prevlastcycle + "','" + difference1 + "','" + formatForMySql + "','');" + query_monsetup_update;
                            _data.executeNonQuery(query22);
                        }
                    }
                    else
                    { //Add code here This Means we dont have previous record where we have status Change last_cycle_time here 
                        DateTime datenow2 = System.DateTime.Now;
                        var DifferenceInSec2 = (datenow2 - date).TotalSeconds;
                        int difference2 = Convert.ToInt32(DifferenceInSec2);
                        string getlastcyclefromMonsetup = "SELECT * FROM csi_dashboard.tbl_monsetup_data WHERE machine_tmp_filename_='" + filenames + ".SYS_'";

                        DataTable dtgetlastcyclefromMonsetup = _data.executeQuery(getlastcyclefromMonsetup);
                        if (dtgetlastcyclefromMonsetup.Rows.Count > 0)
                        {
                            //,correction_date_ = '" + formatForMySql + "'
                            int old_last_cycle_time = dtgetlastcyclefromMonsetup.Rows[0].Field<int>(22);
                            string query_monsetup_update = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET current_status_='" + l1_final[r1] + "',last_cycle_time='" + old_last_cycle_time + "',elapsed_time='" + difference2 + "' WHERE machine_tmp_filename_ ='" + filenames + ".SYS_';";
                            string query23 = "INSERT INTO csi_dashboard.tbl_" + filenames + "(machine_tmp_filename_ , current_status_ , last_cycle_time , elapsed_time , correction_date_ ,machine_part_no_) VALUES ('" + filenames + "','" + l1_final[r1] + "','" + old_last_cycle_time + "','" + difference2 + "','" + formatForMySql + "','');" + query_monsetup_update;
                            _data.executeNonQuery(query23);
                        }
                        //readergetlastcyclefromMonsetup.Close();
                    }
                    //datareader5.Close();
                }
                else // This means  record already exists 
                { //Update Elapsed Time Every Second
                  // Code here for the last entry which has cycle not emty Updates it's elapse_time every second
                    DateTime datenow3 = System.DateTime.Now;
                    var DifferenceInSec3 = (datenow3 - date).TotalSeconds;
                    int difference3 = Convert.ToInt32(DifferenceInSec3);
                    string query24 = "UPDATE IGNORE csi_dashboard.tbl_" + filenames + " SET elapsed_time ='" + difference3 + "' WHERE current_status_<>'' ORDER BY correction_date_ DESC LIMIT 1;";
                    _data.executeNonQuery(query24);

                }
                //datareader4.Close();
            }
            p1 += 3;
            q1 += 3;
            r1 += 3;
            //datareader3.Close();
        }
    }


}
