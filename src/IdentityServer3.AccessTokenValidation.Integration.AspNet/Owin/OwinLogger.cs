/*
 * Copyright 2014, 2015 Dominick Baier, Brock Allen, Tugberk Ugurlu
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

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