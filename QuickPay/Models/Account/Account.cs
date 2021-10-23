using System;
using System.Collections.Generic;

namespace Quickpay.Models.Account
{
	public class Account
	{
		public int id { get; set; }
		public string type { get; set; }
		public string callback_url { get; set; }
		public string contact_email { get; set; }
		public string contact_phone { get; set; }
		public string shop_name { get; set; }
		[Obsolete("deprecated, use shop_urls instead")]
		public string shop_url { get; set; }
		public List<string> shop_urls { get; set; }
		public object shopsystem { get; set; }
		public string timezone { get; set; }
		public string locale { get; set; }
		public int default_branding_id { get; set; }
		public string default_payment_methods { get; set; }
		public string default_text_on_statement { get; set; }
		public string accounting_program { get; set; }
		public bool allow_test_transactions { get; set; }
		public bool autofee { get; set; }
		public object default_branding_config { get; set; }
		public Address customer_address { get; set; }
		public Address billing_address { get; set; }
		public AcquirerSettings acquirer_settings { get; set; }
		public IntegrationSettings integration_settings { get; set; }
		//public PciSettings pci { get; set; } // TODO: Create the PciSettings object
		public Reseller reseller { get; set; }
		public string created_at { get; set; }
		public string suspended_at { get; set; }
		public string logging_stops_at { get; set; }
	}





	public class Nets
	{
		public bool active { get; set; }
		public string identification_code { get; set; }
		public string identification_code_int { get; set; }
		public int business_code { get; set; }
		public object recurring { get; set; }
		public object americanexpress { get; set; }
		public object dinersclub { get; set; }
		public object fbg1886 { get; set; }
	}

	public class Paii
	{
		public object active { get; set; }
		public object merchant_id { get; set; }
		public object public_key { get; set; }
		public object secret_key { get; set; }
	}

	public class Clearhaus
	{
		public object active { get; set; }
		public object api_key { get; set; }
		public object recurring { get; set; }
	}

	public class Mobilepay
	{
		public object active { get; set; }
	}

	public class Paypal
	{
		public object active { get; set; }
		public object recurring { get; set; }
		public object credit_card { get; set; }
		public object token { get; set; }
		public object token_secret { get; set; }
		public List<object> scope { get; set; }
	}

	public class Viabill
	{
		public object active { get; set; }
		public object api_key { get; set; }
	}

	public class Sofort
	{
		public bool active { get; set; }
		public object customer_number { get; set; }
		public object project_id { get; set; }
		public object api_key { get; set; }
	}

	public class Economic
	{
		public object active { get; set; }
		public object agreement { get; set; }
		public object username { get; set; }
		public object password { get; set; }
	}

	public class IntegrationSettings
	{
		public Economic economic { get; set; }
	}

	public class Reseller
	{
		public int id { get; set; }
		public string name { get; set; }
	}
}
