using System;

namespace QuickPay.IntegrationTests
{
    internal static class QpConfig
    {
        public static string ApiKey
        {
            get => Environment.GetEnvironmentVariable("QUICKPAY_API_KEY");
        } 
    }
}
