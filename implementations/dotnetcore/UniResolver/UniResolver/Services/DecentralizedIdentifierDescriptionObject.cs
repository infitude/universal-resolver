using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniResolver.Services
{
    /// <summary>
    /// https://github.com/WebOfTrustInfo/rebooting-the-web-of-trust-fall2016/blob/master/draft-documents/did-implementer-draft-10.md
    /// </summary>
    public class DecentralizedIdentifierDescriptionObject
    {
        [JsonProperty(PropertyName = "@context")]
        public string context { get; set; }
        public string id { get; set; }
        public string guardian { get; set; }
        public List<Owner> owner { get; set; }
        public List<Control> control { get; set; }
        public Service service { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public Signature signature { get; set; }
    }
    
    public class Owner
    {
        public string id { get; set; }
        public List<string> type { get; set; }
        public string curve { get; set; }
        public DateTime expires { get; set; }
        public string publicKeyBase64 { get; set; }
        public string publicKeyHex { get; set; }
    }

    public class Control
    {
        public string type { get; set; }
        public List<string> signer { get; set; }
    }

    public class Service
    {
        public string openid { get; set; }
        public string xdi { get; set; }
    }

    public class Signature
    {
        public string type { get; set; }
        public DateTime created { get; set; }
        public string creator { get; set; }
        public string signatureValue { get; set; }
    }
}