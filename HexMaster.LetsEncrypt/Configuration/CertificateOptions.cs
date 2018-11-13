using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HexMaster.LetsEncrypt.Configuration
{
    public class CertificateOptions
    {
        public LetsEncryptConfiguration Settings { get; set; }
        public Func<string, Task<string>> GetChallengeResponse { get; set; } = (challenge) => Task.FromResult<string>(null);
        public Func<string, string, Task> SetChallengeResponse { get; set; } = (challenge, response) => Task.FromResult(0);
        public Func<string, byte[], Task> StoreCertificate { get; set; } = (domainName, certData) => Task.FromResult(0);
        public Func<string, Task<byte[]>> RetrieveCertificate { get; set; } = (domainName) => Task.FromResult<byte[]>(null);
    }
}
