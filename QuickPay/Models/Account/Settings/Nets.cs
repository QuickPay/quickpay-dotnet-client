using System;

namespace Quickpay.Models.Account.Settings
{
	public class Nets
	{
		public bool active { get; set; }
		public string identification_code { get; set; }
		public int? business_code { get; set; }
		public bool recurring { get; set; }
		public bool fbg1886 { get; set; }
		public bool secured_by_nets { get; set; }
		[Obsolete("Depricated use Teller acquirer")]
		public string identification_code_int { get; set; }
		[Obsolete("Depricated use Teller acquirer")]
		public bool americanexpress { get; set; }
		[Obsolete("Depricated use Teller acquirer")]
		public bool dinersclub { get; set; }
		[Obsolete("Depricated use Teller acquirer")]
		public bool securepay { get; set; }
		[Obsolete("Depricated use Teller acquirer")]
		public string visa_mpi_merchant_id { get; set; }
		[Obsolete("Depricated use Teller acquirer")]
		public string mastercard_mpi_merchant_id { get; set; }
		[Obsolete("Depricated use Teller acquirer")]
		public string visa_bin { get; set; }
		[Obsolete("Depricated use Teller acquirer")]
		public string mastercard_bin {  get; set; }
	}
}