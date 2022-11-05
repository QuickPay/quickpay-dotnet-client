using Quickpay.Models.Payments;
using QuickPay.Models;
using System.Collections.Generic;

namespace Quickpay.RequestParams
{
    public class UpdateSubscriptionRequestParams
    {
        public string deadline_at { get; set; }
        public string[] group_ids { get; set; }
        public string description { get; set; }
        public OptionalAddress invoice_address { get; set; }
        public OptionalAddress shipping_address { get; set; }
        public List<object> variables { get; set; }
        public ThreedsV2 threeds_v2 { get; set; }
    }
}
