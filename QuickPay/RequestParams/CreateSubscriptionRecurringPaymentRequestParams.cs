using System;
namespace QuickPay.RequestParams
{
    public class CreateSubscriptionRecurringPaymentRequestParams
    {
        public CreateSubscriptionRecurringPaymentRequestParams(int amount, string order_id)
        {
            this.amount = amount;
            this.order_id = order_id;
        }

        public int amount { get; set; }
        public string order_id { get; set; }


        public bool auto_capture { get; set; }
        public string auto_capture_at { get; set; }
        public bool autofee { get; set; }
        public bool zero_auth { get; set; }
        public string text_on_statement { get; set; }
        public int fee_vat { get; set; }
        public string description { get; set; }
    }
}