using System;
using Quickpay.Models.Payments;

namespace QuickPay.RequestParams
{
    public class CapturePaymentRequestParams
    {
        public CapturePaymentRequestParams(int amount)
        {
            this.amount = amount;
        }

        public int amount { get; set; }
        public object? extras { get; set; }
    }
}