using HexMaster.LetsEncrypt.Configuration;
using HexMaster.LetsEncrypt.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace HexMaster.PlanningPoker.Api
{
    public class Startup
    {

        private readonly IConfiguration _configuration;

        public Startup(IConfiguration config)
        {
            _configuration = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();



            services.AddLetsEncrypt();
            services.AddMvcCore().AddJsonFormatters();
            services.AddOcelot(_configuration);
        }

        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            await app.UseOcelot();
        }
    }
}
