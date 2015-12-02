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

namespace IdentityServer3.AccessTokenValidation.Integration.AspNet.Owin 
{
    internal class OwinLoggerFactory : Microsoft.Owin.Logging.ILoggerFactory
    {
        private readonly Microsoft.Extensions.Logging.ILoggerFactory _loggerFactory;
        
        internal OwinLoggerFactory(Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));   
            }
                
            _loggerFactory = loggerFactory;
        }
        
        public Microsoft.Owin.Logging.ILogger Create(string name)
        {
            var logger = _loggerFactory.CreateLogger(name);
            
            return new OwinLogger(logger); 
        }
    }
}