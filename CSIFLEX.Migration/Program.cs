using FocasLibrary.Components;
using FocasLibrary.Tools;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSIFLEX.Migration
{
    class Program
    {
        private static FocasLibrary.MainWindow _window = new FocasLibrary.MainWindow();

        [STAThread]
        static void Main(string[] args)
        {
            string fileName = $"migration-{DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss")}.txt";
            using (StreamWriter w = File.AppendText(fileName))
            {
                Log("migration started...", w);
                Log("*****************************Agent/Adapter installations***********************************************", w);
                var connectors = GetFocasMachines();
                if (connectors != null && connectors.Any())
                {
                    Log($"{connectors.Count} Focas Machines loaded from DB", w);
                    var services = ServiceController.GetServices();
                    bool installed = false;
                    foreach (var connector in connectors.Where(c=>c.ConnectorType == "Focas"))
                    {
                        var agentScv = services.FirstOrDefault(s => s.ServiceName == connector.AgentServiceName);
                        if (agentScv == null)
                        {
                            Log($"installing services for {connector.MachineName}", w);
                            installed = false;
                            //servic doesnt exist
                            var deviceInfo = new DeviceInfo()
                            {
                                DeviceName = connector.MachineName,
                                AdapterFocasIp = connector.MachineIp,
                                FocasMachinePort = connector.FocasPort,
                                Manufacturer = connector.Manufacturer,
                                Adapter = connector.ControllerType,
                                AgentPort = connector.AgentPort,
                                AdapterPort = connector.AdapterPort,
                                AgentFolderPath = String.Format(Paths.AGENT_FOLDER_FORMAT, connector.MachineName)
                            };
                            try
                            {
                                _window.CreateFilesAndInstallAgentService(deviceInfo);
                                _window.InstallAdapterForNewAgent(deviceInfo);

                                Log($"Adapter and Agent successfully installed for machine {connector.MachineName}",w);
                                installed = true;
                            }
                            catch (Exception ex)
                            {
                                Log($"error while installing service for machine {connector.MachineName}: "+ex.Message , w);
                            }
                            if (installed)
                            {
                                Log($"starting services for {connector.MachineName}",w);
                                AdapterManagement.Start(connector.AdapterServiceName);
                                AgentManagement.Start(connector.AgentServiceName);
                            }
                        }
                        else
                        {
                            Log($"Service already installed for {connector.MachineName}", w);
                        }
                    }
                }
                Log("*****************************Agent/Adapter installations***********************************************",w);
                Log("migration successfully ended.",w);
            }
        }        

        private static List<CsiMachineConnector> GetFocasMachines()
        {
            var retVal = new List<CsiMachineConnector>();
            if (ConfigurationManager.ConnectionStrings["ServerDB"] != null)
            {
                var cntsql = new MySqlConnection(ConfigurationManager.ConnectionStrings["ServerDB"].ConnectionString);
                try
                {
                    cntsql.Open();
                    var SelectAllcsiconnector = "SELECT * from csi_auth.tbl_csiconnector Where ConnectorType = 'Focas';";
                    var cmdSelectAllcsiconnector = new MySqlCommand(SelectAllcsiconnector, cntsql);
                    var mysqlReader = cmdSelectAllcsiconnector.ExecuteReader();
                    DataTable dTable = new DataTable();
                    dTable.Load(mysqlReader);
                    if (dTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in dTable.Rows)
                        {
                            retVal.Add(new CsiMachineConnector
                            {
                                MachineName = row["MachineName"] as string,
                                MachineIp = row["MachineIp"] as string,
                                Manufacturer = row["Manufacturer"] as string,
                                EnetMachineName = row["eNETMachineName"] as string,
                                FocasPort = Convert.ToInt32(row["FocasPort"]),
                                ConnectorType = row["ConnectorType"] as string,
                                ControllerType = row["ControllerType"] as string,
                                AgentPort = Convert.ToInt32(row["AgentPort"]),
                                AdapterPort = Convert.ToInt32(row["AdapterPort"]),
                                AgentServiceName = row["AgentServiceName"] as string,
                                AdapterServiceName = row["AdapterServiceName"] as string
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"error while loading focas machines from DB : ",ex);
                }
            }
            return retVal;
        }

        private static void Log(string msg, StreamWriter w)
        {
            Console.WriteLine(msg);
            w.WriteLine(msg);
        }
    }
}
