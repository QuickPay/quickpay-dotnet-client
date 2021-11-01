using System;
using System.Collections.Generic;

namespace Quickpay.Models
{
	public class Payment
	{
		public int id { get; set; }
		public string ulid { get; set; }
		public int merchant_id { get; set; }
		public string order_id { get; set; }
		public bool accepted { get; set; }
		public string type { get; set; }
		public object text_on_statement { get; set; }
		public int branding_id { get; set; }
		public string variables { get; set; }
		public string currency { get; set; }
		public string state { get; set; }
		public Metadata metadata { get; set; }
		public Link link { get; set; } //TODO: Renamve type to PaymentLink
		//public OptionalAddress shipping_address { get; set; } TODO: Is OptionalAddress the same as address so we don't need 2 address types?
		//public OptionalAddress invoice_address { get; set; } TODO: Is OptionalAddress the same as address so we don't need 2 address types?
		//public Basket basket { get; set; } TODO: Create type Basket
		//public Shipping shipping { get; set; } TODO: Create type Shipping
		public List<Operation> operations { get; set; }
		public bool test_mode { get; set; }
		public string acquirer { get; set; }
		public string facilitator { get; set; }
		public string created_at { get; set; }
		public string updated_at { get; set; }
		public string retented_at { get; set; }
		public int balance { get; set; }
		public int fee { get; set; }
		public int subscription_id { get; set; }
		public string deadline_at { get; set; }
	}




	[Obsolete ("This is an example class, please do not use in production")]
	public class Operation
	{
		public int id { get; set; }
		public string type { get; set; }
		public int amount { get; set; }
		public bool pending { get; set; }
		public string qp_status_code { get; set; }
		public string qp_status_msg { get; set; }
		public string aq_status_code { get; set; }
		public string aq_status_msg { get; set; }
		public Data data { get; set; }
		public object callback_url { get; set; }
		public object callback_success { get; set; }
		public object callback_response_code { get; set; }
		public string created_at { get; set; }
	}


	[Obsolete ("This is an example class, please do not use in production")]
	public class Link
	{
		public string url { get; set; }
		public int agreement_id { get; set; }
		public string language { get; set; }
		public int amount { get; set; }
		public object continueurl { get; set; }
		public object cancelurl { get; set; }
		public string callbackurl { get; set; }
		public string payment_methods { get; set; }
		public bool autofee { get; set; }
		public bool autocapture { get; set; }
		public object branding_id { get; set; }
		public object google_analytics_client_id { get; set; }
		public object google_analytics_tracking_id { get; set; }
		public string version { get; set; }
		public object acquirer { get; set; }
		public object deadline { get; set; }
		public object vat_amount { get; set; }
		public object category { get; set; }
		public object reference_title { get; set; }
		public object product_id { get; set; }
		public object customer_email { get; set; }
	}

}

