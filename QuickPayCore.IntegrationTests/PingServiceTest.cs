using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quickpay.Services;

namespace QuickPay.IntegrationTests
{
    [TestClass]
    public class PingServiceTest
    {
        [TestMethod]
        public void TestConnectivity()
        {
            var service = new PingService(QpConfig.ApiKey);
            service.ping();
        }
    }
}
