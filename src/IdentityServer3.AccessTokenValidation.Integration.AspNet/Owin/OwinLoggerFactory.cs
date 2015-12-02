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