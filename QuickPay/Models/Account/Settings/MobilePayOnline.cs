namespace Quickpay.Models.Account.Settings
{
    public class MobilePayOnline
    {
        public bool active { get; set; }
        public int? merchant_id { get; set; }
        public string delivery_limited_to { get; set; }
    }
}