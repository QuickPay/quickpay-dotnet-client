using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Quickpay
{
    public class Payment
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "order_id")]
        public string OrderId { get; set; }

        [JsonProperty(PropertyName = "accepted")]
        public bool Accepted { get; set; }

        [JsonProperty(PropertyName = "test_mode")]
        public bool TestMode { get; set; }

        [JsonProperty(PropertyName = "branding_id")]
        public int? BrandingId { get; set; }

        [JsonProperty(PropertyName = "variables")]
        public Dictionary<string, string> Variables { get; set; }

        [JsonProperty(PropertyName = "acquirer")]
        public string Acquirer { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "state")]
        public PaymentState State { get; set; }

        [JsonProperty(PropertyName = "operations")]
        public List<PaymentOperation> Operations { get; set; }

        [JsonProperty(PropertyName = "metadata")]
        public AdditionalPaymentData Metadata { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        [JsonConverter(typeof (IsoDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        [JsonProperty(PropertyName = "balance")]
        public int Balance { get; set; }
    }
}