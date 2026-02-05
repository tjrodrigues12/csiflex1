using CSIFLEX.Database.Access;
using CSIFLEX.Server.Library;
using CSIFLEX.Utilities;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Brushes = System.Windows.Media.Brushes;
using Charting = System.Windows.Forms.DataVisualization.Charting;

namespace CSIFLEX.MonitoringUnits.UI
{
    public partial class UnitsDataForm : Form
    {
        int boardId = 0;
        int machineId = 0;
        string machineName = "";
        string unitIpAddress;
        List<MonitoringBoard> boards;
        List<DateTimePoint> palletPoints;

        Point? prevPosition = null;
        ToolTip tooltip = new ToolTip();

        ListSortDirection setSortOrder = ListSortDirection.Ascending;
        int idSortColumn = -1;

        public UnitsDataForm()
        {
            InitializeComponent();
        }

        private void UnitsDataForm_Load(object sender, EventArgs e)
        {
            lblBoardLabel.ResetText();
            lblMachineName.ResetText();

            LoadBoardsGrid();

            cmbDataType.SelectedIndex = 0;
        }

        private void LoadBoardsGrid(bool showDeleted = false)
        {
            if (idSortColumn < 0)
                idSortColumn = 0;

            switch (dgvUnits.SortOrder)
            {
                case SortOrder.Ascending:
                    setSortOrder = ListSortDirection.Ascending;
                    break;
                case SortOrder.Descending:
                    setSortOrder = ListSortDirection.Descending;
                    break;
                case SortOrder.None:
                    setSortOrder = ListSortDirection.Ascending;
                    break;
            }

            BindingSource source = new BindingSource();

            dgvUnits.DataSource = null;
            dgvUnits.AutoGenerateColumns = false;

            boards = MonitoringBoardsService.GetBoards();

            if (showDeleted)
                source.DataSource = boards;
            else
                source.DataSource = boards.Where(m => !m.Deleted).ToList();

            dgvUnits.DataSource = source;

            //dgvUnits.Sort(dgvUnits.Columns[idSortColumn], setSortOrder);

            dgvUnits.Refresh();
        }

        private void dgvUnits_SelectionChanged(object sender, EventArgs e)
        {
            clbSensors.DataSource = null;

            if (dgvUnits.SelectedRows.Count == 0 || dgvUnits.SelectedRows[0].Index < 0) return;

            DataGridViewRow selectedRow = dgvUnits.SelectedRows[0];

            boardId = int.Parse(selectedRow.Cells["Id"].Value.ToString());
            machineName = selectedRow.Cells["BoardMachineName"].Value.ToString();
            unitIpAddress = selectedRow.Cells["BoardIpAddress"].Value.ToString();

            MonitoringBoard board = boards.FirstOrDefault(b => b.Id == boardId);

            List<ComboboxKeyValue> items = new List<ComboboxKeyValue>();

            foreach (MBSensor sensor in board.Sensors)
            {
                items.Add(new ComboboxKeyValue()
                {
                    Id = sensor.SensorLabel,
                    Display = $"Pallet {sensor.SensorGroup} ( {sensor.SensorLabel} )"
                });
            }

            clbSensors.DataSource = items;
            clbSensors.ValueMember = "Id";
            clbSensors.DisplayMember = "Display";
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            if (dgvUnits.SelectedRows.Count == 0 || dgvUnits.SelectedRows[0].Index < 0 || boardId == 0) return;

            int point = 0;

            try
            {
                Log.Info($"Loading data");

                this.Cursor = Cursors.WaitCursor;

                MonitoringBoard board = boards.FirstOrDefault(b => b.Id == boardId);

                Log.Debug($"Loading data from board {boardId}, {board.Label}");

                DataTable dtSensors = MySqlAccess.GetDataTable("SELECT * FROM monitoring.sensors WHERE NOT Deleted;");
                List<Sensor> sensors = new List<Sensor>();

                lblBoardLabel.Text = board.Label;
                lblMachineName.Text = board.MachineName;

                lblLinkSettings.Text = $"Open Unit Settings: {board.Label}";
                point = 1;
                foreach (DataRow row in dtSensors.Rows)
                {
                    sensors.Add(new Sensor()
                    {
                        Id = int.Parse(row["Id"].ToString()),
                        BoardId = int.Parse(row["BoardId"].ToString()),
                        Name = row["Name"].ToString(),
                        Group = row["Group"].ToString()
                    });
                }

                var sensorIndices = clbSensors.SelectedIndices;
                StringBuilder condition = new StringBuilder();
                point = 2;
                foreach (var itemChecked in clbSensors.CheckedItems)
                {
                    ComboboxKeyValue castedItem = itemChecked as ComboboxKeyValue;

                    condition.Append($" OR SensorName = '{ castedItem.Id }'");
                }
                string sensorCond = condition.ToString().StartsWith(" OR") ? $" AND ({ condition.ToString().Substring(4) })" : "";

                string typeCond = "";
                if (cmbDataType.SelectedIndex == 0)
                    typeCond = " AND Metric = 'Pressure'";
                else if (cmbDataType.SelectedIndex == 1)
                    typeCond = " AND Metric = 'Temperature'";

                string dateStart = $" AND CurrentTime > '{dtpStart.Value.Date.ToString("yyyy-MM-dd HH:mm:ss")}'";
                string dateEnd = $" AND CurrentTime < '{dtpEnd.Value.Date.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss")}'";

                string sqlCmd = $"SELECT * FROM monitoring.sensorcurrentreadings WHERE MachineId = {board.Target} {sensorCond} {typeCond} {dateStart} {dateEnd} ORDER BY CurrentTime";

                Log.Debug(sqlCmd);

                point = 3;

                DataTable query = MySqlAccess.GetDataTable(sqlCmd);

                Log.Debug($"Query: [{ sqlCmd }], returned: {query.Rows.Count}");

                BindingSource source = new BindingSource();

                List<SensorReading> sensorReadings = new List<SensorReading>();

                point = 31;

                int palletId = -1;
                int curPalletId = 0;
                int csdStatus = -1;
                int curCSDStatus = 0;
                int naStatus = -1;
                int curNAStatus = 0;
                int nas00Status = -1;
                int curNAS00Status = 0;
                double nas01Status = -1;
                double curNAS01Status = 0;

                List<DateModel> palletPoints = new List<DateModel>();
                List<DateModel> csdPoints = new List<DateModel>();
                List<DateModel> naPoints = new List<DateModel>();
                DateModel lastPalletPoint = new DateModel();
                DateModel lastCSDPoint = new DateModel();
                DateModel lastNAPoint = new DateModel();

                List<DateModel> lstPres00 = new List<DateModel>();
                List<DateModel> lstPres01 = new List<DateModel>();
                List<DateModel> lstNAS00 = new List<DateModel>();
                List<DateModel> lstNAS01 = new List<DateModel>();
                List<DateModel> lstTemp00 = new List<DateModel>();
                List<DateModel> lstTemp01 = new List<DateModel>();

                List<DateModel> lstMCS = new List<DateModel>();

                DateModel lastPres00 = new DateModel();
                DateModel lastPres01 = new DateModel();
                DateModel lastNAS00 = new DateModel();
                DateModel lastNAS01 = new DateModel();
                DateModel lastTemp00 = new DateModel();
                DateModel lastTemp01 = new DateModel();

                string sensorGroup;
                string metric;
                DateTime date;
                double value;
                point = 4;

                foreach (DataRow row in query.Rows)
                {
                    if (sensors.Any(s => s.Name == row["SensorName"].ToString()))
                    {
                        sensorGroup = sensors.FirstOrDefault(s => s.Name == row["SensorName"].ToString()).Group ?? "0";
                        metric = row["Metric"].ToString();
                        date = DateTime.Parse(row["CurrentTime"].ToString());
                        value = double.Parse(row["Value"].ToString());
                        point = 41;
                        sensorReadings.Add(new SensorReading()
                        {
                            Id = int.Parse(row["Id"].ToString()),
                            MachineId = int.Parse(row["MachineId"].ToString()),
                            Machine = machineName,
                            BoardName = row["BoardName"].ToString(),
                            Timestamp = DateTime.Parse(row["Timestamp"].ToString()),
                            CurrentTime = date,
                            IsMonitoring = int.Parse(row["IsMonitoring"].ToString()) == 1,
                            IsSensorAvailable = int.Parse(row["IsSensorAvailable"].ToString()) == 1,
                            IsOverride = int.Parse(row["IsOverride"].ToString()) == 1,
                            IsAlarming = int.Parse(row["IsAlarming"].ToString()) == 1,
                            IsCSD = int.Parse(row["IsCSD"].ToString()) == 1,
                            CurrentPallet = row["CurrentPallet"].ToString(),
                            SensorName = row["SensorName"].ToString(),
                            SensorGroup = sensorGroup,
                            Metric = metric,
                            Value = decimal.Parse(value.ToString())
                        });
                        point = 5;

                        bool addPoint = false;

                        curCSDStatus = int.Parse(row["IsCSD"].ToString()) == 1 ? 5 : 0;

                        if (curCSDStatus != csdStatus)
                        {
                            csdPoints.Add(new DateModel()
                            {
                                DateTime = date,
                                Value = int.Parse(row["IsCSD"].ToString()) == 1 ? 5 : 0
                            });
                            csdStatus = curCSDStatus;
                            addPoint = true;
                        }
                        lastCSDPoint.DateTime = date;
                        lastCSDPoint.Value = curCSDStatus;

                        curNAStatus = int.Parse(row["IsMonitoring"].ToString()) == 1 ? 0 : 3;

                        if (curNAStatus != naStatus)
                        {
                            naPoints.Add(new DateModel()
                            {
                                DateTime = date,
                                Value = int.Parse(row["IsMonitoring"].ToString()) == 1 ? 0 : 3
                            });
                            naStatus = curNAStatus;
                            addPoint = true;
                        }
                        lastNAPoint.DateTime = date;
                        lastNAPoint.Value = curNAStatus;


                        if (int.Parse(row["IsMCS"].ToString()) == 1)
                        {
                            lstMCS.Add(new DateModel()
                            {
                                DateTime = date,
                                Value = 5
                            });
                            addPoint = true;
                        }


                        int.TryParse(row["CurrentPallet"].ToString(), out curPalletId);

                        if (curPalletId != palletId)
                        {
                            palletPoints.Add(new DateModel()
                            {
                                DateTime = date,
                                Value = curPalletId
                            });
                            palletId = curPalletId;
                            addPoint = true;
                        }
                        lastPalletPoint.DateTime = date;
                        lastPalletPoint.Value = curPalletId;



                        point = 6;
                        if (sensorGroup == "0")
                        {
                            curNAS00Status = int.Parse(row["IsSensorAvailable"].ToString()) == 1 ? 0 : 2;
                            if (curNAS00Status != nas00Status)
                            {
                                lstNAS00.Add(new DateModel()
                                {
                                    DateTime = date,
                                    Value = curNAS00Status
                                });
                                nas00Status = curNAS00Status;
                                addPoint = true;
                            }
                            lastNAS00.DateTime = date;
                            lastNAS00.Value = curNAS00Status;

                            if (metric == "Pressure")
                            {
                                if (lstPres00.LastOrDefault() == null || value != lstPres00.LastOrDefault().Value || addPoint)
                                {
                                    lstPres00.Add(new DateModel()
                                    {
                                        DateTime = date,
                                        Value = value
                                    });
                                }
                                lastPres00.DateTime = date;
                                lastPres00.Value = value;
                            }
                            else if (metric == "Temperature")
                            {
                                if (lstTemp00.LastOrDefault() == null || value != lstTemp00.LastOrDefault().Value || addPoint)
                                {
                                    lstTemp00.Add(new DateModel()
                                    {
                                        DateTime = date,
                                        Value = value
                                    });
                                }
                                lastTemp00.DateTime = date;
                                lastTemp00.Value = value;
                            }
                        }
                        else if (sensorGroup == "1")
                        {
                            curNAS01Status = double.Parse(row["IsSensorAvailable"].ToString()) == 1 ? 0 : 2.1;
                            if (curNAS01Status != nas01Status)
                            {
                                lstNAS01.Add(new DateModel()
                                {
                                    DateTime = date,
                                    Value = curNAS01Status
                                });
                                nas01Status = curNAS01Status;
                                addPoint = true;
                            }
                            lastNAS01.DateTime = date;
                            lastNAS01.Value = curNAS01Status;

                            if (metric == "Pressure")
                            {
                                if (lstPres01.LastOrDefault() == null || value != lstPres01.LastOrDefault().Value || addPoint)
                                {
                                    lstPres01.Add(new DateModel()
                                    {
                                        DateTime = date,
                                        Value = value
                                    });
                                }
                                lastPres01.DateTime = date;
                                lastPres01.Value = value;
                            }
                            else if (metric == "Temperature")
                            {
                                if (lstTemp01.LastOrDefault() == null || value != lstTemp01.LastOrDefault().Value || addPoint)
                                {
                                    lstTemp01.Add(new DateModel()
                                    {
                                        DateTime = date,
                                        Value = value
                                    });
                                }
                                lastTemp01.DateTime = date;
                                lastTemp01.Value = value;
                            }
                        }
                    }
                }
                point = 42;
                palletPoints.Add(lastPalletPoint);
                csdPoints.Add(lastCSDPoint);
                naPoints.Add(lastNAPoint);
                lstPres00.Add(lastPres00);
                lstTemp00.Add(lastTemp00);
                lstPres01.Add(lastPres01);
                lstTemp01.Add(lastTemp01);
                lstNAS00.Add(lastNAS00);
                lstNAS01.Add(lastNAS01);
                point = 7;
                source.DataSource = sensorReadings;

                dgvData.AutoGenerateColumns = false;
                dgvData.DataSource = null;
                dgvData.DataSource = source;
                dgvData.Columns["Timestamp"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                dgvData.Columns["Timestamp"].MinimumWidth = 170;
                dgvData.Columns["CurrentTime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                dgvData.Columns["CurrentTime"].MinimumWidth = 170;
                dgvData.Columns["Value"].DefaultCellStyle.Format = "#,##0.000";
                dgvData.Columns["Value"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                this.Cursor = Cursors.Cross;

                DrawPalletChart();

                if (lstPres00.Count > 1 || lstPres01.Count > 1)
                {
                    chartSensors.ChartAreas[0].AxisY2.Title = "Pressure";
                    //chartSensors.ChartAreas[0].AxisY2.Maximum = 5000;
                    chartSensors.ChartAreas[0].AxisY2.Interval = 1000;
                }
                else if (lstTemp00.Count > 0 || lstTemp01.Count > 0)
                {
                    chartSensors.ChartAreas[0].AxisY2.Title = "Temperature";
                    chartSensors.ChartAreas[0].AxisY2.Maximum = 50;
                    chartSensors.ChartAreas[0].AxisY2.Interval = 10;
                }
                point = 8;
                chartSensors.Series.Clear();
                Charting.Series seriePallet = chartSensors.Series.Add("Pallet");
                seriePallet.ChartType = Charting.SeriesChartType.StepLine;
                seriePallet.IsVisibleInLegend = true;
                seriePallet.BorderWidth = 3;
                seriePallet.Color = Color.Blue;
                seriePallet.YAxisType = Charting.AxisType.Primary;
                seriePallet.LegendText = "Current Pallet";
                seriePallet.MarkerStyle = MarkerStyle.Circle;
                seriePallet.MarkerSize = 10;
                chartSensors.Series["Pallet"].Points.DataBind(palletPoints, "Datetime", "Value", null);

                Charting.Series serieCSD = chartSensors.Series.Add("CSD");
                serieCSD.ChartType = Charting.SeriesChartType.StepLine;
                serieCSD.IsVisibleInLegend = true;
                serieCSD.BorderWidth = 4;
                serieCSD.Color = Color.Red;
                serieCSD.YAxisType = Charting.AxisType.Primary;
                serieCSD.LegendText = "Cycle Start Disabled";
                serieCSD.MarkerStyle = MarkerStyle.Circle;
                serieCSD.MarkerSize = 10;
                chartSensors.Series["CSD"].Points.DataBind(csdPoints, "Datetime", "Value", null);

                Charting.Series serieNA = chartSensors.Series.Add("NA");
                serieNA.ChartType = Charting.SeriesChartType.StepLine;
                serieNA.IsVisibleInLegend = true;
                serieNA.BorderWidth = 3;
                serieNA.Color = Color.Purple;
                serieNA.YAxisType = Charting.AxisType.Primary;
                serieNA.LegendText = "No eMonitoring Board";
                serieNA.MarkerStyle = MarkerStyle.Circle;
                serieNA.MarkerSize = 10;
                chartSensors.Series["NA"].Points.DataBind(naPoints, "Datetime", "Value", null);

                Charting.Series serieMCS = chartSensors.Series.Add("MCS");
                serieMCS.ChartType = Charting.SeriesChartType.Point;
                serieMCS.IsVisibleInLegend = true;
                serieMCS.BorderWidth = 6;
                serieMCS.Color = Color.Black;
                serieMCS.YAxisType = Charting.AxisType.Primary;
                serieMCS.LegendText = "Machine Critical Stop";
                serieMCS.MarkerStyle = MarkerStyle.Diamond;
                serieMCS.MarkerSize = 12;
                chartSensors.Series["MCS"].Points.DataBind(lstMCS, "Datetime", "Value", null);
                point = 9;
                if (lstPres00.Count > 1)
                {
                    Charting.Series seriePres00 = chartSensors.Series.Add("Pallet0Pressure");
                    seriePres00.ChartType = Charting.SeriesChartType.StepLine;
                    seriePres00.IsVisibleInLegend = true;
                    seriePres00.BorderWidth = 2;
                    seriePres00.Color = Color.DarkGreen;
                    seriePres00.YAxisType = Charting.AxisType.Secondary;
                    seriePres00.LegendText = "PALLET 0 - Pressure";
                    seriePres00.MarkerStyle = MarkerStyle.Circle;
                    seriePres00.MarkerSize = 8;
                    chartSensors.Series["Pallet0Pressure"].Points.DataBind(lstPres00, "Datetime", "Value", null);

                    Charting.Series serieNAPres00 = chartSensors.Series.Add("Pallet0Offline");
                    serieNAPres00.ChartType = Charting.SeriesChartType.StepLine;
                    serieNAPres00.IsVisibleInLegend = true;
                    serieNAPres00.BorderWidth = 4;
                    serieNAPres00.Color = Color.DarkGreen;
                    serieNAPres00.YAxisType = Charting.AxisType.Primary;
                    serieNAPres00.LegendText = "PALLET 0 - Off line";
                    serieNAPres00.MarkerStyle = MarkerStyle.Circle;
                    serieNAPres00.MarkerSize = 8;
                    chartSensors.Series["Pallet0Offline"].Points.DataBind(lstNAS00, "Datetime", "Value", null);
                    Log.Info($"{ machineName } - Pallet0 Offline: {lstNAS00.Count}");
                }

                if (lstPres01.Count > 1)
                {
                    Charting.Series seriePres01 = chartSensors.Series.Add("Pallet1Pressure");
                    seriePres01.ChartType = Charting.SeriesChartType.StepLine;
                    seriePres01.IsVisibleInLegend = true;
                    seriePres01.BorderWidth = 2;
                    seriePres01.Color = Color.DarkOrange;
                    seriePres01.YAxisType = Charting.AxisType.Secondary;
                    seriePres01.LegendText = "PALLET 1 - Pressure";
                    seriePres01.MarkerStyle = MarkerStyle.Circle;
                    seriePres01.MarkerSize = 8;
                    chartSensors.Series["Pallet1Pressure"].Points.DataBind(lstPres01, "Datetime", "Value", null);

                    Charting.Series serieNAPres01 = chartSensors.Series.Add("Pallet1Offline");
                    serieNAPres01.ChartType = Charting.SeriesChartType.StepLine;
                    serieNAPres01.IsVisibleInLegend = true;
                    serieNAPres01.BorderWidth = 4;
                    serieNAPres01.Color = Color.DarkOrange;
                    serieNAPres01.YAxisType = Charting.AxisType.Primary;
                    serieNAPres01.LegendText = "PALLET 1 - Off line";
                    serieNAPres01.MarkerStyle = MarkerStyle.Circle;
                    serieNAPres01.MarkerSize = 8;
                    chartSensors.Series["Pallet1Offline"].Points.DataBind(lstNAS01, "Datetime", "Value", null);
                    Log.Info($"{ machineName } - Pallet1 Offline: {lstNAS01.Count}");
                }

                if (lstTemp00.Count > 1)
                {
                    Charting.Series serieTemp00 = chartSensors.Series.Add("Pallet0Temperature");
                    serieTemp00.ChartType = Charting.SeriesChartType.StepLine;
                    serieTemp00.IsVisibleInLegend = true;
                    serieTemp00.BorderWidth = 2;
                    serieTemp00.Color = Color.YellowGreen;
                    serieTemp00.YAxisType = Charting.AxisType.Secondary;
                    serieTemp00.LegendText = "PALLET 0 - Temperature";
                    serieTemp00.MarkerStyle = MarkerStyle.Circle;
                    serieTemp00.MarkerSize = 8;

                    chartSensors.Series["Pallet0Temperature"].Points.DataBind(lstTemp00, "Datetime", "Value", null);
                }

                if (lstTemp01.Count > 1)
                {
                    Charting.Series serieTemp01 = chartSensors.Series.Add("Pallet1Temperature");
                    serieTemp01.ChartType = Charting.SeriesChartType.StepLine;
                    serieTemp01.IsVisibleInLegend = true;
                    serieTemp01.BorderWidth = 2;
                    serieTemp01.Color = Color.Gold;
                    serieTemp01.YAxisType = Charting.AxisType.Secondary;
                    serieTemp01.LegendText = "PALLET 1 - Temperature";
                    serieTemp01.MarkerStyle = MarkerStyle.Circle;
                    serieTemp01.MarkerSize = 8;

                    chartSensors.Series["Pallet1Temperature"].Points.DataBind(lstTemp01, "Datetime", "Value", null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + $" - {point}");
                Log.Error(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void btnExportToCsv_Click(object sender, EventArgs e)
        {
            if (dgvData.DataSource == null || dgvData.Rows.Count == 0)
                return;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "csv";
            saveFileDialog.Filter = "CSV Files|*.csv";
            saveFileDialog.FileName = $"MB_{machineName}_{dtpStart.Value.ToString("MMM-dd")}_{dtpEnd.Value.ToString("MMM-dd")}.csv";

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            string filePath = saveFileDialog.FileName;

            var sb = new StringBuilder();

            var headers = dgvData.Columns.Cast<DataGridViewColumn>();
            sb.AppendLine(string.Join(",", headers.Select(column => "\"" + column.HeaderText + "\"").ToArray()));

            foreach (DataGridViewRow row in dgvData.Rows)
            {
                var cells = row.Cells.Cast<DataGridViewCell>();
                sb.AppendLine(string.Join(",", cells.Select(cell => "\"" + cell.Value + "\"").ToArray()));
            }

            string exportText = sb.ToString();

            System.IO.File.WriteAllText(filePath, exportText);
        }

        private void DrawPalletChart()
        {
            chartSensors.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartSensors.ChartAreas[0].AxisX.IntervalType = Charting.DateTimeIntervalType.Seconds;
            chartSensors.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
            chartSensors.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font(chartSensors.ChartAreas[0].AxisX.LabelStyle.Font.FontFamily, 9);
            chartSensors.ChartAreas[0].AxisX.ScaleView.SmallScrollMinSizeType = Charting.DateTimeIntervalType.Seconds;

            chartSensors.ChartAreas[0].CursorX.IntervalType = Charting.DateTimeIntervalType.Seconds;

            chartSensors.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            chartSensors.ChartAreas[0].AxisY.Title = "Pallet";
            chartSensors.ChartAreas[0].AxisY.Enabled = Charting.AxisEnabled.True;
            chartSensors.ChartAreas[0].AxisY.Maximum = 5;
            chartSensors.ChartAreas[0].AxisY.Interval = 1;

            chartSensors.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chartSensors.ChartAreas[0].CursorX.AutoScroll = true;
            chartSensors.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;

            chartSensors.Legends.Clear();
            chartSensors.Legends.Add("Legends");
            chartSensors.Legends[0].Docking = Charting.Docking.Bottom;
        }

        private void chartSensors_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                chartSensors.ChartAreas[0].AxisX.ScaleView.ZoomReset();
            }
        }

        private void chartSensors_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string serieName;
            DateTime dateTime;
            string pallet;

            var pos = e.Location;

            var results = chartSensors.HitTest(pos.X, pos.Y, false, ChartElementType.DataPoint);

            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    serieName = result.Series.Name;

                    var prop = result.Object as DataPoint;
                    if (prop != null)
                    {
                        dateTime = DateTime.FromOADate(prop.XValue);
                        pallet = serieName.Contains("0") ? "0" : "1";

                        int rowIndex = -1;
                        DataGridViewRow row;

                        if (result.Series.YAxisType == AxisType.Secondary)
                        {
                            row = dgvData.Rows
                                .Cast<DataGridViewRow>()
                                .Where(r => r.Cells["Pallet"].Value.ToString().Equals(pallet) && r.Cells["CurrentTime"].Value.ToString().Equals(dateTime.ToString()))
                                .FirstOrDefault();
                        }
                        else
                        {
                            row = dgvData.Rows
                                .Cast<DataGridViewRow>()
                                .Where(r => r.Cells["CurrentTime"].Value.ToString().Equals(dateTime.ToString()))
                                .FirstOrDefault();
                        }

                        if (row != null)
                        {
                            rowIndex = row.Index;
                            dgvData.ClearSelection();
                            dgvData.Rows[rowIndex].Selected = true;
                            dgvData.FirstDisplayedScrollingRowIndex = rowIndex;
                        }
                    }
                }
            }

        }

        private void chartSensors_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;

            tooltip.RemoveAll();

            prevPosition = pos;
            var results = chartSensors.HitTest(pos.X, pos.Y, false, ChartElementType.DataPoint);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    var prop = result.Object as DataPoint;
                    if (prop != null)
                    {
                        if (result.Series.YAxisType == AxisType.Secondary)
                        {
                            var pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
                            var pointYPixel = result.ChartArea.AxisY2.ValueToPixelPosition(prop.YValues[0]);

                            // check if the cursor is really close to the point (2 pixels around the point)
                            if (Math.Abs(pos.X - pointXPixel) < 2 &&
                                Math.Abs(pos.Y - pointYPixel) < 2)
                            {
                                if (result.Series.Name.Contains("Pressure"))
                                {
                                    tooltip.Show($"{result.Series.LegendText}\nPressure: { prop.YValues[0].ToString("#,##0") }\nTime: { DateTime.FromOADate(prop.XValue).ToString("HH:mm:ss") }", this.chartSensors, pos.X + 10, pos.Y - 15);
                                }
                                else
                                {
                                    tooltip.Show($"{result.Series.LegendText}\nTemperature: { prop.YValues[0].ToString("0.000") }\nTime: { DateTime.FromOADate(prop.XValue).ToString("HH:mm:ss") }", this.chartSensors, pos.X + 10, pos.Y - 15);
                                }
                            }
                        }
                        else
                        {
                            var pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
                            var pointYPixel = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);

                            // check if the cursor is really close to the point (2 pixels around the point)
                            if (Math.Abs(pos.X - pointXPixel) < 2 &&
                                Math.Abs(pos.Y - pointYPixel) < 2)
                            {
                                if (result.Series.Name == "Pallet")
                                {
                                    tooltip.Show($"Pallet { prop.YValues[0] }\nTime: { DateTime.FromOADate(prop.XValue).ToString("HH:mm:ss") }", this.chartSensors, pos.X + 10, pos.Y - 15);
                                }
                                else
                                {
                                    tooltip.Show($"{ result.Series.LegendText }\nTime: { DateTime.FromOADate(prop.XValue).ToString("HH:mm:ss") }", this.chartSensors, pos.X + 10, pos.Y - 15);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            dtpEnd.Value = dtpStart.Value;
        }

        private void lblLinkSettings_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitIpAddress))
                return;

            string webAddress = $"http://{unitIpAddress}/";
            Process.Start(webAddress);
        }

        private void chkShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            LoadBoardsGrid(chkShowDeleted.Checked);
        }

        private void dgvUnits_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {

            if (dgvUnits.IsCurrentCellDirty)
                dgvUnits.CommitEdit(DataGridViewDataErrorContexts.Commit);

        }

        private void dgvUnits_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

            int rowIdx = e.RowIndex;

            if (rowIdx < 0) return;

            DataGridViewRow row = dgvUnits.Rows[rowIdx];

            bool isDeleted = Boolean.Parse(row.Cells["BoardDeleted"].Value.ToString());

            if (isDeleted)
            {
                row.DefaultCellStyle.ForeColor = Color.Gray;
                row.DefaultCellStyle.SelectionBackColor = Color.LightCoral;
                row.DefaultCellStyle.SelectionForeColor = Color.Black;
            }
        }

        private void btnRefresh_Paint(object sender, PaintEventArgs e)
        {
            LoadBoardsGrid(chkShowDeleted.Checked);
        }
    }

    public class ComboboxKeyValue
    {
        public string Id { get; set; }
        public string Display { get; set; }
    }

    public class SensorReading
    {
        public int Id { get; set; }
        public int MachineId { get; set; }
        public string Machine { get; set; }
        public string BoardName { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime CurrentTime { get; set; }
        public bool IsMonitoring { get; set; }
        public bool IsSensorAvailable { get; set; }
        public bool IsOverride { get; set; }
        public bool IsAlarming { get; set; }
        public bool IsCSD { get; set; }
        public string CurrentPallet { get; set; }
        public string SensorName { get; set; }
        public string SensorGroup { get; set; }
        public string Metric { get; set; }
        public decimal Value { get; set; }
    }

    public class DateModel
    {
        public System.DateTime DateTime { get; set; }
        public double Value { get; set; }
    }

    public class Sensor
    {
        public int Id { get; set; }
        public int BoardId { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
    }
}
