using System;

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
