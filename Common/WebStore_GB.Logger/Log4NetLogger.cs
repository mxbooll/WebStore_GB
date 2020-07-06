using log4net;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Xml;

namespace WebStore_GB.Logger
{
    public class Log4NetLogger : ILogger
    {
        private readonly ILog _log;

        public Log4NetLogger(string categoryName, XmlElement configuration)
        {
            var loggerRepository = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            _log = LogManager.GetLogger(loggerRepository.Name, categoryName);

            log4net.Config.XmlConfigurator.Configure(loggerRepository, configuration);
        }

        // логгер не поддерживает областей
        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel level)
        {
            switch (level)
            {
                default: throw new ArgumentOutOfRangeException(nameof(level), level, null);
                case LogLevel.None: return false;

                case LogLevel.Trace:
                case LogLevel.Debug:
                    return _log.IsDebugEnabled;

                case LogLevel.Information:
                    return _log.IsInfoEnabled;

                case LogLevel.Warning:
                    return _log.IsWarnEnabled;

                case LogLevel.Error:
                    return _log.IsErrorEnabled;

                case LogLevel.Critical:
                    return _log.IsFatalEnabled;
            }
        }

        public void Log<TState>(
            LogLevel level,
            EventId id,
            TState state,
            Exception error,
            Func<TState, Exception, string> Formatter)
        {
            if (Formatter is null)
            {
                throw new ArgumentNullException(nameof(Formatter));
            }

            if (!IsEnabled(level))
            {
                return;
            }

            var logMessage = Formatter(state, error);
            if (string.IsNullOrEmpty(logMessage) && error is null)
            {
                return;
            }

            switch (level)
            {
                default: throw new ArgumentOutOfRangeException(nameof(level), level, null);
                case LogLevel.None: break;

                case LogLevel.Trace:
                case LogLevel.Debug:
                    _log.Debug(logMessage);
                    break;

                case LogLevel.Information:
                    _log.Info(logMessage);
                    break;

                case LogLevel.Warning:
                    _log.Warn(logMessage);
                    break;

                case LogLevel.Error:
                    _log.Error(logMessage, error);
                    break;

                case LogLevel.Critical:
                    _log.Fatal(logMessage, error);
                    break;
            }
        }
    }
}