using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quickpay.Services;

namespace QuickPay.IntegrationTests
{
    [TestClass]
    public class AccountServiceTests
    {

		[TestMethod]
		public void GET_Account_Test()
		{
			var service = new AccountService(QpConfig.ApiKey);
			var result = service.GetMerchantAccount();
			Assert.AreNotEqual(null, result);
		}

		[TestMethod]
		public void GET_PrivateKey_Test()
		{
			var service = new AccountService(QpConfig.ApiKey);
			var result = service.GetPrivateKeyOfMerchant();
			Assert.AreNotEqual(null, result);
		}

		[TestMethod]
		public void GET_04Platform_Test()
		{
			var service = new AccountService(QpConfig.ApiKey);
			var result = service.Get04PlatformSettings();
			Assert.AreNotEqual(null, result);
		}
		
	}
}
