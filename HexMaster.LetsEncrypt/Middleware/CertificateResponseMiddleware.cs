using System.Threading.Tasks;
using HexMaster.LetsEncrypt.Contracts;
using Microsoft.AspNetCore.Http;

namespace HexMaster.LetsEncrypt.Middleware
{
    public class CertificateResponseMiddleware
    {
        static readonly PathString startPath = new PathString("/.well-known/acme-challenge");
        private readonly RequestDelegate next;
        // private readonly Func<string, Task<string>> challengeResponder;
        private readonly ICertificateManager certificateManager;

        public CertificateResponseMiddleware(RequestDelegate next, ICertificateManager certificateManager)
        {
            this.next = next;
            this.certificateManager = certificateManager;
        }

        public async Task Invoke(HttpContext context)
        {
            PathString requestPathId;
            PathString requestPath = context.Request.PathBase + context.Request.Path;
            if (requestPath.StartsWithSegments(startPath, out requestPathId))
            {
                string challenge = requestPathId.Value.TrimStart('/');

                string response =  certificateManager.GetChallengeResponse(challenge);

                if (!string.IsNullOrEmpty(response))
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsync(response);
                }
                else
                {
                    context.Response.StatusCode = 404;
                }
            }
            else
            {
                await this.next.Invoke(context);
            }
        }
    }
}
