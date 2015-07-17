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

        internal void AddToDictionary(string basestr, Dictionary<string, object> param)
        {
            param.Add(basestr + "[" + "att" + "]", QuickpayClient.MakeEntry(Att, null));
            param.Add(basestr + "[" + "name" + "]", QuickpayClient.MakeEntry(Name, null));
            param.Add(basestr + "[" + "street" + "]", QuickpayClient.MakeEntry(Street, null));
            param.Add(basestr + "[" + "city" + "]", QuickpayClient.MakeEntry(City, null));
            param.Add(basestr + "[" + "zip_code" + "]", QuickpayClient.MakeEntry(ZipCode, null));
            param.Add(basestr + "[" + "region" + "]", QuickpayClient.MakeEntry(Region, null));
            param.Add(basestr + "[" + "country_code" + "]", QuickpayClient.MakeEntry(CountryCode, null));
            param.Add(basestr + "[" + "vat_no" + "]", QuickpayClient.MakeEntry(VatNo, null));
        }
    }
}