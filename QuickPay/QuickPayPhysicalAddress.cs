using System.Collections.Generic;
using Newtonsoft.Json;

namespace Quickpay
{
    public class QuickPayPhysicalAddress
    {
        [JsonProperty(PropertyName = "att")]
        public string Att { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "street")]
        public string Street { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "zip_code")]
        public string ZipCode { get; set; }

        [JsonProperty(PropertyName = "region")]
        public string Region { get; set; }

        [JsonProperty(PropertyName = "country_code")]
        public string CountryCode { get; set; }

        [JsonProperty(PropertyName = "vat_no")]
        public string VatNo { get; set; }

       
    }
}