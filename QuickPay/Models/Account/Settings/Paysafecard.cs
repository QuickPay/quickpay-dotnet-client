using System.Collections.Generic;

namespace QuickPay.Models.Account.Settings
{
    public class Paysafecard
    {
        public bool active { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public List<string> currencies { get; set; }
    }
}