using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CSIFLEX.Utilities;
using FocasLibrary.Components;
using FocasLibrary.Tools;
using TH_Global.Functions;

namespace FocasLibrary
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //InitializeComponent();
            DataContext = this;
            /*
            AgentServiceName = AgentConfigurationFile.GetServiceName();
            AgentPort = AgentConfigurationFile.GetPort();
            ServerMonitor_Initialize();
            */
            
        }

        #region "Dependency Properties"

        public bool IsAgentStarted
        {
            get { return (bool)GetValue(IsAgentStartedProperty); }
            set { SetValue(IsAgentStartedProperty, value); }
        }

        public static readonly DependencyProperty IsAgentStartedProperty =
            DependencyProperty.Register("IsAgentStarted", typeof(bool), typeof(MainWindow), new PropertyMetadata(false));

        public string AgentServiceStatus
        {
            get { return (string)GetValue(AgentServiceStatusProperty); }
            set { SetValue(AgentServiceStatusProperty, value); }
        }

        public static readonly DependencyProperty AgentServiceStatusProperty =
            DependencyProperty.Register("AgentServiceStatus", typeof(string), typeof(MainWindow), new PropertyMetadata(null));



        public string AgentServiceName
        {
            get { return (string)GetValue(AgentServiceNameProperty); }
            set { SetValue(AgentServiceNameProperty, value); }
        }

        public static readonly DependencyProperty AgentServiceNameProperty =
            DependencyProperty.Register("AgentServiceName", typeof(string), typeof(MainWindow), new PropertyMetadata(null));

        public int AgentPort
        {
            get { return (int)GetValue(AgentPortProperty); }
            set { SetValue(AgentPortProperty, value); }
        }

        public static readonly DependencyProperty AgentPortProperty =
            DependencyProperty.Register("AgentPort", typeof(int), typeof(MainWindow), new PropertyMetadata(0));

        #endregion

        #region "Agent Service Monitor"

        private void ServerMonitor_Initialize()
        {
            var timer = new System.Timers.Timer();
            timer.Interval = 100;
            timer.Elapsed += ServerMonitor_Timer_Elapsed;
            timer.Enabled = true;
        }

        private void ServerMonitor_Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var timer = (System.Timers.Timer)sender;
            timer.Interval = 2000;

            Dispatcher.BeginInvoke(new Action(ServerMonitor_GUI), UI_Functions.PRIORITY_BACKGROUND, new object[] { });
        }

        private void ServerMonitor_GUI()
        {
            AgentServiceStatus = Service_Functions.GetServiceStatus(Names.AGENT_SERVICE_NAME).ToString();
            IsAgentStarted = Service_Functions.IsServiceRunning(Names.AGENT_SERVICE_NAME);
        }

        #endregion

        private ObservableCollection<AdapterItem> _adapterItems;

        public ObservableCollection<AdapterItem> AdapterItems
        {
            get
            {
                if (_adapterItems == null)
                    _adapterItems = new ObservableCollection<AdapterItem>();
                return _adapterItems;
            }
            set
            {
                _adapterItems = value;
            }
        }

        public bool IsnotHaving = true; 
        
        private void ReadAdapterFiles()
        {
            try
            {
                string path = Paths.ADAPTERS;

                if (Directory.Exists(path))
                {
                    var files = Directory.GetFiles(path, Files.ADAPTER_INI, SearchOption.AllDirectories);
                    if (files != null)
                    {
                        AdapterItems.Clear();
                        foreach (var file in files)
                        {
                            var info = AdapterInfo.Read(file);

                            var item = new AdapterItem(info);
                            item.RemoveClicked += AdapterItem_RemoveClicked;
                            AdapterItems.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }


        private List<AgentAdapterInfo> ReadAgentAdapters(string agentConfigFile)
        {
            var result = new List<AgentAdapterInfo>();
            result = AgentConfigurationFile.GetAdapters(agentConfigFile);
            return result;
        }
        
        public void InstallAdapterForNewAgent(DeviceInfo deviceInfo)
        {
            try
            {
                
                deviceInfo.DeviceId = deviceInfo.AdapterPort.ToString();
                deviceInfo.Uuid = $"{deviceInfo.DeviceName}-{deviceInfo.AdapterPort}";
                
                var node = AgentDevicesFile.CreateDeviceNode(deviceInfo);//These two lines adds xml Code to device.xml

                if (node != null)
                    AgentDevicesFile.WriteDeviceNodeForAgent(Path.Combine(deviceInfo.AgentFolderPath, Files.AGENT_DEVICES),node);

                CreateAdapterFiles(deviceInfo); // This code creates Adapter.ini File

                string newAgentConfigFile = Path.Combine(deviceInfo.AgentFolderPath, Files.AGENT_CONFIG);
                var adapterInfos = ReadAgentAdapters(newAgentConfigFile);
                var adapterInfo = new AgentAdapterInfo();
                adapterInfo.DeviceName = deviceInfo.AdapterServiceName;
                adapterInfo.Port = deviceInfo.AdapterPort;
                adapterInfos.Add(adapterInfo);                
                AgentConfigurationFile.WriteAdaptersForAgent(newAgentConfigFile,adapterInfos);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Cursor = System.Windows.Input.Cursors.Arrow;
        }

        public void UpdateAdapterInAgentConfig(string configFile,string deviceName, int adapterPort)
        {
            if (File.Exists(configFile))
            {
                var adapterInfos = ReadAgentAdapters(configFile);
                var adapterInfo = adapterInfos.FirstOrDefault(a => a.DeviceName == deviceName);
                if (adapterInfo != null && adapterInfo.Port != adapterPort)
                {
                    adapterInfo.Port = adapterPort;
                    string serviceName = "MTCFocasAgent-" + deviceName.Replace(" ", "-");
                    AgentManagement.Stop(serviceName);
                    AgentConfigurationFile.WriteAdaptersForAgent(configFile, adapterInfos);
                    AgentManagement.Start(serviceName);
                }
            }
        }


        private void CreateAdapterFiles(DeviceInfo deviceInfo)
        {
            string path = Paths.ADAPTERS;

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            //string newPath = Path.Combine(path, deviceInfo.AdapterServiceName);

            string pathOrigin = Path.Combine(Paths.ADAPTER_TEMPLATES, deviceInfo.Adapter);

            Directory.CreateDirectory(deviceInfo.AdapterPath);

            foreach (var file in Directory.GetFiles(pathOrigin))
            {
                string filename = Path.GetFileName(file);

                File.Copy(file, Path.Combine(deviceInfo.AdapterPath, filename), true);
            }

            string iniPath = Path.Combine(deviceInfo.AdapterPath, "adapter.ini");
            string serviceName = "MTCFocasAdapter-" + deviceInfo.DeviceName.Replace(" ", "-");
            serviceName = deviceInfo.AdapterServiceName;

            string fileText = File.ReadAllText(iniPath);

            if (fileText.Contains("{port}"))
            {
                fileText = fileText
                    .Replace("{port}", deviceInfo.AdapterPort.ToString())
                    .Replace("{service}", serviceName)
                    .Replace("{hostip}", deviceInfo.AdapterFocasIp)
                    .Replace("{hostport}", deviceInfo.FocasMachinePort.ToString());

                File.WriteAllText(iniPath, fileText);
            } else
            {
                AdapterPort.Set(iniPath, deviceInfo.AdapterPort);
                FocasMachinePort.Set(iniPath, deviceInfo.FocasMachinePort);
                AdapterFocusHost.Set(iniPath, deviceInfo.AdapterFocasIp);
                AdapterSeviceName.Set(iniPath, serviceName);
            }

            string exePath = Path.Combine(deviceInfo.AdapterPath, "adapter.exe");

            AdapterManagement.Install(exePath);
        }

        public void UpdateAdapterIniFile(string deviceName, string focasMachineIp, int focasMachinePort, int adapterPort, string serviceName )
        {
            string newPath = Path.Combine(Paths.ADAPTERS, deviceName);            

            string iniPath = Path.Combine(newPath, "adapter.ini");

            //string serviceName = "MTCFocasAdapter-" + deviceName.Replace(" ", "-");

            AdapterManagement.Stop(serviceName);

            if (File.Exists(iniPath))
            {
                AdapterPort.Set(iniPath, adapterPort);
                FocasMachinePort.Set(iniPath, focasMachinePort);
                AdapterFocusHost.Set(iniPath, focasMachineIp);
                AdapterSeviceName.Set(iniPath, serviceName);
            }
            AdapterManagement.Start(serviceName);
        }


        public void CreateFilesAndInstallAgentService(DeviceInfo deviceInfo)
        {
            string srcPath = Paths.AGENT_TEMPLATE;

            if (!Directory.Exists(deviceInfo.AgentFolderPath))
                Directory.CreateDirectory(deviceInfo.AgentFolderPath);

            foreach (var file in Directory.GetFiles(srcPath))
            {
                string filename = Path.GetFileName(file);

                File.Copy(file, Path.Combine(deviceInfo.AgentFolderPath, filename), true);
            }

            string configFile = Path.Combine(deviceInfo.AgentFolderPath, Files.AGENT_CONFIG);

            string serviceName = "MTCFocasAgent-" + deviceInfo.DeviceName.Replace(" ", "-");

            serviceName = deviceInfo.AgentServiceName;

            if (File.Exists(configFile))
            {
                var allLines = File.ReadAllLines(configFile);

                if (allLines!= null && allLines.Any())

                for (int i = 0; i < allLines.Length; i++)
                {
                    if (allLines[i].StartsWith("Port ="))
                    {
                        //only the fisrt is the agent port.
                        allLines[i] = "Port = " + deviceInfo.AgentPort.ToString();
                        break;
                    }
                }

                for (int i = 0; i < allLines.Length; i++)
                {
                    if (allLines[i].StartsWith("ServiceName ="))
                    {
                        //only the fisrt is the agent port.
                        allLines[i] = "ServiceName = " + serviceName;
                        break;
                    }
                }
                File.WriteAllLines(configFile, allLines);
            }

            AgentAdapterInfo info = new AgentAdapterInfo()
            {
                DeviceName = deviceInfo.AdapterServiceName,
                FocasMachinePort = deviceInfo.FocasMachinePort,
                Port = deviceInfo.AdapterPort
            };
            List<AgentAdapterInfo> infos = new List<AgentAdapterInfo>();
            infos.Add(info);
            AgentConfigurationFile.WriteAdaptersForAgent(configFile, infos);

            deviceInfo.DeviceId = deviceInfo.AdapterPort.ToString();
            deviceInfo.Uuid = $"{deviceInfo.DeviceName}-{deviceInfo.AdapterPort}";

            var node = AgentDevicesFile.CreateDeviceNode(deviceInfo);//These two lines adds xml Code to device.xml

            if (node != null)
                AgentDevicesFile.WriteDeviceNodeForAgent(Path.Combine(deviceInfo.AgentFolderPath, Files.AGENT_DEVICES), node);

            //string exePath = Path.Combine(deviceInfo.AgentFolderPath, "agent.exe");
            AgentManagement.Install(deviceInfo.AgentFolderPath, serviceName);
        }


        private void AdapterItem_RemoveClicked(AdapterInfo info)
        {
            if (info != null) UninstallAdapter(info);

            int i = AdapterItems.ToList().FindIndex(x => x.ServiceName == info.ServiceName);
            if (i >= 0) AdapterItems.RemoveAt(i);
        }

        public void UninstallAdapter(AdapterInfo info)
        {
            //Cursor = System.Windows.Input.Cursors.Wait;

            try
            {
                ServiceTools.ServiceState state = ServiceTools.ServiceInstaller.GetServiceStatus(info.ServiceName);

                if (state != ServiceTools.ServiceState.NotFound && state != ServiceTools.ServiceState.Unknown)
                {
                    AdapterManagement.Stop(info.ServiceName);
                    AdapterManagement.Uninstall(info.ServiceName);
                }

                // Remove adapter from Agent Configuration File (agent.cfg);
                string agentConfigFile = Path.Combine(string.Format(Paths.AGENT_FOLDER_FORMAT,info.DeviceName), Files.AGENT_CONFIG);
                var adapterInfos = ReadAgentAdapters(agentConfigFile);
                var match1 = adapterInfos.FirstOrDefault(x => x.DeviceName == info.DeviceName);

                if (match1 != null)
                    adapterInfos.Remove(match1);

                AgentConfigurationFile.WriteAdaptersForAgent(agentConfigFile,adapterInfos);
                
                // Remove Device node from Agent's devices.xml file
                string agentDevicesFile = Path.Combine(string.Format(Paths.AGENT_FOLDER_FORMAT, info.DeviceName), Files.AGENT_DEVICES);

                var deviceInfos = AgentDevicesFile.ReadDeviceInfos(agentDevicesFile);

                var match2 = deviceInfos.FirstOrDefault(x => x.DeviceName == info.DeviceName);

                if (match2 != null)
                {
                    string id = match2.DeviceId;
                    string devicesFile = Path.Combine(string.Format(Paths.AGENT_FOLDER_FORMAT, info.DeviceName), Files.AGENT_DEVICES);
                    AgentDevicesFile.DeleteDeviceNodeForAgent(devicesFile, "//Device[@id=\"" + id + "\"]");
                }

                //// Delete adapter folder in "Adapters"
                //string path = Path.Combine(info.Path, info.DeviceName);

                if(Directory.Exists(info.Path))
                    Directory.Delete(info.Path, true);
            }
            catch (Exception ex) {
                Log.Error(ex);
                Console.WriteLine(ex.Message);
            }

            // Cursor = System.Windows.Input.Cursors.Arrow;
        }

        public void UninstallAgent(string machineName, string serviceName)
        {
            try
            {
                //string serviceName = "MTCFocasAgent-" + machineName.Replace(" ", "-");
                AgentManagement.Stop(serviceName);
                AdapterManagement.Uninstall(serviceName);
                // Remove adapter from Agent Configuration File (agent.cfg);

                if (Directory.Exists(string.Format(Paths.AGENT_FOLDER_FORMAT, machineName)))
                {
                    Directory.Delete(string.Format(Paths.AGENT_FOLDER_FORMAT, machineName), true);
                }
                else if (Directory.Exists(string.Format(Paths.AGENT_FOLDER_FORMAT, serviceName)))
                {
                    Directory.Delete(string.Format(Paths.AGENT_FOLDER_FORMAT, serviceName), true);
                }

                //string path = Path.GetDirectoryName(Path.Combine(string.Format(Paths.AGENT_FOLDER_FORMAT, serviceName), Files.AGENT_EXE));

                //Directory.Delete(path, true);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }       
    }
}
