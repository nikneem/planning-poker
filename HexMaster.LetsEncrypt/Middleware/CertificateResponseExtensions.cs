using Microsoft.AspNetCore.Builder;

namespace HexMaster.LetsEncrypt.Middleware
{
        public static class CertificateResponseExtensions
        {
            public static IApplicationBuilder UseCertificateResponse(this IApplicationBuilder builder)
            {
                return builder.UseMiddleware<CertificateResponseMiddleware>();
            }
        }
}
