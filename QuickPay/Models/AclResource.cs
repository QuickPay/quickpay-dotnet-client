using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Quickpay
{
	[Obsolete ("This is an example class, please do not use in production")]
    public class AclResource
    {
        [JsonProperty(PropertyName = "account_type")]
        public AccountType AccountType { get; set; }

        [JsonProperty(PropertyName = "resource")]
        public string Resource { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "actions")]
        public List<HttpVerb> Actions { get; set; }
    }
}