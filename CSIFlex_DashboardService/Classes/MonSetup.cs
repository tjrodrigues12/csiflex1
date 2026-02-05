using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFlex_DashboardService.Classes
{
    public class MonSetup
    {
        DataLayer _data = null;
        ReadFiles _readFiles = null;
        public MonSetup()
        {
            _data = new DataLayer();
            _readFiles = new ReadFiles();
        }
        public void updateMonSetup(string fileName)
        {
            string[] lines2 = File.ReadLines(fileName).ToArray();
            int k = 1, l = 1, p = 0;
            int m2 = lines2.Length;
            while (p < m2)
            {
                if (k <= 16 && l <= 8)
                {
                    if (Convert.ToInt32(lines2[p]) == 0)
                    {
                        string query3 = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET mon_state_='" + Convert.ToInt32(lines2[p]) + "' WHERE monitoring_id_ ='" + Convert.ToString(k) + "," + Convert.ToString(l) + "'";
                        _data.executeNonQuery(query3);
                    }
                    else if (Convert.ToInt32(lines2[p]) == 1)
                    {
                        string query4 = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET monitoring_='1',mon_state_='" + Convert.ToInt32(lines2[p]) + "' WHERE monitoring_id_ ='" + Convert.ToString(k) + "," + Convert.ToString(l) + "'";
                        _data.executeNonQuery(query4);
                    }

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

        public void updteMultiplier(string fileName, string[] textLines2)
        {
            string[] cyclecountermul = File.ReadLines(fileName).ToArray(); //IT has cycle multiplier 
            int k = 1, l = 1, p = 0;
            int m3 = cyclecountermul.Length;
            while (p < m3)
            {
                if (k <= 16 && l <= 8)
                {
                    string query5 = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET part_multiplier_='" + Convert.ToInt32(cyclecountermul[p]) + "' WHERE monitoring_id_ ='" + Convert.ToString(k) + "," + Convert.ToString(l) + "'";
                    _data.executeNonQuery(query5);

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
            // Code for find cycle identifier (MIN/MAX or Ideal) ----->> IC:
            List<string> cycleiden = new List<string>();
            foreach (string line5 in textLines2)
            {
                if (line5.Contains("NM:"))
                {
                    cycleiden.Add(line5.Replace("NM:", "")); //used to set cycle count zero at end of shift if equal to 3 (if 1 then dont count cycle count)
                }
                if (line5.Contains("IC:"))
                {
                    cycleiden.Add(line5.Replace("IC:", ""));
                }
                if (line5.Contains("CI:"))
                {
                    cycleiden.Add(line5.Replace("CI:", ""));
                }
                if (line5.Contains("CA:"))
                {
                    cycleiden.Add(line5.Replace("CA:", ""));
                }
                if (line5.Contains("DA:"))
                {
                    cycleiden.Add(line5.Replace("DA:", ""));
                }
            }
            File.WriteAllLines(fileName, cycleiden);
        }

        public void updateCounter(string fileName)
        {
            string[] cycleiden_final = File.ReadLines(fileName).ToArray();
            int k = 1, l = 1, p = 0;
            int q = 1, r = 2, s = 3, t = 4;
            int m4 = cycleiden_final.Length;
            while (p < m4)
            {
                if (k <= 16 && l <= 8)
                {
                    if (Convert.ToInt32(cycleiden_final[q]) == 0) // MIN/MAX Cycle Type
                    {
                        string query6 = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET cycle_identifier_='MIN/MAX',min_ideal_='" + Convert.ToInt32(cycleiden_final[r]) + "',max_perc_='" + Convert.ToInt32(cycleiden_final[s]) + "',department_name_='" + cycleiden_final[t] + "',reset_counter_='" + Convert.ToInt32(cycleiden_final[p]) + "' WHERE monitoring_id_ ='" + Convert.ToString(k) + "," + Convert.ToString(l) + "'";
                        _data.executeNonQuery(query6);
                    }
                    else if (Convert.ToInt32(cycleiden_final[q]) == 1) // IDEAL Cycle Type In this Case if R<0 then we repalce Minimum value as 0
                    {
                        double a = Convert.ToDouble(cycleiden_final[r]), b = Convert.ToDouble(cycleiden_final[s]);
                        double percentage = 0.00;
                        percentage = Math.Ceiling((a * b) / 100);
                        int R = Convert.ToInt32(a + percentage);
                        int S = Convert.ToInt32(a - percentage);
                        if (S < 0)
                        {
                            S = 0;
                        }
                        string query7 = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET cycle_identifier_='IDEAL',min_ideal_='" + S + "',max_perc_='" + R + "',department_name_='" + cycleiden_final[t] + "',reset_counter_='" + Convert.ToInt32(cycleiden_final[p]) + "' WHERE monitoring_id_ ='" + Convert.ToString(k) + "," + Convert.ToString(l) + "'";
                        _data.executeNonQuery(query7);
                    }
                    k = Convert.ToInt32(k);
                    l = Convert.ToInt32(l);
                    l++;
                    p += 5;
                    q += 5;
                    r += 5;
                    s += 5;
                    t += 5;
                    if (l > 8)
                    {
                        l = 1;
                        k++;
                    }
                }
            }
        }

        public void updateMonsetupFiles(string[] filenames, Stopwatch watch)
        {
            int len = filenames.Length - 1;
            for (int filein = 0; filein < len; filein++)
            {
                //string query28 = "SELECT * FROM csi_dashboard.tbl_" + filenames[filein] + " WHERE current_status_<>'' ORDER BY correction_date_ DESC LIMIT 1";
                string query28 = "SELECT * FROM csi_dashboard.tbl_" + filenames[filein] + " WHERE current_status_<>'' ORDER BY correction_date_ DESC LIMIT 2";

                DataTable dtreader12 = new DataTable();
                dtreader12 = _data.executeQuery(query28);
                if (dtreader12.Rows.Count > 0)
                {
                    string status = dtreader12.Rows[0]["current_status_"].ToString();
                    int lastcycletime = Convert.ToInt32(dtreader12.Rows[0]["last_cycle_time"]); //last cycle time ahiya thi nahi levanu jyare cycle change thai tyare last cycle time ni value carry forward karvani
                                                                          //int elapsedtime = dtreader12.Rows[0].Field<int>(3);
                    DateTime datetime = Convert.ToDateTime(dtreader12.Rows[0]["correction_date_"]);
                    DateTime datetimeofprev = Convert.ToDateTime(dtreader12.Rows[1]["correction_date_"]);
                    string formatForMySql1 = datetime.ToString("yyyy-MM-dd HH:mm:ss");
                    //datetime.ToString();
                    string query36 = "SELECT * FROM csi_dashboard.tbl_monsetup_data WHERE machine_tmp_filename_='" + filenames[filein] + ".SYS_';";
                    DataTable dtq36 = new DataTable();
                    dtq36 = _data.executeQuery(query36);
                    if (dtq36.Rows.Count > 0)
                    {

                        int oldlastcycletime = Convert.ToInt32(dtq36.Rows[0]["old_last_cycle_time"]); // This may be 23 instead of 22 
                        //DateTime datetimemon = (DateTime?)(dtq36.Rows[0].Field<DateTime>(15) == DBNull.Value ? null : dtq36.Rows[0].Field<DateTime>(15));
                        Utility.WriteToFile("Date Crash: " + dtq36.Rows[0][16].ToString());
                        DateTime datetimemon = DateTime.MinValue;
                        if (CheckDate(dtq36.Rows[0]["correction_date_"].ToString()))
                        {
                            datetimemon =Convert.ToDateTime(dtq36.Rows[0]["correction_date_"]);//(DateTime)(dtq36.Rows[0][15] == DBNull.Value ? null : dtq36.Rows[0][15]);
                        }
                        //Convert.IsDBNull(dtq36.Rows[0].Field<DateTime>(15)) ? DateTime.MinValue : (DateTime)dtq36.Rows[0].Field<DateTime>(15);
                        //dtq36.Rows[0].Field<DateTime>(15);
                        if (datetimemon != datetime)
                        {
                            if (status == "_COFF")
                            {
                                string querry33 = "SELECT * FROM csi_dashboard.tbl_" + filenames[filein] + " WHERE current_status_<>'' ORDER BY correction_date_ desc LIMIT 2;";
                                DataTable dtq33 = new DataTable();
                                dtq33 = _data.executeQuery(querry33);

                                if (dtq33.Rows.Count > 1) // Means we have two rows
                                {
                                    string oldstatus = dtq33.Rows[1]["current_status_"].ToString();
                                    if (oldstatus == "_CON")
                                    {
                                        //DateTime last_correction_date_ = dtq33.Rows[0].Field<DateTime>(4);
                                        //DateTime current_correction_date_ = dtq33.Rows[1].Field<DateTime>(4);
                                        //TimeSpan ts1 = TimeSpan.Parse((DateTime.Now.TimeOfDay).ToString());
                                        //int timenow = Convert.ToInt32(ts.TotalSeconds);
                                        //DateTime time_difference = current_correction_date_ - last_correction_date_; 
                                        string query34 = "SELECT * FROM csi_dashboard.tbl_monsetup_data WHERE machine_tmp_filename_='" + filenames[filein] + ".SYS_';";
                                        DataTable dtq34 = new DataTable();
                                        dtq34 = _data.executeQuery(query34);
                                        if (dtq34.Rows.Count > 0)
                                        {
                                            //int part_count_ = dtq34.Rows[0].Field<int>(6);
                                            //int part_multiplier_ = dtq34.Rows[0].Field<int>(7);
                                            //int min_ideal_ = dtq34.Rows[0].Field<int>(9);
                                            //int max_perc_ = dtq34.Rows[0].Field<int>(10);
                                            int part_count_ = Convert.ToInt32(dtq34.Rows[0]["part_count_"]);
                                            int part_multiplier_ = Convert.ToInt32(dtq34.Rows[0]["part_multiplier_"]);
                                            int min_ideal_ = Convert.ToInt32(dtq34.Rows[0]["min_ideal_"]);
                                            int max_perc_ = Convert.ToInt32(dtq34.Rows[0]["max_perc_"]);
                                            int counter = 0;
                                            //DateTime datenow = DateTime.Now;
                                            //var diff = (datenow - datetime).TotalSeconds;
                                            var diff = (datetime-datetimeofprev).TotalSeconds;
                                            int elapsedTimeDiff = Convert.ToInt32(diff);
                                            if (elapsedTimeDiff >= min_ideal_)/* && elapsedtime <= max_perc_*/
                                            {
                                                Utility.WriteToFile("Here Part Count Added Successfully");
                                                counter = part_count_ + part_multiplier_;
                                                string query35 = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET part_count_='" + counter + "',old_part_count_='" + counter + "',last_cycle_time='" + oldlastcycletime + "' WHERE machine_tmp_filename_ ='" + filenames[filein] + ".SYS_'; ";// was addded before hereIGNORE
                                                _data.executeNonQuery(query35);
                                            }
                                            else
                                            {
                                                Utility.WriteToFile("Time Difference :" + elapsedTimeDiff + " Min Value :" + min_ideal_ + " for File Name :" + filenames[filein]);
                                            }

                                        }

                                    }
                                    else
                                    { // If Previous Status is other than Cycle On Then do nothing 
                                    }
                                }
                                else
                                if (dtq33.Rows.Count == 1)
                                {
                                    // It has only one row which has status Do nothing Here
                                    string query35_2 = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET last_cycle_time='" + oldlastcycletime + "' WHERE machine_tmp_filename_ ='" + filenames[filein] + ".SYS_'; ";// was addded before hereIGNORE
                                    _data.executeNonQuery(query35_2);
                                }
                                //readerq33.Close();
                            }
                        }
                        else
                        {
                            //
                        }

                    }
                    //readerq36.Close();
                    watch.Stop();
                    double TN = watch.Elapsed.TotalSeconds;
                    int sec = Convert.ToInt32(TN);
                    DateTime datenow1 = DateTime.Now;
                    var diff1 = (datenow1 - datetime).TotalSeconds;
                    int elapsedTimeDiff1 = Convert.ToInt32(diff1);
                    int newelapsed = elapsedTimeDiff1;
                    string query32 = "UPDATE IGNORE csi_dashboard.tbl_" + filenames[filein] + " SET elapsed_time='" + newelapsed + "' WHERE machine_tmp_filename_ ='" + filenames[filein] + "' AND current_status_<>'' ORDER BY correction_date_ DESC LIMIT 1;";
                    _data.executeNonQuery(query32);

                    /// Update this Query for last cycle time :::::::::: Old Statement :::
                    string query29 = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET current_status_='" + status + "',last_cycle_time='" + lastcycletime + "',elapsed_time='" + newelapsed + "',correction_date_='" + formatForMySql1 + "' WHERE machine_tmp_filename_ ='" + filenames[filein] + ".SYS_';";
                    _data.executeNonQuery(query29);
                    Utility.WriteToFile(TN.ToString() + "Seconds");
                    //label2.Text = TN.ToString() + "Seconds";
                }
                //reader12.Close();
                // string query30 = "SELECT * FROM csi_dashboard.tbl_" + filenames[filein] + " WHERE machine_part_no_<>'' ORDER BY correction_date_ DESC LIMIT 1";
                string query30 = "(SELECT * FROM csi_dashboard.tbl_" + filenames[filein] + " WHERE machine_part_no_ LIKE '%OID:%' order by correction_date_ desc limit 1) union (SELECT * FROM csi_dashboard.tbl_" + filenames[filein] + " WHERE machine_part_no_ NOT LIKE '%OID:%' AND machine_part_no_<>''order by correction_date_ desc limit 1);";
                _data.executeNonQuery(query30);
                DataTable dtreader13 = new DataTable();
                dtreader13 = _data.executeQuery(query30);
                if (dtreader13.Rows.Count == 2) //Means we have two records consists of Operator id and Part Number
                {
                    string operatorID = dtreader13.Rows[0]["machine_part_no_"].ToString();
                    string partnum = dtreader13.Rows[1]["machine_part_no_"].ToString();
                    string queryunion = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET machine_part_no_='" + partnum + " " + operatorID + "' WHERE machine_tmp_filename_ ='" + filenames[filein] + ".SYS_';";
                    _data.executeNonQuery(queryunion);
                }
                if (dtreader13.Rows.Count == 1) // We have output from either one of the tables(Part Number OR Operator ID)
                {
                    string partnum = dtreader13.Rows[0]["machine_part_no_"].ToString();
                    string query31 = "UPDATE IGNORE csi_dashboard.tbl_monsetup_data SET machine_part_no_='" + partnum + "'  WHERE machine_tmp_filename_ ='" + filenames[filein] + ".SYS_';";
                    _data.executeNonQuery(query31);
                }
            }
        }

        protected bool CheckDate(String date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
