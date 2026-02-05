//source:http://stackoverflow.com/questions/358700/how-to-install-a-windows-service-programmatically-in-c
//source:http://www.tech-archive.net/Archive/VB/microsoft.public.vb.winapi/2006-08/msg00238.html

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

//forcing uninstall
using System.Diagnostics;

namespace ServiceTools
{
    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum ServiceManagerRights
    {
        /// <summary>
        /// 
        /// </summary>
        Connect = 0x0001,
        /// <summary>
        /// 
        /// </summary>
        CreateService = 0x0002,
        /// <summary>
        /// 
        /// </summary>
        EnumerateService = 0x0004,
        /// <summary>
        /// 
        /// </summary>
        Lock = 0x0008,
        /// <summary>
        /// 
        /// </summary>
        QueryLockStatus = 0x0010,
        /// <summary>
        /// 
        /// </summary>
        ModifyBootConfig = 0x0020,
        /// <summary>
        /// 
        /// </summary>
        StandardRightsRequired = 0xF0000,
        /// <summary>
        /// 
        /// </summary>
        AllAccess = (StandardRightsRequired | Connect | CreateService |
        EnumerateService | Lock | QueryLockStatus | ModifyBootConfig)
    }

    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum ServiceRights
    {
        /// <summary>
        /// 
        /// </summary>
        QueryConfig = 0x1,
        /// <summary>
        /// 
        /// </summary>
        ChangeConfig = 0x2,
        /// <summary>
        /// 
        /// </summary>
        QueryStatus = 0x4,
        /// <summary>
        /// 
        /// </summary>
        EnumerateDependants = 0x8,
        /// <summary>
        /// 
        /// </summary>
        Start = 0x10,
        /// <summary>
        /// 
        /// </summary>
        Stop = 0x20,
        /// <summary>
        /// 
        /// </summary>
        PauseContinue = 0x40,
        /// <summary>
        /// 
        /// </summary>
        Interrogate = 0x80,
        /// <summary>
        /// 
        /// </summary>
        UserDefinedControl = 0x100,
        /// <summary>
        /// 
        /// </summary>
        Delete = 0x00010000,
        /// <summary>
        /// 
        /// </summary>
        StandardRightsRequired = 0xF0000,
        /// <summary>
        /// 
        /// </summary>
        AllAccess = (StandardRightsRequired | QueryConfig | ChangeConfig |
        QueryStatus | EnumerateDependants | Start | Stop | PauseContinue |
        Interrogate | UserDefinedControl)
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ServiceBootFlag
    {
        /// <summary>
        /// 
        /// </summary>
        Start = 0x00000000,
        /// <summary>
        /// 
        /// </summary>
        SystemStart = 0x00000001,
        /// <summary>
        /// 
        /// </summary>
        AutoStart = 0x00000002,
        /// <summary>
        /// 
        /// </summary>
        DemandStart = 0x00000003,
        /// <summary>
        /// 
        /// </summary>
        Disabled = 0x00000004
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ServiceState
    {
        /// <summary>
        /// 
        /// </summary>
        Unknown = -1, // The state cannot be (has not been) retrieved.
        /// <summary>
        /// 
        /// </summary>
        NotFound = 0, // The service is not known on the host server.
        /// <summary>
        /// 
        /// </summary>
        Stop = 1, // The service is NET stopped.
        /// <summary>
        /// 
        /// </summary>
        Run = 4, // The service is NET starting. Default is 2
        /// <summary>
        /// 
        /// </summary>
        Stopping = 3,
        /// <summary>
        /// 
        /// </summary>
        Starting = 2, // The service is NET running. Default is 4
    }

    /// <summary>
    /// Service types.
    /// </summary>
    [Flags]
    public enum SERVICE_TYPE : int
    {
        /// <summary>
        /// Driver service.
        /// </summary>
        SERVICE_KERNEL_DRIVER = 0x00000001,

        /// <summary>
        /// File system driver service.
        /// </summary>
        SERVICE_FILE_SYSTEM_DRIVER = 0x00000002,

        /// <summary>
        /// Service that runs in its own process.
        /// </summary>
        SERVICE_WIN32_OWN_PROCESS = 0x00000010,

        /// <summary>
        /// Service that shares a process with one or more other services.
        /// </summary>
        SERVICE_WIN32_SHARE_PROCESS = 0x00000020,

        /// <summary>
        /// The service can interact with the desktop.
        /// </summary>
        SERVICE_INTERACTIVE_PROCESS = 0x00000100,
    }

    /// <summary>
    /// Service configuration
    /// </summary>
    [Flags]
    public enum SERVICE_CONFIGURATION : int
    {
        /// <summary>
        /// The lpInfo parameter is a pointer to a SERVICE_DESCRIPTION structure.
        /// </summary>
        SERVICE_CONFIG_DESCRIPTION = 0x00000001,

        /// <summary>
        /// The lpInfo parameter is a pointer to a SERVICE_FAILURE_ACTIONS structure.
        /// If the service controller handles the SC_ACTION_REBOOT action, the caller must have the SE_SHUTDOWN_NAME privilege. 
        /// For more information, see Running with Special Privileges.
        /// </summary>
        SERVICE_CONFIG_FAILURE_ACTIONS = 0x00000002,

        /// <summary>
        /// The lpInfo parameter is a pointer to a SERVICE_DELAYED_AUTO_START_INFO structure.
        /// Windows Server 2003 and Windows XP:  This value is not supported.
        /// </summary>
        SERVICE_CONFIG_DELAYED_AUTO_START_INFO = 0x00000003,

        /// <summary>
        /// The lpInfo parameter is a pointer to a SERVICE_FAILURE_ACTIONS_FLAG structure.
        /// Windows Server 2003 and Windows XP:  This value is not supported.
        /// </summary>
        SERVICE_CONFIG_FAILURE_ACTIONS_FLAG = 0x00000004,

        /// <summary>
        /// The lpInfo parameter is a pointer to a SERVICE_SID_INFO structure.
        /// </summary>
        SERVICE_CONFIG_SERVICE_SID_INFO = 0x00000005,

        /// <summary>
        /// The lpInfo parameter is a pointer to a SERVICE_REQUIRED_PRIVILEGES_INFO structure.
        /// Windows Server 2003 and Windows XP:  This value is not supported.
        /// </summary>
        SERVICE_CONFIG_REQUIRED_PRIVILEGES_INFO = 0x00000006,

        /// <summary>
        /// The lpInfo parameter is a pointer to a SERVICE_PRESHUTDOWN_INFO structure.
        /// Windows Server 2003 and Windows XP:  This value is not supported.
        /// </summary>
        SERVICE_CONFIG_PRESHUTDOWN_INFO = 0x00000007,

        /// <summary>
        /// The lpInfo parameter is a pointer to a SERVICE_TRIGGER_INFO structure. This value is not supported by the ANSI version of ChangeServiceConfig2.
        /// Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP:  This value is not supported until Windows Server 2008 R2.
        /// </summary>
        SERVICE_CONFIG_TRIGGER_INFO = 0x00000008,

        /// <summary>
        /// The lpInfo parameter is a pointer to a SERVICE_PREFERRED_NODE_INFO structure.
        /// Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP:  This value is not supported.
        /// </summary>
        SERVICE_CONFIG_PREFERRED_NODE = 0x00000009,

        /// <summary>
        /// The lpInfo parameter is a pointer a SERVICE_LAUNCH_PROTECTED_INFO structure.
        /// Note  This value is supported starting with Windows 8.1.
        /// </summary>
        SERVICE_CONFIG_LAUNCH_PROTECTED = 0x00000012,
    }


    /// <summary>
    /// 
    /// </summary>
    public enum ServiceControl
    {
        /// <summary>
        /// 
        /// </summary>
        Stop = 0x00000001,
        /// <summary>
        /// 
        /// </summary>
        Pause = 0x00000002,
        /// <summary>
        /// 
        /// </summary>
        Continue = 0x00000003,
        /// <summary>
        /// 
        /// </summary>
        Interrogate = 0x00000004,
        /// <summary>
        /// 
        /// </summary>
        Shutdown = 0x00000005,
        /// <summary>
        /// 
        /// </summary>
        ParamChange = 0x00000006,
        /// <summary>
        /// 
        /// </summary>
        NetBindAdd = 0x00000007,
        /// <summary>
        /// 
        /// </summary>
        NetBindRemove = 0x00000008,
        /// <summary>
        /// 
        /// </summary>
        NetBindEnable = 0x00000009,
        /// <summary>
        /// 
        /// </summary>
        NetBindDisable = 0x0000000A
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ServiceError
    {
        /// <summary>
        /// 
        /// </summary>
        Ignore = 0x00000000,
        /// <summary>
        /// 
        /// </summary>
        Normal = 0x00000001,
        /// <summary>
        /// 
        /// </summary>
        Severe = 0x00000002,
        /// <summary>
        /// 
        /// </summary>
        Critical = 0x00000003
    }

    /// <summary>
    /// Installs and provides functionality for handling windows services
    /// </summary>
    public class ServiceInstaller
    {
        //private const int STANDARD_RIGHTS_REQUIRED = 0xF0000;
        //private const int SERVICE_WIN32_OWN_PROCESS = 0x00000010;        

        [StructLayout(LayoutKind.Sequential)]
        private class SERVICE_STATUS
        {
            public int dwServiceType = 0;
            public ServiceState dwCurrentState = 0;
            public int dwControlsAccepted = 0;
            public int dwWin32ExitCode = 0;
            public int dwServiceSpecificExitCode = 0;
            public int dwCheckPoint = 0;
            public int dwWaitHint = 0;
        }


        // Structure that contains the delayed auto-start setting 
        // for an auto-start service. 
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct SERVICE_DELAYED_AUTO_START_INFO
        {
            // Set true to make service as delayed auto-start,
            // false to start service automatically at system boot (auto-start).
            // This setting works only for auto start service.
            public bool fDelayedAutostart;
        }


        [DllImport("advapi32.dll", EntryPoint = "OpenSCManagerA")]
        private static extern IntPtr OpenSCManager(string lpMachineName, string lpDatabaseName, ServiceManagerRights dwDesiredAccess);

        [DllImport("advapi32.dll", EntryPoint = "OpenServiceA",
        CharSet = CharSet.Ansi)]
        private static extern IntPtr OpenService(IntPtr hSCManager, string lpServiceName, ServiceRights dwDesiredAccess);

        [DllImport("advapi32.dll", EntryPoint = "CreateServiceA")]
        private static extern IntPtr CreateService(IntPtr hSCManager, string lpServiceName, string lpDisplayName, ServiceRights dwDesiredAccess,
            SERVICE_TYPE dwServiceType, ServiceBootFlag dwStartType, ServiceError dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, IntPtr lpdwTagId,
            string lpDependencies, string lp, string lpPassword);


        [DllImport("advapi32.dll")]
        private static extern int CloseServiceHandle(IntPtr hSCObject);

        [DllImport("advapi32.dll")]
        private static extern int QueryServiceStatus(IntPtr hService, SERVICE_STATUS lpServiceStatus);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern int DeleteService(IntPtr hService);

        [DllImport("advapi32.dll")]
        private static extern int ControlService(IntPtr hService, ServiceControl dwControl, SERVICE_STATUS lpServiceStatus);

        [DllImport("advapi32.dll", EntryPoint = "StartServiceA")]
        private static extern int StartService(IntPtr hService, int dwNumServiceArgs, int lpServiceArgVectors);

        // Win32 API to change the service configuration parameters.
        // <"hService"> Service instance handle. 
        // <"dwInfoLevel"> Configuration parameter that need to be modified.
        // <"lpInfo"> A new value to be set for the configuration parameter.
        // True for success, false for failure.
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ChangeServiceConfig2(IntPtr hService, SERVICE_CONFIGURATION dwInfoLevel, IntPtr lpInfo);


        /// <summary>
        /// 
        /// </summary>
        public ServiceInstaller()
        {
        }


        /// <summary>
        /// Takes a service name and tries to stop and then uninstall the windows serviceError
        /// </summary>
        /// <param name="ServiceName">The windows service name to uninstall</param>
        public static void Uninstall(string ServiceName)
        {
            //force uninstall
            //source:http://stackoverflow.com/questions/225275/how-to-force-uninstallation-of-windows-service

            IntPtr scman = OpenSCManager(ServiceManagerRights.Connect);
            try
            {
                IntPtr service = OpenService(scman, ServiceName,
                ServiceRights.StandardRightsRequired | ServiceRights.Stop |
                ServiceRights.QueryStatus);
                if (service == IntPtr.Zero)
                {
                    throw new ApplicationException("Service not installed.");
                }
                try
                {

                    //Force kill
                    //try
                    //{
                    //    //Process[] procs = Process.GetProcesses();
                    //    //foreach (Process p in procs) { p.Kill(); }
                    //    var chromeDriverProcesses = Process.GetProcessesByName(ServiceName);

                    //    foreach (var process in chromeDriverProcesses)
                    //    {
                    //        process.Kill();
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    System.Console.WriteLine(ex.Message);
                    //    //throw;
                    //}

                    StopService(service);
                    int ret = DeleteService(service);
                    if (ret == 0)
                    {
                        int error = Marshal.GetLastWin32Error();
                        throw new ApplicationException("Could not delete service " + error);
                    }


                }
                finally
                {
                    CloseServiceHandle(service);
                }
            }
            finally
            {
                CloseServiceHandle(scman);
            }


        }


        /// <summary>
        /// Accepts a service name and returns true if the service with that service name exists
        /// </summary>
        /// <param name="ServiceName">The service name that we will check for existence</param>
        /// <returns>True if that service exists false otherwise</returns>
        public static bool ServiceIsInstalled(string ServiceName)
        {
            IntPtr scman = OpenSCManager(ServiceManagerRights.Connect);
            try
            {
                IntPtr service = OpenService(scman, ServiceName, ServiceRights.QueryStatus);
                if (service == IntPtr.Zero) return false;
                CloseServiceHandle(service);
                return true;
            }
            finally
            {
                CloseServiceHandle(scman);
            }
        }


        /// <summary>
        /// Takes a service name, a service display name and the path to the service executable and installs / starts the windows service.
        /// </summary>
        /// <param name="ServiceName">The service name that this service will have</param>
        /// <param name="DisplayName">The display name that this service will have</param>
        /// <param name="FileName">The path to the executable of the service</param>
        public static void InstallAndStart(string ServiceName, string DisplayName, string FileName)
        {
            IntPtr scman = OpenSCManager(ServiceManagerRights.Connect | ServiceManagerRights.CreateService);
            try
            {
                IntPtr service = OpenService(scman, ServiceName, ServiceRights.QueryStatus | ServiceRights.Start);
                if (service == IntPtr.Zero)
                {
                    service = CreateService(scman, ServiceName, DisplayName,
                    ServiceRights.QueryStatus | ServiceRights.Start, SERVICE_TYPE.SERVICE_WIN32_OWN_PROCESS,
                    ServiceBootFlag.AutoStart, ServiceError.Normal, FileName, null, IntPtr.Zero,
                    null, null, null);
                }

                if (service == IntPtr.Zero)
                {
                    throw new ApplicationException("Failed to install service.");
                }

                try
                {
                    StartService(service);
                }
                finally
                {
                    CloseServiceHandle(service);
                }
            }
            finally
            {
                CloseServiceHandle(scman);
            }

            SetDelayedStart(ServiceName);
        }


        /// <summary>
        /// Takes a service name and set the service configuration to delayed start
        /// </summary>
        /// <param name="ServiceName">The service name</param>
        public static void SetDelayedStart(string ServiceName)
        {
            IntPtr scman = OpenSCManager(ServiceManagerRights.Connect | ServiceManagerRights.ModifyBootConfig);
            try
            {
                IntPtr service = OpenService(scman, ServiceName, ServiceRights.ChangeConfig);
                if (service != IntPtr.Zero)
                {
                    try
                    {
                        ConfigServiceAsDelayedStart(service);
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Failed to configure delayed autostart service. MSG:" + ex.Message);
                    }
                }
            }
            finally
            {
                CloseServiceHandle(scman);
            }
        }


        /// <summary>
        /// Change the configuration of the service
        /// </summary>
        /// <param name="hService">The handle to the windows service</param>
        private static void ConfigServiceAsDelayedStart(IntPtr hService)
        {
            bool isdelayed = true;

            // Validate service handle
            if (hService != IntPtr.Zero)
            {
                // Create 
                SERVICE_DELAYED_AUTO_START_INFO info = new SERVICE_DELAYED_AUTO_START_INFO();

                // Set the DelayedAutostart property
                info.fDelayedAutostart = isdelayed;

                // Allocate necessary memory
                IntPtr hInfo = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SERVICE_DELAYED_AUTO_START_INFO)));

                // Convert structure to pointer
                Marshal.StructureToPtr(info, hInfo, true);


                // Change the configuration
                bool result = ChangeServiceConfig2(hService, SERVICE_CONFIGURATION.SERVICE_CONFIG_DELAYED_AUTO_START_INFO, hInfo);
                int error = Marshal.GetLastWin32Error();

                // Release memory
                Marshal.FreeHGlobal(hInfo);
            }
        }


        /// <summary>
        /// Takes a service name and starts it
        /// </summary>
        /// <param name="Name">The service name</param>
        public static void StartService(string Name)
        {
            IntPtr scman = OpenSCManager(ServiceManagerRights.Connect);
            try
            {
                IntPtr hService = OpenService(scman, Name, ServiceRights.QueryStatus | ServiceRights.Start);
                if (hService == IntPtr.Zero)
                {
                    throw new ApplicationException("Could not open service.");
                }
                try
                {
                    StartService(hService);
                }
                finally
                {
                    CloseServiceHandle(hService);
                }
            }
            finally
            {
                CloseServiceHandle(scman);
            }
        }


        /// <summary>
        /// Stops the provided windows service
        /// </summary>
        /// <param name="Name">The service name that will be stopped</param>
        public static void StopService(string Name)
        {
            IntPtr scman = OpenSCManager(ServiceManagerRights.Connect);
            try
            {
                IntPtr hService = OpenService(scman, Name, ServiceRights.QueryStatus | ServiceRights.Stop);
                if (hService == IntPtr.Zero)
                {
                    throw new ApplicationException("Could not open service.");
                }
                try
                {
                    StopService(hService);
                }
                finally
                {
                    CloseServiceHandle(hService);
                }
            }
            finally
            {
                CloseServiceHandle(scman);
            }
        }


        /// <summary>
        /// Stars the provided windows service
        /// </summary>
        /// <param name="hService">The handle to the windows service</param>
        private static void StartService(IntPtr hService)
        {
            SERVICE_STATUS status = new SERVICE_STATUS();
            StartService(hService, 0, 0);
            WaitForServiceStatus(hService, ServiceState.Starting, ServiceState.Run);
        }


        /// <summary>
        /// Stops the provided windows service
        /// </summary>
        /// <param name="hService">The handle to the windows service</param>
        private static void StopService(IntPtr hService)
        {
            SERVICE_STATUS status = new SERVICE_STATUS();
            ControlService(hService, ServiceControl.Stop, status);
            WaitForServiceStatus(hService, ServiceState.Stopping, ServiceState.Stop);
        }


        /// <summary>
        /// Takes a service name and returns the <code>ServiceState</code> of the corresponding service
        /// </summary>
        /// <param name="ServiceName">The service name that we will check for his <code>ServiceState</code></param>
        /// <returns>The ServiceState of the service we wanted to check</returns>
        public static ServiceState GetServiceStatus(string ServiceName)
        {
            IntPtr scman = OpenSCManager(ServiceManagerRights.Connect);
            try
            {
                IntPtr hService = OpenService(scman, ServiceName, ServiceRights.QueryStatus);
                if (hService == IntPtr.Zero)
                {
                    return ServiceState.NotFound;
                }
                try
                {
                    return GetServiceStatus(hService);
                }
                finally
                {
                    CloseServiceHandle(scman);
                }
            }
            finally
            {
                CloseServiceHandle(scman);
            }
        }


        /// <summary>
        /// Gets the service state by using the handle of the provided windows service
        /// </summary>
        /// <param name="hService">The handle to the service</param>
        /// <returns>The <code>ServiceState</code> of the service</returns>
        private static ServiceState GetServiceStatus(IntPtr hService)
        {
            SERVICE_STATUS ssStatus = new SERVICE_STATUS();
            if (QueryServiceStatus(hService, ssStatus) == 0)
            {
                throw new ApplicationException("Failed to query service status.");
            }
            return ssStatus.dwCurrentState;
        }


        /// <summary>
        /// Returns true when the service status has been changes from wait status to desired status
        /// ,this method waits around 10 seconds for this operation.
        /// </summary>
        /// <param name="hService">The handle to the service</param>
        /// <param name="WaitStatus">The current state of the service</param>
        /// <param name="DesiredStatus">The desired state of the service</param>
        /// <returns>bool if the service has successfully changed states within the allowed timeline</returns>
        private static bool WaitForServiceStatus(IntPtr hService, ServiceState WaitStatus, ServiceState DesiredStatus)
        {
            SERVICE_STATUS ssStatus = new SERVICE_STATUS();
            int dwOldCheckPoint;
            int dwStartTickCount;

            QueryServiceStatus(hService, ssStatus);
            if (ssStatus.dwCurrentState == DesiredStatus) return true;
            dwStartTickCount = Environment.TickCount;
            dwOldCheckPoint = ssStatus.dwCheckPoint;

            while (ssStatus.dwCurrentState == WaitStatus)
            {
                // Do not wait longer than the wait hint. A good interval is
                // one tenth the wait hint, but no less than 1 second and no
                // more than 10 seconds.

                int dwWaitTime = ssStatus.dwWaitHint / 10;

                if (dwWaitTime < 1000) dwWaitTime = 1000;
                else if (dwWaitTime > 10000) dwWaitTime = 10000;

                System.Threading.Thread.Sleep(dwWaitTime);

                // Check the status again.

                if (QueryServiceStatus(hService, ssStatus) == 0) break;

                if (ssStatus.dwCheckPoint > dwOldCheckPoint)
                {
                    // The service is making progress.
                    dwStartTickCount = Environment.TickCount;
                    dwOldCheckPoint = ssStatus.dwCheckPoint;
                }
                else
                {
                    if (Environment.TickCount - dwStartTickCount > ssStatus.dwWaitHint)
                    {
                        // No progress made within the wait hint
                        break;
                    }
                }
            }
            return (ssStatus.dwCurrentState == DesiredStatus);
        }


        /// <summary>
        /// Opens the service manager
        /// </summary>
        /// <param name="Rights">The service manager rights</param>
        /// <returns>the handle to the service manager</returns>
        private static IntPtr OpenSCManager(ServiceManagerRights Rights)
        {
            IntPtr scman = OpenSCManager(null, null, Rights);
            if (scman == IntPtr.Zero)
            {
                throw new ApplicationException("Could not connect to service control manager.");
            }
            return scman;
        }
    }
}