namespace Quickpay.Models.Account.Settings
{
    public class Coinify
    {
        public bool active { get; set; }
        public string api_key { get; set; }
        public string api_secret { get; set; }
        public string ipn_secret { get; set; }
    }
}