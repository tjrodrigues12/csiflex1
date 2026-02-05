using NLog;
using System;
using System.Diagnostics;
using System.IO;

namespace CSIFLEX.Server.Library
{
    public class Log
    {
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

        private static Logger logger;

        private static StackTrace stackTrace;

        public void Init(string application, string logPath)
        {
            LogManager.Configuration.Variables["logPath"] = logPath;
            LogManager.ReconfigExistingLoggers();

            logger = LogManager.GetLogger(application);
        }

        public static void Fatal(string message, Exception exception = null)
        {
            stackTrace = new StackTrace();
            logger.Fatal(exception, $"{stackTrace.GetFrame(1).GetMethod().ReflectedType}.{stackTrace.GetFrame(1).GetMethod().Name}: {message}");
        }

        public static void Fatal(Exception exception)
        {
            stackTrace = new StackTrace();
            logger.Fatal(exception, $"{stackTrace.GetFrame(1).GetMethod().ReflectedType}.{stackTrace.GetFrame(1).GetMethod().Name}");
        }

        public static void Error(string message, Exception exception = null)
        {
            stackTrace = new StackTrace();
            logger.Error(exception, $"{stackTrace.GetFrame(1).GetMethod().ReflectedType}.{stackTrace.GetFrame(1).GetMethod().Name}: {message}");
        }

        public static void Error(Exception exception = null)
        {
            stackTrace = new StackTrace();
            logger.Error(exception, $"{stackTrace.GetFrame(1).GetMethod().ReflectedType}.{stackTrace.GetFrame(1).GetMethod().Name}");
        }

        public static void Warn(string message)
        {
            stackTrace = new StackTrace();
            logger.Warn($"{stackTrace.GetFrame(1).GetMethod().ReflectedType}.{stackTrace.GetFrame(1).GetMethod().Name}: {message}");
        }

        public static void Info(string message)
        {
            stackTrace = new StackTrace();
            logger.Info($"{stackTrace.GetFrame(1).GetMethod().ReflectedType}.{stackTrace.GetFrame(1).GetMethod().Name}: {message}");
        }

        public static void Debug(string message)
        {
            stackTrace = new StackTrace();
            logger.Debug($"{stackTrace.GetFrame(1).GetMethod().ReflectedType}.{stackTrace.GetFrame(1).GetMethod().Name}: {message}");
        }

        public static void Trace(string message)
        {
            stackTrace = new StackTrace();
            logger.Trace($"{stackTrace.GetFrame(1).GetMethod().ReflectedType}.{stackTrace.GetFrame(1).GetMethod().Name}: {message}");
        }
    }
}
