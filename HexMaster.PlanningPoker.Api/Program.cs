using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using HexMaster.LetsEncrypt.Configuration;
using HexMaster.LetsEncrypt.Contracts;
using HexMaster.LetsEncrypt.Middleware;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HexMaster.PlanningPoker.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Program>(optional: true)
                .Build();


            var loggerFactory = new LoggerFactory()
                .AddConsole(config.GetSection("Logging"))
                .AddDebug();
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogWarning($"Starting Webapp in {environment} environment...");

            X509Certificate2 certificate = null;
            var certificateServer = CreateCertificateHostBuilder(args, config).Build();
            try
            {
                await certificateServer.StartAsync().ConfigureAwait(false);
                 certificate = await AquireCertificates(certificateServer);
            }
            catch (Exception ex)
            {

            }

             await CreateWebHostBuilder(args, certificate).Build().RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args, X509Certificate2 certificate)
        {
            var settings = new CertificateSettings();
            return
                WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>()
                    .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config
                            .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                            .AddJsonFile("ocelot.json")
                            .AddEnvironmentVariables();
                    })
                    .UseKestrel(options =>
                    {
                        var ipAddresses = new List<IPAddress>();
                        var useHttps = false;
                        var port = 443;
                        foreach (var host in settings.DomainNames)
                        {
                            if (host == "localhost")
                            {
                                ipAddresses.Add(IPAddress.IPv6Loopback);
                                ipAddresses.Add(IPAddress.Loopback);
                            }
                            else if (IPAddress.TryParse(host, out var address))
                            {
                                ipAddresses.Add(address);
                                useHttps = certificate != null;
                                port = certificate == null ? 80 : 443;
                            }
                        }

                        foreach (var address in ipAddresses)
                        {
                            options.Listen(address, port, listenOptions =>
                            {
                                if (useHttps)
                                {
                                    listenOptions.UseHttps(certificate);
                                    listenOptions.NoDelay = false;
                                    // listenOptions.UseConnectionLogging();
                                }
                            });
                        }
                    });
        }

        public static IWebHostBuilder CreateCertificateHostBuilder(string[] args, IConfigurationRoot config) 
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(config)
                .ConfigureServices(services =>
                {
                    services.AddMemoryCache();

                    var settingsSection = config.GetSection(CertificateSettings.SettingsSection);
                    var certificateManagerSettings = settingsSection.Get<CertificateSettings>();
                    services.Configure<CertificateSettings>(settingsSection);

                    services.AddLetsEncrypt();
                })
                .PreferHostingUrls(false)
                .UseKestrel(options => { options.Listen(IPAddress.Any, 80); })
                .Configure(app => { app.UseCertificateResponse(); });
        }

        public static async Task<X509Certificate2> AquireCertificates(IWebHost certificatesHost, ILogger logger)
        {
            var certificateManager = certificatesHost.Services.GetRequiredService<ICertificateManager>();
            var settings = certificatesHost.Services.GetService<CertificateSettings>();
            

                X509Certificate2 certificate = null;
                        try
                        {
                            certificate = await certificateManager.GetCertificate(settings.DomainNames.ToArray());
                        }
                        catch (Exception e)
                        {
                            logger.LogCritical(e, $"Exception getting certificate for domain {settings.DomainNames.First()}. PfxPassword configured incorrectly?");
                        }

            return certificate;
        }
    }

    
}
