using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickPay.Services;

namespace QuickPay.IntegrationTests
{
    [TestClass]
    public class PingServiceTest
    {
        [TestMethod]
        public void TestConnectivity()
        {
            var service = new PingService(QpConfig.ApiKey);
            var result = service.ping();
            Assert.IsNotNull(result);
        }
    }
}
