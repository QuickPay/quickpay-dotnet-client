using System;
using System.Collections.Generic;

namespace Quickpay.Models
{
	public class Data
	{
	}
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

	public class Metadata
	{
		public string type { get; set; }
		public string brand { get; set; }
		public string last4 { get; set; }
		public int exp_month { get; set; }
		public int exp_year { get; set; }
		public string country { get; set; }
		public bool is_3d_secure { get; set; }
		public string hash { get; set; }
		public object number { get; set; }
		public string customer_ip { get; set; }
		public string customer_country { get; set; }
		public bool fraud_suspected { get; set; }
		public List<object> fraud_remarks { get; set; }
	}

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

	public class Payment 
	{
		public int id { get; set; }
		public string order_id { get; set; }
		public bool accepted { get; set; }
		public bool test_mode { get; set; }
		public string type { get; set; }
		public object text_on_statement { get; set; }
		public object branding_id { get; set; }
		public Dictionary<string, string> Variables { get; set; }
		public string acquirer { get; set; }
		public string currency { get; set; }
		public string state { get; set; }
		public List<Operation> operations { get; set; }
		public Metadata metadata { get; set; }
		public Link link { get; set; }
		public string created_at { get; set; }
		public int balance { get; set; }
	}
}

