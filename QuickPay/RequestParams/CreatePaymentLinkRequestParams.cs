namespace Quickpay.RequestParams
{
    public class CreatePaymentLinkRequestParams
    {
        public CreatePaymentLinkRequestParams(int amount)
        {
            this.amount = amount;
        }

        public int amount { get; set; }
        public string? agreement_id { get; set; }
        public string? language { get; set; }
        public string? continue_url { get; set; }
        public string? cancel_url { get; set; }
        public string? callback_url { get; set; }
        public string? payment_methods { get; set; }
        public bool? auto_fee { get; set; }
        public int? branding_id { get; set; }
        public string? google_analytics_tracking_id { get; set; }
        public string? google_analytics_client_id { get; set; }
        public string? acquirer { get; set; }
        public int? deadline { get; set; }
        public bool? framed { get; set; }
        public object? branding_config { get; set; }
        public string? fee_vat { get; set; }
        public bool? moto { get; set; }
        public string? customer_email { get; set; }
        public bool? invoice_address_selection { get; set; }
        public bool? shipping_address_selection { get; set; }
        public bool? auto_capture { get; set; }
        public string? auto_capture_at { get; set; }
    }
}
