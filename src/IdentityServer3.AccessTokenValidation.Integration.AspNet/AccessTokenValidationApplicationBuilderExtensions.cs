using Owin;
using System;
using IdentityServer3.AccessTokenValidation.Integration.AspNet.Owin;
using Microsoft.AspNet.Builder;
using Microsoft.Owin.BuilderProperties;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNet.Hosting;
using Microsoft.Owin.Logging;

namespace IdentityServer3.AccessTokenValidation.Integration.AspNet 
{
    using DataProtectionProviderDelegate = Func<string[], Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>>;
    using DataProtectionTuple = Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>;

    public static class AccessTokenValidationApplicationBuilderExtensions 
	{
        public static void UseIdentityServerBearerTokenAuthentication(this IApplicationBuilder app, IdentityServerBearerTokenAuthenticationOptions options)
        {
            app.UseOwin(addToPipeline =>
            {   
                addToPipeline(next =>
                {
                    var builder = new Microsoft.Owin.Builder.AppBuilder();
                    var loggerFactory = app.ApplicationServices.GetService<Microsoft.Extensions.Logging.ILoggerFactory>();
                    var lifetime = app.ApplicationServices.GetService<IApplicationLifetime>();
                    var owinLoggerFactory = new OwinLoggerFactory(loggerFactory);
                    var provider = app.ApplicationServices.GetService(typeof(Microsoft.AspNet.DataProtection.IDataProtectionProvider)) as Microsoft.AspNet.DataProtection.IDataProtectionProvider;

                    var properties = new AppProperties(builder.Properties);
                    properties.OnAppDisposing = lifetime.ApplicationStopping;
                    properties.DefaultApp = next;

                    builder.SetLoggerFactory(owinLoggerFactory);
                    builder.Properties["security.DataProtectionProvider"] = new DataProtectionProviderDelegate(purposes =>
                    {
                        var dataProtection = provider.CreateProtector(string.Join(",", purposes));
                        return new DataProtectionTuple(dataProtection.Protect, dataProtection.Unprotect);
                    });
                    
                    builder.UseIdentityServerBearerTokenAuthentication(options);
                    return builder.Build(typeof(Func<IDictionary<string, object>, Task>)) as Func<IDictionary<string, object>, Task>;
                });
            });
        }
	}
}