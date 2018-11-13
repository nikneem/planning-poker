using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Certes;
using Certes.Acme;
using Certes.Acme.Resource;
using Certes.Pkcs;
using HexMaster.LetsEncrypt.Configuration;
using HexMaster.LetsEncrypt.Contracts;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace HexMaster.LetsEncrypt.Middleware
{
    public class CertificateManager : ICertificateManager
    {
        public IMemoryCache MemoryCache { get; }
        readonly LetsEncryptConfiguration options;
        public CertificateManager(IOptions<LetsEncryptConfiguration> options, IMemoryCache memoryCache)
        {
            MemoryCache = memoryCache;
            this.options = options.Value;
        }

        public string GetChallengeResponse(string challenge)
        {
            if (MemoryCache.TryGetValue(challenge, out object response))
            {
                return response.ToString();
            }

            return null;
        }

        public async Task<X509Certificate2> GetCertificate(string[] domainNames)
        {
            var acme = new AcmeContext(WellKnownServers.LetsEncryptStagingV2);
            var account = await acme.NewAccount(options.EmailAddress, true);
            var pemKey = acme.AccountKey.ToPem();
            var order = await acme.NewOrder(domainNames);
            var authz = (await order.Authorizations()).First();
            var httpChallenge = await authz.Http();
            var keyAuthz = httpChallenge.KeyAuthz;
            var authKey = keyAuthz.Split('.')[0];
            var entry = MemoryCache.CreateEntry(authKey);
            entry.Value = keyAuthz;
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);


            var privateKey = KeyFactory.NewKey(KeyAlgorithm.ES256);
            var cert = await order.Generate(new CsrInfo
            {
                CountryName = "NL",
                State = "South-Holland",
                Locality = "Rijswijk",
                Organization = "HexMaster",
                OrganizationUnit = "Development",
                CommonName = domainNames[0],
            }, privateKey);

            var pfxBuilder = cert.ToPfx(privateKey);
            var pfx = pfxBuilder.Build(options.CertificateName, options.PfxPassword);
            var certificate = new X509Certificate2(pfx);
            return certificate;
        }

        static async Task<byte[]> RequestNewCertificate(string[] domainNames, LetsEncryptConfiguration acmeSettings, Func<string, string, Task> challengeResponseReceiver)
        {
            using (var client = new AcmeClient(new Uri(acmeSettings.AcmeUri)))
            {
                // Create new registration
                var account = await client.NewRegistraton($"mailto:{acmeSettings.EmailAddress}");

                // Accept terms of services
                account.Data.Agreement = account.GetTermsOfServiceUri();
                account = await client.UpdateRegistration(account);

                bool unauthorizedDomain = false;
                // optimization: paralellize this
                // var result = Parallel.ForEach(domainNames, async (domaiName, state) => { state.Break(); });
                // await Task.WhenAll(domainNames.Select(async (domainName) => { await Task.FromResult(0); }));
                foreach (var domainName in domainNames)
                {

                    // Initialize authorization
                    var authz = await client.NewAuthorization(new AuthorizationIdentifier
                    {
                        Type = AuthorizationIdentifierTypes.Dns,
                        Value = domainName
                    });

                    // Comptue key authorization for http-01
                    var httpChallengeInfo = authz.Data.Challenges.Where(c => c.Type == ChallengeTypes.Http01).First();
                    var keyAuthString = client.ComputeKeyAuthorization(httpChallengeInfo);

                    // Do something to fullfill the challenge,
                    // e.g. upload key auth string to well known path, or make changes to DNS

                    // var cacheGrain = base.GrainFactory.GetGrain<ICacheGrain<string>>($"challenge:{httpChallengeInfo}");
                    // await cacheGrain.Set(new Immutable<string>(keyAuthString), TimeSpan.FromHours(2));
                    await challengeResponseReceiver(httpChallengeInfo.Token, keyAuthString);

                    // Info ACME server to validate the identifier
                    var httpChallenge = await client.CompleteChallenge(httpChallengeInfo);

                    // Check authorization status
                    int tryCount = 1;
                    do
                    {
                        // Wait for ACME server to validate the identifier
                        await Task.Delay(5000);
                        authz = await client.GetAuthorization(httpChallenge.Location);
                    }
                    while (authz.Data.Status == EntityStatus.Pending && ++tryCount <= 10);

                    if (authz.Data.Status != EntityStatus.Valid)
                    {
                        unauthorizedDomain = true;
                        break;
                    }
                }

                if (!unauthorizedDomain)
                {
                    // Create certificate
                    var csr = new CertificationRequestBuilder();
                    csr.AddName("CN", domainNames.First()); // "www.my_domain.com";
                    foreach (var alternativeName in domainNames.Skip(1))
                    {
                        csr.SubjectAlternativeNames.Add(alternativeName);
                    }
                    var cert = await client.NewCertificate(csr);

                    // Export Pfx
                    var pfxBuilder = cert.ToPfx();
                    var pfx = pfxBuilder.Build(domainNames.First(), acmeSettings.PfxPassword);

                    return pfx;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
