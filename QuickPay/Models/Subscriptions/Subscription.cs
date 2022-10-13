using System.Collections.Generic;
using QuickPay.Models;

namespace Quickpay.Models.Payments
{
	public class Subscription
	{
		public int id { get; set; }
		public string ulid { get; set; }
		public int merchant_id { get; set; }
		public string order_id { get; set; }
		public bool accepted { get; set; }
		public string type { get; set; }
		public string text_on_statement { get; set; }
		public int? branding_id { get; set; }
		public object variables { get; set; }
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
		public string description { get; set; }
		public List<int> group_ids { get; set; }
		public ThreedsV2 threeds_v2 { get; set; }
		public Boolean? unscheduled { get; set; }
		public string deadline_at { get; set; }
    }
}