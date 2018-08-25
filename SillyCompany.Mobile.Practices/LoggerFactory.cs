using System;

using MetroLog;

namespace SillyCompany.Mobile.Practices
{
    public static class LoggerFactory
    {
        private static ILogManager _logManager;

        public static void Initialize(LoggingConfiguration configuration)
        {
            _logManager = LogManagerFactory.CreateLogManager(configuration);
        }

        public static ILogger GetLogger(string loggerName)
        {
            if (_logManager == null)
            {
                throw new InvalidOperationException("LogFactory must be Initialized before creating any logger");
            }

            return _logManager.GetLogger(loggerName);
        }
    }
}
