using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Quickpay
{
    // ReSharper disable InconsistentNaming
    [JsonConverter(typeof (StringEnumConverter))]
    public enum HttpVerb
    {
        GET,
        POST,
        DELETE,
        PUT,
        PATCH
    }
}