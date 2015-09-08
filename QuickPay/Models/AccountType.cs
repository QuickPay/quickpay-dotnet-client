using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Quickpay
{
	[Obsolete ("This is an example class, please do not use in production")]
    [JsonConverter(typeof (StringEnumConverter))]
    public enum AccountType
    {
        Any,
        Merchant,
        Reseller,
        Administrator,
        User
    }
}