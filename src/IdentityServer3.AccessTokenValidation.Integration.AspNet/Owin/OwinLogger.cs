using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace IdentityServer3.AccessTokenValidation.Integration.AspNet.Owin 
{
    internal class OwinLogger : Microsoft.Owin.Logging.ILogger
    {   
        private readonly Microsoft.Extensions.Logging.ILogger _logger;
        
        internal OwinLogger(Microsoft.Extensions.Logging.ILogger logger) 
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));   
            }
            
            _logger = logger;
        }
        
        public bool WriteCore(TraceEventType eventType, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
            _logger.Log(MapToLogLevel(eventType), eventId, state, exception, formatter);
            return true;
        }
        
        private LogLevel MapToLogLevel(TraceEventType eventType)
        {
            LogLevel logLevel;
            
            switch (eventType)
            {
                case TraceEventType.Critical:
                    logLevel = LogLevel.Critical;
                    break;
                    
                case TraceEventType.Error:
                    logLevel = LogLevel.Error;
                    break;
                    
                case TraceEventType.Information:
                    logLevel = LogLevel.Information;
                    break;
                    
                case TraceEventType.Resume:
                    logLevel = LogLevel.Verbose;
                    break;
                    
                case TraceEventType.Start:
                    logLevel = LogLevel.Verbose;
                    break;
                    
                case TraceEventType.Stop:
                    logLevel = LogLevel.Verbose;
                    break;
                    
                case TraceEventType.Suspend:
                    logLevel = LogLevel.Verbose;
                    break;
                    
                case TraceEventType.Transfer:
                    logLevel = LogLevel.Verbose;
                    break;
                    
                case TraceEventType.Verbose:
                    logLevel = LogLevel.Verbose;
                    break;
                    
                case TraceEventType.Warning:
                    logLevel = LogLevel.Warning;
                    break;
                    
                default:
                    logLevel = LogLevel.Information;
                    break;
            }
            
            return logLevel;
        }
    }
}