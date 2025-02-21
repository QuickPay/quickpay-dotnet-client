using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickPay.Services;

namespace QuickPay.IntegrationTests
{
    [TestClass]
    public class AcquirersServiceTest
    {
        [TestMethod]
        public void GET_Acquirers_Test()
        {
            var service = new AcquirersService(QpConfig.ApiKey);
            var result = service.FetchAcquirers().Result;
            Assert.IsNotNull(result);
        }
    }
}
