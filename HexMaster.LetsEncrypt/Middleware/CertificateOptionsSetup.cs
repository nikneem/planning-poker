using System;
using HexMaster.LetsEncrypt.Configuration;
using Microsoft.Extensions.Options;

namespace HexMaster.LetsEncrypt.Middleware
{
    public class CertificateOptionsSetup : IConfigureOptions<CertificateOptions>
    {
        // private IServiceProvider _services;
        private LetsEncryptConfiguration _acmeSettings;

        public CertificateOptionsSetup(IServiceProvider services, IOptions<LetsEncryptConfiguration> acmeSettings)
        {
            // _services = services;
            _acmeSettings = acmeSettings.Value;
        }

        public void Configure(CertificateOptions options)
        {
            // options.ApplicationServices = _services;
            options.Settings = _acmeSettings;
        }
    }
}
