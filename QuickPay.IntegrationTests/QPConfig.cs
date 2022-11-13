using System;
using QuickPay.IntegrationTests.Exceptions;

namespace QuickPay.IntegrationTests
{
    internal static class QpConfig
    {
        public static string ApiKey
        {
            get
            {
                string? apiKey = Environment.GetEnvironmentVariable("QUICKPAY_API_KEY");
                if (apiKey == null)
                {
                    throw new ApiKeyNotSpecifiedException();
                }
                return apiKey;
            }
        } 
    }
}
