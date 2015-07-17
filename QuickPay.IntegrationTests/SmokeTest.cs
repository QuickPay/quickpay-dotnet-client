using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Quickpay;

namespace QuickPay.IntegrationTests
{
    [TestFixture]
    public class SmokeTest
    {
        [Test]
        public async void CanPingGetApi()
        {
            var sut = new QuickpayClient(QpConfig.ApiKey);
            var result = await sut.Ping();
            Assert.IsTrue(result);
        }

        [Test]
        public async void CanPingPostApi()
        {
            var sut = new QuickpayClient(QpConfig.ApiKey);
            var result = await sut.Ping(true);
            Assert.IsTrue(result);
        }

        [Test]
        public async void CanGetAclResource()
        {
            var sut = new QuickpayClient(QpConfig.ApiKey);
            var result = await sut.GetAclResource();
            Assert.NotNull(result);
        }

        [Test]
        public async void CanGetGetPayments()
        {
            var sut = new QuickpayClient(QpConfig.ApiKey);
            var result = await sut.GetPayments();
            Assert.NotNull(result);
        }

    }
}
