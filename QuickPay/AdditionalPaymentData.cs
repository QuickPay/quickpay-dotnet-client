using Newtonsoft.Json;

namespace Quickpay
{
    public class AdditionalPaymentData
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "brand")]
        public string Brand { get; set; }

        [JsonProperty(PropertyName = "last4")]
        public string Last4 { get; set; }

        [JsonProperty(PropertyName = "exp_month")]
        public int? ExpMonth { get; set; }

        [JsonProperty(PropertyName = "exp_year")]
        public int? ExpYear { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "is_3d_secure")]
        public string Is_3DSecure { get; set; }

        [JsonProperty(PropertyName = "hash")]
        public string Hash { get; set; }

        [JsonProperty(PropertyName = "number")]
        public string Number { get; set; }

        [JsonProperty(PropertyName = "customer_ip")]
        public string CustomerIp { get; set; }

        [JsonProperty(PropertyName = "customer_country")]
        public string CustomerCountry { get; set; }
    }
}