using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPay.IntegrationTests
{
    internal static class QpConfig
    {
        public static string ApiKey
        {
            get { return Environment.GetEnvironmentVariable("QUICKPAY_API_KEY"); }
        }
        public static string Username
        {
            get { return Environment.GetEnvironmentVariable("QUICKPAY_USERNAME"); }
        }
         public static string Password 
        {
            get { return Environment.GetEnvironmentVariable("QUICKPAY_PASSWORD"); }
        }
 
    }
}
