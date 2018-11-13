using System;
using System.Collections.Generic;
using System.Text;

namespace HexMaster.LetsEncrypt.Configuration
{
    public class LetsEncryptConfiguration
    {

        public string EmailAddress { get; set; }
        public string TermsOfServiceUri { get; set; }
        public string CertificateName { get; set; }
        public string PfxPassword { get; set; }
        public string AcmeUri { get; set; }

        public LetsEncryptConfiguration()
        {
            AcmeUri = "https://acme-v01.api.letsencrypt.org/directory";
            TermsOfServiceUri = "https://letsencrypt.org/documents/LE-SA-v1.2-November-15-2017.pdf";
        }
    }
}
