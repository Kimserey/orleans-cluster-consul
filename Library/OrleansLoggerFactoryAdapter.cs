using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using Orleans.Runtime;
using System;
using System.Net;

namespace Library
{
    public class OrleansLoggerFactoryAdapter : ILogConsumer
    {
        private readonly ILoggerFactory _loggerFactory;

        public OrleansLoggerFactoryAdapter(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public void Log(Severity severity, LoggerType loggerType, string caller, string message, IPEndPoint myIPEndPoint, Exception exception, int eventCode = 0)
        {
            var logger = _loggerFactory.CreateLogger(caller);
            var formattedLogValues = new FormattedLogValues("{LoggerType} - {IPEndpoint}: {Message}", loggerType, myIPEndPoint, message);
            logger.Log(Map(severity), eventCode, formattedLogValues, exception, (state, ex) => state.ToString());
        }

        private LogLevel Map(Severity severity)
        {
            switch (severity)
            {
                case Severity.Off:
                    return LogLevel.None;
                case Severity.Error:
                    return LogLevel.Error;
                case Severity.Warning:
                    return LogLevel.Warning;
                case Severity.Info:
                    return LogLevel.Information;
                case Severity.Verbose:
                    return LogLevel.Debug;
                case Severity.Verbose2:
                case Severity.Verbose3:
                    return LogLevel.Trace;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
