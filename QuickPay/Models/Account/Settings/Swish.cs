namespace Quickpay.Models.Account.Settings
{
    public class Swish
    {
        public bool active { get; set; }
        public string merchant_id { get; set; }
        public int cryptography_key_id { get; set; }
    }
}