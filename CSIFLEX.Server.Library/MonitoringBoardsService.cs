using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSIFLEX.Database.Access;
using CSIFLEX.Utilities;
using Newtonsoft.Json;

namespace CSIFLEX.Server.Library
{
    public static class MonitoringBoardsService
    {
        static List<MonitoringBoard> Boards = GetBoards();

        static MonitoringBoardState state;

#if DEBUG
        private static string serviceBaseAddress = @"http://10.0.10.189:2021";
#else
        private static string serviceBaseAddress = @"http://localhost:2021";
#endif 

        public static List<MonitoringBoardStatus> MonitoringBoardsStatus = new List<MonitoringBoardStatus>();

        public static string GetProbeXml(int boardId)
        {
            if (boardId == 0)
                return "";

            try
            {
                Uri uri = new Uri(new Uri(serviceBaseAddress), $"MTConnect/{ boardId }/probe");
                WebClient webClient = new WebClient();

                var response = webClient.DownloadString(uri);

                return response;
            }
            catch (Exception ex)
            {
                Log.Error($"BoardId: {boardId}", ex);
                return "";
            }
        }

        public static string GetCurrentXml(int boardId)
        {
            if (boardId == 0)
                return "";

            try
            {
                Uri uri = new Uri(new Uri(serviceBaseAddress), $"MTConnect/{ boardId }/current");
                WebClient webClient = new WebClient();

                var response = webClient.DownloadString(uri);

                return response;
            }
            catch (Exception ex)
            {
                Log.Error($"BoardId: {boardId}", ex);
                return "";
            }
        }

        public static async void SetPallet(int boardId, string pallet)
        {
            Pallet newPallet = new Pallet()
            {
                group = pallet
            };

            var json = JsonConvert.SerializeObject(newPallet);

            Log.Info($"SetPallet: {boardId} - {json}");

            Uri Url = new Uri(new Uri(serviceBaseAddress), $"api/boards/ActiveGroup/{ boardId }");

            try
            {
                using (var client = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Post, Url))
                    {
                        using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                        {
                            request.Content = stringContent;

                            using (var response = await client
                                .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                                .ConfigureAwait(false))
                            {
                                response.EnsureSuccessStatusCode();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        public static async void SetTarget(int boardId, int target)
        {
            Uri Url = new Uri(new Uri(serviceBaseAddress), $"api/boards/Target/{ boardId }/{ target }");

            try
            {
                using (var client = new HttpClient())
                {
                    using(var request = new HttpRequestMessage(HttpMethod.Post, Url))
                    {
                        request.Content = new StringContent("", Encoding.UTF8, "application/json");

                        using (var response = await client
                            .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                            .ConfigureAwait(false))
                        {
                            response.EnsureSuccessStatusCode();
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        public static async void SetCycleStartDisable(int boardId, bool enable)
        {
            string param = enable ? "StartCsd" : "StopCsd";

            Uri Url = new Uri(new Uri(serviceBaseAddress), $"api/boards/{boardId}/{param}");

            try
            {
                using (var client = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Post, Url))
                    {
                        request.Content = new StringContent("", Encoding.UTF8, "application/json");

                        using (var response = await client
                            .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                            .ConfigureAwait(false))
                        {
                            response.EnsureSuccessStatusCode();
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        public static void SetAlarm(int boardId, bool enable)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(serviceBaseAddress);

                string param = enable ? "StartAlert" : "StopAlert";

                var response = client.PostAsync($"api/boards/{boardId}/{param}", null);

                if (response.Status != TaskStatus.Created)
                    Log.Error($"Error sending Start/Stop Alarm to Monitoring Units Service. Board Id: {boardId}, Sending: {param} - {response}");
            }
        }

        public static async void SetMonitoringOverride(int boardId, bool enable)
        {
            string param = enable ? "StartMonitoringOverride" : "StopMonitoringOverride";

            Uri Url = new Uri(new Uri(serviceBaseAddress), $"api/boards/{boardId}/{param}");

            try
            {
                using (var client = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Post, Url))
                    {
                        request.Content = new StringContent("", Encoding.UTF8, "application/json");

                        using (var response = await client
                            .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                            .ConfigureAwait(false))
                        {
                            response.EnsureSuccessStatusCode();
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        public static async void CreateNewBoard(MonitoringBoard newBoard)
        {
            ServiceMonitoringBoard board = new ServiceMonitoringBoard()
            {
                description = "",
                enabled = true,
                firmware = newBoard.Firmware,
                ip = newBoard.IpAddress,
                mac = newBoard.Mac,
                manufacturer = newBoard.Manufacturer,
                model = newBoard.Model,
                name = newBoard.Label,
                serial = newBoard.SerialNumber,
                target = ""
            };

            if (!ServiceEcho())
                throw new Exception("Monitoring Units Service is not available!");

            var json = JsonConvert.SerializeObject(board);

            Uri Url = new Uri(new Uri(serviceBaseAddress), $"api/boards");

            try
            {
                using (var client = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Post, Url))
                    {
                        using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                        {
                            request.Content = stringContent;

                            using (var response = await client
                                .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                                .ConfigureAwait(false))
                            {
                                response.EnsureSuccessStatusCode();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        public static void DeleteBoard(int boardId)
        {
            StringBuilder sqlCmd = new StringBuilder();

            //sqlCmd.Append($"DELETE FROM monitoring.sensors WHERE BoardId = { boardId }; ");
            sqlCmd.Append($"UPDATE monitoring.monitoringboards SET Deleted = true WHERE Id = { boardId }; ");

            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString());
        }

        public static void GetStatus(int boardId)
        {
            string mac = GetBoardMac(boardId);

            if (mac == "") return;

            try
            {
                Uri uri = new Uri(new Uri(serviceBaseAddress), $"State/{ mac }");
                WebClient webClient = new WebClient();

                var response = webClient.DownloadString(uri);

                state = JsonConvert.DeserializeObject<MonitoringBoardState>(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        public static bool IsMonitoring(int boardId)
        {
            string mac = GetBoardMac(boardId);

            if (mac == "") return false;

            try
            {
                Uri uri = new Uri(new Uri(serviceBaseAddress), $"State/{ mac }");
                WebClient webClient = new WebClient();

                var response = webClient.DownloadString(uri);

                MonitoringBoardState state = JsonConvert.DeserializeObject<MonitoringBoardState>(response);

                return state.monitoring;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            return false;
        }

        public static bool IsMonitOverrideOn(int boardId)
        {
            string mac = GetBoardMac(boardId);

            if (mac == "") return false;

            try
            {
                Uri uri = new Uri(new Uri(serviceBaseAddress), $"State/{ mac }");
                WebClient webClient = new WebClient();

                var response = webClient.DownloadString(uri);

                MonitoringBoardState state = JsonConvert.DeserializeObject<MonitoringBoardState>(response);

                return state.monitoring_override;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            return false;
        }

        public static bool IsCycleStartDisableOn(int boardId)
        {
            string mac = GetBoardMac(boardId);

            if (mac == "") return false;

            try
            {
                Uri uri = new Uri(new Uri(serviceBaseAddress), $"State/{ mac }");
                WebClient webClient = new WebClient();

                var response = webClient.DownloadString(uri);

                MonitoringBoardState state = JsonConvert.DeserializeObject<MonitoringBoardState>(response);

                return state.csd_enabled;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            return false;
        }

        public static List<MonitoringBoard> GetBoards()
        {
            try
            {
                DataTable tblBoards = MySqlAccess.GetDataTable("SELECT * FROM monitoring.monitoringboards WHERE NOT Deleted;");
                DataTable tblSensors = MySqlAccess.GetDataTable("SELECT * FROM monitoring.sensors WHERE NOT Deleted;");
                DataTable tblMachines = MySqlAccess.GetDataTable("SELECT * FROM csi_auth.tbl_ehub_conf;");

                List<MonitoringBoard> Boards = new List<MonitoringBoard>();

                foreach (DataRow rowBoard in tblBoards.Rows)
                {

                    MonitoringBoard board = new MonitoringBoard()
                    {
                        Id = int.Parse(rowBoard["Id"].ToString()),
                        SerialNumber = rowBoard["SerialNumber"].ToString(),
                        Model = rowBoard["Model"].ToString(),
                        Mac = rowBoard["Mac"].ToString(),
                        Label = rowBoard["Name"].ToString(),
                        Manufacturer = rowBoard["Manufacturer"].ToString(),
                        IpAddress = rowBoard["IpAddress"].ToString(),
                        Firmware = rowBoard["Firmware"].ToString(),
                        Target = string.IsNullOrEmpty(rowBoard["Target"].ToString()) ? "0" : rowBoard["Target"].ToString(),
                        CreatedAt = DateTime.Parse(rowBoard["CreatedAt"].ToString()).ToString("MM-dd-yyyy HH:mm"),
                        Enabled = rowBoard["Enabled"].ToString().ToUpper() == "TRUE" || rowBoard["Enabled"].ToString().ToUpper() == "1",
                        Deleted = rowBoard["Deleted"].ToString().ToUpper() == "TRUE" || rowBoard["Deleted"].ToString().ToUpper() == "1"
                    };

                    DataRow rowMachine = tblMachines.Select($"Id = {board.Target}").FirstOrDefault();

                    if (rowMachine != null)
                        board.MachineName = rowMachine["Machine_Name"].ToString();
                    else
                        board.MachineName = "";

                    DataRow[] rowsSensors = tblSensors.Select($"BoardId = {board.Id}");

                    foreach(DataRow row in rowsSensors)
                    {
                        board.Sensors.Add(new MBSensor()
                        {
                            SensorId = int.Parse(row["Id"].ToString()),
                            BoardId = board.Id,
                            SensorMac = row["Mac"].ToString(),
                            SensorLabel = row["Name"].ToString(),
                            SensorSerialNumber = row["SerialNumber"].ToString(),
                            SensorManufacturer = row["Manufacturer"].ToString(),
                            SensorModel = row["Model"].ToString(),
                            SensorType = row["Type"].ToString(),
                            SensorGroup = row["Group"].ToString(),
                            SensorTarget = row["Target"].ToString(),
                            SensorTags = row["Tags"].ToString(),
                            Deleted = row["Deleted"].ToString().ToUpper() == "TRUE" || row["Deleted"].ToString().ToUpper() == "1"
                        });
                    }
                    Boards.Add(board);
                }

                return Boards.OrderBy(b => b.Label).ToList();

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return new List<MonitoringBoard>();
            }
        }

        public static MonitoringBoard GetBoard(int boardId)
        {
            try
            {
                DataTable tblBoards = MySqlAccess.GetDataTable($"SELECT * FROM monitoring.monitoringboards WHERE Id = { boardId };");

                if (tblBoards.Rows.Count > 0)
                {
                    DataRow rowBoard = tblBoards.Rows[0];

                    MonitoringBoard board = new MonitoringBoard()
                    {
                        Id = int.Parse(rowBoard["Id"].ToString()),
                        SerialNumber = rowBoard["SerialNumber"].ToString(),
                        Model = rowBoard["Model"].ToString(),
                        Mac = rowBoard["Mac"].ToString(),
                        Label = rowBoard["Name"].ToString(),
                        Manufacturer = rowBoard["Manufacturer"].ToString(),
                        IpAddress = rowBoard["IpAddress"].ToString(),
                        Firmware = rowBoard["Firmware"].ToString(),
                        Target = rowBoard["Target"].ToString(),
                        CreatedAt = DateTime.Parse(rowBoard["CreatedAt"].ToString()).ToString("MM-dd-yyyy HH:mm"),
                        Enabled = bool.Parse(rowBoard["Enabled"].ToString())
                    };

                    DataTable tblMachines = MySqlAccess.GetDataTable($"SELECT * FROM csi_auth.tbl_ehub_conf WHERE Id = {board.Target};");

                    DataRow rowMachine = tblMachines.Rows[0];
                    board.MachineName = rowMachine["Machine_Name"].ToString();

                    return board;
                }

                return null;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
        }

        private static string GetBoardMac(int boardId)
        {
            MonitoringBoard board = Boards.FirstOrDefault(b => b.Id == boardId);

            if (board == null)
            {
                Boards = GetBoards();
                board = Boards.FirstOrDefault(b => b.Id == boardId);
            }
            if (board == null)
                return "";

            return board.Mac;
        }

        private static bool ServiceEcho()
        {
            try
            {
                Uri uri = new Uri(new Uri(serviceBaseAddress), $"echo");
                WebClient webClient = new WebClient();

                var response = webClient.DownloadString(uri);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

    [DataContract]
    public class Pallet
    {
        [DataMember]
        public string group { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int pressure_alert { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int pressure_csd { get; set; }
    }


    public class MonitoringBoard
    {
        public MonitoringBoard()
        {
            Sensors = new List<MBSensor>();
        }
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public string Mac { get; set; }
        public string Label { get; set; }
        public string Manufacturer { get; set; }
        public string IpAddress { get; set; }
        public string Firmware { get; set; }
        public string Target { get; set; }
        public string CreatedAt { get; set; }
        public bool Enabled { get; set; }
        public bool Deleted { get; set; }
        public string MachineName { get; set; }
        public List<MBSensor> Sensors { get; set; }
    }

    public class ServiceMonitoringBoard
    {
        public string ip { get; set; }
        public string firmware { get; set; }
        public string target { get; set; }
        public string mac { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string model { get; set; }
        public string manufacturer { get; set; }
        public string serial { get; set; }
        public bool enabled { get; set; }
    }

    public class MBSensor
    {
        public int SensorId { get; set; }
        public int BoardId { get; set; }
        public string SensorMac { get; set; }
        public string SensorLabel { get; set; }
        public string SensorSerialNumber { get; set; }
        public string SensorManufacturer { get; set; }
        public string SensorModel { get; set; }
        public string SensorType { get; set; }
        public string SensorGroup { get; set; }
        public string SensorTarget { get; set; }
        public string SensorTags { get; set; }
        public bool Deleted { get; set; }
    }

    public class MonitoringBoardState
    {
        public string name { get; set; }
        public string ip { get; set; }
        public bool monitoring { get; set; }
        public bool active_monitoring { get; set; }
        public bool monitoring_override { get; set; }
        public int sensors_count { get; set; }
        public string active_group { get; set; }
        public string server_url { get; set; }
        public int pressure_alert { get; set; }
        public int pressure_csd { get; set; }
        public bool alert_enabled { get; set; }
        public bool csd_enabled { get; set; }
    }

    public class MonitoringBoardStatus
    {
        public MonitoringBoardStatus()
        {
            SensorsStatus = new List<SensorStatus>();
        }

        public int BoardId { get; set; }
        public string BoardName { get; set; }
        public int MachineId { get; set; }
        public bool IsAvalable { get; set; }
        public bool IsMonitoring { get; set; }
        public bool IsOverride { get; set; }
        public bool IsAlarming { get; set; }
        public bool IsCSD { get; set; }
        public string CurrentPallet { get; set; }
        public double WarningPressure { get; set; }
        public double CriticalPressure { get; set; }
        public int Delay { get; set; }
        public bool StartedDelay { get; set; }
        public bool ActivatedCriticalStop { get; set; }
        public TimeSpan StartCriticalStop { get; set; }
        public List<SensorStatus> SensorsStatus { get; set; }

        public bool IsInWarningPressure
        {
            get
            {
                return (!IsInCriticalPressure && CurrentPressure <= WarningPressure);
            }
        }

        public bool IsInCriticalPressure
        {
            get
            {
                return (CurrentPressure <= CriticalPressure);
            }
        }

        public double CurrentPressure
        {
            get
            {
                double currentPressure = 0;

                SensorStatus sensor = SensorsStatus.FirstOrDefault(s => s.SensorLabel.ToUpper() == CurrentPallet.ToUpper());

                if (sensor != null)
                    double.TryParse(sensor.Pressure, out currentPressure) ;

                return currentPressure;
            }
        }

        public bool IsSensorAvailable
        {
            get
            {
                SensorStatus sensor = SensorsStatus.FirstOrDefault(s => s.SensorLabel.ToUpper() == CurrentPallet.ToUpper());

                if (sensor != null)
                    return sensor.IsAvailable;

                return false;
            }
        }
    }

    public class SensorStatus
    {
        public string SensorId { get; set; }
        public string SensorLabel { get; set; }
        public string SensorName { get; set; }
        public bool IsAvailable { get; set; } 
        public string Pressure { get; set; }
        public DateTime PressureTimeStamp { get; set; }
        public string Temperature { get; set; }
        public DateTime TemperatureTimeStamp { get; set; }
        public string Battery { get; set; }
    }
}
