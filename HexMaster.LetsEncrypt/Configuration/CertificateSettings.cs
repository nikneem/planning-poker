using System;
using System.Collections.Generic;
using System.Text;

namespace HexMaster.LetsEncrypt.Configuration
{
    public sealed class CertificateSettings
    {
        public const string SettingsSection = "Certificates";
        public string EmailAddress { get; set; }
        public List<string> DomainNames { get; set; }
        public string PfxPassword { get; set; }
        public bool AcceptTermsOfService { get; set; } = false;
    }
}
