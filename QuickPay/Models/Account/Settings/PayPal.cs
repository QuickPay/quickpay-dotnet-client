using System.Collections.Generic;

namespace Quickpay.Models.Account.Settings
{
	public class PayPal
	{
		public bool active { get; set; }
		public bool recurring { get; set; }
		public bool credit_card { get; set; }
		public string token { get; set; }
		public string token_secret { get; set; }
		public List<string> scope { get; set; }
	}
}