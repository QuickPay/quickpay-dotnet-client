using Newtonsoft.Json;

namespace Quickpay
{
    public class PaymentLink
    {
        [JsonProperty(PropertyName = "merchant_id")]
        public int MerchantId { get; set; }

        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "agreement_id")]
        public int AgreementId { get; set; }

        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }

        [JsonProperty(PropertyName = "subscription")]
        public int Subscription { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; set; }

        [JsonProperty(PropertyName = "continueurl")]
        public string ContinueUrl { get; set; }

        [JsonProperty(PropertyName = "branding_id")]
        public int? BrandingId { get; set; }

        [JsonProperty(PropertyName = "cancelurl")]
        public string CancelUrl { get; set; }

        [JsonProperty(PropertyName = "autofee")]
        public int? AutoFee { get; set; }

        [JsonProperty(PropertyName = "callbackurl")]
        public string CallbackUrl { get; set; }

        [JsonProperty(PropertyName = "payment_methods")]
        public string PaymentMethods { get; set; }

        [JsonProperty(PropertyName = "google_analytics_tracking_id")]
        public string GoogleAnalyticsTrackingId { get; set; }

        [JsonProperty(PropertyName = "google_analytics_client_id")]
        public string GoogleAnalyticsClientId { get; set; }

        [JsonProperty(PropertyName = "autocapture")]
        public int? AutoCapture { get; set; }

        [JsonProperty(PropertyName = "transaction_id")]
        public int TransactionId { get; set; }

        public string PaymentUrl(string baseurl = "https://payment.quickpay.net/payment-link/")
        {
            return baseurl + Token;
        }
    }
}