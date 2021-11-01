using System.Collections.Generic;

namespace Quickpay.Models.Payment
{
	public class Metadata
	{
		public string type { get; set; }
		public string origin { get; set; }
		public string brand { get; set; }
		public string bin { get; set; }
		public bool corporate { get; set; }
		public string last4 { get; set; }
		public int exp_month { get; set; }
		public int exp_year { get; set; }
		public string country { get; set; }
		public bool is_3d_secure { get; set; }
		//public string 3d_secure_type { get; set; } TODO: variables cannot start with a number, so this fiels must be parsed manually
		public string issued_to { get; set; }
		public string hash { get; set; }
		public object number { get; set; }
		public string customer_ip { get; set; }
		public string customer_country { get; set; }
		public bool fraud_suspected { get; set; }
		public List<string> fraud_remarks { get; set; }
		public bool fraud_reported { get; set; }
		public string fraud_report_description { get; set; }
		public string fraud_reported_at { get; set; }
		public string nin_number { get; set; }
		public string nin_country_code { get; set; }
		public string nin_gender { get; set; }
		public string shopsystem_name { get; set; }
		public string shopsystem_version { get; set; }
	}
}
