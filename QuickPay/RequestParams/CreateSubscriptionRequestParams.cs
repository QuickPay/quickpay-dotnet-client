using Quickpay.Models.Payments;
using System.Collections.Generic;

namespace Quickpay.RequestParams
{
    public class CreateSubscriptionRequestParams
    {
        public CreateSubscriptionRequestParams(string currency, string order_id, string description)
        {
            this.currency = currency;
            this.order_id = order_id;
            this.description = description;
        }

        public string order_id { get; set; }
        public string currency { get; set; }
        public string description { get; set; }
        public OptionalAddress invoice_address { get; set; }
        public OptionalAddress shipping_address { get; set; }
        public List<object> variables { get; set; }
        public int? branding_id { get; set; }
        public string text_on_statement { get; set; }
        public ShopSystem shopsystem { get; set; }
        public bool? unscheduled { get; set; }
    }
}
