using System.Collections.Generic;
using QuickPay.Models.Payments;

namespace QuickPay.RequestParams
{
    public class UpdatePaymentRequestParams
    {
        public string deadline_at { get; set; }
        public OptionalAddress invoice_address { get; set; }
        public OptionalAddress shipping_address { get; set; }
        public Basket[] basket { get; set; }
        public Shipping shipping { get; set; }
        public List<object> variables { get; set; }
    }
}
