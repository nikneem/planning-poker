using System.IO;
using System.Linq;
using Certes;
using Certes.Acme;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace HexMaster.PlanningPoker.Spa
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {



    }

    public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {

      var certificatePath = Path.Combine(env.ContentRootPath, "certificate.pfx");

      var acme = new AcmeContext(WellKnownServers.LetsEncryptStagingV2);
      var account = await acme.NewAccount("credentialv@gmail.com", true);

      // Save the account key for later use
      var pemKey = acme.AccountKey.ToPem();
      var order = await acme.NewOrder(new[] { "www.planning-poker.net" });
      var authz = (await order.Authorizations()).First();
      var httpChallenge = await authz.Http();
      var keyAuthz = httpChallenge.KeyAuthz;
      await httpChallenge.Validate();


      var privateKey = KeyFactory.NewKey(KeyAlgorithm.ES256);
      var cert = await order.Generate(new CsrInfo
      {
        CountryName = "NL",
        State = "South-Holland",
        Locality = "Rijswijk",
        Organization = "HexMaster",
        OrganizationUnit = "Development",
        CommonName = "www.planning-poker.net",
      }, privateKey);
      var certPem = cert.ToPem();
      var pfxBuilder = cert.ToPfx(privateKey);
      var pfx = pfxBuilder.Build("Planning Poker SPA Certificate", "He#47&xX");
      using (var outStream = new FileStream("D:\\Temp\\cert.pfx", FileMode.Create, FileAccess.Write, FileShare.None))
      {
        outStream.Write(pfx, 0, pfx.Length);
      }

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
