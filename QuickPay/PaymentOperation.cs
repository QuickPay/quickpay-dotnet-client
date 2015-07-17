using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Quickpay
{
    public class PaymentOperation
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; set; }

        [JsonProperty(PropertyName = "pending")]
        public bool Pending { get; set; }

        [JsonProperty(PropertyName = "qp_status_code")]
        public string QpStatusCode { get; set; }

        [JsonProperty(PropertyName = "qp_status_msg")]
        public string QpStatusMsg { get; set; }

        [JsonProperty(PropertyName = "aq_status_code")]
        public string AqStatusCode { get; set; }

        [JsonProperty(PropertyName = "aq_status_msg")]
        public string AqStatusMsg { get; set; }

        [JsonProperty(PropertyName = "data")]
        public dynamic Data { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        [JsonConverter(typeof (IsoDateTimeConverter))]
        public DateTime CreatedAt { get; set; }
    }
}