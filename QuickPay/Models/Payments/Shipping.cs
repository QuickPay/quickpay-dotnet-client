namespace Quickpay.Models.Payments
{
    public class Shipping
    {
        public string method { get; set; }
        public string company { get; set; }
        public int amount { get; set; }
        public double vat_rate { get; set; }
        public string tracking_number { get; set; }
        public string tracking_url { get; set; }
    }
}
