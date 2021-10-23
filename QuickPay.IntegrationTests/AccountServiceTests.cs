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
			var result = service.GET_Account();
			Assert.AreNotEqual(null, result);
		}

		[TestMethod]
		public void GET_PrivateKey_Test()
		{
			var service = new AccountService(QpConfig.ApiKey);
			var result = service.GET_PrivateKey();
			Assert.AreNotEqual(null, result);
		}

		[TestMethod]
		public void GET_04Platform_Test()
		{
			var service = new AccountService(QpConfig.ApiKey);
			var result = service.GET_04Platform();
			Assert.AreNotEqual(null, result);
		}
		
	}
}
