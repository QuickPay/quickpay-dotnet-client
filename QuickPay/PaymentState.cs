using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Quickpay
{
    [JsonConverter(typeof (StringEnumConverter))]
    public enum PaymentState
    {
        Pending,
        New,
        Rejected,
        Processed
    }
}