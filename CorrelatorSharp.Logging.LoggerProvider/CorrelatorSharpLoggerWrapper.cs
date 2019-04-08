using System;
using Microsoft.Extensions.Logging;

namespace CorrelatorSharp.Logging.LoggerProvider
{
    public class CorrelatorSharpLoggerWrapper : Microsoft.Extensions.Logging.ILogger
    {
        private readonly CorrelatorSharp.Logging.ILogger _realLogger;
        private readonly IScopeProvider _scopeProvider;

        public CorrelatorSharpLoggerWrapper(CorrelatorSharp.Logging.ILogger realLogger, IScopeProvider scopeProvider)
        {
            _realLogger = realLogger;
            _scopeProvider = scopeProvider;
        }

        public void Log<TState>(
            LogLevel logLevel, 
            EventId eventId, 
            TState state, 
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    _realLogger.LogTrace(exception, formatter(state, exception), eventId);
                    break;
                case LogLevel.Debug:
                    _realLogger.LogDebug(exception, formatter(state, exception), eventId);
                    break;
                case LogLevel.Information:
                    _realLogger.LogInfo(exception, formatter(state, exception), eventId);
                    break;
                case LogLevel.Warning:
                    _realLogger.LogWarn(exception, formatter(state, exception), eventId);
                    break;
                case LogLevel.Error:
                    _realLogger.LogError(exception, formatter(state, exception), eventId);
                    break;
                case LogLevel.Critical:
                    _realLogger.LogFatal(exception, formatter(state, exception), eventId);
                    break;
                case LogLevel.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    return _realLogger.IsTraceEnabled;
                case LogLevel.Debug:
                    return _realLogger.IsDebugEnabled;
                case LogLevel.Information:
                    return _realLogger.IsInfoEnabled;
                case LogLevel.Warning:
                    return _realLogger.IsWarnEnabled;
                case LogLevel.Error:
                    return _realLogger.IsErrorEnabled;
                case LogLevel.Critical:
                    return _realLogger.IsFatalEnabled;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            }
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            try
            {
                return _scopeProvider.BeginScope(state);
            }
            catch (Exception ex)
            {
                _scopeProvider.ErrorBuildingScope(ex);
                return null;
            }
        }
    }
}