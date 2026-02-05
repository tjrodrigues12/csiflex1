using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace RSSFeedManager
{
    public partial class RSSFeedManager : Form
    {
        MySqlConnection myConnection;
        MySqlCommand sqlCommand = new MySqlCommand();
        MySqlDataReader drDashboards;
        DataTable dtDashboards;
        MySqlDataReader drMessages;
        DataTable dtMessages;

        List<Dashboard> Dashboards = new List<Dashboard>();
        List<Message> Messages = new List<Message>();

        int selectedDashboardIndex = -1;
        int rowMessageSelected = -1;
        int deviceId = 0;

        public RSSFeedManager()
        {
            InitializeComponent();
        }

        private void RSSFeedManager_Load(object sender, EventArgs e)
        {
            LoginFrm loginFrm = new LoginFrm();

            if (loginFrm.ShowDialog() != DialogResult.OK)
            {
                this.Close();
            }

            myConnection = new MySqlConnection(Global.ConnectionString);

            myConnection.Open();
            sqlCommand.Connection = myConnection;

            sqlCommand.CommandText = "SELECT * FROM csi_database.tbl_devices";
            drDashboards = sqlCommand.ExecuteReader();
            dtDashboards = new DataTable("Dashboards");
            dtDashboards.Load(drDashboards);

            sqlCommand.CommandText = "SELECT * FROM csi_database.tbl_messages order by Priority";
            drMessages = sqlCommand.ExecuteReader();
            dtMessages = new DataTable("Dashboards");
            dtMessages.Load(drMessages);

            foreach (DataRow row in dtMessages.Rows)
            {
                Messages.Add(new Message(
                    int.Parse(row["DeviceId"].ToString()),
                    row["message"].ToString()
                    ));
            }


            foreach (DataRow row in dtDashboards.Rows)
            {
                Dashboards.Add(new Dashboard(int.Parse(row["deviceId"].ToString())));

                lstDashboards.Items.Add(new ListViewItem(row["deviceName"].ToString()).Text);

                Dashboards.LastOrDefault().messages = Messages.Where(msg => msg.deviceId == int.Parse(row["DeviceId"].ToString())).ToList<Message>();
            }
        }

        private void rdbMessageType_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

            if (radioButton.Text == "User Message")
            {
                txtNewMessage.Enabled = true;
                txtNewMessage.Text = "";
                grpPeriod.Enabled = false;
            }
            else
            {
                txtNewMessage.Enabled = false;
                txtNewMessage.Text = "";
                grpPeriod.Enabled = true;
            }
        }

        private void rdbPeriod_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

            switch (radioButton.Text)
            {
                case "Day":
                    txtNewMessage.Text = "_CONDDaily Best CYCLE ON Machine";
                    break;
                case "Week":
                    txtNewMessage.Text = "_CONWWeekly Best CYCLE ON Machine";
                    break;
                case "Month":
                    txtNewMessage.Text = "_CONMMonthly Best CYCLE ON Machine";
                    break;
            }
        }

        private void lstDashboards_Click(object sender, EventArgs e)
        {
            if (selectedDashboardIndex != lstDashboards.SelectedIndex)
                rowMessageSelected = -1;

            selectedDashboardIndex = lstDashboards.SelectedIndex;

            dataGridMessages.Rows.Clear();

            var msg = "";
            foreach (Message message in Dashboards[selectedDashboardIndex].messages)
            {
                msg = message.message.StartsWith("_") ? message.message.Substring(5) : message.message;
                dataGridMessages.Rows.Add(msg);
            }

            dataGridMessages.ClearSelection();

            if (rowMessageSelected > -1)
                dataGridMessages.CurrentCell = dataGridMessages.Rows[rowMessageSelected].Cells[0];

            clearInsertForm();
        }

        private void btnUpMessage_Click(object sender, EventArgs e)
        {
            int indexNow = dataGridMessages.CurrentCell.RowIndex;

            if (indexNow == 0) return;

            var message = Dashboards[selectedDashboardIndex].messages[indexNow];
            Dashboards[selectedDashboardIndex].messages.RemoveAt(indexNow);
            Dashboards[selectedDashboardIndex].messages.Insert(indexNow - 1, message);

            rowMessageSelected = indexNow - 1;

            lstDashboards_Click(this, new EventArgs());
            btnSaveChanges.ForeColor = Color.Red;
        }

        private void btnDownMessage_Click(object sender, EventArgs e)
        {
            int indexNow = dataGridMessages.CurrentCell.RowIndex;

            if (indexNow == dataGridMessages.Rows.Count) return;

            var message = Dashboards[selectedDashboardIndex].messages[indexNow];
            Dashboards[selectedDashboardIndex].messages.RemoveAt(indexNow);
            Dashboards[selectedDashboardIndex].messages.Insert(indexNow + 1, message);

            rowMessageSelected = indexNow + 1;

            lstDashboards_Click(this, new EventArgs());
            btnSaveChanges.ForeColor = Color.Red;
        }

        private void btnDeleteMessge_Click(object sender, EventArgs e)
        {
            int indexNow = dataGridMessages.CurrentCell.RowIndex;

            Dashboards[selectedDashboardIndex].messages.RemoveAt(indexNow);

            lstDashboards_Click(this, new EventArgs());
            btnSaveChanges.ForeColor = Color.Red;
        }

        private void btnAddMessage_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNewMessage.Text)) return;

            Message msg;

            for (int index = 0; index < Dashboards.Count; index++)
            {
                if (chkAllDashboards.Checked || index == selectedDashboardIndex)
                {
                    msg = new Message(Dashboards[index].deviceId, txtNewMessage.Text);

                    if (chkAddOnTop.Checked)
                    {
                        Dashboards[index].messages.Insert(0, msg);
                    }
                    else
                    {
                        Dashboards[index].messages.Add(msg);
                    }
                }
            }

            lstDashboards_Click(this, new EventArgs());
            btnSaveChanges.ForeColor = Color.Red;
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            MySqlTransaction myTransaction;

            foreach (Dashboard dash in Dashboards)
            {
                // Check if the messages are "on" or "off" for this device. If is "on", turns it off.
                sqlCommand.CommandText = $"SELECT message FROM csi_database.tbl_deviceconfig WHERE DeviceId = {dash.deviceId} LIMIT 1";
                string status = sqlCommand.ExecuteScalar().ToString();

                if (String.IsNullOrEmpty(status)) status = "off";
                if (status == "on")
                {
                    sqlCommand.CommandText = $"UPDATE csi_database.tbl_deviceconfig SET messages = 'off' WHERE DeviceId = {dash.deviceId} LIMIT 1";
                    sqlCommand.ExecuteNonQuery();
                }

                myTransaction = myConnection.BeginTransaction();
                sqlCommand.Transaction = myTransaction;

                try
                {
                    sqlCommand.CommandText = $"DELETE FROM csi_database.tbl_messages WHERE DeviceId = {dash.deviceId}";
                    sqlCommand.ExecuteNonQuery();

                    sqlCommand.CommandText = "INSERT INTO csi_database.tbl_messages (deviceId, name, IP_adress, messages, Priority) VALUES (?deviceId, ?name, ?IP_adress, ?messages, ?Priority);";

                    int index = 1;
                    foreach (Message msg in dash.messages)
                    {
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Parameters.Add("?deviceId", MySqlDbType.Int16).Value = msg.deviceId;
                        sqlCommand.Parameters.Add("?name", MySqlDbType.VarChar).Value = msg.name;
                        sqlCommand.Parameters.Add("?IP_adress", MySqlDbType.VarChar).Value = msg.ip_address;
                        sqlCommand.Parameters.Add("?messages", MySqlDbType.VarChar).Value = msg.message;
                        sqlCommand.Parameters.Add("?Priority", MySqlDbType.Int16).Value = index++;

                        sqlCommand.ExecuteNonQuery();
                    }
                    myTransaction.Commit();

                    if (status == "on")
                    {
                        sqlCommand.CommandText = $"UPDATE csi_database.tbl_deviceconfig SET messages = 'on' WHERE DeviceId = {dash.deviceId} LIMIT 1";
                        sqlCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    myTransaction.Rollback();
                }
            }
            send_http_req();
            btnSaveChanges.ForeColor = SystemColors.ControlText;

            MessageBox.Show("Update Done!");
        }

        private void clearInsertForm()
        {
            txtNewMessage.Text = "";
            chkAddOnTop.Checked = false;
            chkAllDashboards.Checked = false;
            rdbUserMessage.Checked = true;
        }


        public void send_http_req()
        {
            try
            {
                //DataTable portT = new DataTable();
                //MySqlDataAdapter dadapter_name = new MySqlDataAdapter("Select port From csi_database.tbl_rm_port;", myConnection);
                //dadapter_name.Fill(portT);

                sqlCommand.CommandText = "Select port From csi_database.tbl_rm_port;";
                string port = sqlCommand.ExecuteScalar().ToString();

                WebRequest request;

                if (string.IsNullOrEmpty(port))
                {
                    request = WebRequest.Create($"http://{Global.Database}:8008/readconfig");
                }
                else
                {
                    request = WebRequest.Create($"http://{Global.Database}:{port}/readconfig");
                }

                request.Method = "POST";

                string postData = "";
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postData);
                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;
                // Get the request stream.
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.

                dataStream.Close();
            }
            catch (Exception ex)
            {
                //CSI_Lib.LogServerError("Unable to send http req" + ex.Message, 1);
            }
        }



    }

    class Message
    {
        public Message() { }

        //public Message( string name, string ip_address, string message)
        //{
        //    this.name = name;
        //    this.ip_address = ip_address;
        //    this.message = message;
        //}

        public Message(int deviceId, string message)
        {
            this.deviceId = deviceId;
            this.message = message;
        }

        public int deviceId { get; set; }

        public string name { get; set; }

        public string ip_address { get; set; }

        public string message { get; set; }
    }

    class Dashboard
    {
        public Dashboard()
        {
            messages = new List<Message>();
        }

        public Dashboard(string deviceName, string ip_address)
        {
            this.deviceName = deviceName;
            this.ip_address = ip_address;
        }

        public Dashboard(int deviceId)
        {
            this.deviceId = deviceId;
        }

        public int deviceId { get; set; }

        public string deviceName { get; set; }

        public string ip_address { get; set; }

        public List<Message> messages { get; set; }
    }
}
