using System;
using System.Collections.Generic;
using Quickpay.Models.Account.Settings;

namespace Quickpay.Models.Account
{
	public class Merchant
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
		public PciSettings pci { get; set; }
		public MerchantReseller reseller { get; set; }
		public string created_at { get; set; }
		public string suspended_at { get; set; }
		public string logging_stops_at { get; set; }
	}



	public class PciSettings
    {
		public bool saq_a { get; set; }
		public bool saq_a_ep { get; set; }
		public bool saq_b { get; set; }
		public bool saq_b_ip { get; set; }
		public bool saq_c { get; set; }
		public bool saq_c_vt { get; set; }
		public bool saq_d_merchant { get; set; }
		public bool saq_d_service_provider { get; set; }
		public bool saq_p2pe_hw { get; set; }
	}

	public class Economic
	{
		public bool active { get; set; }
		public string agreement { get; set; }
		public string agreement_token { get; set; }
	}

	public class IntegrationSettings
	{
		public Economic economic { get; set; }
	}

	public class MerchantReseller
	{
		public int id { get; set; }
		public string name { get; set; }
	}
}
