using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quickpay;

namespace QuickPay.IntegrationTests
{
    [TestClass]
    public class RestClientSmokeTests
    {
		[TestMethod]
		public void CanPingGetSync()
		{
			var sut = new PingExample(QpConfig.ApiKey);
			var result = sut.GetPing();
			StringAssert.Contains(result.Msg, "Pong");
		}

		[TestMethod]
		public async Task CanPingGetAsync()
		{
			var sut = new PingExample(QpConfig.ApiKey);
			var result = await sut.GetPingAsync();
			StringAssert.Contains(result.Msg, "Pong");
		}

		[TestMethod]
		public void CanPingPostSync()
		{
			var sut = new PingExample(QpConfig.ApiKey);
			var result = sut.PostPing();
			StringAssert.Contains(result.Msg, "Pong");
		}

		[TestMethod]
		public void CanGetAccountInformation()
		{
			var sut = new MerchantExample(QpConfig.ApiKey);
			var result = sut.Account();
			Assert.AreNotEqual(null, result);
		}

        [TestMethod]
        public void CanGetAclResources()
        {
            var sut = new MerchantExample(QpConfig.ApiKey);
			var result = sut.AclResources(AccountType.Merchant, new PageParameters{Page = 1, PageSize = 20});
            Assert.AreNotEqual(0, result.Count);
			Assert.IsNotNull(result[0].Description);
        }

        [TestMethod]
        public void GetsPayments()
        {
			var sut = new MerchantExample(QpConfig.ApiKey);
            var result = sut.Payments();

			Assert.AreNotEqual (0, result.Count);
        }

		[TestMethod]
        public void GetsAgreements()
        {
			var sut = new MerchantExample(QpConfig.ApiKey);
			var result = sut.Agreements(null, new SortingParameters{SortDirection = SortDirection.asc, SortBy = "id"});

			Assert.AreNotEqual (0, result.Count);
        }

		[TestMethod]
        public void GetsBrandings()
        {
			var sut = new MerchantExample(QpConfig.ApiKey);
			var result = sut.Branding();
        }

		[TestMethod]
		public void GetsActivity()
		{
			var sut = new MerchantExample(QpConfig.ApiKey);
			var result = sut.Activity();

			Assert.AreNotEqual (0, result.Count);
		}

		[TestMethod]
		public void GetsAcquirerOperationalStatus()
		{
			var sut = new MerchantExample(QpConfig.ApiKey);
			var result = sut.AcquirerOperationalStatus();

			Assert.AreNotEqual (0, result.Count);
		}
    }
}
