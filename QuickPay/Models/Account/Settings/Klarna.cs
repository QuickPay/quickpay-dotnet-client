namespace Quickpay.Models.Account.Settings
{
    public class Klarna
    {
        public bool active { get; set; }
        public int eid { get; set; }
        public string shared_secret { get; set; }
    }
}