namespace Quickpay.Models.Account.Settings
{
    public class Vipps
    {
        public bool active { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string serial_number { get; set; }
        public string access_token_subscription_key { get; set; }
        public string ecommerce_subscription_key { get; set; }
        public string orgno { get; set; }
    }
}