using System;
using HexMaster.LetsEncrypt.Configuration;
using HexMaster.LetsEncrypt.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HexMaster.LetsEncrypt.Middleware
{
   public static  class LetsEncryptMiddleware
    {

        public static void AddLetsEncrypt(this IServiceCollection services)
        {
            services.AddTransient<ICertificateManager, CertificateManager>();
        }
    }
}
