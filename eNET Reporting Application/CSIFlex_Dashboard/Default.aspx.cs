using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using MySql.Data.MySqlClient;
using CSI_Library;
using System.Text;
using System.Drawing;
using System.Configuration;
using System.Data;
using System.IO;

namespace CSIFlex_Dashboard
{
    public partial class Default : System.Web.UI.Page
    {
        //private Timer timer1;
        MySqlConnection mysqlcon = new MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                MySqlCommand cmdSELECT = mysqlcon.CreateCommand();
                mysqlcon.Open();
                if (!IsPostBack)
                {
                    //string queryforall = "SELECT * FROM csi_dashboard.tbl_monsetup_data WHERE mon_state_='1';";
                    //MySqlCommand cmdqueryforall = new MySqlCommand(queryforall, mysqlcon);
                    //MySqlDataReader readerqueryforall = cmdqueryforall.ExecuteReader();
                    //DataTable dtqueryforall = new DataTable();
                    //dtqueryforall.Load(readerqueryforall);
                    //if (dtqueryforall.Rows.Count > 0)
                    //{
                    //    foreach (DataRow dr in dtqueryforall.Rows)
                    //    {
                    //        //string current_status_ = dr[11].ToString();
                    //        //var current_cycle_time = 0;
                    //        //if (current_status_ == "_CON")
                    //        //{
                    //        //    // current time eauals to elapsed time 
                    //        //    DateTime correction_date = Convert.ToDateTime(dr[16].ToString());
                    //        //    DateTime now = System.DateTime.Now;
                    //        //    current_cycle_time = Convert.ToInt32((now - correction_date).TotalSeconds);
                    //        //}
                    //        //else
                    //        //{
                    //        //    // current time euals to 0
                    //        //    //string select_query = "SELECT machine_name_,current_status_,machine_part_no_,part_count_,SEC_TO_TIME(0) as last_cycle_time,SEC_TO_TIME(current_cycle_time) as current_cycle_time,SEC_TO_TIME(TIMESTAMPDIFF(SECOND,correction_date_,now())) as elapsed_time FROM csi_dashboard.tbl_monsetup_data WHERE mon_state_='1';";
                    //        //    current_cycle_time = 0;

                    //        // }
                    string select_query = "SELECT machine_name_,current_status_,machine_part_no_,part_count_,SEC_TO_TIME(last_cycle_time) as last_cycle_time,SEC_TO_TIME(TIMESTAMPDIFF(SECOND,correction_date_,now())) as current_cycle_time,SEC_TO_TIME(TIMESTAMPDIFF(SECOND,correction_date_,now())) as elapsed_time FROM csi_dashboard.tbl_monsetup_data WHERE mon_state_='1';";
                    MySqlCommand cmdSELECTDATA = new MySqlCommand(select_query, mysqlcon);
                    MySqlDataReader readercmdSELECTDATA = cmdSELECTDATA.ExecuteReader();
                    DataTable dtcmdSELECTDATA = new DataTable();
                    dtcmdSELECTDATA.Load(readercmdSELECTDATA);
                    if (dtcmdSELECTDATA.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dtcmdSELECTDATA.Rows)
                        {
                            string status = dr1.Field<string>(1);
                            if (status == "_CON")
                            {
                                status = "CYCLE ON";
                            }
                            else if (status == "_COFF")
                            {
                                status = "CYCLE OFF";
                            }
                            else if (status == "_SETUP")
                            {
                                status = "SETUP";
                            }
                            dr1[1] = status;
                        }

                    }
                    GridView1.DataSource = dtcmdSELECTDATA;
                    GridView1.DataBind();
                    //  }

                    // }


                }
                mysqlcon.Close();

            }
            catch (Exception ex)
            {
                ErrorDis.Visible = true;
                ErrorDis.Text = ex.Message;
            }
            finally
            {
                mysqlcon.Close();
                //ErrorDis.Visible = false;
            }

        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string StatusId;
                foreach (GridViewRow row in GridView1.Rows)
                {
                    Label status = (Label)row.FindControl("lblStatus");
                    Label currenttime = (Label)row.FindControl("lblCurrentCycle");
                    StatusId = status.Text;
                    if (string.IsNullOrEmpty(e.Row.Cells[1].Text))
                    {
                        if (StatusId == "CYCLE ON")
                        {
                            row.Cells[1].BackColor = Color.LightGreen;

                        }
                        else if (StatusId == "CYCLE OFF")
                        {
                            row.Cells[1].BackColor = Color.LightSkyBlue;
                            currenttime.Text = "00:00:00";

                        }
                        else if (StatusId != " ")
                        {
                            row.Cells[1].BackColor = Color.Red;
                            status.ForeColor = System.Drawing.Color.White;
                            currenttime.Text = "00:00:00";
                        }

                    }
                    else
                    {
                        row.Cells[1].BackColor = Color.White;
                        currenttime.Text = "00:00:00";

                    }
                }


            }
        }
        //private string ReplaceHTML(string strInput)
        //{
        //    return strInput.Replace("&nbsp;", string.Empty);
        //}
    }
}