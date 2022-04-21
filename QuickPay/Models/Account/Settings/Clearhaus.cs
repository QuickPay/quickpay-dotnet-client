namespace Quickpay.Models.Account.Settings
{
	public class Clearhaus
	{
		public object active { get; set; }
		public string api_key { get; set; }
		public bool apple_pay { get; set; }
		public string business_code { get; set; }
		public bool recurring { get; set; }
		public bool payout { get; set; }
		public string mpi_merchant_id { get; set; }
	}
}
