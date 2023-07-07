namespace Quickpay.Models.Payments
{
	public class PaymentLink
	{
		public string url { get; set; }
		public int agreement_id { get; set; }
		public string language { get; set; }
		public int amount { get; set; }
		public string continue_url { get; set; }
		public string cancel_url { get; set; }
		public string callback_url { get; set; }
		public string payment_methods { get; set; }
		public bool? auto_fee { get; set; }
		public bool? auto_capture { get; set; }
		public int? branding_id { get; set; }
		public string google_analytics_client_id { get; set; }
		public string google_analytics_tracking_id { get; set; }
		public string version { get; set; }
		public string acquirer { get; set; }
		public int? deadline { get; set; }
		public bool? framed { get; set; }
		public object branding_config { get; set; }
		public bool? invoice_address_selection { get; set; }
		public bool? shipping_address_selection { get;set; }
		public string customer_email { get; set; }
	}
}