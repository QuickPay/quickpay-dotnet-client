namespace Quickpay.Models.Account.Settings
{
    public class Valitor
    {
        public bool active { get; set; }
        public string identification_code { get; set; }
        public string identification_code_int { get; set; }
        public string business_code { get; set; }
        public bool recurring { get; set; }
        public bool americanexpress { get; set; }
        public bool dinersclub { get; set; }
        public bool securepay { get; set; }
        public string visa_mpi_merchant_id { get; set; }
        public string mastercard_mpi_merchant_id { get; set; }
        public string visa_bin { get; set; }
        public string mastercard_bin { get; set; }
    }
}