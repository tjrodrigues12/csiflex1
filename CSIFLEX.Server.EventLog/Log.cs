using System;
using log4net;
using log4net.Repository.Hierarchy;
using log4net.Core;
using log4net.Appender;
using log4net.Layout;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace CSIFLEX.Server.Log
{
    public class Log
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region singleton 
        private Log() { }

        private static Log _instance = null;

        public static Log Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Log();
                return _instance;
            }
        }
        #endregion



        public static void LogError(string errorMessage, Exception exception = null)
        {
            log.Error(errorMessage, exception);
        }
    }
}
