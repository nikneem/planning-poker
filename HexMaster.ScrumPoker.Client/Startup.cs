using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace HexMaster.ScrumPoker.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
      app.Use(async (context, next) =>
      {
        await next.Invoke();

        if (context.Response.StatusCode == 404)
        {
          context.Request.Path = new PathString("/index.html");
          await next.Invoke();
        }
      });
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseDefaultFiles();
      app.UseStaticFiles();

        }
    }
}