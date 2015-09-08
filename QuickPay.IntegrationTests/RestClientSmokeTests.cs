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
        public void CanGetAclResources()
        {
            var sut = new QuickPayRestClient(QpConfig.Username, QpConfig.Password);
			var result = sut.AclResources(AccountType.Merchant, new PageParameters{Page = 1, PageSize = 20});
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

		[Test]
		public void GetsActivity()
		{
			var sut = new QuickPayRestClient(QpConfig.Username, QpConfig.Password);
			var result = sut.Activity();

			Assert.AreNotEqual (0, result.Count);
		}

		[Test]
		public void GetsAcquirerOperationalStatus()
		{
			var sut = new QuickPayRestClient(QpConfig.Username, QpConfig.Password);
			var result = sut.AcquirerOperationalStatus();

			Assert.AreNotEqual (0, result.Count);
		}
    }
}
