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
			var sut = new PingExample(QpConfig.Username, QpConfig.Password);
            var result = sut.Ping();
            StringAssert.Contains("Pong", result.Msg);
        }

		[Test]
		public void CanPingAsync()
		{
			var sut = new PingExample(QpConfig.Username, QpConfig.Password);
			var result = sut.PingAsync();
			StringAssert.Contains("Pong", result.Result.Msg);
		}

		[Test]
		public void CanPingPost()
		{
			var sut = new PingExample(QpConfig.Username, QpConfig.Password);
			var result = sut.PingPost();
			StringAssert.Contains("Pong", result.Msg);
		}

		[Test]
		public void CanPingAndGetDictionary()
		{
			var sut = new PingExample(QpConfig.Username, QpConfig.Password);
			var result = sut.PingDictionary();
			StringAssert.Contains("Pong", result["msg"]);
		}

		[Test]
		public void CanGetAccountInformation()
		{
			var sut = new MerchantExample(QpConfig.ApiKey);
			var result = sut.Account();
			Assert.AreNotEqual(null, result);
		}

        [Test]
        public void CanGetAclResources()
        {
            var sut = new MerchantExample(QpConfig.Username, QpConfig.Password);
			var result = sut.AclResources(AccountType.Merchant, new PageParameters{Page = 1, PageSize = 20});
            Assert.AreNotEqual(0, result.Count);
            Assert.NotNull(result[0].Description);
        }

        [Test]
        public void GetsPayments()
        {
			var sut = new MerchantExample(QpConfig.ApiKey);
            var result = sut.Payments();

			Assert.AreNotEqual (0, result.Count);
        }

		[Test]
        public void GetsAgreements()
        {
			var sut = new MerchantExample(QpConfig.ApiKey);
			var result = sut.Agreements(null, new SortingParameters{SortDirection = SortDirection.asc, SortBy = "id"});

			Assert.AreNotEqual (0, result.Count);
        }

		[Test]
        public void GetsBrandings()
        {
			var sut = new MerchantExample(QpConfig.ApiKey);
			var result = sut.Branding();

			Assert.AreNotEqual (0, result.Count);
        }

		[Test]
		public void GetsActivity()
		{
			var sut = new MerchantExample(QpConfig.Username, QpConfig.Password);
			var result = sut.Activity();

			Assert.AreNotEqual (0, result.Count);
		}

		[Test]
		public void GetsAcquirerOperationalStatus()
		{
			var sut = new MerchantExample(QpConfig.Username, QpConfig.Password);
			var result = sut.AcquirerOperationalStatus();

			Assert.AreNotEqual (0, result.Count);
		}
    }
}
