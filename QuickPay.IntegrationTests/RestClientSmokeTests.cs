using System;
using NUnit.Framework;
using Quickpay;

namespace QuickPay.IntegrationTests
{
    [TestFixture]
    public class RestClientSmokeTests
    {
        [Test]
        public void CanPingGetApiWithCredentials()
        {
            var sut = new QuickPayRestClient(QpConfig.Username, QpConfig.Password);
            var result = sut.Ping();
            StringAssert.Contains("Pong", result.Msg);
        }
        [Test]
        public void CanPingGetApiWithApiKey()
        {
            //var sut = new QuickPayRestClient(QpConfig.ApiKey);
            //var result = sut.Ping();
			Assert.Inconclusive ();
            //StringAssert.Contains("Pong", result.Msg);
        }

        [Test]
        public void CanGetAclResourcesAsync()
        {
            var sut = new QuickPayRestClient(QpConfig.Username, QpConfig.Password);
			var result = sut.AclResources(AccountType.Merchant, 1, 90);
            Assert.AreNotEqual(0, result.Count);
            Assert.NotNull(result[0].Description);
        }
    }
}
