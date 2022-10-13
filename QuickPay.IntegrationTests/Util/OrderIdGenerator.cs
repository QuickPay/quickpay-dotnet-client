using System;

namespace QuickPay.IntegrationTests.Util
{
    public static class OrderIdGenerator
    {
        public static string createRandomOrderId()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
        }
    }
}
