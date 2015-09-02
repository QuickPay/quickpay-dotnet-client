using System;
using NUnit.Framework;
using Quickpay;

namespace QuickPay.IntegrationTests
{
    [TestFixture]
    public class RestClientSmokeTests
    {
        [Test]
        public async void CanPingGetApiWithCredentials()
        {
            var sut = new QuickPayRestClient(QpConfig.Username, QpConfig.Password);
            var result = await sut.PingAsync();
            StringAssert.Contains("Pong", result.Msg);
        }
        /*
        [Test]
        public async void CanPingGetApiWithApiKey()
        {
            var sut = new QuickPayRestClient(QpConfig.ApiKey);
            var result = await sut.PingAsync();
            StringAssert.Contains("Pong", result.Msg);
        }
        */

        [Test]
        public void CanGetAclResourcesAsync()
        {
            var sut = new QuickPayRestClient(QpConfig.Username, QpConfig.Password);
            var result = sut.AclResources();
            Assert.AreNotEqual(0, result.Count);
            Assert.NotNull(result[0].Description);
        }
    }
}
