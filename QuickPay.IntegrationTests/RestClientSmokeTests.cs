using System.Threading.Tasks;
using NUnit.Framework;
using Quickpay;

namespace QuickPay.IntegrationTests
{
    [TestFixture]
    public class RestClientSmokeTests
    {
		[Test]
		public void CanPingGetSync()
		{
			var sut = new PingExample(QpConfig.ApiKey);
			var result = sut.GetPing();
			StringAssert.Contains("Pong", result.Msg);
		}

		[Test]
		public async Task CanPingGetAsync()
		{
			var sut = new PingExample(QpConfig.ApiKey);
			var result = await sut.GetPingAsync();
			StringAssert.Contains("Pong", result.Msg);
		}

		[Test]
		public void CanPingPostSync()
		{
			var sut = new PingExample(QpConfig.ApiKey);
			var result = sut.PostPing();
			StringAssert.Contains("Pong", result.Msg);
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
            var sut = new MerchantExample(QpConfig.ApiKey);
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

			Assert.Pass();
        }

		[Test]
		public void GetsActivity()
		{
			var sut = new MerchantExample(QpConfig.ApiKey);
			var result = sut.Activity();

			Assert.AreNotEqual (0, result.Count);
		}

		[Test]
		public void GetsAcquirerOperationalStatus()
		{
			var sut = new MerchantExample(QpConfig.ApiKey);
			var result = sut.AcquirerOperationalStatus();

			Assert.AreNotEqual (0, result.Count);
		}
    }
}
