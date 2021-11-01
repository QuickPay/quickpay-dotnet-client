using System.Collections.Generic;

namespace Quickpay.Models.Payments
{
	public class Payment
	{
		public int id { get; set; }
		public string ulid { get; set; }
		public int merchant_id { get; set; }
		public string order_id { get; set; }
		public bool accepted { get; set; }
		public string type { get; set; }
		public string text_on_statement { get; set; }
		public int branding_id { get; set; }
		public string variables { get; set; }
		public string currency { get; set; }
		public string state { get; set; }
		public Metadata metadata { get; set; }
		public PaymentLink link { get; set; }
		public OptionalAddress shipping_address { get; set; }
		public OptionalAddress invoice_address { get; set; }
		public List<Basket> basket { get; set; }
		public Shipping shipping { get; set; }
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
}