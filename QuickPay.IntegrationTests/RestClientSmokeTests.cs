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
        public async void CanGetAclResourcesAsync()
        {
            var sut = new QuickPayRestClient(QpConfig.Username, QpConfig.Password);
            var result = await sut.AclResourcesAsync(AccountType.Merchant, 1, 90);
            Assert.AreNotEqual(0, result.Count);
            Assert.NotNull(result[0].Description);
        }

        [Test]
        public void GetsPayments()
        {
			var sut = new QuickPayRestClient(QpConfig.ApiKey);
            var result = sut.Payments();

			Assert.AreNotEqual (0, result.Count);
        }
    }
}
