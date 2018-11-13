using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace HexMaster.LetsEncrypt.Contracts
{
    public interface ICertificateManager
    {
        string GetChallengeResponse(string challenge);
        Task<X509Certificate2> GetCertificate(string[] domainNames);
    }
}
