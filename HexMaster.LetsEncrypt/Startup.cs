using System;
using System.Threading.Tasks;
using HexMaster.LetsEncrypt.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace HexMaster.LetsEncrypt
{
    public class Startup
    {

        public static async Task CertificateService(string[] args)
        {
            var certificateServer = CreateCertificateHostBuilder(args).Build();
            try
            {
                certificateServer.Start();
                var certificateManager = certificateServer.Services.GetRequiredService<ICertificateManager>();
                try
                {
                    var domains = new[] {
                        "api.planning-poker.net"
                    };

                    var certificate = await certificateManager.GetCertificate(domains);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);//, $"Exception getting certificate for domain {domains.First()}. PfxPassword configured incorrectly?");
                }
            }
            catch (Exception ex)
            {

            }

        }


        public static IWebHostBuilder CreateCertificateHostBuilder(string[] args)
        {
            //var config = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //    .AddUserSecrets<Program>(optional: true)
            //    .Build();

            //return WebHost.CreateDefaultBuilder(args)
            //    .UseConfiguration(config)
            //    .ConfigureServices(services =>
            //    {
            //        services.AddMemoryCache();

            //        var settingsSection = config.GetSection("CertificateManager");
            //        var certificateManagerSettings = settingsSection.Get<LetsEncryptConfiguration>();
            //        services.Configure<LetsEncryptConfiguration>(settingsSection);

            //        services.AddLetsEncrypt();
            //    })
            //    .PreferHostingUrls(false)
            //    .UseKestrel(options => { options.Listen(IPAddress.Any, 80); })
            //    .Configure(app => { app.UseCertificateResponse(); });
            return null;
        }

    }
}
