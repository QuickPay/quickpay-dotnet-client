namespace Quickpay.Models.Payments
{
    public class OptionalAddress
    {
		public string name { get; set; }
		public string att { get; set; }
		public string street { get; set; }
		public string city { get; set; }
		public string zip_code { get; set; }
		public string region { get; set; }
		public string country_code { get; set; }
		public string vat_no { get; set; }
		public string company_name { get; set; }
		public string house_number { get; set; }
		public string house_extension { get; set; }
		public string phone_number { get; set; }
		public string mobile_number { get; set; }
		public string email { get; set; }
    }
}