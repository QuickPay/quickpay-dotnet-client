using System;
namespace QuickPay.RequestParams
{
    public class RefundPaymentRequestParams
    {
        public RefundPaymentRequestParams(int amount)
        {
            this.amount = amount;
        }

        public int amount { get; set; }
        public int? vat_rate { get; set; }
        public object extras { get; set; }
    }
}
