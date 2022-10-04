namespace Quickpay.Models.Payments
{
    public class Basket
    {
        public int qty { get; set; }
        public string item_no { get; set; }
        public string item_name { get; set; }
        public double item_price { get; set; }
        public double vat_rate { get; set; }
    }
}
